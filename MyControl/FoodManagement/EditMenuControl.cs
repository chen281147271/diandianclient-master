﻿using System;
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
using DevExpress.Utils;
using System.IO;

namespace DianDianClient.MyControl.FoodManagement
{
    public partial class EditMenu : UserControl
    {
        Biz.BIZFoodController bIZFoodController = new Biz.BIZFoodController();
        List<Models.v_category_items> list;
        List<Models.item_category> list_itemcategory;
        List<CriteriaOperator> criteriaOperator = new List<CriteriaOperator>();
        int iflag = 0; //0全部 1上架 2估清
        public EditMenu()
        {
            InitializeComponent();
            InitData();
            SetupView();
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
                tileView1.OptionsTiles.ItemSize = new Size(240, 150);
                tileView1.Appearance.ItemNormal.ForeColor = Color.White;
                tileView1.Appearance.ItemNormal.BorderColor = Color.Transparent;
                tileView1.Appearance.ItemNormal.BackColor = Color.Transparent;
               //Setup tiles template
               TileViewItemElement leftPanel = new TileViewItemElement();
                TileViewItemElement splitLine = new TileViewItemElement();
                TileViewItemElement addressCaption = new TileViewItemElement();
                TileViewItemElement addressValue = new TileViewItemElement();
                TileViewItemElement yearBuiltCaption = new TileViewItemElement();
                TileViewItemElement yearBuiltValue = new TileViewItemElement();
                TileViewItemElement price = new TileViewItemElement();
                TileViewItemElement image = new TileViewItemElement();
                TileViewItemElement state = new TileViewItemElement();
                tileView1.TileTemplate.Add(leftPanel);
                tileView1.TileTemplate.Add(splitLine);
                tileView1.TileTemplate.Add(addressCaption);
                tileView1.TileTemplate.Add(addressValue);
                tileView1.TileTemplate.Add(yearBuiltCaption);
                tileView1.TileTemplate.Add(yearBuiltValue);
                tileView1.TileTemplate.Add(price);
                tileView1.TileTemplate.Add(image);
                tileView1.TileTemplate.Add(state);
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
                
                price.Column = tileView1.Columns["FoodPrice"];
                price.TextAlignment = TileItemContentAlignment.BottomLeft;
                price.Appearance.Normal.Font = new Font("Segoe UI Semilight", 25.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //
                image.Column = tileView1.Columns["FoodImage"];
                image.ImageSize = new Size(180, 180);
                image.ImageAlignment = TileItemContentAlignment.MiddleRight;
                image.ImageScaleMode = TileItemImageScaleMode.ZoomOutside;
                image.ImageLocation = new Point(10, 10);
                //
                state.Column = tileView1.Columns["State"];
                state.TextVisible = false;
                //
                tileView1.ColumnSet.GroupColumn = tileView1.Columns["FoodGroupName"];
                tileView1.OptionsTiles.Orientation = Orientation.Vertical;
                //
                DevExpress.Utils.ContextButton contextButton1 = new DevExpress.Utils.ContextButton();
                DevExpress.Utils.ContextButton contextButton2 = new DevExpress.Utils.ContextButton();
                DevExpress.Utils.ContextButton contextButton3 = new DevExpress.Utils.ContextButton();
                this.tileView1.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                this.tileView1.ContextButtonOptions.BottomPanelPadding = new System.Windows.Forms.Padding(10);
                //
            //    contextButton1.Caption = "下架";
                contextButton1.ImageOptions.Image = global::DianDianClient.Properties.Resources.offshelf;
                contextButton2.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
                contextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
                contextButton1.Id = new System.Guid("5679cac7-1f0e-4f93-a9d4-cd3f82547937");
                contextButton1.Name = "contextButton1";

            //    contextButton2.Caption = "contextButton2";
                //
                contextButton2.ImageOptions.Image = global::DianDianClient.Properties.Resources.delete;
                contextButton3.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far;
                contextButton2.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
                contextButton2.Id = new System.Guid("9a35eabb-9479-4144-a912-725a1da88885");
                contextButton2.Name = "contextButton2";
                //
             //   contextButton3.Caption = "contextButton3";
                contextButton3.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far;
                contextButton3.ImageOptions.Image = global::DianDianClient.Properties.Resources.edit;
                contextButton3.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
                contextButton3.Id = new System.Guid("d54ff57a-998d-4251-b811-3b17e36c75aa");
                contextButton3.Name = "contextButton3";
                this.tileView1.ContextButtons.Add(contextButton1);
                this.tileView1.ContextButtons.Add(contextButton2);
                this.tileView1.ContextButtons.Add(contextButton3);
                this.tileView1.GridControl = this.gridControl1;
                this.tileView1.Name = "tileView1";
            }
            finally
            {
                tileView1.EndUpdate();
            }
        }
        #endregion

