using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.cantingSetUp
{
    public partial class cantingControl : UserControl
    {
        private cantingSetUpControl CantingSetUpControl;
        private EditPayWayControl editPayWayControl;
        private QuanXianControl quanXianControl;
        int ichecked = 0;
        public cantingControl()
        {
            InitializeComponent();
            iniControl();
            initoolstrip();
        }
        private void iniControl()
        {
            CantingSetUpControl = new  cantingSetUpControl();
            CantingSetUpControl.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(this.CantingSetUpControl, 0, 1);
            editPayWayControl = new EditPayWayControl();
            editPayWayControl.Dock = DockStyle.Fill;
            quanXianControl = new QuanXianControl();
            quanXianControl.Dock = DockStyle.Fill;
        }
        private void initoolstrip()
        {
            Font font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("iColor", typeof(Int32));

            dt.Rows.Add(new object[] { "基本信息", 999 });
            dt.Rows.Add(new object[] { "收银设置", 999 });
            dt.Rows.Add(new object[] { "权限设置", 999 });

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
                case "基本信息":
                    ReplaceControl(0);
                    ichecked = 0;
                    break;
                case "收银设置":
                    ReplaceControl(1);
                    ichecked = 1;
                    break;
                case "权限设置":
                    ReplaceControl(2);
                    ichecked = 2;
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
                    this.tableLayoutPanel1.Controls.Remove(CantingSetUpControl);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Remove(editPayWayControl);
                    break;
                case 2:
                    this.tableLayoutPanel1.Controls.Remove(quanXianControl);
                    break;
            }
            AddControl(cur);
        }
        private void AddControl(int cur)
        {
            switch (cur)
            {
                case 0:
                    this.tableLayoutPanel1.Controls.Add(this.CantingSetUpControl, 0, 1);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Add(this.editPayWayControl, 0, 1);
                    break;
                case 2:
                    this.tableLayoutPanel1.Controls.Add(this.quanXianControl, 0, 1);
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
