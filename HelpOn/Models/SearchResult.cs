using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOn.Models
{
    public class SearchResult<T> where T : class
    {
        public IList<T> Result { get; set; }
        public int TotalCount { get; set; }
    }
}