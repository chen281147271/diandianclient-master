using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DianDianClient.Utils;
using DevExpress.XtraEditors;

namespace DianDianClient.MyControl.More.TableManage
{
    public partial class TableManageControl : UserControl
    {
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        List<Models.table_pos> list_pos;
        List<Models.dd_table_floor> list_floor;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        RepositoryItem _disabledItem;
        RepositoryItemCheckEdit CheckItem = new RepositoryItemCheckEdit();
        const string gcCheckFieldName = "isCheck";
        int? itoolStripCheck = null;
        string strtoolStripCheck = "";
        public TableManageControl()
        {
            InitializeComponent();
            initoolstrip();
            iniData();
        }
        private void iniData()
        {
            this.gridControl1.DataSource = this.translate(list_pos).Take(10);

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
            allcount = this.list_pos.Count;
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
            this.list_pos = BizSPInfo.GetTableList(0);
            var q = (this.strtoolStripCheck == "餐桌管理")?this.list_pos: this.list_pos.Where(o => o.floorid == this.itoolStripCheck);
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
        public class tablepos
        {
            public string tableName { get; set; }
            public string floorname{ get; set; }
            public string isRoom { get; set; }
            public string peopleNum { get; set; }
            public string enable { get; set; }
            public string tfuwu { get; set; }
            public string qrCode { get; set; }
            public string tableposkey { get; set; }
            public string isCheck { get; set; }
            public string tableNo { get; set; }
            public string floorid { get; set; }
        }
        public List<tablepos> translate(List<Models.table_pos> list)
        {
            List<tablepos> list_temp = new List<tablepos>();
            foreach (Models.table_pos item in list)
            {
                tablepos temp = new tablepos();
                temp.floorname = "";
                var c = list_floor.Where(o => o.floorid == item.floorid);
                foreach (var d in c)
                {
                    temp.floorname = d.floorname;
                }
                temp.isRoom =(item.isRoom==0)?"否":"是";
                temp.peopleNum = item.peopleNum.ToString();
                temp.enable = item.enable.ToString();
                temp.tfuwu = item.tfuwu.ToString();
                temp.tableName = (item.tableName!=null)? item.tableName:"";
                temp.qrCode = (item.qrCode != null) ? item.qrCode : "";
                if (temp.qrCode == "")
                {
                    temp.isCheck = "-1";
                }
                else
                {
                    temp.isCheck = "0";
                }
                temp.tableposkey = item.tableposkey.ToString();
                temp.tableNo = item.tableNo.ToString();
                temp.floorid = item.floorid.ToString();
                list_temp.Add(temp);
            }
            return list_temp;
        }

        private void initoolstrip()
        {
            Font font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("tag", typeof(String));


            list_pos =BizSPInfo.GetTableList(0);
            var a = list_pos.GroupBy(o => o.floorid).Select(o=>new { id=o.Key });
            list_floor=BizSPInfo.GetFloorList();
            dt.Rows.Add(new object[] { "餐桌管理", 0 });
            int i = 0;
            foreach (var b in a)
            {
                var c = list_floor.Where(o => o.floorid == b.id);
                if (c.Count() != 0)
                {
                    foreach (var d in c)
                    {
                        dt.Rows.Add(new object[] { d.floorname, b.id });
                    }
                }
                else
                {
                    dt.Rows.Add(new object[] { "其他"+i++.ToString(), b.id });
                }
            }
            Color color = System.Drawing.Color.FromArgb(200, 32, 15, 17);
            ToolStripItemBind(toolStrip1, dt, color,"Text", font, toolStripItem_Click,"tag");
        }
        private void ToolStripItemBind(ToolStrip toolStrip, DataTable dataTable, Color ToolStripBackColor, string TextColumn, Font font, EventHandler eventHandler,string tagColumn)
        {
            int iCount = 0;
            toolStrip.Refresh();
            toolStrip.Items.Clear();
            toolStrip.Font = font;
            iCount = dataTable.Rows.Count;
            ToolStripItem[] toolStripItem = new ToolStripItem[iCount];
            for (int i = 0; i < iCount; i++)
            {
                toolStripItem[i] = new ToolStripButton();
                toolStripItem[i].DisplayStyle = ToolStripItemDisplayStyle.Text;
                toolStripItem[i].Text = dataTable.Rows[i][TextColumn].ToString();
                toolStripItem[i].AutoSize = false;
                toolStripItem[i].Size = new Size(150, 100);
                toolStripItem[i].Click += eventHandler;
                toolStripItem[i].ForeColor = Color.White;
                toolStripItem[i].Margin = new System.Windows.Forms.Padding(0);
                toolStripItem[i].Padding = new Padding(0);
                toolStripItem[i].BackColor = ToolStripBackColor;
                toolStripItem[i].Tag= dataTable.Rows[i][tagColumn].ToString();
            }
            toolStrip.Items.AddRange(toolStripItem);
            toolStrip.BackColor = ToolStripBackColor;
            toolStrip.Items[0].PerformClick();
            toolStrip.RenderMode = ToolStripRenderMode.System;
            toolStrip.Refresh();
        }
        private void toolStripItem_Click(object sender, EventArgs e)
        {
            //数据库操作
            AllDisabel();
            ToolStripButton toolStripButton = (ToolStripButton)sender;
            toolStripButton.ForeColor = Color.Black;
            toolStripButton.BackColor = Color.White;
            //this.itoolStripCheck = (toolStripButton.Tag.ToString()=="")?null: Convert.ToInt32(toolStripButton.Tag);
            if(toolStripButton.Tag.ToString() == "")
            {
                this.itoolStripCheck = null;
            }
            else
            {
                this.itoolStripCheck = Convert.ToInt32(toolStripButton.Tag);
            }
            //itoolStripCheck =Convert.ToInt32(toolStripButton.Tag);
            strtoolStripCheck = toolStripButton.Text;
            this.curPage = 1;
            RefreshGridList();
            // MessageBox.Show(sender.ToString());

        }
        private void AllDisabel()
        {
            foreach (ToolStripItem ti in toolStrip1.Items)
            {
                ti.BackColor = Color.FromArgb(200, 32, 15, 17);
                ti.ForeColor = Color.White;
            }
        }
        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            string qr_code = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "qrCode").ToString();
            string tableposkey = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tableposkey").ToString();
            qr_code = (qr_code == "")?"0":qr_code;
            MyForm.More.TableManage.QRCodeForm qRCode = new MyForm.More.TableManage.QRCodeForm(Convert.ToInt32(tableposkey), Convert.ToInt32(qr_code));
            qRCode.StartPosition = FormStartPosition.CenterScreen;
            qRCode.ShowDialog();
        }

