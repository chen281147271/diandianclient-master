using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.MyModels
{
    public static  class userinfo
    {
        public static _userinfo user = new _userinfo();
        public class _userinfo
        {
            public int uid { get; set; }
            public string uname { get; set; }
            public string token { get; set; }
        }
    }
}
