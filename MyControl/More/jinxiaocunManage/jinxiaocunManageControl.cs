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
    public partial class jinxiaocunManageControl : UserControl
    {
        private zongControl zongControl;
        private rukuControl rukuControl;
        private sunyiControl sunyiControl;
        private kucunControl kucunControl;
        private yuanliaoControl yuanliaoControl;
        int ichecked = 0;
        public jinxiaocunManageControl()
        {
            InitializeComponent();
            iniControl();
            initoolstrip();
        }
        private void iniControl()
        {
            zongControl = new zongControl();
            zongControl.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(this.zongControl, 0, 1);
            rukuControl = new rukuControl();
            rukuControl.Dock = DockStyle.Fill;
            sunyiControl = new sunyiControl();
            sunyiControl.Dock = DockStyle.Fill;
            kucunControl = new kucunControl();
            kucunControl.Dock = DockStyle.Fill;
            yuanliaoControl = new yuanliaoControl();
            yuanliaoControl.Dock = DockStyle.Fill;
        }
        private void initoolstrip()
        {
            Font font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("iColor", typeof(Int32));

            dt.Rows.Add(new object[] { "总表", 999 });
            dt.Rows.Add(new object[] { "入库表", 999 });
            dt.Rows.Add(new object[] { "损益表", 999 });
            dt.Rows.Add(new object[] { "库存表", 999 });
            dt.Rows.Add(new object[] { "原料管理", 999 });

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
            }
            toolStrip.Items.AddRange(toolStripItem);
            toolStrip.BackColor = ToolStripBackColor;
            toolStrip.Items[0].PerformClick();
            toolStrip.Refresh();
        }
        private void toolStripItem_Click(object sender, EventArgs e)
        {
            //数据库操作
            AllDisabel();
            ToolStripButton toolStripButton = (ToolStripButton)sender;
            toolStripButton.ForeColor = Color.Black;
            toolStripButton.BackColor = Color.White;

            switch (sender.ToString())
            {
                case "总表":
                    ReplaceControl(0);
                    ichecked = 0;
                    break;
                case "入库表":
                    ReplaceControl(1);
                    ichecked = 1;
                    break;
                case "损益表":
                    ReplaceControl(2);
                    ichecked = 1;
                    break;
                case "库存表":
                    ReplaceControl(3);
                    ichecked = 1;
                    break;
                case "原料管理":
                    ReplaceControl(4);
                    ichecked = 1;
                    break;

            }

            // MessageBox.Show(sender.ToString());

        }
        private void ReplaceControl(int cur)
        {
            if (cur == ichecked)
                return;
            switch (ichecked)
            {
                case 0:
                    this.tableLayoutPanel1.Controls.Remove(zongControl);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Remove(rukuControl);
                    break;
                case 2:
                    this.tableLayoutPanel1.Controls.Remove(sunyiControl);
                    break;
                case 3:
                    this.tableLayoutPanel1.Controls.Remove(kucunControl);
                    break;
                case 4:
                    this.tableLayoutPanel1.Controls.Remove(yuanliaoControl);
                    break;
            }
            AddControl(cur);
        }
        private void AddControl(int cur)
        {
            switch (cur)
            {
                case 0:
                    this.tableLayoutPanel1.Controls.Add(this.zongControl, 0, 1);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Add(this.rukuControl, 0, 1);
                    break;
                case 2:
                    this.tableLayoutPanel1.Controls.Add(this.sunyiControl, 0, 1);
                    break;
                case 3:
                    this.tableLayoutPanel1.Controls.Add(this.kucunControl, 0, 1);
                    break;
                case 4:
                    this.tableLayoutPanel1.Controls.Add(this.yuanliaoControl, 0, 1);
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
        }
    }
}
