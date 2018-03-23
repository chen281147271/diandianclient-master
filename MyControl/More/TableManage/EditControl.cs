using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.TableManage
{
    public partial class EditControl : UserControl
    {
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        public delegate void MyDelegate();
        public  event MyDelegate MyEvent;
        List<Models.dd_table_floor> list;
        string floorid;
        string tableposkey;
        string isRoom;
        string tableNo;
        string tableName;
        public EditControl(List<Models.dd_table_floor> list, string peopleNum,string tfuwu,string floorName, string floorid,string tableposkey,string isRoom,string tableNo,string tableName)
        {
            InitializeComponent();
            this.list = list;
            this.floorid = floorid;
            this.floorid = (this.floorid == "") ? "0" : this.floorid;
            this.tableposkey = tableposkey;
            this.isRoom = isRoom;
            this.tableNo = tableNo;
            this.tableName = tableName;
            foreach(var a in list)
            {
                this.comboBoxEdit1.Properties.Items.Add(a.floorname);
            }
            this.Txt_peopleNum.Text = peopleNum;
            this.Txt_tfuwu.Text = tfuwu;
            this.comboBoxEdit1.Text = floorName;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider1.Validate())
                return;
            BizSPInfo.ModifyTable(Convert.ToInt32(this.tableposkey), Convert.ToInt32(this.isRoom), Convert.ToInt32(this.tableNo), this.tableName, Convert.ToInt32(Txt_peopleNum.Text), Convert.ToDecimal(Txt_tfuwu.Text), Convert.ToInt32(this.floorid));
            MyEvent();
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.floorid = list[comboBoxEdit1.SelectedIndex].floorid.ToString();
            this.floorid=(this.floorid=="")?"0":this.floorid;
        }
    }
}
