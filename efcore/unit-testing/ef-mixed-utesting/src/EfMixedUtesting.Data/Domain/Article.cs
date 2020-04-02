using System;

namespace EfMixedUtesting.Data.Domain
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ContentId { get; set; }
        public virtual ArticleContent Content { get; set; }
    }
}
