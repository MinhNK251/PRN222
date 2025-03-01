using System;
using System.Collections.Generic;

namespace BusinessObjectsLayer.DTOs
{
    public class NewsArticleDTO
    {
        public string NewsArticleId { get; set; }
        public string? NewsTitle { get; set; } = null!;
        public string Headline { get; set; } = null!;
        public string? NewsContent { get; set; } = null!;
        public string? NewsSource { get; set; } = null!;
        public short? CategoryId { get; set; }
        public bool? NewsStatus { get; set; }
        public short? CreatedById { get; set; }
        public short? UpdatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int[] SelectedTagIds { get; set; } = Array.Empty<int>();
    }
}
