using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.MyEvent.MemberManagement
{
  public static  class RechargeRecordEvent
    {
        public delegate void MyDelegate();
        public static event MyDelegate CloseEvent;

        public static void Close()
        {
            CloseEvent();
        }
    }
}
