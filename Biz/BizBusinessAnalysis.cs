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
        //private DianDianEntities db = new DianDianEntities();

        public class StatisticBean{
            public string name { get; set; }
            public int cnt { get; set; }
            public decimal sum { get; set; }
        }

        public class StatisticViewResult
        {
            public int usernum { get; set; }
            public string type { get; set; }
            public int itemnum { get; set; }
            public int itemkey { get; set; }
            public string itemname { get; set; }
        }

        public class StatisticMonthBean
        {
            public string name { get; set; }
            public int month { get; set; }
            public decimal summoney { get; set; }
        }

        public class StatisticMonthResult
        {
            public decimal summoney { get; set; }
            public int month { get; set; }
            public int type { get; set; }
        }
        //查询营业额
      public decimal QueryTotleTurnover(DateTime sdate, DateTime edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
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
                DianDianEntities db = new DianDianEntities();
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
                DianDianEntities db = new DianDianEntities();
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
                DianDianEntities db = new DianDianEntities();
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

        //按日期支付方式分类统计
        public List<RecordGroupTotleBean> QueryRecordGroupByDateByPayType(DateTime sdate, DateTime edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                List<RecordGroupTotleBean> rslbeanList = new List<RecordGroupTotleBean>();
                List<v_cfmainaccount> recordList = db.v_cfmainaccount.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                    && p.createdate >= sdate && p.createdate <= edate).ToList();
                var rslList = recordList.GroupBy(p => p.createdate.Value.ToString("yyyy-MM-dd"));

                foreach(var rsl in rslList)
                {
                    var tmpList = rsl.GroupBy(p => p.paytype);
                    RecordGroupTotleBean rslbean = new RecordGroupTotleBean();
                    rslbean.totleCount = rsl.Count();
                    rslbean.createdate=rsl.FirstOrDefault().createdate.ToString();
                    rslbean.sumMoney = rsl.Sum(p => p.money - p.brokerage).Value;
                    rslbean.groupList = new List<RecordGroupBean>();

                    foreach (var tmp in tmpList)
                    {
                        RecordGroupBean bean = new RecordGroupBean();
                        bean.keyName = (tmp.FirstOrDefault().payway);
                        bean.sumMoney = tmp.Sum(p => p.money - p.brokerage).Value;
                        bean.recList = tmp.ToList();
                        rslbean.groupList.Add(bean);
                    }
                    rslbeanList.Add(rslbean);
                }           

                return rslbeanList;
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

        public List<StatisticBean> getStatisticInfo(DateTime? sdate, DateTime? edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                List<StatisticBean> statisticList = new List<StatisticBean>();

                int shopkey = Properties.Settings.Default.shopkey;
                string datesql = "";
                if(sdate!= null)
                {
                    datesql += " and a.createdate >= '"+sdate.Value.ToString("yyyy-MM-dd")+" 00:00:00' ";
                }
                if(edate != null)
                {
                    datesql += " and a.createdate <= '" + edate.Value.ToString("yyyy-MM-dd") + " 23:59:59' ";
                }
                string sql = "SELECT  x.count usernum,x.type,y.count itemnum,y.itemkey,y.itemname  FROM ( "
                + "	SELECT COUNT(1) 'count',TYPE FROM v_dd_count_mid a WHERE a.shopkey = " + shopkey + datesql + "  GROUP BY a.type)X "
                + "LEFT JOIN "
                + "( "
                + "	(SELECT COUNT(1) 'count',a.* FROM v_dd_count_mid_2 a WHERE a.type=1 AND a.shopkey = " + shopkey + datesql + " GROUP BY a.itemkey ORDER BY COUNT(1) DESC LIMIT 0,3) "
                + "	UNION ALL "
                + "	(SELECT COUNT(1) 'count',a.* FROM v_dd_count_mid_2 a WHERE a.type=2 AND a.shopkey = " + shopkey + datesql + " GROUP BY a.itemkey ORDER BY COUNT(1) DESC LIMIT 0,3) "
                + "	UNION ALL "
                + "	(SELECT COUNT(1) 'count',a.* FROM v_dd_count_mid_2 a WHERE a.type=3 AND a.shopkey = " + shopkey + datesql + " GROUP BY a.itemkey ORDER BY COUNT(1) DESC LIMIT 0,3) "
                + ") AS Y ON y.type=x.type ";

                var resultList = db.Database.SqlQuery<StatisticViewResult>(sql);
                string flag = "0";
                foreach(var result in resultList)
                {
                    StatisticBean bean1 = new StatisticBean();
                    if (!flag.Equals(result.type))
                    {
                        bean1.cnt = result.usernum;
                        bean1.sum = result.itemnum;
                        if ("1".Equals(result.type))
                        {
                            bean1.name = "新顾客";
                        }else if ("2".Equals(result.type))
                        {
                            bean1.name = "老顾客";
                        }
                        else if ("3".Equals(result.type))
                        {
                            bean1.name = "会员";
                        }
                        statisticList.Add(bean1);
                    }
                }
                return statisticList;
            }
            catch(Exception e)
            {
                log.Error("getStatisticInfo error! msg="+e.Message);
                throw;
            }
        }

        public List<StatisticMonthResult> getStatisticLineInfo(int year)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                List<StatisticMonthResult> statisticList = new List<StatisticMonthResult>();
                int shopkey = Properties.Settings.Default.shopkey;

                string sql = "select sum(a.summoney) as summoney,month(a.createdate) month,a.type from ( "
                + "	select sum(mn.realpay) as summoney,"
                + "	  `mr`.`memberkey`  AS `userid`,"
                + "	  `ml`.`shopkey`    AS `shopkey`,"
                + "	  `ml`.`createDate` AS `createdate`,"
                + "	  (case "
                + "		when ((count(1) = 1) and isnull(`d`.`userid`)) then 1"
                + "		when ((count(1) > 1) and isnull(`d`.`userid`)) then 2 "
                + "		when (`d`.`userid` is not null) then 3 "
                + "	   end) AS `type`"
                + "	from ((`cf_member` `mr`"
                + "	    left join `cf_meal` `ml`"
                + "	      on ((`ml`.`cfmealkey` = `mr`.`cfmealkey`) )) "
                + "	    left join `dd_mem_card` `d`"
                + "	      on ((`d`.`userid` = `mr`.`memberkey`)AND UNIX_TIMESTAMP(ml.createDate)>=d.addtime))"
                + "	    left join cf_main mn on mn.cfmealkey=ml.cfmealkey "
                + "	where mn.realpay IS NOT NULL and year(a.createdate) = " + year + " and a.shopkey = " + shopkey
                  + "	group by mr.memberkey,d.addtime  "
                + ")a "                
                + "group by a.type, month(a.createdate) "
                + "order by a.type, month(a.createdate) ";

                statisticList = db.Database.SqlQuery<StatisticMonthResult>(sql).ToList();

                return statisticList;
            }
            catch(Exception e)
            {
                log.Error("getStatisticRechageInfo error! msg=" + e.Message);
                throw;
            }
        }

        public void getStatisticGlabolInfo()
        {
            try
            {

            }catch(Exception e)
            {
                log.Error("getStatisticGlabolInfo error! msg=" + e.Message);
                throw;
            }
        }
    }
}
