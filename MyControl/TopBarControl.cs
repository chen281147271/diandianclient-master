using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreAudioApi;
using System.Runtime.InteropServices;
using DevExpress.XtraBars.Navigation;
namespace DianDianClient.MyControl
{
    public partial class TopBarControl : UserControl
    {
        public delegate void MyDelegate(int op);//1最小化 2最大化 3退出 4打印机
        public event MyDelegate MyEvent;
        public delegate void MyMoveDelegate(int op,int MousePosition_X, int MousePosition_Y);//1down 2move 3up
        public event MyMoveDelegate MyMoveEvent;
        private MMDevice defaultDevice = null;
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
        const uint WM_APPCOMMAND = 0x319;
        const uint APPCOMMAND_VOLUME_UP = 0x0a;
        const uint APPCOMMAND_VOLUME_DOWN = 0x09;
        const uint APPCOMMAND_VOLUME_MUTE = 0x08;
        public TopBarControl()
        {
            InitializeComponent();
            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
            defaultDevice =
             devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            tileNavItem_voice.TileImage = (isMuted()) ? global::DianDianClient.Properties.Resources.voice_close : global::DianDianClient.Properties.Resources.voice;
            inistylelist();
            bindevent();
        }
        private void inistylelist()
        {
            foreach (DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins)
            {
                DevExpress.XtraBars.Navigation.TileNavSubItem tileNavSubItem = new DevExpress.XtraBars.Navigation.TileNavSubItem();
                DevExpress.XtraEditors.TileItemElement tileItemElement = new DevExpress.XtraEditors.TileItemElement();

                tileNavSubItem.Caption = skin.SkinName;
                tileNavSubItem.Name = skin.SkinName;

                tileNavSubItem.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
                tileItemElement.Text = skin.SkinName;
                tileNavSubItem.Tile.Elements.Add(tileItemElement);
                tileNavSubItem.Tile.Name = "tileItemElement";
                this.tileNavItem_style.SubItems.Add(tileNavSubItem);
            }
        }
        private void bindevent()
        {
            foreach(TileNavSubItem tileNavSubItem in this.tileNavItem_style.SubItems)
            {
                tileNavSubItem.ElementClick+= new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(Style_ElementClick);
            }
        }

        private void navbtn_min_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            MyEvent(1);
        }

        private void navbtn_max_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            MyEvent(2);
        }

        private void navbtn_quit_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            MyEvent(3);
        }

        private void tileNavItem_voice_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            SendMessage(this.Handle, WM_APPCOMMAND, 0x200eb0, APPCOMMAND_VOLUME_MUTE * 0x10000);
            tileNavItem_voice.TileImage = (!isMuted()) ? global::DianDianClient.Properties.Resources.voice_close : global::DianDianClient.Properties.Resources.voice;
        }
        private bool isMuted()
        {
            return defaultDevice.AudioEndpointVolume.Mute;
        }

        private void Style_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            Utils.utils._ShowWait("请稍后,主题正在应用","大约需要10秒");
            DevExpress.XtraBars.Navigation.TileNavSubItem tileNavSubItem = (sender) as DevExpress.XtraBars.Navigation.TileNavSubItem;
            defaultLookAndFeel1.LookAndFeel.SkinName = tileNavSubItem.Caption;
            Utils.utils._EndShowWait();
        }

        private void tileNavPane1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                MyMoveEvent(1, MousePosition.X, MousePosition.Y);
            }
        }

        private void tileNavPane1_MouseMove(object sender, MouseEventArgs e)
        {
            MyMoveEvent(2, MousePosition.X, MousePosition.Y);
        }

        private void tileNavPane1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MyMoveEvent(3, MousePosition.X, MousePosition.Y);
            }
        }

        private void tileNavItem_print_ElementClick(object sender, NavElementEventArgs e)
        {
            MyEvent(4);
        }

        private void tileNavItem_pw_ElementClick(object sender, NavElementEventArgs e)
        {
            MyForm.More.StaffManage.PWForm pWForm = new MyForm.More.StaffManage.PWForm(1);
            pWForm.StartPosition = FormStartPosition.CenterScreen;
            pWForm.ShowDialog();
        }
    }
}
