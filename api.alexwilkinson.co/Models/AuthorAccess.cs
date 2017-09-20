using System;
using System.Collections.Generic;

namespace api.alexwilkinson.co
{
    public partial class AuthorAccess
    {
        public bool AccessEnabled { get; set; }
        public string AuthAccessName { get; set; }
        public bool AuthorAbleToCreate { get; set; }
        public bool AuthorAbleToDelete { get; set; }
        public bool AuthorAbleToUpdate { get; set; }
        public int Id { get; set; }
    }
}
