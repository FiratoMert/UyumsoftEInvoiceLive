using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UyumsoftEInvoiceLive
{
    public class EDespatchIncomingDTO
    {
        public DateTime ActualShipmentDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DespatchDate { get; set; }
        public string DespatchNo { get; set; }
        public string UUID { get; set; }
        public string Title { get; set; }
        public string VKN { get; set; }
    }
}
