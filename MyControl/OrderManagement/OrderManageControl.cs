using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.OrderManagement
{
    
    public partial class OrderManageControl : UserControl
    {
        private OrderProcessingControl OrderProcessingControl;
        private OrderQueryControl OrderQueryControl;
        private ReservationOrderControl ReservationOrderControl;
        private TradingFlowControl TradingFlowControl;
        int ichecked = 0;//0订单处理 1订单查询 2预定订单 3交易流水
        public OrderManageControl()
        {
            InitializeComponent();
            initoolstrip();
            iniControl();
        }
        private void iniControl()
        {
            OrderProcessingControl = new DianDianClient.MyControl.OrderManagement.OrderProcessingControl();
            OrderProcessingControl.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(this.OrderProcessingControl, 0, 1);
            OrderQueryControl = new DianDianClient.MyControl.OrderManagement.OrderQueryControl();
            OrderQueryControl.Dock = DockStyle.Fill;
            ReservationOrderControl = new DianDianClient.MyControl.OrderManagement.ReservationOrderControl();
            ReservationOrderControl.Dock = DockStyle.Fill;
            TradingFlowControl = new DianDianClient.MyControl.OrderManagement.TradingFlowControl();
            TradingFlowControl.Dock = DockStyle.Fill;
        }
        private void initoolstrip()
        {
            Font font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("iColor", typeof(Int32));

            dt.Rows.Add(new object[] { "订单处理", 999 });
            dt.Rows.Add(new object[] { "订单查询", 999 });
            dt.Rows.Add(new object[] { "预定订单", 999 });
            dt.Rows.Add(new object[] { "交易流水", 999 });

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
                case "订单处理":
                    ReplaceControl(0);
                    ichecked = 0;
                    break;
                case "订单查询":
                    ReplaceControl(1);
                    ichecked = 1;
                    break;
                case "预定订单":
                    ReplaceControl(2);
                    ichecked = 2;
                    break;
                case "交易流水":
                    ReplaceControl(3);
                    ichecked = 3;
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
                    this.tableLayoutPanel1.Controls.Remove(OrderProcessingControl);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Remove(OrderQueryControl);
                    break;
                case 2:
                    this.tableLayoutPanel1.Controls.Remove(ReservationOrderControl);
                    break;
                case 3:
                    this.tableLayoutPanel1.Controls.Remove(TradingFlowControl);
                    break;
            }
            AddControl(cur);
        }
        private void AddControl(int cur)
        {
            switch (cur)
            {
                case 0:
                    this.tableLayoutPanel1.Controls.Add(this.OrderProcessingControl, 0, 1);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Add(this.OrderQueryControl, 0, 1);
                    break;
                case 2:
                    this.tableLayoutPanel1.Controls.Add(this.ReservationOrderControl, 0, 1);
                    break;
                case 3:
                    this.tableLayoutPanel1.Controls.Add(this.TradingFlowControl, 0, 1);
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
