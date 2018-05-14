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
using System.Runtime.InteropServices;
namespace DianDianClient.MyForm
{
    public partial class StarForm : DevExpress.XtraEditors.XtraForm
    {
        //是否第一次打开选项卡
        bool FoodManagement = false;
        bool BusinessDetails = false;
        bool ActivityManagement = false;
        bool OrderManagement = false;
        bool MemberManagement = false;
        bool PaiDui = false;

        public StarForm()
        {
            InitializeComponent();
            ini();
        }
        #region 窗体移动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        bool beginMove = false;//初始化鼠标位置  
        int currentXPosition;
        int currentYPosition;
        #endregion 
        #region 加载顶部bar
        private void IniTopBar()
        {
            MyControl.TopBarControl topBar = new MyControl.TopBarControl();
            topBar.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(topBar, 0, 0);
            topBar.MyEvent += TopBarEvent;
            topBar.MyMoveEvent += MoveEvent;
        }
        private void MoveEvent(int op,int MousePosition_X, int MousePosition_Y)
        {
            switch (op)
            {
                case 1:
                    _MouseDown(MousePosition.X, MousePosition.Y);
                    break;
                case 2:
                    _MouseMove(MousePosition.X, MousePosition.Y);
                    break;
                case 3:
                    _MouseUp();
                    break;
            }
        }
        private void TopBarEvent(int op)//1最小化 2最大化 3退出 4打印机
        {
            switch (op)
            {
                case 1:
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case 2:
                    this.WindowState = (this.WindowState == FormWindowState.Maximized) ? FormWindowState.Normal : FormWindowState.Maximized;
                    break;
                case 3:
                    //this.Close();
                    MyForm.JiaoBan.QuitForm quit = new JiaoBan.QuitForm();
                    quit.StartPosition = FormStartPosition.CenterScreen;
                    quit.ShowDialog();
                    break;
                case 4:
                    navigationPane1.SelectedPage = nav_more;
                    open_more();
                    MyReplaceEvent(2);
                    break;
            }
        }
        private void _MouseDown(int MousePosition_X, int MousePosition_Y)
        {
            beginMove = true;
            currentXPosition = MousePosition_X;//鼠标的x坐标为当前窗体左上角x坐标  
            currentYPosition = MousePosition_Y;//鼠标的y坐标为当前窗体左上角y坐标  
        }

        private void _MouseMove(int MousePosition_X, int MousePosition_Y)
        {
            if (beginMove)
            {
                this.Left += MousePosition_X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x  
                this.Top += MousePosition_Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标  
                currentXPosition = MousePosition_X;
                currentYPosition = MousePosition_Y;
            }
        }

