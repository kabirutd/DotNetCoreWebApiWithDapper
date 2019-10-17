using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MK.DotNetCore.Dapper.ApplicationCore.Entities;
using MK.DotNetCore.Dapper.ApplicationCore.Interfaces;

namespace MK.DotNetCore.Dapper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;
        private readonly IAsyncRepository<Employees> _employeeRepository;

        public EmployeeController(ILogger<EmployeeController> logger, IAsyncRepository<Employees> employeeRepository)
        {
            _logger = logger;
            _employeeRepository = employeeRepository;
        }
        // GET: api/Employee
        [HttpGet]
        public async Task<IReadOnlyList<Employees>> Get()
        {
            return await _employeeRepository.ListAllAsync();
        }
    
        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<Employees> Get(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }
      
        // POST: api/Employee
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]      
        public async Task<string> Delete(int id)
        //public async Task<string> Delete([FromUri] int id, [FromBody] String name)            
        {
            if (id <= 0)
                return BadRequest("Not a valid student id").ToString();
            var deleted = await _employeeRepository.DeleteByIdAsync(id);
            return "Id " + id + " was deleted successfully!";

        }
        //public async Task<bool> Delete(int id)
        //{

        //    if (id <= 0)
        //        return false; // BadRequest("Not a valid student id");

        //    var deleted = await _employeeRepository.DeleteByIdAsync(id);

        //    return deleted;

           
        //}
    }
}
