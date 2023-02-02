using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOn.Models
{
    public class RequestForm
    {
        public string Name { get; set; } = "";
        public string Mobile { get; set; } = "";
        public string Email { get; set; } = "";
        public string Service { get; set; } = "";
    }
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string password { get; set; }
        public string cpassword { get; set; }
    }
    public class SubmitReview
    {
        public int MID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
    public class ListReview
    {
        public int MID { get; set; }
        public string CustomerName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string AddDate { get; set; }
    }
    public class AppointmentsModel
    {
        public int MID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string AppointmentsDate { get; set; }
        public string AppointmentsTime { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

    }
    public class SearchModel
    {
        public string Name { get; set; }
        public string State { get; set; }
        public int TopCategoryID { get; set; }
    }
}