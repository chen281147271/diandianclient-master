using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace DianDianClient.MyControl.MemberManagement
{
    public partial class MemberQueryControl : UserControl
    {
        Biz.BizMemberCard MemberCard = new Biz.BizMemberCard();
        DateTime? s_time =null;
        DateTime e_time = DateTime.Now;
        List<Models.dd_mem_card> list;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        public MemberQueryControl()
        {
            InitializeComponent();
            list= MemberCard.QueryMembers("", "", s_time, e_time);
            iniData();
        }
        public class ddmemcard
        {
            public string cardid { get; set; }
            public string addtime { get; set; }
            public string isvalid { get; set; }
            public string realname { get; set; }
            public string telno { get; set; }
            public string cardno { get; set; }
            public string money { get; set; }
            public string birthday { get; set; }
            public string expirydate { get; set; }
        }
        public List<ddmemcard> translate(List<Models.dd_mem_card> list)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            List<ddmemcard> list_temp = new List<ddmemcard>();
            foreach (Models.dd_mem_card item in list)
            {
                ddmemcard temp = new ddmemcard();
                temp.addtime = (item.addtime != null) ? startTime.AddSeconds(Convert.ToDouble(item.addtime)).ToShortDateString() : "";
                temp.birthday = (item.birthday != null) ? item.birthday : "";
                temp.cardid = item.cardid.ToString();
                temp.cardno = (item.cardno != null) ? item.cardno : "";
                temp.expirydate = (item.expirydate != null) ? startTime.AddSeconds(Convert.ToDouble(item.expirydate)).ToShortDateString() : "";
                temp.isvalid = item.isvalid.ToString();
                temp.money= item.money.ToString();
                temp.realname= (item.realname != null) ? item.realname : "";
                temp.telno= (item.telno != null) ? item.telno : "";
                list_temp.Add(temp);
            }
            return list_temp;
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowhandle = this.gridView1.FocusedRowHandle;
            MyModels.ddmemcard2 ddmemcard2 = new MyModels.ddmemcard2();
            ddmemcard2.addtime = this.gridView1.GetRowCellValue(rowhandle, "addtime").ToString();
            ddmemcard2.birthday = this.gridView1.GetRowCellValue(rowhandle, "birthday").ToString();
            ddmemcard2.cardid = this.gridView1.GetRowCellValue(rowhandle, "cardid").ToString();
            ddmemcard2.cardno = this.gridView1.GetRowCellValue(rowhandle, "cardno").ToString();
            ddmemcard2.expirydate = this.gridView1.GetRowCellValue(rowhandle, "expirydate").ToString();
            ddmemcard2.money = this.gridView1.GetRowCellValue(rowhandle, "money").ToString();
            ddmemcard2.telno = this.gridView1.GetRowCellValue(rowhandle, "telno").ToString();
            ddmemcard2.realname = this.gridView1.GetRowCellValue(rowhandle, "realname").ToString();
            if (e.Button.Index == 0)
            {
                MyForm.MemberManagement.MemberDetaileForm memberDetaileForm = new MyForm.MemberManagement.MemberDetaileForm(ddmemcard2);
                memberDetaileForm.StartPosition = FormStartPosition.CenterScreen;
                memberDetaileForm.ShowDialog();

            }
            else
            {
                MyForm.MemberManagement.RechargeForm rechargeForm = new MyForm.MemberManagement.RechargeForm(ddmemcard2);
                rechargeForm.StartPosition = FormStartPosition.CenterScreen;
                rechargeForm.ShowDialog();
                RefreshGridList();
            }
        }
        private void iniData()
        {
            this.gridControl1.DataSource = this.translate(list).Take(10);

            this.gridView1.RowHeight = 50;
            this.gridView1.ColumnPanelRowHeight = 50;

            this.dt_etime.Text = DateTime.Now.ToShortDateString();
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
            string phone = Txt_phone.Text;
            string name = Txt_name.Text;
            DateTime? s_time=null;
            if (dt_stime.Text.Length > 0)
            {
                s_time = Convert.ToDateTime(dt_stime.Text);
            }
            DateTime e_time = Convert.ToDateTime(dt_etime.Text);
            this.list = MemberCard.QueryMembers(name, phone, s_time, e_time.AddDays(1));
            var q = this.list;
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

        private void Txt_name_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void Txt_phone_EditValueChanged(object sender, EventArgs e)
        {
            this.curPage = 1;
            RefreshGridList();
        }

        private void dt_stime_EditValueChanged(object sender, EventArgs e)
        {
            if (dt_etime.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void dt_etime_EditValueChanged(object sender, EventArgs e)
        {
            if (dt_etime.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MyForm.MemberManagement.AddForm addForm = new MyForm.MemberManagement.AddForm();
            addForm.StartPosition = FormStartPosition.CenterScreen;
            addForm.ShowDialog();

            this.curPage = 1;
            RefreshGridList();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MyEvent.MemberManagement.MemberDetaileEvent.Close_Rule();
        }
    }
}
