using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.More.jinxiaocunManage
{
    public partial class EditsunyiForm : Form
    {
        public EditsunyiForm()
        {
            InitializeComponent();
            MyControl.More.jinxiaocunManage.EditsunyiControl editsunyi = new MyControl.More.jinxiaocunManage.EditsunyiControl();
            editsunyi.Dock = DockStyle.Fill;
            editsunyi.MyEnent += CloseEvent;
            this.Controls.Add(editsunyi);
        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
