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
    public partial class EditdetailrukuForm : DevExpress.XtraEditors.XtraForm
    {
        public EditdetailrukuForm(int depotinid)
        {
            InitializeComponent();
            MyControl.More.jinxiaocunManage.EditdetailrukuControl editdetailruku = new MyControl.More.jinxiaocunManage.EditdetailrukuControl(depotinid);
            editdetailruku.Dock = DockStyle.Fill;
            editdetailruku.MyEvent += CloseEvent;
            this.Controls.Add(editdetailruku);
        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
