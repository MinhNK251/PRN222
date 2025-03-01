using System;
using System.Collections.Generic;

namespace BusinessObjectsLayer.DTOs
{
    public class NewsArticleDTO
    {
        public int NewsArticleId { get; set; }
        public string NewsTitle { get; set; } = null!;
        public string Headline { get; set; } = null!;
        public string NewsContent { get; set; } = null!;
        public string NewsSource { get; set; } = null!;
        public int CategoryId { get; set; }
        public bool NewsStatus { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int[] SelectedTagIds { get; set; } = Array.Empty<int>();
    }
}
