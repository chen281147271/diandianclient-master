using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.MyEvent.More.jinxiaocunManage
{
   public static class jinxiaocunEvent
    {
        public delegate void MyDelegate();
        public static event MyDelegate ReplaceEvent;

        public static void Replace()
        {
            ReplaceEvent();
        }
    }
}
