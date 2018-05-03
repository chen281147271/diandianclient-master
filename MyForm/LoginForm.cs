using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Configuration;

namespace DianDianClient.MyForm
{
    public partial class LoginForm : Form
    {
        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        Biz.BizLoginController BizLogin = new Biz.BizLoginController();
        bool first = true;
        bool changed = false;
        public LoginForm()
        {
            InitializeComponent();
            ini();
        }
        #region 背景GIF
        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);
        private void SetGifBackground(string gifPath)
        {
            //Image gif = Image.FromFile(gifPath);
            Image gif = Properties.Resources.login;
            System.Drawing.Imaging.FrameDimension fd = new System.Drawing.Imaging.FrameDimension(gif.FrameDimensionsList[0]);
            int count = gif.GetFrameCount(fd);    //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)
            Timer giftimer = new Timer();
            giftimer.Interval = 10;
            int i = 0;
            Image bgImg = null;
            giftimer.Tick += (s, e) => {
                if (i >= count) { i = 0; }
                gif.SelectActiveFrame(fd, i);
                System.IO.Stream stream = new System.IO.MemoryStream();
                gif.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                if (bgImg != null) { bgImg.Dispose(); }
                bgImg = Image.FromStream(stream);
                panelEnhanced1.BackgroundImage = bgImg;
                panelEnhanced1.BackgroundImageLayout = ImageLayout.Stretch;
                i++;
            };
            giftimer.Start();
        }
        #endregion
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
            this.topBarControl1.MyEvent += TopBarEvent;
            this.topBarControl1.MyMoveEvent += MoveEvent;
        }
        private void MoveEvent(int op, int MousePosition_X, int MousePosition_Y)
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
                    this.Close();
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
        #region 登入逻辑
        private void ini()
        {
            SetGifBackground("");
            IniTopBar();
            iniDate();
        }
        private void iniDate()
        {
            txt_no.Text =BizLogin.FindShopName(Convert.ToInt32(Properties.Settings.Default.shopkey.ToString()));
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            if (cfa.AppSettings.Settings["PW"].Value != "")
            {
                txt_pw.Text = cfa.AppSettings.Settings["PW"].Value;
                txt_uid.Text = cfa.AppSettings.Settings["UID"].Value;
                this.checkEdit1.Checked = true;
            }
            first = false;
        }
        private void btn_submit_Click(object sender, EventArgs e)
        {
            int result= 0;
            if (this.dxValidationProvider1.Validate())
            {
                if (cfa.AppSettings.Settings["PW"].Value != "" && !changed)
                {
                    result = BizLogin.LocalLogin(txt_uid.Text, txt_pw.Text);
                }
                else
                {
                    result = BizLogin.LocalLogin(txt_uid.Text, MD5_substring(Utils.Md5Helper.Encrypt(txt_pw.Text)));
                }
                if (result == 0)
                {
                    if (checkEdit1.Checked)
                    {
                        cfa.AppSettings.Settings["PW"].Value =(changed)? MD5_substring(Utils.Md5Helper.Encrypt(txt_pw.Text)): txt_pw.Text;
                        cfa.AppSettings.Settings["UID"].Value = txt_uid.Text;
                        cfa.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");
                    }
                    else
                    {
                        cfa.AppSettings.Settings["PW"].Value = "";
                        cfa.AppSettings.Settings["UID"].Value = "";
                        cfa.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");
                        this.checkEdit1.Checked = false;
                    }
                    this.DialogResult = DialogResult.OK;    //返回一个登录成功的对话框状态
                    this.Close();    //关闭登录窗口
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("请检查用户名或者密码 是否输入正确！");
                }
            }
        }

        private void txt_pw_EditValueChanged(object sender, EventArgs e)
        {
            if(!first)
            {
                changed = true;
            }
        }
        private string MD5_substring(string str)
        {
            if (str.Length > 16)
            {
                str = str.Substring(8);
                str = str.Substring(0, str.Length - 8);
                return str.ToLower();
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
