using Dapper;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Dapper;


namespace Shop.Infrastructure.Persistent.Ef.SellerAgg;

internal class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    private readonly DapperContext _dapperContext;
    /// <summary></summary>
    /// <param name="context"></param>
    /// <param name="dapperContext"></param>
    public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }

    
    public async Task<InventoryResult?> GetInventoryByIdAsync(long Id)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECT * from {_dapperContext.Inventories} where Id = @InventoryId";
       return await connection.QueryFirstOrDefaultAsync<InventoryResult>(sql, new { inventoryId = Id });
    }
}