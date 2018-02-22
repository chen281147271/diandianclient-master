using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Models
{
    class LoginResponseBean
    {
        public int code { get; set; }
        public string result { get; set; }
        public string user { get; set; }
        public string token { get; set; }
    }

    class LoginUserResponseBean
    {
        public int chaintype { get; set; }
        public string headimg { get; set; }
        public int isaudit { get; set; }
        public string password { get; set; }
        public string realname { get; set; }
        public int role { get; set; }
        public string rolename { get; set; }
        public int rtype { get; set; }
        public int shopkey { get; set; }
        public int shoptype { get; set; }
        public int srole { get; set; }
        public string success { get; set; }
        public string token { get; set; }
        public int type { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
    }
}
