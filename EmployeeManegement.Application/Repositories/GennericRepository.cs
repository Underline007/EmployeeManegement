using EmployeeManegement.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManegement.Entities.Models;
using EmployeeManegement.Infrastructure.Data;
using EmployeeManegement.Application.Dtos;
using Microsoft.Data.SqlClient;

namespace EmployeeManegement.Application.Repositories
{
    public class GennericRepository<XClass> : IGenericRepository<XClass> where XClass : class
    {
        private readonly ApplicationDbContext _context;

        public GennericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(XClass entity)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Set<XClass>().AddAsync(entity);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    Save();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task Delete(Guid id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var entity = await _context.Set<XClass>().FindAsync(id);
                    if (entity != null)
                    {
                        _context.Set<XClass>().Remove(entity);
                        await _context.SaveChangesAsync();
                        transaction.Commit();
                        Save();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; 
                }
            }
        }

        public async Task Update(XClass entity)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    Save();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; 
                }
            }
        }

        public async Task<IEnumerable<XClass>> GetAll()
        {
            return await _context.Set<XClass>().ToListAsync();
        }

        public async Task<XClass> GetById(Guid id)
        {
            return await _context.Set<XClass>().FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<IEnumerable<EmployeeDepartmentDto>> GetAllEmployeesWithDepartment()
        {
            var query = @"
                        SELECT e.Id, e.Name, e.DepartmentId, e.JoinedDate, d.Name AS DepartmentName
                        FROM Employees e
                        INNER JOIN Departments d ON e.DepartmentId = d.Id";

            var result = await _context.Database.ExecuteSqlRawAsync(query);

            var employeesWithDepartment = await _context.Set<Employee>()
                .FromSqlRaw(query)
                .Select(e => new EmployeeDepartmentDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    DepartmentId = e.DepartmentId,
                    JoinedDate = e.JoinedDate,
                    DepartmentName = e.Department.Name
                }).ToListAsync();

            return employeesWithDepartment;
        }

        public async Task<IEnumerable<EmployeeProjectDto>> GetAllEmployeesWithProjects()
        {
            var query = @"
                        SELECT e.Id AS EmployeeId, e.Name AS EmployeeName, 
                               p.Id AS ProjectId, p.Name AS ProjectName
                        FROM Employees e
                        LEFT JOIN ProjectEmployees pe ON e.Id = pe.EmployeeId
                        LEFT JOIN Projects p ON pe.ProjectId = p.Id";

            var employeesWithProjects = await _context.Set<ProjectEmployee>()
                .FromSqlRaw(query)
                .Select(e => new EmployeeProjectDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Employee.Name,
                    ProjectId = e.ProjectId,
                    ProjectName = e.Project.Name
                    
                }).ToListAsync();

            return employeesWithProjects;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithSalaryAndJoinedDate(decimal minSalary, DateTime minJoinedDate)
        {
            var query = @"
                        SELECT e.*
                        FROM Employees e
                        INNER JOIN Salaries s ON e.Id = s.EmployeeId
                        WHERE s.EmployeeSalary > {0} AND e.JoinedDate >= {1}";

            var employeesWithSalaryAndJoinedDate = await _context.Employees
                .FromSqlRaw(query, minSalary, minJoinedDate)
                .ToListAsync();

            return employeesWithSalaryAndJoinedDate;
        }

    }
}
