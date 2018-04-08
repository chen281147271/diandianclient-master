using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;

namespace DianDianClient.MyControl.More.cantingSetUp
{
    public partial class EditPayWayControl : UserControl
    {
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        List<Models.dd_shop_payway> list;
        public EditPayWayControl()
        {
            InitializeComponent();
            RefreshList();
        }
        private void RefreshList()
        {
            list = BizSPInfo.QueryPayWay();
            this.tileView1.OptionsTiles.IndentBetweenItems = 100;
            Models.dd_shop_payway payway = new Models.dd_shop_payway();
            payway.id = -1;
            payway.payway = "添加收银方式";
            list.Add(payway);
            this.gridControl1.DataSource = list;
        }
        private void tableLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            this.tileView1.OptionsTiles.ItemSize = new System.Drawing.Size((this.tableLayoutPanel1.Width-250) / 2, 76);
        }
        private void tileView1_ContextButtonCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewContextButtonCustomizeEventArgs e)
        {
            string _payway = tileView1.GetRowCellValue(e.RowHandle, "payway").ToString();
            string _shopid = (tileView1.GetRowCellValue(e.RowHandle, "shopid")==null)?"":tileView1.GetRowCellValue(e.RowHandle, "shopid").ToString();
            if (e.Item.Name== "contextButton1"&& (_shopid == ""|| _payway == "添加收银方式"))
            {
                e.Item.Visibility = ContextItemVisibility.Hidden;
            }
        }

        private void tileView1_ContextButtonClick(object sender, ContextItemClickEventArgs e)
        {
            DevExpress.XtraGrid.Views.Tile.TileViewItem tileViewItem = (DevExpress.XtraGrid.Views.Tile.TileViewItem)e.DataItem;
            string payway = this.tileView1.GetRowCellValue(tileViewItem.RowHandle, this.tileView1.Columns["payway"]).ToString();
            int id = Convert.ToInt32(this.tileView1.GetRowCellValue(tileViewItem.RowHandle, this.tileView1.Columns["id"]));
            if (e.Item.Name== "contextButton1")
            {
                MyForm.More.cantingSetUp.EditPayWayNameForm editPayWayName = new MyForm.More.cantingSetUp.EditPayWayNameForm(id, payway, list,1);
                editPayWayName.StartPosition = FormStartPosition.CenterScreen;
                editPayWayName.ShowDialog();
                RefreshList();
            }
        }

        private void tileView1_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            string payway = tileView1.GetRowCellValue(e.RowHandle, "payway").ToString();
            if (payway== "添加收银方式")
            {
                e.Item.AppearanceItem.Normal.BackColor = Color.Red;
                e.Item.AppearanceItem.Normal.ForeColor = Color.White;
            }
        }

        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            if (e.Item["payway"].ToString() == "添加收银方式")
            {
                MyForm.More.cantingSetUp.EditPayWayNameForm editPayWayName = new MyForm.More.cantingSetUp.EditPayWayNameForm(-1, "", list, 2);
                editPayWayName.StartPosition = FormStartPosition.CenterScreen;
                editPayWayName.ShowDialog();
                RefreshList();
            }
        }
    }
}
