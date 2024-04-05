using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using UyumsoftEInvoiceLive.Despatch;
using UyumsoftEInvoiceLive.Uyumsoft;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UyumsoftEInvoiceLive
{
    public class Client
    {
        Uyumsoft.BasicIntegrationClient basicIntegrationClient = new Uyumsoft.BasicIntegrationClient();
        Uyumsoft.UserInformation userInformation = new Uyumsoft.UserInformation();
        Uyumsoft.InboxInvoiceListQueryModel inboxInvoiceListQueryModel = new Uyumsoft.InboxInvoiceListQueryModel();
        Uyumsoft.InboxInvoiceListResponse inboxInvoiceListResponse = new Uyumsoft.InboxInvoiceListResponse();

        Despatch.BasicDespatchIntegrationClient basicDespatchIntegrationClient = new Despatch.BasicDespatchIntegrationClient();
        Despatch.InboxDespatchListQueryModel inboxDespatchListQueryModel = new Despatch.InboxDespatchListQueryModel();
        Despatch.UserInformation despatchUserInformation = new Despatch.UserInformation();

        public List<EInvoiceIncomingDTO> GetInboxInvoiceList(string username, string password, DateTime startDate, DateTime endDate, int? pageSize = int.MaxValue)
        {
            try
            {
                userInformation.Username = username;
                userInformation.Password = password;

                inboxInvoiceListQueryModel.PageSize = (int)pageSize;
                inboxInvoiceListQueryModel.CreateEndDate = endDate;
                inboxInvoiceListQueryModel.CreateStartDate = startDate;
                inboxInvoiceListQueryModel.OnlyNewestInvoices = false;

                inboxInvoiceListResponse = basicIntegrationClient.GetInboxInvoiceList(userInformation, inboxInvoiceListQueryModel);

                List<EInvoiceIncomingDTO> eInvoiceIncomingDTOs = new List<EInvoiceIncomingDTO>();

                if (inboxInvoiceListResponse.Value.TotalCount != 0)
                {
                    foreach (var item in inboxInvoiceListResponse.Value.Items)
                    {
                        EInvoiceIncomingDTO eInvoiceIncomingDTO = new EInvoiceIncomingDTO();

                        eInvoiceIncomingDTO.InvoiceNo = item.InvoiceId.ToString();
                        eInvoiceIncomingDTO.CreateDate = item.CreateDateUtc.AddHours(3);
                        eInvoiceIncomingDTO.Title = item.TargetTitle.ToString();
                        eInvoiceIncomingDTO.VKN = item.TargetTcknVkn.ToString();
                        if (item.ExecutionDate != null)
                        {
                            DateTime time = (DateTime)item.ExecutionDate;
                            eInvoiceIncomingDTO.InvoiceDate = time.ToString("dd.MM.yyyy");
                        }
                        eInvoiceIncomingDTO.UUID = item.DocumentId.ToString();
                        eInvoiceIncomingDTO.ProfileId = item.Type.ToString();
                        eInvoiceIncomingDTO.InvoiceType = item.InvoiceTipType.ToString();
                        eInvoiceIncomingDTO.Currency = item.DocumentCurrencyCode.ToString();
                        eInvoiceIncomingDTO.TaxInclusiveValue = item.PayableAmount.ToString("N4");
                        eInvoiceIncomingDTO.TaxExlusiveValue = item.TaxExclusiveAmount.ToString("N4");
                        eInvoiceIncomingDTO.Tax = item.TaxTotal.ToString();
                        eInvoiceIncomingDTO.ExchangeRate = item.ExchangeRate.ToString("N4");

                        switch (item.Status.ToString())
                        {
                            case "Approved":
                                eInvoiceIncomingDTO.Status = "ONAYLANDI";
                                break;

                            case "WaitingForAprovement":
                                eInvoiceIncomingDTO.Status = "ONAY BEKLİYOR";
                                break;

                            case "Declined":
                                eInvoiceIncomingDTO.Status = "REDDEDİLDİ";
                                break;

                            case "Return":
                                eInvoiceIncomingDTO.Status = "İADE EDİLDİ";
                                break;

                            default:
                                eInvoiceIncomingDTO.Status = "E-ARŞİV İPTAL";
                                break;
                        }

                        switch (item.Type.ToString())
                        {
                            case "BaseInvoice":
                                eInvoiceIncomingDTO.ProfileId = "TEMEL FATURA";
                                break;

                            case "ComercialInvoice":
                                eInvoiceIncomingDTO.ProfileId = "TİCARİ FATURA";
                                break;

                            default:
                                eInvoiceIncomingDTO.ProfileId = "BELİRTİLMEMİŞ";
                                break;
                        }

                        switch (item.InvoiceTipType.ToString())
                        {
                            case "Sales":
                                eInvoiceIncomingDTO.InvoiceType = "SATIŞ";
                                break;

                            case "Return":
                                eInvoiceIncomingDTO.InvoiceType = "İADE";
                                break;

                            case "Exception":
                                eInvoiceIncomingDTO.InvoiceType = "İSTİSNA";
                                break;

                            case "Tax":
                                eInvoiceIncomingDTO.InvoiceType = "TEVKİFAT";
                                break;

                            default:
                                eInvoiceIncomingDTO.InvoiceType = "BELİRTİLMEMİŞ";
                                break;
                        }

                        eInvoiceIncomingDTOs.Add(eInvoiceIncomingDTO);
                    }


                }

                return eInvoiceIncomingDTOs;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public InvoiceDataResponse GetInboxInvoicePdf(string username, string password, string UUID)
        {
            try
            {
                userInformation.Username = username;
                userInformation.Password = password;

                var result = basicIntegrationClient.GetInboxInvoicePdf(userInformation, UUID);

                return result;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public InvoiceDataResponse GetInboxInvoiceData(string username, string password, string UUID)
        {
            try
            {
                userInformation.Username = username;
                userInformation.Password = password;

                var result = basicIntegrationClient.GetInboxInvoiceData(userInformation, UUID);

                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public InvoiceInfo GetInvoiceInfo(string username, string password, string UUID)
        {
            try
            {
                userInformation.Username = username;
                userInformation.Password = password;

                var result = basicIntegrationClient.GetInboxInvoice(userInformation, UUID);

                return result.Value;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool AcceptInvoice(string username, string password, string UUID)
        {
            try
            {
                userInformation.Username = username;
                userInformation.Password = password;


                DocumentResponseInfo[] documentResponseInfos = new DocumentResponseInfo[1];
                DocumentResponseInfo documentResponseInfo = new DocumentResponseInfo();
                documentResponseInfo.InvoiceId = UUID;
                documentResponseInfo.ResponseStatus = DocumentResponseStatus.Approved;

                documentResponseInfos[0] = documentResponseInfo;

                Uyumsoft.FlagResponse flagResponse = basicIntegrationClient.SendDocumentResponse(userInformation, documentResponseInfos);

                return flagResponse.Value;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool RejectInvoice(string username, string password, string UUID)
        {
            try
            {
                userInformation.Username = username;
                userInformation.Password = password;


                DocumentResponseInfo[] documentResponseInfos = new DocumentResponseInfo[1];
                DocumentResponseInfo documentResponseInfo = new DocumentResponseInfo();
                documentResponseInfo.InvoiceId = UUID;
                documentResponseInfo.ResponseStatus = DocumentResponseStatus.Declined;

                documentResponseInfos[0] = documentResponseInfo;

                Uyumsoft.FlagResponse flagResponse = basicIntegrationClient.SendDocumentResponse(userInformation, documentResponseInfos);

                return flagResponse.Value;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<EinvoiceDetailDTO> GetInvoiceLineInfo(string username, string password, string UUID)
        {
            try
            {
                userInformation.Username = username;
                userInformation.Password = password;
                List<EinvoiceDetailDTO> einvoiceDetailDTOs = new List<EinvoiceDetailDTO>();

                var result = basicIntegrationClient.GetInboxInvoice(userInformation, UUID);

                if (result.Value.Invoice.InvoiceLine.Count() != 0)
                {

                    foreach (var item in result.Value.Invoice.InvoiceLine)
                    {
                        EinvoiceDetailDTO einvoiceDetailDTO = new EinvoiceDetailDTO();

                        einvoiceDetailDTO.SiraNo = item.ID.Value;
                        einvoiceDetailDTO.MalHizmet = item.Item.Name.Value;
                        einvoiceDetailDTO.Miktar = item.InvoicedQuantity.Value.ToString();
                        einvoiceDetailDTO.BirimFiyat = item.Price.PriceAmount.Value.ToString();
                        einvoiceDetailDTO.ParaBirimi = item.Price.PriceAmount.currencyID.ToString();
                        einvoiceDetailDTO.KdvOrani = item.TaxTotal.TaxSubtotal[0].Percent.Value.ToString();
                        einvoiceDetailDTO.KdvTutari = item.TaxTotal.TaxAmount.Value.ToString();
                        einvoiceDetailDTO.MalHizmetTutari = item.LineExtensionAmount.Value.ToString();
                        if (item.Item.SellersItemIdentification != null)
                        {
                            einvoiceDetailDTO.MalHizmetKod = item.Item.SellersItemIdentification.ID.Value;

                        }

                        einvoiceDetailDTOs.Add(einvoiceDetailDTO);
                    }
                }

                return einvoiceDetailDTOs;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<EDespatchIncomingDTO> GetInboxDespatchList(string username, string password, DateTime Str, DateTime End, int? pageSize = int.MaxValue)
        {
            despatchUserInformation.Username = username;
            despatchUserInformation.Password = password;


            inboxDespatchListQueryModel.CreateStartDate = Str;
            inboxDespatchListQueryModel.CreateEndDate = End;
            inboxDespatchListQueryModel.PageSize = (int)pageSize;


            InboxDespatchListResponse inboxDespatchListResponse = basicDespatchIntegrationClient.GetInboxDespatchList(despatchUserInformation, inboxDespatchListQueryModel);

            List<EDespatchIncomingDTO> eDespatchIncomingDTOs = new List<EDespatchIncomingDTO>();

            if (inboxDespatchListResponse.Value.TotalCount != 0)
            {
                foreach (var item in inboxDespatchListResponse.Value.Items)
                {
                    EDespatchIncomingDTO eDespatchIncomingDTO = new EDespatchIncomingDTO();
                    eDespatchIncomingDTO.UUID = item.DespatchId;
                    eDespatchIncomingDTO.DespatchNo = item.DespatchNumber;
                    eDespatchIncomingDTO.ActualShipmentDate = (DateTime)item.ActualDespatchDate;
                    eDespatchIncomingDTO.VKN = item.TargetTcknVkn;
                    eDespatchIncomingDTO.Title = item.TargetTitle;
                    eDespatchIncomingDTO.DespatchDate = (DateTime)item.IssueDate;
                    eDespatchIncomingDTO.CreateDate = item.CreateDateUtc.AddHours(3);

                    eDespatchIncomingDTOs.Add(eDespatchIncomingDTO);
                }
            }

            return eDespatchIncomingDTOs;
        }

        public DespatchData GetInboxDespatchPdf(string username, string password, string UUID)
        {
            try
            {
                despatchUserInformation.Username = username;
                despatchUserInformation.Password = password;


                var result = basicDespatchIntegrationClient.GetInboxDespatchPdf(despatchUserInformation, UUID);
                PagedResponseOfDespatchData pagedResponseOfDespatchData = result.Value;
                DespatchData despatchData = pagedResponseOfDespatchData.Items[0];

                return despatchData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string SendInvoice(string username, string password, SendEInvoiceDTO sendEInvoiceDTO)
        {
            userInformation.Username = username;
            userInformation.Password = password;

            InvoiceType invoiceType = new InvoiceType();

            #region Fatura Bilgileri
            invoiceType.ID = new Uyumsoft.IDType() { Value = sendEInvoiceDTO.InvoiceID };
            invoiceType.ProfileID = new Uyumsoft.ProfileIDType() { Value = sendEInvoiceDTO.ProfileID };
            invoiceType.CopyIndicator = new Uyumsoft.CopyIndicatorType() { Value = sendEInvoiceDTO.CopyIndicator };
            invoiceType.IssueDate = new Uyumsoft.IssueDateType() { Value = sendEInvoiceDTO.IssueDate };
            invoiceType.IssueTime = new Uyumsoft.IssueTimeType() { Value = sendEInvoiceDTO.IssueTime };
            invoiceType.InvoiceTypeCode = new Uyumsoft.InvoiceTypeCodeType() { Value = sendEInvoiceDTO.InvoiceTypeCode };
            invoiceType.Note = new Uyumsoft.NoteType[] { new Uyumsoft.NoteType() { Value = sendEInvoiceDTO.InvoiceNote } };
            invoiceType.DocumentCurrencyCode = new Uyumsoft.DocumentCurrencyCodeType() { Value = sendEInvoiceDTO.DocumentCurrencyCode };
            invoiceType.LineCountNumeric = new Uyumsoft.LineCountNumericType() { Value = sendEInvoiceDTO.LineCountNumeric };
            #endregion

            #region Fatura Gönderen Firma Bilgileri
            Uyumsoft.PartyType partyType = new Uyumsoft.PartyType()
            {
                PartyIdentification = new Uyumsoft.PartyIdentificationType[] {
                    new Uyumsoft.PartyIdentificationType() { ID = new Uyumsoft.IDType() { schemeID = sendEInvoiceDTO.SenderCompanyPartyIdentificationschemeID, Value = sendEInvoiceDTO.SenderCompanyPartyIdentificationVKNTCKNNo } },//GÖNDEREN FİRMA VERGİ KİMLİK NO YA DA TCKN TC KİMLİK NO 
                    new Uyumsoft.PartyIdentificationType() { ID = new Uyumsoft.IDType(){ schemeID = "MERSISNO", Value = sendEInvoiceDTO.SenderCompanyPartyIdentificationMersisNo } },
                    new Uyumsoft.PartyIdentificationType() { ID = new Uyumsoft.IDType() { schemeID = "TICARETSICILNO", Value = sendEInvoiceDTO.SenderCompanyPartyIdentificationTicaretSicilNo } },
                },
                PartyName = new Uyumsoft.PartyNameType() { Name = new Uyumsoft.NameType1 { Value = sendEInvoiceDTO.SenderCompanyPartyName } },
                PostalAddress = new Uyumsoft.AddressType()
                {
                    StreetName = new Uyumsoft.StreetNameType() { Value = sendEInvoiceDTO.SenderCompanyPostalAddressStreetName },
                    CitySubdivisionName = new Uyumsoft.CitySubdivisionNameType() { Value = sendEInvoiceDTO.SenderCompanyPostalAddressCitySubdivisionName },
                    CityName = new Uyumsoft.CityNameType() { Value = sendEInvoiceDTO.SenderCompanyPostalAddressCityName },
                    Country = new Uyumsoft.CountryType() { Name = new Uyumsoft.NameType1() { Value = sendEInvoiceDTO.SenderCompanyPostalAddressCountryName } }
                },
                PartyTaxScheme = new Uyumsoft.PartyTaxSchemeType() { TaxScheme = new Uyumsoft.TaxSchemeType() { Name = new Uyumsoft.NameType1() { Value = sendEInvoiceDTO.SenderCompanyTaxSchemeName } } },
                Person = new Uyumsoft.PersonType() { FirstName = new Uyumsoft.FirstNameType() { Value = sendEInvoiceDTO.SenderCompanyPersonFirstName }, FamilyName = new Uyumsoft.FamilyNameType() { Value = sendEInvoiceDTO.SenderCompanyPersonFamilyName } }
            };

            invoiceType.AccountingSupplierParty = new Uyumsoft.SupplierPartyType() { Party = partyType };

            #endregion

            #region Fatura Alıcı Firma Bilgileri 
            Uyumsoft.CustomerPartyType customerParty = new Uyumsoft.CustomerPartyType()
            {
                Party = new Uyumsoft.PartyType()
                {
                    PartyName = new Uyumsoft.PartyNameType() { Name = new Uyumsoft.NameType1() { Value = sendEInvoiceDTO.CustomerCompanyPartyName } },
                    PartyIdentification = new Uyumsoft.PartyIdentificationType[] { new Uyumsoft.PartyIdentificationType() { ID = new Uyumsoft.IDType() { schemeID = sendEInvoiceDTO.CustomerCompanySchemeID, Value = sendEInvoiceDTO.CustomerCompanyVKNTCKNNo } } }, //FATURA ALICI VKN VERGİ KİMLİK NO YA DA TCKN TC KİMLİK NO 
                    Person = new Uyumsoft.PersonType() { FirstName = new Uyumsoft.FirstNameType() { Value = sendEInvoiceDTO.CustomerCompanyPersonName }, FamilyName = new Uyumsoft.FamilyNameType() { Value = sendEInvoiceDTO.CustomerCompanyPersonFamilyName } }, //ALICI ADI SOYADI
                    PostalAddress = new Uyumsoft.AddressType() //FATURA ALICI ADRES BİLGİLERİ 
                    {
                        Room = new Uyumsoft.RoomType() { Value = sendEInvoiceDTO.CustomerCompanyPostalAddressRoom },
                        StreetName = new Uyumsoft.StreetNameType() { Value = sendEInvoiceDTO.CustomerCompanyPostalAddressStreetName }, // ALICI ADRESİ 
                        BuildingNumber = new Uyumsoft.BuildingNumberType() { Value = sendEInvoiceDTO.CustomerCompanyPostalAddressBuildingNumber }, // ALICI BİNA NO 
                        CitySubdivisionName = new Uyumsoft.CitySubdivisionNameType() { Value = sendEInvoiceDTO.CustomerCompanyPostalAddressCitySubdivisionName },//ALICI ADRES İLÇE
                        CityName = new Uyumsoft.CityNameType() { Value = sendEInvoiceDTO.CustomerCompanyPostalAddressCityName },
                        Country = new Uyumsoft.CountryType() { Name = new Uyumsoft.NameType1() { Value = sendEInvoiceDTO.CustomerCompanyPostalAddressCountryName } }
                    },
                    PartyTaxScheme = new Uyumsoft.PartyTaxSchemeType() { TaxScheme = new Uyumsoft.TaxSchemeType() { Name = new Uyumsoft.NameType1() { Value = sendEInvoiceDTO.CustomerCompanyTaxSchemeName } } },
                    Contact = new Uyumsoft.ContactType() { Telephone = new Uyumsoft.TelephoneType() { Value = sendEInvoiceDTO.CustomerCompanyTelephoneNumber } } //ALICI TELEFONU
                }
            };
            invoiceType.AccountingCustomerParty = customerParty;

            #endregion

            #region Taksit Bilgileri

            invoiceType.TaxTotal = sendEInvoiceDTO.TaxTotalTypes;

            /*{
                new Uyumsoft.TaxTotalType(){
                    TaxAmount = new Uyumsoft.TaxAmountType(){ currencyID = sendEInvoiceDTO.TaxTotalTaxAmountCurrencyID, Value = sendEInvoiceDTO.TaxTotalTaxAmountValue },
                    TaxSubtotal = new Uyumsoft.TaxSubtotalType[]{
                        new Uyumsoft.TaxSubtotalType(){
                            TaxableAmount = new Uyumsoft.TaxableAmountType() { currencyID = sendEInvoiceDTO.TaxSubTotalTaxableAmountCurrencyId,Value= sendEInvoiceDTO.TaxSubTotalTaxableAmountValue },
                            TaxAmount = new Uyumsoft.TaxAmountType() { currencyID = sendEInvoiceDTO.TaxSubTotalTaxAmountCurrencyId , Value=sendEInvoiceDTO.TaxSubTotalTaxAmountValue},
                            Percent = new Uyumsoft.PercentType1() { Value = sendEInvoiceDTO.TaxSubTotalPercentValue},
                            TaxCategory = new Uyumsoft.TaxCategoryType() {
                                TaxScheme = new Uyumsoft.TaxSchemeType() {
                                    Name = new Uyumsoft.NameType1() { Value = sendEInvoiceDTO.TaxSubtotalTaxCategoryTaxSchemeName} ,
                                    TaxTypeCode = new Uyumsoft.TaxTypeCodeType() { Value = sendEInvoiceDTO.TaxSubtotalTaxCategoryTaxSchemeTaxTypeCode }
                                }
                            }
                        }
                    }
                },

            };
            */

            #endregion

            #region Tevkifat Bilgileri



            invoiceType.WithholdingTaxTotal = sendEInvoiceDTO.TaxTotalTypeWithHolding;
            //    new Uyumsoft.TaxTotalType[]
            //{
            //    new Uyumsoft.TaxTotalType
            //    {
            //        TaxAmount = new Uyumsoft.TaxAmountType { currencyID = sendEInvoiceDTO.WithHoldingTaxTotalTaxAmountCurrencyID, Value = sendEInvoiceDTO.WithHoldingTaxTotalTaxAmountCurrencyValue },
            //        TaxSubtotal = sendEInvoiceDTO.TaxSubtotalTypes
            //    }


            //};

            #endregion

            #region Faturanın genel ücret bilgileri
            invoiceType.LegalMonetaryTotal = new Uyumsoft.MonetaryTotalType()
            {
                LineExtensionAmount = new Uyumsoft.LineExtensionAmountType() { currencyID = sendEInvoiceDTO.LineExtensionAmountcurrencyID, Value = sendEInvoiceDTO.LineExtensionAmountValue }, //Toplam ana tutar
                TaxExclusiveAmount = new Uyumsoft.TaxExclusiveAmountType() { currencyID = sendEInvoiceDTO.TaxExclusiveAmountCurrencyId, Value = sendEInvoiceDTO.TaxExclusiveAmountValue }, //Vergisiz toplam
                TaxInclusiveAmount = new Uyumsoft.TaxInclusiveAmountType() { currencyID = sendEInvoiceDTO.TaxInclusiveAmountCurrencyId, Value = sendEInvoiceDTO.TaxInclusiveAmountValue }, //Vergili toplam tutar
                AllowanceTotalAmount = new Uyumsoft.AllowanceTotalAmountType() { currencyID = sendEInvoiceDTO.AllowanceTotalAmountCurrenyId },
                PayableAmount = new Uyumsoft.PayableAmountType() { currencyID = sendEInvoiceDTO.PayableAmountCurrencyId, Value = sendEInvoiceDTO.PayableAmountValue } //Ödenecek toplam tutar
            };
            #endregion

            #region Fatura Döviz Bilgileri

            if (sendEInvoiceDTO.PricingExchangeRateCalculationRate > 1)
            {
                invoiceType.PricingExchangeRate = new Uyumsoft.ExchangeRateType()
                {
                    SourceCurrencyCode = new Uyumsoft.SourceCurrencyCodeType() { Value = sendEInvoiceDTO.PricingExchangeRateSourceCurrencyCode },
                    TargetCurrencyCode = new Uyumsoft.TargetCurrencyCodeType() { Value = sendEInvoiceDTO.PricingExchangeRateTargetCurrencyCode },
                    CalculationRate = new Uyumsoft.CalculationRateType() { Value = (decimal)sendEInvoiceDTO.PricingExchangeRateCalculationRate },
                    Date = new Uyumsoft.DateType1() { Value = (DateTime)sendEInvoiceDTO.PricingExchangeRateDate }

                };
            }           

            #endregion

            #region Faturaya ait ürünlerin bilerileri (Array)

            Uyumsoft.InvoiceLineType[] invoiceLineTypes = new Uyumsoft.InvoiceLineType[sendEInvoiceDTO.InvoiceLine.Length];

            for (int i = 0; i < sendEInvoiceDTO.InvoiceLine.Length; i++)
            {
                Uyumsoft.InvoiceLineType invoiceLineType = new Uyumsoft.InvoiceLineType()
                {
                    ID = new Uyumsoft.IDType() { Value = (i + 1).ToString() },
                    Note = new Uyumsoft.NoteType[] { new Uyumsoft.NoteType() { Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineNote } },
                    InvoicedQuantity = new Uyumsoft.InvoicedQuantityType() { unitCode = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineInvoicedQuantityUnitCode, Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineInvoicedQuantityValue },
                    LineExtensionAmount = new Uyumsoft.LineExtensionAmountType() { currencyID = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineLineExtensionAmountCurrencyId, Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineLineExtensionAmountValue },
                    TaxTotal = new Uyumsoft.TaxTotalType()
                    {
                        TaxAmount = new Uyumsoft.TaxAmountType() { currencyID = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineTaxAmountCurrencyId, Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineTaxAmountValue },
                        TaxSubtotal = new Uyumsoft.TaxSubtotalType[]{
                new Uyumsoft.TaxSubtotalType(){
                    TaxableAmount = new Uyumsoft.TaxableAmountType() { currencyID = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineTaxSubTotalTaxableAmountCurrencyId,Value= sendEInvoiceDTO.InvoiceLine[i].InvoiceLineTaxSubTotalTaxableAmountValue },
                    TaxAmount = new Uyumsoft.TaxAmountType() { currencyID = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineTaxSubTotalTaxAmountCurrencyId , Value=sendEInvoiceDTO.InvoiceLine[i].InvoiceLineTaxSubTotalTaxAmountValue},
                    Percent = new Uyumsoft.PercentType1() { Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineTaxSubTotalPercent},
                    TaxCategory = new Uyumsoft.TaxCategoryType() {
                        TaxScheme = new Uyumsoft.TaxSchemeType() {
                            Name = new Uyumsoft.NameType1() { Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineTaxSubTotalTaxSchemeName} ,
                            TaxTypeCode = new Uyumsoft.TaxTypeCodeType() { Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineTaxSubTotalTaxSchemeTaxTypeCode }
                            }
                        }
                    }
                }
                    },
                    Item = new Uyumsoft.ItemType()
                    {
                        Description = new Uyumsoft.DescriptionType() { Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineItemDescriptionValue },
                        Name = new Uyumsoft.NameType1() { Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineItemNameValue },
                        ModelName = new Uyumsoft.ModelNameType() { Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLineItemModelNameValue }
                    },
                    Price = new Uyumsoft.PriceType()
                    {
                        PriceAmount = new Uyumsoft.PriceAmountType()
                        {
                            currencyID = sendEInvoiceDTO.InvoiceLine[i].InvoiceLinePricePriceAmountCurrencyId,
                            Value = sendEInvoiceDTO.InvoiceLine[i].InvoiceLinePricePriceAmountValue
                        }
                    }
                };

                invoiceLineTypes[i] = invoiceLineType;
            }


            invoiceType.InvoiceLine = invoiceLineTypes;


            #endregion

            #region Fatura ile ilgili diğer bilgiler
            Uyumsoft.InvoiceInfo infos = new Uyumsoft.InvoiceInfo();
            infos.Invoice = invoiceType;
            infos.EArchiveInvoiceInfo = new Uyumsoft.EArchiveInvoiceInformation() { DeliveryType = Uyumsoft.InvoiceDeliveryType.Electronic };
            infos.Scenario = Uyumsoft.InvoiceScenarioChoosen.Automated;
            infos.Notification = new Uyumsoft.NotificationInformation()
            {
                Mailing = new Uyumsoft.MailingInformation[] {
                    new Uyumsoft.MailingInformation(){
                        Subject = sendEInvoiceDTO.NotificationMailingSubject,
                        EnableNotification = sendEInvoiceDTO.NotificationMailingEnableNotification,
                        To = sendEInvoiceDTO.NotificationMailingTo,
                        Attachment = new Uyumsoft.MailAttachmentInformation() { Xml=sendEInvoiceDTO.NotificationMailingAttachmentXml, Pdf=sendEInvoiceDTO.NotificationMailingAttachmentPdf}
                    }
                }
            };
            infos.LocalDocumentId = sendEInvoiceDTO.LocalDocumentId;
            #endregion

            var request = new Uyumsoft.InvoiceInfo[] { infos };

            Uyumsoft.InvoiceIdentitiesResponse response = new Uyumsoft.InvoiceIdentitiesResponse();

            response = basicIntegrationClient.SendInvoice(userInformation, request);

            if (response.Message == null)
            {
                response.Message = "Başarıyla Gönderildi!";
            }
            return response.Message;
        }
    }
}
