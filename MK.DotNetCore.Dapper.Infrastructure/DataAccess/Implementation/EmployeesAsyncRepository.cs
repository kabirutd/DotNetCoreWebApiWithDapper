using Dapper;
using MK.DotNetCore.Dapper.ApplicationCore.Entities;
using MK.DotNetCore.Dapper.ApplicationCore.Interfaces;
using MK.DotNetCore.Dapper.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK.DotNetCore.Dapper.Infrastructure.DataAccess.Implementation
{
    public class EmployeesAsyncRepository : IAsyncRepository<Employees>
    {
        private readonly IConnectionFactory _connectionFactory;

        public EmployeesAsyncRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public Task<Employees> AddAsync(Employees entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(ISpecification<Employees> spec)
        {
            throw new NotImplementedException();
        }

        //public async Task DeleteAsync(Employees entity)
        public async Task<bool> DeleteByIdAsync(int employeeId)
        {
            //throw new NotImplementedException();
            bool IsDeleted = true;
            var SqlQuery = @"DELETE FROM Employees WHERE EmployeeID = @Id";

            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                var rowsaffected = await conn.ExecuteAsync(SqlQuery, new { Id = employeeId });
                if (rowsaffected <= 0)
                {
                    IsDeleted = false;
                }
            }
            return IsDeleted;
        }

        public async Task<Employees> GetByIdAsync(int id)
        {
            var Employees = new Employees();
            var procName = "spEmployeesFetch";
            var param = new DynamicParameters();
            param.Add("@EmployeeId", id);

            try
            {
                using (var multiResult = await SqlMapper.QueryMultipleAsync(_connectionFactory.GetConnection,
                procName, param, commandType: CommandType.StoredProcedure, commandTimeout: 1800))
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

        public async Task<IReadOnlyList<Employees>> ListAllAsync()
        {
            //throw new NotImplementedException();
            //var EmpList = new List<Employees>();
            var SqlQuery = @"SELECT [EmployeeID],[LastName],[FirstName],[Title],[TitleOfCourtesy],[City],[Country] FROM [Northwind].[dbo].[Employees]";

            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                var result = await conn.QueryAsync<Employees>(SqlQuery, commandTimeout: 1800);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<Employees>> ListAllAsync(string filters)
        {
            //throw new NotImplementedException();
            //var EmpList = new List<Employees>();
            var SqlQuery = @"SELECT [EmployeeID],[LastName],[FirstName],[Title],[TitleOfCourtesy],[City],[Country] FROM [Northwind].[dbo].[Employees]";

            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                var result = await conn.QueryAsync<Employees>(SqlQuery, commandTimeout: 1800);
                return result.ToList();
            }
        }

        public Task<IReadOnlyList<Employees>> ListAsync(ISpecification<Employees> spec)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Employees entity)
        {
            throw new NotImplementedException();
        }
    }
}
