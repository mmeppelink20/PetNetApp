using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPresentation.Models
{
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => (TotalItems - 1) / ItemsPerPage + 1;
    }
}