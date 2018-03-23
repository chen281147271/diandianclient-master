using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DianDianClient.MyControl.More.TableManage
{
    public partial class AddTableControl : UserControl
    {
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        List<Models.dd_table_floor> list;
        int floorid = 0;
        int floorid2 = 0;
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public AddTableControl(List<Models.dd_table_floor> list)
        {
            InitializeComponent();
            this.list = list;
            IniData();
        }
        private void IniData()
        {
            foreach(var a in list)
            {
                comboBoxEdit1.Properties.Items.Add(a.floorname);
                comboBoxEdit2.Properties.Items.Add(a.floorname);
            }
            comboBoxEdit1.SelectedIndex = 0;
            comboBoxEdit2.SelectedIndex = 0;
        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text =(this.radioGroup1.SelectedIndex == 0)? "桌号":"餐桌名称";
            this.Txt_TableNo.Properties.Mask.MaskType = (this.radioGroup1.SelectedIndex == 0)? DevExpress.XtraEditors.Mask.MaskType.Numeric: DevExpress.XtraEditors.Mask.MaskType.None;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            int tableNo = 0;
            string tableName = "";
            int peopleNum = 0;
            decimal tfuwu = 0;
            int isRoom = 0;
            if (this.tabPane1.SelectedPageIndex == 0)
            {
                if (!dxValidationProvider1.Validate())
                    return;

                isRoom = (this.radioGroup1.SelectedIndex == 0) ? 0 : 1;
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    tableNo = Convert.ToInt32(this.Txt_TableNo.Text);
                    tableName = "";
                }
                else
                {
                    tableNo = 0;
                    tableName = this.Txt_TableNo.Text;
                }
                peopleNum = Convert.ToInt32(Txt_peopleNum.Text);
                tfuwu = Convert.ToDecimal(Txt_tfuwu.Text);
                BizSPInfo.AddTable(isRoom, tableNo, tableName, peopleNum, tfuwu, this.floorid);
                this.MyEvent();
            }
            else
            {
                if (!dxValidationProvider2.Validate())
                    return;
                isRoom = 0;
                tableName = "";
                peopleNum = Convert.ToInt32(Txt_peopleNum2.Text);
                tfuwu = Convert.ToDecimal(Txt_tfuwu2.Text);
                ArrayList tableNoList = new ArrayList();
                int addnum = Convert.ToInt32(this.Txt_AddNum.Text);
                int starnum = Convert.ToInt32(this.Txt_StarTableNo.Text);
                for (int i = 0; i < addnum; i++)
                {
                    if(checkBox1.Checked)
                        while(Utils.utils.Count(starnum.ToString(), "3")!=0)
                        {
                            starnum++;
                        }
                    if (checkBox2.Checked)
                        while (Utils.utils.Count(starnum.ToString(), "4") != 0)
                        {
                            starnum++;
                        }
                    tableNoList.Add(starnum);
                    starnum++;
                }
                foreach(var a in tableNoList)
                {
                    tableNo = Convert.ToInt32(a);
                    BizSPInfo.AddTable(isRoom, tableNo, tableName, peopleNum, tfuwu, this.floorid2);
                }
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.floorid = list[this.comboBoxEdit1.SelectedIndex].floorid;
            this.Txt_tfuwu.Text = list[this.comboBoxEdit1.SelectedIndex].ffuwu.ToString();
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.floorid2 = list[this.comboBoxEdit2.SelectedIndex].floorid;
            this.Txt_tfuwu2.Text = list[this.comboBoxEdit2.SelectedIndex].ffuwu.ToString();
        }
    }
}
