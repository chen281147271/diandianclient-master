using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
namespace DianDianClient.MyForm.FoodManagement
{
    public partial class TaoCanFoodForm : Form
    {
        Biz.BIZFoodController bIZFoodController = new Biz.BIZFoodController();
        Biz.BIZFoodController BIZFood = new Biz.BIZFoodController();
        List<Models.v_category_items> list;
        List<MyModels.selected_category_items._selected_category_items> _list=new List<MyModels.selected_category_items._selected_category_items>();
        //List<Models.item_category> list_category;
        decimal Sum_price;
        // DataTable right_dt;
        List<_item_category> list_category=new List<_item_category>();
        public TaoCanFoodForm()
        {
            //deepclone
            foreach(var a in MyModels.selected_category_items.list)
            {
                MyModels.selected_category_items._selected_category_items _Selected_Category_Items = new MyModels.selected_category_items._selected_category_items();
                _Selected_Category_Items.itemcategorykey = a.itemcategorykey;
                _Selected_Category_Items.itemImgs = a.itemImgs;
                _Selected_Category_Items.itemKey = a.itemKey;
                _Selected_Category_Items.itemName = a.itemName;
                _Selected_Category_Items.num = a.num;
                _Selected_Category_Items.sprice= a.sprice;
                _Selected_Category_Items.standardkey = a.standardkey;
                _Selected_Category_Items.standardname = a.standardname;
                _Selected_Category_Items.bitmap = Utils.utils.GetBitmap(a.itemImgs);
                this._list.Add(_Selected_Category_Items);
            }
            InitializeComponent();
            InitData();
            SetupView();
            SetupView3();
        }
        private class _item_category
        {
           public int num { get; set; }
           public int itemcategorykey { get; set; }
           public string name { get; set; }
        }
        #region UI
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
                tileView1.OptionsTiles.ItemSize = new Size(340, 195);
                tileView1.Appearance.ItemNormal.ForeColor = Color.White;
                tileView1.Appearance.ItemNormal.BorderColor = Color.Transparent;
                //Setup tiles template
                TileViewItemElement leftPanel = new TileViewItemElement();
                TileViewItemElement splitLine = new TileViewItemElement();
                TileViewItemElement addressCaption = new TileViewItemElement();
                TileViewItemElement addressValue = new TileViewItemElement();
                TileViewItemElement yearBuiltCaption = new TileViewItemElement();
                TileViewItemElement yearBuiltValue = new TileViewItemElement();
                TileViewItemElement price = new TileViewItemElement();
                TileViewItemElement image = new TileViewItemElement();
                tileView1.TileTemplate.Add(leftPanel);
                tileView1.TileTemplate.Add(splitLine);
                tileView1.TileTemplate.Add(addressCaption);
                tileView1.TileTemplate.Add(addressValue);
                tileView1.TileTemplate.Add(yearBuiltCaption);
                tileView1.TileTemplate.Add(yearBuiltValue);
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
                yearBuiltCaption.TextAlignment = TileItemContentAlignment.TopLeft;
                yearBuiltCaption.Text = "菜名";
                //yearBuiltCaption.AnchorElement = addressValue;
                //yearBuiltCaption.AnchorIndent = 14;
                yearBuiltCaption.Appearance.Normal.FontSizeDelta = -1;
                //
                yearBuiltValue.Column = tileView1.Columns["FoodName"];
                yearBuiltValue.AnchorElement = yearBuiltCaption;
                yearBuiltValue.AnchorIndent = 2;
                yearBuiltValue.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                yearBuiltValue.Appearance.Normal.Font = new Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                price.Column = tileView1.Columns["FoodPrice"];
                price.AnchorElement = yearBuiltValue;
                yearBuiltValue.AnchorIndent = 2;
                // price.TextAlignment = TileItemContentAlignment.BottomLeft;
                price.Appearance.Normal.Font = new Font("Segoe UI Semilight", 25.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //
                addressCaption.Text = "是否分规格";
                //addressCaption.TextAlignment = TileItemContentAlignment.TopLeft;
                addressCaption.AnchorElement = price;
               // addressCaption.AnchorIndent = 10;
                addressCaption.Appearance.Normal.FontSizeDelta = -1;
                //
                addressValue.Column = tileView1.Columns["isStandard"];
                addressValue.AnchorElement = addressCaption;
                addressValue.AnchorIndent = 2;
                addressValue.MaxWidth = 100;
                addressValue.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                // addressValue.TextAlignment = TileItemContentAlignment.BottomLeft;
                addressValue.Appearance.Normal.Font = new Font("Segoe UI Semilight", 20.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //
                image.Column = tileView1.Columns["FoodImage"];
                image.ImageSize = new Size(280, 215);
                image.ImageAlignment = TileItemContentAlignment.MiddleRight;
                image.ImageScaleMode = TileItemImageScaleMode.ZoomOutside;
                image.ImageLocation = new Point(10, 10);
                //
                tileView1.ColumnSet.GroupColumn = tileView1.Columns["FoodGroupName"];
                tileView1.OptionsTiles.Orientation = Orientation.Vertical;
                //
                //DevExpress.Utils.ContextButton contextButton1 = new DevExpress.Utils.ContextButton();
                //DevExpress.Utils.ContextButton contextButton2 = new DevExpress.Utils.ContextButton();
                //DevExpress.Utils.ContextButton contextButton3 = new DevExpress.Utils.ContextButton();
                //this.tileView1.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
                //this.tileView1.ContextButtonOptions.BottomPanelPadding = new System.Windows.Forms.Padding(10);
                ////
                ////    contextButton1.Caption = "下架";
                //contextButton1.ImageOptions.Image = global::DianDianClient.Properties.Resources.offshelf;
                //contextButton2.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
                //contextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
                //contextButton1.Id = new System.Guid("5679cac7-1f0e-4f93-a9d4-cd3f82547937");
                //contextButton1.Name = "contextButton1";
                ////    contextButton2.Caption = "contextButton2";
                ////
                //contextButton2.ImageOptions.Image = global::DianDianClient.Properties.Resources.delete;
                //contextButton3.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far;
                //contextButton2.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
                //contextButton2.Id = new System.Guid("9a35eabb-9479-4144-a912-725a1da88885");
                //contextButton2.Name = "contextButton2";
                ////
                ////   contextButton3.Caption = "contextButton3";
                //contextButton3.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far;
                //contextButton3.ImageOptions.Image = global::DianDianClient.Properties.Resources.edit;
                //contextButton3.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
                //contextButton3.Id = new System.Guid("d54ff57a-998d-4251-b811-3b17e36c75aa");
                //contextButton3.Name = "contextButton3";
                //this.tileView1.ContextButtons.Add(contextButton1);
                //this.tileView1.ContextButtons.Add(contextButton2);
                //this.tileView1.ContextButtons.Add(contextButton3);
                this.tileView1.GridControl = this.gridControl1;
                this.tileView1.Name = "tileView1";
            }
            finally
            {
                tileView1.EndUpdate();
            }
        }
        void SetupView3()
        {
            try
            {
                // Setup tiles options
                tileView3.BeginUpdate();
                tileView3.OptionsTiles.RowCount = 3;
                tileView3.OptionsTiles.Padding = new Padding(20);
                tileView3.OptionsTiles.ItemPadding = new Padding(10);
                tileView3.OptionsTiles.IndentBetweenItems = 20;
                tileView3.OptionsTiles.ItemSize = new Size(340, 160);
                tileView3.Appearance.ItemNormal.ForeColor = Color.White;
                tileView3.Appearance.ItemNormal.BorderColor = Color.Transparent;
                //Setup tiles template
                TileViewItemElement leftPanel = new TileViewItemElement();
                TileViewItemElement splitLine = new TileViewItemElement();
                TileViewItemElement addressCaption = new TileViewItemElement();
                TileViewItemElement addressValue = new TileViewItemElement();
                TileViewItemElement yearBuiltCaption = new TileViewItemElement();
                TileViewItemElement yearBuiltValue = new TileViewItemElement();
                TileViewItemElement price = new TileViewItemElement();
                TileViewItemElement image = new TileViewItemElement();
                TileViewItemElement num = new TileViewItemElement();
                tileView3.TileTemplate.Add(leftPanel);
                tileView3.TileTemplate.Add(splitLine);
                tileView3.TileTemplate.Add(addressCaption);
                tileView3.TileTemplate.Add(addressValue);
                tileView3.TileTemplate.Add(yearBuiltCaption);
                tileView3.TileTemplate.Add(yearBuiltValue);
                tileView3.TileTemplate.Add(price);
                tileView3.TileTemplate.Add(image);
                tileView3.TileTemplate.Add(num);
                //
                leftPanel.StretchVertical = true;
                leftPanel.Width = 118;
                leftPanel.TextLocation = new Point(-10, 0);
                leftPanel.Appearance.Normal.BackColor = Color.FromArgb(58, 166, 101);
                //
                splitLine.StretchVertical = true;
                splitLine.Width = 3;
                splitLine.TextAlignment = TileItemContentAlignment.Manual;
                splitLine.TextLocation = new Point(105, 0);
                splitLine.Appearance.Normal.BackColor = Color.White;
                //
                addressCaption.Text = "itemKey";
                addressCaption.TextAlignment = TileItemContentAlignment.TopLeft;
                addressCaption.Appearance.Normal.FontSizeDelta = -1;
                addressCaption.TextVisible = false;
                //
                addressValue.Column = tileView3.Columns["itemKey"];
                addressValue.AnchorElement = addressCaption;
                addressValue.AnchorIndent = 2;
                addressValue.MaxWidth = 100;
                addressValue.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                addressValue.TextVisible = false;
                //
                yearBuiltCaption.TextAlignment = TileItemContentAlignment.TopLeft;
                yearBuiltCaption.Text = "菜名";
               // yearBuiltCaption.AnchorElement = addressValue;
               // yearBuiltCaption.AnchorIndent = 14;
                yearBuiltCaption.Appearance.Normal.FontSizeDelta = -1;
                //
                yearBuiltValue.Column = tileView3.Columns["itemName"];
                yearBuiltValue.AnchorElement = yearBuiltCaption;
                yearBuiltValue.AnchorIndent = 2;
                yearBuiltValue.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                yearBuiltValue.Appearance.Normal.Font = new Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                price.Column = tileView3.Columns["sprice"];
                // price.TextAlignment = TileItemContentAlignment.BottomLeft;
                price.AnchorElement = yearBuiltValue;
                price.AnchorIndent = 2;
                price.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
                price.Appearance.Normal.Font = new Font("Segoe UI Semilight", 25.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //
                image.Column = tileView3.Columns["itemImgs"];
                image.ImageSize = new Size(280, 180);
                image.ImageAlignment = TileItemContentAlignment.MiddleRight;
                image.ImageScaleMode = TileItemImageScaleMode.ZoomOutside;
                image.ImageLocation = new Point(10, 10);
                //
                num.Column= tileView3.Columns["num"];
                num.TextAlignment = TileItemContentAlignment.BottomLeft;
                num.TextLocation = new Point(35, 5);


                num.Appearance.Normal.Font = new Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //
                //tileView3.ColumnSet.GroupColumn = tileView3.Columns["FoodGroupName"];
                tileView3.OptionsTiles.Orientation = Orientation.Vertical;
                ////
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaoCanFoodForm));
                DevExpress.Utils.ContextButton contextButton1 = new DevExpress.Utils.ContextButton();
                DevExpress.Utils.ContextButton contextButton2 = new DevExpress.Utils.ContextButton();
                DevExpress.Utils.ContextButton contextButton3 = new DevExpress.Utils.ContextButton();
               // this.tileView3.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
              //  this.tileView3.ContextButtonOptions.BottomPanelPadding = new System.Windows.Forms.Padding(10);
                this.tileView3.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.Transparent;
                contextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
                contextButton1.Id = new System.Guid("268d6cbb-1a66-45df-8bcf-b78da8e5f6b6");
                contextButton1.ImageOptions.Image = global::DianDianClient.Properties.Resources.remove_32x321;
                contextButton1.Name = "contextButton1";
                contextButton1.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
                contextButton2.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
                contextButton2.AppearanceNormal.Font = new Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                contextButton2.AppearanceNormal.ForeColor = System.Drawing.Color.White;
                contextButton2.AppearanceNormal.Options.UseFont = true;
                contextButton2.AppearanceNormal.Options.UseForeColor = true;
                contextButton2.AppearanceHover.Font = new Font("Segoe UI Semilight", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                contextButton2.AppearanceHover.ForeColor =  System.Drawing.Color.White;
                contextButton2.AppearanceHover.Options.UseFont = true;
                contextButton2.AppearanceHover.Options.UseForeColor = true;
                contextButton2.Caption = "      ";
                contextButton2.Id = new System.Guid("4e4e01ab-05d0-4e61-a986-799123a1585b");
                contextButton2.Name = "contextButton2";
                contextButton2.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
                contextButton3.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
                contextButton3.Id = new System.Guid("c1138ada-4615-4742-ac92-5d01e0ab43dc");
                contextButton3.ImageOptions.Image = global::DianDianClient.Properties.Resources.add_32x32;
                contextButton3.Name = "contextButton3";
                contextButton3.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;


                this.tileView3.ContextButtons.Add(contextButton1);
                this.tileView3.ContextButtons.Add(contextButton2);
                this.tileView3.ContextButtons.Add(contextButton3);
                this.tileView3.GridControl = this.gridControl3;
                this.tileView3.Name = "tileView3";
            }
            finally
            {
                tileView3.EndUpdate();
            }
        }
        #endregion

