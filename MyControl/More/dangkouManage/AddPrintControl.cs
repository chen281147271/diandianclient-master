using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.dangkouManage
{
    public partial class AddPrintControl : UserControl
    {
        public delegate void MyDelegate();
        public event MyDelegate MyEnent;
        Biz.BizPrinter bizPrinter = new Biz.BizPrinter();
        public AddPrintControl()
        {
            InitializeComponent();
            this.cbo_Paperwidth.Text = "58MM";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string ip = Txt_printIp.Text;
            string width = this.cbo_Paperwidth.Text;
            string botelv = this.Txt_botelv.Text;
            bizPrinter.AddPrinter(ip, 0,0);
            this.MyEnent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.MyEnent();
        }
    }
}
