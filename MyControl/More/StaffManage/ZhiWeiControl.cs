using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.StaffManage
{
    public partial class ZhiWeiControl : UserControl
    {
        Biz.BizEmployee BizEmployee = new Biz.BizEmployee();
        List<Models.sys_role> list;
        List<sysrole> list_sysrole;
        List<Models.v_memberrole> list_memberrole;
        public int curPage = 1;
        public int pageSize = 8;
        public int allcount = 0;
        public ZhiWeiControl()
        {
            InitializeComponent();
            list=BizEmployee.QueryPostion("0");
            list_memberrole=BizEmployee.QueryEmployee("");
            iniData();
        }
        private void iniData()
        {
            this.gridControl1.DataSource = this.translate(list).Take(pageSize);

            this.gridView1.RowHeight = 50;
            this.gridView1.ColumnPanelRowHeight = 50;
            for (int i = 0; i < this.gridView1.Columns.Count - 1; i++)
            {
                this.gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
            foreach (DevExpress.XtraGrid.Columns.GridColumn gc in this.gridView1.Columns)
            {
                gc.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                gc.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            //分页
            mgncPager1.myPagerEvents += MyPagerEvents; //new MgncPager.MyPagerEvents(MyPagerEvents);
            mgncPager1.exportEvents += ExportEvents;// new MgncPager.ExportEvents(ExportEvents);
            //必须更新allcount！！！！！！！！！！！！！！！！！！！
            allcount = this.list.Count;
            mgncPager1.RefreshPager(pageSize, allcount, curPage);//更新分页控件显示。
        }
        #region MgncPager 实现
        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="singlePage"></param>
        public void ExportEvents(bool singlePage)//单页，所有      
        {
            //导出GridControl代码写在这。
            if (singlePage)
            {
                ExportToXls();
            }
            else
            {
                BindGrid(false);
                ExportToXls();
                RefreshGridList();
            }
        }
        public void ExportToXls()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                gridControl1.ExportToXls(saveFileDialog.FileName, options);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //分页相关
        public void RefreshGridList()
        {
            FillGridListCtrlQuery(curPage);
        }

        private void FillGridListCtrlQuery(int curPage = 1)   //更新控件
        {
            BindGrid();
            mgncPager1.RefreshPager(pageSize, allcount, curPage);//更新分页控件显示。
        }
        private void BindGrid(bool singlePage = true)//单页，所有     
        {
            this.list = BizEmployee.QueryPostion("0");
            var q = list;
            if (singlePage)
            {
                this.gridControl1.DataSource = this.translate((q.Skip((curPage - 1) * pageSize).Take(pageSize)).ToList());
            }
            else
            {
                this.gridControl1.DataSource = this.translate(q.ToList());
            }
            this.gridView1.FocusedRowHandle = 0;
            this.allcount = q.Count();
        }

        private void MyPagerEvents(int curPage, int pageSize)
        {
            this.curPage = curPage;
            this.pageSize = pageSize;
            FillGridListCtrlQuery(curPage);

        }
        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_query_Click(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }
        #endregion
        private class sysrole
        {
            public long? sysroleid { get; set; }
            public string rolename { get; set; }
            public string num { get; set; }
            public string state { get; set; }
        }
        private List<sysrole> translate(List<Models.sys_role> list)
        {

            var c=list_memberrole.GroupBy(o=>o.sysroleid).Select(p => new
            {
                sysroleid = p.FirstOrDefault().sysroleid,
                num = p.Count()
            });
            list_sysrole = new List<sysrole>();
            var a = list;
            foreach(var b in a)
            {
                sysrole sysrole = new sysrole();
                sysrole.rolename = b.rolename.ToString();
                sysrole.sysroleid = b.sysroleid;
                sysrole.state = b.state.ToString();
               // sysrole.state = (sysrole.state == "0") ? "无效" : "有效";
                var d = c.Where(o => o.sysroleid == sysrole.sysroleid);
                if (d.Count() != 0)
                {
                    foreach (var e in d)
                    {
                        sysrole.num = e.num.ToString();
                    }
                }
                else
                {
                    sysrole.num = "0";
                }
                list_sysrole.Add(sysrole);
            }
            return list_sysrole;
        }

        private void repositoryItemRadioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            int state = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "state"));
            int sysroleid = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "sysroleid"));
            if (state == 1)
            {
                BizEmployee.EnabelPosition(sysroleid, "0");
            }
            else
            {
                BizEmployee.EnabelPosition(sysroleid, "1");
            }
            RefreshGridList();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string rolename = Convert.ToString(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "rolename"));
            int sysroleid = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "sysroleid"));
            if (e.Button.Index == 0)
            {
                MyForm.More.StaffManage.ZhiWeiEidtForm zhiWeiEidt = new MyForm.More.StaffManage.ZhiWeiEidtForm(sysroleid, rolename,1);
                zhiWeiEidt.StartPosition = FormStartPosition.CenterScreen;
                zhiWeiEidt.ShowDialog();

            }
            else
            {
                BizEmployee.DelPositon(sysroleid);
            }
            RefreshGridList();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            MyForm.More.StaffManage.ZhiWeiEidtForm zhiWeiEidt = new MyForm.More.StaffManage.ZhiWeiEidtForm(-1, "", 2);
            zhiWeiEidt.StartPosition = FormStartPosition.CenterScreen;
            zhiWeiEidt.ShowDialog();
            RefreshGridList();
        }
    }
}
