using HelpOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HelpOn.Helper;

namespace HelpOn.Repository
{
    public class LoginRepository
    {
        public HomeModel GetHomeDetail()
        {
            HomeModel home = new HomeModel();
            home.sliders = GetSlider().FindAll(e => e.AddByType == "Admin");
            if (home.sliders.Count <= 0)
            {
                home.sliders = new List<SliderModel>();
            }
            home.categories = GetCategory("GetCategory").FindAll(e => e.Type == "Main");
            if (home.categories.Count <= 0)
            {
                home.categories = new List<CategoryModel>();
            }
            home.popularCategories = GetPopularCategory();
            if (home.popularCategories.Count <= 0)
            {
                home.popularCategories = new List<PopularCategoryModel>();
            }
            home.popularCities = GetPopularCity();
            if (GetBanner().Count > 0)
            {
                if (GetBanner().Count > 0)
                {
                    home.Banner1 = GetBanner().Find(e => e.Position == 1 && e.Type == "IMG").Img;
                }
                if (GetBanner().Count > 1)
                {
                    home.Banner2 = GetBanner().Find(e => e.Position == 2 && e.Type == "IMG").Img;
                }
                if (GetBanner().Count > 2)
                {
                    home.Banner3 = GetBanner().Find(e => e.Position == 3 && e.Type == "IMG").Img;
                }

                if (GetBanner().Count > 3)
                {
                    home.Video1 = GetBanner().Find(e => e.Position == 1 && e.Type == "Video").Img;
                }
                if (GetBanner().Count > 4)
                {
                    home.Video2 = GetBanner().Find(e => e.Position == 2 && e.Type == "Video").Img;
                }
                if (GetBanner().Count > 5)
                {
                    home.Video3 = GetBanner().Find(e => e.Position == 3 && e.Type == "Video").Img;
                }
            }
            return home;
        }
        public List<PopularCategoryModel> GetPopularCategory()
        {
            List<PopularCategoryModel> sliders = new List<PopularCategoryModel>();
            sliders = Connection.Query<PopularCategoryModel>("Exec ProcGet_List 'GetPopularCategory'").ToList();
            if (sliders.Count <= 0)
            {
                sliders = new List<PopularCategoryModel>();
            }
            return sliders;
        }
        public List<PopularCityModel> GetPopularCity()
        {
            List<PopularCityModel> sliders = new List<PopularCityModel>();
            sliders = Connection.Query<PopularCityModel>("Exec ProcMaster_PopularCites 'GetAll'").ToList();
            if (sliders.Count <= 0)
            {
                sliders = new List<PopularCityModel>();
            }
            return sliders;
        }
        public List<StateModel> GetState()
        {
            List<StateModel> sliders = new List<StateModel>();
            sliders = Connection.Query<StateModel>("Exec ProcGet_List 'GetState'").ToList();
            if (sliders.Count <= 0)
            {
                sliders = new List<StateModel>();
            }
            return sliders;
        }
        public List<StateModel> GetCity(int id)
        {
            List<StateModel> sliders = new List<StateModel>();
            sliders = Connection.Query<StateModel>("Exec ProcGet_List 'GetCity','" + id + "'").ToList();
            if (sliders.Count <= 0)
            {
                sliders = new List<StateModel>();
            }
            return sliders;
        }
        public List<SliderModel> GetSlider()
        {
            List<SliderModel> sliders = new List<SliderModel>();
            sliders = Connection.Query<SliderModel>("Exec ProcGet_List 'GetSlider'").ToList();
            if (sliders.Count <= 0)
            {
                sliders = new List<SliderModel>();
            }
            return sliders;
        }
        public List<SliderModel> GetBanner()
        {
            List<SliderModel> sliders = new List<SliderModel>();
            sliders = Connection.Query<SliderModel>("Exec ProcGet_List 'GetBanner'").ToList();
            if (sliders.Count <= 0)
            {
                sliders = new List<SliderModel>();
            }
            return sliders;
        }
        public List<CategoryModel> GetCategory(string Name)
        {
            List<CategoryModel> sliders = new List<CategoryModel>();
            sliders = Connection.Query<CategoryModel>("Exec ProcGet_List '" + Name + "'").ToList();
            if (sliders.Count <= 0)
            {
                sliders = new List<CategoryModel>();
            }
            return sliders;
        }
        public List<MasterMerchant> GetMerchant()
        {
            List<MasterMerchant> merchants = new List<MasterMerchant>();
            merchants = Connection.Query<MasterMerchant>("select * from ViewManage_Merchant").ToList();
            if (merchants.Count <= 0)
            {
                merchants = new List<MasterMerchant>();
            }
            return merchants;
        }
        public List<MerchantViewModel> GetMainCategoryFooter(int? cId)
        {
            List<MerchantViewModel> CatName = new List<MerchantViewModel>();
            CatName = Connection.Query<MerchantViewModel>("select * from tblMaster_Category where Id='" + cId + "'").ToList();
            if (CatName.Count <= 0)
            {
                CatName = new List<MerchantViewModel>();
            }
            return CatName;
        }
        public List<ProductModel> GetMerchantProduct()
        {
            List<ProductModel> products = new List<ProductModel>();
            products = Connection.Query<ProductModel>("Exec ProcGet_List 'GetMerchantProduct','" + SessionHelper.CustomerID + "'").ToList();
            return products;
        }
        public MerchantDetailModel GetMerchantDetail(int id)
        {
            MerchantDetailModel merchant = new MerchantDetailModel();
            merchant.merchant = GetMerchant().FindAll(e => e.MID == id).FirstOrDefault();
            merchant.products = GetMerchantProduct().FindAll(e => e.MID == id);
            merchant.Reletedmerchant = GetMerchant().FindAll(e => e.CID == merchant.merchant.CID || e.SCID == merchant.merchant.SCID).FindAll(e => e.MID != id);
            merchant.serviceAvailablities = Connection.Query<ServiceAvailablityModel>("Exec ProcMaster_Merchant 'GetMerchantServiceAvailablity','" + id + "'").ToList();
            merchant.aminities = Connection.Query<AminitiesModel>("select * from tblManage_Aminities where MID='" + id + "'").ToList();
            merchant.merchantgalleries = Connection.Query<Merchantgallery>("ProcGet_List 'GetGallery','" + id + "'").ToList();
            merchant.coupons = Connection.Query<CouponModel>("ProcMaster_MerchantCoupon 'GetAll',@MId='" + id + "'").ToList();
            if (merchant.aminities.Count <= 0)
            {
                merchant.aminities = new List<AminitiesModel>();
            }

            if (merchant.serviceAvailablities != null && merchant.serviceAvailablities.Count <= 0)
            {
                merchant.serviceAvailablities = new List<ServiceAvailablityModel>();
            }
            if (merchant.Reletedmerchant.Count <= 0)
            {
                merchant.Reletedmerchant = new List<MasterMerchant>();
            }
            if (merchant.merchantgalleries.Count <= 0)
            {
                merchant.merchantgalleries = new List<Merchantgallery>();
            }
            merchant.sliders = GetSlider().FindAll(e => e.AddByID == id && e.AddByType == "Merchant");
            return merchant;

        }
        public List<CartModel> GetCart(int MID, int CustomerID)
        {
            List<CartModel> carts = new List<CartModel>();
            carts = Connection.Query<CartModel>("Exec ProcManage_Cart 'GetCart','" + CustomerID + "','" + MID + "'").ToList();
            if (carts.Count <= 0)
            {
                carts = new List<CartModel>();
            }
            return carts;
        }
        public List<MyOrderModel> GetMyOrder()
        {
            List<MyOrderModel> myOrders = new List<MyOrderModel>();
            myOrders = Connection.Query<MyOrderModel>("Exec ProcManage_Report 'OrderReport','" + SessionHelper.CustomerID + "'").ToList();
            if (myOrders.Count <= 0)
            {
                myOrders = new List<MyOrderModel>();
            }
            else
            {
                foreach (var item in myOrders)
                {
                    List<OrderProductReport> orderProducts = new List<OrderProductReport>();
                    orderProducts = Connection.Query<OrderProductReport>("Exec ProcManage_Report 'OrderReportWithProduct','" + item.ID + "'").ToList();
                    if (orderProducts.Count <= 0)
                    {
                        orderProducts = new List<OrderProductReport>();
                    }
                    item.productReports = orderProducts;
                }
            }
            return myOrders;
        }
        public List<MerchantBankDetail> GetMerchantBankDetail()
        {
            List<MerchantBankDetail> bankDetails = new List<MerchantBankDetail>();
            bankDetails = Connection.Query<MerchantBankDetail>("Exec ProcGet_List 'GetMerchantBankDetail'").ToList();
            if (bankDetails.Count <= 0)
            {
                bankDetails = new List<MerchantBankDetail>();
            }
            return bankDetails;
        }
    }
}