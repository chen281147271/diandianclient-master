using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors;

namespace DianDianClient.MyControl.MemberManagement
{
    public partial class RuleControl : UserControl
    {
        Biz.BizMemberCard MemberCard = new Biz.BizMemberCard();
        List<Models.dd_chargecar_rule> list;
        public RuleControl()
        {
            InitializeComponent();
            list= MemberCard.QueryMemberRules();
            this.gridControl1.DataSource = translate(list);
        }
        public class ddchargecarrule
        {
            public string ruleid { get; set; }
            public string addtime { get; set; }
            public string rname { get; set; }
            public string realmoney { get; set; }
            public string songmoney { get; set; }
        }
        public List<ddchargecarrule> translate(List<Models.dd_chargecar_rule> list)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            List<ddchargecarrule> list_temp = new List<ddchargecarrule>();
            foreach (Models.dd_chargecar_rule item in list)
            {
                ddchargecarrule temp = new ddchargecarrule();
                temp.addtime = (item.addtime!=null)? item.addtime:"";
                temp.rname = (item.rname != null) ? item.rname : "";
                temp.realmoney = item.realmoney.ToString();
                temp.songmoney = (item.money - item.realmoney).ToString();
                temp.ruleid = item.ruleid.ToString();
                list_temp.Add(temp);
            }
            return list_temp;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MyEvent.MemberManagement.RuleEventClass.Close();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
            {
                string id = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ruleid").ToString();
                MemberCard.DeleteMemberRule(Convert.ToInt32(id));
                list = MemberCard.QueryMemberRules();
                this.gridControl1.DataSource = translate(list);
            }
            else
            {
                string id= this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "ruleid").ToString();
                string rname = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "rname").ToString();
                string realmoney = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "realmoney").ToString();
                string songmoney = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, "songmoney").ToString();
                if (rname=="")
                {
                    XtraMessageBox.Show( "活动标题不能为空！","提示");
                    return;
                }else if (!IsNumeric(realmoney))
                {
                    XtraMessageBox.Show( "充值金额必须为大于0的正整数！", "提示");
                    return;
                }
                else if (!IsNumeric(songmoney))
                {
                    XtraMessageBox.Show( "赠送金额必须为大于0的正整数！", "提示");
                    return;
                }
                MemberCard.EditMemberRule(Convert.ToInt32(id), rname, Convert.ToInt32(realmoney), Convert.ToInt32(songmoney));
                Utils.utils.ShowTip("提示", "操作成功");
            }
        }
        static bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
            return reg1.IsMatch(str);
        }
        private void Btn_AddNewRow_Click(object sender, EventArgs e)
        {
            MyForm.MemberManagement.AddDetaileForm addDetaile = new MyForm.MemberManagement.AddDetaileForm();
            addDetaile.StartPosition = FormStartPosition.CenterScreen;
            addDetaile.ShowDialog();
            list = MemberCard.QueryMemberRules();
            this.gridControl1.DataSource = translate(list);
        }
    }
}
