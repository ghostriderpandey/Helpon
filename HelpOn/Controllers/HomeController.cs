using HelpOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpOn.Repository;
using HelpOn.Helper;
using System.Data;

namespace HelpOn.Controllers
{
    public class HomeController : Controller
    {
        LoginRepository repo = new LoginRepository();
        Cls_Connection cons = new Cls_Connection();
        public ActionResult Index()
        {
            HomeModel home = new HomeModel();
            home = repo.GetHomeDetail();
            return View(home);
        }
        [HttpPost]
        public JsonResult RequestForm(RequestForm request)
        {
            AppTransactionMessage appTransaction = new AppTransactionMessage();
            if (request.Name == "" || request.Name == null)
            {
                appTransaction.Status = 0;
                appTransaction.Message = "Enter Name";
                return Json(appTransaction);
            }
            else if (request.Mobile == "" || request.Mobile == null)
            {
                appTransaction.Status = 0;
                appTransaction.Message = "Enter Mobile";
                return Json(appTransaction);
            }
            else if (request.Email == "" || request.Email == null)
            {
                appTransaction.Status = 0;
                appTransaction.Message = "Enter Email";
                return Json(appTransaction);
            }
            else if (request.Service == "" || request.Service == null)
            {
                appTransaction.Status = 0;
                appTransaction.Message = "Enter Your Service";
                return Json(appTransaction);
            }
            else if ((Connection.ExecuteScalar<int>("select Count(*) from tblManage_RequestService where Mobile='" + request.Mobile + "'")) > 0)
            {
                appTransaction.Status = 0;
                appTransaction.Message = "Mobile Number Already Requested";
                return Json(appTransaction);
            }
            Connection.Execute("insert into tblManage_RequestService(Name,Email,Mobile,Service)values(N'" + request.Name + "','" + request.Email + "','" + request.Mobile + "',N'" + request.Service + "')");
            appTransaction.Status = 1;
            appTransaction.Message = "Successfully Submited Your Request";
            return Json(appTransaction);
        }
        public ActionResult _TopHeader()
        {
            WebColor web = new WebColor();
            web = Connection.Query<WebColor>("ProcManage_WebColor 'GetForTopHeader'").FirstOrDefault();
            web.Name = Connection.ExecuteScalar<string>("select Name from tblMaster_Customer where ID='" + SessionHelper.CustomerID + "'");
            web.Wallet = Connection.ExecuteScalar<string>("select Balance from tblAccount_Customer where CustomerID='" + SessionHelper.CustomerID + "'");
            return PartialView("_TopHeader", web);
        }
        [Route("about-us")]
        public ActionResult About()
        {
            return View();
        }
        [Route("Privacy-Policy")]
        public ActionResult PrivacyPolicy()
        {

            return View();
        }
        [Route("Coming-Soon")]
        public ActionResult ComingSoon()
        {

            return View();
        }
        [Route("Terms-and-Conditions")]
        public ActionResult TermsandConditions()
        {

            return View();
        }
        [Route("Disclaimer")]
        public ActionResult Disclaimer()
        {

            return View();
        }
        [Route("Segments")]
        public ActionResult AllCategory()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            categories = repo.GetCategory("GetCategory").FindAll(e => e.Type == "Main");
            return View(categories);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SideDropDownMenu()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            categories = repo.GetCategory("GetCategory");
            if (categories.Count <= 0)
            {
                categories = new List<CategoryModel>();
            }
            return PartialView("SideDropDownMenu", categories);
        }
        public ActionResult Search()
        {
            List<string> str = new List<string>();
            str = Connection.Query<string>("ProcGet_List 'GetforSearch'").ToList();
            var text = string.Join("|", str);
            ViewBag.Search = text;
            List<string> states = new List<string>();
            states = Connection.Query<string>("ProcGet_List 'GetLocationforSearch'").ToList();
            var state = string.Join("|", states);
            ViewBag.State = state;
            return PartialView("Search");
        }
        [Route("Logout")]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return Redirect("/Home");
        }
        public ActionResult GetallCategoryFirst(long CId)
        {
            DataSet dsRecord = new DataSet();
            CategoryModel resultSet = new CategoryModel();
            try
            {
                string sQryeraw = string.Format(@"Exec ProcMaster_Category Getcatname,@ID='" + CId + "' ");
                dsRecord = cons.GetDataSet(sQryeraw);
                resultSet.Name = Convert.ToString(dsRecord.Tables[0].Rows[0]["Name"].ToString());
                resultSet.subcateName = (from item in dsRecord.Tables[1].AsEnumerable()
                                          select new SubCategory
                                          {
                                              SubCatId = Convert.ToInt16(item.Field<int>("ID")),
                                              SubCatName = Convert.ToString(item.Field<string>("Name")),
                                          }).ToList();
            }
            catch (Exception ex)
            {
            }
            return Json(resultSet);
        }

        [Route("mobilesearch")]
        public ActionResult MobileSearch()
        {

            return View();
        }
    }
}