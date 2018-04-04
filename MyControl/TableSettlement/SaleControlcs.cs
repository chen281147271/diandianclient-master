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
    public partial class SaleControlcs : UserControl
    {
        //需要提供下面这个写
        public decimal price = 100.11M;//应收款
        public decimal SalePrice = 0;
        public delegate void CloseEvents();
        public event CloseEvents MyCloseEvents;
        public decimal Price;

        public SaleControlcs()
        {
            InitializeComponent();
            this.simpleButton1.Click += new System.EventHandler(this.Btn_Click);
            this.simpleButton2.Click += new System.EventHandler(this.Btn_Click);
            this.simpleButton3.Click += new System.EventHandler(this.Btn_Click);
            this.simpleButton4.Click += new System.EventHandler(this.Btn_Click);
            this.simpleButton5.Click += new System.EventHandler(this.Btn_Click);
            this.simpleButton6.Click += new System.EventHandler(this.Btn_Click);
            this.simpleButton7.Click += new System.EventHandler(this.Btn_Click);

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton Btn = sender as DevExpress.XtraEditors.SimpleButton;
            int isale = Convert.ToInt32(Btn.Text.ToString().Substring(0, Btn.Text.ToString().Length - 1));
            switch (isale)
            {
                case 90:
                    Txt_Price.Text = (Price * 0.9M).ToString();
                    break;
                case 80:
                    Txt_Price.Text = (Price * 0.8M).ToString();
                    break;
                case 70:
                    Txt_Price.Text = (Price * 0.7M).ToString();
                    break;
                case 60:
                    Txt_Price.Text = (Price * 0.6M).ToString();
                    break;
                case 50:
                    Txt_Price.Text = (Price * 0.5M).ToString();
                    break;
                case 40:
                    Txt_Price.Text = (Price * 0.4M).ToString();
                    break;
                case 30:
                    Txt_Price.Text = (Price * 0.3M).ToString();
                    break;
            }
        }

        private void Btn_Custom_Click(object sender, EventArgs e)
        {
            if (Btn_Custom.Text.Length == 0)
            {
                Btn_Custom.Text = 20.ToString();
            }
            Txt_Price.Text = (Price * Convert.ToDecimal(Btn_Custom.Text)*0.01M).ToString();
        }
        private void Btn_Custom_EditValueChanged(object sender, EventArgs e)
        {
            if (Btn_Custom.Text.Length != 0)
                Txt_Price.Text = (Price * Convert.ToDecimal(Btn_Custom.Text) * 0.01M).ToString();
        }

        private void Btn_Submit_Click(object sender, EventArgs e)
        {
            if(Txt_Price.Text.Length!=0)
            Price = Convert.ToDecimal(Txt_Price.Text);
            MyCloseEvents();
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            MyCloseEvents();
        }
    }
}
