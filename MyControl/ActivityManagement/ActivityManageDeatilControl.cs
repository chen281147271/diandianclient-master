using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;

namespace DianDianClient.MyControl.ActivityManagement
{
    public partial class ActivityManageDeatilControl : UserControl
    {
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        Biz.BizPromotionActivities PromotionActivities = new Biz.BizPromotionActivities();
        public Models.dd_coupons list;
        int couponid;
        int isonlineok;
        public ActivityManageDeatilControl( Models.dd_coupons list)
        {
            InitializeComponent();
            this.list = list;
            if(list!=null)
            IniData(list.couponid,list.isonlineok,list.cname, list.sdate, list.edate, list.minmoney.ToString(), list.okjian.ToString(), list.extendway, list.ifmoney.ToString(), list.istogether, list.memtogether);
        }
        void IniData(int couponid,int? isonlineok, string came,string sdate,string edate,string minmoney,string okjian,string extendway,string ifmoney,string istogether,string memtogether)
        {
            this.couponid = couponid;
            this.isonlineok = Convert.ToInt32(isonlineok);
            this.Txt_cname.Text = came;
            this.dae_sdate.Text = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(sdate));
            this.dae_edate.Text = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(edate));
            if (extendway=="0")
                this.radio_extendway.SelectedIndex=0;
            else if(extendway == "1")
                this.radio_extendway.SelectedIndex = 1;
            this.Txt_minmoney.Text = minmoney;
            this.Txt_okjian.Text = okjian;
            this.Txt_ifmoney.Text = ifmoney;
            if (istogether == "0")
            {
                this.istogether.IsOn = false;
            }
            else
            {
                this.istogether.IsOn = true;
            }
            if (memtogether == "0")
            {
                this.memtogether.IsOn = false;
            }
            else
            {
                this.memtogether.IsOn = true;
            }
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (this.dxValidationProvider1.Validate())
            {

                DateTime sdate = Convert.ToDateTime(this.dae_sdate.Text);
                DateTime edate = Convert.ToDateTime(this.dae_edate.Text);
                if (sdate > edate)
                {
                    Utils.utils.ShowTip("提示", "活动结束时间必须大于开始时间");
                  //  Utils.utils.ShowMessageBox("需要弹出的信息","标题可以不填默认是提示");
                    return;
                }
                string cname = this.Txt_cname.Text;
                decimal minmoney = Convert.ToDecimal(this.Txt_minmoney.Text);
                int okjian = Convert.ToInt32(this.Txt_okjian.Text);
                string extendway = radio_extendway.SelectedIndex.ToString();
                int ifmoney = Convert.ToInt32(this.Txt_ifmoney.Text);
                string istogether = this.istogether.IsOn ? "1" : "0";
                string memtogether = this.memtogether.IsOn ? "1" : "0";
                if (list != null)
                    PromotionActivities.EditActivity(this.couponid, cname, sdate, edate, istogether, memtogether, this.isonlineok, ifmoney, okjian, extendway, minmoney);
                else
                    PromotionActivities.AddActivity(cname, sdate, edate, istogether, memtogether, this.isonlineok, ifmoney, okjian, extendway, minmoney);
                MyEvent();
            }

        }
    }
}
