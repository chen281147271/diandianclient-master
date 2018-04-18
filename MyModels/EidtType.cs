using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.MyModels
{
   public static class  EidtType
    {
        public static List<_EidtType> list = new List<_EidtType>();
        public  class _EidtType
        {
            public string standardname { get; set; }
            public string yuanliao { get; set; }
            public string itemKey { get; set; }
            public string standardkey { get; set; }
            public decimal sprice { get; set; }
            public bool state { get; set; }
            public List<Models.v_item_crude> item_standard { get; set; }
        }
    }
}
