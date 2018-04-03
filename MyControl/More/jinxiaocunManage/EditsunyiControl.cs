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
    public partial class EditsunyiControl : UserControl
    {
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        List<Models.storage_genre> list_storagegenre;
        int genreid=0;
        List<Models.storage_crude> list_storagecrude;
        int crudeid = 0;
        public delegate void MyDelegate();
        public event MyDelegate MyEnent;
        public EditsunyiControl()
        {
            InitializeComponent();
            iniCob();
        }
        private void iniCob()
        {
            list_storagegenre = BizStorage.QueryGenre();
            cbo_Type.Properties.Items.Add("全部");
            foreach (var a in list_storagegenre)
            {
                cbo_Type.Properties.Items.Add(a.genrename);
            }
            cbo_Type.SelectedIndex = 0;
            cbo_sunyi.Text = "损";
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(this.txt_num.Text);
            string reason = txt_yuanyin.Text;
            int itype = (cbo_Type.Text == "损") ? 1 : 2;
            sbyte type = Convert.ToSByte(itype);
            BizStorage.AddLossOrSpillInfo(this.crudeid, num, type, reason);
            this.MyEnent();
        }

        private void cbo_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.genreid = (cbo_Type.SelectedIndex == 0) ? 0 : list_storagegenre[cbo_Type.SelectedIndex - 1].genreid;
            QueryCrude(genreid);
        }
        private void QueryCrude(int genreid)
        {
            cbo_name.Properties.Items.Clear();
            list_storagecrude =(genreid==0)? BizStorage.QueryCrude("", 0):BizStorage.QueryCrude("", genreid);
            foreach(var a in list_storagecrude)
            {
                cbo_name.Properties.Items.Add(a.crudename);
            }
            cbo_name.SelectedIndex = 0;


        }

        private void cbo_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.crudeid = list_storagecrude[cbo_name.SelectedIndex].crudeid;
        }
    }
}
