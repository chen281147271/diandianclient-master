using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.FoodManagement
{
    public partial class TuiJianForm : Form
    {
        Biz.BIZFoodController BIZFood = new Biz.BIZFoodController();
        List<Models.dd_tuijian> list = new List<Models.dd_tuijian>();
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        public TuiJianForm()
        {
            InitializeComponent();
            list=BIZFood.QueryTuijianList();
            iniData();


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
            this.list = BIZFood.QueryTuijianList();
            var q = list;
            if (singlePage)
            {
                this.gridControl1.DataSource = ((q.Skip((curPage - 1) * pageSize).Take(pageSize)).ToList());
            }
            else
            {
                this.gridControl1.DataSource = (q.ToList());
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
        private void btn_add_Click(object sender, EventArgs e)
        {
            Models.dd_tuijian dd_Tuijian = new Models.dd_tuijian();
            MyForm.FoodManagement.EdittuijianForm edittuijian = new EdittuijianForm(dd_Tuijian);
            edittuijian.StartPosition = FormStartPosition.CenterScreen;
            edittuijian.ShowDialog();

            RefreshGridList();
            this.Close();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            int tjid = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tjid"));
            if (e.Button.Index == 0)
            {
                MyForm.FoodManagement.EdittuijianForm edittuijian = new EdittuijianForm(list.Find(o=>o.tjid== tjid));
                edittuijian.StartPosition = FormStartPosition.CenterScreen;
                edittuijian.ShowDialog();
            }
            else
            {
                BIZFood.DeleteTuijian(tjid);
            }
            RefreshGridList();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
