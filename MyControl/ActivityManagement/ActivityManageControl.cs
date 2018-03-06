using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraEditors;
using DevExpress.Utils;

namespace DianDianClient.MyControl.ActivityManagement
{
    public partial class ActivityManageControl : UserControl
    {
        DataTable table;
        List<Models.dd_coupons> list;
        string strtoolStrip = "";//选中了谁
        string itoolStrip = "3";//选中了谁
        Biz.BizPromotionActivities BizPromotionActivities = new Biz.BizPromotionActivities();
        public ActivityManageControl()
        {
            InitializeComponent();
            //    SetupView();
            this.tileView1.TileTemplate[0].Appearance.Normal.BackColor = Color.FromArgb(58, 166, 101);
            this.tileView1.TileTemplate[0].Text = "减";
            this.tileView1.TileTemplate[1].Text = "类型:";
            this.tileView1.TileTemplate[2].Text = "规则:";
            this.tileView1.TileTemplate[3].Text = "日期:";
            this.tileView1.TileTemplate[8].Text = " ";
            this.tileView1.TileTemplate[8].Width = 3000;
            this.tileView1.TileTemplate[8].Height =1;
            this.tileView1.TileTemplate[8].Appearance.Normal.BackColor = Color.FromArgb(100,224, 224, 224);
            //
            this.tileView1.ContextButtons[0].Click += contextButton1_click;
            this.tileView1.ContextButtons[1].Click += contextButton2_click;

            initoolstrip();

        }
        private DataTable CreateTableData(int State=3)
        {

            list= BizPromotionActivities.QueryActivities();
            table = new DataTable("Table1"); 
            table.Columns.Add("couponid", typeof(Int32));
            table.Columns.Add("Title", typeof(String));
            table.Columns.Add("Type", typeof(String));
            table.Columns.Add("Rule", typeof(String));
            table.Columns.Add("DateTime", typeof(String));
            table.Columns.Add("StateGroup", typeof(Int32));

            for(int i = 0; i < list.Count; i++)
            {
                int couponid = list[i].couponid;
                string Title = list[i].cname;
                string Type = "满减";
                string Rule = "满" + list[i].minmoney + "元减," + list[i].okjian;
                string strDateTime = list[i].sdate + "至" + list[i].edate;
                int StateGroup = 0;//0 还没开始 1进行中 2已结束  3全部
                if(Convert.ToDateTime(list[i].sdate)>DateTime.Now)
                {
                    StateGroup = 0;
                }else if(Convert.ToDateTime(list[i].edate)>= DateTime.Now)
                {
                    StateGroup = 1;
                }else if(DateTime.Now> Convert.ToDateTime(list[i].edate))
                {
                    StateGroup = 2;
                }
                table.Rows.Add(new object[] { couponid,Title , Type , Rule , strDateTime , StateGroup });
            }
            if (State != 3)
            {
                var q = from tb in table.AsEnumerable()
                        where tb.Field<Int32>("StateGroup").Equals(State)
                        select new
                        {
                            couponid = tb.Field<Int32>("couponid"),
                            Title = tb.Field<String>("Title"),
                            Type = tb.Field<String>("Type"),
                            Rule = tb.Field<String>("Rule"),
                            DateTime = tb.Field<String>("DateTime"),
                            StateGroup = tb.Field<Int32>("StateGroup")
                        };

                return Utils.utils.CopyToDataTable(q.ToList());
            }
            else
            {
                return table;
            }

        }
        private void contextButton1_click(object send , ContextItemClickEventArgs e)
        {
            int couponid = Convert.ToInt32(e.Item.Tag);
            MyForm.ActivityManagement.ActivityManageDeatilForm deatilForm = new MyForm.ActivityManagement.ActivityManageDeatilForm(list.Find(s => s.couponid == couponid));
            deatilForm.StartPosition = FormStartPosition.CenterScreen;
            deatilForm.ShowDialog();
            RefreshGrid();
        }
        private void contextButton2_click(object send, ContextItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("你确定要作废改优惠吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                BizPromotionActivities.DisableActivity(Convert.ToInt32(e.Item.Tag));
            }
        }
        private void initoolstrip()
        {
            Font font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("iColor", typeof(Int32));

            dt.Rows.Add(new object[] { "待开始", 999 });
            dt.Rows.Add(new object[] { "进行中", 999 });
            dt.Rows.Add(new object[] { "已结束", 999 });
            dt.Rows.Add(new object[] { "全部", 999 });
            dt.Rows.Add(new object[] { "添加优惠", 999 });

            Color color = System.Drawing.Color.FromArgb(200, 32, 15, 17);
            ToolStripItemBind(toolStrip1, dt, color, "iColor", "Text", font, toolStripItem_Click);
        }
        private void ToolStripItemBind(ToolStrip toolStrip, DataTable dataTable, Color ToolStripBackColor, string ForeColorColumn, string TextColumn, Font font, EventHandler eventHandler)
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
                toolStripItem[i].Margin = new System.Windows.Forms.Padding(5, 0, 10, 0);
                toolStripItem[i].BackColor = Color.Transparent;
                toolStripItem[i].Name = i.ToString();
            }
            toolStrip.Items.AddRange(toolStripItem);
            toolStrip.BackColor = ToolStripBackColor;
            toolStrip.Items[3].PerformClick();
            strtoolStrip = "全部";
            itoolStrip = "3";
            toolStrip.Refresh();
        }
        private void toolStripItem_Click(object sender, EventArgs e)
        {
            //数据库操作
            ToolStripItem toolStripItem = (ToolStripItem)sender;
            AllDisabel();
            ToolStripButton toolStripButton = (ToolStripButton)sender;
            toolStripButton.ForeColor = Color.Black;
            toolStripButton.BackColor = Color.White;
            if (sender.ToString() != "添加优惠")
            {
                strtoolStrip = sender.ToString();
                itoolStrip = toolStripItem.Name;
                bindData(sender.ToString());
            }
            else
            {
                Models.dd_coupons _Coupons=null;
                toolStrip1.Items[itoolStrip].BackColor = Color.White;
                toolStrip1.Items[itoolStrip].ForeColor = Color.Black;
                MyForm.ActivityManagement.ActivityManageDeatilForm deatilForm = new MyForm.ActivityManagement.ActivityManageDeatilForm(_Coupons);
                deatilForm.StartPosition = FormStartPosition.CenterScreen;
                deatilForm.ShowDialog();
                RefreshGrid();
            }
        }
        void RefreshGrid()
        {
            bindData(strtoolStrip);
        }
        void bindData(string type)
        {
            switch (type)
            {
                case "全部":
                    gridControl1.DataSource = CreateTableData(3);
                    break;
                case "待开始":
                    gridControl1.DataSource = CreateTableData(0);
                    break;
                case "进行中":
                    gridControl1.DataSource = CreateTableData(1);
                    break;
                case "已结束":
                    gridControl1.DataSource = CreateTableData(2);
                    break;
            }
        }
        private void AllDisabel()
        {
            foreach (ToolStripItem ti in toolStrip1.Items)
            {
                ti.BackColor = Color.Transparent;
                ti.ForeColor = Color.White;
            }
            toolStrip1.Items[toolStrip1.Items.Count-1].BackColor = Color.White;
            toolStrip1.Items[toolStrip1.Items.Count-1].ForeColor = Color.Black;
        }

        private void tileView1_ContextButtonCustomize(object sender, TileViewContextButtonCustomizeEventArgs e)
        {
            ((ContextButton)e.Item).Tag = this.tileView1.GetRowCellValue(e.RowHandle, "couponid");
        }
    }
}
