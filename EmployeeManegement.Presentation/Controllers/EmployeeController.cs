using EmployeeManegement.Application.Dtos;
using EmployeeManegement.Application.Interfaces;
using EmployeeManegement.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManegement.Presentation.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IGenericRepository<Employee> _employeeRepository;

        public EmployeeController(IGenericRepository<Employee> employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetAll();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all employees");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            try
            {
                var employee = await _employeeRepository.GetById(id);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound($"Cannot find employee with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting employee by id");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var newEmployee = new Employee
                {
                    Name = employeeDto.Name,
                    DepartmentId = employeeDto.DepartmentId,
                    JoinedDate = employeeDto.JoinedDate,
                    EmployeeProject = employeeDto.ProjectIds?.Select(id => new ProjectEmployee { ProjectId = id }).ToList()
                };

                await _employeeRepository.Add(newEmployee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, employeeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating employee");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeDto employeeDto)
        {
            try
            {
                var existingEmployee = await _employeeRepository.GetById(id);
                if (existingEmployee == null)
                {
                    return NotFound($"Employee with Id: {id} not found");
                }
                if (string.IsNullOrWhiteSpace(employeeDto.Name))
                {
                    return BadRequest("Name must not be null or empty");
                }

                existingEmployee.Name = employeeDto.Name;
                existingEmployee.DepartmentId = employeeDto.DepartmentId;
                existingEmployee.JoinedDate = employeeDto.JoinedDate;
                existingEmployee.EmployeeProject = employeeDto.ProjectIds?.Select(pid => new ProjectEmployee { ProjectId = pid, EmployeeId = id }).ToList();

                await _employeeRepository.Update(existingEmployee);
                return Ok("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating employee");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                await _employeeRepository.Delete(id);
                return Ok("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting employee");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("attachdepartment")]
        public async Task<IActionResult> GetAllEmployeesWithDepartment()
        {
            try
            {
                var employeesWithDepartment = await _employeeRepository.GetAllEmployeesWithDepartment();
                return Ok(employeesWithDepartment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all employees with department");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("attachprojects")]
        public async Task<IActionResult> GetAllEmployeesWithProjects()
        {
            try
            {
                var employeesWithProjects = await _employeeRepository.GetAllEmployeesWithProjects();
                return Ok(employeesWithProjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all employees with projects");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("filtersalaryandjoineddate")]
        public async Task<IActionResult> GetEmployeesWithSalaryAndJoinedDate(decimal minSalary, DateTime minJoinedDate)
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesWithSalaryAndJoinedDate(minSalary, minJoinedDate);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting employees with salary and joined date");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
