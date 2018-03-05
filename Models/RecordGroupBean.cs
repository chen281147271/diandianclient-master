using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Models
{
    class RecordGroupBean
    {
        public string keyName { get; set; }
        public int count { get; set; }
        public decimal sumMoney { get; set; }
        public decimal tuiMoney { get; set; }
        public List<v_cfmainaccount> recList { get; set; }
    }

    class RecordGroupTotleBean
    {
        public int totleCount { get; set; }
        public decimal sumMoney { get; set; }
        public decimal tuiMoney { get; set; }
        public List<RecordGroupBean> groupList { get; set; }
    }
}
