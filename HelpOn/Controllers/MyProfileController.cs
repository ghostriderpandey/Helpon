using HelpOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpOn.Repository;
using HelpOn.Helper;
using Dapper;

namespace HelpOn.Controllers
{
    public class MyProfileController : Controller
    {
        LoginRepository repo = new LoginRepository();
        // GET: MyProfile
        [Route("Profile")]
        public ActionResult Index()
        {
            if (SessionHelper.Islogin == false)
            {
                return Redirect("/Home");
            }
            
            CustomerModel customer = Connection.Query<CustomerModel>("ProcMaster_Customer 'GetProfile','" + SessionHelper.CustomerID + "'").FirstOrDefault();
            customer.State = new SelectList(repo.GetState(), "ID", "Name");
            return View(customer);
        }
        [Route("UpdateProfile")]
        public JsonResult UpdateProfile(CustomerModel customer)
        {
            AppTransactionMessage appTransaction = new AppTransactionMessage();
            DynamicParameters para = new DynamicParameters();
            para.Add("@Action", "insert");
            para.Add("@ID", SessionHelper.CustomerID);
            para.Add("@Name", customer.Name);
            para.Add("@Email", customer.Email);
            para.Add("@Mobile", customer.Mobile);
          
            appTransaction = Connection.ReturnList<AppTransactionMessage>("ProcMaster_Customer", para).FirstOrDefault();
            if (appTransaction.Status > 0)
            {
                para = new DynamicParameters();
                para.Add("@Action", "insert");
                para.Add("@CustomerID", SessionHelper.CustomerID);
                para.Add("@Name", customer.Name);
                para.Add("@Email", customer.Email);
                para.Add("@Mobile", customer.Mobile);
                para.Add("@StateID", customer.StateID);
                para.Add("@CityID", customer.CityID);
                para.Add("@Address", customer.Address);
                para.Add("@Pincode", customer.Pincode);
                appTransaction = Connection.ReturnList<AppTransactionMessage>("ProcManage_Address", para).FirstOrDefault();
            }
            return Json(appTransaction);

        }
        
        public JsonResult getCity(int stateId)
        {
            List<StateModel> states = repo.GetCity(stateId);
            
            return Json(states);

        }
        [Route("ChangePassword")]
        public ActionResult ChangePassword()
        {
            if (SessionHelper.Islogin == false)
            {
                return Redirect("/Home");
            }
            return PartialView("ChangePassword");
        }

        [Route("ChangePasswordSubmit")]
        public JsonResult ChangePasswordSubmit(ChangePasswordModel changePassword)
        {
            AppTransactionMessage appTransaction = new AppTransactionMessage();
            if ((Connection.ExecuteScalar<string>("select Password from tblMaster_Customer where ID='" + HelpOn.Helper.SessionHelper.CustomerID + "'")) != changePassword.OldPassword)
            {
                appTransaction.Status = 0;
                appTransaction.Message = "OLD Password Invalid";
                return Json(appTransaction);
            }
            else if (changePassword.password != changePassword.cpassword)
            {
                appTransaction.Status = 0;
                appTransaction.Message = "Confirm Password Does Not Match";
                return Json(appTransaction);
            }
            else
            {
                Connection.Execute("Update tblMaster_Customer SET Password='" + changePassword.password + "' where ID='" + SessionHelper.CustomerID + "'");
                appTransaction.Status = 1;
                appTransaction.Message = "Password Successfully Update";
                Session.Contents.RemoveAll();
                return Json(appTransaction);
            }
        }
        [Route("My-Order")]
        public ActionResult MyOrder()
        {
            if (SessionHelper.Islogin == false)
            {
                return Redirect("/Home");
            }
            List<MyOrderModel> myOrders = new List<MyOrderModel>();
            myOrders = repo.GetMyOrder().FindAll(e=>e.CustomerID==SessionHelper.CustomerID);
            return View(myOrders);
        }
        [Route("Order-Product/{id}")]
        public ActionResult OrderProduct(int id)
        {
            List<OrderProductReport> orderProducts = new List<OrderProductReport>();
            orderProducts = Connection.Query<OrderProductReport>("Exec ProcManage_Report 'OrderReportWithProduct','" + id + "'").ToList();
            if (orderProducts.Count <= 0)
            {
                orderProducts = new List<OrderProductReport>();
            }
            return View(orderProducts);
        }
        [Route("Invoice/{id}")]
        public ActionResult Invoice(int id)
        {
            if (SessionHelper.Islogin == false)
            {
                return Redirect("/Home");
            }
            InvoiceModel invoice = new InvoiceModel();
            invoice = Connection.Query<InvoiceModel>("Proc_GetInvoice 'GetDetail','"+id+"'").FirstOrDefault();
            invoice.products=Connection.Query<InvoiceProduct>("Proc_GetInvoice 'InvoiceProduct','" + id + "'").ToList();
            return View(invoice);
        }
    }
}