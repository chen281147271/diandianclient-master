using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More
{
    public partial class MoreControl : UserControl
    {
        public MoreControl()
        {
            InitializeComponent();
        }

        private void tileControl1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            MyEvent.More.MoreEvent.Replace(e.Item.Id);
        }
    }
}
