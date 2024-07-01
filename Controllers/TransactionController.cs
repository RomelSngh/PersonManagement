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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        public TransactionController(ITransactionService service) {
            this.transactionService = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Transaction person)
        {
            var data= await this.transactionService.Create(person);
            return Ok(data);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int code)
        {
            var data = await this.transactionService.GetAll(code);
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
            var data = await this.transactionService.GetByCode(code);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Transaction data, int code)
        {
            var acc = await this.transactionService.Update(data, code);
            return Ok(acc);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int code)
        {
            var data = await this.transactionService.Remove(code);
            return Ok(data);
        }
    }
}
