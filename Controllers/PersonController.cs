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
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        public PersonController(IPersonService service) {
            this.personService = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Person person)
        {
            var data= await this.personService.Create(person);
            return Ok(data);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(int code)
        {
            var data = await this.personService.GetAll(code);
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
            var data = await this.personService.GetByCode(code);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(Person data, int code)
        {
            var person = await this.personService.Update(data, code);
            return Ok(person);
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int code)
        {
            var data = await this.personService.Remove(code);
            return Ok(data);
        }
    }
}
