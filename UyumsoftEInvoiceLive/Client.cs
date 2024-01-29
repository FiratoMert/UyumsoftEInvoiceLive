using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<EInvoiceIncomingDTO> GetInboxInvoiceList(string username, string password, DateTime startDate, DateTime endDate, int? pageSize = int.MaxValue)
        {
            try
            {
                userInformation.Username = username;
                userInformation.Password = password;

                inboxInvoiceListQueryModel.PageSize = (int)pageSize;
                inboxInvoiceListQueryModel.CreateEndDate = endDate;
                inboxInvoiceListQueryModel.CreateStartDate = startDate;

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
                        eInvoiceIncomingDTO.TaxInclusiveValue = item.PayableAmount.ToString("N2");
                        eInvoiceIncomingDTO.TaxExlusiveValue = item.TaxExclusiveAmount.ToString("N2");
                        eInvoiceIncomingDTO.Tax = item.TaxTotal.ToString();

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
    }
}
