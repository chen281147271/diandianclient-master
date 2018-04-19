using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
namespace DianDianClient.MyForm.FoodManagement
{
    public partial class EditDetailForm : DevExpress.XtraEditors.XtraForm
    {
        public int? FoodID = 0;
        List<Models.v_category_items> list;
        MyControl.FoodManagement.EditDetail editDetail1;
        Biz.BIZFoodController BIZFood = new Biz.BIZFoodController();
        List<Models.item_category> list_itemcategory;
        List<Models.v_item_crude> list_itemcrude;
        List<Models.item_standard> list_itemstandard;
        Models.v_category_items v_Category_Items;
        public EditDetailForm(int FoodID, List<Models.v_category_items> list)
        {
            InitializeComponent();
            this.FoodID = FoodID;
            this.list = list;
            list_itemcategory = BIZFood.GetFoodFL();
            if (FoodID != -1)
            {
                v_Category_Items = list.Where(o => o.itemkey.Equals(FoodID)).FirstOrDefault();
            }
            MyControl.FoodManagement.EditDetail editDetail = new MyControl.FoodManagement.EditDetail(list_itemcategory, v_Category_Items);
            this.editDetail1 = editDetail;
            editDetail.Dock = DockStyle.Fill;
            editDetail.MyEvent += CloseEvent;
            this.Controls.Add(editDetail);
            if (FoodID != -1)
            {
                IniData();
            }
            else
            {
                editDetail1.gridView1.SetRowCellValue(rowHandl("商品编码:"), "Value", BIZFood.FindMax_itemCode());
                foreach (var a in list_itemcategory)
                {
                    editDetail1.repositoryItemComboBox1.Items.Add(a.name);
                }
            }
        }
        private void CloseEvent()
        {
            MyModels.EidtType.list.Clear();
            MyModels.selected_category_items.list.Clear();
            
            this.Close();
        }
        /// <summary>
        /// 初始化EditDetail
        /// </summary>
        private void IniData()
        {
           // var a = list.Where(o => o.itemkey.Equals(FoodID)).FirstOrDefault();
            SetRowCellValue(v_Category_Items);
            MyModels.selected_category_items.list.Clear();
            if (v_Category_Items.isSet == 1)
            {
                BIZFood.TaoCanFood(FoodID.Value);
            }
            SetCombobox(v_Category_Items.itemcategorykey);
            SetType(v_Category_Items.isStandard.Value, v_Category_Items.itemkey.Value);
        }
        private void SetType(int isStandard,int itemkey)
        {
            string str = "";
            if (isStandard == 0)
            {
                list_itemcrude = BIZFood.QueryItemCrude(itemkey, 0);
                this.editDetail1.list_itemcrude = list_itemcrude;
                foreach (var b in list_itemcrude)
                {
                    str += (b.crudename + "*" + b.num.ToString());
                    str += ",";
                }
            }
            else
            {
                var list_itemstandard = BIZFood.QueryStandards(itemkey);
                this.editDetail1.list_itemstandard = list_itemstandard;
                foreach (var b in list_itemstandard)
                {
                    str += b.standardname;
                    str += ",";
                }
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            editDetail1.gridView1.SetRowCellValue(rowHandl("是否分规格:")+1, "Value", str);
            if(isStandard == 0)
            {
                editDetail1.str_guigeNo = str;
            }
            else
            {
                editDetail1.str_guigeYse = str;
            }
        }
        private void SetCombobox(int? itemcategorykey)
        {
            editDetail1.repositoryItemComboBox1.Items.Clear();
            foreach(var a in list_itemcategory)
            {
                editDetail1.repositoryItemComboBox1.Items.Add(a.name);
            }
            if (itemcategorykey != null)
            {
                string name = "";
                var a = list_itemcategory.Where(o => o.itemcategorykey == itemcategorykey).FirstOrDefault();
                if (a != null) {
                     name = a.name;
                }
                else
                {
                     name = "";
                }
               editDetail1.gridView1.SetRowCellValue(rowHandl("所属类型:"), "Value", name);
            }
        }
        private int rowHandl(string name)
        {
            for(int i=0;i< editDetail1.gridView1.RowCount; i++)
            {
                if (editDetail1.gridView1.GetRowCellValue(i, "Name").Equals(name))
                    return i;
            }
            return -1;
        }
        private void SetRowCellValue(Models.v_category_items a)
        {
            editDetail1.gridView1.SetRowCellValue(rowHandl("折扣价格:"), "Value", a.discountPrice);
            editDetail1.gridView1.SetRowCellValue(rowHandl("菜品起点数:"), "Value", a.minnum);
            editDetail1.gridView1.SetRowCellValue(rowHandl("商品名称:"), "Value", a.itemName);
            editDetail1.gridView1.SetRowCellValue(rowHandl("商品编码:"), "Value", a.itemCode);
            editDetail1.gridView1.SetRowCellValue(rowHandl("商品价格:"), "Value", a.price);
            editDetail1.gridView1.SetRowCellValue(rowHandl("商品单位:"), "Value", a.unit);
            editDetail1.gridView1.SetRowCellValue(rowHandl("商家推荐:"), "Value", a.instructions);
            editDetail1.gridView1.SetRowCellValue(rowHandl("菜品图片:"), "Value",Utils.utils.GetBitmap(a.itemImgs));
           // editDetail1.gridView1.SetRowCellValue(rowHandl("是否为必选菜:"), "Value", (a.ismust == 1) ? true : false);
            editDetail1.gridView1.SetRowCellValue(rowHandl("是否是套餐:"), "Value", (a.isSet == 1) ? true : false);
            if (a.isSet == 1)
            {
                editDetail1.taocanan_yes();
                string str="";
                foreach (var b in MyModels.selected_category_items.list)
                {
                    str += b.itemName;
                    str += ",";
                }
                if (str.Length > 0)
                {
                    str = str.Substring(0, str.Length - 1);
                }
                editDetail1.gridView1.SetRowCellValue(rowHandl("套餐内菜品:"), "Value", str);
                editDetail1.gridView1.SetRowCellValue(rowHandl("是否打印套餐详情:"), "Value", a.isprintset);
            }
            editDetail1.gridView1.SetRowCellValue(rowHandl("是否打印:"), "Value", (a.isprint == 1) ? true : false);
            editDetail1.gridView1.SetRowCellValue(rowHandl("是否不参与结算打折:"), "Value", (a.ispayagio == 1) ? true : false);
            editDetail1.gridView1.SetRowCellValue(rowHandl("是否分规格:"), "Value", (a.isStandard == 1) ? true : false);
            if(a.isStandard == 1)
            {
                editDetail1.guige_yes();
            }
            editDetail1.gridView1.SetRowCellValue(rowHandl("是否称重:"), "Value", (a.selebyunit == 1) ? true : false);
            if (a.selebyunit == 1)
            {
                editDetail1.chengzhong_yes();
            }
        }
    }
}
