using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
namespace DianDianClient.MyControl.TableSettlement
{
    public partial class TableSettlement : UserControl
    {
        public decimal Saleprice = 0;//优惠金额
        public decimal ServiceCharge = 0;//服务金额
        int ichange = 0;//买单修改的项目金额
        MyControl.TableSettlement.PayBillControl payBill;
        public TableSettlement()
        {
            InitializeComponent();
            Inipanel();
            initoolstrip();
            Inipanelex();
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            this.tableLayoutPanel1.GetType().GetProperty("DoubleBuffered",
System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
true, null);

            MyEvent.TableSettlement.PayBillEvent.PayEvent += PayEvent;
            MyEvent.TableSettlement.PayBillEvent.CloseEvent += CloseEvent;
            payBill = new PayBillControl();
            payBill.Dock = DockStyle.Fill;
            //this.tableLayoutPanel1.Controls.Add(payBill, 0, 1);
            //this.payBill.Visible = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var parms = base.CreateParams;
        //        parms.Style &= ~0x02000000; // Turn off WS_CLIPCHILDREN   
        //        return parms;
        //    }
        //}
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
            // panel1.BackgroundImage = Properties.Resources._1;
            // panel1.BackgroundImageLayout = ImageLayout.Stretch;
             Bitmap bmp = new Bitmap(Properties.Resources._1);
            this.panelEnhanced1.BackgroundImage = bmp;
            //this.BackgroundImage = bmp;
            this.panelEnhanced1.BackgroundImageLayout = ImageLayout.Stretch;
            this.tableLayoutPanel1.BackColor = Color.FromArgb(50, 0, 0, 0);
        }
        private void ItemContainerBinding(ItemContainer ic, DataTable dtt, string TableName, string TableID,String TableState)
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
                metroitem[i].TileStyle.BackColor = System.Drawing.Color.FromArgb(200,255,255,255);
                metroitem[i].TileStyle.BackColor2= System.Drawing.Color.FromArgb(200,255,255,255);
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
            //if (((MetroTileItem)sender).TileStyle.BackColor == Color.SkyBlue)
            //{
            //    ((MetroTileItem)sender).TileStyle.BackColor = Color.Silver;
            //    ((MetroTileItem)sender).TileStyle.BackColor2 = Color.Silver;
            //}
            //else
            //{
            //    ((MetroTileItem)sender).TileStyle.BackColor = Color.SkyBlue;
            //    ((MetroTileItem)sender).TileStyle.BackColor2 = Color.SkyBlue;
            //}
            //            tableLayoutPanel1.SuspendLayout();
            //            tableLayoutPanel2.SuspendLayout();
            //            tableLayoutPanel3.SuspendLayout();
            //            // tableLayoutPanel1.Controls.Clear();

