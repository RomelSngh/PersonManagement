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
    public class PersonService : IPersonService
    {
        private readonly PersonAccountTransactionDataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;
        public PersonService(PersonAccountTransactionDataContext context,IMapper mapper,ILogger<PersonService> logger) { 
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<APIResponse> Create(Person data)
        {
            var idnumberExists = this._context.Persons.Any<Person>(p => p.IdNumber == data.IdNumber && p.Code != data.Code);

            APIResponse response = new APIResponse();

            if (idnumberExists)
            {
                response.ResponseCode = 404;
                response.Message = string.Format("Identity Number {0} already exists for another person.", data.IdNumber);
                return response;
            }

            try
            {
                this._logger.LogInformation("Create Begins");
                //Person _Person = this.mapper.Map<Personmodal, Person>(data);
                await this._context.Persons.AddAsync(data);
                await this._context.SaveChangesAsync();
                response.ResponseCode = 201;
                response.Message = "Person Created Successfully!.";
                response.Result = "pass";
            }
            catch(Exception ex)
            {
                response.ResponseCode = 400;
                response.Message = ex.Message;
                this._logger.LogError(ex.Message,ex);
            }
            return response;
        }
        public async Task<List<Person>> GetAll(int code=0)
        { 
            List<Person> response=new List<Person>();
            var pcode = new SqlParameter("@Code", code);
            var data = await this._context.Persons.FromSqlRaw<Person>("exec [dbo].[sp_GetAllPersons] @code", pcode).ToListAsync();
            //var data = await this._context.Persons.ToListAsync();
            if(data != null )
            {
                response=data;
            }
            return response;
        }

        public async Task<Person> GetByCode(int code)
        {
            Person response = new Person();
            var data = await this._context.Persons.FindAsync(code);
            if (data != null)
            {
                response = data;
            }
            return response;
        }

        public async Task<APIResponse> Remove(int code)
        {
            APIResponse response = new APIResponse();
            var canDeletePerson = true; 
            try
            {
                var person = await this._context.Persons.Include(p=>p.Accounts).FirstAsync(p=>p.Code==code);
                if(person != null)
                {

                    if (person.Accounts != null)
                    {
                        if (person.Accounts.Count > 0)
                        {
                            canDeletePerson = false;
                            //if theres any accounts with an outstanding balance we cannot delete this person
                            canDeletePerson = !person.Accounts.Any<Account>(a => a.OutstandingBalance > 0);
                        }
                    }
                    else canDeletePerson = true;//if accounts collection is null person can be deleted. 

                    if (canDeletePerson)
                    {
                        this._context.Persons.Remove(person);
                        await this._context.SaveChangesAsync();
                        response.ResponseCode = 200;
                        response.Message = "Person deleted successfully!";
                        response.Result = "pass";
                    }
                    else {
                        response.ResponseCode = 404;
                        response.Message = "This person cannot be deleted as there are accounts opened.";
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

        public async Task<APIResponse> Update(Person data, int code)
        {
            var idnumberExists = this._context.Persons.Any<Person>(p => p.IdNumber == data.IdNumber && p.Code != data.Code);

            APIResponse response = new APIResponse();

            if (idnumberExists)
            {
                response.ResponseCode = 404;
                response.Message = string.Format("Identity Number {0} already exists for another person.",data.IdNumber);
                return response;
            }

            try
            {
                var person = await this._context.Persons.FindAsync(code);
                if (person != null)
                {
                    person.Name = data.Name;
                    person.Surname = data.Surname;
                    person.IdNumber=data.IdNumber;
                    await this._context.SaveChangesAsync();
                    response.ResponseCode = 200;
                    response.Message = "Person Updated Successfully!";
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
