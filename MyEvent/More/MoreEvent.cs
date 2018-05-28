using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.MyEvent.More
{
   public static  class MoreEvent
    {
        public delegate void MyDelegate(int ControlId);
        public static event MyDelegate ReplaceEvent;

        public delegate void ShowWaitDelegate(string Caption , string Description);
        public static event ShowWaitDelegate ShowWaitEvent;

        public delegate void EndShowWaitDelegate();
        public static event EndShowWaitDelegate EndShowWaitEvent;

        public static void Replace(int ControlId)
        {
            ReplaceEvent(ControlId);
        }
        public static void ShowWait(string Caption = "请稍后,正在加载中", string Description = "正在初始化.....")
        {
            ShowWaitEvent(Caption, Description);
        }
        public static void EndShowWait()
        {
            EndShowWaitEvent();
        }

    }
}
