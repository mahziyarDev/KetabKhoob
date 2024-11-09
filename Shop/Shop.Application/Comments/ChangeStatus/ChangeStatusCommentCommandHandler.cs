using Common.Application;
using Shop.Domain.CommentAgg.Repository;

namespace Shop.Application.Comments.ChangeStatus;

public class ChangeStatusCommentCommandHandler : IBaseCommandHandler<ChangeStatusCommentCommand>
{
    private readonly ICommentRepository _commentRepository;
    /// <summary></summary>
    /// <param name="commentRepository"></param>
    public ChangeStatusCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(ChangeStatusCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetAsync(request.Id);
        if (comment == null)
            return OperationResult.NotFound();
        comment.ChangeStatus(request.CommentStatus);
        await _commentRepository.Save();
        return OperationResult.Success();
    }
}