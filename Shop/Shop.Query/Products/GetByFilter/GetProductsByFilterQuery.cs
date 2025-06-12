using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.GetByFilter;

public class GetProductsByFilterQuery : BaseQueryFilter<ProductFilterResult, ProductFilterParams>
{
    public GetProductsByFilterQuery(ProductFilterParams filterParams) : base(filterParams)
    {
    }
}