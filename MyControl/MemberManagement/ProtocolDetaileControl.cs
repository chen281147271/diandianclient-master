using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.MemberManagement
{
    public partial class ProtocolDetaileControl : UserControl
    {
        public delegate void MyDelegate();
        public event MyDelegate CloseEvent;
        Biz.BizMemberCard memberCard = new Biz.BizMemberCard();
        int id=0;
        int type;
        public ProtocolDetaileControl(Models.dd_shop_signusers dd_Shop_Signusers,int type)//1编辑 2新增
        {
            InitializeComponent();
             this.type= type;
            if (type == 1)
            {
                id = dd_Shop_Signusers.id;
                this.Txt_maxprice.Text = dd_Shop_Signusers.maxprice.ToString();
                this.Txt_maxusenums.Text = dd_Shop_Signusers.maxusenums.ToString();
                this.Txt_name.Text = dd_Shop_Signusers.name.ToString();
                this.Txt_phone.Text = dd_Shop_Signusers.telno.ToString();
            }
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider1.Validate())
            {
                string name = this.Txt_name.Text;
                string tel = this.Txt_phone.Text;
                int maxusenums = Convert.ToInt32(this.Txt_maxusenums.Text);
                decimal maxprice = Convert.ToDecimal(this.Txt_maxprice.Text);
                if (type == 1)
                {
                    memberCard.EditSignUser(id, name, tel, maxusenums, maxprice);
                }
                else
                {
                    memberCard.AddSignUser(name, tel, maxusenums, maxprice);
                }

                this.CloseEvent();
            }
        }
    }
}
