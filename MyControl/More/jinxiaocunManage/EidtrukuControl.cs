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
    public partial class EidtrukuControl : UserControl
    {
        public delegate void Mydelegate();
        public event Mydelegate MyEvent;
        Models.v_depotin_crude depotinCrude;
        int? depotinid=0;
        int type;
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        public EidtrukuControl(Models.v_depotin_crude depotinCrude,int type)
        {
            InitializeComponent();
            this.depotinCrude = depotinCrude;
            if (type == 1)
            {
                inidata();
            }
            this.type = type;
        }
        private void inidata()
        {
            txt_CarNo.Text = depotinCrude.platenum;
            txt_duijieren.Text = depotinCrude.dutyperson;
            txt_jiehuoren.Text = depotinCrude.deliveryman;
            txt_phone.Text = depotinCrude.tel;
            de_rukutime.Text = depotinCrude.productiondate.ToString();
            txt_songhuosiji.Text = depotinCrude.driver;
            txt_sumprice.Text = depotinCrude.cost.ToString();
            this.depotinid = depotinCrude.depotinid;
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            string platenum = txt_CarNo.Text;
            string dutyperson = txt_duijieren.Text;
            string deliveryman = txt_jiehuoren.Text;
            string tel = txt_phone.Text;
            string productiondate = de_rukutime.Text;
            string driver = txt_songhuosiji.Text;
            decimal cost =Convert.ToDecimal(txt_songhuosiji.Text);
            DateTime dt = Convert.ToDateTime(this.de_rukutime.Text);
            if (this.type == 2)
            {
                BizStorage.AddDepotIn(-1, dt,cost, dutyperson, deliveryman, tel, driver, platenum);
            }else if (this.type == 1)
            {
                BizStorage.AddDepotIn(Convert.ToInt32(this.depotinid), dt, cost, dutyperson, deliveryman, tel, driver, platenum);
            }
            this.MyEvent();
        }
    }
}
