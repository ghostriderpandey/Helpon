using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOn.Models
{
    public class ShoppingModel
    {
    }
    public class CartModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int PID { get; set; }
        public int MID { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public string IMG { get; set; }
        public decimal MRP { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
       

    }
    public class ApplyCouponCode
    {
        public int MID { get; set; }
        public string Code { get; set; }
    }
    public class CartTotal
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal PaybleAmount { get; set; }
        public string Code { get; set; }
    }
    public class CheckOut
    {
        public int MID { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNo { get; set; }
        public string Address { get; set; }
        public HttpPostedFileBase Screenshot { get; set; }
        public string TotalAmount { get; set; }
        public string Discount { get; set; }
        public string PaybleAmount { get; set; }
        public string CouponCode { get; set; }
    }
}