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
    public partial class zongControl : UserControl
    {
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        List<Models.v_depotout_crude> list;
        public zongControl()
        {
            InitializeComponent();
            IniDate();
            list=BizStorage.QueryDepotOut("", "", "", 0, Convert.ToDateTime(this.de_stime.Text), Convert.ToDateTime(this.de_etime.Text));
        }
        private void IniDate()
        {
            de_stime.Text = "2017 - 02 - 27";
            //this.de_stime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.de_etime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
