using DianDianClient.Models;
using DianDianClient.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizLoginController
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizLoginController");
        //private DianDianEntities db = new DianDianEntities();
        static private string loginUrl = "http://app.diandiancaidan.com/back/pclogin.html";
        static public int userid = 0;
        static public string username = "";

        //远程登录
        public LoginResponseBean RemoteLogin(int shopcode, String username, String password)
        {
            try
            {
                string request_param = "shopcode=" + shopcode + "&username=" + username + "&password=" + Md5Helper.Encrypt(password);

                String resultJson = SyncClient.Login(loginUrl, request_param);
                LoginResponseBean loginResponse = JsonConvert.DeserializeObject<LoginResponseBean>(resultJson);
                if (loginResponse.code == 100)
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
            catch (Exception e)
            {
                log.Error("RemoteLogin error. msg=" + e.Message);
                throw;
            }
        }

        //本地登录
        public int LocalLogin( String username, String password)
        {
            DianDianEntities db = new DianDianEntities();
            string result = "";
            List<member> rsl = db.member.Where(t => t.loginName.Equals(username) && t.pwd.Equals(password) 
                && t.shopkey == Properties.Settings.Default.shopkey && t.enable == 1).ToList();
            if(rsl.Count > 0)
            {
                int userid = rsl.First().memberkey;
                dd_user user = db.dd_user.Where(p => p.userid == userid).FirstOrDefault();
                if (user == null)
                {
                    //本地登录逻辑
                    user = new dd_user();
                    user.userid = rsl.First().memberkey;
                    user.username = rsl.First().name;

                    db.dd_user.Add(user);
                }
                else
                {
                    user.userid = rsl.First().memberkey;
                    user.username = rsl.First().name;

                    db.dd_user.Attach(user);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(user);
                    stateEntity.SetModifiedProperty("userid");
                    stateEntity.SetModifiedProperty("username");
                }

                db.SaveChanges();
                return 0;
            }
            return -1;
        }

    }
}
