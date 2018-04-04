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
    public partial class yuanliaoControl : UserControl
    {
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        List<Models.v_crude_genre> list;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        bool isfirst = true;
        int genreid = 0;
        List<Models.storage_genre> list_storagegenre;
        public yuanliaoControl()
        {
            InitializeComponent();
            list = BizStorage.QueryCrude("", 0,"","",0);
            iniCob();
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
            allcount = list.Count;
            mgncPager1.RefreshPager(pageSize, allcount, curPage);//更新分页控件显示。
            isfirst = false;
        }
        private void iniCob()
        {
            list_storagegenre = BizStorage.QueryGenre();
            cbo_yuanliaoType.Properties.Items.Clear();
            cbo_yuanliaoType.Properties.Items.Add("全部");
            foreach (var a in list_storagegenre)
            {
                cbo_yuanliaoType.Properties.Items.Add(a.genrename);
            }
            cbo_yuanliaoType.SelectedIndex = 0;
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
            string crudename = txt_yuanliaoName.Text;
            string categoryname = txt_shangpinType.Text;
            int genreid = this.genreid;
            this.list = BizStorage.QueryCrude(crudename, genreid, categoryname, itemname,0);
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
            this.genreid = (cbo_yuanliaoType.SelectedIndex == 0) ? 0 : list_storagegenre[cbo_yuanliaoType.SelectedIndex - 1].genreid;
            if (!isfirst)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            MyForm.More.jinxiaocunManage.EdityuanliaoTypeForm edityuanliaoType = new MyForm.More.jinxiaocunManage.EdityuanliaoTypeForm();
            edityuanliaoType.StartPosition = FormStartPosition.CenterScreen;
            edityuanliaoType.ShowDialog();
            iniCob();
            this.curPage = 1;
            RefreshGridList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MyForm.More.jinxiaocunManage.EdityuanliaoForm edityuanliao = new MyForm.More.jinxiaocunManage.EdityuanliaoForm();
            edityuanliao.StartPosition = FormStartPosition.CenterScreen;
            edityuanliao.ShowDialog();
            this.curPage = 1;
            RefreshGridList();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowhandle = this.gridView1.FocusedRowHandle;
            int crudeid =Convert.ToInt32(this.gridView1.GetRowCellValue(rowhandle, "crudeid"));
            BizStorage.DelCrude(crudeid);
            RefreshGridList();
        }

        private void txt_yuanliaoName_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void txt_shangpinType_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void Txt_shangpinName_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }
    }
}
