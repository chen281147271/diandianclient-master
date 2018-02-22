using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DianDianClient.MyControl.TableSettlement
{
    public partial class QianDanControl : UserControl
    {
        public delegate void CloseEvents(int iControl, string resoult);
        public event CloseEvents MyCloseEvents;
        public bool QiandanOrVIP;//ture 签单 false会员买单
        public QianDanControl()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(QiandanOrVIP)
                MyCloseEvents(4,"");
            else
                MyCloseEvents(5, "");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (QiandanOrVIP)
                MyCloseEvents(4, this.textEdit1.Text);
            else
                MyCloseEvents(5, this.textEdit1.Text);
        }
    }
}
