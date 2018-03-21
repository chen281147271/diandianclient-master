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

        public delegate void ShowWaitDelegate();
        public static event ShowWaitDelegate ShowWaitEvent;

        public delegate void EndShowWaitDelegate();
        public static event EndShowWaitDelegate EndShowWaitEvent;

        public static void Replace(int ControlId)
        {
            ReplaceEvent(ControlId);
        }
        public static void ShowWait()
        {
            ShowWaitEvent();
        }
        public static void EndShowWait()
        {
            EndShowWaitEvent();
        }

    }
}
