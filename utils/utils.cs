using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using System.Drawing;

namespace DianDianClient.Utils
{
    public static class utils
    {
        public delegate void MyDelegate(string title, string msg, int FormDelayTime);
        public static event MyDelegate MyEvent;

        public delegate void MessageBoxDelegate(string msg, string title);
        public static event  MessageBoxDelegate MessageBoxEvent;

        public delegate void MessageBoxYesNoDelegate(string msg, string title,int id);
        public static event MessageBoxYesNoDelegate MessageBoxYesNoEvent;

        public delegate void MessageBoxYesNoResult(int id);
        public static event MessageBoxYesNoResult MessageBoxYesNoResultEvent;
        //右下角提示框
        public static void ShowTip(string title, string msg, int FormDelayTime=5000)
        {
            MyEvent(title, msg, FormDelayTime);
        }
        public static void ShowMessageBox( string msg, string title="提示")
        {
            MessageBoxEvent(msg,title);
        }
        public static void ShowMessageYesNoBox(string msg,  string title = "提示",int id=0)
        {
            MessageBoxYesNoEvent(msg, title,id);
        }
        public static void ShowMessageYesNoBoxResult(int id = 0)
        {
            MessageBoxYesNoResultEvent(id);
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

        /// <summary>
        /// 为列头绘制CheckBox
        /// </summary>
        /// <param name="view">GridView</param>
        /// <param name="checkItem">RepositoryItemCheckEdit</param>
        /// <param name="fieldName">需要绘制Checkbox的列名</param>
        /// <param name="e">ColumnHeaderCustomDrawEventArgs</param>
        public static void DrawHeaderCheckBox(this GridView view, RepositoryItemCheckEdit checkItem, string fieldName, ColumnHeaderCustomDrawEventArgs e)
        {
            /*说明：
             *参考：https://www.devexpress.com/Support/Center/Question/Details/Q354489
             *在CustomDrawColumnHeader中使用
             *eg：
             * private void gvCabChDetail_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
             * {
             * GridView _view = sender as GridView;
             * _view.DrawHeaderCheckBox(CheckItem, "Check", e);
             * }
             */
            if (e.Column != null && e.Column.FieldName.Equals(fieldName))
            {
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                DrawCheckBox(checkItem, e.Graphics, e.Bounds, getCheckedCount(view, fieldName) == view.DataRowCount);
                e.Handled = true;
            }
        }
        private static void DrawCheckBox(RepositoryItemCheckEdit checkItem, Graphics g, Rectangle r, bool Checked)
        {
            CheckEditViewInfo _info;
            CheckEditPainter _painter;
            ControlGraphicsInfoArgs _args;
            _info = checkItem.CreateViewInfo() as CheckEditViewInfo;
            _painter = checkItem.CreatePainter() as CheckEditPainter;
            _info.EditValue = Checked;

            _info.Bounds = r;
            _info.PaintAppearance.ForeColor = Color.Black;
            _info.CalcViewInfo(g);
            _args = new ControlGraphicsInfoArgs(_info, new DevExpress.Utils.Drawing.GraphicsCache(g), r);
            _painter.Draw(_args);
            _args.Cache.Dispose();
        }
        private static int getCheckedCount(GridView view, string filedName)
        {
            int count = 0;
            for (int i = 0; i < view.DataRowCount; i++)
            {
                object _cellValue = view.GetRowCellValue(i, view.Columns[filedName]);
                if (_cellValue == null) continue;
                if (string.IsNullOrEmpty(_cellValue.ToString().Trim())) continue;
                bool _checkStatus = false;
                if (bool.TryParse(_cellValue.ToString(), out _checkStatus))
                {
                    if (_checkStatus)
                        count++;
                }
            }
            return count;
        }
        public static int Count(string str, string constr)
        {
            return System.Text.RegularExpressions.Regex.Matches(str, constr).Count;
        }
    }
}
