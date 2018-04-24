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

namespace DianDianClient.MyControl
{
    public partial class TopBarControl : UserControl
    {
        public delegate void MyDelegate(int op);//1最小化 2最大化 3退出
        public event MyDelegate MyEvent;
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
    }
}
