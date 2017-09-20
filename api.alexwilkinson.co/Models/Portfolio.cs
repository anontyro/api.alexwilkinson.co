using System;
using System.Collections.Generic;

namespace api.alexwilkinson.co
{
    public partial class Portfolio
    {
        public int Portfolioid { get; set; }
        public int? Portfolioauthorid { get; set; }
        public string Portfoliobody { get; set; }
        public DateTime? Portfoliodate { get; set; }
        public int Portfoliodraft { get; set; }
        public string Portfoliofeaturedimage { get; set; }
        public DateTime? Portfoliolastmodified { get; set; }
        public int Portfoliopublished { get; set; }
        public string Portfolioslug { get; set; }
        public string Portfoliotitle { get; set; }
    }
}
