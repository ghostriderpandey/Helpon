using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpOn.Models
{
   public class CustomerModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public bool IsRemeber { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        [NotMapped]
        public SelectList State { get; set; }
    }
    
    
}