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
    public partial class AddDetaileForm : Form
    {
        public AddDetaileForm()
        {
            InitializeComponent();
            MyControl.MemberManagement.AddDetaileControl addDetaile = new MyControl.MemberManagement.AddDetaileControl();
            addDetaile.Dock = DockStyle.Fill;
            addDetaile.CloseEvent += MyCloseEvent;
            this.Controls.Add(addDetaile);

        }
        private void MyCloseEvent()
        {
            this.Close();
        }
    }
}
