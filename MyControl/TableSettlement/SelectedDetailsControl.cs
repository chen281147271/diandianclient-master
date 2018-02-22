using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DianDianClient.MyControl.TableSettlement
{
    public partial class SelectedDetails : UserControl
    {
        public DataTable Grid2DataTable;
        public SelectedDetails()
        {
            InitializeComponent();
            inigrid();
        }
        private void inigrid()
        {
            DataTable dt = new DataTable("Menudetail");
            dt.Columns.Add("Number", typeof(String));
            dt.Columns.Add("FoodName", typeof(String));
            dt.Columns.Add("Price", typeof(String));
            dt.Columns.Add("State", typeof(Int32));
            //  int iID = 0;
            for (int i = 0; i < 2; i++)
            {
                string str = i + "号菜";
                string strprice = "¥" + i + new Random().Next(1, 10);
                string strNumber = "X" + new Random().Next(1,12)+i;
                int State = new Random().Next(0, 1);
                dt.Rows.Add(new object[] { strNumber, str, strprice, State });
            }

            this.gridControl1.DataSource = dt.DefaultView;
            // tileView1.Appearance.ItemNormal.BorderColor = Color.Transparent;
            // this..Appearance.Normal.BackColor = Color.FromArgb(58, 166, 101);
              this.tileView1.Appearance.ItemNormal.BackColor = Color.FromArgb(20,20, 166, 101);
            // this.tileView1.Appearance.ItemNormal.BackColor = Color.Transparent;
            // this.gridControl1.BackColor = Color.Red;
            // this.gridControl1.BackColor= System.Drawing.Color.Red;
            this.tileView1.ViewCaption = "用餐编号：2 已接单 ";


            //
            Grid2DataTable = new DataTable();
            Grid2DataTable.Columns.Add("ItemName", typeof(String));
            Grid2DataTable.Columns.Add("ItemValue", typeof(String));
            Grid2DataTable.Rows.Add(new object[] { "订单金额", "20"});
            Grid2DataTable.Rows.Add(new object[] { "需付金额", "20" });
            this.gridControl2.DataSource = Grid2DataTable.DefaultView;
            tileView2.Appearance.ItemNormal.BorderColor = Color.Transparent;
            gridControl2.Size = new Size(200, 72);






        }

        private void tileView1_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            //取值Demo
            string strNumber ="";
            string strFoodName = "";
            decimal price = 0;
            DevExpress.XtraGrid.Views.Tile.TileViewItem tileViewItem = (DevExpress.XtraGrid.Views.Tile.TileViewItem)e.DataItem;
            strNumber = tileView1.GetRowCellDisplayText(tileViewItem.RowHandle, Number).ToString();
            strFoodName = tileView1.GetRowCellDisplayText(tileViewItem.RowHandle, FoodName).ToString();
            price = Convert.ToDecimal(tileView1.GetRowCellDisplayText(tileViewItem.RowHandle, Price).ToString().Substring(1));
            if (e.Item.Name == "contextButton3")
            {
                if ((bool)tileView1.ContextButtons["contextButton3"].Tag)
                {
                    tileView1.ContextButtons["contextButton3"].ImageOptions.Image = Properties.Resources.finish;
                    tileView1.ContextButtons["contextButton3"].Tag = false;
                }
                else
                {
                    tileView1.ContextButtons["contextButton3"].ImageOptions.Image = Properties.Resources.wait;
                    tileView1.ContextButtons["contextButton3"].Tag = true;
                }
            }
            else if (e.Item.Name == "contextButton1")
            {
                //参数需要增加
                MyForm.TableSettlement.TuiCai tuiCai = new MyForm.TableSettlement.TuiCai(strNumber.Substring(1));
                tuiCai.StartPosition = FormStartPosition.CenterScreen;
                tuiCai.ShowDialog();
                //MessageBox.Show(tuiCai.TuiCaiControl1.TuiCaiDetaile1.Reason);
                //MessageBox.Show(tuiCai.TuiCaiControl1.TuiCaiDetaile1.Number);
                //MessageBox.Show(tuiCai.TuiCaiControl1.TuiCaiDetaile1.Solution);
            }
            else if (e.Item.Name == "contextButton2")
            {
                DianDianClient.MyForm.TableSettlement.SaleForm saleForm = new DianDianClient.MyForm.TableSettlement.SaleForm(price, strFoodName);
                saleForm.StartPosition = FormStartPosition.CenterScreen;
                saleForm.ShowDialog();
                MessageBox.Show(saleForm.saleControlcs1.Price.ToString());
            }
        }

        private void tileView1_ContextButtonCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewContextButtonCustomizeEventArgs e)
        {
          //  tileView1.ContextButtons["contextButton3"].ImageOptions.Image.Tag = true;
           // this.conte
        }

        private void tileView2_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
        {
            string[] Key = new string[2];
            Key[0] = "订单金额";
            Key[1] = "需付金额";
            string str = tileView2.GetRowCellDisplayText(e.RowHandle, "ItemName");
            if (Array.IndexOf(Key, str.Trim()) >=0)
            {
                e.Item.AppearanceItem.Normal.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
          
        }
    }
}