        private void repositoryItemRadioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string enable = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "enable").ToString();
                string tableposkey = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tableposkey").ToString();
                string isRoom = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "isRoom").ToString();
                string tableNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tableNo").ToString();
                string tableName = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tableName").ToString();
                string peopleNum = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "peopleNum").ToString();
                string tfuwu = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tfuwu").ToString();
                string floorid = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "floorid").ToString();
                enable = (enable == "在用") ? "1" : "0";
                isRoom = (isRoom == "是") ? "1" : "0";
                // BizSPInfo.ModifyTable(Convert.ToInt32(tableposkey), Convert.ToInt32(isRoom), Convert.ToInt32(tableNo), tableName, Convert.ToInt32(peopleNum), Convert.ToDecimal(tfuwu), Convert.ToInt32(floorid));
                BizSPInfo.EnbaleTable(Convert.ToInt32(tableposkey), Convert.ToInt32(enable));
            }
            catch
            {

            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.Name != "isCheck")//需要设置的列名
                return;
            if (_disabledItem == null)
            {
                // _disabledItem = (RepositoryItem)e.RepositoryItem.Clone();
                _disabledItem = new RepositoryItem();
                _disabledItem.ReadOnly = true;
                _disabledItem.Enabled = false;
            }
            //判断条件
            string  code = this.gridView1.GetRowCellValue(e.RowHandle, "qrCode").ToString();
            if (code=="")
            //    return;
            ////满足条件，设置成只读
            //if (electric.IsLimited)
                e.RepositoryItem = _disabledItem;
        }

        private void gridView1_CustomDrawColumnHeader(object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            //GridView _view = sender as GridView;
            //_view.DrawHeaderCheckBox(CheckItem, gcCheckFieldName, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < this.gridView1.RowCount; i++)
                {
                    if (this.gridView1.GetRowCellValue(i, "isCheck").ToString()!="-1")
                    {
                        this.gridView1.SetRowCellValue(i, "isCheck", 1);
                    }
                }
            }
        }

        private void Btn_export_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("tableName", typeof(String)); 
            dt.Columns.Add("qrCode", typeof(String));
            dt.Columns.Add("tableposkey", typeof(String));
            for (int i = 0; i < this.gridView1.RowCount; i++)
            {
                if (this.gridView1.GetRowCellValue(i, "isCheck").ToString() == "1")
                {
                    dt.Rows.Add(new object[] { this.gridView1.GetRowCellValue(i, "tableName").ToString(), this.gridView1.GetRowCellValue(i, "qrCode").ToString() , this.gridView1.GetRowCellValue(i, "tableposkey").ToString() });
                }
            }
            MyForm.More.TableManage.ExportForm export = new MyForm.More.TableManage.ExportForm(dt);
            export.StartPosition = FormStartPosition.CenterScreen;
            export.ShowDialog();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        { 
            string peopleNum = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "peopleNum").ToString();
            string tfuwu = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tfuwu").ToString();
            string floorname = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "floorname").ToString();
            string floorid = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "floorid").ToString();
            string tableposkey = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tableposkey").ToString();
            string isRoom = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "isRoom").ToString();
            string tableNo = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tableNo").ToString();
            string tableName = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "tableName").ToString();
            isRoom = (isRoom == "是") ? "1" : "0";
            if (e.Button.Index == 0)
            {
                MyForm.More.TableManage.EditForm edit = new MyForm.More.TableManage.EditForm(list_floor, peopleNum, tfuwu, floorname, floorid,tableposkey,isRoom,tableNo,tableName);
                edit.StartPosition = FormStartPosition.CenterScreen;
                edit.ShowDialog();
            }
            else
            {
                if (XtraMessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    BizSPInfo.DelTable(Convert.ToInt32(tableposkey), 1);
                }
            }
            RefreshGridList();
            initoolstrip();
        }

        private void btn_addtable_Click(object sender, EventArgs e)
        {
            MyForm.More.TableManage.AddTableForm addTable = new MyForm.More.TableManage.AddTableForm(list_floor);
            addTable.StartPosition = FormStartPosition.CenterScreen;
            addTable.ShowDialog();
            RefreshGridList();
            initoolstrip();
        }

        private void Btn_quyu_Click(object sender, EventArgs e)
        {
            MyEvent.More.MoreEvent.Replace(1001);
        }
    }
}
