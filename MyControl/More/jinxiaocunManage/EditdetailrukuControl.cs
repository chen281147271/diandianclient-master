using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.jinxiaocunManage
{
    public partial class EditdetailrukuControl : UserControl
    {
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        List<Models.storage_genre> list_storagegenre;
        List<Models.v_crude_genre> list_storagecrude;
        int genreid = 0;
        int crudeid = 0;
        public delegate void Mydelegate();
        public event Mydelegate MyEvent;
        int depotinid = 0;
        public EditdetailrukuControl(int depotinid)
        {
            InitializeComponent();
            iniCombobox();
            this.depotinid = depotinid;
        }
        private void iniCombobox()
        {
            list_storagegenre = BizStorage.QueryGenre();
            foreach (var b in list_storagegenre)
            {
                this.cbo_Type.Properties.Items.Add(b.genrename);
            }
            this.cbo_Type.SelectedIndex = 0;
            //list_storagecrude =BizStorage.QueryCrude("", this.genreid);
            //foreach(var b in list_storagecrude)
            //{
            //    this.cbo_yuanliaoname.Properties.Items.Add(b.crudename);
            //}
            //this.cbo_yuanliaoname.SelectedIndex = 0;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider1.Validate())
                return;
            int genreid = this.genreid;
            int crudeid = this.crudeid;
            decimal cost =Convert.ToDecimal(this.txt_sumprice.Text);
            int num = Convert.ToInt32(this.txt_num.Text);
            DateTime? validity = Convert.ToDateTime(this.de_baozhiqi.Text);
            DateTime? productiondate = Convert.ToDateTime(this.de_shenchan.Text);
            DateTime? backdate = Convert.ToDateTime(this.de_tuihuan.Text);
            string maker = txt_shengchanfactory.Text;
            string remarks = txt_beizhu.Text;
            string supplier = "";
            BizStorage.AddDepotInInfo(this.depotinid,genreid, crudeid, cost,num,validity,productiondate,backdate,maker,remarks,supplier);
            this.MyEvent();
        }

        private void cbo_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.genreid = list_storagegenre[cbo_Type.SelectedIndex].genreid;
            list_storagecrude = BizStorage.QueryCrude("", this.genreid,"","");
            this.cbo_yuanliaoname.Properties.Items.Clear();
            foreach (var b in list_storagecrude)
            {
                this.cbo_yuanliaoname.Properties.Items.Add(b.crudename);
            }
            this.cbo_yuanliaoname.SelectedIndex = 0;
        }

        private void cbo_yuanliaoname_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.crudeid = list_storagecrude[cbo_yuanliaoname.SelectedIndex].crudeid;
        }
        private void GetPrice()
        {
            if (txt_sumprice.Text.Length != 0 && txt_num.Text.Length != 0 && Convert.ToDecimal(txt_num.Text) != 0)
            {
                decimal temp = Convert.ToDecimal(txt_sumprice.Text) / Convert.ToDecimal(txt_num.Text);
                txt_price.Text = temp.ToString();
            }
            else
            {
                txt_price.Text = "0";
            }
        }
        private void txt_num_EditValueChanged(object sender, EventArgs e)
        {
            GetPrice();
        }

        private void txt_sumprice_EditValueChanged(object sender, EventArgs e)
        {
            GetPrice();
        }
    }
}
