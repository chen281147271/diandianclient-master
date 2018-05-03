using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.jinxiaocunManage
{
    public partial class sunyiControl : UserControl
    {
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        List<Models.v_storagelossorspill_crude> list;
        public int curPage = 1;
        public int pageSize = 8;
        public int allcount = 0;
        bool isfirst = true;
        int genreid = 0;
        List<Models.storage_genre> list_storagegenre;
        public sunyiControl()
        {
            InitializeComponent();
            string s_date = "2017 - 02 - 27";
            // string s_date = DateTime.Now.ToString("yyyy-MM-dd");
            string e_date = DateTime.Now.ToString("yyyy-MM-dd");
            de_stime.Text = s_date;
            de_etime.Text = e_date;
            list = BizStorage.QueryLossOrSpillInfo("", "", "", 0, Convert.ToDateTime(s_date),Convert.ToDateTime(e_date).AddDays(1));
            iniCob();
            iniData();
        }
        private void iniCob()
        {
            list_storagegenre = BizStorage.QueryGenre();
            cbo_yuanliaoType.Properties.Items.Add("全部");
            foreach (var a in list_storagegenre)
            {
                cbo_yuanliaoType.Properties.Items.Add(a.genrename);
            }
            cbo_yuanliaoType.SelectedIndex = 0;
        }
        private void iniData()
        {
            this.gridControl1.DataSource = (list).Take(pageSize);
            this.gridView1.RowHeight = 50;
            this.gridView1.ColumnPanelRowHeight = 50;
            gridView1.OptionsBehavior.Editable = false;
            foreach (DevExpress.XtraGrid.Columns.GridColumn gc in this.gridView1.Columns)
            {
                gc.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                gc.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            //分页
            mgncPager1.myPagerEvents += MyPagerEvents; //new MgncPager.MyPagerEvents(MyPagerEvents);
            mgncPager1.exportEvents += ExportEvents;// new MgncPager.ExportEvents(ExportEvents);
            //必须更新allcount！！！！！！！！！！！！！！！！！！！
            allcount = list.Count;
            mgncPager1.RefreshPager(pageSize, allcount, curPage);//更新分页控件显示。
            isfirst = false;
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
            string itemname = txt_shangpinName.Text;
            string crudename = Txt_yuanliaoname.Text;
            string categoryname = txt_shangpinType.Text;
            int genreid = this.genreid;
            DateTime sdate = Convert.ToDateTime(de_stime.Text);
            DateTime edate = Convert.ToDateTime(de_etime.Text);
            edate = edate.AddDays(1);
            this.list = BizStorage.QueryLossOrSpillInfo(itemname, categoryname, crudename, genreid, sdate, edate);
            var q = (list);
            if (singlePage)
            {
                this.gridControl1.DataSource = (q.Skip((curPage - 1) * pageSize).Take(pageSize)).ToList();
            }
            else
            {
                this.gridControl1.DataSource = q.ToList();
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

        private void cbo_yuanliaoType_SelectedIndexChanged(object sender, EventArgs e)
        {
          this.genreid=(cbo_yuanliaoType.SelectedIndex==0)?0:list_storagegenre[cbo_yuanliaoType.SelectedIndex-1].genreid;
            if (!isfirst)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void Txt_yuanliaoname_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void txt_shangpinName_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void txt_shangpinType_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void de_stime_EditValueChanged(object sender, EventArgs e)
        {
            if (!isfirst)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void de_etime_EditValueChanged(object sender, EventArgs e)
        {
            if (!isfirst)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            MyForm.More.jinxiaocunManage.EditsunyiForm editsunyi = new MyForm.More.jinxiaocunManage.EditsunyiForm();
            editsunyi.StartPosition = FormStartPosition.CenterScreen;
            editsunyi.ShowDialog();
            this.curPage = 1;
            RefreshGridList();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.Name== "type")
            {
                e.DisplayText = (e.DisplayText == "1") ? "损" : "益";
            }
        }
    }
}
