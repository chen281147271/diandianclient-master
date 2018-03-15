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
    public partial class MemberManageControl : UserControl
    {
        private MemberQueryControl memberQueryControl;
        private ProtocolCustomerControl protocolCustomerControl;
        private UserManagementControl userManagementControl;
        int ichecked = 0;//0会员查询 1用户管理 2协议客户
        public MemberManageControl()
        {
            InitializeComponent();
            initoolstrip();
            iniControl();

            MyEvent.MemberManagement.MemberDetaileEvent.CloseEvent += MyCloseEvent_add;
            MyEvent.MemberManagement.RechargeRecordEvent.CloseEvent += MyCloseEvent_close;
            MyEvent.MemberManagement.RuleEventClass.CloseEvent += MyCloseEvent_close;
            MyEvent.MemberManagement.MemberDetaileEvent.CloseEvent_Rule += MyCloseEvent_Rule;


        }
        private void MyCloseEvent_close()
        {
            this.tableLayoutPanel1.Controls.RemoveAt(0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1);
            this.tableLayoutPanel1.Controls.Add(memberQueryControl);
        }
            private void MyCloseEvent_add(string cardid, int type)
        {
                this.tableLayoutPanel1.Controls.Remove(this.toolStrip1);
                this.tableLayoutPanel1.Controls.Remove(memberQueryControl);
                MyControl.MemberManagement.RechargeRecordControl rechargeRecord = new RechargeRecordControl(cardid,type);
                rechargeRecord.Dock = DockStyle.Fill;
                this.tableLayoutPanel1.Controls.Add(rechargeRecord, 0, 1);     
        }
        private void MyCloseEvent_Rule()
        {
            this.tableLayoutPanel1.Controls.Remove(this.toolStrip1);
            this.tableLayoutPanel1.Controls.Remove(memberQueryControl);
            MyControl.MemberManagement.RuleControl rule = new RuleControl();
            rule.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(rule, 0, 1);
        }
        private void iniControl()
        {
            memberQueryControl = new DianDianClient.MyControl.MemberManagement.MemberQueryControl();
            memberQueryControl.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(this.memberQueryControl, 0, 1);
            protocolCustomerControl = new DianDianClient.MyControl.MemberManagement.ProtocolCustomerControl();
            protocolCustomerControl.Dock = DockStyle.Fill;
            userManagementControl = new DianDianClient.MyControl.MemberManagement.UserManagementControl();
            userManagementControl.Dock = DockStyle.Fill;
        }
        private void initoolstrip()
        {
            Font font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("iColor", typeof(Int32));

            dt.Rows.Add(new object[] { "会员查询", 999 });
            dt.Rows.Add(new object[] { "用户管理", 999 });
            dt.Rows.Add(new object[] { "协议客户", 999 });

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
                case "会员查询":
                    ReplaceControl(0);
                    ichecked = 0;
                    break;
                case "用户管理":
                    ReplaceControl(1);
                    ichecked = 1;
                    break;
                case "协议客户":
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
                    this.tableLayoutPanel1.Controls.Remove(memberQueryControl);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Remove(protocolCustomerControl);
                    break;
                case 2:
                    this.tableLayoutPanel1.Controls.Remove(userManagementControl);
                    break;
            }
            AddControl(cur);
        }
        private void AddControl(int cur)
        {
            switch (cur)
            {
                case 0:
                    this.tableLayoutPanel1.Controls.Add(this.memberQueryControl, 0, 1);
                    break;
                case 1:
                    this.tableLayoutPanel1.Controls.Add(this.protocolCustomerControl, 0, 1);
                    break;
                case 2:
                    this.tableLayoutPanel1.Controls.Add(this.userManagementControl, 0, 1);
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
