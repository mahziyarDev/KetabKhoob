using Common.Query;
using Shop.Domain.CommentAgg.Enum;

namespace Shop.Query.Comments.DTOs;

public class CommentDto : BaseDto
{
    public long ProductId { get; set; }
    public long UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public string ProductTitle { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public CommentStatus Status { get; set; }
}
