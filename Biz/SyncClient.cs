using DianDianClient.Models;
using DianDianClient.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace DianDianClient.Biz
{
    class SyncClient
    {
        static log4net.ILog log = log4net.LogManager.GetLogger("SyncClient");
        //private DianDianEntities db = new DianDianEntities();
        static public String token = "1507700568237";
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


        public class SyncItemBean 
        {
            public item itemInfo { get; set; }
            public List<item_standard> itemStandardList { get; set; }
            public List<item_category_map> itemCategoryList { get; set; }

            public List<item_set> itemSetList { get; set; }
            public List<storage_item_crude> itemCrudeList { get; set; }
        }
        public class SyncItemRequest
        {
            public List<item_category> categoryList { get; set; }
            public List<SyncItemBean> itemBeanList { get; set; }
        }

        public class SyncTableRequest
        {
            public List<dd_table_floor> floorList { get; set; }
            public List<table_pos> tableList { get; set; }
        }

        public class SyncStorageRequest
        {
            public List<storage_genre> genresList { get; set; }
            public List<storage_crude> crudeList { get; set; }
            public List<storage_stock> stockList { get; set; }
        }

        public class SyncMemberRoleRequest
        {
            public List<member> memList { get; set; }
            public List<sys_role> roleList { get; set; }
        }

        public class SyncSignUserRequest
        {
            public List<dd_shop_signusers> signUserList { get; set; }
            public List<dd_sign_accounts> signAccountsList { get; set; }
            public List<dd_sign_meals> signMealsList { get; set; }
            public List<dd_mem_card> memCard { get; set; }
        }
        public SyncClient()
        {
            
        }
        static public string Login(String requestUrl, String requestParam)
        {
            try
            {
                RestClient client = new RestClient();
                client.ContentType = "application/json";
                client.EndPoint = requestUrl + "?" + requestParam;
                client.Method = HttpVerb.GET;
                string result = client.MakeRequest();
                log.Debug(result);
                return result;
            }catch(Exception e)
            {
                log.Error("Login error, msg = " + e.Message);
                throw;
            }
        }

        public void SyncMethod()
        {
            try {
                var json = "";
                DianDianEntities db = new DianDianEntities();
                RestClient client = new RestClient();
                client.ContentType = "application/json";
                while (true)
                {
                    try
                    {
                        List<remote_request> requestList = db.remote_request.Where(p => p.deal_flag == 0).OrderBy(p => p.create_time).ToList();
                        foreach (remote_request reqInfo in requestList)
                        {
                            client.EndPoint = reqInfo.request_url + "?" + reqInfo.request_param + "&token=" + token;
                            if (reqInfo.request_type.Equals("POST", StringComparison.OrdinalIgnoreCase))
                            {
                                client.Method = HttpVerb.POST;
                            }
                            else if (reqInfo.request_type.Equals("PUT", StringComparison.OrdinalIgnoreCase))
                            {
                                client.Method = HttpVerb.PUT;
                            }
                            else if (reqInfo.request_type.Equals("DELETE", StringComparison.OrdinalIgnoreCase))
                            {
                                client.Method = HttpVerb.DELETE;
                            }
                            else
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
                    }catch(Exception e)
                    {
                        log.Error("SyncMethod error, msg = " + e.Message);
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Login error, msg = " + e.Message);
                throw;
            }
        }

        public void SyncInfoList()
        {
            
            while (true)
            {
                try
                {
                    //Thread.Sleep(5000);
                    //SyncItemInfo();

                    //SyncTablePos();

                    //SyncStorageInfo();
                    SyncMemberInfo();
                    //SyncSignUserInfo();
                    //SyncActivityInfo();
                    //SyncWindowsInfo();
                    
                }catch(Exception e)
                {
                    log.Error("SyncInfoList error, msg = " + e.Message);
                    continue;
                }
            }
            
        }

        public void SyncItemInfo()
        {
            try
            {
                var json = "";
                DianDianEntities db = new DianDianEntities();
                RestClient client = new RestClient();
                client.ContentType = "application/json";
                client.Method = HttpVerb.POST;
                var strShopkey = "?shopkey=" + Properties.Settings.Default.shopkey;

                SyncItemRequest itemRequest = new SyncItemRequest();
                itemRequest.itemBeanList = new List<SyncItemBean>();
                itemRequest.categoryList = db.item_category.Where(p => p.syncFlag == 1).ToList();

                //增量同步菜品
                var foodList = db.item.Where(p => p.syncFlag == 1).ToList();
                foreach (var fooditem in foodList)
                {
                    SyncItemBean itemBean = new SyncItemBean();
                    itemBean.itemInfo = fooditem;
                    itemBean.itemCategoryList = db.item_category_map.Where(p => p.itemkey == fooditem.itemkey).ToList();
                    itemBean.itemStandardList = db.item_standard.Where(p => p.itemKey == fooditem.itemkey).ToList();
                    itemBean.itemSetList = db.item_set.Where(p => p.itemkey == fooditem.itemkey || p.tcItemKey == fooditem.itemkey).ToList();
                    itemBean.itemCrudeList = db.storage_item_crude.Where(p => p.itemkey == fooditem.itemkey).ToList();
                    itemRequest.itemBeanList.Add(itemBean);
                }

                client.EndPoint = SysConstant.BASE_URI + SysConstant.SYNC_ITEM_URL + token;

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //JsonSerializer ser = JsonSerializer.Create(settings);

                client.PostData = JsonConvert.SerializeObject(itemRequest, settings);
                log.Debug(client.PostData);
                json = client.MakeRequest();
                log.Debug(json.ToString());
                var res = JsonConvert.DeserializeObject<Models.CommonResponseBean>(json);
                if (res.code == 100)
                {
                    //db.ClearItemSyncFlag();                
                    //db.ClearCategorySyncFlag();
                }
            }catch(Exception e)
            {
                log.Error("Login SyncItemInfo, msg = " + e.Message);
                throw;
            }
        }

        private void SyncTablePos()
        {
            try
            {
                var json = "";
                DianDianEntities db = new DianDianEntities();
                RestClient client = new RestClient();
                client.ContentType = "application/json";
                client.Method = HttpVerb.POST;
                SyncTableRequest request = new SyncTableRequest();
                request.tableList = db.table_pos.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
                request.floorList = db.dd_table_floor.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();

                client.EndPoint = SysConstant.BASE_URI + SysConstant.SYNC_ITEM_URL + token;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                client.PostData = JsonConvert.SerializeObject(request, settings);
                log.Debug(client.PostData);
                json = client.MakeRequest();
                log.Debug(json.ToString());
                var res = JsonConvert.DeserializeObject<Models.CommonResponseBean>(json);
                if (res.code == 100)
                {
                    //db.ClearTablePosSyncFlag();
                }
            }catch(Exception e)
            {
                log.Error("Login SyncItemInfo, msg = " + e.Message);
                throw;
            }
        }

        private void SyncStorageInfo()
        {
            try
            {
                var json = "";
                DianDianEntities db = new DianDianEntities();
                RestClient client = new RestClient();
                client.ContentType = "application/json";
                client.Method = HttpVerb.POST;
                SyncStorageRequest request = new SyncStorageRequest();
                int shopkey = Properties.Settings.Default.shopkey;

                request.genresList = db.storage_genre.Where(p => p.shopkey == shopkey).DefaultIfEmpty().ToList();
                request.crudeList = db.storage_crude.Where(p => p.shopkey == shopkey).DefaultIfEmpty().ToList();
                request.stockList = db.storage_stock.Where(p => p.shopkey == shopkey).DefaultIfEmpty().ToList();
                
                client.EndPoint = SysConstant.BASE_URI + SysConstant.SYNC_ITEM_URL + token;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                client.PostData = JsonConvert.SerializeObject(request, settings);
                log.Debug(client.PostData);
                json = client.MakeRequest();
                log.Debug(json.ToString());
                var res = JsonConvert.DeserializeObject<Models.CommonResponseBean>(json);
                if (res.code == 100)
                {
                    //db.ClearTablePosSyncFlag();
                }
            }
            catch (Exception e)
            {
                log.Error("Login SyncStorageInfo, msg = " + e.Message);
                throw;
            }
        }

        private void SyncMemberInfo()
        {
            try
            {
                var json = "";
                DianDianEntities db = new DianDianEntities();
                RestClient client = new RestClient();
                client.ContentType = "application/json";
                client.Method = HttpVerb.POST;
                SyncMemberRoleRequest request = new SyncMemberRoleRequest();
                int shopkey = Properties.Settings.Default.shopkey;
                request.memList = db.member.Where(p => p.syncFlag == 1 && p.shopkey == shopkey).ToList();
                request.roleList = db.sys_role.Where(p => p.shopkey == shopkey && p.syncFlag == 1).ToList();
                

                client.EndPoint = SysConstant.BASE_URI + SysConstant.SYNC_ITEM_URL + token;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                client.PostData = JsonConvert.SerializeObject(request, settings);
                log.Debug(client.PostData);
                json = client.MakeRequest();
                log.Debug(json.ToString());
                var res = JsonConvert.DeserializeObject<Models.CommonResponseBean>(json);
                if (res.code == 100)
                {
                    //db.ClearTablePosSyncFlag();
                }
            }
            catch (Exception e)
            {
                log.Error("Login SyncMemberInfo, msg = " + e.Message);
                throw;
            }
        }

        private void SyncSignUserInfo()
        {
            try
            {
                var json = "";
                DianDianEntities db = new DianDianEntities();
                RestClient client = new RestClient();
                client.ContentType = "application/json";
                client.Method = HttpVerb.POST;
                SyncSignUserRequest request = new SyncSignUserRequest();
                int shopkey = Properties.Settings.Default.shopkey;

                request.signUserList = db.dd_shop_signusers.Where(p => p.shopkey == shopkey && p.syncFlag == 1).ToList();
                request.signAccountsList = db.dd_sign_accounts.Where(p => p.shopkey == shopkey && p.syncFlag == 1).ToList();
                request.signMealsList = db.dd_sign_meals.Where(p => p.shopkey == shopkey && p.syncFlag == 1).ToList();
                request.memCard = db.dd_mem_card.Where(p => p.shopkey == shopkey && p.syncFlag == 1).ToList();

                client.EndPoint = SysConstant.BASE_URI + SysConstant.SYNC_ITEM_URL + token;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                client.PostData = JsonConvert.SerializeObject(request, settings);
                log.Debug(client.PostData);
                json = client.MakeRequest();
                log.Debug(json.ToString());
                var res = JsonConvert.DeserializeObject<Models.CommonResponseBean>(json);
                if (res.code == 100)
                {
                    //db.ClearTablePosSyncFlag();
                }
            }
            catch (Exception e)
            {
                log.Error("Login SyncSignUserInfo, msg = " + e.Message);
                throw;
            }
        }

        private void SyncActivityInfo()
        {
            try
            {
                var json = "";
                DianDianEntities db = new DianDianEntities();
                RestClient client = new RestClient();
                client.ContentType = "application/json";
                client.Method = HttpVerb.POST;
                List<dd_coupons> request = new List<dd_coupons>();
                int shopkey = Properties.Settings.Default.shopkey;

                request = db.dd_coupons.Where(p => p.shopid == shopkey && p.syncFlag == 1).ToList();                

                client.EndPoint = SysConstant.BASE_URI + SysConstant.SYNC_ITEM_URL + token;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                client.PostData = JsonConvert.SerializeObject(request, settings);
                log.Debug(client.PostData);
                json = client.MakeRequest();
                log.Debug(json.ToString());
                var res = JsonConvert.DeserializeObject<Models.CommonResponseBean>(json);
                if (res.code == 100)
                {
                    //db.ClearTablePosSyncFlag();
                }
            }
            catch (Exception e)
            {
                log.Error("Login SyncActivityInfo, msg = " + e.Message);
                throw;
            }
        }

        private void SyncWindowsInfo()
        {
            try
            {
                var json = "";
                DianDianEntities db = new DianDianEntities();
                RestClient client = new RestClient();
                client.ContentType = "application/json";
                client.Method = HttpVerb.POST;
                List<dd_shop_windows> request = new List<dd_shop_windows>();
                int shopkey = Properties.Settings.Default.shopkey;

                request = db.dd_shop_windows.Where(p => p.shopid == shopkey && p.syncFlag == 1).ToList();

                client.EndPoint = SysConstant.BASE_URI + SysConstant.SYNC_ITEM_URL + token;
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                client.PostData = JsonConvert.SerializeObject(request, settings);
                log.Debug(client.PostData);
                json = client.MakeRequest();
                log.Debug(json.ToString());
                var res = JsonConvert.DeserializeObject<Models.CommonResponseBean>(json);
                if (res.code == 100)
                {
                    //db.ClearTablePosSync
                }
            }
            catch (Exception e)
            {
                log.Error("Login SyncWindowsInfo, msg = " + e.Message);
                throw;
            }
        }
    }
}
