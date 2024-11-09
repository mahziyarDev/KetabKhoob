using Common.Application;

namespace Shop.Application.Comments.Edit;
/// <summary></summary>
/// <param name="Id"></param>
/// <param name="Text"></param>
/// <param name="UserId"></param>
public record EditCommentCommand
(
    long Id,
    string Text,
    long UserId
)
: IBaseCommand;