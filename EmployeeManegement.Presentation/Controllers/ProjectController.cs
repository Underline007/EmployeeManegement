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
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IGenericRepository<Project> _projectRepository;

        public ProjectController(IGenericRepository<Project> projectRepository, ILogger<ProjectController> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _projectRepository.GetAll();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all projects");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            try
            {
                var project = await _projectRepository.GetById(id);
                if (project != null)
                {
                    return Ok(project);
                }
                else
                {
                    return NotFound($"Can not find project with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting project by id");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectDto  projectDto)
        {
            try
            {
                var newProject = new Project
                {
                    
                    Name = projectDto.Name
            
                };
                await _projectRepository.Add(newProject);
                return CreatedAtAction(nameof(GetProjectById), new { id = newProject.Id }, projectDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating project");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, ProjectDto projectDto)
        {
            try
            {
              
                var existingProject = await _projectRepository.GetById(id);
                if (existingProject == null)
                {
                    return NotFound($"Project with Id: {id} not found");
                }
                if (projectDto.Name == "")
                {
                    return BadRequest($"Name is not valid");
                }
                existingProject.Name = projectDto.Name;
                await _projectRepository.Update(existingProject);
                return Ok("Project edit successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating project");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            try
            {
                await _projectRepository.Delete(id);
                return Ok("Project delete successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting project");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
