using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DianDianClient.MyForm.TableSettlement
{
    public partial class TuiCai : DevExpress.XtraEditors.XtraForm
    {
        public TuiCai(string strNumber)
        {
            InitializeComponent();
            inistrNumber(strNumber);
            TuiCaiControl1.MyCloseEvents += MyCloseEvents;
        }
        private void inistrNumber(string strNumber)
        {
            TuiCaiControl1.strNumber.Text = "已点"+strNumber+"个";
           // TuiCaiControl1.strNumber.Tag = strNumber;
            TuiCaiControl1.spinEdit1.Properties.MaxValue = Convert.ToInt32(strNumber);
            TuiCaiControl1.spinEdit1.Properties.MinValue =1;
        }
        private void MyCloseEvents()
        {
            this.Close();

        }
    }
}
