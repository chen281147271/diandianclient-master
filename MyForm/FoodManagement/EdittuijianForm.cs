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
    public partial class EdittuijianForm : DevExpress.XtraEditors.XtraForm
    {
        Models.dd_tuijian dd_Tuijian = new Models.dd_tuijian();
        Biz.BIZFoodController BIZFood = new Biz.BIZFoodController();
        public EdittuijianForm(Models.dd_tuijian dd_Tuijian)
        {
            this.dd_Tuijian = dd_Tuijian;
            InitializeComponent();
            iniData();
        }
        private void iniData()
        {
            MyModels.selected_category_items.list.Clear();
            txt_item.Text = this.dd_Tuijian.items;
            txt_num.Text = this.dd_Tuijian.peoplenum.ToString();
            txt_liyou.Text = this.dd_Tuijian.liyou;
        }

        private void btn_canel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            int tjkey = dd_Tuijian.tjid;
            string items = txt_item.Text;
            int afternum =Convert.ToInt32(txt_num.Text);
            string liyou = txt_liyou.Text;
            List<Biz.BIZFoodController.itemTuijian> tuijianList = new List<Biz.BIZFoodController.itemTuijian>();
            foreach(var a in MyModels.selected_category_items.list)
            {
                Biz.BIZFoodController.itemTuijian itemTuijian = new Biz.BIZFoodController.itemTuijian();
                itemTuijian.guigeid = a.standardkey;
                itemTuijian.itemkey = a.itemKey;
                itemTuijian.name = a.itemName;
                itemTuijian.num = a.num;
                itemTuijian.price = a.sprice;
                itemTuijian.thumb = a.itemImgs;
                tuijianList.Add(itemTuijian);
            }
            BIZFood.SaveTuijian(tjkey, items, afternum, liyou, tuijianList);
            this.Close();
        }

        private void txt_item_Click(object sender, EventArgs e)
        {
            if (MyModels.selected_category_items.list.Count == 0)
            {
                var v_category_items = BIZFood.GetFoodList(0);
                var item_standard = BIZFood.QueryStandards(-1);
                var a = BIZFood.QueryTuijianLinkList(this.dd_Tuijian.tjid);
                foreach (var b in a)
                {
                    MyModels.selected_category_items._selected_category_items _Selected_Category_Items = new MyModels.selected_category_items._selected_category_items();
                    _Selected_Category_Items.itemcategorykey = v_category_items.Find(o => o.itemkey == b.itemkey).itemcategorykey.Value;
                    _Selected_Category_Items.itemImgs = b.thumb;
                    _Selected_Category_Items.itemKey = b.itemkey.Value;
                    _Selected_Category_Items.itemName = b.name;
                    _Selected_Category_Items.num = b.num.Value;
                    _Selected_Category_Items.sprice = b.price.Value;
                    _Selected_Category_Items.standardkey = b.guigeid.Value;
                    _Selected_Category_Items.standardname = item_standard.Where(o => o.standardkey == b.guigeid.Value && o.itemKey == b.itemkey.Value).FirstOrDefault().standardname;
                    MyModels.selected_category_items.list.Add(_Selected_Category_Items);
                }
            }
            MyForm.FoodManagement.TaoCanFoodForm taoCanFood = new TaoCanFoodForm();
            taoCanFood.WindowState = FormWindowState.Maximized;
            taoCanFood.ShowDialog();

            string str = "";
            foreach (var c in MyModels.selected_category_items.list)
            {
                str += c.itemName;
                str += ",";
            }
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 1);
            }
            txt_item.Text = str;
        }
    }
}
