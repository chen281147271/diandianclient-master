using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace DianDianClient.MyControl.PaiDui
{
    public partial class PaiDuiListControl : UserControl
    {
        class MyClass
        {
            public string str { get; set; }
            public int id { get; set; }
        }
        List<MyClass> list = new List<MyClass>();
        public PaiDuiListControl()
        {
            InitializeComponent();

            for (int i = 0; i < 4; i++)
            {
                MyClass myClass = new MyClass();
                myClass.str = "A" + i.ToString();
                myClass.id = i;
                list.Add(myClass);
            }
            //this.listBoxControl1.DataSource = list;
            //this.listBoxControl1.SelectedIndex = 1;
            this.gridControl1.DataSource = list;
            iniEvent();
        }
        #region grid事件
        private void tileView1_ContextButtonCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewContextButtonCustomizeEventArgs e)
        {

            if (e.Item.Name == "contextButton1" && this.tileView1.IsRowSelected(e.RowHandle))
            {
                DevExpress.Utils.ContextButton contextButton = e.Item as DevExpress.Utils.ContextButton;
                contextButton.Caption = "呼叫";
                contextButton.AppearanceNormal.Font = new Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                contextButton.AppearanceHover.Font = new Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // contextButton.AppearanceNormal.ForeColor = Color.Red;
                //// this.tileView1.ContextButtonOptions.FarPanelColor = Color.FromArgb(250,180,116);
                //contextButton.AppearanceHover.BackColor= Color.FromArgb(250, 180, 116);
                //contextButton.AppearanceNormal.BackColor = Color.FromArgb(250, 180, 116);
            }
        }

        private void tileView1_ContextButtonClick(object sender, DevExpress.Utils.ContextItemClickEventArgs e)
        {
            DevExpress.XtraGrid.Views.Tile.TileViewItem tileViewItem = (DevExpress.XtraGrid.Views.Tile.TileViewItem)e.DataItem;
            int id = Convert.ToInt32(this.tileView1.GetRowCellValue(tileViewItem.RowHandle, "id"));
            string str = Convert.ToString(this.tileView1.GetRowCellValue(tileViewItem.RowHandle, "str"));
            if (e.Item.Name == "contextButton1")
            {
                this.tileView1.FocusedRowHandle = tileViewItem.RowHandle;
                DevExpress.Utils.ContextButton contextButton = e.Item as DevExpress.Utils.ContextButton;
                if (contextButton.Caption == "呼叫")
                {
                    MessageBox.Show("你呼叫了第" + str);
                }
            }
            if (e.Item.Name == "contextButton2")
            {
                list.Remove(list.Find(o => o.id == id));
                this.tileView1.RefreshData();
            }
        }
        #endregion
        #region 顶部tab
        ///////////////////////////////////////////////////////////////////
        private void iniEvent()
        {
            inidefault();
            inipic();
            iniG1();
            iniG2();
            iniG3();
            iniG4();
            iniG5();
        }
        private void inidefault()
        {
            g1_lab1.ForeColor = Color.YellowGreen;
            g1_lab2.ForeColor = Color.YellowGreen;
            g1_pic.Image = Properties.Resources._line;
        }
        private void inipic()
        {
            g1_pic.Image = Properties.Resources.line;
            g2_pic.Image = Properties.Resources.line;
            g3_pic.Image = Properties.Resources.line;
            g4_pic.Image = Properties.Resources.line;
            g5_pic.Image = Properties.Resources.line;
        }
        private void iniG1()
        {
            g1_lab1.Click += G1_Click;
            g1_lab2.Click += G1_Click;
            g1_pic.Click += G1_Click;
        }
        private void iniG2()
        {
            g2_lab1.Click += G2_Click;
            g2_lab2.Click += G2_Click;
            g2_pic.Click += G2_Click;
        }
        private void iniG3()
        {
            g3_lab1.Click += G3_Click;
            g3_lab2.Click += G3_Click;
            g3_pic.Click += G3_Click;
        }
        private void iniG4()
        {
            g4_lab1.Click += G4_Click;
            g4_lab2.Click += G4_Click;
            g4_pic.Click += G4_Click;
        }
        private void iniG5()
        {
            g5_lab1.Click += G5_Click;
            g5_lab2.Click += G5_Click;
            g5_pic.Click += G5_Click;
        }
        private void iniALL()
        {
            g1_lab1.ForeColor = Color.Black;
            g1_lab2.ForeColor = Color.Black;
            g1_pic.Image = Properties.Resources.line;

            g2_lab1.ForeColor = Color.Black;
            g2_lab2.ForeColor = Color.Black;
            g2_pic.Image = Properties.Resources.line;

            g3_lab1.ForeColor = Color.Black;
            g3_lab2.ForeColor = Color.Black;
            g3_pic.Image = Properties.Resources.line;

            g4_lab1.ForeColor = Color.Black;
            g4_lab2.ForeColor = Color.Black;
            g4_pic.Image = Properties.Resources.line;

            g5_lab1.ForeColor = Color.Black;
            g5_lab2.ForeColor = Color.Black;
            g5_pic.Image = Properties.Resources.line;
        }
        private void G1_Click(object sender, EventArgs e)
        {
            iniALL();
            g1_lab1.ForeColor = Color.YellowGreen;
            g1_lab2.ForeColor = Color.YellowGreen;
            g1_pic.Image = Properties.Resources._line;
        }
        private void G2_Click(object sender, EventArgs e)
        {
            iniALL();
            g2_lab1.ForeColor = Color.YellowGreen;
            g2_lab2.ForeColor = Color.YellowGreen;
            g2_pic.Image = Properties.Resources._line;
        }
        private void G3_Click(object sender, EventArgs e)
        {
            iniALL();
            g3_lab1.ForeColor = Color.YellowGreen;
            g3_lab2.ForeColor = Color.YellowGreen;
            g3_pic.Image = Properties.Resources._line;
        }
        private void G4_Click(object sender, EventArgs e)
        {
            iniALL();
            g4_lab1.ForeColor = Color.YellowGreen;
            g4_lab2.ForeColor = Color.YellowGreen;
            g4_pic.Image = Properties.Resources._line;
        }
        private void G5_Click(object sender, EventArgs e)
        {
            iniALL();
            g5_lab1.ForeColor = Color.YellowGreen;
            g5_lab2.ForeColor = Color.YellowGreen;
            g5_pic.Image = Properties.Resources._line;
        }
        #endregion
    }
}
