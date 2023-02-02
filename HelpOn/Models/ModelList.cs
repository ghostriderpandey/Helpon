using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOn.Models
{
    public class WebColor
    {
        public string FontColor { get; set; }
        public string BgColor { get; set; }
        public string Name { get; set; }
        public string Wallet { get; set; } = "0.00";
    }
    public class HomeModel
    {
        public List<SliderModel> sliders { get; set; }
        public List<CategoryModel> categories { get; set; }
        public List<PopularCategoryModel> popularCategories { get; set; }
        public List<PopularCityModel> popularCities { get; set; }
        public string Banner1 { get; set; }
        public string Banner2 { get; set; }
        public string Banner3 { get; set; }
        public string Video1 { get; set; }
        public string Video2 { get; set; }
        public string Video3 { get; set; }
    }
    public class PopularCategoryModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string IMG { get; set; }
        public string Description { get; set; }

    }
    public class PopularCityModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string IMG { get; set; }


    }
    public class SliderModel
    {
        public int ID { get; set; }
        public int Position { get; set; }
        public int AddByID { get; set; }
        public string AddByType { get; set; }
        public string Img { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
    }
    public class CategoryModel
    {
        public int ID { get; set; }
        public string CName { get; set; }
        public string Name { get; set; }
        public string IMG { get; set; }
        public string Type { get; set; }
        public string FontColor { get; set; }
        public string BgColor { get; set; }

        public List<SubCategory> subcateName { get; set; }
    }
    public class SubCategory
    {
        public long SubCatId { get; set; }

        public string SubCatName { get; set; }
    }
    public class MerchantFilterModel
    {
        public int TopCategoryID { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
    }
    public class MerchantViewModel
    {
        public List<CategoryModel> Category { get; set; }
        public List<CategoryModel> SubCategory { get; set; }
        public List<MasterMerchant> Merchants { get; set; }
    }
    public class MasterMerchant
    {
        public int MID { get; set; }
        public int CID { get; set; }
        public int SCID { get; set; }
        public string CategoryName { get; set; }
        public string CIMG { get; set; }
        public string SlideIMG { get; set; }
        public string SubCategoryName { get; set; }
        public string Shopname { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string AlternetMobile { get; set; }
        public string WhatsappNo { get; set; }
        public string TelPhoneNo { get; set; }
        public string Email { get; set; }
        public int StateID { get; set; }
        public string StateName { get; set; }
        public int DistrictID { get; set; }
        public string Districtname { get; set; }
        public int TehsilID { get; set; }
        public string TehsilName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string Password { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string WebSite { get; set; }
        public string Aboutus { get; set; }
        public string Amenities { get; set; }
        public string ProfileImg { get; set; }
        public string ThumbnailIMG { get; set; }
        public string VideoName { get; set; }
        public string SponserID { get; set; }
        public string SponserType { get; set; }
        public int IsPaid { get; set; }
        public string PaidDate { get; set; }
        public int IsApprove { get; set; }
        public string ApproveDate { get; set; }
        public int IsReject { get; set; }
        public string RejectDate { get; set; }
        public string AddDate { get; set; }
        public string Status { get; set; }
        public string StatusClass { get; set; }
        public string BgColor { get; set; }
        public string PopularName { get; set; }
    }
    public class MerchantBankDetail
    {
        public int MID { get; set; }
        public string Bankname { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
        public string Branch { get; set; }
        public string HolderName { get; set; }
        public string UPI { get; set; }
        public string Qrcode { get; set; }
    }
    public class GalleryModel
    {
        public int ID { get; set; }
        public string IMG { get; set; }
    }
    public class ProductDetailModel
    {
        public ProductModel product { get; set; }

    }
    public class MerchantProductModel
    {
        public List<CategoryModel> categories { get; set; }
        public List<ProductModel> products { get; set; }
    }
    public class ProductModel
    {
        public int ID { get; set; }
        public int MID { get; set; }
        public int CID { get; set; }
        public int SCID { get; set; }
        public string Name { get; set; }
        public decimal MRP { get; set; }
        public decimal Price { get; set; }
        public string Discount { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string About { get; set; }
        public string IMG1 { get; set; }
        public string IMG2 { get; set; }
        public string IMG3 { get; set; }
        public string IMG4 { get; set; }
        public string IMG5 { get; set; }
        public int IsCart { get; set; }
        public int IsStock { get; set; }
        public int Qty { get; set; }
    }
    public class MerchantDetailModel
    {
        public MasterMerchant merchant { get; set; }
        public List<SliderModel> sliders { get; set; }
        public List<ProductModel> products { get; set; }
        public List<MasterMerchant> Reletedmerchant { get; set; }
        public List<ServiceAvailablityModel> serviceAvailablities { get; set; }
        public List<AminitiesModel> aminities { get; set; }
        public List<Merchantgallery> merchantgalleries { get; set; }
        public List<CouponModel> coupons { get; set; }
    }
    public class CouponModel
    {
        public string CID { get; set; } = "";
        public string TemplatePath { get; set; } = "";
        public string BusinessName { get; set; } = "";
        public string FestivalName { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string Name { get; set; } = "";
        public string Discount { get; set; } = "";
        public string CouponsCode { get; set; } = "";
        public string Description { get; set; } = "";
        public string ExpireDate { get; set; } = "";
        public string BgColor { get; set; } = "";
        public string Color { get; set; } = "";
        public string Noofdays { get; set; } = "";
        public string Amount { get; set; } = "";
        public string TextColor { get; set; } = "";
        public string Remark { get; set; } = "";
        public string Expiry { get; set; } = "";
        public string AddDate { get; set; } = "";
    }
    public class Merchantgallery
    {
        public int ID { get; set; }
        public string IMG { get; set; }
    }
    public class AminitiesModel
    {
        public int ID { get; set; }
        public string Aminities { get; set; }
    }
    public class ServiceAvailablityModel
    {
        public string WeekName { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string Status { get; set; }
    }
    public class StateModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class MyOrderModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string MerchantName { get; set; }
        public string ShopName { get; set; }
        public string MerchantAddress { get; set; }
        public string PaymentMode { get; set; }
        public string UPI { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string TransactionNo { get; set; }
        public string ScreenShot { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string ShippedRemark { get; set; }
        public string DeliveredRemark { get; set; }
        public string CancelRemark { get; set; }
        public List<OrderProductReport> productReports { get; set; }
    }
    public class OrderProductReport
    {
        public int OrderID { get; set; }
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string IMG1 { get; set; }
        public string Qty { get; set; }
        public string Amount { get; set; }
        public string NetAmount { get; set; }
        public string OrderStatus { get; set; }
    }
}