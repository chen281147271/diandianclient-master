using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.dangkouManage
{
    public partial class QuerydangkouControl : UserControl
    {
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        List<Models.dd_shop_windows> list_shopwindows;
        public QuerydangkouControl()
        {
            InitializeComponent();
            iniGrid();
            GetGrid();
        }
        private void iniGrid()
        {
            this.gridView1.RowHeight = 50;
            this.gridView1.ColumnPanelRowHeight = 50;
            for (int i = 0; i < this.gridView1.Columns.Count - 5; i++)
            {
                this.gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
            foreach (DevExpress.XtraGrid.Columns.GridColumn gc in this.gridView1.Columns)
            {
                gc.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                gc.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
        }
        private void GetGrid()
        {
            list_shopwindows = BizSPInfo.StallsList();
            this.gridControl1.DataSource = list_shopwindows;
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string windowid = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "windowid"));
            string windowname = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "windowname"));
            string windowdesc = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "windowdesc"));
            string status = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "status"));
            string printname = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "printname"));
            string isdefault = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "isdefault"));
            string isyicaiyidan = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "isyicaiyidan"));
            string isprintexcep = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "isprintexcep"));
            string printnum = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "printnum"));

            Models.dd_shop_windows shopWindows = new Models.dd_shop_windows();
            shopWindows.isdefault =Convert.ToBoolean(isdefault);
            shopWindows.isprintexcep =Convert.ToBoolean(isprintexcep);
            shopWindows.isyicaiyidan = Convert.ToBoolean(isyicaiyidan);
            shopWindows.printname = printname;
            shopWindows.printnum =Convert.ToInt32(printnum);
            shopWindows.status = (status=="使用")?1:0;
            shopWindows.windowdesc = windowdesc;
            shopWindows.windowname = windowname;
            shopWindows.windowid = Convert.ToInt32(windowid);
            if (e.Button.Index == 0)
            {
                MyForm.More.dangkouManage.EditdangkouForm editdangkou = new MyForm.More.dangkouManage.EditdangkouForm(shopWindows,1);
                editdangkou.StartPosition = FormStartPosition.CenterScreen;
                editdangkou.ShowDialog();
            }else if(e.Button.Index == 1)
            {
                
            }else if(e.Button.Index == 2)
            {
                BizSPInfo.Delwindow(Convert.ToInt32(windowid));
            }
            GetGrid();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "status")
            {
                switch (e.DisplayText)
                {
                    case "0":
                        e.DisplayText = "未使用";

                        break;
                    case "1":
                        e.DisplayText = "使用";
                        break;
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Models.dd_shop_windows shopWindows = new Models.dd_shop_windows();
            MyForm.More.dangkouManage.EditdangkouForm editdangkou = new MyForm.More.dangkouManage.EditdangkouForm(shopWindows, 0);
            editdangkou.StartPosition = FormStartPosition.CenterScreen;
            editdangkou.ShowDialog();
            GetGrid();
        }
    }
}
