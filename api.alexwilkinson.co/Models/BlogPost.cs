using System;
using System.Collections.Generic;

namespace api.alexwilkinson.co
{
    public partial class BlogPost
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Body { get; set; }
        public string CoverImg { get; set; }
        public string Date { get; set; }
        public string Draft { get; set; }
        public string LastModified { get; set; }
        public string Publish { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
    }
}
