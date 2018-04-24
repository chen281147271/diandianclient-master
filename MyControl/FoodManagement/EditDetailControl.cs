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
using DianDianClient.Utils;
using System.Drawing.Imaging;
using System.IO;

namespace DianDianClient.MyControl.FoodManagement
{
    public partial class EditDetail :UserControl 
    {
        public string FoodID = "";
        private GridEditorCollection gridEditors;
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        List<Models.item_category> list_itemcategory;
        public List<Models.v_item_crude> list_itemcrude=new List<Models.v_item_crude>();
        public List<Models.item_standard> list_itemstandard;
        int itemcategorykey;
        bool isStandard;
        public string str_guigeYse;//分规格和不分规格 暂存
        public string str_guigeNo;
        Biz.BIZFoodController BIZFood = new Biz.BIZFoodController();
        Models.v_category_items v_Category_Items;
        public EditDetail(List<Models.item_category> list_itemcategory, Models.v_category_items v_Category_Items)
        {
            //  this.FoodID = FoodID;
            this.v_Category_Items = v_Category_Items;
            this.list_itemcategory = list_itemcategory;
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
            public string Name { get { return this.fName; } set { this.fName = value; } }
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

        //#region rowHandle定义
        ///// <summary>
        ///// rowHandle_CommodityName 商品名称
        ///// </summary>
        //public int rowHandle_CommodityName = 0;
        ///// <summary>
        ///// rowHandle_CommodityName 商品编码
        ///// </summary>
        //public int rowHandle_CommodityNo = 1;
        ///// <summary>
        ///// rowHandle_CommodityName 商品价格
        ///// </summary>
        //public int rowHandle_CommodityPrice = 2;
        ///// <summary>
        ///// rowHandle_CommodityName 折扣价格
        ///// </summary>
        //public int rowHandle_CommodityDiscountPrice = 3;
        ///// <summary>
        ///// rowHandle_CommodityName 所属类型
        ///// </summary>
        //public int rowHandle_CommodityType = 4;
        ///// <summary>
        ///// rowHandle_CommodityName 菜品图片
        ///// </summary>
        //public int rowHandle_CommodityImage = 5;
        ///// <summary>
        ///// rowHandle_CommodityName 菜品起点数
        ///// </summary>
        //public int rowHandle_CommodityMiniNum = 6;
        ///// <summary>
        ///// rowHandle_CommodityName 是否分规格
        ///// </summary>
        //public int rowHandle_TypeYesNo = 7;
        ///// <summary>
        ///// rowHandle_CommodityName 原料设置
        ///// </summary>
        //public int rowHandle_MaterialSetting = 8;
        ///// <summary>
        ///// rowHandle_CommodityName 是否是套餐
        ///// </summary>
        //public int rowHandle_PackageYseNo = 9;
        ///// <summary>
        ///// rowHandle_CommodityName 是否打印
        ///// </summary>
        //public int rowHandle_PrintYseNo = 10;
        ///// <summary>
        ///// rowHandle_CommodityName 是否按单位销售
        ///// </summary>
        //public int rowHandle_UnitSalesYseNo = 11;
        ///// <summary>
        ///// rowHandle_CommodityName 商品单位
        ///// </summary>
        //public int rowHandle_CommodityUnit = 12;
        ///// <summary>
        ///// rowHandle_CommodityName 是否不参与结算打折
        ///// </summary>
        //public int rowHandle_SaleYseNo = 13;
        ///// <summary>
        ///// rowHandle_CommodityName 是否为必选菜
        ///// </summary>
        //public int rowHandle_MandatoryFoodYseNo = 14;
        ///// <summary>
        ///// rowHandle_CommodityName 商家推荐
        ///// </summary>
        //public int rowHandle_Recommend = 15;
        ////
        ///// <summary>
        ///// rowHandle_CommodityName 套餐内菜品
        ///// </summary>
        //public int rowHandle_PackageFood = 0;
        ///// <summary>
        ///// rowHandle_CommodityName 是否打印套餐详情:
        ///// </summary>
        //public int rowHandle_PackagePrint = 0;
        //#endregion

        #region IniData

        void IniData()
        {
            InitInplaceEditors();
        }
        private int rowHandl(string name)
        {
            for (int i = 0; i < this.gridView1.RowCount; i++)
            {
                if (this.gridView1.GetRowCellValue(i, "Name").Equals(name))
                    return i;
            }
            return -1;
        }
        void InitInplaceEditors()
        {
            //设置Demo 从数据库取值后这样设置
            //this.gridView1.SetRowCellValue(rowHandle_CommodityName, "Value","hahhahahahah");

            this.gridEditors = new GridEditorCollection();
            //
            this.gridEditors.Add(this.repositoryItemTextEdit1, "商品名称:", "");
            this.gridEditors.Add(this.repositoryItemTextEdit3, "商品编码:", FoodID);
            this.gridEditors.Add(this.repositoryItemTextEdit2, "商品价格:", 0);
            this.gridEditors.Add(this.repositoryItemTextEdit2, "折扣价格:", 0);
            this.gridEditors.Add(this.repositoryItemComboBox1, "所属类型:", "");
            this.gridEditors.Add(this.repositoryItemPictureEdit1, "菜品图片:", Properties.Resources._1);
            this.gridEditors.Add(this.repositoryItemTextEdit2, "菜品起点数:", 0);
            this.gridEditors.Add(this.repositoryItemToggleSwitch2, "是否分规格:", false);
            this.gridEditors.Add(this.repositoryItemButtonEdit1, "原料设置:", "");
            this.gridEditors.Add(this.repositoryItemToggleSwitch3, "是否是套餐:", false);
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否打印:", true);
            this.gridEditors.Add(this.repositoryItemToggleSwitch4, "是否称重:", false);
            this.gridEditors.Add(this.repositoryItemTextEdit1, "商品单位:", "");
            this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否不参与结算打折:", false);
           // this.gridEditors.Add(this.repositoryItemToggleSwitch1, "是否为必选菜:", true);
            this.gridEditors.Add(this.repositoryItemMemoEdit1, "商家推荐:", "");
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
            Save();
            MyEvent();

        }
        private void Save()
        {
            string imgs = SaveImage();
            imgs = (imgs == "") ? (v_Category_Items==null)?"": v_Category_Items.itemImgs: imgs;
            int itemkey = (v_Category_Items==null)?0: v_Category_Items.itemkey.Value;
            string name = this.gridView1.GetRowCellValue(rowHandl("商品名称:"), "Value").ToString();
            string code = this.gridView1.GetRowCellValue(rowHandl("商品编码:"), "Value").ToString();
            double discountPrice = Convert.ToDouble(this.gridView1.GetRowCellValue(rowHandl("商品价格:"), "Value"));
            double agioprice = Convert.ToDouble(this.gridView1.GetRowCellValue(rowHandl("折扣价格:"), "Value"));
            int itemType = this.itemcategorykey;
            int minnum = Convert.ToInt32(this.gridView1.GetRowCellValue(rowHandl("菜品起点数:"), "Value"));
            int isStandard = Convert.ToInt32(this.gridView1.GetRowCellValue(rowHandl("是否分规格:"), "Value"));
            int isSet = Convert.ToInt32(this.gridView1.GetRowCellValue(rowHandl("是否是套餐:"), "Value"));
            int isPrint = Convert.ToInt32(this.gridView1.GetRowCellValue(rowHandl("是否打印:"), "Value"));
            sbyte selebyunit = Convert.ToSByte(this.gridView1.GetRowCellValue(rowHandl("是否称重:"), "Value"));
            string unit = Convert.ToString(this.gridView1.GetRowCellValue(rowHandl("商品单位:"), "Value"));
            sbyte ispayagio = Convert.ToSByte(this.gridView1.GetRowCellValue(rowHandl("是否不参与结算打折:"), "Value"));
            sbyte ismust = Convert.ToSByte(this.gridView1.GetRowCellValue(rowHandl("是否为必选菜:"), "Value"));
            string introduce = Convert.ToString(this.gridView1.GetRowCellValue(rowHandl("商家推荐:"), "Value"));
            List<Biz.BIZFoodController.ItemTCInfo> tcList = new List<Biz.BIZFoodController.ItemTCInfo>();
            Biz.BIZFoodController.ItemTCInfo itemTCInfo = new Biz.BIZFoodController.ItemTCInfo();
            foreach (var a in MyModels.selected_category_items.list)
            {
                itemTCInfo.guigeid = a.standardkey;
                itemTCInfo.itemid = a.itemKey;
                itemTCInfo.itemnum = a.num;
                tcList.Add(itemTCInfo);
            }
            List<Biz.BIZFoodController.ItemCrudeInfo> crudeList = new List<Biz.BIZFoodController.ItemCrudeInfo>();

            if (isStandard == 0)
            {
                Biz.BIZFoodController.ItemCrudeInfo itemCrudeInfo = new Biz.BIZFoodController.ItemCrudeInfo();
                foreach(var a in list_itemcrude)
                {
                    itemCrudeInfo.crudeid = a.crudeid.Value;
                    itemCrudeInfo.crudenum = a.num.Value;
                    itemCrudeInfo.guigeid = a.genreid.Value;
                    crudeList.Add(itemCrudeInfo);
                }
            }
            else
            {
               foreach(var a in MyModels.EidtType.list)
                {

                }
            }
        }
        private string SaveImage()
        {
            Bitmap bitmap = null;
            var temp = this.gridView1.GetRowCellValue(rowHandl("菜品图片:"), "Value");
            if (temp != null)
            {
                Type type = this.gridView1.GetRowCellValue(rowHandl("菜品图片:"), "Value").GetType();
                if (type.Name == "Bitmap")
                {
                    bitmap = (Bitmap)this.gridView1.GetRowCellValue(rowHandl("菜品图片:"), "Value");
                    return "";
                }
                else if (type.Name == "Byte[]")
                {
                    bitmap = new Bitmap(BytesToImage((byte[])this.gridView1.GetRowCellValue(rowHandl("菜品图片:"), "Value")));
                }
            }
            else
            {
                return "";
            }

            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; // 相差秒数
            string CurrentDirectory = System.Environment.CurrentDirectory;
            string path = CurrentDirectory + "\\FoodImages";
            checkDir(path);
            string strImage= timeStamp.ToString() + ".png";
            path += "\\" + strImage;
            bitmap.Save(path, ImageFormat.Png);
            bitmap.Dispose();
            return strImage;
        }
        public static Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        public static bool checkDir(string url)
        {
            try
            {
                if (!Directory.Exists(url))//如果不存在就创建file文件夹　　             　　                
                    Directory.CreateDirectory(url);//创建该文件夹　　              
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 原料设置按钮 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            //strSelected是返回值
            isStandard = Convert.ToBoolean(this.gridView1.GetRowCellValue(rowHandl("是否分规格:"), "Value"));
            if (!isStandard)
            {
                MyForm.FoodManagement.MaterialSettingForm materialSetting = new MyForm.FoodManagement.MaterialSettingForm(list_itemcrude);
                materialSetting.StartPosition = FormStartPosition.CenterScreen;
                materialSetting.ShowDialog();
                this.gridView1.SetRowCellValue(rowHandl("原料设置:"), "Value", materialSetting.strSelected);
                str_guigeNo = materialSetting.strSelected;
                this.list_itemcrude = materialSetting.list_itemcrude;
            }
            else
            {
                if (MyModels.EidtType.list.Count == 0)
                {
                    iniEditTypedata(list_itemstandard);
                }
                MyForm.FoodManagement.EidtTypeForm eidtType = new MyForm.FoodManagement.EidtTypeForm(MyModels.EidtType.list);
                eidtType.StartPosition = FormStartPosition.CenterScreen;
                eidtType.ShowDialog();
                this.gridView1.SetRowCellValue(rowHandl("规格:"), "Value", eidtType.str);
                str_guigeYse = eidtType.str;
            }
            this.gridView1.RefreshData();
        }
        private void iniEditTypedata(List<Models.item_standard> list_itemstandard)
        {
            MyModels.EidtType.list.Clear();
            if (list_itemstandard == null)
            {
                return;
            }
            foreach (var a in list_itemstandard)
            {
                string str = "";
                var b = BIZFood.QueryItemCrude(a.itemKey.Value, a.standardkey);
                Models.v_item_crude v_Item_Crude = new Models.v_item_crude();
                List<Models.v_item_crude> list_v_Item_Crude=new List<Models.v_item_crude>();
                list_v_Item_Crude.Clear();
                foreach (var c in b)
                {
                    str += (c.crudename + "*" + c.num.ToString());
                    str += ",";
                    v_Item_Crude.crudeid = c.crudeid;
                    v_Item_Crude.num = c.num;
                    list_v_Item_Crude.Add(v_Item_Crude);
                }
                if (str.Length > 0)
                {
                    str = str.Substring(0, str.Length - 1);
                }
                MyModels.EidtType._EidtType _EidtType = new MyModels.EidtType._EidtType();
                _EidtType.itemKey = a.itemKey.Value.ToString();
                _EidtType.sprice = a.sprice.Value;
                _EidtType.standardkey = a.standardkey.ToString();
                _EidtType.standardname = a.standardname;
                _EidtType.yuanliao = str;
                _EidtType.state = (a.state.Value==1)?true:false;
                _EidtType.item_standard = list_v_Item_Crude;
                MyModels.EidtType.list.Add(_EidtType);
            }
        }
        private void repositoryItemComboBox1_EditValueChanged(object sender, EventArgs e)
        {
            ComboBoxEdit comboBox = (ComboBoxEdit)sender;
            this.itemcategorykey = list_itemcategory[comboBox.SelectedIndex].itemcategorykey;
        }
        #endregion
        public void guige_yes()
        {
            int irow = rowHandl("是否分规格:");
            this.gridEditors[irow + 1].Name = "规格:";
            this.gridEditors[irow].Value = true;
            this.gridEditors[irow + 1].Value = str_guigeYse;
            this.gridView1.RefreshData();
        }
        private void repositoryItemToggleSwitch2_EditValueChanged(object sender, EventArgs e)
        {
            int irow = this.gridView1.FocusedRowHandle;
            ToggleSwitch toggleSwitch = (ToggleSwitch)sender;
            if (toggleSwitch.IsOn)
            {
                this.gridEditors[irow + 1].Name = "规格:";
                this.gridEditors[irow].Value = true;
                this.gridEditors[irow + 1].Value = str_guigeYse;


            }
            else
            {
                this.gridEditors[irow + 1].Name = "原料设置:";
                this.gridEditors[irow].Value = false;
                this.gridEditors[irow + 1].Value = str_guigeNo;
            }
            this.gridView1.RefreshData();
            
        }
        public void taocanan_yes()
        {
            int irow = rowHandl("是否是套餐:");
            GridEditorItem editorItem = new GridEditorItem(this.repositoryItemToggleSwitch1, "是否打印套餐详情:", (v_Category_Items.isprintset == 1) ? true : false);
            GridEditorItem editorItem2 = new GridEditorItem(this.repositoryItemButtonEdit2, "套餐内菜品:", "");
            this.gridEditors.Insert(irow + 2, editorItem2);
            this.gridEditors.Insert(irow + 3, editorItem);
            this.gridView1.RefreshData();
        }
            private void repositoryItemToggleSwitch3_EditValueChanged(object sender, EventArgs e)
        {
            int irow = this.gridView1.FocusedRowHandle;
            ToggleSwitch toggleSwitch = (ToggleSwitch)sender;
            if (toggleSwitch.IsOn)
            {
                // GridEditorCollection grid = new GridEditorCollection(); 
                GridEditorItem editorItem = new GridEditorItem(this.repositoryItemToggleSwitch1, "是否打印套餐详情:", (v_Category_Items.isprintset == 1) ? true : false);
                GridEditorItem editorItem2 = new GridEditorItem(this.repositoryItemButtonEdit2, "套餐内菜品:", "");
                this.gridEditors.Insert(irow + 2, editorItem2);
                this.gridEditors.Insert(irow+3, editorItem);


            }
            else
            {
                this.gridEditors.RemoveAt(irow + 2);
                this.gridEditors.RemoveAt(irow + 2);
            }
            this.gridView1.RefreshData();
        }
        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            int irow = this.gridView1.FocusedRowHandle;
            MyForm.FoodManagement.TaoCanFoodForm taoCanFood = new MyForm.FoodManagement.TaoCanFoodForm();
            taoCanFood.StartPosition = FormStartPosition.CenterScreen;
            taoCanFood.WindowState = FormWindowState.Maximized;
            taoCanFood.ShowDialog();
            string str = "";
            foreach(var a in MyModels.selected_category_items.list)
            {
                str += a.itemName;
                str += ",";
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            this.gridView1.SetRowCellValue(irow, "Value", str);
            
        }
        public void chengzhong_yes()
        {
            int irow = rowHandl("是否称重:");
            GridEditorItem editorItem = new GridEditorItem(this.repositoryItemToggleSwitch1, "是否为必选菜:", (v_Category_Items.ismust == 1) ? true : false);
            this.gridEditors.Insert(irow + 3, editorItem);
            this.gridView1.RefreshData();
        }
            private void repositoryItemToggleSwitch4_EditValueChanged(object sender, EventArgs e)
        {
            int irow = this.gridView1.FocusedRowHandle;
            ToggleSwitch toggleSwitch = (ToggleSwitch)sender;
            if (toggleSwitch.IsOn)
            {
                GridEditorItem editorItem = new GridEditorItem(this.repositoryItemToggleSwitch1, "是否为必选菜:", (v_Category_Items.ismust == 1) ? true : false);
                this.gridEditors.Insert(irow + 3, editorItem);


            }
            else
            {
                this.gridEditors.RemoveAt(irow + 3);
            }
            this.gridView1.RefreshData();
        }
    }
}
