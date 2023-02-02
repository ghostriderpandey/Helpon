using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOn.Helper
{
    public class Application
    {
       
        public static string ImagePath
        {
            get
            {

                return System.Web.Configuration.WebConfigurationManager.AppSettings["ImagePath"].ToString();
            }
        }
    }
}