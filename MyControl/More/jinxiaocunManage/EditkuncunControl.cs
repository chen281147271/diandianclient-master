using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.jinxiaocunManage
{
    public partial class EditkuncunControl : UserControl
    {
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        int crudeid;
        DateTime validate;
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public EditkuncunControl(int crudeid, DateTime validate)
        {
            InitializeComponent();
            this.crudeid=crudeid;
            this.validate = validate;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            DateTime changedate = Convert.ToDateTime(this.de_baozhiqi.Text);
            BizStorage.StockModifyValidate(crudeid, validate,changedate);
            this.MyEvent();
        }
    }
}
