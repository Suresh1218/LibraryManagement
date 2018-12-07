using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BookStatisticsViewModel
    {
        public string BookName { get; set; }

        public int BookCount { get; set; }

        public int NoOfBookInUse { get; set; }

        public double BookEarnings { get; set; }
    }
}