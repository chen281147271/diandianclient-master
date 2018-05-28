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
    public partial class QuitForm : DevExpress.XtraEditors.XtraForm
    {
        MyForm.JiaoBan.jiaobanForm jiaoban = new jiaobanForm();
        public QuitForm()
        {
            InitializeComponent();
            iniData();
        }
        private void iniData()
        {
            this.lab_uname.Text = MyModels.userinfo.user.uname;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           // MyForm.JiaoBan.jiaobanForm jiaoban = new jiaobanForm();
            jiaoban.StartPosition = FormStartPosition.CenterScreen;
            jiaoban.ShowDialog();
        }

        private void btn_close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
