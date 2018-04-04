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
    public partial class EdityuanliaoForm : Form
    {
        public EdityuanliaoForm()
        {
            InitializeComponent();
            MyControl.More.jinxiaocunManage.EdityuanliaoControl edityuanliao = new MyControl.More.jinxiaocunManage.EdityuanliaoControl();
            edityuanliao.Dock = DockStyle.Fill;
            edityuanliao.MyEvent += CloseEvent;
            this.Controls.Add(edityuanliao);
        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
