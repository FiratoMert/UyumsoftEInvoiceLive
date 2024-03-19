using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UyumsoftEInvoiceLive
{
    public class SendEInvoiceDTO
    {
        #region Fatura Bilgileri

        public string InvoiceID { get; set; }
        /// <summary>
        /// TEMELFATURA ya da TICARIFATURA olarak girilmesi gerekir.
        /// </summary>
        public string ProfileID { get; set; }
        public bool CopyIndicator { get; set; } = false;
        public DateTime IssueDate { get; set; }
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// SATIŞ - TEVKIFAT - IADE - ISTISNA tiplerinden birinin girilmesi gerekir.
        /// </summary>
        public string InvoiceTypeCode { get; set; }
        public string InvoiceNote { get; set; }
        public string DocumentCurrencyCode { get; set; }
        public decimal LineCountNumeric { get; set; }

        #endregion

        #region Fatura Gönderici Firma Bilgileri

        /// <summary>
        /// VKN ya da TCKN hangisi ise o yazılacak
        /// </summary>
        public string SenderCompanyPartyIdentificationschemeID { get; set; }
        /// <summary>
        /// Gönderen Firma VKN ya da TCKN numarası girilecek 
        /// </summary>
        public string SenderCompanyPartyIdentificationVKNTCKNNo { get; set; }
        public string SenderCompanyPartyIdentificationMersisNo { get; set; }
        public string SenderCompanyPartyIdentificationTicaretSicilNo { get; set; }
        public string SenderCompanyPartyName { get; set; }
        /// <summary>
        /// Gönderen Firma Full Adresi
        /// </summary>
        public string SenderCompanyPostalAddressStreetName { get; set; }
        /// <summary>
        /// Gönderen Firma İlçesi
        /// </summary>
        public string SenderCompanyPostalAddressCitySubdivisionName { get; set; }
        /// <summary>
        /// Gönderen Firma İli
        /// </summary>
        public string SenderCompanyPostalAddressCityName { get; set; }
        /// <summary>
        /// Gönderen Firma Ülkesi
        /// </summary>
        public string SenderCompanyPostalAddressCountryName { get; set; }
        /// <summary>
        /// Gönderen Firma Vergi Dairesi
        /// </summary>
        public string SenderCompanyTaxSchemeName { get; set; }
        /// <summary>
        /// Gönderen Firma Adı 
        /// </summary>
        public string SenderCompanyPersonFirstName { get; set; }
        /// <summary>
        /// Gönderen Firma Soyadı - Şahıs İse 
        /// </summary>
        public string SenderCompanyPersonFamilyName { get; set; }

        #endregion

        #region Fatura Alıcı Firma Bilgileri

        /// <summary>
        /// Alıcı Firma TCKN ya da VKN şeklinde girmek lazım
        /// </summary>
        public string CustomerCompanySchemeID { get; set; }
        /// <summary>
        /// Alıcı Firma TCKN ya da VKN numarası 
        /// </summary>
        public string CustomerCompanyVKNTCKNNo { get; set; }
        public string CustomerCompanyPersonName { get; set; }
        public string CustomerCompanyPersonFamilyName { get; set; }
        public string CustomerCompanyPostalAddressRoom { get; set; }
        /// <summary>
        /// Alıcı Firma Full Adresi
        /// </summary>
        public string CustomerCompanyPostalAddressStreetName { get; set; }
        public string CustomerCompanyPostalAddressBuildingNumber { get; set; }
        /// <summary>
        /// Alıcı Firma İlçesi
        /// </summary>
        public string CustomerCompanyPostalAddressCitySubdivisionName { get; set; }
        public string CustomerCompanyPostalAddressCityName { get; set; }
        public string CustomerCompanyPostalAddressCountryName { get; set; }
        /// <summary>
        /// Alıcı Firma Vergi Dairesi Adı 
        /// </summary>
        public string CustomerCompanyTaxSchemeName { get; set; }
        public string CustomerCompanyTelephoneNumber { get; set; }
        #endregion

        #region Toplam Vergi Bilgileri

        public string TaxTotalTaxAmountCurrencyID { get; set; }
        public decimal TaxTotalTaxAmountValue { get; set; }
        public string TaxSubTotalTaxableAmountCurrencyId { get; set; }
        public decimal TaxSubTotalTaxableAmountValue { get; set; }
        public string TaxSubTotalTaxAmountCurrencyId { get; set; }
        /// <summary>
        /// Hesaplanan (%)
        /// </summary>
        public decimal TaxSubTotalTaxAmountValue { get; set; }
        public decimal TaxSubTotalPercentValue { get; set; }
        public string TaxSubtotalTaxCategoryTaxSchemeName { get; set; }
        public string TaxSubtotalTaxCategoryTaxSchemeTaxTypeCode { get; set; }

        #endregion

        #region Genel Ücret 

        public string LineExtensionAmountcurrencyID { get; set; }
        public decimal LineExtensionAmountValue { get; set; }
        public string TaxExclusiveAmountCurrencyId { get; set; }
        public decimal TaxExclusiveAmountValue { get; set; }
        public string TaxInclusiveAmountCurrencyId { get; set; }
        /// <summary>
        /// Vergiler Dahil Toplam Tutar
        /// </summary>
        public decimal TaxInclusiveAmountValue { get; set; }
        public string AllowanceTotalAmountCurrenyId { get; set; }
        public string PayableAmountCurrencyId { get; set; }
        /// <summary>
        /// Ödenecek Tutar
        /// </summary>
        public decimal PayableAmountValue { get; set; }

        #endregion

        #region Faturadaki Ürünlerin Listesi (Dizi)
        public InvoiceLine[] InvoiceLine { get; set; }

        #endregion

        public Uyumsoft.InvoiceDeliveryType EArchiveInvoiceInfoDeliveryType { get; set; }
        public Uyumsoft.InvoiceScenarioChoosen Scenario { get; set; }
        public string NotificationMailingSubject { get; set; }
        public bool NotificationMailingEnableNotification { get; set; }
        public string NotificationMailingTo { get; set; }
        public bool NotificationMailingAttachmentXml { get; set; }
        public bool NotificationMailingAttachmentPdf { get; set; }

        public string LocalDocumentId { get; set; }

    }
}
