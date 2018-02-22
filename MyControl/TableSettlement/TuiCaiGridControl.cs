using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data.Filtering;

namespace DianDianClient.MyControl.TableSettlement
{
    public partial class TuiCaiGridControl : UserControl
    {
        DataTable MenuTable;//菜单
        string TableNo;//桌号
        string EatNo;//用餐编号
        public delegate void CloseEvents(int iControl, string resoult);
        public event CloseEvents MyCloseEvents;
        public TuiCaiGridControl(string  TableNo, string EatNo, DataTable MenuTable)
        {
            this.MenuTable = MenuTable;
            this.TableNo = TableNo;
            this.EatNo = EatNo;
            InitializeComponent();
            IniData();
            SetupView();
        }
        private void IniData()
        {
            Bitmap bm = Properties.Resources._1;
            DataTable dt = new DataTable("Menudetail");
            dt.Columns.Add("FoodName", typeof(String));
            dt.Columns.Add("FoodImage", typeof(Bitmap));
            dt.Columns.Add("FoodCount", typeof(String));
            dt.Columns.Add("FoodID", typeof(Int32));
            //  int iID = 0;
            for (int i = 0; i < 20; i++)
            {
                string str = i + "号菜";
                string strprice = "X" + i + new Random().Next(1, 10);
                string strNumber = "菜名" + i * 2 + new Random().Next(1, 10);
                dt.Rows.Add(new object[] { strNumber, bm, strprice, i });
            }
            gridControl1.DataSource = dt.DefaultView;

            this.label1.Text = this.TableNo + "号桌";
            this.label2.Text = "用户编号:" + this.EatNo;
        }
        void SetupView()
        {
            try
            {
                // Setup tiles options
                tileView1.BeginUpdate();
                tileView1.OptionsTiles.RowCount = 3;
                tileView1.OptionsTiles.Padding = new Padding(20);
                tileView1.OptionsTiles.ItemPadding = new Padding(10);
                tileView1.OptionsTiles.IndentBetweenItems = 20;
                tileView1.OptionsTiles.ItemSize = new Size(340, 190);
                tileView1.Appearance.ItemNormal.ForeColor = Color.White;
                tileView1.Appearance.ItemNormal.BorderColor = Color.Transparent;
                //Setup tiles template
                TileViewItemElement leftPanel = new TileViewItemElement();
                TileViewItemElement splitLine = new TileViewItemElement();
                TileViewItemElement addressCaption = new TileViewItemElement();
                TileViewItemElement addressValue = new TileViewItemElement();
                TileViewItemElement yearBuiltCaption = new TileViewItemElement();
                TileViewItemElement yearBuiltValue = new TileViewItemElement();
                TileViewItemElement priceCaption = new TileViewItemElement();
                TileViewItemElement price = new TileViewItemElement();
                TileViewItemElement image = new TileViewItemElement();
                tileView1.TileTemplate.Add(leftPanel);
                tileView1.TileTemplate.Add(splitLine);
                tileView1.TileTemplate.Add(addressCaption);
                tileView1.TileTemplate.Add(addressValue);
                tileView1.TileTemplate.Add(yearBuiltCaption);
                tileView1.TileTemplate.Add(yearBuiltValue);
                tileView1.TileTemplate.Add(priceCaption);
                tileView1.TileTemplate.Add(price);
                tileView1.TileTemplate.Add(image);
                //
                leftPanel.StretchVertical = true;
                leftPanel.Width = 122;
                leftPanel.TextLocation = new Point(-10, 0);
                leftPanel.Appearance.Normal.BackColor = Color.FromArgb(58, 166, 101);
                //
                splitLine.StretchVertical = true;
                splitLine.Width = 3;
                splitLine.TextAlignment = TileItemContentAlignment.Manual;
                splitLine.TextLocation = new Point(110, 0);
                splitLine.Appearance.Normal.BackColor = Color.White;
                //
                addressCaption.Text = "FoodID";
                addressCaption.TextAlignment = TileItemContentAlignment.TopLeft;
                addressCaption.Appearance.Normal.FontSizeDelta = -1;
                //
                addressValue.Column = tileView1.Columns["FoodID"];
                addressValue.AnchorElement = addressCaption;
                addressValue.AnchorIndent = 2;
                addressValue.MaxWidth = 100;
                addressValue.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                //
                yearBuiltCaption.Text = "菜名";
                yearBuiltCaption.AnchorElement = addressValue;
                yearBuiltCaption.AnchorIndent = 14;
                yearBuiltCaption.Appearance.Normal.FontSizeDelta = -1;
                //
                yearBuiltValue.Column = tileView1.Columns["FoodName"];
                yearBuiltValue.AnchorElement = yearBuiltCaption;
                yearBuiltValue.AnchorIndent = 2;
                yearBuiltValue.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                yearBuiltValue.Appearance.Normal.Font = new Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //
                priceCaption.Text = "数量";
                priceCaption.AnchorElement = yearBuiltValue;
                priceCaption.AnchorIndent = 14;
                priceCaption.Appearance.Normal.FontSizeDelta = -1;
                //
                price.Column = tileView1.Columns["FoodCount"];
                price.TextAlignment = TileItemContentAlignment.BottomLeft;
                price.Appearance.Normal.Font = new Font("Segoe UI Semilight", 25.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //
                image.Column = tileView1.Columns["FoodImage"];
                image.ImageSize = new Size(280, 220);
                image.ImageAlignment = TileItemContentAlignment.MiddleRight;
                image.ImageScaleMode = TileItemImageScaleMode.ZoomOutside;
                image.ImageLocation = new Point(10, 10);
                //
                //tileView1.ColumnSet.GroupColumn = tileView1.Columns["FoodGroup"];
                tileView1.OptionsTiles.Orientation = Orientation.Horizontal;

                tileView1.ItemClick += tileView1_ItemClick;
            }
            finally
            {
                tileView1.EndUpdate();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MyCloseEvents(2, "结果");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MyCloseEvents(2, "结果");
        }
        void tileView1_ItemClick(object sender, TileViewItemClickEventArgs e)
        {
            string FoodID = tileView1.GetRowCellDisplayText(e.Item.RowHandle, "FoodID");
            string FoodCount = tileView1.GetRowCellDisplayText(e.Item.RowHandle, "FoodCount");
            // MessageBox.Show(String.Format("'{0}' item clicked", FoodID));
            MyForm.TableSettlement.TuiCai tuiCai = new MyForm.TableSettlement.TuiCai(FoodCount.Substring(1));
            tuiCai.StartPosition = FormStartPosition.CenterScreen;
            tuiCai.ShowDialog();
            //MessageBox.Show(tuiCai.TuiCaiControl1.TuiCaiDetaile1.Reason);
            //MessageBox.Show(tuiCai.TuiCaiControl1.TuiCaiDetaile1.Number);
            //MessageBox.Show(tuiCai.TuiCaiControl1.TuiCaiDetaile1.Solution);
        }
    }
}
