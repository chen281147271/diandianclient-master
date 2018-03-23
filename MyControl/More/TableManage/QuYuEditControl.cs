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
    public partial class QuYuEditControl : UserControl
    {
        public delegate void MyDelegate();
        public event MyDelegate Myevent;
        Biz.BizSPInfoController BizSPInfo = new Biz.BizSPInfoController();
        int OpType;//1修改 2新增
        int floorid;
        public QuYuEditControl(string QuYuName,string QuYuNo,string tfuwu,int floorid,int OpType)
        {
            InitializeComponent();
            this.Txt_QuYuName.Text = QuYuName;
            this.Txt_QuYuNo.Text = QuYuNo;
            this.Txt_tfuwu.Text = tfuwu;
            this.floorid = floorid;
            this.OpType = OpType;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!this.dxValidationProvider1.Validate())
                return;
            string floorname = this.Txt_QuYuName.Text;
            int orderno = Convert.ToInt32(this.Txt_QuYuNo.Text);
            decimal tfuwu = Convert.ToDecimal(this.Txt_tfuwu.Text);
            if (this.OpType == 1)
            {
                BizSPInfo.SaveFloor(this.floorid, floorname, orderno, tfuwu);
            }
            else
            {
                BizSPInfo.SaveFloor(-1, floorname, orderno, tfuwu);
            }
            this.Myevent();
        }
    }
}
