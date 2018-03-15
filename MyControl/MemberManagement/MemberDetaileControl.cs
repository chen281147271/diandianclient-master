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
    public partial class MemberDetaileControl : UserControl
    {
        string cardid;
        public MemberDetaileControl(MyModels.ddmemcard2 ddmemcard2)
        {
            InitializeComponent();
            this.Lab_addtime.Text = ddmemcard2.addtime;
            this.Lab_birthday.Text = ddmemcard2.birthday;
            this.Lab_cardno.Text = ddmemcard2.cardno;
            this.Lab_expirydate.Text = ddmemcard2.expirydate;
            this.Lab_money.Text = ddmemcard2.money;
            this.Lab_realname.Text = ddmemcard2.realname;
            this.Lab_telno.Text = ddmemcard2.telno;
            this.cardid = ddmemcard2.cardid;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MyEvent.MemberManagement.MemberDetaileEvent.Close(this.cardid,0);
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            MyEvent.MemberManagement.MemberDetaileEvent.Close(this.cardid, 1);
        }
    }
}
