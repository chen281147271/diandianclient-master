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
    public partial class MemberDetaileForm : DevExpress.XtraEditors.XtraForm
    {
        public MemberDetaileForm(MyModels.ddmemcard2 ddmemcard2)
        {
            MyControl.MemberManagement.MemberDetaileControl memberDetaile = new MyControl.MemberManagement.MemberDetaileControl(ddmemcard2);
            memberDetaile.Dock = DockStyle.Fill;
            this.Controls.Add(memberDetaile);
            InitializeComponent();

            MyEvent.MemberManagement.MemberDetaileEvent.CloseEvent += MyCloseEvent;
        }
        private void MyCloseEvent(string cardid, int type)
        {
            this.Close();
        }
    }
}
