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
    public partial class zongControl : UserControl
    {
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        Biz.BizStorage.QueryDepotOutResult queryDepotOut;
        List<Models.v_depotout_crude> list = new List<Models.v_depotout_crude>();
        public int curPage = 1;
        public int pageSize = 8;
        public int allcount = 0;
        bool isfirst = true;
        List<Models.storage_genre> list_storagegenre;
        int genreid;
        public zongControl()
        {
            InitializeComponent();
            string s_date = DateTime.Now.ToString("yyyy-MM-dd");
            string e_date = DateTime.Now.ToString("yyyy-MM-dd");
            queryDepotOut = BizStorage.QueryDepotOut("", "", "", 0,Convert.ToDateTime(s_date), Convert.ToDateTime( e_date).AddDays(1));
            list = queryDepotOut.depotoutList;
            iniData();
            iniCob();
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
            txt_sumjinjia.Text = queryDepotOut.buymoney.ToString();
            txt_sumshoujia.Text = queryDepotOut.salemoney.ToString();
            txt_sumlirun.Text = (queryDepotOut.salemoney - queryDepotOut.buymoney).ToString();

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
            string categoryname = Txt_shangpinType.Text;
            string crudename = txt_yuanliaoName.Text;
            int genreid = this.genreid;
            if (de_stime.Text == "")
            {
                IniDate();
            }
            DateTime sdate = Convert.ToDateTime(de_stime.Text);
            DateTime edate = Convert.ToDateTime(de_etime.Text);
            edate = edate.AddDays(1);
            queryDepotOut = BizStorage.QueryDepotOut(itemname, categoryname, crudename, genreid, sdate, edate);
            txt_sumjinjia.Text = queryDepotOut.buymoney.ToString();
            txt_sumshoujia.Text = queryDepotOut.salemoney.ToString();
            txt_sumlirun.Text = (queryDepotOut.salemoney - queryDepotOut.buymoney).ToString();
            this.list = queryDepotOut.depotoutList;
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
        }

        private void Txt_shangpinName_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void Txt_shangpinType_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void txt_yuanliaoName_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void txt_yuanliangType_EditValueChanged(object sender, EventArgs e)
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
            if(e.Column.Name== "type")
            {
                if (e.DisplayText == "1")
                {
                    e.DisplayText = "消耗";
                }
                else if(e.DisplayText == "3")
                {
                    e.DisplayText = "出库";
                }
            
            }
        }

        private void cbo_yuanliaoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.genreid = (cbo_yuanliaoType.SelectedIndex == 0) ? 0 : list_storagegenre[cbo_yuanliaoType.SelectedIndex - 1].genreid;
            if (!isfirst)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }
    }
}
