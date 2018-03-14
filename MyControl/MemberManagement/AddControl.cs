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
    public partial class AddControl : UserControl
    {
        Biz.BizMemberCard memberCard = new Biz.BizMemberCard();
        public delegate void MyDelegate();
        public event MyDelegate CloseEvent;
        public AddControl()
        {
            InitializeComponent();
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider1.Validate())
                return;
            string cardno = (Txt_cardid.Text== "不填时系统自动生成")?"": Txt_cardid.Text;
            string birthday = dt_birthday.Text;
            string name = Txt_Name.Text;
            string phone = Txt_phone.Text;
            int sex = (rad_sex.SelectedIndex == 0) ? 0 : 1;
            decimal money = Convert.ToDecimal(Txt_money.Text);
            decimal songmoney = Convert.ToDecimal(Txt_songmoney.Text);

            memberCard.AddMember(cardno, name, phone, birthday, sex, money, songmoney);

            this.CloseEvent();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.CloseEvent();
        }
    }
}
