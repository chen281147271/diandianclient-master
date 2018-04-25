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
    public partial class ProtocolDetaileForm : DevExpress.XtraEditors.XtraForm
    {
        public ProtocolDetaileForm(Models.dd_shop_signusers dd_Shop_Signusers,int type)
        {
            InitializeComponent();
            MyControl.MemberManagement.ProtocolDetaileControl protocolDetaile = new MyControl.MemberManagement.ProtocolDetaileControl(dd_Shop_Signusers,type);
            protocolDetaile.Dock = DockStyle.Fill;
            protocolDetaile.CloseEvent += MyCloseEvent;
            this.Controls.Add(protocolDetaile);
        }
        private void MyCloseEvent()
        {
            this.Close();
        }
    }
}
