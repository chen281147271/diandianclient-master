using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.MemberManagement
{
    public partial class RechargeForm : DevExpress.XtraEditors.XtraForm
    {
        public RechargeForm(MyModels.ddmemcard2 ddmemcard2)
        {
            MyControl.MemberManagement.RechargeControl recharge = new MyControl.MemberManagement.RechargeControl(ddmemcard2);
            recharge.CloseEvent += MyCloseEvent;
            recharge.Dock = DockStyle.Fill;
            this.Controls.Add(recharge);
            InitializeComponent();
        }
        private void MyCloseEvent()
        {
            this.Close();
        }
    }
}
