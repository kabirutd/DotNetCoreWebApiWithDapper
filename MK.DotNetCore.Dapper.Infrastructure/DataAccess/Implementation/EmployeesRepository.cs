using Dapper;
using MK.DotNetCore.Dapper.ApplicationCore.Entities;
using MK.DotNetCore.Dapper.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MK.DotNetCore.Dapper.Infrastructure.DataAccess.Implementation
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public EmployeesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public int AddEmployee(Employees employees)
        {
            string procName = "spEmployeeInsert";
            var param = new DynamicParameters();
            int EmployeeId = 0;

            param.Add("@EmployeeId", employees.EmployeeID, null, ParameterDirection.Output);
            param.Add("@Title", employees.Title);
            param.Add("@TitleOfCourtesy", employees.TitleOfCourtesy);
            param.Add("@FirstName", employees.FirstName);
            param.Add("@LastName", employees.LastName);
            param.Add("@Address", employees.Address);
            param.Add("@City", employees.City);
            param.Add("@Region", employees.Region);
            param.Add("@PostalCode", employees.PostalCode);
            param.Add("@HomePhone", employees.HomePhone);
            param.Add("@Country", employees.Country);

            try
            {
                SqlMapper.Execute(_connectionFactory.GetConnection,
                procName, param, commandType: CommandType.StoredProcedure, commandTimeout:1800);

                EmployeeId = param.Get<int>("@EmployeeId");
            }
            finally
            {
                _connectionFactory.CloseConnection();
            }

            return EmployeeId;
        }

        public bool DeleteEmployee(int employeeId)
        {
            bool IsDeleted = true;
            var SqlQuery = @"DELETE FROM Employees WHERE EmployeeID = @Id";

            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                var rowsaffected = conn.Execute(SqlQuery, new { Id = employeeId });
                if (rowsaffected <= 0)
                {
                    IsDeleted = false;
                }
            }
            return IsDeleted;
        }

        public Employees GetEmployeesById(int empId)
        {
            var Employees = new Employees();
            var procName = "spEmployeesFetch";
            var param = new DynamicParameters();
            param.Add("@EmployeeId", empId);

            try
            {
                using (var multiResult = SqlMapper.QueryMultiple(_connectionFactory.GetConnection,
                procName, param, commandType: CommandType.StoredProcedure, commandTimeout:1800))
                {
                    Employees = multiResult.ReadFirstOrDefault<Employees>();
                    Employees.Territories = multiResult.Read<EmployeesTerritory>().ToList();
                }
            }
            finally
            {
                _connectionFactory.CloseConnection();
            }

            return Employees;
        }

        public IList<Employees> GetEmployeesByQuery()
        {
            var EmpList = new List<Employees>();
            var SqlQuery = @"SELECT [EmployeeID],[LastName],[FirstName],[Title],[TitleOfCourtesy],[City],[Country] FROM [Northwind].[dbo].[Employees]";

            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                var result = conn.Query<Employees>(SqlQuery, commandTimeout: 1800);
                return result.ToList();
            }
        }

        public bool UpdateEmployee(int EmployeeId, Employees employees)
        {
            string procName = "spEmployeeUpdate";
            var param = new DynamicParameters();
            bool IsSuccess = true;

            param.Add("@EmployeeId", EmployeeId, null, ParameterDirection.Input);
            param.Add("@Title", employees.Title);
            param.Add("@TitleOfCourtesy", employees.TitleOfCourtesy);
            param.Add("@FirstName", employees.FirstName);
            param.Add("@LastName", employees.LastName);
            param.Add("@Address", employees.Address);
            param.Add("@City", employees.City);
            param.Add("@Region", employees.Region);
            param.Add("@PostalCode", employees.PostalCode);
            param.Add("@HomePhone", employees.HomePhone);
            param.Add("@Country", employees.Country);

            try
            {
                var rowsAffected = SqlMapper.Execute(_connectionFactory.GetConnection,
                procName, param, commandType: CommandType.StoredProcedure);
                if (rowsAffected <= 0)
                {
                    IsSuccess = false;
                }
            }
            finally
            {
                _connectionFactory.CloseConnection();
            }

            return IsSuccess;
        }
    }
}
