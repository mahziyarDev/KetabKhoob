using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByFilter;

public class GetUserByFilterQuery : BaseQueryFilter<UserFilterResult, UserFilterParams>
{
    public GetUserByFilterQuery(UserFilterParams filterParams) : base(filterParams)
    {
    }
}