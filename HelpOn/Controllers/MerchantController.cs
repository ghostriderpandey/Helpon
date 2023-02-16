using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using HelpOn.Helper;
using HelpOn.Models;
using HelpOn.Repository;

namespace HelpOn.Controllers
{
    public class MerchantController : Controller
    {
        LoginRepository repo = new LoginRepository();
        // GET: Merchant
        [Route("Segment/{Name}")]
        public ActionResult Index(string Name)
        {
            MerchantViewModel merchantView = new MerchantViewModel();
            merchantView.Merchants = repo.GetMerchant().FindAll(e => (e.CategoryName == Name.Replace("-", " ").Replace("_", "&") || e.SubCategoryName.Trim() == Name.Replace("-", " ").Replace("_", "&").Trim()) || (e.StateName.Trim() == Name.Replace("-", " ").Replace("_", "&").Trim() || e.Districtname.Trim() == Name.Replace("-", " ").Replace("_", "&").Trim()));
            if (merchantView.Merchants.Count <= 0)
            {
                merchantView.Merchants = new List<MasterMerchant>();
            }
            merchantView.SubCategory = repo.GetCategory("GetSubCategory").FindAll(e => e.CName.Trim() == Name.Replace("-", " ").Replace("_", "&").Trim() && e.Type == "Main");
            if (merchantView.SubCategory.Count <= 0)
            {
                merchantView.SubCategory = new List<CategoryModel>();
            }

            ViewBag.CategoryName = Name.Replace("-", " ").Replace("_", "&");
            return View(merchantView);
        }
        [Route("Popular/{Name}")]
        public ActionResult Popular(string Name)
        {
            MerchantViewModel merchantView = new MerchantViewModel();
            List<MasterMerchant> merchants = new List<MasterMerchant>();
            merchantView.Category = Connection.Query<CategoryModel>("Exec ProcGet_List 'GetCategoryByTopCategory',0,'" + Name.Replace("-", " ").Replace("_", "&").Trim() + "'").ToList();
            if (merchantView.Category.Count <= 0)
            {
                merchantView.Category = new List<CategoryModel>();
                merchantView.SubCategory = Connection.Query<CategoryModel>("Exec ProcGet_List 'GetSubCategoryByTopCategory',0,'" + Name.Replace("-", " ").Replace("_", "&").Trim() + "'").ToList();
            }

            if (merchantView.SubCategory != null)
            {
                if (merchantView.SubCategory.Count <= 0)
                {
                    merchantView.SubCategory = new List<CategoryModel>();
                    merchantView.Merchants = Connection.Query<MasterMerchant>("Exec ProcGet_List 'GetMerchantByTopCategory',0,'" + Name.Replace("-", " ").Replace("_", "&").Trim() + "'").ToList();
                }
                else
                {
                    merchantView.Merchants = new List<MasterMerchant>();
                }
            }
            else
            {
                merchantView.Merchants = new List<MasterMerchant>();
                merchantView.SubCategory = new List<CategoryModel>();
            }
            if (merchantView.Merchants.Count <= 0)
            {
                merchantView.Merchants = new List<MasterMerchant>();
            }

            ViewBag.CategoryName = Name.Replace("-", " ").Replace("_", "&");
            return View(merchantView);
        }
        [Route("Detail/{id}")]
        public ActionResult MerchantDetail(int id)
        {
            MerchantDetailModel merchant = new MerchantDetailModel();
            ViewBag.MID = id;
            merchant = repo.GetMerchantDetail(id);
            if (merchant == null)
            {
                merchant = new MerchantDetailModel();
            }
            return View(merchant);
        }
        [Route("Gallery/{id}")]
        public ActionResult Gallery(int id)
        {
            List<GalleryModel> merchant = new List<GalleryModel>();
            merchant = Connection.Query<GalleryModel>("ProcGet_List 'GetGallery','" + id + "'").ToList();
            if (merchant == null)
            {
                merchant = new List<GalleryModel>();
            }
            return View(merchant);
        }
        [Route("Product/{id}")]
        public ActionResult ProductDetail(int id)
        {

            ProductDetailModel productDetail = new ProductDetailModel();
            productDetail.product = repo.GetMerchantProduct().FindAll(e => e.ID == id).FirstOrDefault();
            ViewBag.MID = productDetail.product.MID;
            ViewBag.Itemshow = 0;
            return View(productDetail);
        }
        public ActionResult _ReletiveProduct(int id)
        {
            ProductDetailModel productDetail = new ProductDetailModel();
            productDetail.product = repo.GetMerchantProduct().FindAll(e => e.ID == id).FirstOrDefault();

            List<ProductModel> Reletedproduct = repo.GetMerchantProduct().FindAll(e => (e.CID == productDetail.product.CID || e.SCID == productDetail.product.SCID) && e.MID == productDetail.product.MID);
            List<CartModel> cart = repo.GetCart(productDetail.product.MID, SessionHelper.CustomerID);

            ViewBag.CartItem = cart.Count;
            return PartialView(Reletedproduct);
        }

