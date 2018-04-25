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
    public partial class EditkuncunForm : DevExpress.XtraEditors.XtraForm
    {
        public EditkuncunForm(int crudeid, DateTime valida)
        {
            InitializeComponent();
            MyControl.More.jinxiaocunManage.EditkuncunControl editkuncun = new MyControl.More.jinxiaocunManage.EditkuncunControl(crudeid,valida);
            editkuncun.Dock = DockStyle.Fill;
            editkuncun.MyEvent += CloseEvent;
            this.Controls.Add(editkuncun);

        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
