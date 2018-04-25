using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.FoodManagement
{
    public partial class StandardsForm : DevExpress.XtraEditors.XtraForm
    {
        List<Models.item_standard> list;
        List<Temp> listtemp = new List<Temp>();
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public StandardsForm(List<Models.item_standard> list)
        {
            MyModels.selected_category_items.temp.itemKey = 0;
            this.list = list;
            InitializeComponent();
            iniData();
        }
        public class Temp
        {
            public string str { get; set; }
            public decimal sprice { get; set; }
            public string standardname { get; set; }
            public int itemKey { get; set; }
            public int standardkey { get; set; }
        }
        private void iniData()
        {
            foreach (var a in list)
            {
                string str = a.standardname + " ￥" + a.sprice;
                Temp temp = new Temp();
                temp.str = str;
                temp.itemKey = a.itemKey.Value;
                temp.sprice = a.sprice.Value;
                temp.standardkey = a.standardkey;
                temp.standardname = a.standardname;

                listtemp.Add(temp);
            }
            Temp temp1 = new Temp();
            temp1.str = "取消";
            listtemp.Add(temp1);
            this.gridControl1.DataSource = listtemp;
        }

        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {
            string str = this.tileView1.GetRowCellValue(this.tileView1.FocusedRowHandle, "str").ToString();
            int itemKey = Convert.ToInt32(this.tileView1.GetRowCellValue(this.tileView1.FocusedRowHandle, "itemKey"));
            decimal sprice = Convert.ToDecimal(this.tileView1.GetRowCellValue(this.tileView1.FocusedRowHandle, "sprice"));
            int standardkey = Convert.ToInt32(this.tileView1.GetRowCellValue(this.tileView1.FocusedRowHandle, "standardkey"));
            string standardname = Convert.ToString(this.tileView1.GetRowCellValue(this.tileView1.FocusedRowHandle, "standardname"));
            if (str != "取消")
            {
                MyModels.selected_category_items.temp.itemKey = itemKey;
                MyModels.selected_category_items.temp.sprice = sprice;
                MyModels.selected_category_items.temp.standardkey = standardkey;
                MyModels.selected_category_items.temp.standardname = standardname;
            }
            this.MyEvent();
            this.Close();
        }
    }
}
