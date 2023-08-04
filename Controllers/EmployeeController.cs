using EmployeeInfoWebAPI.Data;
using EmployeeInfoWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInfoWebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
           var employees = await _appDbContext.Employees.ToListAsync();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employeeRequest)
        {
            //here we are generating here the id by ourselves
            employeeRequest.Id = Guid.NewGuid();
            await _appDbContext.Employees.AddAsync(employeeRequest);
            await _appDbContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
         var employee = await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
       }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute]Guid id,Employee updateEmployeeRequest)
        {
           var employee = await _appDbContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Department = updateEmployeeRequest.Department;
            await _appDbContext.SaveChangesAsync();

            return Ok(employee);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task <IActionResult> DeleteEmployee( [FromRoute] Guid id )
        {
          var employee = await _appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _appDbContext.Employees.Remove(employee);
            await _appDbContext.SaveChangesAsync();
            return Ok(employee);


        }


    }
}
