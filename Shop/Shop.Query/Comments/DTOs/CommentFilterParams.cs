﻿using Common.Query.Filter;
using Shop.Domain.CommentAgg.Enum;

namespace Shop.Query.Comments.DTOs;

public class CommentFilterParams : BaseFilterParam
{
    public long? UserId { get; set; }
    public long? ProductId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public CommentStatus? CommentStatus { get; set; }

}
