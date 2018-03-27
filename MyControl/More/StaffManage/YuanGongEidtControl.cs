using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace DianDianClient.MyControl.More.StaffManage
{
    public partial class YuanGongEidtControl : UserControl
    {
        Biz.BizEmployee BizEmployee = new Biz.BizEmployee();
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        List<Models.dd_table_floor> list_tablefloor;
        List<Models.sys_role> list_sysrole;
        List<Models.v_emp_floor> list_empfloor;
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        int sysroleid;
        List<int> floorList;
        int memberkey;
        int Optype;
        public YuanGongEidtControl(int memberkey,int sysroleid,string rolename,int code,string name,int Optype)
        {
            InitializeComponent();
            this.Optype = Optype;
            if (Optype == 1)
            {
                this.txt_code.Text = code.ToString();
                this.Txt_name.Text = name;
                btn_pw.Enabled = true;
            }
            else
            {
                btn_pw.Enabled = false;
            }
            this.memberkey = memberkey;
            floorList = new List<int>();
            floorList.Clear();
            list_sysrole =BizEmployee.QueryPostion("0");
            this.sysroleid = sysroleid;
            foreach ( var a in list_sysrole)
            {
                this.cbo_rolename.Properties.Items.Add(a.rolename);
            }
            list_tablefloor=BizSPInfo.GetFloorList(0);
            foreach(var a in list_tablefloor)
            {
                this.cbo_quyu.Properties.Items.AddRange(new CheckedListBoxItem[] { new CheckedListBoxItem(a.floorid, a.floorname) });
            }
            list_empfloor = BizEmployee.QueryEmployeeFloorList(memberkey);
            foreach(var a in list_empfloor)
            {
                var b = this.cbo_quyu.Properties.Items.Where(o => o.Value.ToString() == a.floorid.ToString());
                foreach (var c in b)
                {
                    c.CheckState = CheckState.Checked;
                }
            }
            this.cbo_rolename.Text = rolename;


        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider1.Validate())
                return;
            var a = this.cbo_quyu.Properties.Items.Where(o=>o.CheckState==CheckState.Checked);
            foreach ( var b in a)
            {
                floorList.Add(Convert.ToInt32(b.Value));
            }
            if(this.Optype==1)
            BizEmployee.SaveEmployee(this.memberkey, Txt_name.Text, this.sysroleid, floorList);
            else
                BizEmployee.SaveEmployee(-1, Txt_name.Text, this.sysroleid, floorList);
            this.MyEvent();
        }
        private void cbo_rolename_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.sysroleid = Convert.ToInt32(list_sysrole[this.cbo_rolename.SelectedIndex].sysroleid);
        }

        private void btn_pw_Click(object sender, EventArgs e)
        {
           
        }
    }
}