            //            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered",
            //System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
            //true, null);
            //            tableLayoutPanel3.GetType().GetProperty("DoubleBuffered",
            //    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
            //    true, null);
            //            tableLayoutPanel2.GetType().GetProperty("DoubleBuffered",
            //    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
            //    true, null);
            string str = ((MetroTileItem)sender).TitleText.ToString();
            MessageBox.Show(str);
            //tableLayoutPanel1.ResumeLayout();
            //tableLayoutPanel2.ResumeLayout();
            //tableLayoutPanel3.ResumeLayout();
        }
        //toolstrip重绘
        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width - 8, this.toolStrip1.Height - 8);
                e.Graphics.SetClip(rect);
            }
        }
        private void initoolstrip()
        {
            //1
            Font font= new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            DataTable dt = new DataTable();
            dt.Columns.Add("Text", typeof(String));
            dt.Columns.Add("iColor", typeof(Int32));
            for (int i = 0; i < 50; i++)
            {
                string str = i + "号桌";
                dt.Rows.Add(new object[] {str, 999 });
            }
            Color color= System.Drawing.Color.FromArgb(200, 32, 15, 17);
            ToolStripItemBind(toolStrip1, dt, color, "iColor", "Text", font, toolStripItem_Click);
            //2
            DataTable dtt = new DataTable();
            dtt.Columns.Add("Text", typeof(String));
            dtt.Columns.Add("iColor", typeof(Int32));
            dtt.Rows.Add(new object[] { "打印小票", 0 });
            dtt.Rows.Add(new object[] { "买单", 1 });
            dtt.Rows.Add(new object[] { "转台", 2 });
            dtt.Rows.Add(new object[] { "退菜", 3 });
            dtt.Rows.Add(new object[] { "点菜", 4 });
            dtt.Rows.Add(new object[] { "合桌", 5 });
            color = System.Drawing.Color.FromArgb(68,68,68);
            ToolStripItemBind(toolStrip2, dtt, color, "iColor", "Text", font, toolStripItem2_Click);

            //仅示例
            //toolStrip1.Refresh();
            //toolStrip1.Items.Clear();
            //this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //ToolStripItem[] toolStripItem = new ToolStripItem[5];
            //for (int i = 0; i < 5; i++)
            //{
            //    toolStripItem[i] = new ToolStripButton();
            //    toolStripItem[i].DisplayStyle = ToolStripItemDisplayStyle.Text;
            //    toolStripItem[i].Text = i.ToString() + "楼";
            //    toolStripItem[i].Tag = i;
            //    toolStripItem[i].AutoSize = false;
            //    toolStripItem[i].Size = new Size(150, 60);
            //    toolStripItem[i].Click += toolStripItem_Click;
            //    toolStripItem[i].ForeColor = Color.White;
            //   // toolStripItem[i].BackColor = Color.Red;
            //}
            //toolStrip1.Items.AddRange(toolStripItem);
            //toolStrip1.BackColor = System.Drawing.Color.FromArgb(200,32, 15, 17);
            //toolStrip1.Refresh();
            //
            //this.toolStrip2.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            //ToolStripButton toolStripButton = new ToolStripButton("点菜");
            //toolStripButton.AutoSize = false;
            //toolStripButton.Size = new Size(150, 50);
            //toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            //toolStrip2.Items.Add(toolStripButton);
        }
        private void ToolStripItemBind(ToolStrip toolStrip, DataTable dataTable, Color ToolStripBackColor, string ForeColorColumn, string TextColumn, Font font,EventHandler eventHandler)
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
               // string str = ((ToolStripButton)sender).Tag.ToString();
                //  MessageBox.Show(str);
              //  int istr = Convert.ToInt32(str);
                //DataTable dt = new DataTable("Student");
                //dt.Columns.Add("TableID", typeof(Int32));
                //dt.Columns.Add("TableName", typeof(String));
                //dt.Columns.Add("TableState", typeof(Int32));
                ////  int iID = 0;
                //for (int i = istr * 20; i < (istr + 1) * 20; i++)
                //{
                //    str = i + "号桌";
                //    dt.Rows.Add(new object[] { i, str, i });
                //}

               // ItemContainerBinding(itemContainer1, dt, "TableName", "TableID", "TableState");

        }
        private void AllDisabel()
        {
            foreach(ToolStripItem ti in toolStrip1.Items)
            {
                ti.BackColor = Color.Transparent;
                ti.ForeColor = Color.White;
            }
        }
        private void PayEvent(string name, decimal value)
        {
            if (name == "Saleprice")
            {
                this.Saleprice = value;
            }
            else if (name == "ServiceCharge")
            {
                this.ServiceCharge = value;
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ItemName", typeof(String));
            dataTable.Columns.Add("ItemValue", typeof(String));
            //删除操作
            if (Saleprice == 0) //优惠
            {
                try
                {
                    var row = selectedDetails1.Grid2DataTable.AsEnumerable().First(r => r.Field<string>("ItemName") == "优惠金额");
                    selectedDetails1.Grid2DataTable.Rows.Remove(row);
                    selectedDetails1.gridControl2.Size = new Size(200, selectedDetails1.gridControl2.Size.Height - 36);
                }
                catch
                {

                }

            }
            if (ServiceCharge == 0)//服务费
            {
                try
                {
                    var row = selectedDetails1.Grid2DataTable.AsEnumerable().First(r => r.Field<string>("ItemName") == "服务费");
                    selectedDetails1.Grid2DataTable.Rows.Remove(row);
                    selectedDetails1.gridControl2.Size = new Size(200, selectedDetails1.gridControl2.Size.Height - 36);
                }
                catch
                {

                }

            }
            //新增操作
            if (Saleprice > 0)
            {
                dataTable.Rows.Add(new object[] { "优惠金额", "-" + Saleprice.ToString() });
                ichange++;
            }
            if (ServiceCharge > 0)
            {
                dataTable.Rows.Add(new object[] { "服务费", "+" + ServiceCharge.ToString() });
                ichange++;
            }
            //
            if (ichange > 0)
            {
                //
                int iHeight = selectedDetails1.gridControl2.Size.Height;
                //
                int conut = selectedDetails1.Grid2DataTable.Rows.Count;
                DataRow dataRow = selectedDetails1.Grid2DataTable.Rows[conut - 1];
                dataTable.Rows.Add(dataRow.ItemArray);
                selectedDetails1.Grid2DataTable.Rows.RemoveAt(conut - 1);
                iHeight -= 36;
                foreach (DataRow dr in dataTable.Rows)
                {
                    try
                    {
                        string aaa = dr["ItemName"].ToString();
                        var row = selectedDetails1.Grid2DataTable.AsEnumerable().First(r => r.Field<string>("ItemName") == dr["ItemName"].ToString());
                        if (row != null)
                        {
                            row["ItemValue"] = dr.ItemArray[1];
                        }
                    }
                    catch
                    {
                        selectedDetails1.Grid2DataTable.Rows.Add(dr.ItemArray);
                        iHeight += 36;
                        selectedDetails1.gridControl2.Size = new Size(200, iHeight);
                    }
                }
            }

        }
        private void CloseEvent()
        {
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();

            this.tableLayoutPanel1.Controls.Add(metroTilePanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(toolStrip2, 0, 2);

            // this.metroTilePanel1.Visible = true;
            // this.toolStrip2.Visible = true;
            this.tableLayoutPanel1.Controls.RemoveAt(2);
            // this.tableLayoutPanel1.Controls.RemoveAt(4);
           // this.payBill.Visible = false;

            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
        }
        private void toolStripItem2_Click(object sender, EventArgs e)
        {
            string str = sender.ToString();
            switch (str)
            {
                case "买单":
                    //MyForm.TableSettlement.PayBillForm payBillForm = new MyForm.TableSettlement.PayBillForm();
                    //payBillForm.StartPosition = FormStartPosition.CenterScreen;
                    //payBillForm.ShowDialog();
                    //MyControl.TableSettlement.PayBillControl payBill = new PayBillControl();

                    this.tableLayoutPanel1.SuspendLayout();
                    this.SuspendLayout();

                    this.tableLayoutPanel1.Controls.Remove(metroTilePanel1);
                    this.tableLayoutPanel1.Controls.Remove(toolStrip2);
                    //this.metroTilePanel1.Visible = false;
                   // this.toolStrip2.Visible = false;
                    this.tableLayoutPanel1.Controls.Add(payBill, 0, 1);
                    //this.payBill.Visible = true;

                    this.tableLayoutPanel1.ResumeLayout(false);
                    this.tableLayoutPanel1.PerformLayout();
                    this.ResumeLayout(false);


                    break;
                case "退菜":
                    MyForm.TableSettlement.TuiCaiGrid tuiCaiGrid = new MyForm.TableSettlement.TuiCaiGrid();
                    tuiCaiGrid.StartPosition = FormStartPosition.CenterScreen;
                    tuiCaiGrid.ShowDialog();
                    break;
            }
           // MessageBox.Show(sender.ToString());


        }
        private Color ChangeClolor(int iColor)
        {
            switch (iColor)
            {
                case 0:
                    return Color.FromArgb(26,173,25);
                case 1:
                    return Color.FromArgb(255, 122, 0);
                case 2:
                    return Color.FromArgb(18, 150, 29);
                case 3:
                    return Color.FromArgb(255, 0, 0);
                case 4:
                    return Color.FromArgb(255, 155, 0);
                case 5:
                    return Color.FromArgb(185, 77, 254);
                default:
                    return Color.Transparent;
            }
        }
        private void Inipanelex()
        {
            //this.panelEx1.Style.BackColor1.Alpha = ((byte)(100));
            //this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(20, 0, 191, 255);
            //this.panelEx1.Style.BackColor2.Alpha = ((byte)(100));
            //this.panelEx1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(20, 0, 191, 255);
            //this.panelEx1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;

            //this.panelEx3.Style.BackColor1.Alpha = ((byte)(100));
            //this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(20, 124, 252, 0);
            //this.panelEx3.Style.BackColor2.Alpha = ((byte)(100));
            //this.panelEx3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(20, 124, 252, 0);
            // this.panelEx3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;

            //  this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered",
            //System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
            //true, null);
            //            //        tableLayoutPanel3.GetType().GetProperty("DoubleBuffered",
            //            //System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
            //            //true, null);
            //                    tableLayoutPanel2.GetType().GetProperty("DoubleBuffered",
            //            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1,
            //            true, null);
            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == 0x0014) // 禁掉清除背景消息
        //        return;
        //    base.WndProc(ref m);
        //}

        private void tableLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            int width = this.tableLayoutPanel1.Width / 10;
            int i = 1;
            foreach (ToolStripItem ti in toolStrip1.Items)
            {
                ti.AutoSize = (i <= 10) ? false : true;
                ti.Size = new Size(width - 10, 50);
                i++;
            }
        }
    }
}
