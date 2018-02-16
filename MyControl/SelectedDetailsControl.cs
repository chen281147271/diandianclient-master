using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DianDianClient.MyControl
{
    public partial class SelectedDetails : UserControl
    {
        public SelectedDetails()
        {
            InitializeComponent();
            inigrid();
        }
        private void inigrid()
        {
            DataTable dt = new DataTable("Menudetail");
            dt.Columns.Add("Number", typeof(String));
            dt.Columns.Add("Cainame", typeof(String));
            dt.Columns.Add("Price", typeof(String));
            dt.Columns.Add("State", typeof(Int32));
            //  int iID = 0;
            for (int i = 0; i < 2; i++)
            {
                string str = i + "号菜";
                string strprice = "¥" + i + new Random().Next(1, 10);
                string strNumber = "X" + new Random().Next(1, 1);
                int State = new Random().Next(0, 1);
                dt.Rows.Add(new object[] { strNumber, str, strprice, State });
            }

            this.gridControl1.DataSource = dt.DefaultView;
           // tileView1.Appearance.ItemNormal.BorderColor = Color.Transparent;
            // this..Appearance.Normal.BackColor = Color.FromArgb(58, 166, 101);
            this.tileView1.Appearance.ItemNormal.BackColor = Color.FromArgb(20,20, 166, 101);
            this.gridControl1.BackColor = Color.Red;
            






        }
    }
}
