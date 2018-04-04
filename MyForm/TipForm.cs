using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm
{
    public partial class TipForm : Form
    {
        public TipForm(string title,string msg)
        {
            InitializeComponent();
            lab_titile.Text = title;
            lab_msg.Text = msg;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            MyModels.TipMsg.list.RemoveAt(0);
            this.Close();
        }
    }
}
