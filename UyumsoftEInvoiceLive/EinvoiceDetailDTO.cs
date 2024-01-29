using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UyumsoftEInvoiceLive
{
    public class EinvoiceDetailDTO
    {
        public string SiraNo { get; set; }
        public string MalHizmet { get; set; }
        public string Miktar {  get; set; }
        public string BirimFiyat { get; set; }
        public string KdvOrani { get; set; }
        public string KdvTutari { get; set; }
        public string DigerVergiler { get; set; }
        public string MalHizmetTutari { get; set; }
        public string MalHizmetKod {  get; set; }
    }
}
