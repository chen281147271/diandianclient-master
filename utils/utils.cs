using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
namespace DianDianClient.Utils
{
    public static class utils
    {
        public delegate void MyDelegate(string title, string msg, int FormDelayTime);
        public static event MyDelegate MyEvent;

        public delegate void MessageBoxDelegate(string msg, string title);
        public static event  MessageBoxDelegate MessageBoxEvent;
        //右下角提示框
        public static void ShowTip(string title, string msg, int FormDelayTime=5000)
        {
            MyEvent(title, msg, FormDelayTime);
        }
        public static void ShowMessageBox( string msg, string title="提示")
        {
            MessageBoxEvent(msg,title);
        }
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> array)
        {
            var ret = new DataTable();
            foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
                ret.Columns.Add(dp.Name, dp.PropertyType);
            foreach (T item in array)
            {
                var Row = ret.NewRow();
                foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
                    Row[dp.Name] = dp.GetValue(item);
                ret.Rows.Add(Row);
            }
            return ret;
        }

    }
}
