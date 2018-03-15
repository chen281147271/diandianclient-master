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
    public partial class RechargeControl : UserControl
    {
        public delegate void MyDelegate();
        public  event MyDelegate CloseEvent;
        Biz.BizMemberCard MemberCard = new Biz.BizMemberCard();
        string cardid;
        public RechargeControl(MyModels.ddmemcard2 ddmemcard2)
        {
            InitializeComponent();
            this.Lab_money.Text = ddmemcard2.money;
            this.cardid = ddmemcard2.cardid;
            this.Txt_money.Text= "0";
            this.Txt_songmoney.Text = "0";

        }

        private void Btn_query_Click(object sender, EventArgs e)
        {
            MemberCard.Rechange(Convert.ToInt32(this.cardid), Convert.ToDecimal(Txt_money.Text), Convert.ToDecimal(Txt_songmoney.Text));
            this.CloseEvent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.CloseEvent();
        }
    }
}
