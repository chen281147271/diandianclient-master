using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace DianDianClient.MyControl.More.dangkouManage
{
    public partial class EditdangkouControl : UserControl
    {
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        Models.dd_shop_windows ShopWindows;
        Biz.BizPrinter BizPrinter = new Biz.BizPrinter();
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        int id;
        public EditdangkouControl(Models.dd_shop_windows ShopWindows,int OpType)
        {
            InitializeComponent();
            this.ShopWindows = ShopWindows;
            inicbo();
            if (OpType == 1)
            {
                iniData();
            }
            else
            {
                this.id = -1;
            }
        }
        private void iniData()
        {
            this.txt_windowdesc.Text = this.ShopWindows.windowdesc.ToString();
            this.Txt_windowname.Text = this.ShopWindows.windowname.ToString();
            this.cbo_printname.Text = this.ShopWindows.printname.ToString();
            this.cbo_printnum.Text = this.ShopWindows.printnum.ToString();
            this.rbtn_isdefault.EditValue = this.ShopWindows.isdefault;
            this.rbtn_isprintexcep.EditValue = this.ShopWindows.isprintexcep;
            this.rbtn_isyicaiyidan.EditValue = this.ShopWindows.isyicaiyidan;
            this.rbtn_status.EditValue = this.ShopWindows.status;
            this.id = this.ShopWindows.windowid;
        }
        private void inicbo()
        {
            cbo_printname.Properties.Items.Clear();
            cbo_printname.Properties.Items.Add("请选择打印机");
            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                cbo_printname.Properties.Items.Add(sPrint);
            }
            var a=BizPrinter.QueryPrinters(null, "").Where(o=>o.status==1);
            foreach(var b in a)
            {
                cbo_printname.Properties.Items.Add(b.printername);
            }
          
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            string name = Txt_windowname.Text;
            string desc = txt_windowdesc.Text;
            string printname = cbo_printname.Text;
            int  printnum = Convert.ToInt32(cbo_printnum.Text);
            bool isdefault = (rbtn_isdefault.SelectedIndex == 0) ? false : true;
            int status = rbtn_status.SelectedIndex;
            bool isprintexcep = (rbtn_isprintexcep.SelectedIndex == 0) ? false : true;
            bool isyicaiyidan = (rbtn_isyicaiyidan.SelectedIndex == 0) ? false : true;
            BizSPInfo.UpdateStall(this.id, name, desc, printname, printnum, isdefault, status, isprintexcep, isyicaiyidan);
            this.MyEvent();
        }
    }
}
