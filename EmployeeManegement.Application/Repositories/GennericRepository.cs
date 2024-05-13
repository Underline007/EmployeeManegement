using EmployeeManegement.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManegement.Entities.Models;
using EmployeeManegement.Infrastructure.Data;

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
                catch (Exception ex)
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

        public async Task<IEnumerable<Employee>> GetAllEmployeesWithDepartment()
        {
            var employeesWithDepartment = await (from employee in _context.Set<Employee>()
                                                 join department in _context.Set<Department>() on employee.DepartmentId equals department.Id
                                                 select new
                                                 {
                                                     Employee = employee,
                                                     DepartmentName = department.Name
                                                 }).ToListAsync();

            return employeesWithDepartment.Select(x => x.Employee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesWithProjects()
        {
            var employeesWithProjects = await (from employee in _context.Set<Employee>()
                                               join projectEmployees in _context.Set<ProjectEmployee>() on employee.Id equals projectEmployees.EmployeeId into groupEmployeeProject
                                               from project in groupEmployeeProject.DefaultIfEmpty()
                                               select new
                                               {
                                                   Employee = employee,
                                                   Project = project != null ? project.Project : null
                                               }).ToListAsync();

            return employeesWithProjects.Select(x => x.Employee);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithSalaryAndJoinedDate(decimal minSalary, DateTime minJoinedDate)
        {
            var employeesWithSalaryAndJoinedDate = await (from employee in _context.Set<Employee>()
                                                          where employee.Salary.EmployeeSalary > minSalary && employee.JoinedDate >= minJoinedDate
                                                          select employee).ToListAsync();

            return employeesWithSalaryAndJoinedDate;
        }

    }
}
