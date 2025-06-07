﻿using Common.Query.Filter;
using MediatR;

namespace Common.Query;

public interface IBaseQuery<TResponse> :  IRequest<TResponse> where TResponse : class{}

public class QueryFilter<TResponse, TParam> : IBaseQuery<TResponse>
    where TResponse : BaseFilter
    where TParam : BaseFilterParam
{
    public TParam FilterParams { get; set; }
    public QueryFilter(TParam filterParams)
    {
        FilterParams = filterParams;
    }
}