using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.MyEvent.TableSettlement
{
    public static class PayBillEvent
    {
        public delegate void MyDelegate(string name, decimal value);
        public static event MyDelegate PayEvent;

        public delegate void CloseDelegate();
        public static event CloseDelegate CloseEvent;
        public static void SnedDeatil(string name, decimal value)
        {
            PayEvent(name, value);
        }
        public static void Close()
        {
            CloseEvent();
        }
    }
}
