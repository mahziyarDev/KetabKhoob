using Common.Application;
using Shop.Domain.CommentAgg.Repository;

namespace Shop.Application.Comments.Edit;

public class EditCommentCommandHandler : IBaseCommandHandler<EditCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    /// <summary></summary>
    /// <param name="commentRepository"></param>
    public EditCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    public async Task<OperationResult> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetAsync(request.Id);
        if (comment == null || comment.UserId != request.UserId)
            return OperationResult.NotFound();
        comment.Edit(request.Text);
        await _commentRepository.SaveChangeAsync();
        return OperationResult.Success();
    }
    
}