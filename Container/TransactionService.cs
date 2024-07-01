using AutoMapper;
using PersonManagement.Helper;
using PersonManagement.Model;
using PersonManagement.Repos;
using PersonManagement.Repos.Models;
using PersonManagement.Service;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonManagement.Container
{
    public class TransactionService : ITransactionService
    {
        private readonly PersonAccountTransactionDataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionService> _logger;
        public TransactionService(PersonAccountTransactionDataContext context, IMapper mapper, ILogger<TransactionService> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<APIResponse> Create(Transaction data)
        {
            var transactionDateInvalid = (data.TransactionDate > DateTime.Now);
            var transactionAccount = this._context.Accounts.FirstOrDefault(a => a.Code == data.AccountCode);
            APIResponse response = new APIResponse();

            if (transactionAccount.StatusId == 2)
            {
                response.ResponseCode = 404;
                response.Message = string.Format("Transaction cannot be actioned as account is closed!", data.TransactionDate.ToString());
                return response;
            }

            if (transactionDateInvalid)
            {
                response.ResponseCode = 404;
                response.Message = string.Format("Transaction Date Cannot be in the future : {0}",data.TransactionDate.ToString());
                return response;
            }

            try
            {
                data.Capturedate = DateTime.Now;//User can never set capture date
                this._logger.LogInformation("Create Begins");
                transactionAccount.OutstandingBalance += data.Amount;
                this._context.Accounts.Update(transactionAccount);
                await this._context.Transactions.AddAsync(data);
                await this._context.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Message = "Transaction Created Successfully!.";
                response.Result = "pass";
            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.Message = ex.Message;
                this._logger.LogError(ex.Message, ex);
            }
            return response;
        }
        public async Task<List<Transaction>> GetAll(int code)
        {
            List<Transaction> response = new List<Transaction>();
            var acode = new SqlParameter("@aCode", code);
            var data = await _context.Transactions.FromSqlRaw<Transaction>("exec [dbo].[sp_GetAllTransactions] @acode", acode).ToListAsync();
           // var data = await this._context.Transactions.ToListAsync();
            if (data != null)
            {
                response = data;
            }
            return response;
        }

        public async Task<Transaction> GetByCode(int code)
        {
            Transaction response = new Transaction();
            var data = await this._context.Transactions.FindAsync(code);
            if (data != null)
            {
                response = data;
            }
            return response;
        }

        public async Task<APIResponse> Remove(int code)
        {
            APIResponse response = new APIResponse();
            var canDeleteTransaction = true;
            try
            {
                var Transaction = await this._context.Transactions.FirstAsync(p => p.Code == code);
                if (Transaction != null)
                {                    
                    if (canDeleteTransaction)
                    {
                        this._context.Transactions.Remove(Transaction);
                        await this._context.SaveChangesAsync();
                        response.ResponseCode = 200;
                        response.Message = "Transaction deleted successfully!";
                        response.Result = "pass";
                    }
                    else
                    {
                        response.ResponseCode = 404;
                        response.Message = "This Transaction cannot be deleted as there are transactions linked to it.";
                    }
                }
                else
                {
                    response.ResponseCode = 404;
                    response.Message = "Data not found";
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> Update(Transaction data, int code)
        {
            APIResponse response = new APIResponse();
            var transactionAccount = this._context.Accounts.FirstOrDefault(a=>a.Code==data.AccountCode);

            if (transactionAccount.StatusId==2)
            {
                response.ResponseCode = 404;
                response.Message = string.Format("Transaction cannot be actioned as account is closed!", data.TransactionDate.ToString());
                return response;
            }

            try
            {
                var Transaction = await this._context.Transactions.FindAsync(code);
                if (Transaction != null)
                {
                    //adjust account balance
                    if (Transaction.Amount > 0)
                    {
                        transactionAccount.OutstandingBalance = transactionAccount.OutstandingBalance - Math.Abs(Transaction.Amount);
                        transactionAccount.OutstandingBalance = transactionAccount.OutstandingBalance + data.Amount;
                    }
                    else if (Transaction.Amount < 0)
                    {
                        transactionAccount.OutstandingBalance = transactionAccount.OutstandingBalance +Math.Abs(Transaction.Amount);
                        transactionAccount.OutstandingBalance = transactionAccount.OutstandingBalance + data.Amount;
                    }
                    this._context.Accounts.Update(transactionAccount);
                    Transaction.Description = data.Description;
                    Transaction.Amount = data.Amount;                    

                    await this._context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Message = "Transaction Updated Successfully!";
                    response.Result = "pass";
                }
                else
                {
                    response.ResponseCode = 404;
                    response.Message = "Data not found";
                }

            }
            catch (Exception ex)
            {
                response.ResponseCode = 400;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
