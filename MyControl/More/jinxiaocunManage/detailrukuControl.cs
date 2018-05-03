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
    public partial class detailrukuControl : UserControl
    {
        Biz.BizStorage bizStorage = new Biz.BizStorage();
        List<Models.v_depotin_crude> list = new List<Models.v_depotin_crude>();
        public int curPage = 1;
        public int pageSize = 8;
        public int allcount = 0;
        int depotinid = 0;
        public detailrukuControl( int depotinid)
        {
            this.depotinid = depotinid;
            InitializeComponent();
            list=bizStorage.QueryDepotDetail(depotinid);
            iniData();
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
            string categoryname = txt_shangpinType.Text;
            list = bizStorage.QueryDepotDetail(depotinid);
            var q = list.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
            if (itemname.Length != 0)
            {
                 q = list.Where(o => o.crudename.Contains(itemname));
            }
            if (categoryname.Length != 0)
            {
                q = list.Where(o => o.crudename.Contains(categoryname));
            }
            if (this.de_zhibaoqi.Text.Length != 0)
            {
                DateTime validate = Convert.ToDateTime(this.de_zhibaoqi.Text);
                validate = validate.AddDays(1);
                q = list.Where(o => o.validity<= validate);
            }
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MyEvent.More.jinxiaocunManage.jinxiaocunEvent.Replace();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            MyForm.More.jinxiaocunManage.EditdetailrukuForm editdetailruku = new MyForm.More.jinxiaocunManage.EditdetailrukuForm(depotinid);
            editdetailruku.StartPosition = FormStartPosition.CenterScreen;
            editdetailruku.ShowDialog();
            this.curPage = 1;
            RefreshGridList();
        }
    }
}
