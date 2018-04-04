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
    public partial class EdityuanliaoTypeForm : Form
    {
        public EdityuanliaoTypeForm()
        {
            InitializeComponent();
            MyControl.More.jinxiaocunManage.EdityuanliaoTypeControl edityuanliaoType = new MyControl.More.jinxiaocunManage.EdityuanliaoTypeControl();
            edityuanliaoType.Dock = DockStyle.Fill;
            this.Controls.Add(edityuanliaoType);
        }
    }
}
