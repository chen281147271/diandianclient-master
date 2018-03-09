using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace DianDianClient.Biz
{
    class BizBusinessAnalysis
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizBusinessAnalysis");
        private DianDianEntities db = new DianDianEntities();

        //查询营业额
      public decimal QueryTotleTurnover(DateTime sdate, DateTime edate)
        {
            try
            {
                decimal rsl = db.dd_shop_account.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                    && p.createdate >= sdate && p.createdate <= edate).Sum(p => p.money - p.brokerage).Value;

                return rsl;
            }
            catch (Exception e)
            {
                log.Error("QueryTotleTurnover error. msg=" + e.Message);
                throw;
            }
        }
        //查询业务笔数
        public int QueryRecordNums(DateTime sdate, DateTime edate)
        {
            try
            {
                return db.dd_shop_account.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                    && p.createdate >= sdate && p.createdate <= edate).Count();
            }
            catch (Exception e)
            {
                log.Error("QueryRecordNums error. msg=" + e.Message);
                throw;
            }
        }

        //按类型分类统计
        public RecordGroupTotleBean QueryRecordGroupByType(DateTime sdate, DateTime edate)
        {
            try
            {
                //List<dd_shop_account> recordList = db.dd_shop_account.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                //    && Convert.ToDateTime(p.createdate) >= sdate && Convert.ToDateTime(p.createdate) <= edate).ToList();

                //List<dd_shop_account> recordList = db.dd_shop_account.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                //&& SqlFunctions.DateAdd("dd", 0, p.createdate) <= edate).ToList();

                List<v_cfmainaccount> recordList = (from a in db.v_cfmainaccount
                                                    where a.shopkey == Properties.Settings.Default.shopkey
                                                    &&
                                                    a.createdate >= sdate
                                                    &&
                                                    a.createdate <= edate
                                                    && a.state == 2
                                                    select a).ToList();
                RecordGroupTotleBean rslbean = new RecordGroupTotleBean();

                rslbean.groupList = new List<RecordGroupBean>();

                RecordGroupBean bean1 = new RecordGroupBean();
                bean1.keyName = "优惠金额";
                bean1.sumMoney = Convert.ToDecimal(recordList.Sum(p => p.youhui).Value);
                bean1.recList = recordList.ToList();
                rslbean.groupList.Add(bean1);

                RecordGroupBean bean2 = new RecordGroupBean();
                bean2.keyName = "异常金额";
                bean2.sumMoney = Convert.ToDecimal(recordList.Sum(p => p.exceptionAmount).Value);
                bean2.recList = recordList.ToList();
                rslbean.groupList.Add(bean2);

                var feiyongList = (from a in db.dd_meal_tips
                                   where a.shopkey == Properties.Settings.Default.shopkey
                                   && a.paytime >= sdate
                                   && a.paytime <= edate
                                   select a).ToList();

                RecordGroupBean bean3 = new RecordGroupBean();
                bean3.keyName = "服务费";
                bean3.sumMoney = feiyongList.Sum(p => p.feiyong).Value;
                bean3.recList = recordList.Where(p => feiyongList.Select(t => t.cfmealkey).Contains( p.cfmealkey) ).ToList();
                rslbean.groupList.Add(bean3);

                var cardList = (from a in db.dd_card_userecord
                                where a.shopkey == Properties.Settings.Default.shopkey
                                && a.addtime >= sdate
                                && a.addtime <= edate select a).ToList();
                var cardGroupList = cardList.GroupBy(p => p.type).ToList();
                foreach (var rsl in cardGroupList)
                {
                    if(rsl.FirstOrDefault().type == 1)
                    {
                        RecordGroupBean bean4 = new RecordGroupBean();
                        bean4.keyName = "会员消费";
                        bean4.sumMoney = rsl.Sum(p => p.consume).Value;
                        bean4.recList = recordList.Where(p => rsl.Select(t => t.cfmealkey).Contains(p.cfmealkey)).ToList();
                        rslbean.groupList.Add(bean4);
                    }
                    else if(rsl.FirstOrDefault().type == 0)
                    {
                        /*
                        RecordGroupBean bean5 = new RecordGroupBean();
                        bean5.keyName = "会员充值";
                        bean5.sumMoney = rsl.Sum(p => p.smoney).Value;
                        bean5.recList = recordList.Where(p => rsl.Select(t => t.cfmealkey).Contains(p.cfmealkey)).ToList();
                        rslbean.groupList.Add(bean5);
                        */
                        RecordGroupBean bean6 = new RecordGroupBean();
                        bean6.keyName = "会员充值";
                        bean6.sumMoney = rsl.Sum(p => p.consume).Value;
                        bean6.recList = recordList.Where(p => rsl.Select(t => t.cfmealkey).Contains(p.cfmealkey)).ToList();
                        rslbean.groupList.Add(bean6);
                    }
                }
                return rslbean;
            }
            catch (Exception e)
            {
                log.Error("QueryTotleTurnover error. msg=" + e.Message);
                throw;
            }
        }
        //按支付方式分类统计
        public RecordGroupTotleBean QueryRecordGroupByPayType(DateTime sdate, DateTime edate)
        {
            try
            {
                List<v_cfmainaccount> recordList = db.v_cfmainaccount.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                    && p.createdate >= sdate && p.createdate <= edate).ToList();
                var rslList = recordList.GroupBy(p => p.paytype);
                RecordGroupTotleBean rslbean = new RecordGroupTotleBean();
                rslbean.totleCount = recordList.Count();
                rslbean.sumMoney = recordList.Sum(p => p.money - p.brokerage).Value;
                rslbean.groupList = new List<RecordGroupBean>();

                foreach (var rsl in rslList)
                {
                    RecordGroupBean bean = new RecordGroupBean();
                    bean.keyName = (rsl.FirstOrDefault().payway);
                    bean.sumMoney = rsl.Sum(p => p.money - p.brokerage).Value;
                    bean.recList = rsl.ToList();
                    rslbean.groupList.Add(bean);
                }

                return rslbean;
            }
            catch (Exception e)
            {
                log.Error("QueryRecordGroupByPayType error. msg=" + e.Message);
                throw;
            }
        }

        public string BillType2Name(string type)
        {
            string name = "";
            switch (type)
            {
                case "1":
                    name = "正常消费";
                    break;
                case "2":
                    name = "会员充值";
                    break;
                case "3":
                    name = "会员卡消费";
                    break;
                case "4":
                    name = "扫码付款";
                    break;
                case "5":
                    name = "预定消费";
                    break;
                default:
                    name = "未知";
                    break;
            }

            return name;
        }
    }
}
