using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAggregator.DTO
{
    public class NewsResponseDto
    {
        public string Status { get; set; }
        public int ToTalResults { get; set; }
        public List<NewsDto> Articles { get; set; }
    }

    public class NewsDto
    {
        public SourceDto Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
    }

    public class SourceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
