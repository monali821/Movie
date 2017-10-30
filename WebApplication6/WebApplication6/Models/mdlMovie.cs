
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class mdlMovie
    {
        public String Movie_Id { get; set; }
        public String Movie_Name { get; set; }
        public String Language { get; set; }
        public DateTime PublishDate { get; set; }
        public String YearofRelease { get; set; }
        public String MoviePoster { get; set; }

    }
}