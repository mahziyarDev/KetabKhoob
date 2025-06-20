﻿using Common.Domain.Repository;

namespace Shop.Domain.SellerAgg.Repository;

public interface ISellerRepository : IBaseRepository<Seller>
{
    Task<InventoryResult?> GetInventoryByIdAsync(long Id);
}


public class InventoryResult
{
    public long Id { get; set; }
    public long SellerId { get; set; }
    public long ProductId { get; set; }
    public int Count { get; set; }
    public int Price { get; set; }
    
}