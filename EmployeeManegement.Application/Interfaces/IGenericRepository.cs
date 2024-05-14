using EmployeeManegement.Application.Dtos;
using EmployeeManegement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManegement.Application.Interfaces
{
    public interface IGenericRepository<XClass> where XClass : class
    {
        Task<IEnumerable<XClass>> GetAll();
        Task<XClass> GetById(Guid id);
        Task Add(XClass entity);
        Task Update(XClass entity);
        Task Delete(Guid id);
        bool Save();
        Task<IEnumerable<EmployeeDepartmentDto>> GetAllEmployeesWithDepartment(); 
        Task<IEnumerable<EmployeeProjectDto>> GetAllEmployeesWithProjects(); 
        Task<IEnumerable<Employee>> GetEmployeesWithSalaryAndJoinedDate(decimal minSalary, DateTime minJoinedDate); 
    }
}
