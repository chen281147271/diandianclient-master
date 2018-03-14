using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.MemberManagement
{
    public partial class RechargeRecordControl : UserControl
    {
        Biz.BizMemberCard memberCard = new Biz.BizMemberCard();
        List<Models.dd_card_userecord> list;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        int itype=0;
        string cardid;
        public RechargeRecordControl(string cardid,int type)//0充值记录 1消费记录
        {
            this.itype = type;
            this.cardid = cardid;
            InitializeComponent();
            //string str = "2015-01-01";
            //DateTime strdt = Convert.ToDateTime(str);
            DateTime? dt = null;
            list= memberCard.QueryCardUseRecord(Convert.ToInt32(cardid), type, dt, DateTime.Now.AddDays(1));
            this.gridControl1.DataSource = list;
            iniData();
        }
        public class ddcarduserecord
        {
            public string usetime { get; set; }
            public string shopkey { get; set; }
            public string consume { get; set; }
            public string type { get; set; }
        }
        public List<ddcarduserecord> translate(List<Models.dd_card_userecord> list)
        {
            List<ddcarduserecord> list_temp = new List<ddcarduserecord>();
            foreach (Models.dd_card_userecord item in list)
            {
                ddcarduserecord temp = new ddcarduserecord();
                temp.consume = item.consume.ToString();
                temp.shopkey = item.shopkey.ToString();
                temp.type = (item.type == 0) ? "充值" : "消费";
                temp.usetime = (item.usetime != null) ? item.usetime.ToString() : "";
                list_temp.Add(temp);
            }
            return list_temp;
        }
        private void iniData()
        {
            radioGroup1.SelectedIndex = (itype == 0) ? 1 : 2;

            this.gridControl1.DataSource = this.translate(list).Take(10);

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
            this.dt_etime.Text = DateTime.Now.ToShortDateString();
            mgncPager1.RefreshPager(pageSize, allcount, curPage);//更新分页控件显示。
        }
        private void Btn_Return_Click(object sender, EventArgs e)
        {
            MyEvent.MemberManagement.RechargeRecordEvent.Close();
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
            //string phone = Txt_phone.Text;
            //string name = Txt_name.Text;
            //DateTime? s_time = null;
            //if (dt_stime.Text.Length > 0)
            //{
            //    s_time = Convert.ToDateTime(dt_stime.Text);
            //}
            //DateTime e_time = Convert.ToDateTime(dt_etime.Text);
            //this.list = MemberCard.QueryMembers(name, phone, s_time, e_time);
            int type=0;
            DateTime? s_time;
            DateTime e_time;
            if (dt_stime.Text.Length > 0)
            {
                s_time = Convert.ToDateTime(dt_stime.Text);
               
            }
            else
            {
                s_time = null;
            }
            e_time = Convert.ToDateTime(dt_etime.Text);
            switch (radioGroup1.SelectedIndex)
            {
                case 0:
                    type = 2;
                    break;
                case 1:
                    type = 0;
                    break;
                case 2:
                    type = 1;
                    break;
            }
            this.list= memberCard.QueryCardUseRecord(Convert.ToInt32(this.cardid), type, s_time, e_time);
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
        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (dt_etime.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (dt_etime.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dt_etime.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }
    }
}
