using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.JiaoBan
{
    public partial class jiaobanForm : DevExpress.XtraEditors.XtraForm
    {
        public jiaobanForm()
        {
            InitializeComponent();
            lab_uname.Text = MyModels.userinfo.user.uname;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
