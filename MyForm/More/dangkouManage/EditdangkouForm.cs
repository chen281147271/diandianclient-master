using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.More.dangkouManage
{
    public partial class EditdangkouForm : DevExpress.XtraEditors.XtraForm
    {
        public EditdangkouForm(Models.dd_shop_windows shopwindows, int OpType)
        {
            InitializeComponent();
            MyControl.More.dangkouManage.EditdangkouControl editdangkou = new MyControl.More.dangkouManage.EditdangkouControl(shopwindows, OpType);
            editdangkou.Dock = DockStyle.Fill;
            editdangkou.MyEvent += CloseEvent;
            this.Controls.Add(editdangkou);
        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
