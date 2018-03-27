﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.StaffManage
{
    public partial class StaffManageControl : UserControl
    {
        private YuanGongControl yuanGongControl;
        private ZhiWeiControl zhiWeiControl;
        int ichecked = 0;//0职位 1员工
        public StaffManageControl()
        {
            InitializeComponent();
            iniControl();
            initoolstrip();
        }
        private void iniControl()
        {
            zhiWeiControl = new ZhiWeiControl();
            zhiWeiControl.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(this.zhiWeiControl, 0, 1);
            yuanGongControl = new YuanGongControl();
            yuanGongControl.Dock = DockStyle.Fill;
        }
        private void initoolstrip()
        {
            Font font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("iColor", typeof(Int32));

            dt.Rows.Add(new object[] { "职位管理", 999 });
            dt.Rows.Add(new object[] { "员工管理", 999 });

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
                case "职位管理":
                    ReplaceControl(0);
                    ichecked = 0;
                    break;
                case "员工管理":
                    ReplaceControl(1);
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
                    this.tableLayoutPanel1.Controls.Remove(zhiWeiControl);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Remove(yuanGongControl);
                    break;
            }
            AddControl(cur);
        }
        private void AddControl(int cur)
        {
            switch (cur)
            {
                case 0:
                    this.tableLayoutPanel1.Controls.Add(this.zhiWeiControl, 0, 1);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Add(this.yuanGongControl, 0, 1);
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
