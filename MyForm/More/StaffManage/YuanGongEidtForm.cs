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
    public partial class YuanGongEidtForm : DevExpress.XtraEditors.XtraForm
    {
        public YuanGongEidtForm(int memberkey, int sysroleid, string rolename, int code, string name, int Optype)
        {
            InitializeComponent();
            MyControl.More.StaffManage.YuanGongEidtControl yuanGongEidt = new MyControl.More.StaffManage.YuanGongEidtControl(memberkey, sysroleid, rolename,code,name, Optype);
            yuanGongEidt.Dock = DockStyle.Fill;
            yuanGongEidt.MyEvent += CloseEvent;
            this.Controls.Add(yuanGongEidt);
        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
