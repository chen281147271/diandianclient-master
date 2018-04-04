using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.MyModels
{
    public static class TipMsg
    {
        public static List<_TipMsg> list=new List<_TipMsg>();

        public class _TipMsg
        {
            public string title { get; set; }
            public string msg { get; set; }
        }
    }
    
    
}
