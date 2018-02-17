using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizLoginController
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizLoginController");
        private DianDianEntities db = new DianDianEntities();

        //远程登录
        public String RemoteLogin(String shopcode, String username, String password)
        {

            return "";
        }

        //本地登录
        public String LocalLogin(String shopcode, String username, String password)
        {
            db.member.Where(t => t.loginName.Equals(username));
            return "";
        }

        //修改密码
        public void ChangePwd()
        {

        }
    }
}
