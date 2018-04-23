using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DianDianClient.MyControl.BusinessDetails
{
    public partial class FoodStatisticsControl : UserControl
    {
        Biz.BizBusinessAnalysis BizBusiness = new Biz.BizBusinessAnalysis();
        List<Biz.BizBusinessAnalysis.StatisticItemBean> list;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        bool isfirst = true;
        public FoodStatisticsControl()
        {
            InitializeComponent();
            string stime = "2017-01-01";
            string etime = "2019-01-01";
            this.de_stime.Text = stime;
            this.de_etime.Text = etime;
            this.radioGroup1.SelectedIndex = 0;
            this.radioGroup2.SelectedIndex = 0;
            this.radioGroup3.SelectedIndex = 0;
            list =BizBusiness.QueryStatisticItem("", "", 1, 1, 1, Convert.ToDateTime(stime), Convert.ToDateTime(etime));
            iniData();
            isfirst = false;
        }
        private void iniData()
        {
            this.gridControl1.DataSource = (list).Take(10);
            this.gridView1.RowHeight = 50;
            this.gridView1.ColumnPanelRowHeight = 50;

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
        private void UpGridHead(int issum, int bycategory)
        {
            if (bycategory == 0)
            {
                if (issum == 1)
                {
                    this.gridView1.Columns["createdate"].Visible = false;
                    //this.gridView1.Columns["createdate"].VisibleIndex = 0;
                    this.gridView1.Columns["caegoryname"].Visible = true;
                    this.gridView1.Columns["caegoryname"].VisibleIndex = 0;
                    this.gridView1.Columns["name"].Visible = true;
                    this.gridView1.Columns["name"].VisibleIndex = 1;
                    this.gridView1.Columns["sellnum"].Visible = true;
                    this.gridView1.Columns["sellnum"].VisibleIndex = 2;
                    this.gridView1.Columns["excepnum"].Visible = true;
                    this.gridView1.Columns["excepnum"].VisibleIndex = 3;
                    this.gridView1.Columns["shixiao"].Visible = true;
                    this.gridView1.Columns["shixiao"].VisibleIndex = 4;
                    this.gridView1.Columns["price"].Visible = true;
                    this.gridView1.Columns["price"].VisibleIndex = 5;
                    this.gridView1.Columns["amount"].Visible = true;
                    this.gridView1.Columns["amount"].VisibleIndex = 6;
                }
                else
                {
                    this.gridView1.Columns["createdate"].Visible = false;
                    this.gridView1.Columns["caegoryname"].Visible = true;
                    this.gridView1.Columns["caegoryname"].VisibleIndex = 0;
                    this.gridView1.Columns["name"].Visible = false;
                    this.gridView1.Columns["sellnum"].Visible = true;
                    this.gridView1.Columns["sellnum"].VisibleIndex = 1;
                    this.gridView1.Columns["excepnum"].Visible = true;
                    this.gridView1.Columns["excepnum"].VisibleIndex = 2;
                    this.gridView1.Columns["shixiao"].Visible = true;
                    this.gridView1.Columns["shixiao"].VisibleIndex = 3;
                    this.gridView1.Columns["price"].Visible = false;
                    this.gridView1.Columns["amount"].Visible = true;
                    this.gridView1.Columns["amount"].VisibleIndex = 4;
                }
            }
            else
            {
                if (bycategory == 1)
                {
                    this.gridView1.Columns["createdate"].Visible = true;
                    this.gridView1.Columns["createdate"].VisibleIndex = 0;
                    this.gridView1.Columns["caegoryname"].Visible = true;
                    this.gridView1.Columns["caegoryname"].VisibleIndex = 1;
                    this.gridView1.Columns["name"].Visible = true;
                    this.gridView1.Columns["name"].VisibleIndex = 2;
                    this.gridView1.Columns["sellnum"].Visible = true;
                    this.gridView1.Columns["sellnum"].VisibleIndex = 3;
                    this.gridView1.Columns["excepnum"].Visible = true;
                    this.gridView1.Columns["excepnum"].VisibleIndex = 4;
                    this.gridView1.Columns["shixiao"].Visible = true;
                    this.gridView1.Columns["shixiao"].VisibleIndex = 5;
                    this.gridView1.Columns["price"].Visible = true;
                    this.gridView1.Columns["price"].VisibleIndex = 6;
                    this.gridView1.Columns["amount"].Visible = true;
                    this.gridView1.Columns["amount"].VisibleIndex = 7;
                }
                else
                {
                    this.gridView1.Columns["createdate"].Visible = true;
                    this.gridView1.Columns["createdate"].VisibleIndex = 0;
                    this.gridView1.Columns["caegoryname"].Visible = true;
                    this.gridView1.Columns["caegoryname"].VisibleIndex = 1;
                    this.gridView1.Columns["name"].Visible = false;
                    this.gridView1.Columns["sellnum"].Visible = true;
                    this.gridView1.Columns["sellnum"].VisibleIndex = 2;
                    this.gridView1.Columns["excepnum"].Visible = true;
                    this.gridView1.Columns["excepnum"].VisibleIndex = 3;
                    this.gridView1.Columns["shixiao"].Visible = true;
                    this.gridView1.Columns["shixiao"].VisibleIndex = 4;
                    this.gridView1.Columns["price"].Visible = false;
                    this.gridView1.Columns["amount"].Visible = true;
                    this.gridView1.Columns["amount"].VisibleIndex = 5;
                }
            }
        }
        private void BindGrid(bool singlePage = true)//单页，所有     
        {
            int issum = (radioGroup2.SelectedIndex == 0) ? 1 : 0;
            int bycategory = (radioGroup1.SelectedIndex == 0) ? 0 : 1;
            int order = (radioGroup3.SelectedIndex == 0) ? 0 : 1;
            UpGridHead(issum, bycategory);
            string itemname = txt_foodname.Text;
            string categoryname = txt_categoryname.Text;
            list = BizBusiness.QueryStatisticItem(itemname, categoryname, bycategory, issum, order, Convert.ToDateTime(de_stime.Text), Convert.ToDateTime(de_etime.Text));
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

        private void txt_foodname_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void txt_categoryname_EditValueChanged(object sender, EventArgs e)
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

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //实销 存在问题 不知道怎么拼出来的
            if (e.Column.Name == "shixiao")
            {
                int sellnum = Convert.ToInt32(this.gridView1.GetRowCellValue(e.ListSourceRowIndex, "sellnum"));
                int excepnum = Convert.ToInt32(this.gridView1.GetRowCellValue(e.ListSourceRowIndex, "excepnum"));
                int shixiao = sellnum - excepnum;
                e.DisplayText = shixiao.ToString();
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isfirst)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isfirst)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void radioGroup3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isfirst)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }
    }
}
