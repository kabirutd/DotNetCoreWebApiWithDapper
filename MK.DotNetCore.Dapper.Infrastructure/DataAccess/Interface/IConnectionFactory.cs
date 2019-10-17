using System.Data;

namespace MK.DotNetCore.Dapper.Infrastructure.DataAccess.Interface
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
        void CloseConnection();
    }
}
