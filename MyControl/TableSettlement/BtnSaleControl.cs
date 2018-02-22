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
    public partial class BtnSaleControl : UserControl
    {
        //DEV textedit change事件 貌似存在BUG 在change事件中部能给自己赋值！
        public delegate void CloseEvents(int iControl, string Resoult);
        public event CloseEvents MyCloseEvents;
        public decimal price;//原价
        decimal NoSale;//不能打折的部分
        decimal Sale;//可以打折的部分
        decimal Saled;//打折后的价钱
        public int iFocus;//焦点 0 抹零 1折扣
        public int TXT1lock;//防止打折和抹零来回修改
        public int TXT2lock;//防止打折和抹零来回修改
        public BtnSaleControl(decimal price)
        {
            this.price = price;
            Saled = price;
            InitializeComponent();
            IniLabel();


            ///
            textBox1.AutoSize = false;
            textBox1.Size = new Size(217, 55);
            textBox2.AutoSize = false;
            textBox2.Size = new Size(217, 55);
        }
        public void IniLabel()
        {
            NoSale = Math.Floor(price);
            string strNoSale = NoSale.ToString();
            NoSale = Convert.ToDecimal(strNoSale.Substring(0, strNoSale.Length - 1)+"0");
            Sale = NoSale;
            NoSale = price - NoSale;
            label1.Text = "优惠(消费金额：￥"+price.ToString()+")";
            label4.Text = "（应付金额：￥"+ price.ToString() + "）";
            label5.Text = "（不参与打折金额：￥"+ NoSale.ToString() + "）";

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MyCloseEvents(1, price.ToString());
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (TXT1lock == 0)
                {
                    if (textBox1.Text == "")
                        textBox1.Text = "0";
                    if (Convert.ToDecimal(textBox1.Text) < Sale)
                    {
                        TXT2lock = 1;
                        decimal temp = Sale - Convert.ToDecimal(textBox1.Text);
                        textBox2.Text = ((temp / Sale) * 10).ToString();
                        Saled = price - Convert.ToDecimal(textBox1.Text);
                        label4.Text = "（应付金额：￥" + Saled.ToString() + "）";
                    }
                    else
                    {
                        string str = textBox1.Text;
                        str = str.Substring(0, str.Length - 1);
                        textBox1.Text = str;
                        //让文本框获取焦点 
                        this.textBox1.Focus();
                        //设置光标的位置到文本尾 
                        this.textBox1.Select(this.textBox1.TextLength, 0);
                        //滚动到控件光标处 
                        this.textBox1.ScrollToCaret();
                    }
                }
            }
            catch
            {
               
            }
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(textBox2.Text) <= 10)
                {
                    if (TXT2lock == 0)
                    {
                        TXT1lock = 1;
                        decimal temp = Sale - (Convert.ToDecimal(textBox2.Text) / 10 * Sale);
                        textBox1.Text = Math.Round(temp, 2).ToString();
                        Saled = price - Convert.ToDecimal(textBox1.Text);
                        label4.Text = "（应付金额：￥" + Saled.ToString() + "）";
                    }
                }
                else
                {
                    string str = textBox2.Text;
                    str = str.Substring(0, str.Length - 1);
                    textBox2.Text = str;
                    //让文本框获取焦点 
                    this.textBox2.Focus();
                    //设置光标的位置到文本尾 
                    this.textBox2.Select(this.textBox2.TextLength, 0);
                    //滚动到控件光标处 
                    this.textBox2.ScrollToCaret();
                }
            }
            catch
            {

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MyCloseEvents(1, Saled.ToString());
        }

        private void textEdit1_Click(object sender, EventArgs e)
        {
            iFocus = 0;
        }

        private void textEdit2_Click(object sender, EventArgs e)
        {
            iFocus = 1;
        }

        private void textEdit1_MouseClick(object sender, MouseEventArgs e)
        {
            iFocus = 0;
        }

        private void textEdit2_MouseClick(object sender, MouseEventArgs e)
        {
            iFocus = 1;
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
           TXT1lock = 0;
           TXT2lock = 0;
            //try {
            //    if (Convert.ToDecimal(textBox1.Text) >= Sale)
            //    {
            //        string str = textBox1.Text;
            //        str = str.Substring(0, str.Length - 1);
            //        textBox1.Text = "1";
            //    }

            //}
            //catch
            //{

            //}
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TXT1lock = 0;
            TXT2lock = 0;
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (textBox1.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(textBox1.Text, out oldf);
                    b2 = float.TryParse(textBox1.Text + e.KeyChar.ToString(), out f);
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TXT1lock = 0;
            TXT2lock = 0;
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (textBox2.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(textBox2.Text, out oldf);
                    b2 = float.TryParse(textBox2.Text + e.KeyChar.ToString(), out f);
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
