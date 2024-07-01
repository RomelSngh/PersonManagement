using PersonManagement.Model;
using PersonManagement.Repos.Models;
using PersonManagement.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace PersonManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService service) {
            this.accountService = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Account person)
        {
            var data= await this.accountService.Create(person);
            return Ok(data);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int pcode)
        {
            var data = await this.accountService.GetAll(pcode);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpGet("GetAllAccountStatuses")]
        public async Task<IActionResult> GetAllAccountStatuses()
        {
            var data = await this.accountService.GetAllAccountStatuses();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [DisableRateLimiting]
        [HttpGet("GetByCode")]
        public async Task<IActionResult> GetByCode(int code)
        {
            var data = await this.accountService.GetByCode(code);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Account data, int code)
        {
            var acc = await this.accountService.Update(data, code);
            return Ok(acc);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int code)
        {
            var data = await this.accountService.Remove(code);
            return Ok(data);
        }
    }
}
