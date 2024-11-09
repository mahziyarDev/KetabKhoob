using Common.Application;
using Shop.Domain.CommentAgg.Enum;

namespace Shop.Application.Comments.ChangeStatus;

public record ChangeStatusCommentCommand
(
    CommentStatus CommentStatus,
    long Id
) : IBaseCommand;