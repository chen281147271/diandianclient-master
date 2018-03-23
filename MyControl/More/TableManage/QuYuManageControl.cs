using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

namespace DianDianClient.MyControl.More.TableManage
{
    public partial class QuYuManageControl : UserControl
    {
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        List<Models.dd_table_floor> list;
        List<Models.table_pos> list_pos;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        public QuYuManageControl()
        {
            InitializeComponent();
            list=BizSPInfo.GetFloorList(0);
            iniData();
        }
        public class tablefloor : Models.dd_table_floor
        {
            public string str_state { get; set; }
        }
        public List<tablefloor> translate(List<Models.dd_table_floor> list)
        {
            List<tablefloor> list_temp = new List<tablefloor>();
            foreach (Models.dd_table_floor item in list)
            {
                tablefloor temp = new tablefloor();
                temp.floorid = item.floorid;
                temp.floorname = item.floorname;
                temp.orderno = item.orderno;
                temp.createdate = item.createdate;
                temp.str_state = item.state.ToString();
                temp.ffuwu = item.ffuwu;
                list_temp.Add(temp);
            }
            return list_temp;
        }
        private bool table_empty(int floorid)
        {
            return (this.list_pos.Where(o => o.floorid == floorid && o.enable != 0 && o.isDel != 1).Count() == 0) ? true : false;
        }
        private void iniData()
        {
            this.gridControl1.DataSource = this.translate(list).Take(10);
            this.list_pos = BizSPInfo.GetTableList(0);
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
            this.list = BizSPInfo.GetFloorList(0);
            var q = this.translate(list);
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
        private void btn_fanhui_Click(object sender, EventArgs e)
        {
            MyEvent.More.MoreEvent.Replace(0);
        }

        private void repositoryItemRadioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            int state = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "str_state"));
            int floorid = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "floorid"));

            if (state != 1)
            {
                if (!table_empty(floorid))
                {
                    Utils.utils.ShowTip("提示", "餐桌并非空闲状态!");
                    this.gridView1.SetRowCellValue(this.gridView1.FocusedRowHandle, "state", "1");
                    return;
                }
                else
                {
                    BizSPInfo.SavFloor(floorid, 0);
                }
            }
            else
            {
                BizSPInfo.SavFloor(floorid,1);
            }
            RefreshGridList();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int floorid = Convert.ToInt32(this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "floorid"));
            if (e.Button.Index == 0)
            {
                int OpType = 1;
                string QuYuName = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "floorname").ToString();
                string QuYuNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "floorid").ToString();
                string ffuwu = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ffuwu").ToString();
                MyForm.More.TableManage.QuYuEditForm quYuEdit = new MyForm.More.TableManage.QuYuEditForm(QuYuName, QuYuNo, ffuwu, floorid, OpType);
                quYuEdit.StartPosition = FormStartPosition.CenterScreen;
                quYuEdit.ShowDialog();
            }
            else
            {
                BizSPInfo.DelFloor(floorid);
            }
            RefreshGridList();
        }

        private void btn_addquyu_Click(object sender, EventArgs e)
        {
            MyForm.More.TableManage.QuYuEditForm quYuEdit = new MyForm.More.TableManage.QuYuEditForm("", "", "", -1, 2);
            quYuEdit.StartPosition = FormStartPosition.CenterScreen;
            quYuEdit.ShowDialog();
            RefreshGridList();
        }
    }
}
