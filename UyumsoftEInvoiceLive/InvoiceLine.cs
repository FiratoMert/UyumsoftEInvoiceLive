using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UyumsoftEInvoiceLive
{
    public class InvoiceLine
    {
        public string InvoiceLineId { get; set; }
        public string InvoiceLineNote { get; set; }
        public string InvoiceLineInvoicedQuantityUnitCode { get; set; }
        /// <summary>
        /// Miktar
        /// </summary>
        public decimal InvoiceLineInvoicedQuantityValue { get; set; }
        public string InvoiceLineLineExtensionAmountCurrencyId { get; set; }
        public decimal InvoiceLineLineExtensionAmountValue { get; set; }
        public string InvoiceLineTaxAmountCurrencyId { get; set; }
        public decimal InvoiceLineTaxAmountValue { get; set; }
        public string InvoiceLineTaxSubTotalTaxableAmountCurrencyId { get; set; }
        public decimal InvoiceLineTaxSubTotalTaxableAmountValue { get; set; }
        public string InvoiceLineTaxSubTotalTaxAmountCurrencyId { get; set; }
        public decimal InvoiceLineTaxSubTotalTaxAmountValue { get; set; }
        public decimal InvoiceLineTaxSubTotalPercent { get; set; }
        public string InvoiceLineTaxSubTotalTaxSchemeName { get; set; }
        public string InvoiceLineTaxSubTotalTaxSchemeTaxTypeCode { get; set; }
        public string InvoiceLineItemDescriptionValue { get; set; }
        /// <summary>
        /// Mal Hizmet
        /// </summary>
        public string InvoiceLineItemNameValue { get; set; }
        public string InvoiceLineItemModelNameValue { get; set; }
        public string InvoiceLinePricePriceAmountCurrencyId { get; set; }
        public decimal InvoiceLinePricePriceAmountValue { get; set; }
    }
}
