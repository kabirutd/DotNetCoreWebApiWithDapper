using MK.DotNetCore.Dapper.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MK.DotNetCore.Dapper.Infrastructure.DataAccess.Interface
{
    public interface IEmployeesRepository
    {
        IList<Employees> GetEmployeesByQuery();
        Employees GetEmployeesById(int employeeId);
        int AddEmployee(Employees employees);
        bool UpdateEmployee(int employeeId, Employees employees);
        bool DeleteEmployee(int employeeId);
    }
}
