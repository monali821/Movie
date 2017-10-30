using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class mdlUser
    {
       public String User_Id { get; set; }
        public String User_Email { get; set; }
        public Boolean Isactive { get; set; }
        public String UserPassword { get; set; }

    }
}