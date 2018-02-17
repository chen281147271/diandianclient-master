using DianDianClient.Models;
using DianDianClient.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class SyncClient
    {
        log4net.ILog log = log4net.LogManager.GetLogger("SyncClient");
        private RestClient client;
        private DianDianEntities db = new DianDianEntities();

        public SyncClient()
        {
            client = new RestClient();
        }

        public void syncMethod()
        {
            var json = "";
            client.ContentType = "application/json";
            client.AddHeader("token:14b0c863-df27-4e05-8e4c-a2bd3169d7d5");
            while (true)
            {
                List<remote_request> requestList = db.remote_request.Where(p => p.deal_flag == 0).OrderBy(p =>p.create_time).ToList();
                foreach(remote_request reqInfo in requestList){
                    client.EndPoint = reqInfo.request_url +"?"+ reqInfo.request_param;
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

                    reqInfo.deal_flag = 1;
                    reqInfo.deal_time = DateTime.Now;
                    reqInfo.result_message = json;

                    db.remote_request.Attach(reqInfo);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(reqInfo);
                    stateEntity.SetModifiedProperty("deal_flag");
                    stateEntity.SetModifiedProperty("deal_time");
                    stateEntity.SetModifiedProperty("result_message");

                    db.SaveChanges();
                }
            }
            client.EndPoint = "http://182.61.43.29/webapi/api/Users/GetUserList?myUnitId=1";
            
            client.Method = HttpVerb.GET;
            //client.PostData = "{jsonbean:value}";
            client.AddHeader("token:14b0c863-df27-4e05-8e4c-a2bd3169d7d5");
            //var json = client.MakeRequest();
            log.Info(json.ToString());
            //this.responseText.Text = json.ToString();
        }
    }
}
