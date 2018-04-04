using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyControl.More.jinxiaocunManage
{
    public partial class EdityuanliaoControl : UserControl
    {
        int genreid = 0;
        List<Models.storage_genre> list_storagegenre;
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        public delegate void MyDelegate();
        public event MyDelegate MyEvent;
        public EdityuanliaoControl()
        {
            InitializeComponent();
            iniCob();
        }
        private void iniCob()
        {
            list_storagegenre = BizStorage.QueryGenre();
            cbo_yuanliaoType.Properties.Items.Clear();
            cbo_yuanliaoType.Properties.Items.Add("请选择分类");
            foreach (var a in list_storagegenre)
            {
                cbo_yuanliaoType.Properties.Items.Add(a.genrename);
            }
            cbo_yuanliaoType.SelectedIndex = 0;
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (this.genreid == 0)
            {
                Utils.utils.ShowMessageBox("请选择原料分类！");
                return;
            }
            if (!this.dxValidationProvider1.Validate())
            {
                return;
            }
            string crudename = txt_name.Text;
            string unit = txt_unit.Text;
            BizStorage.SaveCrude(-1, this.genreid, crudename, unit);
            this.MyEvent();
        }

        private void cbo_yuanliaoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.genreid = (cbo_yuanliaoType.SelectedIndex == 0) ? 0 : list_storagegenre[cbo_yuanliaoType.SelectedIndex - 1].genreid;
        }
    }
}
