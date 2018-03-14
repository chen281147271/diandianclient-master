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
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
            MyControl.MemberManagement.AddControl add = new MyControl.MemberManagement.AddControl();
            add.Dock = DockStyle.Fill;
            add.CloseEvent += MyCloseEvent;
            this.Controls.Add(add);
        }
        private void MyCloseEvent()
        {
            this.Close();
        }
    }
}
