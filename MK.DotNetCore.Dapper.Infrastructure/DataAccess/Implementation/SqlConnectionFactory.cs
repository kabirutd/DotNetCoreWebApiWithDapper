using Microsoft.Extensions.Options;
using MK.DotNetCore.Dapper.ApplicationCore.Entities;
using MK.DotNetCore.Dapper.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MK.DotNetCore.Dapper.Infrastructure.DataAccess.Implementation
{
    public class SqlConnectionFactory: IConnectionFactory
    {
        private IDbConnection _connection;

        //Needs to point to the right dbconnection string in the appsettings.json file. 
        //We could use oracle and other db too
        private readonly IOptions<NorthWindConfiguration> _configs;

        public SqlConnectionFactory (IOptions<NorthWindConfiguration> Configs)
        {
            _configs = Configs;
        }


        public IDbConnection GetConnection
        {
            get
            {
                if (_connection == null)
                {
                    //
                    _connection = new SqlConnection(_configs.Value.SqlLocalDbConnectionString);
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
