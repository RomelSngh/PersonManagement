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
    public class AccountService : IAccountService
    {
        private readonly PersonAccountTransactionDataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;
        public AccountService(PersonAccountTransactionDataContext context, IMapper mapper, ILogger<AccountService> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<APIResponse> Create(Account data)
        {
            var accountExists = this._context.Accounts.Any<Account>(p => p.AccountNumber == data.AccountNumber && p.Code != data.Code);

            APIResponse response = new APIResponse();

            if (accountExists)
            {
                response.ResponseCode = 404;
                response.Message = string.Format("Account Number {0} already exists for another Account.", data.AccountNumber);
                return response;
            }

            try
            {
                this._logger.LogInformation("Create Begins");
                //Account _Account = this.mapper.Map<Accountmodal, Account>(data);
                await this._context.Accounts.AddAsync(data);
                await this._context.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Message = "Account Created Successfully!.";
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
        public async Task<List<Account>> GetAll(int code)
        {
            List<Account> response = new List<Account>();
            var pcode = new SqlParameter("@pCode",code);
            var data = await this._context.Accounts.FromSqlRaw<Account>("exec [dbo].[sp_GetAllAccounts] @pcode",pcode).ToListAsync();
            //var data = await this._context.Accounts.ToListAsync();
            if (data != null)
            {
                response = data;
            }
            return response;
        }

        public async Task<List<AccountStatus>> GetAllAccountStatuses()
        {
            List<AccountStatus> response = new List<AccountStatus>();
            var data = await this._context.AccountStatuses.ToListAsync();
            if (data != null)
            {
                response = data;
            }
            return response;
        }


        public async Task<Account> GetByCode(int code)
        {
            Account response = new Account();
            var data = await this._context.Accounts.FindAsync(code);
            if (data != null)
            {
                response = data;
            }
            return response;
        }

        public async Task<APIResponse> Remove(int code)
        {
            APIResponse response = new APIResponse();
            var canDeleteAccount = true;
            try
            {
                var Account = await this._context.Accounts.Include(p => p.Transactions).FirstAsync(p => p.Code == code);
                if (Account != null)
                {

                    if (Account.Transactions != null)
                    {
                        if (Account.Transactions.Count > 0)
                        {
                            canDeleteAccount = false;
                        }
                    }
                    else canDeleteAccount = true;//if accounts collection is null Account can be deleted. 

                    if (canDeleteAccount)
                    {
                        this._context.Accounts.Remove(Account);
                        await this._context.SaveChangesAsync();
                        response.ResponseCode = 200;
                        response.Message = "Account deleted successfully!";
                        response.Result = "pass";
                    }
                    else
                    {
                        response.ResponseCode = 404;
                        response.Message = "This Account cannot be deleted as there are transactions linked to it.";
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

        public async Task<APIResponse> Update(Account data, int code)
        {
           APIResponse response = new APIResponse();         
            try
            {
                var Account = await this._context.Accounts.FindAsync(code);
                if (Account != null)
                {
                    if (data.StatusId == 2 && Account.StatusId == 1) //attempting to close an account with an outstanding balance
                    {
                        if (Account.OutstandingBalance != 0) {
                            response.ResponseCode = 404;
                            response.Message = string.Format("Account Number {0} has an outstanding balance and cannot be closed.", data.AccountNumber);
                            return response;
                        }
                    }
                    

                    Account.AccountNumber = data.AccountNumber;
                    Account.PersonCode = data.PersonCode;
                    Account.StatusId = data.StatusId;
                    await this._context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Message = "Account Updated Successfully!";
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
