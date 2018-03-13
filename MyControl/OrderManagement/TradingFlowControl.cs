﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.OrderManagement
{
    public partial class TradingFlowControl : UserControl
    {
        Biz.BizBillController billController = new Biz.BizBillController();
        List<Models.v_cfmainaccount> list;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        public TradingFlowControl()
        {
            string str = "2018-01-01";
            DateTime strdt = Convert.ToDateTime(str);
            // billController.QueryBillList(0,0,DateTime.Now, DateTime.Now);
            this.list = billController.QueryShopAccount(0, "", 0, "", 0, strdt, DateTime.Now);
            InitializeComponent();
            IniData();
        }
        public class vcfmainaccount
        {
            public string tableNo { get; set; }
            public string createDate { get; set; }
            public string billNo { get; set; }
            public string state { get; set; }
            public string money { get; set; }
            public string realPay { get; set; }
            public string type { get; set; }
            public string payway { get; set; }
            public string cfmainkey { get; set; }
        }
        public List<vcfmainaccount> translate(List<Models.v_cfmainaccount> list)
        {
            List<vcfmainaccount> list_temp = new List<vcfmainaccount>();
            foreach (Models.v_cfmainaccount item in list)
            {
                
                vcfmainaccount temp = new vcfmainaccount();
                temp.cfmainkey = item.cfmainkey;
                temp.tableNo = item.tableNo.ToString();
                temp.createDate = string.Format("{0:MM-dd hh:mm}", item.createdate);
                temp.billNo = item.billNo.ToString();
                temp.realPay = item.realPay.ToString();
                temp.type = this.BillType2Name(item.type.ToString());
                temp.payway = (item.payway != null)?item.payway.ToString():"";
                temp.money = item.money.ToString();
                temp.state = (item.state == 5) ? "异常" : "正常";
                list_temp.Add(temp);
            }
            return list_temp;
        }
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
        private void IniData()
        {
            this.gridControl1.DataSource = this.translate(list).Take(10);
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

            this.Com_PayWay.SelectedIndex = 0;
            this.s_time.Text = DateTime.Now.ToShortDateString();
            this.e_time.Text = DateTime.Now.ToShortDateString();

            mgncPager1.RefreshPager(pageSize, allcount, curPage);//更新分页控件显示。
        }
        public string BillType2Name(string type)
        {
            string name = "";
            switch (type)
            {
                case "1":
                    name = "正常消费";
                    break;
                case "2":
                    name = "会员充值";
                    break;
                case "3":
                    name = "会员卡消费";
                    break;
                case "4":
                    name = "扫码付款";
                    break;
                case "5":
                    name = "预定消费";
                    break;
                default:
                    name = "未知";
                    break;
            }

            return name;
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
            var q = this.list;
            if (singlePage)
            {
                this.gridControl1.DataSource = this.translate((q.Skip((curPage - 1) * pageSize).Take(pageSize)).ToList());
            }
            else
            {
                this.gridControl1.DataSource = this.translate(q.ToList());
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
        private void tileView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (tileView1.FocusedRowHandle >= 0)
            {
                string cfmainkey = this.tileView1.GetRowCellValue(e.FocusedRowHandle, this.tileView1.Columns["cfmainkey"]).ToString();
                string tableNo = this.tileView1.GetRowCellValue(e.FocusedRowHandle, this.tileView1.Columns["tableNo"]).ToString();
                List<Models.v_cfdetailitem> list1 = billController.GetTableDetailInfo(cfmainkey);
                this.orderDetailDetailControl1.gridControl1.DataSource = translate(list1);
                this.orderDetailDetailControl1.Lab_TableNo.Text = tableNo + "号桌";
            }
        }
        private void gridControl1_DataSourceChanged(object sender, EventArgs e)
        {
            tileView1.FocusedRowHandle = 1;
        }

        private void tileView1_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            DevExpress.XtraGrid.Views.Tile.TileViewItem tileViewItem = (DevExpress.XtraGrid.Views.Tile.TileViewItem)e.DataItem;
            string cfmainkey = this.tileView1.GetRowCellValue(tileViewItem.RowHandle, this.tileView1.Columns["cfmainkey"]).ToString();
            if (e.Item.Name == "contextButton1")
            {
                MessageBox.Show("再次打印");
            }
            else
            {
                billController.ConfirmBill(Convert.ToInt32(cfmainkey));
            }
        }

        private void Txt_orderNo_EditValueChanged(object sender, EventArgs e)
        {
            if(this.s_time.Text.Length>0&& this.e_time.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void Txt_TableNo_EditValueChanged(object sender, EventArgs e)
        {
            if (this.s_time.Text.Length > 0 && this.e_time.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }
        private void s_time_EditValueChanged(object sender, EventArgs e)
        {
            if (this.s_time.Text.Length > 0 && this.e_time.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void e_time_EditValueChanged(object sender, EventArgs e)
        {
            if (this.s_time.Text.Length > 0 && this.e_time.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void Com_PayWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.s_time.Text.Length > 0 && this.e_time.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void Com_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.s_time.Text.Length > 0 && this.e_time.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void Com_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.s_time.Text.Length > 0 && this.e_time.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }
    }
}