        #region InitData
        protected virtual void InitData()
        {
            try
            {
                RefreshList();
                //Right_RefreshList();
            }
            catch { }
        }
        //private void Right_RefreshList()
        //{
        //    Bitmap bm = Properties.Resources._1;
        //   // right_dt.Clear();
        //    foreach (var a in MyModels.selected_category_items.list)
        //    {
        //        right_dt.Rows.Add(new object[] { a.itemName, bm, a.sprice, a.itemKey, a.num });
        //    }
        //    gridControl3.DataSource = right_dt;
        //    //tileView3.RefreshData();
        //}
        private void RefreshList()
        {
           // MyModels.selected_category_items
            list = bIZFoodController.GetFoodList(0, 0);
            var vlist  = list.OrderBy(o => o.itemcategorykey);
            var list_itemcategory = bIZFoodController.GetFoodFL().OrderBy(o => o.itemcategorykey);



            // Demo 数据 字段名请不要改变
            DataTable dt = new DataTable();
            dt.Columns.Add("FoodName", typeof(String));
            dt.Columns.Add("FoodImage", typeof(Bitmap));
            dt.Columns.Add("FoodPrice", typeof(String));
            dt.Columns.Add("FoodGroupName", typeof(String));
            dt.Columns.Add("FoodID", typeof(Int32));
            dt.Columns.Add("FoodGroupID", typeof(Int32));
            dt.Columns.Add("isStandard", typeof(String));

            //DataTable _dt = new DataTable();
            //_dt.Columns.Add("FoodGroupName", typeof(String));
            //_dt.Columns.Add("FoodGroupID", typeof(Int32));
            //_dt.Columns.Add("num", typeof(Int32));

            //right_dt = new DataTable();
            //right_dt.Columns.Add("FoodName", typeof(String));
            //right_dt.Columns.Add("FoodImage", typeof(Bitmap));
            //right_dt.Columns.Add("FoodPrice", typeof(String));
            //right_dt.Columns.Add("FoodID", typeof(Int32));
            //right_dt.Columns.Add("num", typeof(String));

            foreach (var a in vlist)
            {
                string temp = a.itemName;
                if (temp == null)
                    temp = "";
                try
                {
                    if (System.Text.Encoding.Default.GetBytes(temp).Length > 10)
                    {
                        a.itemName = temp.Substring(0, 5) + "\r\n" + temp.Substring(5);
                    }
                }
                catch
                {
                    temp = "";
                }
                dt.Rows.Add(new object[] { a.itemName, Utils.utils.GetBitmap(a.itemImgs), a.price, a.categoryName, a.itemkey, a.itemcategorykey, (a.isStandard==1)?"是":"否" });
            }

            // _dt.Rows.Add("全部", 0);
            list_category.Clear();
            _item_category Item = new _item_category();
            Item.itemcategorykey =0;
            Item.name = "全部";
            Item.num = 0;
            list_category.Add(Item);
            foreach (var a in list_itemcategory)
            {
                //_dt.Rows.Add(new object[] { a.name, a.itemcategorykey });
                _item_category _Item = new _item_category();
                _Item.itemcategorykey = a.itemcategorykey;
                _Item.name = a.name;
                _Item.num = 0;
                list_category.Add(_Item);
            }

            gridControl1.DataSource = dt;

            //foreach (var a in _list)
            //{
            //    right_dt.Rows.Add(new object[] { a.itemName, bm, a.sprice, a.itemKey, a.num });
            //}
            gridControl3.DataSource = _list;
            sumPrice();
            //var q = from p in dt.AsEnumerable()
            //        group p by  new {t1= p.Field<int>("FoodGroupID"), t2 = p.Field<string>("FoodGroupName") } into g
            //        select new { FoodGroupID =g.Key.t1, FoodGroupName = g.Key.t2 };
            gridControl2.DataSource = list_category;
        }
        #endregion

