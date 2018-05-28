using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DianDianClient.Biz;
using DianDianClient.Models;
namespace DianDianClient
{
    public partial class MainForm : OfficeForm
    {
        //�Ƿ��һ�δ�ѡ�
        bool FoodManagement = false;
        bool BusinessDetails = false;
        bool ActivityManagement = false;
        bool OrderManagement = false;
        bool MemberManagement = false;
        bool More = false;
        public MainForm()
        {
            InitializeComponent();

            SyncClient client = new SyncClient();
            //client.SyncInfoList();
            //������̨ͬ�����߳�
            Thread syncThread = new Thread(new ThreadStart(client.SyncMethod));
            Thread TipFormThread = new Thread(new ThreadStart(ShowTipFormThread));
          //  Thread testtipThread = new Thread(new ThreadStart(showtesttipThread));
            //syncThread.Start();
            BizBillController bbc = new BizBillController();
            Thread billThread = new Thread(new ThreadStart(bbc.RemoteGetBillList));
          //  billThread.Start();

            BIZFoodController bfc = new BIZFoodController();

          //  var tmp = bfc.GetFoodList(0);

            comboBoxEx1.Items.AddRange(new object[] { eStyle.Office2013, eStyle.OfficeMobile2014, eStyle.Office2010Blue,
                eStyle.Office2010Silver, eStyle.Office2010Black, eStyle.VisualStudio2010Blue, eStyle.VisualStudio2012Light, 
                eStyle.VisualStudio2012Dark, eStyle.Office2007Blue, eStyle.Office2007Silver, eStyle.Office2007Black});
            comboBoxEx1.SelectedIndex = 0;
            foreach (DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins)
                comboBoxEdit1.Properties.Items.Add(skin.SkinName);
            this.WindowState = FormWindowState.Maximized;

            //Ĭ�ϴ���λ
            OpenDefaultTable();

            Utils.utils.MyEvent += ShowTip;
            Utils.utils.MessageBoxEvent += ShowMessageBox;
            Utils.utils.MessageBoxYesNoEvent += ShowMessageYesNoBox;
            MyEvent.More.MoreEvent.ReplaceEvent += MyReplaceEvent;
            MyEvent.More.MoreEvent.ShowWaitEvent += MyShowWaitEvent;
            MyEvent.More.MoreEvent.EndShowWaitEvent += MyEndShowWaitEvent;
            Utils.utils.MessageBoxTipsFormListEvent += ShowMessageBoxTipsFormListEvent;
            TipFormThread.Start();
            //  testtipThread.Start();
            //���ض���bar
            MyControl.TopBarControl topBar = new MyControl.TopBarControl();
            topBar.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(topBar, 0, 0);
            topBar.MyEvent += TopBarEvent;

        }
        private void TopBarEvent(int op)//1��С�� 2��� 3�˳�
        {
            switch(op)
            {
                case 1:
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case 2:
                    this.WindowState = (this.WindowState == FormWindowState.Maximized) ? FormWindowState.Normal : FormWindowState.Maximized;
                    break;
                case 3:
                    //this.WindowState = FormWindowState.Minimized;
                    break;
            }
        }
        private void ShowMessageBoxTipsFormListEvent()
        {
            if (MyModels.TipMsg.list.Count == 0)
                return;
            MyModels.TipMsg._TipMsg a = new MyModels.TipMsg._TipMsg();
            a = MyModels.TipMsg.list[0];
            MyForm.TipForm tip = new MyForm.TipForm(a.title, a.msg);
            tip.StartPosition = FormStartPosition.CenterScreen;
            tip.ShowDialog();
        }
        private void MyShowWaitEvent(string Caption , string Description)
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("���Ժ�,���ڼ�����");     // ����
            splashScreenManager1.SetWaitFormDescription("���ڳ�ʼ��.....");����������// ��Ϣ
        }
        private void MyEndShowWaitEvent()
        {
            splashScreenManager1.CloseWaitForm();
        }
        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = comboBoxEdit1.EditValue.ToString();
        }

        private void switchButton1_ValueChanged(object sender, EventArgs e)
        {
            sideNav1.EnableClose = switchButton1.Value;
            UpdateSideNavDock();
        }

        private void switchButton2_ValueChanged(object sender, EventArgs e)
        {
            sideNav1.EnableMaximize = switchButton2.Value;
            UpdateSideNavDock();
        }

        private void switchButton3_ValueChanged(object sender, EventArgs e)
        {
            sideNav1.EnableSplitter = switchButton3.Value;
            UpdateSideNavDock();
        }

        /// <summary>
        /// Updates the Dock property of SideNav control since when Close/Maximize/Splitter functionality is enabled
        /// the Dock cannot be set to fill since control needs ability to resize itself.
        /// </summary>
        private void UpdateSideNavDock()
        {
            if (sideNav1.EnableClose || sideNav1.EnableMaximize || sideNav1.EnableSplitter)
            {
                if (sideNav1.Dock != DockStyle.Left)
                {
                    sideNav1.Dock = DockStyle.Left;
                    sideNav1.Width = this.ClientRectangle.Width - 32;
                    ToastNotification.Close(this); // Closes any toast messages if already open
                    ToastNotification.Show(this, "With current settings SideNav control must be able to resize itself so its Dock is set to Left.", 4000);
                }
            }
            else if (sideNav1.Dock != DockStyle.Fill)
            {
                sideNav1.Dock = DockStyle.Fill;
                ToastNotification.Close(this); // Closes any toast messages if already open
                ToastNotification.Show(this, "SideNav control Dock is set to Fill.", 2000);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedItem == null) return;
            eStyle style = (eStyle)comboBoxEx1.SelectedItem;
            if (styleManager1.ManagerStyle != style)
                styleManager1.ManagerStyle = style;
        }

        private void labelX13_MarkupLinkClick(object sender, MarkupLinkClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.devcomponents.com/kb2/?p=1687");
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void sideNavItem6_Click(object sender, EventArgs e)
        {
            if (!BusinessDetails)
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("���Ժ�,���ڼ�����");     // ����
                splashScreenManager1.SetWaitFormDescription("���ڳ�ʼ��.....");����������// ��Ϣ
                MyControl.BusinessDetails.BusinessDetailsControl businessDetailsControl1 = new MyControl.BusinessDetails.BusinessDetailsControl();
                businessDetailsControl1.Dock = DockStyle.Fill;
                this.sideNavPanel5.Controls.Add(businessDetailsControl1);
                BusinessDetails = true;
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void sideNavItem4_Click(object sender, EventArgs e)
        {
            if (!FoodManagement)
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("���Ժ�,���ڼ�����");     // ����
                splashScreenManager1.SetWaitFormDescription("���ڳ�ʼ��.....");����������// ��Ϣ
                MyControl.FoodManagement.EditMenu editMenu1 = new MyControl.FoodManagement.EditMenu();
                editMenu1.Dock = DockStyle.Fill;
                this.sideNavPanel3.Controls.Add(editMenu1);
                FoodManagement = true;
                splashScreenManager1.CloseWaitForm();
            }
        }
        private void OpenDefaultTable()
        {
            sideNavItem3.Select();
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("���Ժ�,���ڼ�����");     // ����
            splashScreenManager1.SetWaitFormDescription("���ڳ�ʼ��.....");����������// ��Ϣ
            MyControl.TableSettlement.TableSettlement tableSettlement1 = new MyControl.TableSettlement.TableSettlement();
            tableSettlement1.Dock = DockStyle.Fill;
            this.sideNavPanel2.Controls.Add(tableSettlement1);
            splashScreenManager1.CloseWaitForm();
        }
        public void ShowTip(string title, string msg, int FormDelayTime)
        {
            this.Invoke(new MessageBoxShow(MessageBoxShow_F), new object[] { title, msg, FormDelayTime });
        }
        delegate void MessageBoxShow(string title, string msg, int FormDelayTime);
        void MessageBoxShow_F(string title, string msg, int FormDelayTime)
        {
                DevExpress.XtraBars.Alerter.AlertInfo info = new DevExpress.XtraBars.Alerter.AlertInfo(title, msg);
                //���ֵ�Ч����ʽ
                this.alertControl1.FormShowingEffect = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.MoveHorizontal;
                //�������ٶ�
                this.alertControl1.FormDisplaySpeed = DevExpress.XtraBars.Alerter.AlertFormDisplaySpeed.Slow;
                //�Ժ���Ϊ��λ
                this.alertControl1.AutoFormDelay = FormDelayTime;
                alertControl1.Show(this, info);
        }

        public void ShowMessageBox(string msg, string title )
        {
            this.Invoke(new MyMessageBoxShow(MyMessageBoxShow_F), new object[] { msg , title });
        }
        delegate void MyMessageBoxShow(string msg, string title);
        void MyMessageBoxShow_F(string msg, string title)
        {
            XtraMessageBox.Show(msg,title);
        }


        public void ShowMessageYesNoBox(string msg, string title,int id)
        {
            this.Invoke(new MyMessageYesNoBoxShow(MyMessageYesNoBoxShow_F), new object[] { msg, title, id});
        }
        delegate void MyMessageYesNoBoxShow(string msg, string title,int id);
        void MyMessageYesNoBoxShow_F(string msg, string title,int id)
        {
            if (XtraMessageBox.Show(msg, title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Utils.utils.ShowMessageYesNoBoxResult(id);
            }
        }
        private void sideNavItem2_Click(object sender, EventArgs e)
        {
            if (!ActivityManagement)
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("���Ժ�,���ڼ�����");     // ����
                splashScreenManager1.SetWaitFormDescription("���ڳ�ʼ��.....");����������// ��Ϣ
                MyControl.ActivityManagement.ActivityManageControl activityManage = new MyControl.ActivityManagement.ActivityManageControl();
                activityManage.Dock = DockStyle.Fill;
                this.sideNavPanel1.Controls.Add(activityManage);
                ActivityManagement = true;
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void sideNavItem7_Click(object sender, EventArgs e)
        {
            if (!OrderManagement)
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("���Ժ�,���ڼ�����");     // ����
                splashScreenManager1.SetWaitFormDescription("���ڳ�ʼ��.....");����������// ��Ϣ
                MyControl.OrderManagement.OrderManageControl orderManage = new MyControl.OrderManagement.OrderManageControl();
                orderManage.Dock = DockStyle.Fill;
                this.sideNavPanel6.Controls.Add(orderManage);
                OrderManagement = true;
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void sideNavItem8_Click(object sender, EventArgs e)
        {
            if (!MemberManagement)
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("���Ժ�,���ڼ�����");     // ����
                splashScreenManager1.SetWaitFormDescription("���ڳ�ʼ��.....");����������// ��Ϣ
                MyControl.MemberManagement.MemberManageControl memberManage = new MyControl.MemberManagement.MemberManageControl();
                memberManage.Dock = DockStyle.Fill;
                this.sideNavPanel7.Controls.Add(memberManage);
                MemberManagement = true;
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void sideNavItem9_Click(object sender, EventArgs e)
        {
            //if (!More)
            //{
                this.sideNavPanel8.Controls.Clear();
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("���Ժ�,���ڼ�����");     // ����
                splashScreenManager1.SetWaitFormDescription("���ڳ�ʼ��.....");����������// ��Ϣ
                MyControl.More.MoreControl moreControl = new MyControl.More.MoreControl();
                moreControl.Dock = DockStyle.Fill;
                this.sideNavPanel8.Controls.Add(moreControl);
                More = true;
                splashScreenManager1.CloseWaitForm();
            //}
        }
        private void MyReplaceEvent(int ControlId)
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("���Ժ�,���ڼ�����");     // ����
            splashScreenManager1.SetWaitFormDescription("���ڳ�ʼ��.....");����������// ��Ϣ
            switch (ControlId)
            {
                case 0:
                    this.sideNavPanel8.Controls.Clear();
                    MyControl.More.TableManage.TableManageControl tableManage = new MyControl.More.TableManage.TableManageControl();
                    tableManage.Dock = DockStyle.Fill;
                    this.sideNavPanel8.Controls.Add(tableManage);
                    break;
                case 1:
                    this.sideNavPanel8.Controls.Clear();
                    MyControl.More.StaffManage.StaffManageControl staffManage = new MyControl.More.StaffManage.StaffManageControl();
                    staffManage.Dock = DockStyle.Fill;
                    this.sideNavPanel8.Controls.Add(staffManage);
                    break;
                case 2:
                    this.sideNavPanel8.Controls.Clear();
                    MyControl.More.dangkouManage.dangkouManageControl dangkouManage = new MyControl.More.dangkouManage.dangkouManageControl();
                    dangkouManage.Dock = DockStyle.Fill;
                    this.sideNavPanel8.Controls.Add(dangkouManage);
                    break;
                case 3:
                    this.sideNavPanel8.Controls.Clear();
                    MyControl.More.jinxiaocunManage.jinxiaocunManageControl jinxiaocunManage = new MyControl.More.jinxiaocunManage.jinxiaocunManageControl();
                    jinxiaocunManage.Dock = DockStyle.Fill;
                    this.sideNavPanel8.Controls.Add(jinxiaocunManage);
                    break;
                case 4:
                    this.sideNavPanel8.Controls.Clear();
                    MyControl.More.cantingSetUp.cantingControl cantingSetUp = new MyControl.More.cantingSetUp.cantingControl();
                    cantingSetUp.Dock = DockStyle.Fill;
                    this.sideNavPanel8.Controls.Add(cantingSetUp);
                    break;
                case 1001:
                    this.sideNavPanel8.Controls.Clear();
                    MyControl.More.TableManage.QuYuManageControl quYuManage = new MyControl.More.TableManage.QuYuManageControl();
                    quYuManage.Dock = DockStyle.Fill;
                    this.sideNavPanel8.Controls.Add(quYuManage);
                    break;
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Utils.utils.AddMessageBoxTipsFormList("��Ϣ", "��ʾ��");
        }
        private void ShowTipFormThread()
        {
            while (true)
            {
                Utils.utils.ShowMessageBoxTipsForm();
                Thread.Sleep(500);
            }
        }
        //private void showtesttipThread()
        //{
        //    int i = 0;
        //    while (true)
        //    {
        //        i++;
        //        Utils.utils.AddMessageBoxTipsFormList("��������" + i.ToString());
        //        Thread.Sleep(3000);
        //        if (i == 5)
        //        {
        //            return;
        //        }
        //    }
        //}
    }
}