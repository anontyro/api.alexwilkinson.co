using System;
using System.Collections.Generic;

namespace api.alexwilkinson.co
{
    public partial class Author
    {
        public int AuthorActive { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorFirstname { get; set; }
        public string AuthorLastname { get; set; }
        public string AuthorPassword { get; set; }
        public string AuthorUsername { get; set; }
        public int AuthorAccess { get; set; }
        public int Id { get; set; }
    }
}