        public ActionResult _MerchantProduct(int id, int cid = 0, string name = "")
        {

            List<ProductModel> products = new List<ProductModel>();
            products = repo.GetMerchantProduct().FindAll(e => (e.MID == id && (e.CID == cid || (cid) == 0) && (e.Name == name || (name) == ""))).Take(15).ToList();
            List<CartModel> cart = repo.GetCart(id, SessionHelper.CustomerID);
            ViewBag.Itemshow = products.Count();
            ViewBag.CartItem = cart.Count;
            if (products.Count <= 0)
            {
                products = new List<ProductModel>();
            }

            return PartialView("_MerchantProduct", products);
        }
        public ActionResult _MerchantMoreProduct(int id, int cid = 0, string name = "")
        {

            List<ProductModel> products = new List<ProductModel>();
            products = repo.GetMerchantProduct().FindAll(e => (e.MID == id && (e.CID == cid || (cid) == 0) && (e.Name == name || (name) == ""))).ToList();
            List<CartModel> cart = repo.GetCart(id, SessionHelper.CustomerID);
            ViewBag.Itemcount = products.Count();
            ViewBag.CartItem = cart.Count;
            if (products.Count <= 0)
            {
                products = new List<ProductModel>();
            }

            return PartialView("_MerchantProduct", products);
        }
        public ActionResult _MerchantProductByCategory(int id)
        {

            List<CategoryModel> categories = Connection.Query<CategoryModel>("select * from tblMaster_Category where Type='Ecommerce' and ID IN(select DISTINCT CID from tblManage_MerchantProduct where MID='" + id + "')").ToList();
            if (categories.Count <= 0)
            {
                categories = new List<CategoryModel>();
            }
            List<string> str = new List<string>();
            str = Connection.Query<string>("select Name from tblManage_MerchantProduct where MID='" + id + "'").ToList();
            var text = string.Join("|", str);
            ViewBag.ProductSearch = text;
            return PartialView("_MerchantProductByCategory", categories);
        }

        public ActionResult _Appointments(int id)
        {
            ViewBag.MID = id;
            return PartialView("_Appointments", null);
        }
        public ActionResult _Review(int id)
        {
            ViewBag.MID = id;
            List<ListReview> reviews = new List<ListReview>();
            reviews = Connection.Query<ListReview>("Exec ProcManage_Report 'MerchantReview'").ToList().FindAll(e => e.MID == id);
            if (reviews.Count <= 0)
            {
                reviews = new List<ListReview>();
            }
            return PartialView("_Review", reviews);
        }
        public ActionResult _EnquiryNow(int id)
        {
            ViewBag.MID = id;
            return PartialView("_EnquiryNow", null);
        }
        [HttpPost]
        [Route("Submit_Appointments")]
        public JsonResult Submit_Appointments(AppointmentsModel appointments)
        {
            AppTransactionMessage appTransaction = new AppTransactionMessage();
            // appTransaction = Connection.Query<AppTransactionMessage>("Exec ProcManage_AppointMentAndEquiry 'insert','" + appointments.MID + "','" + appointments.Name + "','" + appointments.Mobile + "','" + appointments.Email + "','" + appointments.AppointmentsDate + "','" + appointments.AppointmentsTime + "','" + appointments.Description + "','" + appointments.Type + "'").FirstOrDefault();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Action", "insert");
            parameters.Add("@MID", appointments.MID);
            parameters.Add("@Name", appointments.Name);
            parameters.Add("@Mobile", appointments.Mobile);
            parameters.Add("@Email", appointments.Email);
            parameters.Add("@AppointmentsDate", appointments.AppointmentsDate);
            parameters.Add("@AppointmentsTime", appointments.AppointmentsTime);
            parameters.Add("@Description", appointments.Description);
            parameters.Add("@Type", appointments.Type);
            appTransaction = Connection.ReturnList<AppTransactionMessage>("ProcManage_AppointMentAndEquiry", parameters).FirstOrDefault();
            return Json(appTransaction);
        }
        [Route("GetMerchantBySearch")]
        public ActionResult GetMerchantBySearch(SearchModel search)
        {
            List<MasterMerchant> merchants = new List<MasterMerchant>();
            merchants = Connection.Query<MasterMerchant>("Exec ProcGet_List 'GetBySearch',0,'" + search.Name + "','" + search.State + "','"+search.TopCategoryID+"'").ToList();
            if (merchants.Count <= 0)
            {
                merchants = new List<MasterMerchant>();
            }
            if (search.Name != null)
            {
                ViewBag.CategoryName = search.Name;
            }
            else
            {
                ViewBag.CategoryName = search.State;
            }

            return PartialView(merchants);
        }
        public ActionResult FilterMerchant()
        {
            MerchantFilterModel merchantFilter = new MerchantFilterModel();
            ViewBag.Category = new SelectList(repo.GetCategory("GetPopularCategory"), "ID", "Name");
            List<string> str = new List<string>();
            str = Connection.Query<string>("ProcGet_List 'GetforSearch'").ToList();
            var text = string.Join("|", str);
            ViewBag.Search = text;
            List<string> states = new List<string>();
            states = Connection.Query<string>("ProcGet_List 'GetLocationforSearch'").ToList();
            var state = string.Join("|", states);
            ViewBag.State = state;
            return PartialView();
        }
        [HttpPost]
        [Route("Submit_Review")]
        public JsonResult Submit_Review(SubmitReview review)
        {
            AppTransactionMessage appTransaction = new AppTransactionMessage();
            appTransaction = Connection.Query<AppTransactionMessage>("Exec ProcManage_Review 'insert',0,'" + SessionHelper.CustomerID + "','" + review.MID + "','" + review.Rating + "','" + review.Comment + "'").FirstOrDefault();
            return Json(appTransaction);
        }
    }
}