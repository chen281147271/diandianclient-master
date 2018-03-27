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
    public partial class PWForm : Form
    {
        public PWForm(int code)
        {
            InitializeComponent();
            MyControl.More.StaffManage.PWControl pW = new MyControl.More.StaffManage.PWControl(code);
            pW.Dock = DockStyle.Fill;
            pW.MyEvent += CloseEvent;
            this.Controls.Add(pW);
        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
