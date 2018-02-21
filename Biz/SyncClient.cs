using DianDianClient.Models;
using DianDianClient.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;

namespace DianDianClient.Biz
{
    class SyncClient
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("SyncClient");
        private DianDianEntities db = new DianDianEntities();
        static public String token = "";
        static public bool needSyncCC = false;
        static public bool needSyncYL = false;
        static public bool needSyncFL = false;
        static public bool needSyncZW = false;
        static public bool needSyncYG = false;
        static public bool needSyncQY = false;
        static public bool needSyncCZ = false;
        static public bool needSyncHY = false;
        static public bool needSyncHD = false;
        static public bool needSyncSPInfo = false;
        static public bool needSyncDK = false;

        public SyncClient()
        {
            
        }
        static public string Login(String requestUrl, String requestParam)
        {
            RestClient client = new RestClient();
            client.ContentType = "application/json";
            client.EndPoint = requestUrl + "?" + requestParam;
            client.Method = HttpVerb.GET;
            string result = client.MakeRequest();
            log.Debug(result);
            return result;
        }

        public void SyncMethod()
        {
            var json = "";
            RestClient client = new RestClient();
            client.ContentType = "application/json";
            while (true)
            {
                List<remote_request> requestList = db.remote_request.Where(p => p.deal_flag == 0).OrderBy(p =>p.create_time).ToList();
                foreach(remote_request reqInfo in requestList){
                    client.EndPoint = reqInfo.request_url +"?"+ reqInfo.request_param+"&token="+token;
                    if (reqInfo.request_type.Equals("POST",StringComparison.OrdinalIgnoreCase))
                    {
                        client.Method = HttpVerb.POST;
                    }else if(reqInfo.request_type.Equals("PUT", StringComparison.OrdinalIgnoreCase))
                    {
                        client.Method = HttpVerb.PUT;
                    }else if(reqInfo.request_type.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
                    {
                        client.Method = HttpVerb.DELETE;
                    }else
                    {
                        client.Method = HttpVerb.GET;
                    }
                    
                    json = client.MakeRequest();
                    log.Debug(json.ToString());
                    CommonResponseBean crb = JsonConvert.DeserializeObject<CommonResponseBean>(json);
                    reqInfo.deal_flag = 1;
                    reqInfo.deal_time = DateTime.Now;
                    reqInfo.result_code = crb.code;
                    reqInfo.result_message = crb.msg;

                    db.remote_request.Attach(reqInfo);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(reqInfo);
                    stateEntity.SetModifiedProperty("deal_flag");
                    stateEntity.SetModifiedProperty("deal_time");
                    stateEntity.SetModifiedProperty("result_message");

                    db.SaveChanges();
                }
                Thread.Sleep(5000);
            }
        }

        public void SyncInfoList()
        {
            while (true)
            {
                //库存同步
                if(needSyncCC)
                {

                }
                //原料同步
                if (needSyncYL)
                {

                }
                //分类同步
                if (needSyncFL)
                {

                }
                //职位同步
                if (needSyncZW)
                {

                }
                //员工同步
                if (needSyncYG)
                {

                }
                //区域同步
                if (needSyncQY)
                {

                }
                //餐桌同步
                if (needSyncCZ)
                {

                }
                //会员同步
                if (needSyncHY)
                {

                }
                //活动同步
                if (needSyncHD)
                {

                }
                //餐厅基本信息
                if (needSyncSPInfo)
                {

                }
                //档口同步       
                if (needSyncDK)
                {

                }
            }
            
        }
    }
}
