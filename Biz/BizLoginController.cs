using DianDianClient.Models;
using DianDianClient.Utils;
using Newtonsoft.Json;
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
        static private string loginUrl = "http://app.diandiancaidan.com/back/pclogin.html";
        static public int userid = 0;
        static public string username = "";

        //远程登录
        public LoginResponseBean RemoteLogin(int shopcode, String username, String password)
        {
            string request_param = "shopcode=" + shopcode + "&username=" + username + "&password=" + Md5Helper.Encrypt(password);
            
            String resultJson = SyncClient.Login(loginUrl, request_param);
            LoginResponseBean loginResponse = JsonConvert.DeserializeObject<LoginResponseBean>(resultJson);
            if(loginResponse.code == 100)
            {
                SyncClient.token = loginResponse.token;
                log.Info("remote login success.");
            }
            else
            {
                log.Error("remote login error, msg = " + loginResponse.result);
            }            

            return loginResponse;
        }

        //本地登录
        public String LocalLogin(int shopcode, String username, String password)
        {
            string result = "";
            List<member> rsl = db.member.Where(t => t.loginName.Equals(username) && t.pwd.Equals(password) && t.shopkey == shopcode).ToList();
            if(rsl.Count > 0)
            {

            }
            return "";
        }

        //修改密码
        public void ChangePwd()
        {

        }
    }
}
