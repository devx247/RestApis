using System.Collections.Generic;
using CodeFirstRestApi.Models;
using CodeFirstRestApi.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstRestApi.Controllers
{
    [Route("api/employee")]
    [ApiController, Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IDataRepository<Employee> _dataRepository;

        public EmployeeController(IDataRepository<Employee> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/employee
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Employee> employees = _dataRepository.GetAll();
            return Ok(employees);
        }


        // GET: api/employee/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            Employee employee = _dataRepository.Get(id);

            if (employee == null) return NotFound("The Employee record couldn't be found.");

            return Ok(employee);
        }

        // POST: api/employee
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            if (employee == null) return BadRequest("Employee is null.");

            _dataRepository.Add(employee);

            return CreatedAtRoute("Get", new { Id = employee.Id }, employee);
        }

        // PUT: api/employee/5
        [HttpPut]
        public IActionResult Put(long id, [FromBody] Employee employee)
        {
            if (employee == null) return BadRequest("Employee is null.");

            Employee employeeToUpdate = _dataRepository.Get(id);

            if (employeeToUpdate == null) return NotFound($"Employee with id {id} not found in DB.");

            _dataRepository.Update(employeeToUpdate, employee);
            return NoContent();
        }

        // DELETE: api/employee/5
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            Employee employee = _dataRepository.Get(id);

            if (employee == null) return NotFound($"Employee with id {id} not found in DB.");

            _dataRepository.Delete(employee);
            return NoContent();
        }
    }
}