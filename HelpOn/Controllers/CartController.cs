using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpOn.Helper;
using HelpOn.Models;
using HelpOn.Repository;
using Dapper;
using System.Dynamic;
using System.IO;

namespace HelpOn.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        LoginRepository repo = new LoginRepository();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult _Cart(int id,int cid)
        {
            ViewBag.MID = id;
            List<CartModel> cartitem = repo.GetCart(id, SessionHelper.CustomerID);
            MerchantBankDetail bankDetail = repo.GetMerchantBankDetail().FindAll(e => e.MID == id).FirstOrDefault();
            if (bankDetail == null)
            {
                bankDetail = new MerchantBankDetail();
            }
            CartTotal total = new CartTotal();
            total = Connection.Query<CartTotal>("Proc_ApplyCoupon 'GetTotal','" + SessionHelper.CustomerID + "','" + id+ "'").FirstOrDefault();
            dynamic mymodel = new ExpandoObject();
            
            ViewBag.TotalAmount = total.Total;
            mymodel.cartitem = cartitem;
            mymodel.bankDetail = bankDetail;
            return PartialView("_Cart", mymodel);
        }
        public JsonResult ApplyCoupon(ApplyCouponCode applyCoupon)
        {
            CartTotal total = new CartTotal();
            total = Connection.Query<CartTotal>("Proc_ApplyCoupon 'ApplyNow','" + SessionHelper.CustomerID + "','" + applyCoupon.MID + "','" + applyCoupon.Code + "'").FirstOrDefault();
           return Json(total);
        }
        
        public ActionResult _ShippingAddress()
        {
            
            return PartialView("_ShippingAddress", null);
        }
        [Route("Cart/AddToCart")]
        public JsonResult AddToCart(int PID,int Qty)
        {
            AppTransactionMessage appTransaction = new AppTransactionMessage();
            appTransaction = Connection.Query<AppTransactionMessage>("Exec ProcManage_Cart 'insert','" + SessionHelper.CustomerID + "','" + PID + "','" + Qty + "'").FirstOrDefault();
           
            return Json(appTransaction);
        }
        [Route("Cart/DeleteCart")]
        public JsonResult DeleteCart(int PID)
        {
            AppTransactionMessage appTransaction = new AppTransactionMessage();
            appTransaction = Connection.Query<AppTransactionMessage>("Exec ProcManage_Cart 'Delete','" + SessionHelper.CustomerID + "','" + PID + "'").FirstOrDefault();
           
            return Json(appTransaction);
        }
        [HttpPost]
        public JsonResult Checkout(CheckOut check)
        {
            string FileName = "";
            AppTransactionMessage appTransaction = new AppTransactionMessage();
            try
            {
                if (check.Screenshot != null)
                {
                    if (check.Screenshot.ContentLength > 0)
                    {
                        FileName = Path.GetFileName(check.Screenshot.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Upload/Screenshot"), FileName);
                        check.Screenshot.SaveAs(_path);
                    }
                }

                DynamicParameters para = new DynamicParameters();
                para.Add("@Action", "insert");
                para.Add("@CustomerID", SessionHelper.CustomerID);
                para.Add("@MID", check.MID);
                para.Add("@PaymentMode", check.PaymentMode);
                para.Add("@TransactionNo", check.TransactionNo);
                para.Add("@ScreenShot", FileName);
                para.Add("@Address", check.Address);
                para.Add("@TotalAmount", check.TotalAmount);
                para.Add("@Discount", check.Discount);
                para.Add("@PaybleAmount", check.PaybleAmount);
                para.Add("@CouponCode", check.CouponCode);
                appTransaction = Connection.ReturnList<AppTransactionMessage>("ProcManage_Order", para).FirstOrDefault();
                return Json(appTransaction);
            }
            catch(Exception ex)
            {
                appTransaction.Status = 0;
                appTransaction.Message = ex.Message;
            }
            return Json(appTransaction);
        }
        [Route("Cart/_UploadFile")]
        public ActionResult _UploadFile()
        {
            return PartialView("_UploadFile");
        }
        

    }
}