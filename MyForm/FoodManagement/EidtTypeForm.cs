using DevExpress.XtraEditors.Repository;
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
    public partial class EidtTypeForm : DevExpress.XtraEditors.XtraForm
    {
       // List<Models.item_standard> list_itemstandard;
        Biz.BIZFoodController BIZFood = new Biz.BIZFoodController();
        List<MyModels.EidtType._EidtType> list;
        public string str="";
        RepositoryItem _disabledItem;
        public EidtTypeForm(List<MyModels.EidtType._EidtType> list)
        {
            this.list = list;
            InitializeComponent();
            this.gridControl1.DataSource = this.list;
        }
        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            //string standardkey = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "standardkey").ToString();
            //string itemKey = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "itemKey").ToString();
            //int istandardkey = Convert.ToInt32((standardkey == "")?"0": standardkey);
            //int iitemKey = Convert.ToInt32((itemKey == "") ? "0" : itemKey);
            //var b = BIZFood.QueryItemCrude(iitemKey, istandardkey);
            //foreach (var c in b)
            //{
            //    Models.v_item_crude item_Crude = new Models.v_item_crude();
            //    item_Crude.crudeid = c.crudeid;
            //    item_Crude.num = c.num;
            //    list_itemcrude.Add(item_Crude);
            //}
            List<Models.v_item_crude> list_itemcrude = new List<Models.v_item_crude>();
            list_itemcrude=(List<Models.v_item_crude>)this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "item_standard");

            MyForm.FoodManagement.MaterialSettingForm materialSetting = new MyForm.FoodManagement.MaterialSettingForm(list_itemcrude);
            materialSetting.StartPosition = FormStartPosition.CenterScreen;
            materialSetting.ShowDialog();
            

            this.gridView1.SetRowCellValue(this.gridView1.FocusedRowHandle, "yuanliao", materialSetting.strSelected);
            this.gridView1.SetRowCellValue(this.gridView1.FocusedRowHandle, "item_standard", materialSetting.list_itemcrude);
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            List<MyModels.EidtType._EidtType> list_EidtType = new List<MyModels.EidtType._EidtType>();
            list_EidtType.Clear();
            for (int i=0; i < this.gridView1.RowCount; i++)
            {
                string standardname= this.gridView1.GetRowCellValue(i, "standardname").ToString();
                str += standardname;
                str += ",";
                string itemKey = this.gridView1.GetRowCellValue(i, "itemKey").ToString();
                decimal sprice = Convert.ToDecimal(this.gridView1.GetRowCellValue(i, "sprice"));
                string standardkey = this.gridView1.GetRowCellValue(i, "standardkey").ToString();
                string yuanliao = this.gridView1.GetRowCellValue(i, "yuanliao").ToString();
                bool state=Convert.ToBoolean(this.gridView1.GetRowCellValue(i, "state"));
                List<Models.v_item_crude> item_standard =(List<Models.v_item_crude>)this.gridView1.GetRowCellValue(i, "item_standard");
                MyModels.EidtType._EidtType _EidtType = new MyModels.EidtType._EidtType();
                _EidtType.itemKey = itemKey;
                _EidtType.sprice = sprice;
                _EidtType.standardkey = standardkey;
                _EidtType.standardname = standardname;
                _EidtType.yuanliao = yuanliao;
                _EidtType.item_standard = item_standard;
                _EidtType.state = state;
                list_EidtType.Add(_EidtType);
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            MyModels.EidtType.list.Clear();
            MyModels.EidtType.list = list_EidtType;
            this.Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            int irow = this.gridView1.FocusedRowHandle;
            MyModels.EidtType._EidtType _EidtType = new MyModels.EidtType._EidtType();
            List<Models.v_item_crude> item_standard = new List<Models.v_item_crude>();
            _EidtType.itemKey = "";
            _EidtType.sprice = 0;
            _EidtType.standardkey = "";
            _EidtType.standardname = "";
            _EidtType.yuanliao = "";
            _EidtType.item_standard = item_standard;
            this.list.Add(_EidtType);
            this.gridView1.RefreshData();
        }
        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.gridView1.DeleteRow(this.gridView1.FocusedRowHandle);
        }
        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RepositoryItem.Name != "repositoryItemRadioGroup1")//需要设置的列名
                return;
            if (_disabledItem == null)
            {
                _disabledItem = (RepositoryItem)e.RepositoryItem.Clone();
                _disabledItem.ReadOnly = true;
                _disabledItem.Enabled = false;
            }
            string itemKey = (this.gridView1.GetRowCellValue(e.RowHandle, "itemKey")==null)?"": this.gridView1.GetRowCellValue(e.RowHandle, "itemKey").ToString();
            if (itemKey == "")
                e.RepositoryItem = _disabledItem;

        }
    }
}
