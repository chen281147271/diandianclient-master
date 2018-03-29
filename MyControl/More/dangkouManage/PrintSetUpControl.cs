using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using DevExpress.Utils;

namespace DianDianClient.MyControl.More.dangkouManage
{
    public partial class PrintSetUpControl : UserControl
    {
        Biz.BizPrinter BizPrinter = new Biz.BizPrinter();
        List<MyClass> list_LocalPrint;
        List<Models.dd_printers> list_ddprinters;
        public PrintSetUpControl()
        {
            InitializeComponent();
            GetPrintList();
        }
        private void GetPrintList()
        {
            list_LocalPrint = new List<MyClass>();
            list_LocalPrint.Clear();
            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                //MyClass myClass = new MyClass();
                //myClass.cishu = "每次打印份数：1份";
                //myClass.Paperwidth = "纸张宽度：58";
                //myClass.sPrint = sPrint;
                //myClass.isdefault = 0;
                //myClass.status = 0;
                //myClass.id = -1;
                //list_LocalPrint.Add(myClass);
                BizPrinter.AddPrinter(sPrint);
            }
            list_ddprinters=BizPrinter.QueryPrinters(null, "");
            foreach(var a in list_ddprinters)
            {
                MyClass myClass = new MyClass();
                myClass.cishu = "0";//不知道是哪个 后期改下
                myClass.Paperwidth = a.psize.ToString();
                myClass.sPrint = a.printername;
                myClass.isdefault = Convert.ToInt32(a.isdefault);
                myClass.status = Convert.ToInt32(a.status);
                myClass.id = a.id;
                list_LocalPrint.Add(myClass);
            }
            this.gridControl1.DataSource = list_LocalPrint;
        }
        public class MyClass
        {
            public string sPrint { get; set; }
            public string Paperwidth { get; set; }
            public string cishu { get; set; }
            public int isdefault { get; set; }
            public int status { get; set; }
            public int id { get; set; }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            MyForm.More.dangkouManage.AddPrintForm addPrint = new MyForm.More.dangkouManage.AddPrintForm();
            addPrint.StartPosition = FormStartPosition.CenterScreen;
            addPrint.ShowDialog();
            GetPrintList();
        }

        private void tileView1_ContextButtonCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewContextButtonCustomizeEventArgs e)
        {
            int status = Convert.ToInt32(this.tileView1.GetRowCellValue(e.RowHandle, "status"));
            int isdefault = Convert.ToInt32(this.tileView1.GetRowCellValue(e.RowHandle, "isdefault"));
            if (e.Item.Name == "contextButton1")
            {
                if (status==1)
                {
                    e.Item.ImageOptions.Image = global::DianDianClient.Properties.Resources.stopshiyong;
                    e.Item.Tag = "stop";
                }
                else
                {
                    e.Item.ImageOptions.Image = global::DianDianClient.Properties.Resources.kaishishiyong;
                    e.Item.Tag = "star";
                }
            }
            if (e.Item.Name == "contextButton3")
            {
                if (isdefault == 0 && status == 1)
                {
                    
                    e.Item.ImageOptions.Image = global::DianDianClient.Properties.Resources.isdefault;
                }
                else
                {
                    e.Item.ImageOptions.Image = null;
                }
            }
        }

        private void tileView1_ContextButtonClick(object sender, ContextItemClickEventArgs e)
        {
            DevExpress.XtraGrid.Views.Tile.TileViewItem tileViewItem = (DevExpress.XtraGrid.Views.Tile.TileViewItem)e.DataItem;
            string id = this.tileView1.GetRowCellValue(tileViewItem.RowHandle, this.tileView1.Columns["sPrint"]).ToString();
            if (e.Item.Name == "contextButton1")
            {
                if(e.Item.Tag.ToString() == "stop")
                {
                    BizPrinter.AddPrinter(id, 0, 0);
                }
                else
                {
                    BizPrinter.AddPrinter(id, 1, 0);
                }
            }else if(e.Item.Name == "contextButton3")
            {
                BizPrinter.AddPrinter(id, 1, 1);
                //  e.Item.Visibility = ContextItemVisibility.Hidden;
                //e.Item.ImageOptions.Image = null;
            }
            GetPrintList();
        }
    }
}
