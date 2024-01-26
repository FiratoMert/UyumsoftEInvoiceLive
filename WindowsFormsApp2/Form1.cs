using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UyumsoftEInvoiceLive;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        UyumsoftEInvoiceLive.Client client = new UyumsoftEInvoiceLive.Client();

        private void Form1_Load(object sender, EventArgs e)
        {
            //DateTime EndDate = DateTime.Now;
            //DateTime StartDate = EndDate.AddDays(-25);


            //List<EInvoiceIncomingDTO> eInvoiceIncomingDTOs = client.GetInboxInvoiceList("Bomaksan_WebServis", "C8AyWmHA", StartDate, EndDate);

            //gridControl1.DataSource = eInvoiceIncomingDTOs;
            //gridView1.BestFitColumns();

            var result = client.GetInvoiceInfo("Bomaksan_WebServis", "C8AyWmHA", "e6e4a706-cc0d-406a-acc4-97bb165cd36a");
        }
    }
}
