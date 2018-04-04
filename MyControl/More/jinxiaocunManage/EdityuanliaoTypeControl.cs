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
    public partial class EdityuanliaoTypeControl : UserControl
    {
        List<Models.storage_genre> list_storagegenre;
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        public EdityuanliaoTypeControl()
        {
            InitializeComponent();
            Refreshlist();
        }
        private void Refreshlist()
        {
            list_storagegenre = BizStorage.QueryGenre();
            gridControl1.DataSource = list_storagegenre;
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (list_storagegenre.Select(o => o.genrename.Equals(txt_yuanliapname.Text)).Count() > 0)
            {
                Utils.utils.ShowMessageBox("不能添加重复的分类!");
                return;
            }
            BizStorage.SaveGenre(-1, txt_yuanliapname.Text);
            Refreshlist();


        }
    }
}
