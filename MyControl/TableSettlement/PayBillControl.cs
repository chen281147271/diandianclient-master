using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DianDianClient.MyControl.TableSettlement
{
    public partial class PayBillControl : UserControl
    {
        public decimal price=105.5m;//应收金额
        public decimal Saleprice = 0;//优惠金额
        public decimal ServiceCharge = 0;//服务金额
        public string TableNo = "6";//桌号
        public string EatNo = "2";//用餐编号
        string QianDanPhoneNo = "";//签单号
        string VIPNo = "";//会员买单号
        public DataTable MenuTable;//菜单
        DataTable dt= new DataTable();
        MyControl.TableSettlement.BtnSaleControl btnSaleControl;
        MyControl.TableSettlement.TuiCaiGridControl tuiCaiGridControl;
        MyControl.TableSettlement.ServiceChargeControl serviceChargeControl;
        MyControl.TableSettlement.QianDanControl qianDanControl;
        int ActivityControl = 0;//键盘输入控件绑定  0-主界面 1优惠界面
        public PayBillControl()
        {
            InitializeComponent();
            // IniGrid();
            IniLab();
            BindNumEvet();
            BindBtnEvet();
            IniControl();
            dt.Columns.Add("PaymentID", typeof(Int32));
            dt.Columns.Add("Payment", typeof(String));
            //  int iID = 0;
            for (int i = 0; i < 10; i++)
            {
                string str = i + "号付款方式";
                dt.Rows.Add(new object[] { i,str });
            }
            IniPaymentLayout(dt);
            Btn_ActualPrice.Text = (price + ServiceCharge - Saleprice).ToString();
        }
        //    private void IniGrid()
        //    {
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("Payment", typeof(String));
        //        //  int iID = 0;
        //        for (int i = 0; i < 15; i++)
        //        {
        //            string str = i + "号付款方式";
        //            dt.Rows.Add(new object[] { str });
        //        }
        //        gridControl1.DataSource = dt.DefaultView;
        //    }
        private void IniPaymentLayout(DataTable dataTable)
        {
          //  TableLayoutPanel tableLayoutPanel=new TableLayoutPanel ();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            //暂定一行最多8个
            int iDataRow = 0;
            int iCount = dataTable.Rows.Count;
            int iColumn = 5;
            int iRow=iCount/ iColumn;
            if (iCount % iColumn > 0)
                iRow += 1;
            this.tableLayoutPanel4.ColumnCount = iColumn;
            float ColumnPercent = 100 / Convert.ToSingle(iColumn);
            this.tableLayoutPanel4.ColumnStyles.Clear();
            for (int i = 0; i < iColumn; i++)
            {
                this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, ColumnPercent));
            }
            this.tableLayoutPanel4.RowCount = iRow;
            for (int i = 0; i < iRow; i++)
            {
                this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100/iRow));
            }

            DevExpress.XtraEditors.ButtonEdit buttonEdit;
            for(int i = 0; i < iRow; i++)
            {
                for (int k = 0; k < iColumn; k++)
                {
                    iDataRow = i * iColumn + k;
                    if (iCount > iDataRow)
                    {
                        buttonEdit = new DevExpress.XtraEditors.ButtonEdit();
                         buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
                       // buttonEdit.Dock = DockStyle.Fill;
                        buttonEdit.Properties.AutoHeight = false;
                        buttonEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                        buttonEdit.Size = new Size(163,60);
                        buttonEdit.Properties.Appearance.BackColor = System.Drawing.Color.White;
                        buttonEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        buttonEdit.Properties.Appearance.Options.UseBackColor = true;
                        buttonEdit.Properties.Appearance.Options.UseFont = true;
                       // buttonEdit.Properties.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                        buttonEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                        buttonEdit.Text = dataTable.Rows[i * iColumn + k]["Payment"].ToString();
                        buttonEdit.Name = "BtnName_" + dataTable.Rows[i * iColumn + k]["PaymentID"].ToString();
                        buttonEdit.Properties.Buttons.Clear();
                        buttonEdit.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        buttonEdit.Click += Btn_Click;
                        this.tableLayoutPanel4.Controls.Add(buttonEdit, k, i);
                    }
                }
            }
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        private void Btn_Click(object send,EventArgs e)
        {
            //DevExpress.XtraEditors.SimpleButton simpleButton = (DevExpress.XtraEditors.SimpleButton)send;
            //MessageBox.Show(simpleButton.Text);
            AllButtonClear(dt);
            DevExpress.XtraEditors.ButtonEdit buttonEdit = (DevExpress.XtraEditors.ButtonEdit)send;
            buttonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                                   new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)});
            //buttonEdit.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            //buttonEdit.Properties.Buttons[0].Visible = false;
        }
        private void AllButtonClear(DataTable dataTable)
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                string BtnName = "BtnName_" +dr["PaymentID"].ToString();
                ((DevExpress.XtraEditors.ButtonEdit)(tableLayoutPanel4.Controls[BtnName])).Properties.Buttons.Clear();
            }
        }

        private void Btn_ActualPrice_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Btn_ActualPrice.Text = "";
        }

        private void Btn_ActualPrice_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal ActualPrice = Convert.ToDecimal(Btn_ActualPrice.Text);
                decimal temp = ActualPrice - price-ServiceCharge+Saleprice;
                if (temp <= 0)
                {
                    label5.Text = "差额:￥ " + temp.ToString();
                }
                else
                {
                    label5.Text = "找零:￥ " + temp.ToString();
                }
            }
            catch
            {

            }
        }
        private void IniLab()
        {
            label1.Text = "应收金额:" + price.ToString();
        }
        private void BindNumEvet()
        {
            int iCount = tableLayoutPanel6.Controls.Count;
            for(int i = 0; i < iCount; i++)
            {
                if (tableLayoutPanel6.Controls[i].GetType() == typeof(DevExpress.XtraEditors.SimpleButton))
                {
                    ((DevExpress.XtraEditors.SimpleButton)(tableLayoutPanel6.Controls[i])).Click += Btn_NumClick;
                }
            }
        }
        //数字键事件
        private void Btn_NumClick(object send,EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton simpleButton = (DevExpress.XtraEditors.SimpleButton)send;
            switch (ActivityControl)
            {
                case 0://0主界面
                        Btn_ActualPrice.Text += simpleButton.Text;
                 break;
                case 1://1优惠
                    btnSaleControl.TXT1lock = 0;
                    btnSaleControl.TXT2lock = 0;
                    if (btnSaleControl.iFocus==0)
                    {
                        btnSaleControl.textBox1.Text += simpleButton.Text;
                    }
                    else if(btnSaleControl.iFocus==1)
                    {
                            btnSaleControl.textBox2.Text += simpleButton.Text;
                    }
                    break;
                case 3://3服务费
                    serviceChargeControl.Txt_Price.Text += simpleButton.Text;
                    break;
                case 4://4签单
                    qianDanControl.textEdit1.Text += simpleButton.Text;
                    break;
            }

        }
        private void BindBtnEvet()
        {
            int iCount = tableLayoutPanel3.Controls.Count;
            for (int i = 0; i < iCount; i++)
            {
                if (tableLayoutPanel3.Controls[i].GetType() == typeof(DevExpress.XtraEditors.SimpleButton))
                {
                    ((DevExpress.XtraEditors.SimpleButton)(tableLayoutPanel3.Controls[i])).Click += Btn_BtnClick;
                }
            }
        }
        private void IniControl()
        {
            //btnSaleControl 优惠
            btnSaleControl = new BtnSaleControl(price);
            btnSaleControl.Dock = DockStyle.Fill;
            btnSaleControl.MyCloseEvents += CloseEvents;
            //tuiCaiGridControl 退菜
            tuiCaiGridControl = new TuiCaiGridControl(TableNo,EatNo,MenuTable);
            tuiCaiGridControl.Dock = DockStyle.Fill;
            tuiCaiGridControl.MyCloseEvents += CloseEvents; 
             //serviceChargeControl 服务费
             serviceChargeControl = new ServiceChargeControl(price);
            serviceChargeControl.Dock = DockStyle.Fill;
            serviceChargeControl.MyCloseEvents += CloseEvents;
            //serviceChargeControl 签单
            qianDanControl = new QianDanControl();
            qianDanControl.Dock = DockStyle.Fill;
            qianDanControl.MyCloseEvents += CloseEvents;
        }
        //最上面一排按钮事件
        private void Btn_BtnClick(object send, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton simpleButton = (DevExpress.XtraEditors.SimpleButton)send;
            switch (simpleButton.Tag.ToString())
            {
                case "优惠":
                    tableLayoutPanel1.Controls.Remove(tableLayoutPanel2);
                    this.tableLayoutPanel1.Controls.Add(btnSaleControl, 0, 0);
                    ActivityControl = 1;
                    break;
                case "退菜":
                    tableLayoutPanel1.Controls.Remove(tableLayoutPanel2);
                    this.tableLayoutPanel1.Controls.Add(tuiCaiGridControl, 0, 0);
                    ActivityControl = 2;
                    break;
                case "服务费":
                    tableLayoutPanel1.Controls.Remove(tableLayoutPanel2);
                    this.tableLayoutPanel1.Controls.Add(serviceChargeControl, 0, 0);
                    ActivityControl = 3;
                    break;
                case "签单":
                    tableLayoutPanel1.Controls.Remove(tableLayoutPanel2);
                    this.tableLayoutPanel1.Controls.Add(qianDanControl, 0, 0);
                    ActivityControl = 4;
                    if (QianDanPhoneNo.Length > 0)
                    {
                        qianDanControl.textEdit1.Text = QianDanPhoneNo;
                    }
                    else
                    {
                        qianDanControl.textEdit1.Text = "";
                    }
                    qianDanControl.textEdit1.Properties.NullValuePromptShowForEmptyValue = true;
                    qianDanControl.textEdit1.Properties.NullValuePrompt = "输入签约客户手机号";
                    qianDanControl.label1.Text = "签单";
                    qianDanControl.QiandanOrVIP = true;
                    break;
                case "会员":
                    tableLayoutPanel1.Controls.Remove(tableLayoutPanel2);
                    this.tableLayoutPanel1.Controls.Add(qianDanControl, 0, 0);
                    ActivityControl = 4;
                    if (VIPNo.Length > 0)
                    {
                        qianDanControl.textEdit1.Text = VIPNo;
                    }
                    else
                    {
                        qianDanControl.textEdit1.Text = "";
                    }
                    qianDanControl.textEdit1.Properties.NullValuePromptShowForEmptyValue = true;
                    qianDanControl.textEdit1.Properties.NullValuePrompt = "输入会员的手机号或卡号";
                    qianDanControl.label1.Text = "会员买单";
                    qianDanControl.QiandanOrVIP = false;
                    break;
                case "免单":
                    if (XtraMessageBox.Show("你确定要免单吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        XtraMessageBox.Show("yes");
                    }
                    else
                    {
                        XtraMessageBox.Show("no");
                    }
                    break;
            }
            if (simpleButton.Tag.ToString() != "免单")
            {
                Btn_CheckOut.Enabled = false;
                Btn_yuda.Enabled = false;
            }
        }
        //窗体关闭后返回值
        private void CloseEvents(int icontrol, string Resoult)
        {
            switch (icontrol)
            {
                case 1:
                    tableLayoutPanel1.Controls.Remove(btnSaleControl);
                    Saleprice = price - Convert.ToDecimal(Resoult);
                    Btn_ActualPrice.Text = (price + ServiceCharge - Saleprice).ToString();
                    label4.Text = "优惠：￥" + Saleprice.ToString();
                    if (Saleprice > 0)
                    {
                        simpleButton1.Text = "优惠:￥" + Saleprice;
                        simpleButton1.ForeColor = Color.Orange;
                    }
                    else
                    {
                        simpleButton1.Text = "优惠";
                        simpleButton1.ForeColor = Color.Black;
                    }
                    break;
                case 2:
                    tableLayoutPanel1.Controls.Remove(tuiCaiGridControl);
                   // MessageBox.Show(Resoult);
                    break;
                case 3:
                    tableLayoutPanel1.Controls.Remove(serviceChargeControl);
                    ServiceCharge = Convert.ToDecimal(Resoult);
                    Btn_ActualPrice.Text = (price + ServiceCharge - Saleprice).ToString();
                    label1.Text= "应收金额：￥"+(price + ServiceCharge).ToString();
                    if (ServiceCharge > 0)
                    {
                        simpleButton3.Text = "服务费:￥" + ServiceCharge;
                        simpleButton3.ForeColor = Color.Orange;
                    }
                    else
                    {
                        simpleButton3.Text = "服务费";
                        simpleButton3.ForeColor = Color.Black;
                    }

                    break;
                case 4:
                    tableLayoutPanel1.Controls.Remove(qianDanControl);
                    QianDanPhoneNo = Resoult;
                    if (QianDanPhoneNo.Length>0)
                    {
                        simpleButton4.Text = "签单 号码:" + QianDanPhoneNo;
                        simpleButton4.ForeColor = Color.Orange;
                    }
                    else
                    {
                        simpleButton4.Text = "签单";
                        simpleButton4.ForeColor = Color.Black;
                    }

                    break;
                case 5:
                    tableLayoutPanel1.Controls.Remove(qianDanControl);
                    VIPNo = Resoult;
                    if (VIPNo.Length > 0)
                    {
                        simpleButton5.Text = "会员 号码:" + VIPNo;
                        simpleButton5.ForeColor = Color.Orange;
                    }
                    else
                    {
                        simpleButton5.Text = "会员";
                        simpleButton5.ForeColor = Color.Black;
                    }

                    break;
            }
            this.tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            Btn_CheckOut.Enabled = true;
            Btn_yuda.Enabled = true;
            ActivityControl = 0;
        }
    }
    
}
