using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.More.cantingSetUp
{
    public partial class EditPayWayNameForm : Form
    {
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        int id;
        List<Models.dd_shop_payway> list;
        int Type;
        public EditPayWayNameForm(int id,string PayWay, List<Models.dd_shop_payway> list,int Type)
        {
            InitializeComponent();
            this.list = list;
            this.Type = Type;
            this.id = id;
            if (Type == 1)
            {
                this.textEdit1.Text = PayWay;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider1.Validate())
                return;
            if (list.Where(o => o.payway.Equals(textEdit1.Text)).Count() != 0)
            {
                Utils.utils.ShowMessageBox("该支付方式已存在!");
            }
            else if(Type==1)
            {
                BizSPInfo.EditPayWayName(id, textEdit1.Text);
                this.Close();
            }
            else
            {
                BizSPInfo.EditPayWayName(-1, textEdit1.Text);
                this.Close();
            }
        }
    }
}
