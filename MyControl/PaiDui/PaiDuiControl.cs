using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

namespace DianDianClient.MyControl.PaiDui
{
    public partial class PaiDuiControl : UserControl
    {
        public PaiDuiControl()
        {
            InitializeComponent();
            Inipanel();
            initoolstrip();
            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered",
System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
true, null);

            MyControl.PaiDui.PaiDuiListControl paiDuiList = new PaiDuiListControl();
            paiDuiList.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Controls.Add(paiDuiList, 1, 1);
        }
        private void Inipanel()
        {
            DataTable dt = new DataTable("Student");
            dt.Columns.Add("TableID", typeof(Int32));
            dt.Columns.Add("TableName", typeof(String));
            dt.Columns.Add("TableState", typeof(Int32));
            //  int iID = 0;
            for (int i = 0; i < 200; i++)
            {
                string str = i + "号桌";
                dt.Rows.Add(new object[] { i, str, i });
            }

            ItemContainerBinding(itemContainer1, dt, "TableName", "TableID", "TableState");
            itemContainer1.ItemSpacing = 20;
            //  itemContainer1.TitleText = "桌位";
            itemContainer1.TitleVisible = false;
            //  metroTilePanel1.BackgroundStyle.BackgroundImage = Properties.Resources._2;
            tableLayoutPanel1.BackgroundImage = Properties.Resources._1;
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void ItemContainerBinding(ItemContainer ic, DataTable dtt, string TableName, string TableID, String TableState)
        {
            ic.Refresh();
            ic.SubItems.Clear();

            DataTable dt = dtt;

            MetroTileItem[] metroitem = new MetroTileItem[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                metroitem[i] = new MetroTileItem();
                metroitem[i].Text = dt.Rows[i][TableName].ToString();
                metroitem[i].TitleText = dt.Rows[i][TableID].ToString();
                metroitem[i].TileStyle.TextAlignment = eStyleTextAlignment.Center;
                metroitem[i].TileStyle.TextAlignment = eStyleTextAlignment.Center;
                // metroitem[i].TileStyle.BackColor2 = Color.Silver;
                metroitem[i].TileStyle.BackColor = System.Drawing.Color.FromArgb(200, 255, 255, 255);
                metroitem[i].TileStyle.BackColor2 = System.Drawing.Color.FromArgb(200, 255, 255, 255);
                //metroitem[i].TileColor = eMetroTileColor.Green;
                metroitem[i].Click += metroTileItem_Click;
                metroitem[i].TileSize = new System.Drawing.Size(150, 150);
                metroitem[i].TileStyle.CornerType = eCornerType.Rounded;
                metroitem[i].TileStyle.Font = new Font("楷体", 12, FontStyle.Bold);
                metroitem[i].TileStyle.TextColor = Color.Black;

            }
            ic.SubItems.AddRange(metroitem);
            ic.Refresh();
        }
        private void metroTileItem_Click(object sender, EventArgs e)
        {
            string str = ((MetroTileItem)sender).TitleText.ToString();
            MessageBox.Show(str);
        }
        private void initoolstrip()
        {
            //1
            Font font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("iColor", typeof(Int32));
            for (int i = 0; i < 50; i++)
            {
                string str = i + "号桌";
                dt.Rows.Add(new object[] { str, 999 });
            }
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
                // toolStripItem[i].Margin = new System.Windows.Forms.Padding(5, 0, 10, 0);
                // toolStripItem[i].BackColor = ChangeClolor(Convert.ToInt32(dataTable.Rows[i][ForeColorColumn].ToString()));
                toolStripItem[i].Padding = new System.Windows.Forms.Padding(0);
                toolStripItem[i].Margin = new System.Windows.Forms.Padding(0);
                toolStripItem[i].BackColor = ToolStripBackColor;
            }
            toolStrip.Items.AddRange(toolStripItem);
            toolStrip.BackColor = ToolStripBackColor;
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



            MessageBox.Show(sender.ToString());
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
