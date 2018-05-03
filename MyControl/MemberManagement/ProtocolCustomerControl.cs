using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Repository;

namespace DianDianClient.MyControl.MemberManagement
{
    public partial class ProtocolCustomerControl : UserControl
    {
        Biz.BizMemberCard MemberCard = new Biz.BizMemberCard();
        List<Models.dd_shop_signusers> list;
        public int curPage = 1;
        public int pageSize = 8;
        public int allcount = 0;
        public ProtocolCustomerControl()
        {
            InitializeComponent();
            DateTime? sdt = null;
            list = MemberCard.QuerySignUser("", "", sdt, DateTime.Now);
            this.gridControl1.DataSource = list;
            iniData();

            Utils.utils.MessageBoxYesNoResultEvent += MyMessageBoxYesNoResultEvent;

        }
        private void iniData()
        {
            this.gridControl1.DataSource = this.list.Take(pageSize);

            this.gridView1.RowHeight = 50;
            this.gridView1.ColumnPanelRowHeight = 50;
            for (int i = 0; i < this.gridView1.Columns.Count - 1; i++)
            {
                this.gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
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
            DateTime? s_time = null;
            if (dt_stime.Text.Length > 0)
            {
                s_time = Convert.ToDateTime(dt_stime.Text);
            }
            DateTime e_time = Convert.ToDateTime(dt_etime.Text);
            this.list = MemberCard.QuerySignUser(name, phone, s_time, e_time.AddDays(1));
            var q = this.list;
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

        private void Txt_name_EditValueChanged(object sender, EventArgs e)
        {
            if (dt_etime.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
        }

        private void Txt_phone_EditValueChanged(object sender, EventArgs e)
        {
            if (dt_etime.Text.Length > 0)
            {
                this.curPage = 1;
                RefreshGridList();
            }
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
        private void MyMessageBoxYesNoResultEvent(int id)
        {
            //MemberCard.FreezeSignUser(id);
            Utils.utils.ShowTip("提示", "操作已成功完成!");
        }
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                int rowhandle = this.gridView1.FocusedRowHandle;
                Models.dd_shop_signusers dd_Shop_Signusers = new Models.dd_shop_signusers();
                dd_Shop_Signusers.name = this.gridView1.GetRowCellValue(rowhandle, "name").ToString();
                dd_Shop_Signusers.telno = this.gridView1.GetRowCellValue(rowhandle, "telno").ToString();
                dd_Shop_Signusers.maxusenums = Convert.ToInt32(this.gridView1.GetRowCellValue(rowhandle, "maxusenums"));
                dd_Shop_Signusers.maxprice = Convert.ToDecimal(this.gridView1.GetRowCellValue(rowhandle, "maxprice"));
                dd_Shop_Signusers.id = Convert.ToInt32(this.gridView1.GetRowCellValue(rowhandle, "id"));
                if (e.Button.Index == 0)
                {
                    MyForm.MemberManagement.ProtocolDetaileForm protocolDetaile = new MyForm.MemberManagement.ProtocolDetaileForm(dd_Shop_Signusers, 1);
                    protocolDetaile.StartPosition = FormStartPosition.CenterScreen;
                    protocolDetaile.ShowDialog();
                    RefreshGridList();
                }
                else if (e.Button.Index == 1)
                {
                    if (XtraMessageBox.Show("确定清帐？清帐后账单将归零", "清帐确认", MessageBoxButtons.YesNo) == DialogResult.OK)
                    {
                        MemberCard.ClearBillSignUser(dd_Shop_Signusers.id);
                        Utils.utils.ShowTip("提示", "清帐成功!");
                        RefreshGridList();
                    }
                }
            }
            catch { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Models.dd_shop_signusers dd_Shop_Signusers = new Models.dd_shop_signusers();
            MyForm.MemberManagement.ProtocolDetaileForm protocolDetaile = new MyForm.MemberManagement.ProtocolDetaileForm(dd_Shop_Signusers, 0);
            protocolDetaile.StartPosition = FormStartPosition.CenterScreen;
            protocolDetaile.ShowDialog();
            RefreshGridList();
        }
        private void gridView1_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                //if (e.Column.FieldName == "state")
                //{
                //    switch (e.DisplayText)
                //    {
                //        case "0":
                //            e.DisplayText = "正常";

                //            break;
                //        case "1":
                //            e.DisplayText = "冻结";
                //            break;
                //    }
                //}
                //if (e.Column.Name == "operate")
                //{
                //    switch (e.DisplayText)
                //    {
                //        case "0":
                //            if ((e.Column.ColumnEdit as RepositoryItemButtonEdit).Buttons[1].Caption != "冻结")
                //            {
                //                (e.Column.ColumnEdit as RepositoryItemButtonEdit).Buttons[1].Caption = "冻结";
                //            }
                //            break;
                //        case "1":
                //            if ((e.Column.ColumnEdit as RepositoryItemButtonEdit).Buttons[1].Caption != "解冻")
                //            {
                //                (e.Column.ColumnEdit as RepositoryItemButtonEdit).Buttons[1].Caption = "解冻";
                //            }
                //            break;
                //    }
                //}
            }
            catch { }
        }
        private void repositoryItemRadioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            string id = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "id").ToString();
            string state = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "state").ToString();

            if (state == "0")
            {
                if (XtraMessageBox.Show("确定冻结？冻结后将不能对此用户操作", "冻结确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MemberCard.FreezeSignUser(Convert.ToInt32( id),1);
                    Utils.utils.ShowTip("提示", "冻结成功!");
                }
            }
            else
            {
                if (XtraMessageBox.Show("确定解冻？", "解冻确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    MemberCard.FreezeSignUser(Convert.ToInt32(id),0);
                    Utils.utils.ShowTip("提示", "解冻成功!");
                }

            }
            RefreshGridList();

        }
    }
}
