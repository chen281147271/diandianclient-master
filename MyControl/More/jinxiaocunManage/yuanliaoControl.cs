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
    public partial class yuanliaoControl : UserControl
    {
        Biz.BizStorage BizStorage = new Biz.BizStorage();
        List<Models.v_crude_genre> list;
        public yuanliaoControl()
        {
            InitializeComponent();
            list = BizStorage.QueryCrude("", 0);
        }
    }
}
