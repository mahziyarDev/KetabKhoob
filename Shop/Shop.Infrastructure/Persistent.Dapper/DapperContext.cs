using System.Data;
using Microsoft.Data.SqlClient;

namespace Shop.Infrastructure.Persistent.Dapper;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }
//     var connectionString = "Server=localhost;Initial Catalog=DapperDB;Integrated Security=true;TrustServerCertificate=True";
// // Connect to the database 
//     var connection = new SqlConnection(connectionString);

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    
    public string Inventories = "[Seller].Inventories";    
    public string UserAddresses => "[user].Addresses";
    public string OrderItems => "[order].Items";
    public string Products => "[product].Products";
    public string Sellers => "[seller].Sellers";
    public string UserTokens => "[user].Tokens";

}