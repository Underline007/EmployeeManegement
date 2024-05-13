using EmployeeManegement.Application.Dtos;
using EmployeeManegement.Application.Interfaces;
using EmployeeManegement.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManegement.Presentation.Controllers
{
    [Route("api/salaries")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ILogger<SalaryController> _logger;
        private readonly IGenericRepository<Salary> _salaryRepository;

        public SalaryController(IGenericRepository<Salary> salaryRepository, ILogger<SalaryController> logger)
        {
            _salaryRepository = salaryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalaries()
        {
            try
            {
                var salaries = await _salaryRepository.GetAll();
                return Ok(salaries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all salaries");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalaryById(Guid id)
        {
            try
            {
                var salary = await _salaryRepository.GetById(id);
                if (salary != null)
                {
                    return Ok(salary);
                }
                else
                {
                    return NotFound($"Can not find salary with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting salary by id");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalary(SalaryDto salaryDto)
        {
            try
            {
                var newSalary = new Salary
                {
                    EmployeeId = salaryDto.EmployeeId,
                    EmployeeSalary = salaryDto.EmployeeSalary
                };
                await _salaryRepository.Add(newSalary);
                return CreatedAtAction(nameof(GetSalaryById), new { id = newSalary.Id }, salaryDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating salary");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalary(Guid id, SalaryDto salaryDto)
        {
            try
            {
                var existingSalary = await _salaryRepository.GetById(id);
                if (existingSalary == null)
                {
                    return NotFound($"Salary with Id: {id} not found");
                }
                existingSalary.EmployeeSalary = salaryDto.EmployeeSalary;
                await _salaryRepository.Update(existingSalary);
                return Ok("Salary updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating salary");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalary(Guid id)
        {
            try
            {
                await _salaryRepository.Delete(id);
                return Ok("Salary deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting salary");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
