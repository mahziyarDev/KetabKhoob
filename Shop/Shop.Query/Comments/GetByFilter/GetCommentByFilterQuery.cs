using Common.Query;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.GetByFilter;

public class GetCommentByFilterQuery : BaseQueryFilter<CommentFilterResult, CommentFilterParams>
{
    public GetCommentByFilterQuery(CommentFilterParams filterParams) : base(filterParams)
    {
    }
}