        #region InitData
        protected virtual void InitData()
        {
            try
            {
                RefreshList();
            }
            catch { }
        }
        private void RefreshList()
        {
            this.radioGroup1.SelectedIndex = 0;
            list = bIZFoodController.GetFoodList(0,0);
            list_itemcategory = bIZFoodController.GetFoodFL();



            // Demo 数据 字段名请不要改变
            DataTable dt = new DataTable();
            dt.Columns.Add("FoodName", typeof(String));
            dt.Columns.Add("FoodImage", typeof(Bitmap));
            dt.Columns.Add("FoodPrice", typeof(String));
            dt.Columns.Add("FoodGroupName", typeof(String));
            dt.Columns.Add("FoodID", typeof(Int32));
            dt.Columns.Add("FoodGroupID", typeof(Int32));
            dt.Columns.Add("State", typeof(Int32));

            DataTable _dt = new DataTable();
            _dt.Columns.Add("FoodGroupName", typeof(String));
            _dt.Columns.Add("FoodGroupID", typeof(Int32));

            foreach (var a in list)
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
                
                dt.Rows.Add(new object[] { a.itemName, Utils.utils.GetBitmap(a.itemImgs), a.price, a.categoryName, a.itemkey, a.itemcategorykey,a.state });
            }
            _dt.Rows.Add("全部", 0);
            foreach (var a in list_itemcategory)
            {
                _dt.Rows.Add(new object[] { a.name, a.itemcategorykey });
            }

            gridControl1.DataSource = dt;
            //var q = from p in dt.AsEnumerable()
            //        group p by  new {t1= p.Field<int>("FoodGroupID"), t2 = p.Field<string>("FoodGroupName") } into g
            //        select new { FoodGroupID =g.Key.t1, FoodGroupName = g.Key.t2 };
            gridControl2.DataSource = _dt;
        }
        #endregion

        #region event
        private void tileView1_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            string FoodID = e.Item.Tag.ToString();
            if (e.Item.Name == "contextButton1")
            {
               if( XtraMessageBox.Show(FoodID + "下架","提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
                {
                    ContextBtnOffShelf_Click(FoodID);
                }
                
            }
            else if (e.Item.Name == "contextButton2")
            {
                ContextBtnDelete_Click(FoodID);
            }
            else if (e.Item.Name == "contextButton3")
            {
                ContextBtnEdit_Click(FoodID);
            }
        }
        /// <summary>
        /// 把FoodID值复制到ContextButton.Tag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileView1_ContextButtonCustomize(object sender, TileViewContextButtonCustomizeEventArgs e)
        {
            int FoodID= Convert.ToInt32(tileView1.GetRowCellDisplayText(e.RowHandle, "FoodID"));
            ((DevExpress.Utils.ContextButton)e.Item).Tag = FoodID;
            int? state = list.Where(o => o.itemkey.Equals(FoodID)).FirstOrDefault().state;
              if (e.Item.Name == "contextButton1")
              {
                if (state != 1)
                {
                    e.Item.ImageOptions.Image= global::DianDianClient.Properties.Resources.shangjia;
                }
                else
                {
                    e.Item.ImageOptions.Image = global::DianDianClient.Properties.Resources.offshelf;
                }
              }
        }



        private void tileView2_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            string GroupID = e.Item.Tag.ToString();
            if (e.Item.Name == "contextButton1")
            {
                ContextBtnLeftEdit_Click(GroupID);
            }
            else if (e.Item.Name == "contextButton2")
            {
                ContextBtnSort_Click(GroupID);
            }
        }
        /// <summary>
        /// 把FoodGroup值复制到ContextButton.Tag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileView2_ContextButtonCustomize(object sender, TileViewContextButtonCustomizeEventArgs e)
        {
            ((DevExpress.Utils.ContextButton)e.Item).Tag = tileView2.GetRowCellDisplayText(e.RowHandle, "FoodGroupID");
        }

        #endregion