        private void _MouseUp()
        {
            currentXPosition = 0; //设置初始状态  
            currentYPosition = 0;
            beginMove = false;

        }
        #endregion
        #region 绑定子控件事件 开启后台线程
        private void ini()
        {
            bandevent();
            IniTopBar();
        }
        private void bandevent()
        {
            SyncClient client = new SyncClient();
            //client.SyncInfoList();
            //创建后台同步的线程
            Thread syncThread = new Thread(new ThreadStart(client.SyncMethod));
            Thread TipFormThread = new Thread(new ThreadStart(ShowTipFormThread));
            //  Thread testtipThread = new Thread(new ThreadStart(showtesttipThread));
            //syncThread.Start();
            BizBillController bbc = new BizBillController();
            Thread billThread = new Thread(new ThreadStart(bbc.RemoteGetBillList));
            //  billThread.Start();

            OpenDefaultTable();

            Utils.utils.MyEvent += ShowTip;
            Utils.utils.MessageBoxEvent += ShowMessageBox;
            Utils.utils.MessageBoxYesNoEvent += ShowMessageYesNoBox;
            MyEvent.More.MoreEvent.ReplaceEvent += MyReplaceEvent;
            MyEvent.More.MoreEvent.ShowWaitEvent += MyShowWaitEvent;
            MyEvent.More.MoreEvent.EndShowWaitEvent += MyEndShowWaitEvent;
            Utils.utils.MessageBoxTipsFormListEvent += ShowMessageBoxTipsFormListEvent;
            TipFormThread.Start();
        }
        private void ShowTipFormThread()
        {
            while (true)
            {
                Utils.utils.ShowMessageBoxTipsForm();
                Thread.Sleep(500);
            }
        }
        private void OpenDefaultTable()
        {
            nav_table.Select();
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");　　　　　// 信息
            MyControl.TableSettlement.TableSettlement tableSettlement1 = new MyControl.TableSettlement.TableSettlement();
            tableSettlement1.Dock = DockStyle.Fill;
            this.nav_table.Controls.Add(tableSettlement1);
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
            //出现的效果方式
            this.alertControl1.FormShowingEffect = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.MoveHorizontal;
            //弹出的速度
            this.alertControl1.FormDisplaySpeed = DevExpress.XtraBars.Alerter.AlertFormDisplaySpeed.Slow;
            //以毫秒为单位
            this.alertControl1.AutoFormDelay = FormDelayTime;
            alertControl1.Show(this, info);
        }
        public void ShowMessageBox(string msg, string title)
        {
            this.Invoke(new MyMessageBoxShow(MyMessageBoxShow_F), new object[] { msg, title });
        }
        delegate void MyMessageBoxShow(string msg, string title);
        void MyMessageBoxShow_F(string msg, string title)
        {
            XtraMessageBox.Show(msg, title);
        }
        public void ShowMessageYesNoBox(string msg, string title, int id)
        {
            this.Invoke(new MyMessageYesNoBoxShow(MyMessageYesNoBoxShow_F), new object[] { msg, title, id });
        }
        delegate void MyMessageYesNoBoxShow(string msg, string title, int id);
        void MyMessageYesNoBoxShow_F(string msg, string title, int id)
        {
            if (XtraMessageBox.Show(msg, title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Utils.utils.ShowMessageYesNoBoxResult(id);
            }
        }
        private void MyReplaceEvent(int ControlId)
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");　　　　　// 信息
            switch (ControlId)
            {
                case 0:
                    this.nav_more.Controls.Clear();
                    MyControl.More.TableManage.TableManageControl tableManage = new MyControl.More.TableManage.TableManageControl();
                    tableManage.Dock = DockStyle.Fill;
                    this.nav_more.Controls.Add(tableManage);
                    break;
                case 1:
                    this.nav_more.Controls.Clear();
                    MyControl.More.StaffManage.StaffManageControl staffManage = new MyControl.More.StaffManage.StaffManageControl();
                    staffManage.Dock = DockStyle.Fill;
                    this.nav_more.Controls.Add(staffManage);
                    break;
                case 2:
                    this.nav_more.Controls.Clear();
                    MyControl.More.dangkouManage.dangkouManageControl dangkouManage = new MyControl.More.dangkouManage.dangkouManageControl();
                    dangkouManage.Dock = DockStyle.Fill;
                    this.nav_more.Controls.Add(dangkouManage);
                    break;
                case 3:
                    this.nav_more.Controls.Clear();
                    MyControl.More.jinxiaocunManage.jinxiaocunManageControl jinxiaocunManage = new MyControl.More.jinxiaocunManage.jinxiaocunManageControl();
                    jinxiaocunManage.Dock = DockStyle.Fill;
                    this.nav_more.Controls.Add(jinxiaocunManage);
                    break;
                case 4:
                    this.nav_more.Controls.Clear();
                    MyControl.More.cantingSetUp.cantingControl cantingSetUp = new MyControl.More.cantingSetUp.cantingControl();
                    cantingSetUp.Dock = DockStyle.Fill;
                    this.nav_more.Controls.Add(cantingSetUp);
                    break;
                case 1001:
                    this.nav_more.Controls.Clear();
                    MyControl.More.TableManage.QuYuManageControl quYuManage = new MyControl.More.TableManage.QuYuManageControl();
                    quYuManage.Dock = DockStyle.Fill;
                    this.nav_more.Controls.Add(quYuManage);
                    break;
            }
            splashScreenManager1.CloseWaitForm();
        }
        private void MyShowWaitEvent()
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");　　　　　// 信息
        }
        private void MyEndShowWaitEvent()
        {
            splashScreenManager1.CloseWaitForm();
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
        //禁止缩放
        private void navigationPane1_StateChanged(object sender, DevExpress.XtraBars.Navigation.StateChangedEventArgs e)
        {
            if (e.State == DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed)
            {
                this.navigationPane1.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Expanded;
            }

        }
        private void nav_more_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            open_more();
        }
        #endregion
        #region 打开子选项卡
        private void navigationPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            switch (e.Page.Caption)
            {
                case "订单管理":
                    if (!OrderManagement)
                    {
                        open_OrderManagement();
                    }
                    break;
                case "营业详情":
                    if (!BusinessDetails)
                    {
                        open_BusinessDetails();
                    }
                    break;
                case "排队叫号":
                    if (!PaiDui)
                    {
                        open_PaiDui();
                    }
                    break;
                case "会员管理":
                    if (!MemberManagement)
                    {
                        open_MemberManagement();
                    }
                    break;
                case "菜品管理":
                    if (!FoodManagement)
                    {
                        open_FoodManagement();
                    }
                    break;
                case "活动管理":
                    if (!ActivityManagement)
                    {
                        open_ActivityManagement();
                    }
                    break;
                case "更多选项":
                    open_more();
                    break;
            }
        }
        private void open_ActivityManagement()
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");     // 信息
            MyControl.ActivityManagement.ActivityManageControl activityManage = new MyControl.ActivityManagement.ActivityManageControl();
            activityManage.Dock = DockStyle.Fill;
            this.nav_activity.Controls.Add(activityManage);
            ActivityManagement = true;
            splashScreenManager1.CloseWaitForm();
        }
        private void open_FoodManagement()
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");     // 信息
            MyControl.FoodManagement.EditMenu editMenu1 = new MyControl.FoodManagement.EditMenu();
            editMenu1.Dock = DockStyle.Fill;
            this.nav_food.Controls.Add(editMenu1);
            FoodManagement = true;
            splashScreenManager1.CloseWaitForm();
        }
        private void open_PaiDui()
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");     // 信息
            MyControl.PaiDui.PaiDuiControl _paiDui = new MyControl.PaiDui.PaiDuiControl();
            _paiDui.Dock = DockStyle.Fill;
            this.nav_paidui.Controls.Add(_paiDui);
            PaiDui = true;
            splashScreenManager1.CloseWaitForm();
        }
        private void open_MemberManagement()
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");     // 信息
            MyControl.MemberManagement.MemberManageControl memberManage = new MyControl.MemberManagement.MemberManageControl();
            memberManage.Dock = DockStyle.Fill;
            this.nav_vip.Controls.Add(memberManage);
            MemberManagement = true;
            splashScreenManager1.CloseWaitForm();
        }
        private void open_BusinessDetails()
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");     // 信息
            MyControl.BusinessDetails.BusinessDetailsControl businessDetailsControl1 = new MyControl.BusinessDetails.BusinessDetailsControl();
            businessDetailsControl1.Dock = DockStyle.Fill;
            this.nav_yingye.Controls.Add(businessDetailsControl1);
            BusinessDetails = true;
            splashScreenManager1.CloseWaitForm();
        }
        private void open_OrderManagement()
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");     // 信息
            MyControl.OrderManagement.OrderManageControl orderManage = new MyControl.OrderManagement.OrderManageControl();
            orderManage.Dock = DockStyle.Fill;
            this.nav_order.Controls.Add(orderManage);
            OrderManagement = true;
            splashScreenManager1.CloseWaitForm();
        }
        private void open_more()
        {
            this.nav_more.Controls.Clear();
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("请稍后,正在加载中");     // 标题
            splashScreenManager1.SetWaitFormDescription("正在初始化.....");     // 信息
            MyControl.More.MoreControl moreControl = new MyControl.More.MoreControl();
            moreControl.Dock = DockStyle.Fill;
            this.nav_more.Controls.Add(moreControl);
            splashScreenManager1.CloseWaitForm();
        }
        private void nav_order_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            open_OrderManagement();
        }

        private void nav_table_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            OpenDefaultTable();
        }

        private void nav_yingye_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            open_BusinessDetails();
        }

        private void nav_vip_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            open_MemberManagement();
        }

        private void nav_food_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            open_FoodManagement();
        }

        private void nav_activity_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            open_ActivityManagement();
        }
        private void nav_paidui_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            open_PaiDui();
        }
        #endregion
    }
}
