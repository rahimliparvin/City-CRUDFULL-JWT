﻿using Services.DTOs.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int? id);
        Task CreateAsync(EmployeeCreateDto employee);
        Task DeleteAsync(int? id);
        Task UpdateAsync(int id, EmployeeUpdateDto employee);
        Task<IEnumerable<EmployeeDto>> SearchAsync(string? searchText);
        Task SoftDeleteAsync(int? id);
    }
}
