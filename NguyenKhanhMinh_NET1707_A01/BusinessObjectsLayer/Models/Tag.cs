using System;
using System.Collections.Generic;

namespace BusinessObjectsLayer.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public string Note { get; set; } = null!;

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
