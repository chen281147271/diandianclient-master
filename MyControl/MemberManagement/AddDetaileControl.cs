using System;
using System.Windows.Forms;

namespace DianDianClient.MyControl.MemberManagement
{
    public partial class AddDetaileControl : UserControl
    {
        Biz.BizMemberCard memberCard = new Biz.BizMemberCard();
        public delegate void MyDelegate();
        public event MyDelegate CloseEvent;
        public AddDetaileControl()
        {
            InitializeComponent();
            this.Txt_money.Text = "0";
            this.Txt_rmoney.Text = "0";
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider1.Validate())
                return;
            memberCard.AddMemberRule(Txt_name.Text,Convert.ToInt32(Txt_rmoney.Text),Convert.ToInt32(Txt_money.Text));
            this.CloseEvent();
        }
    }
}
