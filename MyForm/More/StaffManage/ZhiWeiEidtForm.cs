using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.More.StaffManage
{
    public partial class ZhiWeiEidtForm : Form
    {
        public ZhiWeiEidtForm(int sysroleid, string rolename, int OpType)
        {
            InitializeComponent();
            MyControl.More.StaffManage.ZhiWeiEidtControl zhiWeiEidt = new MyControl.More.StaffManage.ZhiWeiEidtControl(sysroleid, rolename,OpType);
            zhiWeiEidt.Dock = DockStyle.Fill;
            zhiWeiEidt.Myevent += CloseEvent;
            this.Controls.Add(zhiWeiEidt);
        }
        private void CloseEvent()
        {
            this.Close();
        }

    }
}
