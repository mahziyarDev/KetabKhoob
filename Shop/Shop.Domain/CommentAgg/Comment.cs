using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.CommentAgg.Enum;

namespace Shop.Domain.CommentAgg;

public class Comment : AggregateRoot
{

    #region Properties

    public long UserId { get; private set; }
    public long ProductId { get; private set; }
    public string Text { get; private set; }
    public CommentStatus CommentStatus { get; private set; }
    public DateTime? UpdateDate { get; private set; }
    
    #endregion

    #region Functions

    private Comment(){}

    public Comment(long userId, long productId, string text)
    {
        UserId = userId;
        ProductId = productId;
        Text = text;
        CommentStatus = CommentStatus.Pending;
    }
    public void Edit(string text)
    {
        NullOrEmptyDomainDataException.CheckString(text,nameof(text));
        Text = text;
    }

    public void ChangeStatus(CommentStatus commentStatus)
    {
        CommentStatus =commentStatus;
        UpdateDate = DateTime.Now;
    }
    
    #endregion
}