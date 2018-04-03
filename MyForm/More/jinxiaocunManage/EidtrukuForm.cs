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
    public partial class EidtrukuForm : Form
    {
        public EidtrukuForm(Models.v_depotin_crude depotinCrude, int type)
        {
            InitializeComponent();
            MyControl.More.jinxiaocunManage.EidtrukuControl eidtruku = new MyControl.More.jinxiaocunManage.EidtrukuControl(depotinCrude, type);
            eidtruku.Dock = DockStyle.Fill;
            eidtruku.MyEvent += CloseEvent;
            this.Controls.Add(eidtruku);

        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
