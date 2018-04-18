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
namespace DianDianClient.MyControl.FoodManagement
{
    public partial class EditGroupControl : UserControl
    {
        private GridEditorCollection gridEditors;
        Biz.BIZFoodController BIZFood = new Biz.BIZFoodController();
        Models.item_category item_Category;
        int itemCategoryCode;
        int itemcategorykey;
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public EditGroupControl(int itemcategorykey, int itemCategoryCode)
        {
            this.itemCategoryCode = itemCategoryCode;
            this.itemcategorykey = itemcategorykey;
            item_Category = BIZFood.GetCategoryDetail(itemcategorykey);
            InitializeComponent();
            InitInplaceEditors();
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
            public string Name { get { return this.fName; } set { this.fValue = value; } }
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
        #endregion

        #region rowHandle定义
        /// <summary>
        /// rowHandle_GroupName 分类名称
        /// </summary>
        public int rowHandle_GroupName = 0;
        /// <summary>
        /// rowHandle_GroupNo 分类编码
        /// </summary>
        public int rowHandle_GroupNo = 1;
        /// <summary>
        /// rowHandle_GroupOrder 排列顺序
        /// </summary>
        public int rowHandle_GroupOrder = 2;
        /// <summary>
        /// rowHandle_GroupName 起点菜品种数
        /// </summary>
        public int rowHandle_Miniqidian = 3;
        /// <summary>
        /// rowHandle_GroupName 最低消费
        /// </summary>
        public int rowHandle_Minixiaofei = 4;
        /// <summary>
        /// rowHandle_GroupName 是否有效
        /// </summary>
        public int rowHandle_GroupvalidYesNo = 5;
        /// <summary>
        /// rowHandle_GroupName 是否打印
        /// </summary>
        public int rowHandle_GroupPrintYesNo = 6;
        /// <summary>
        /// rowHandle_GroupName 是否打印
        /// </summary>
        public int rowHandle_GroupSaleYesNo = 7;

        #endregion

        #region IniData
        private void IniGrid()
        {
            if (itemcategorykey != -1)
            {
                this.labelControl1.Text = "修改商品分类";
                this.gridView1.SetRowCellValue(rowHandle_GroupNo, "Value", item_Category.itemCategoryCode);
                this.gridView1.SetRowCellValue(rowHandle_GroupName, "Value", item_Category.name);
                this.gridView1.SetRowCellValue(rowHandle_GroupOrder, "Value", item_Category.orderNo);
                this.gridView1.SetRowCellValue(rowHandle_GroupvalidYesNo, "Value", (item_Category.enable == 1) ? true : false);
                this.gridView1.SetRowCellValue(rowHandle_Miniqidian, "Value", 0);
                this.gridView1.SetRowCellValue(rowHandle_Minixiaofei, "Value",0);
                this.gridView1.SetRowCellValue(rowHandle_GroupPrintYesNo, "Value", false);
                bool GroupSaleYesNo = true;
                if (GroupSaleYesNo)
                {
                    GroupSaleYesNo_YSE();
                }
                this.gridView1.SetRowCellValue(rowHandle_GroupSaleYesNo, "Value", GroupSaleYesNo);
            }
            else
            {
                this.labelControl1.Text = "新增商品分类";
                this.gridView1.SetRowCellValue(rowHandle_GroupNo, "Value", this.itemCategoryCode);
            }

        }
        void InitInplaceEditors()
        {
            this.gridEditors = new GridEditorCollection();

            this.gridEditors.Add(this.repositoryItemTextEdit1, "分类名称:", "");
            this.gridEditors.Add(this.repositoryItemTextEdit3, "分类编码:", 0);
            this.gridEditors.Add(this.repositoryItemTextEdit1, "排列顺序:", 0);
            this.gridEditors.Add(this.repositoryItemTextEdit1, "起点菜品种数:", 0);
            this.gridEditors.Add(this.repositoryItemTextEdit1, "最低消费:", 0);
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否有效:", false);
            if (itemcategorykey != -1)
            {
                this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否打印:", false);
                this.gridEditors.Add(this.repositoryItemToggleSwitch2, "是否打折:", false);
            }
            else
            {
                this.btn_delete.Visible = false;
            }

            this.gridControl1.DataSource = gridEditors;
            IniGrid();
        }

        #endregion

        #region evet function
        private void btn_save_Click(object sender, EventArgs e)
        {
            string categoryName = this.gridView1.GetRowCellValue(rowHandle_GroupName, "Value").ToString();
            string code= this.gridView1.GetRowCellValue(rowHandle_GroupNo, "Value").ToString();
            int orderNo =Convert.ToInt32(this.gridView1.GetRowCellValue(rowHandle_GroupOrder, "Value"));
            int enable= Convert.ToInt32(this.gridView1.GetRowCellValue(rowHandle_GroupvalidYesNo, "Value"));
            if (itemcategorykey != -1)
            {
                BIZFood.EditItemCategory(this.itemcategorykey, categoryName,code, orderNo, enable);
            }
            else
            {
                BIZFood.AddItemCategory(categoryName, this.itemCategoryCode.ToString(), orderNo, enable);
            }
            MyEvent();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            BIZFood.DelItemCategory(this.itemcategorykey);
            this.MyEvent();
        }
        #endregion

        private void repositoryItemToggleSwitch2_EditValueChanged(object sender, EventArgs e)
        {
            int irow = this.gridView1.FocusedRowHandle;
            ToggleSwitch toggleSwitch = (ToggleSwitch)sender;
            if (toggleSwitch.IsOn)
            {
                GridEditorItem editorItem = new GridEditorItem(this.repositoryItemTextEdit1, "打折幅度(注：最大填10，填10为不打折):", "");
                this.gridEditors.Insert(irow + 1, editorItem);


            }
            else
            {
                this.gridEditors.RemoveAt(irow + 1);
            }
            this.gridView1.RefreshData();
        }
        private void GroupSaleYesNo_YSE()
        {
            GridEditorItem editorItem = new GridEditorItem(this.repositoryItemTextEdit1, "打折幅度(注：最大填10，填10为不打折):", "");
            this.gridEditors.Insert(rowHandle_GroupSaleYesNo + 1, editorItem);
            this.gridView1.RefreshData();
        }
    }
}
