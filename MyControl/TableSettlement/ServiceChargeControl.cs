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
    public partial class ServiceChargeControl : UserControl
    {
        public delegate void CloseEvents(int iControl, string resoult);
        public event CloseEvents MyCloseEvents;
        decimal price;
        public ServiceChargeControl(decimal price)
        {
            this.price = price;
            InitializeComponent();
            this.simpleButton3.Click += new System.EventHandler(this.BtnNum_Click);
            this.simpleButton4.Click += new System.EventHandler(this.BtnNum_Click);
            this.simpleButton5.Click += new System.EventHandler(this.BtnNum_Click);
            this.simpleButton6.Click += new System.EventHandler(this.BtnNum_Click);
            this.simpleButton7.Click += new System.EventHandler(this.BtnNum_Click);
            this.simpleButton8.Click += new System.EventHandler(this.BtnNum_Click);
            simpleButton1.PerformClick();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            MyCloseEvents(3, "0");
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            if (Txt_Price.Text.Length == 0)
                Txt_Price.Text = "0";
            MyCloseEvents(3,Txt_Price.Text);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.simpleButton3.Text = "1元";
            this.simpleButton4.Text = "2元";
            this.simpleButton5.Text = "5元";
            this.simpleButton6.Text = "10元";
            this.simpleButton7.Text = "15元";
            this.simpleButton8.Text = "20元";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.simpleButton3.Text = "1%";
            this.simpleButton4.Text = "2%";
            this.simpleButton5.Text = "5%";
            this.simpleButton6.Text = "10%";
            this.simpleButton7.Text = "15%";
            this.simpleButton8.Text = "20%";
        }

        private void BtnNum_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton simpleButton = (DevExpress.XtraEditors.SimpleButton)sender;
            string str = simpleButton.Text;
            decimal i = Convert.ToDecimal(str.Substring(0, str.Length - 1));
            bool PrecentOrNum;//ture % false 元
            if (str.IndexOf('%') > 0)
                PrecentOrNum = true;
            else
                PrecentOrNum = false;
            if (PrecentOrNum)
                this.Txt_Price.Text = (price * i / 100).ToString();
            else
                this.Txt_Price.Text = i.ToString();
        }
        private void Txt_Price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (Txt_Price.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(Txt_Price.Text, out oldf);
                    b2 = float.TryParse(Txt_Price.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }
    }
}
