using HelpOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpOn.Helper;
using Dapper;
using System.Web.Security;

namespace HelpOn.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return PartialView("Login",null);
        }
        public JsonResult CustomerLogin(CustomerModel customer)
        {
            AppTransactionMessage appTransaction = new AppTransactionMessage();
            DynamicParameters para = new DynamicParameters();
            if (customer.Type.ToUpper() == "login".ToUpper())
            {
                para.Add("@Action", "CustomerPanel");
                para.Add("@UserID", customer.Mobile);
                para.Add("@Password", customer.Password);
                customer = Connection.ReturnList<CustomerModel>("ProcMaster_Login", para).FirstOrDefault();
                if (customer.Status > 0)
                {
                    SessionHelper.CustomerID = customer.ID;
                    SessionHelper.Name = customer.Name;
                }
                appTransaction.Status = customer.Status;
                appTransaction.Message = customer.Message;
            }
            else if(customer.Type.ToUpper()== "Signup".ToUpper())
            {
                para.Add("@Action", "insert");
                para.Add("@Name", customer.Name);
                para.Add("@Email", customer.Email);
                para.Add("@Mobile", customer.Mobile);
                para.Add("@Password", customer.Password);
                appTransaction = Connection.ReturnList<AppTransactionMessage>("ProcMaster_Customer", para).FirstOrDefault();
            }
            return Json(appTransaction);
        }
        public ActionResult Logout()
        {
            Session.Contents.RemoveAll();
            return Redirect("/Home");
        }
    }
}