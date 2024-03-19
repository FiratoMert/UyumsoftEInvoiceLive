using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UyumsoftEInvoiceLive
{
    public class EInvoiceIncomingDTO
    {
        public DateTime CreateDate { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string UUID { get; set; }
        public string Title { get; set; }
        public string VKN { get; set; }
        public string ProfileId { get; set; }
        public string InvoiceType { get; set; }
        public string Currency { get; set; }
        public string TaxInclusiveValue { get; set; }
        public string TaxExlusiveValue { get; set; }
        public string Tax { get; set; }
        public string Status { get; set; }
        public string ErpStatus { get; set; }
        public string ExchangeRate { get; set; }

    }
}
