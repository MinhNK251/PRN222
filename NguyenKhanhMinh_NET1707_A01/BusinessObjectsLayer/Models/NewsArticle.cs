using System;
using System.Collections.Generic;

namespace BusinessObjectsLayer.Models;

public partial class NewsArticle
{
    public int NewsArticleId { get; set; }

    public string NewsTitle { get; set; } = null!;

    public string Headline { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string NewsContent { get; set; } = null!;

    public string NewsSource { get; set; } = null!;

    public int CategoryId { get; set; }

    public bool NewsStatus { get; set; }

    public int CreatedById { get; set; }

    public int UpdatedById { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual SystemAccount CreatedBy { get; set; } = null!;

    public virtual SystemAccount UpdatedBy { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
