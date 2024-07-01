using PersonManagement.Repos;
using PersonManagement.Repos.Models;
using PersonManagement.Service;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace PersonManagement.Container
{
    public class RefreshHandler : IRefreshHandler
    {
        private readonly SecurityContext context;
        public RefreshHandler(SecurityContext context) { 
            this.context = context;
        }
        public async Task<string> GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using(var randomnumbergenerator= RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string refreshtoken=Convert.ToBase64String(randomnumber);
                var Existtoken = this.context.Refreshtokens.FirstOrDefaultAsync(item=>item.Userid==username).Result;
                if (Existtoken != null)
                {
                    Existtoken._Refreshtoken = refreshtoken;
                }
                else
                {
                   await this.context.Refreshtokens.AddAsync(new Refreshtoken
                    {
                       Userid=username,
                       Tokenid= new Random().Next().ToString(),
                       _Refreshtoken=refreshtoken
                   });
                }
                await this.context.SaveChangesAsync();

                return refreshtoken;

            }
        }
    }
}
