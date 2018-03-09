using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DianDianClient.MyModels;

namespace DianDianClient.MyControl.BusinessDetails
{
    public partial class OrderDetailControl : UserControl
    {
        Biz.BizBillController billController = new Biz.BizBillController();
        object data;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        List<v_cfmainaccount2> list;
        public OrderDetailControl(object data)
        {
            this.list = (List < v_cfmainaccount2 >) data;
            this.data = data;
            InitializeComponent();
            IniData();
        }

        private void IniData()
        {
            this.gridControl1.DataSource = this.list.Take(10);
            this.tileView1.FocusedRowHandle = 0;
            //字体
            for (int i = 0; i < this.tileView1.TileTemplate.Count; i++)
            {
                if (i == 0)
                {
                    this.tileView1.TileTemplate[i].Appearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(119)))), ((int)(((byte)(51)))));
                    this.tileView1.TileTemplate[i].Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                else
                {
                    this.tileView1.TileTemplate[i].Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
            for (int i = 0; i < this.orderDetailDetailControl1.tileView1.TileTemplate.Count; i++)
            {

                this.orderDetailDetailControl1.tileView1.TileTemplate[i].Appearance.Normal.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
           // int iNo = Convert.ToInt32(this.Txt_where.Text);
            var q=this.list.Where(p =>( p.billNo.Contains(this.Txt_where.Text)||p.tableNo.Equals(this.Txt_where.Text)));
            if (singlePage)
            {
                this.gridControl1.DataSource = (q.Skip((curPage - 1) * pageSize).Take(pageSize)).ToList();
            }
            else
            {
                this.gridControl1.DataSource = Utils.utils.CopyToDataTable(q.ToList());
            }
            this.tileView1.FocusedRowHandle = 0;
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }
        #endregion


        public class cfdetailitem
        {
            public string name { get; set; }
            public string num { get; set; }
        }
        public List<cfdetailitem> translate(List<Models.v_cfdetailitem> list)
        {
            List<cfdetailitem> list_temp = new List<cfdetailitem>();
            foreach (Models.v_cfdetailitem item in list)
            {
                cfdetailitem temp = new cfdetailitem();
                temp.name = item.name;
                temp.num = "X" + item.num;

                list_temp.Add(temp);
            }
            return list_temp;
        }

        private void tileView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (tileView1.FocusedRowHandle >= 0)
            {
                string cfmainkey = this.tileView1.GetRowCellValue(e.FocusedRowHandle, this.tileView1.Columns["cfmainkey"]).ToString();
                string tableNo = this.tileView1.GetRowCellValue(e.FocusedRowHandle, this.tileView1.Columns["tableNo"]).ToString();
                List<Models.v_cfdetailitem> list = billController.GetTableDetailInfo(cfmainkey);
                this.orderDetailDetailControl1.gridControl1.DataSource = translate(list);
                this.orderDetailDetailControl1.Lab_TableNo.Text = tableNo+"号桌";
            }
        }

        private void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {
            tileView1.FocusedRowHandle = 1;
        }

        private void Txt_where_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }
    }
}
