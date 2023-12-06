using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.RequestHelpers
{
    public class BookParams : PaginationParams
    {
        public string OrderBy { get; set; } 
        public string SearchTerm { get; set; }
        public string Rating { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
       
        

    }
}