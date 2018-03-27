using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.StaffManage
{
    public partial class ZhiWeiEidtControl : UserControl
    {
        Biz.BizEmployee bizEmployee = new Biz.BizEmployee();
        List<Models.sys_right> list;
        int sysroleid;
        string rolename;
        List<int> rightlist;
        public delegate void MyDelegate();
        public event MyDelegate Myevent;
        int OpType;
        public ZhiWeiEidtControl(int sysroleid,string rolename,int OpType)
        {
            InitializeComponent();
            list=bizEmployee.QuerySysRight(sysroleid);
            this.rolename = rolename;
            this.sysroleid = sysroleid;
            this.OpType = OpType;
            IniData();
        }
        private void IniData()
        {
            rightlist = new List<int>();
            rightlist.Clear();
            this.Txt_rolename.Text=this.rolename;
            foreach (var a in list)
            {
                switch (a.menuid)
                {
                    case 134:
                        chk_dingdan.Checked = true;
                        rightlist.Add(134);
                        break;
                    case 137:
                        chk_canzhuo.Checked = true;
                        rightlist.Add(137);
                        break;
                    case 159:
                        chk_zhuowei.Checked = true;
                        rightlist.Add(159);
                        break;
                    case 133:
                        chk_caiping.Checked = true;
                        rightlist.Add(133);
                        break;
                    case 144:
                        chk_yingye.Checked = true;
                        rightlist.Add(144);
                        break;
                    case 147:
                        chk_shanghu.Checked = true;
                        rightlist.Add(147);
                        break;
                    case 148:
                        chk_yuangong.Checked = true;
                        rightlist.Add(148);
                        break;
                    case 150:
                        chk_huiyuan.Checked = true;
                        rightlist.Add(150);
                        break;
                }
            }

            chk_dingdan.Tag = 134;
            chk_canzhuo.Tag = 137;
            chk_zhuowei.Tag = 159;
            chk_caiping.Tag = 133;
            chk_yingye.Tag = 144;
            chk_shanghu.Tag = 147;
            chk_yuangong.Tag = 148;
            chk_huiyuan.Tag = 150;

            this.chk_dingdan.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            this.chk_canzhuo.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            this.chk_zhuowei.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            this.chk_caiping.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            this.chk_yingye.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            this.chk_shanghu.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            this.chk_yuangong.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            this.chk_huiyuan.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider1.Validate())
                return;
            if (OpType == 1)
            {
                bizEmployee.SavePosition(this.sysroleid, Txt_rolename.Text, this.rightlist);
            }
            else
            {
                bizEmployee.SavePosition(-1, Txt_rolename.Text, this.rightlist);
            }
            this.Myevent();
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            int menuId = Convert.ToInt32((sender as DevExpress.XtraEditors.CheckEdit).Tag);
            bool ischeck = (sender as DevExpress.XtraEditors.CheckEdit).Checked;
            if(ischeck)
            this.rightlist.Add(menuId);
            else
                this.rightlist.Remove(menuId);
        }
    }
}
