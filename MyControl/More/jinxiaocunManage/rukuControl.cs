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
    public partial class rukuControl : UserControl
    {
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        List<Models.v_depotin_crude> list;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        bool isfirst=true;
        public rukuControl()
        {
            InitializeComponent();
             string s_date = "2017 - 02 - 27";
           // string s_date = DateTime.Now.ToString("yyyy-MM-dd");
            string e_date = DateTime.Now.ToString("yyyy-MM-dd");
            string validate = "2018 - 08 - 03";
            list=BizStorage.QueryDepotIn("", Convert.ToDateTime(validate).AddDays(1),Convert.ToDateTime(s_date),Convert.ToDateTime(e_date).AddDays(1), "", "", "");
            IniDate();
            iniData();
            MyEvent.More.jinxiaocunManage.jinxiaocunEvent.ReplaceEvent += MyReplaceEvent;
        }
        private void MyReplaceEvent()
        {
            this.Controls.RemoveAt(0);
            this.Controls.Add(this.tableLayoutPanel1);
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
            string itemname = Txt_shangpinName.Text;
            string dutyperson = txt_duijieren.Text;
            string deliveryman = txt_jiehuoren.Text;
            string categoryname = txt_shangpinType.Text;
            DateTime sdate = Convert.ToDateTime(de_stime.Text);
            DateTime edate = Convert.ToDateTime(de_etime.Text);
            edate = edate.AddDays(1);
            DateTime validate = Convert.ToDateTime(this.de_zhibaoqi.Text);
            validate = validate.AddDays(1);
            this.list = BizStorage.QueryDepotIn(itemname, validate, sdate, edate, dutyperson, deliveryman, categoryname);
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
        private void IniDate()
        {
            //de_stime.Text = "2017 - 02 - 27";
            this.de_stime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.de_etime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.de_zhibaoqi.Text = DateTime.Now.ToString("yyyy-MM-dd");
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

        private void Txt_shangpinName_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void de_zhibaoqi_EditValueChanged(object sender, EventArgs e)
        {
            if (!isfirst)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void txt_duijieren_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void txt_shangpinType_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void txt_jiehuoren_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Models.v_depotin_crude depotinCrude = new Models.v_depotin_crude();
            MyForm.More.jinxiaocunManage.EidtrukuForm eidtruku = new MyForm.More.jinxiaocunManage.EidtrukuForm(depotinCrude,2);
            eidtruku.StartPosition = FormStartPosition.CenterScreen;
            eidtruku.ShowDialog();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowhandle = this.gridView1.FocusedRowHandle;
            Models.v_depotin_crude  depotinCrude = new Models.v_depotin_crude();
            depotinCrude.productiondate = Convert.ToDateTime(this.gridView1.GetRowCellValue(rowhandle, "productiondate"));
            depotinCrude.cost = Convert.ToDecimal(this.gridView1.GetRowCellValue(rowhandle, "cost"));
            depotinCrude.dutyperson = Convert.ToString(this.gridView1.GetRowCellValue(rowhandle, "dutyperson"));
            depotinCrude.deliveryman = Convert.ToString(this.gridView1.GetRowCellValue(rowhandle, "deliveryman"));
            depotinCrude.tel = Convert.ToString(this.gridView1.GetRowCellValue(rowhandle, "tel"));
            depotinCrude.driver = Convert.ToString(this.gridView1.GetRowCellValue(rowhandle, "driver"));
            depotinCrude.platenum = Convert.ToString(this.gridView1.GetRowCellValue(rowhandle, "platenum"));
            depotinCrude.depotinid = Convert.ToInt32(this.gridView1.GetRowCellValue(rowhandle, "depotinid"));

            if (e.Button.Index == 0)
            {
                MyForm.More.jinxiaocunManage.EidtrukuForm eidtruku = new MyForm.More.jinxiaocunManage.EidtrukuForm(depotinCrude,1);
                eidtruku.StartPosition = FormStartPosition.CenterScreen;
                eidtruku.ShowDialog();
            }else if (e.Button.Index == 1)
            {
                this.Controls.Remove(this.tableLayoutPanel1);
                MyControl.More.jinxiaocunManage.detailrukuControl detailruku = new detailrukuControl(Convert.ToInt32(depotinCrude.depotinid));
                detailruku.Dock = DockStyle.Fill;
                this.Controls.Add(detailruku);
            }
            RefreshGridList();
        }
    }
}
