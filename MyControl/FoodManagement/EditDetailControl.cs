using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace DianDianClient.MyControl
{
    public partial class EditDetail :UserControl 
    {
        public string FoodID = "";
        private GridEditorCollection gridEditors;
        public EditDetail()
        {
          //  this.FoodID = FoodID;
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

        #region rowHandle定义
        /// <summary>
        /// rowHandle_CommodityName 商品名称
        /// </summary>
        public int rowHandle_CommodityName = 0;
        /// <summary>
        /// rowHandle_CommodityName 商品编码
        /// </summary>
        public int rowHandle_CommodityNo = 1;
        /// <summary>
        /// rowHandle_CommodityName 商品价格
        /// </summary>
        public int rowHandle_CommodityPrice = 2;
        /// <summary>
        /// rowHandle_CommodityName 折扣价格
        /// </summary>
        public int rowHandle_CommodityDiscountPrice = 3;
        /// <summary>
        /// rowHandle_CommodityName 所属类型
        /// </summary>
        public int rowHandle_CommodityType = 4;
        /// <summary>
        /// rowHandle_CommodityName 菜品图片
        /// </summary>
        public int rowHandle_CommodityImage = 5;
        /// <summary>
        /// rowHandle_CommodityName 菜品起点数
        /// </summary>
        public int rowHandle_CommodityMiniNum = 6;
        /// <summary>
        /// rowHandle_CommodityName 是否分规格
        /// </summary>
        public int rowHandle_TypeYesNo = 7;
        /// <summary>
        /// rowHandle_CommodityName 原料设置
        /// </summary>
        public int rowHandle_MaterialSetting = 8;
        /// <summary>
        /// rowHandle_CommodityName 是否是套餐
        /// </summary>
        public int rowHandle_PackageYseNo = 9;
        /// <summary>
        /// rowHandle_CommodityName 是否打印
        /// </summary>
        public int rowHandle_PrintYseNo = 10;
        /// <summary>
        /// rowHandle_CommodityName 是否按单位销售
        /// </summary>
        public int rowHandle_UnitSalesYseNo = 11;
        /// <summary>
        /// rowHandle_CommodityName 商品单位
        /// </summary>
        public int rowHandle_CommodityUnit = 12;
        /// <summary>
        /// rowHandle_CommodityName 是否不参与结算打折
        /// </summary>
        public int rowHandle_SaleYseNo = 13;
        /// <summary>
        /// rowHandle_CommodityName 是否为必选菜
        /// </summary>
        public int rowHandle_MandatoryFoodYseNo = 14;
        /// <summary>
        /// rowHandle_CommodityName 商家推荐
        /// </summary>
        public int rowHandle_Recommend = 15;
        #endregion

        #region IniData

        void IniData()
        {
            InitInplaceEditors();

            InitCommbox();
        }
        void InitCommbox()
        {
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "哈哈哈",
            "呵呵",
            "嘿嘿"});
        }
        void InitInplaceEditors()
        {
            //设置Demo 从数据库取值后这样设置
            //this.gridView1.SetRowCellValue(rowHandle_CommodityName, "Value","hahhahahahah");

            this.gridEditors = new GridEditorCollection();
            //
            this.gridEditors.Add(this.repositoryItemTextEdit1, "商品名称:", "商品名称");
            this.gridEditors.Add(this.repositoryItemTextEdit1, "商品编码:", FoodID);
            this.gridEditors.Add(this.repositoryItemTextEdit2, "商品价格:", "121");
            this.gridEditors.Add(this.repositoryItemTextEdit2, "折扣价格:", "100");
            this.gridEditors.Add(this.repositoryItemComboBox1, "所属类型:", "嘿嘿");
            this.gridEditors.Add(this.repositoryItemPictureEdit1, "菜品图片", Properties.Resources._1);
            this.gridEditors.Add(this.repositoryItemTextEdit2, "菜品起点数:", "100");
            this.gridEditors.Add(this.repositoryItemRadioGroup, "是否分规格:", 1);
            this.gridEditors.Add(this.repositoryItemButtonEdit1, "原料设置:", 1);
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否是套餐:", true);
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否打印:", true);
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否按单位销售:", true);
            this.gridEditors.Add(this.repositoryItemTextEdit1, "商品单位:", "瓶");
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否不参与结算打折:", true);
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否为必选菜:", true);
            this.gridEditors.Add(this.repositoryItemMemoEdit1, "商家推荐", "商家推荐商家推荐商家推荐");
            //
            this.gridControl1.DataSource = gridEditors;
        }
        #endregion

        #region evet function
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // Bitmap aaa=(Bitmap)this.gridView1.GetRowCellValue(3, "Value");
            string aaa = (string)this.gridView1.GetRowCellValue(1, "Value");
            MessageBox.Show("第二行:" + aaa);

        }

        /// <summary>
        /// 原料设置按钮 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            //strSelected是返回值
            MaterialSettingForm form4 = new MaterialSettingForm();
            form4.StartPosition = FormStartPosition.CenterScreen;
            form4.ShowDialog();
            this.gridView1.SetRowCellValue(rowHandle_MaterialSetting, "Value", form4.strSelected);
        }
        #endregion


    }
}
