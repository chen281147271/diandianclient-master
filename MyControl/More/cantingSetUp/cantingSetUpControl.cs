﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using DianDianClient.Utils;
namespace DianDianClient.MyControl.More.cantingSetUp
{
    public partial class cantingSetUpControl : UserControl
    {
        private GridEditorCollection gridEditors;
        int dqcode;
        public cantingSetUpControl()
        {
            InitializeComponent();
            gridView1.OptionsMenu.ShowAutoFilterRowItem = false;
            IniData();
        }
        #region UI



        public class GridEditorItem
        {
            string fName;
            object fValue;
            RepositoryItem fRepositoryItem;

            public GridEditorItem(RepositoryItem fRepositoryItem, string fName, object fValue)
            {
                this.fRepositoryItem = fRepositoryItem;
                this.fName = fName;
                this.fValue = fValue;
            }
            public string Name { get { return this.fName; } }
            public object Value { get { return this.fValue; } set { this.fValue = value; } }
            public RepositoryItem RepositoryItem { get { return this.fRepositoryItem; } }
        }
        class GridEditorCollection : ArrayList
        {
            public GridEditorCollection()
            {
            }
            public new GridEditorItem this[int index] { get { return base[index] as GridEditorItem; } }
            public void Add(RepositoryItem fRepositoryItem, string fName, object fValue)
            {
                base.Add(new GridEditorItem(fRepositoryItem, fName, fValue));
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == this.gridEditorValue)
            {
                GridEditorItem item = gridView1.GetRow(e.RowHandle) as GridEditorItem;
                if (item != null) e.RepositoryItem = item.RepositoryItem;
            }
        }
        private void repositoryItemPictureEdit1_MouseDown(object sender, MouseEventArgs e)
        {
            Io_Api ia = new Io_Api();
            ia.mouse_click("R");
        }
        #endregion
        #region IniData
        void IniData()
        {
            InitInplaceEditors();
        }
        void InitInplaceEditors()
        {
            //设置Demo 从数据库取值后这样设置
            //this.gridView1.SetRowCellValue(rowHandle_CommodityName, "Value","hahhahahahah");

            this.gridEditors = new GridEditorCollection();
            //
            Bitmap bitmap = new Bitmap(Properties.Resources._1,new Size(100,50));

            this.gridEditors.Add(this.repositoryItemPictureEdit1, "门店头像", bitmap);
            this.gridEditors.Add(this.repositoryItemTextEdit1, "门店名称:", "门店名称");
            this.gridEditors.Add(this.repositoryItemTextEdit1, "商铺编码:", "000003");
            this.gridEditors.Add(this.repositoryItemTextEdit1, "门店电话:", "1232232555555");
            this.gridEditors.Add(this.repositoryItemTextEdit1, "联系人:", "联系人");
            this.gridEditors.Add(this.repositoryItemTextEdit1, "银行卡号:", "62222552252252");
            this.gridEditors.Add(this.repositoryItemTextEdit1, "开户人姓名:", "奥马巴");
            this.gridEditors.Add(this.repositoryItemTextEdit1, "开户行:", "6666银行");
            this.gridEditors.Add(this.repositoryItemTextEdit1, "详细地址:", "地址啊");
            this.gridEditors.Add(this.repositoryItemButtonEdit1, "门店所在地区:", "嘿嘿");
            this.gridEditors.Add(this.repositoryItemButtonEdit2, "菜品口味:", "嘿嘿");
            this.gridEditors.Add(this.repositoryItemTextEdit1, "餐具费:", "100");
            //
            this.gridControl1.DataSource = gridEditors;
        }
        #endregion

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            MyForm.More.cantingSetUp.TreeViewForm treeView = new MyForm.More.cantingSetUp.TreeViewForm();
            treeView.StartPosition = FormStartPosition.CenterScreen;
            treeView.ShowDialog();
            this.gridView1.SetRowCellValue(9, "Value", treeView.strSelected);
            this.dqcode = treeView.dqcode;
        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {

        }
    }
}
