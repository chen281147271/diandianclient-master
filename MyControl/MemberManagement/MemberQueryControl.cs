using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace DianDianClient.MyControl.MemberManagement
{
    public partial class MemberQueryControl : UserControl
    {
        Biz.BizMemberCard MemberCard = new Biz.BizMemberCard();
        DateTime s_time = new DateTime ();
        DateTime e_time = DateTime.Now;
        List<Models.dd_mem_card> list;
        public int curPage = 1;
        public int pageSize = 10;
        public int allcount = 0;
        public MemberQueryControl()
        {
            InitializeComponent();
            list= MemberCard.QueryMembers("", "", s_time, e_time);
            this.gridControl1.DataSource = translate(list);
            this.gridView1.RowHeight = 50;
            this.gridView1.ColumnPanelRowHeight = 50;
        }
        public class ddmemcard
        {
            public string cardid { get; set; }
            public string addtime { get; set; }
            public string isvalid { get; set; }
            public string realname { get; set; }
            public string telno { get; set; }
            public string cardno { get; set; }
            public string money { get; set; }
            public string birthday { get; set; }
            public string expirydate { get; set; }
        }
        public List<ddmemcard> translate(List<Models.dd_mem_card> list)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            List<ddmemcard> list_temp = new List<ddmemcard>();
            foreach (Models.dd_mem_card item in list)
            {
                ddmemcard temp = new ddmemcard();
                temp.addtime = (item.addtime != null) ? startTime.AddSeconds(Convert.ToDouble(item.addtime)).ToShortDateString() : "";
                temp.birthday = (item.birthday != null) ? item.birthday : "";
                temp.cardid = item.cardid.ToString();
                temp.cardno = (item.cardno != null) ? item.cardno : "";
                temp.expirydate = (item.expirydate != null) ? startTime.AddSeconds(Convert.ToDouble(item.expirydate)).ToShortDateString() : "";
                temp.isvalid = item.isvalid.ToString();
                temp.money= item.money.ToString();
                temp.realname= (item.realname != null) ? item.realname : "";
                temp.telno= (item.telno != null) ? item.telno : "";
                list_temp.Add(temp);
            }
            return list_temp;
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowhandle = this.gridView1.FocusedRowHandle;
            string aa= this.gridView1.GetRowCellValue(rowhandle, "realname").ToString();
        }
    }
}
