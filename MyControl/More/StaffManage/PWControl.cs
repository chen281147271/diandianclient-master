using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.StaffManage
{
    public partial class PWControl : UserControl
    {
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public PWControl(int code)
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            this.MyEvent();
        }
    }
}