        #region event function 需要改的都在这里
        /// <summary>
        /// 下架按钮
        /// </summary>
        /// <param name="FoodId">菜品ID</param>
        private void ContextBtnOffShelf_Click(string FoodId)
        {
            bIZFoodController.ChangeState(Convert.ToInt32(FoodId), 0);
            RefreshList();
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="FoodId">菜品ID</param>
        private void ContextBtnDelete_Click(string FoodId)
        {
            bIZFoodController.DelItem(Convert.ToInt32(FoodId));
            RefreshList();
        }
        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="FoodId">菜品ID</param>
        private void ContextBtnEdit_Click(string FoodId)
        {
            MyForm.FoodManagement.EditDetailForm editDetailForm = new MyForm.FoodManagement.EditDetailForm(Convert.ToInt32(FoodId),list);
            editDetailForm.StartPosition = FormStartPosition.CenterScreen;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            editDetailForm.Height = yHeight;
            editDetailForm.ShowDialog();
        }
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
        /// <summary>
        /// 左侧编辑按钮
        /// </summary>
        private void ContextBtnLeftEdit_Click(string GroupID)
        {
            MyForm.FoodManagement.EditGroupForm editGroupForm = new MyForm.FoodManagement.EditGroupForm(Convert.ToInt32(GroupID),-1);
            editGroupForm.StartPosition = FormStartPosition.CenterScreen;
            editGroupForm.ShowDialog();
            RefreshList();
        }
        /// <summary>
        /// 左侧排序按钮
        /// </summary>
        /// <param name="GroupName"></param>
        private void ContextBtnSort_Click(string GroupID)
        {
            MessageBox.Show(GroupID += "排序");
        }
        #endregion

        private void Txt_FoodName_EditValueChanged(object sender, EventArgs e)
        {
            Changed(iflag);
        }
        private void Changed(int i=0)
        {
            iflag = i;
            this.tileView1.ClearColumnsFilter();
            criteriaOperator.Clear();
            Txt_FoodNameEditValueChanged();
            //  radioGroup1SelectedIndexChanged(radioGroup1.SelectedIndex);
            radioGroup1SelectedIndexChanged(i);
            this.tileView1.ActiveFilterCriteria = GroupOperator.And(criteriaOperator);

        }
        private void Txt_FoodNameEditValueChanged()
        {
            if (Txt_FoodName.Text.Length > 0)
            {
                CriteriaOperator CriteriaOperator = new BinaryOperator("FoodName", "%" + Txt_FoodName.Text + "%", BinaryOperatorType.Like);
                criteriaOperator.Add(CriteriaOperator);
            }
            else
            {
                this.tileView1.ClearColumnsFilter();
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Changed();
        }
        private void radioGroup1SelectedIndexChanged(int SelectedIndex)
        {
            switch (SelectedIndex)
            {
                case 0:
                    this.tileView1.ClearColumnsFilter();
                    break;
                case 1:
                    CriteriaOperator CriteriaOperator = new BinaryOperator("State", 1, BinaryOperatorType.Equal);
                    criteriaOperator.Add(CriteriaOperator);
                    break;
                case 2:
                    CriteriaOperator _CriteriaOperator = new BinaryOperator("State", 1, BinaryOperatorType.NotEqual);
                    criteriaOperator.Add(_CriteriaOperator);
                    break;
            }
        }

        private void btn_addclass_Click(object sender, EventArgs e)
        {
           int itemCategoryCode = bIZFoodController.FindMax_itemCategoryCode();
           MyForm.FoodManagement.EditGroupForm editGroupForm = new MyForm.FoodManagement.EditGroupForm(-1, itemCategoryCode);
           editGroupForm.StartPosition = FormStartPosition.CenterScreen;
           editGroupForm.ShowDialog();
           RefreshList();
        }

        private void btn_addfood_Click(object sender, EventArgs e)
        {
            MyForm.FoodManagement.EditDetailForm editDetailForm = new MyForm.FoodManagement.EditDetailForm(-1, list);
            editDetailForm.StartPosition = FormStartPosition.CenterScreen;
            int yHeight = SystemInformation.PrimaryMonitorSize.Height;
            editDetailForm.Height = yHeight;
            editDetailForm.ShowDialog();
        }

        private void btn_tuijian_Click(object sender, EventArgs e)
        {
            MyForm.FoodManagement.TuiJianForm tuiJian = new MyForm.FoodManagement.TuiJianForm();
            tuiJian.StartPosition = FormStartPosition.CenterScreen;
            tuiJian.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btn_shangjia_Click(object sender, EventArgs e)
        {
            Changed(1);
        }

        private void btn_guqing_Click(object sender, EventArgs e)
        {
            Changed(2);
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            Changed(0);
        }
    }
}
