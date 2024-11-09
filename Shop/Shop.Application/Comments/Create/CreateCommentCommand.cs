using Common.Application;

namespace Shop.Application.Comments.Create;

/// <summary></summary>
/// <param name="Text"></param>
/// <param name="UserId"></param>
/// <param name="ProductId"></param>
public record CreateCommentCommand
(
    string Text,
    long UserId,
    long ProductId
)
: IBaseCommand;