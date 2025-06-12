using Common.Query.Filter;
using MediatR;

namespace Common.Query;

public interface IBaseQuery<TResponse> :  IRequest<TResponse> where TResponse : class{}

/// <summary>
/// از کلاس استفاده شده به این دلیل اگه ما میخواهیم constructor داشته باشیم که مقادیر فیلتر حتما وارد بشه
/// </summary>
/// <typeparam name="TResponse"></typeparam>
/// <typeparam name="TParam"></typeparam>
public class BaseQueryFilter<TResponse, TParam> : IBaseQuery<TResponse>
    where TResponse : BaseFilter
    where TParam : BaseFilterParam
{
    public TParam FilterParams { get; set; }
    public BaseQueryFilter(TParam filterParams)
    {
        FilterParams = filterParams;
    }
}