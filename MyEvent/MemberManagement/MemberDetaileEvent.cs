using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.MyEvent.MemberManagement
{
    public static  class MemberDetaileEvent
    {
        public delegate void MyDelegate(string cardid,int type);
        public static event MyDelegate CloseEvent;

        public static void Close(string cardid, int type)
        {
            CloseEvent(cardid, type);
        }
    }
}
