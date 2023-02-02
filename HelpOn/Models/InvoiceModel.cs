using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOn.Models
{
    public class InvoiceModel
    {
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public string CustomerMobile { get; set; }
        public string ShopName { get; set; }
        public string Merchantname { get; set; }
        public string PaymentMode { get; set; }
        public string UPI { get; set; }
        public string BankName { get; set; }
        public string IFSCCode { get; set; }
        public string TransactionNo { get; set; }
        public string AddDate { get; set; }
        public string Pincode { get; set; }
        public string Merchantmobile { get; set; }
        public string MerchantAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal PaybleAmount { get; set; }
        public List<InvoiceProduct> products { get; set; }
    }
    public class InvoiceProduct
    {
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Qty { get; set; }
        public string NetAmount { get; set; }
    }
}