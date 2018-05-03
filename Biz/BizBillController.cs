using DianDianClient.Models;
using DianDianClient.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace DianDianClient.Biz
{
    class BizBillController
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizBillController");
        //private DianDianEntities db = new DianDianEntities();
        
        //获取订单
        private string GetBillRequestUrl = "http://test.diandiancaidan.com/item/api.do";
        //确认订单
        private string ConfirmBillUrl = "http://app.diandiancaidan.com/shop/api.do";
        //退单
        private string CancelBillUrl = "http://app.diandiancaidan.com/shop/api.do";

        public class BillDetailResponseBean
        {
            public List<CfMemberMealBean> memberList { get; set; }
            public List<v_cfdetailitem> itemList { get; set; }
        }

        public class CfMemberMealBean
        {
            public int? userid { get; set; }
            public int? shopkey { get; set; }
            public DateTime? createdate { get; set; }
            public decimal? avedate { get; set; }
            public string name { get; set; }
            public string icon { get; set; }
            public string tel { get; set; }
            public string cardid { get; set; }
            public int? cnt { get; set; }
        }
        //从服务器获取订单
        public void RemoteGetBillList()
        {
            try
            {
                
                DianDianEntities db = new DianDianEntities();
                RestClient client = new RestClient();
                client.ContentType = "application/json";

                client.Method = HttpVerb.GET;

                while (true)
                {
                    Thread.Sleep(6000);
                    if (MyModels.userinfo.user.token.Equals(""))
                    {
                        log.Warn("have not remote login");
                        continue;
                    }
                    string requestParam = "m=getBillListWithoutClient&sdate=" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    requestParam += "&edate=" + DateTime.Now.ToString("yyyy-MM-dd");
                    requestParam += "&p=1";
                    requestParam += "&isConfirm=0";
                    requestParam += "&token=" + MyModels.userinfo.user.token;
                    //requestParam += "&type=0";
                    client.EndPoint = GetBillRequestUrl + "?" + requestParam;

                    string result = client.MakeRequest();
                    log.Debug(result);

                    GetBillResponseBean gbrb = JsonConvert.DeserializeObject<GetBillResponseBean>(result);
                    //插入本地数据库
                    if(gbrb.code.Equals("100"))
                    {
                        foreach (GetBillResultResponseBean billInfo in gbrb.results)
                        {
                            //已存在的跳过，未存在的插入
                        }
                    }
                    

                    
                }
            }
            catch (Exception e)
            {
                log.Error("RemoteGetBillList error. msg=" + e.Message);
                throw;
            }
        }

        //查询订单
        public List<v_cfmainmeal> QueryBillList(int isConfirm, int state, DateTime sdate, DateTime edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var billList = db.v_cfmainmeal.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                    && p.createDate >= sdate && p.createDate <= edate);
                if (isConfirm != 0)
                {
                    billList = billList.Where(p => p.isConfirm == isConfirm);
                }
                if (state != 0)
                {
                    billList = billList.Where(p => p.state == state);
                }
                return billList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryBillList error. msg=" + e.Message);
                throw;
            }
        }

        //查询交易流水
        public List<v_cfmainaccount> QueryShopAccount(int paytype, string type, int state, string billNo, int tableNo, DateTime sdate, DateTime edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var rslList = db.v_cfmainaccount.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                    && p.createdate >= sdate && p.createdate <= edate);
                if (!billNo.Equals(""))
                {
                    rslList = rslList.Where(p => p.type.Equals(billNo));
                }
                if (paytype != 0)
                {
                    rslList = rslList.Where(p => p.paytype == paytype);
                }
                if (state != 0)
                {
                    rslList = rslList.Where(p => p.state == state);
                }
                if (!type.Equals(""))
                {
                    rslList = rslList.Where(p => p.type.Equals(type));
                }
                if (tableNo != 0)
                {
                    rslList = rslList.Where(p => p.tableNo == tableNo);
                }
                return rslList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryShopAccount error. msg=" + e.Message);
                throw;
            }
        }

        //查询订单
        public List<dd_book_orders> QueryOrderList(string orderNo, int status, DateTime sdate, DateTime edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var rslList = db.dd_book_orders.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                    && p.addtime >= sdate && p.addtime <= edate);
                if (!orderNo.Equals(""))
                {
                    rslList = rslList.Where(p => p.orderno.Equals(orderNo));
                }
                if (status != 0)
                {
                    rslList = rslList.Where(p => p.status == status);
                }
                return rslList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryOrderList error. msg=" + e.Message);
                throw;
            }
        }
        //确认接单
        public void ConfirmBill(string cfMainkey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                //本地业务
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = ConfirmBillUrl;
                rr.request_param = "m=setPrintState&cfMainkey=" + cfMainkey;

                db.remote_request.Add(rr);
                db.SaveChanges();
                //打印小票

            }
            catch (Exception e)
            {
                log.Error("ConfirmBill error. msg=" + e.Message);
                throw;
            }

        }

        //退单
        /*
        paytype string 支付类型对应dd_shop_payway表id字段
        itemkey string 菜品id
        cfmealkey string 就餐订单key
        mainkey string 分单key
        num string 退菜数量
        guigeid string 退菜规格
        exc string 退菜理由
        excstr string 解决方案
        */

        public void Tuicai(int paytype, int itemkey, int cfmealkey, int mainkey, int num, int guigeid, string exc, string excstr)
        {
            try
            {
                //本地业务
                DianDianEntities db = new DianDianEntities();
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = ConfirmBillUrl;
                rr.request_param = "m=tuicai&paytype=" + paytype;
                rr.request_param += "&itemkey=" + itemkey;
                rr.request_param += "&cfmealkey=" + cfmealkey;
                rr.request_param += "&mainkey=" + mainkey;
                rr.request_param += "&num=" + num;
                rr.request_param += "&guigeid=" + guigeid;
                rr.request_param += "&exc=" + exc;
                rr.request_param += "&excstr=" + excstr;

                db.remote_request.Add(rr);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("Tuicai error. msg=" + e.Message);
                throw;
            }
        }

        //开桌
        public void BeginToUse(int tableposkey, int peoples)
        {
            try
            {
                //本地业务
                DianDianEntities db = new DianDianEntities();
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = ConfirmBillUrl;
                rr.request_param = "m=beginToUse&tableposkey=" + tableposkey;
                rr.request_param += "&peoples=" + peoples;

                db.remote_request.Add(rr);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("BeginToUse error. msg=" + e.Message);
                throw;
            }
        }

        //清桌
        public void ClearTable(int tableposkey)
        {
            try
            {
                //本地业务
                DianDianEntities db = new DianDianEntities();
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = ConfirmBillUrl;
                rr.request_param = "m=clearTable&tableposkey=" + tableposkey;

                db.remote_request.Add(rr);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("ClearTable error. msg=" + e.Message);
                throw;
            }
        }

        //餐桌使用列表
        public List<TableInfoBean> GetTablePosList()
        {
            try
            {
                int shopkey = Properties.Settings.Default.shopkey;
                DianDianEntities db = new DianDianEntities();

                List<TableInfoBean> data = (from o in db.table_pos.Where(p => p.shopkey == shopkey)
                                            join d in db.v_cfmainmeal.Where(p => p.shopkey == shopkey)
                                            on o.tableNo equals d.tableNo into dc
                                            from dci in dc.DefaultIfEmpty()
                                            select new TableInfoBean
                                            {
                                                tableposkey = o.tableposkey,
                                                qrCode = o.qrCode,
                                                tableNo = o.tableNo,
                                                tableName = o.tableName,
                                                peopleNum = o.peopleNum,
                                                peoples = dci.peoples,
                                                state = dci.state,
                                                enable = o.enable,
                                                isDel = o.isDel,
                                                isRoom = o.isRoom,
                                                floorid = o.floorid,
                                                opentime = o.opentime,
                                                shopuserid = o.shopuserid,
                                                shopusername = o.shopusername,
                                                tfuwu = o.tfuwu,
                                                cfmainkey = dci.cfmainkey,
                                                cfmealkey = dci.cfmealkey,
                                                amount = dci.amount,
                                                iscomplete = dci.isComplete
                                            }).ToList();

                return data;
            }
            catch (Exception e)
            {
                log.Error("GetTablePosList error. msg=" + e.Message);
                throw;
            }
        }

        //餐桌使用详情
        public BillDetailResponseBean GetTableDetailInfo(string cfmainkey)
        {
            try
            {
                BillDetailResponseBean bdrb = new BillDetailResponseBean();
                
                DianDianEntities db = new DianDianEntities();
                var itemList = db.v_cfdetailitem.Where(p => p.shopkey == Properties.Settings.Default.shopkey && p.cfmainkey.Equals(cfmainkey)).ToList();
                bdrb.itemList = itemList;
                bdrb.memberList = new List<CfMemberMealBean>();

                var cfMainItem = db.cf_main.Where(p => p.cfmainkey.Equals(cfmainkey)).FirstOrDefault();
                if(cfMainItem == null)
                {
                    log.Error("can not find cf_main ,cfmainkey = " + cfmainkey);
                    return null;
                }
                var userList = db.v_cf_member.Where(p => p.cfmealkey.Equals(cfMainItem.cfmealkey)).GroupBy(p =>p.userid).ToList();
                foreach(var user in userList)
                {
                    string sql = "SELECT 	a.userid  AS userid, a.shopkey AS shopkey, MAX(a.createdate) AS createdate, ";
                    sql += " a.avedate AS avedate, a.name AS name,a.icon AS icon,a.tel AS tel, a.cardid AS cardid, a.cnt ";
                    sql += " FROM( SELECT mr.memberkey AS userid,ml.shopkey AS shopkey, MAX(mr.createDate) AS createdate,";
                    sql += " CAST((TIMESTAMPDIFF(DAY, MIN(mr.createDate), CURDATE()) / COUNT(1)) AS DECIMAL(10, 0)) AS avedate,";
                    sql += " m.name AS name,m.icon AS icon, m.tel  AS tel,mc.cardid AS cardid,  COUNT(1) cnt";
                    sql += " FROM((((cf_member mr  LEFT JOIN member m ON((m.memberkey = mr.memberkey))) ";
                    sql += " LEFT JOIN cf_meal ml	ON((ml.cfmealkey = mr.cfmealkey))) LEFT JOIN dd_mem_card mc ";
                    sql += " ON(((mc.userid = m.memberkey) AND(mc.shopkey = ml.shopkey))))";
                    sql += " LEFT JOIN cf_main mn ON((mn.cfmealkey = mr.cfmealkey)))";
                    sql += " WHERE(mn.state = 2 AND mr.memberkey= "+user.Key+" AND ml.shopkey = "+ Properties.Settings.Default.shopkey + ")";
                    sql += " GROUP BY mr.memberkey,ml.shopkey  ) a GROUP BY a.userid,a.shopkey; ";
                    var result = db.Database.SqlQuery<CfMemberMealBean>(sql);
                    bdrb.memberList.Add(result.FirstOrDefault());
                }
                
                return bdrb;
            }
            catch (Exception e)
            {
                log.Error("GetTableDetailInfo error. msg=" + e.Message);
                throw;
            }
        }


        //转台
        public void ChangeTable(int tableposkey, int newtableno)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                //本地业务

                //异步通知服务器
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = ConfirmBillUrl;
                rr.request_param = "m=clearTable&tableposkey=" + tableposkey;
                rr.request_param += "&newtableno=" + newtableno;

                db.remote_request.Add(rr);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("ChangeTable error. msg=" + e.Message);
                throw;
            }
        }

        //合桌
        //合并的mealkey，合并前提必须开桌并点餐：例如以下
        //[{cfmealkey:"e5f5fd47f8f4413c8000eef71fcaa6c0"},{cfmealkey:"badfc83729104a008c4bbf517b7de130"}]

        public void MergeTable(string meals)
        {
            try
            {
                //本地业务
                DianDianEntities db = new DianDianEntities();
                //异步通知服务器
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = ConfirmBillUrl;
                rr.request_param = "m=mergeTable&meals=" + meals;

                db.remote_request.Add(rr);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("MergeTable error. msg=" + e.Message);
                throw;
            }
        }

        //10. 菜品打折接口
        public void Dazhe(double price, int cfdetailkey, int cfmainkey)
        {
            try
            {
                //本地业务
                DianDianEntities db = new DianDianEntities();
                //异步通知服务器
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = GetBillRequestUrl;
                rr.request_param = "m=dazhe&price=" + price;
                rr.request_param += "&cfdetailkey=" + cfdetailkey;
                rr.request_param += "&cfmainkey=" + cfmainkey;

                db.remote_request.Add(rr);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("Dazhe error. msg=" + e.Message);
                throw;
            }
        }

        //11. 餐桌订单优惠接口
        public void Saveyouhui(double youhui, int cfmainkey)
        {
            try
            {
                //本地业务
                DianDianEntities db = new DianDianEntities();

                //异步通知服务器
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = ConfirmBillUrl;
                rr.request_param = "m=Saveyouhui&youhui=" + youhui;
                rr.request_param += "&cfmainkey=" + cfmainkey;

                db.remote_request.Add(rr);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("Saveyouhui error. msg=" + e.Message);
                throw;
            }
        }

        //12. 买单接口
        public void Maidan(int payway, int cardid, int tableposkey, int isClear, double amount, double realPay)
        {
            try
            {
                //本地业务
                DianDianEntities db = new DianDianEntities();
                //异步通知服务器
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = ConfirmBillUrl;
                rr.request_param = "m=Maidan&payway=" + payway;
                rr.request_param += "&cardid=" + cardid;
                rr.request_param += "&tableposkey=" + tableposkey;
                rr.request_param += "&isClear=" + isClear;
                rr.request_param += "&amount=" + amount;
                rr.request_param += "&realPay=" + realPay;

                db.remote_request.Add(rr);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("Maidan error. msg=" + e.Message);
                throw;
            }
        }

        //13. 下单接口
        public void SubmitBill(int peopleNum, string remark, string scTime, int signid, int cardid, string str, int tableposkey, string customRemark)
        {
            //本地业务
            try
            {
                DianDianEntities db = new DianDianEntities();
                //异步通知服务器
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = ConfirmBillUrl;
                rr.request_param = "m=submitBill&peopleNum=" + peopleNum;
                rr.request_param += "&remark=" + remark;
                rr.request_param += "&scTime=" + scTime;
                rr.request_param += "&signid=" + signid;
                rr.request_param += "&cardid=" + cardid;
                rr.request_param += "&str=" + str;
                rr.request_param += "&tableposkey=" + tableposkey;
                rr.request_param += "&customRemark=" + customRemark;

                db.remote_request.Add(rr);
                db.SaveChanges();
                
            }catch(Exception e)
            {
                log.Error("SubmitBill error. msg=" + e.Message);
                throw;
            }
        }
        
    }
}