        #region event function 需要改的都在这里
        /// <summary>
        /// 左侧 item click event
        /// </summary>
        private void tileView2_ItemClick(object sender, TileViewItemClickEventArgs e)
        {
            string FoodGroupID = tileView2.GetRowCellDisplayText(e.Item.RowHandle, "FoodGroupID");
            if (FoodGroupID == "0")
            {
                this.tileView1.ClearColumnsFilter();
            }
            else
                this.tileView1.ActiveFilterCriteria = new BinaryOperator("FoodGroupID", FoodGroupID, BinaryOperatorType.Equal);
        }
        private void tileView3_ContextButtonClick(object sender, ContextItemClickEventArgs e)
        {
            TileViewItem tileViewItem = (e.DataItem) as TileViewItem;
            int num = Convert.ToInt32(tileViewItem["num"].Text);
            int itemKey = Convert.ToInt32(tileViewItem["itemKey"].Text);
            if(e.Item.Name== "contextButton1")
            {
                _list.Find(o => o.itemKey == itemKey).num = --num;

                if (num == 0)
                {
                    _list.Remove(_list.Where(o => o.itemKey == itemKey).FirstOrDefault());
                }
            }else if (e.Item.Name == "contextButton3")
            {
                _list.Find(o => o.itemKey == itemKey).num = ++num;
            }
            sumPrice();
            this.tileView3.RefreshData();
        }
        private void tileView1_ItemClick(object sender, TileViewItemClickEventArgs e)
        {

            int itemkey = Convert.ToInt32(this.tileView1.GetRowCellValue(this.tileView1.FocusedRowHandle, "FoodID"));
            string isStandard = Convert.ToString(this.tileView1.GetRowCellValue(this.tileView1.FocusedRowHandle, "isStandard"));
            if (isStandard == "是")
            {
                List<Models.item_standard> list_itemstandard = BIZFood.QueryStandards(itemkey);
                MyForm.FoodManagement.StandardsForm standards = new StandardsForm(list_itemstandard);
                standards.StartPosition = FormStartPosition.CenterScreen;
                standards.MyEvent += CloseEvent;
                standards.ShowDialog();
            }
            else
            {
                var a = list.Where(o => o.itemkey == itemkey).FirstOrDefault();

                insertlist(itemkey, 0,Convert.ToDecimal(a.price), "", a.itemImgs,a.itemcategorykey.Value);
            }

        }
        private void CloseEvent()
        {
            if (MyModels.selected_category_items.temp.itemKey != 0)
            {
                int itemKey = MyModels.selected_category_items.temp.itemKey;
                decimal sprice = MyModels.selected_category_items.temp.sprice;
                int standardkey = MyModels.selected_category_items.temp.standardkey;
                string standardname = MyModels.selected_category_items.temp.standardname;
                int itemcategorykey = list.Find(o => o.itemkey == itemKey).itemcategorykey.Value;
                insertlist(itemKey, standardkey, sprice, standardname,null, itemcategorykey);
            }
            
        }
        private void insertlist(int itemKey,int standardkey,decimal sprice,string standardname,string itemImgs, int itemcategorykey)
        {
            if (_list.Find(o => o.itemKey == itemKey && o.standardkey == standardkey) == null)
            {
                MyModels.selected_category_items._selected_category_items selected_Category_Items = new MyModels.selected_category_items._selected_category_items();
                //selected_Category_Items.itemImgs = list.Find(o => o.itemkey == itemKey).itemImgs;
                selected_Category_Items.itemImgs = itemImgs;
                selected_Category_Items.itemKey = itemKey;
                selected_Category_Items.itemName = list.Find(o => o.itemkey == itemKey).itemName + "(" + standardname + ")";
                selected_Category_Items.num = 1;
                selected_Category_Items.sprice = sprice;
                selected_Category_Items.standardkey = standardkey;
                selected_Category_Items.standardname = standardname;
                selected_Category_Items.itemcategorykey = itemcategorykey;
                _list.Add(selected_Category_Items);

            }
            else
            {
                if (XtraMessageBox.Show("已存在，确定还要继续添加吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int num = _list.Find(o => o.itemKey == itemKey).num;
                    _list.Find(o => o.itemKey == itemKey).num = ++num;
                }
            }
            sumPrice();
            tileView3.RefreshData();
        }
        private void sumPrice()
        {
            Sum_price = _list.Sum(o => o.sprice * o.num);
            btn_save.Text = "保存(合计" + Sum_price.ToString() + ")";
            var a = _list.GroupBy(o => o.itemcategorykey).Select(p => new
            {
                num = p.Sum(o => o.num),
                categorykey = p.FirstOrDefault().itemcategorykey
            });
            foreach (var c in a)
            {
                list_category.Find(o => o.itemcategorykey == c.categorykey).num = c.num;
            }
            tileView2.RefreshData();
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            MyModels.selected_category_items.list = _list;
            this.Close();
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
