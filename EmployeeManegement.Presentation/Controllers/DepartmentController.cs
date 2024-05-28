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
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IGenericRepository<Department> _departmentRepository;

        public DepartmentController(IGenericRepository<Department> departmentRepository, ILogger<DepartmentController> logger)
        {
            _departmentRepository = departmentRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentRepository.GetAll();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all departments");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            try
            {
                var department = await _departmentRepository.GetById(id);
                if (department != null)
                {
                    return Ok(department);
                }
                else
                {
                    return NotFound($"Can not find department with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting department by id");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentDto departmentDto)
        {
            try
            {
                var newDepartment = new Department
                {
                    Name = departmentDto.Name,
                };
                await _departmentRepository.Add(newDepartment);
                return CreatedAtAction(nameof(GetDepartmentById), new { id = newDepartment.Id }, departmentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating department");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, DepartmentDto departmentDto)
        {
            try
            {
                var existingDepartment = await _departmentRepository.GetById(id);
                if (existingDepartment == null)
                {
                    return NotFound($"Department with Id: {id} not found");
                }
                if(departmentDto.Name == "")
                {
                    return BadRequest($"Name is not valid");
                }
                existingDepartment.Name = departmentDto.Name;
                await _departmentRepository.Update(existingDepartment);
                return Ok("Department updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating department");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                await _departmentRepository.Delete(id);
                return Ok("Department deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting department");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
