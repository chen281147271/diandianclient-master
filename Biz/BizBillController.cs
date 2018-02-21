using DianDianClient.Models;
using DianDianClient.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace DianDianClient.Biz
{
    class BizBillController
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizBillController");
        private DianDianEntities db = new DianDianEntities();

        //获取订单
        private string GetBillRequestUrl = "http://app.diandiancaidan.com/item/api.do";
        //确认订单
        private string ConfirmBillUrl = "http://app.diandiancaidan.com/shop/api.do";
        //退单
        private string CancelBillUrl = "http://app.diandiancaidan.com/shop/api.do";



        //从服务器获取订单
        public void RemoteGetBillList()
        {
            DianDianEntities db = new DianDianEntities();
            RestClient client = new RestClient();
            client.ContentType = "application/json";
            
            client.Method = HttpVerb.GET;

            while (!SyncClient.token.Equals(""))
            {
                string requestParam = "m=GetBillRequestUrl&sdate=" + DateTime.Now.ToString("d");
                requestParam += "&edate=" + DateTime.Now.ToString("d");
                requestParam += "&p=1";
                requestParam += "&isConfirm=0";
                requestParam += "&token=" + SyncClient.token;
                client.EndPoint = GetBillRequestUrl + "?" + requestParam;

                string result = client.MakeRequest();
                log.Debug(result);

                GetBillResponseBean gbrb = JsonConvert.DeserializeObject<GetBillResponseBean>(result);
                //插入本地数据库
                List<GetBillResultResponseBean> billList = JsonConvert.DeserializeObject< List<GetBillResultResponseBean> >(gbrb.results);
                foreach(GetBillResultResponseBean billInfo in billList)
                {

                }


                Thread.Sleep(5000);
            }
        }

        //查询订单
        public List<cf_main> QueryBillList(int isConfirm, int state, DateTime sdate, DateTime edate)
        {
            List<cf_main> billList = db.cf_main.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                && Convert.ToDateTime(p.createDate) >= sdate && Convert.ToDateTime(p.createDate) <= edate).ToList();
            if(isConfirm != 0)
            {
                billList = billList.Where(p => p.isConfirm == isConfirm).ToList();
            }
            if(state != 0)
            {
                billList = billList.Where(p => p.state == state).ToList();
            }
            return billList;
        }

        //查询交易流水
        public List<v_cfmainaccount> QueryShopAccount(int paytype, string type, int state, string billNo, int tableNo, DateTime sdate, DateTime edate)
        {
            List<v_cfmainaccount> rslList = db.v_cfmainaccount.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                && Convert.ToDateTime(p.createdate) >= sdate && Convert.ToDateTime(p.createdate) <= edate).ToList();
            if (!billNo.Equals(""))
            {
                rslList = rslList.Where(p => p.type.Equals(billNo)).ToList();
            }
            if (paytype != 0)
            {
                rslList = rslList.Where(p => p.paytype == paytype).ToList();
            }
            if (state != 0)
            {
                rslList = rslList.Where(p => p.state == state).ToList();
            }
            if (!type.Equals("0"))
            {
                rslList = rslList.Where(p => p.type.Equals(type)).ToList();
            }
            if(tableNo != 0)
            {
                rslList = rslList.Where(p => p.tableNo == tableNo).ToList();
            }

            return rslList;
        }

        //查询订单
        public List<dd_book_orders> QueryOrderList(string orderNo, int status, DateTime sdate, DateTime edate)
        {
            List<dd_book_orders> rslList = db.dd_book_orders.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                && Convert.ToDateTime(p.addtime) >= sdate && Convert.ToDateTime(p.addtime) <= edate).ToList();
            if (!orderNo.Equals(""))
            {
                rslList = rslList.Where(p => p.orderno.Equals(orderNo)).ToList();
            }
            if (status != 0)
            {
                rslList = rslList.Where(p => p.status == status).ToList();
            }

            return rslList;

        }
        //确认接单
        public void ConfirmBill(int cfMainkey)
        {
            //本地业务
            remote_request rr = new remote_request();
            rr.create_time = DateTime.Now;
            rr.deal_flag = 0;
            rr.request_type = "GET";
            rr.request_url = ConfirmBillUrl;
            rr.request_param = "m=setPrintState&cfMainkey="+cfMainkey;

            db.remote_request.Add(rr);
            db.SaveChanges();

            //打印小票


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
            //本地业务

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

        //开桌
        public void BeginToUse(int tableposkey, int peoples)
        {
            //本地业务

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

        //清桌
        public void ClearTable(int tableposkey)
        {
            //本地业务

            remote_request rr = new remote_request();
            rr.create_time = DateTime.Now;
            rr.deal_flag = 0;
            rr.request_type = "GET";
            rr.request_url = ConfirmBillUrl;
            rr.request_param = "m=clearTable&tableposkey=" + tableposkey;

            db.remote_request.Add(rr);
            db.SaveChanges();
        }

        //餐桌使用列表
        public List<TableInfoBean> GetTablePosList()
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

        //餐桌使用详情
        public List<v_cfdetailitem> GetTableDetailInfo(string cfmainkey)
        {
            return db.v_cfdetailitem.Where(p => p.shopkey == Properties.Settings.Default.shopkey && p.cfmainkey.Equals(cfmainkey)).ToList();
            
        }


        //转台
        public void ChangeTable(int tableposkey, int newtableno)
        {
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

        //合桌
        //合并的mealkey，合并前提必须开桌并点餐：例如以下
        //[{cfmealkey:"e5f5fd47f8f4413c8000eef71fcaa6c0"},{cfmealkey:"badfc83729104a008c4bbf517b7de130"}]

        public void MergeTable(string meals)
        {
            //本地业务

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

        //10. 菜品打折接口
        public void Dazhe(double price, int cfdetailkey, int cfmainkey)
        {
            //本地业务

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

        //11. 餐桌订单优惠接口
        public void Saveyouhui(double youhui, int cfmainkey)
        {
            //本地业务

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

        //12. 买单接口
        public void Maidan(int payway, int cardid, int tableposkey, int isClear, double amount, double realPay)
        {
            //本地业务

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

        //13. 下单接口
        public void SubmitBill(int peopleNum, string remark, string scTime, int signid, int cardid, string str, int tableposkey, string customRemark)
        {
            //本地业务

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
        }

        //查询交易流水
        public void QueryJYRecord(int dh, int zh, int skfs, int lx,int zt, DateTime sdate, DateTime edate)
        {

        }

    }
}
