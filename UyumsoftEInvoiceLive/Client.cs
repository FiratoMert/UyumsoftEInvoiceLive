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

                foreach (var item in inboxInvoiceListResponse.Value.Items)
                {
                    EInvoiceIncomingDTO eInvoiceIncomingDTO = new EInvoiceIncomingDTO();

                    eInvoiceIncomingDTO.InvoiceNo = item.InvoiceId.ToString();
                    eInvoiceIncomingDTO.CreateDate = item.CreateDateUtc.AddHours(3).ToString();
                    eInvoiceIncomingDTO.Title = item.TargetTitle.ToString();
                    eInvoiceIncomingDTO.VKN = item.TargetTcknVkn.ToString();
                    eInvoiceIncomingDTO.InvoiceDate = item.ExecutionDate.ToString();
                    eInvoiceIncomingDTO.UUID = item.DocumentId.ToString();
                    eInvoiceIncomingDTO.ProfileId = item.Type.ToString();
                    eInvoiceIncomingDTO.InvoiceType = item.InvoiceTipType.ToString();
                    eInvoiceIncomingDTO.Currency = item.DocumentCurrencyCode.ToString();
                    eInvoiceIncomingDTO.TaxInclusiveValue = item.PayableAmount.ToString();
                    eInvoiceIncomingDTO.TaxExlusiveValue = item.TaxExclusiveAmount.ToString();
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

        public InvoiceInfo GetInvoiceInfo(string username, string password , string UUID)
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
    }
}
