using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOn.Helper
{
    public class SessionHelper
    {
        public static bool Islogin
        {
            get
            {
               return CustomerID<=0 ? false : true;
            }
            set
            {
                Islogin = value;
            }
        }
        public static int CustomerID
        {
            get
            {
                return HttpContext.Current.Session["_CustomerID"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["_CustomerID"]);
            }
            set
            {
                HttpContext.Current.Session["_CustomerID"] = value;
            }
        }
        public static string Name
        {
            get
            {
                return HttpContext.Current.Session["_Name"] == null ? "" : HttpContext.Current.Session["_Name"].ToString();
            }
            set
            {
                HttpContext.Current.Session["_Name"] = value;
            }
        }
    }
}