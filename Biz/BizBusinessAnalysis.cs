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

        public class StatisticGlobalBean
        {
            public double avgdaypeople { get; set; }
            public int itemNum { get; set; }
            public decimal avgsummoney { get; set; }
            public double olduserpercent { get; set; }
            public double memuserpercent { get; set; }
            public double periodnum { get; set; }
        }

        public class StatisticActivityBean
        {
            public  int cnt { get; set; }
            public string cname { get; set; }
            public int used { get; set; }
            public DateTime edate { get; set; }
            public int ifmoney { get; set; }
            public int okjian { get; set; }

        }

        public class StatisticItemBean
        {
            public decimal amount { get; set; }
            public decimal price { get; set; }
            public int excepnum { get; set; }
            public int sellnum { get; set; }
            public decimal weight { get; set; }
            public string name { get; set; }
            public int itemkey { get; set; }
            public string unit { get; set; }
            public int guigeid { get; set; }
            public string guigename { get; set; }
            public DateTime createdate { get; set; }
            public string caegoryname { get; set; }
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

        public StatisticGlobalBean getStatisticGlabolInfo(DateTime? sdate, DateTime? edate)
        {
            try
            {
                StatisticGlobalBean bean = new StatisticGlobalBean();
                DianDianEntities db = new DianDianEntities();
                int shopkey = Properties.Settings.Default.shopkey;
                string datesql = "";
                if (sdate != null)
                {
                    datesql += " and a.createdate >= '" + sdate.Value.ToString("yyyy-MM-dd") + " 00:00:00' ";
                }
                if (edate != null)
                {
                    datesql += " and a.createdate <= '" + edate.Value.ToString("yyyy-MM-dd") + " 23:59:59' ";
                }
                int daynum = 0;
                var sir = db.shop_income_record.Where(p => p.shopkey == shopkey).OrderBy(p => p.createDate).FirstOrDefault();
                if(sir != null)
                {
                    var timespan = DateTime.Now - sir.createDate;
                    daynum = timespan.Value.Days;
                }
                int peopleNum = 0;
                if (daynum != 0)
                {
                    string sql = "select sum(a.peopleNum) peopleNum from( select peopleNum,shopkey from cf_main mn  where state = 2 " + datesql + " group by cfmainkey)a WHERE a.shopkey=" + shopkey;
                    peopleNum = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                    bean.avgdaypeople = peopleNum / daynum;
                }

                bean.itemNum = db.item.Where(p => p.isDel != 1 && p.state == 1 && p.shopkey == shopkey).Count();
                if (peopleNum > 0)
                {
                    var sumrsl = db.cf_main.Where(p => p.state == 2 && p.shopkey == shopkey);
                    if (sdate != null)
                    {
                        sumrsl = sumrsl.Where(p => p.createDate >= sdate);
                    }
                    if (edate != null)
                    {
                        sumrsl = sumrsl.Where(p => p.createDate <= edate);
                    }
                    bean.avgsummoney = sumrsl.Sum(p => p.realPay).Value/peopleNum;
                }
                var userList = getStatisticInfo(sdate, edate);
                int newusernum=0, oldusernum=0, memusernum=0;
                foreach(var userInfo in userList)
                {
                    if (userInfo.name.Equals("新顾客"))
                    {
                        newusernum = userInfo.cnt;
                    }else if (userInfo.name.Equals("老顾客"))
                    {
                        oldusernum = userInfo.cnt;
                    }else if (userInfo.name.Equals("会员"))
                    {
                        memusernum = userInfo.cnt;
                    }
                }
                int allnum = newusernum + oldusernum + memusernum;
                if(oldusernum != 0)
                {
                    bean.olduserpercent = (oldusernum + 0.0) * 100 / allnum;
                }
                if(memusernum != 0)
                {
                    bean.memuserpercent = (memusernum + 0.0) * 100 / allnum;
                }

                string sql2 = "select sum(b.hours) / Sum(b.cnt) as period from ( "
                + "select TIMESTAMPDIFF(HOUR, min(a.createdate),'" + edate.Value.ToString("yyyy-MM-dd") + "') hours,count(a.memberkey) cnt,a.memberkey from( "
                + "   SELECT m.createdate,m.memberkey FROM cf_member m LEFT JOIN cf_meal ml ON m.cfmealkey=ml.cfmealkey WHERE ml.isComplete=1 AND shopkey=" + shopkey + datesql + " GROUP BY m.cfmealkey ORDER BY m.memberkey,m.createdate "
                + " )a  group by a.memberkey) b";
                bean.periodnum = db.Database.SqlQuery<double>(sql2).FirstOrDefault();

                return bean;
            }
            catch(Exception e)
            {
                log.Error("getStatisticGlabolInfo error! msg=" + e.Message);
                throw;
            }
        }

        public List<StatisticActivityBean> QueryStatisticCoupons()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                int shopkey = Properties.Settings.Default.shopkey;

                string sql = "SELECT COUNT(mp.id) cnt,dc.cname,COUNT(mp.isuse=1) used,dc.addtime,dc.edate,dc.ifmoney,dc.okjian"
                + " FROM dd_coupons dc LEFT JOIN dd_mem_coupons mp ON dc.couponid=mp.couponid WHERE dc.shopid=" + shopkey + " GROUP BY dc.couponid ORDER BY dc.addtime desc";

                return db.Database.SqlQuery<StatisticActivityBean>(sql).ToList();
            }
            catch (Exception e)
            {
                log.Error("queryStatisticCoupons error! msg=" + e.Message);
                throw;
            }
        }

        public List<StatisticItemBean> QueryStatisticItem(string itemname, string categoryname, int bycategory, int issum, int order, DateTime? sdate, DateTime? edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                int shopkey = Properties.Settings.Default.shopkey;
                string sql = "";
                if (bycategory != 1)
                {
                    string strItemList = "";
                    if (!categoryname.Equals(""))
                    {
                        sql = "SELECT GROUP_CONCAT(icm.itemkey) itemkeys FROM item_category_map icm LEFT JOIN item_category ic ON ic.itemcategorykey=icm.itemcategorykey"
                            + "  WHERE ic.name LIKE '%" + categoryname + "%'AND shopkey=" + shopkey;
                        strItemList = db.Database.SqlQuery<string>(sql).FirstOrDefault();
                    }
                    string fromsql = " cf_main mn"
                    + " LEFT JOIN cf_detail cd ON cd.cfmainkey = mn.cfmainkey"
                    + " LEFT JOIN item it ON it.itemkey = cd.itemkey"
                    + " LEFT JOIN item_standard gg ON gg.standardkey=cd.guigeid";
                    String wheresql = " mn.shopkey = " + shopkey + " AND it.itemkey IS NOT NULL AND it.isware!=1 AND mn.state=2";
                    if (sdate != null)
                    {
                        wheresql += " and mn.createDate>='" + sdate.Value.ToString("yyyy-MM-dd") + "'";
                    }
                    if (edate != null)
                    {
                        wheresql += " and mn.createDate <='" + edate.Value.ToString("yyyy-MM-dd") + "'";
                    }
                    if (!itemname.Equals(""))
                    {
                        wheresql += " and it.name like '%" + itemname + "%'";
                    }

                    if (!strItemList.Equals(""))
                    {
                        wheresql += " and it.itemkey in (" + strItemList + ")";
                    }
                    String queryStr = " 1=1 ";
                    String fieldssql = null;

                    if (issum == 1)
                    {
                        wheresql += "  GROUP BY cd.itemkey,cd.guigeid";
                        fieldssql = "	SUM(IF(cd.isException!=1,cd.price,0)) amount,"
                                + " IF(gg.sprice IS NOT NULL,gg.sprice,it.discountprice)price,"
                                + " COUNT(IF(cd.isException=1,TRUE,NULL))    excepnum,"
                                + " COUNT(IF(cd.isException!=1,TRUE,NULL))    `sellnum`,"
                                + " SUM(IF(cd.isException!=1,weight,0))    `weight`,"
                                + " it.name,"
                                + " it.itemkey,"
                                + " it.unit,"
                                + " cd.guigeid,"
                                + " cd.guigename";
                        queryStr += " GROUP BY a.itemkey,a.guigeid ORDER BY ";

                        if (order == 1)
                        {
                            queryStr += " ic.name ,";
                        }
                        queryStr += "a.sellNum DESC,CONVERT(a.name USING gbk)COLLATE gbk_chinese_ci ASC";
                    }
                    else
                    {
                        wheresql += "  GROUP BY LEFT(mn.createdate,10),cd.itemkey,cd.guigeid";
                        fieldssql = "	SUM(IF(cd.isException!=1,cd.price,0)) amount,"
                                + " IF(gg.sprice IS NOT NULL,"
                                + "		(IF(gg.sagioprice IS NOT NULL AND gg.sagioprice!=0,gg.sagioprice, gg.sprice)),"
                                + "		(IF(it.agioprice IS NOT NULL AND it.agioprice!=0,it.agioprice, it.discountprice)))    price,"
                                + " COUNT(IF(cd.isException=1,TRUE,NULL))    excepnum,"
                                + " COUNT(IF(cd.isException!=1,TRUE,NULL))    `sellnum`,"
                                + " SUM(IF(cd.isException!=1,weight,0))    `weight`,"
                                + " it.name,"
                                + " it.itemkey,"
                                + " it.unit,"
                                + " cd.guigeid,"
                                + " cd.guigename,"
                                + " DATE(mn.createdate) createdate";
                        queryStr += " GROUP BY a.itemkey,a.guigeid,a.createdate ORDER BY ";
                        if (order == 1)
                        {
                            queryStr += " ic.name ,";
                        }
                        queryStr += " a.createdate DESC ,a.sellNum DESC,CONVERT(a.name USING gbk)COLLATE gbk_chinese_ci ASC";
                    }
                    sql = "select " + fieldssql + " from " + fromsql + " where " + wheresql;

                    sql = "select a.*,GROUP_CONCAT(ic.name)  categoryname from (" + sql + ") a "
                        + "  LEFT JOIN item_category_map icm  ON icm.itemkey=a.itemkey  "
                        + "  LEFT JOIN item_category ic ON ic.itemcategorykey=icm.itemcategorykey where " + queryStr;

                    var rslList = db.Database.SqlQuery<StatisticItemBean>(sql);
                    return rslList.ToList();
                }
                else
                {
                    String fromsql = " cf_main mn"
                    + " LEFT JOIN cf_detail cd ON cd.cfmainkey = mn.cfmainkey"
                    + " LEFT JOIN item it ON it.itemkey = cd.itemkey"
                    + " LEFT JOIN ("
                    + "		SELECT icm.*,ic.name categoryname FROM item_category_map icm LEFT JOIN item_category ic  ON ic.itemcategorykey=icm.itemcategorykey WHERE icm.itemcategorykey IS NOT NULL AND ic.shopkey=" + shopkey + " GROUP BY  icm.itemkey ORDER BY icm.itemcategorykey"
                    + " ) itc ON itc.itemkey=it.itemkey";
                    String wheresql = " mn.shopkey = " + shopkey + " AND it.itemkey IS NOT NULL AND it.isware!=1 AND mn.state=2";
                    if (sdate != null)
                    {
                        wheresql += " and mn.createDate >='" + sdate.Value.ToString("yyyy-MM-dd") + "'";
                    }
                    if (edate != null )
                    {
                        wheresql += " and mn.createDate <='" + edate.Value.ToString("yyyy-MM-dd") + "'";
                    }
                    if (!itemname.Equals(""))
                    {
                        wheresql += " and it.name like '%" + itemname + "%'";
                    }
                    String queryStr = " 1=1 ";
                    String fieldssql = null;
                    if (issum == 1)
                    {
                        wheresql += "  GROUP BY itc.itemcategorykey  ORDER BY ";
                        if (order==1)
                        {
                            wheresql += " itc.categoryname ,";
                        }
                        wheresql += "COUNT(IF(cd.isException!=1,TRUE,NULL)) DESC ";
                        fieldssql = " SUM(IF(cd.isException!=1,cd.price,0))    amount,"
                                + " COUNT(IF(cd.isException=1,TRUE,NULL))    excepnum,"
                                + " COUNT(IF(cd.isException!=1,TRUE,NULL))    `sellnum`,"
                                + "	itc.categoryname";
                    }
                    else
                    {
                        wheresql += "  GROUP BY itc.itemcategorykey,mn.createdate  ORDER BY ";
                        if (order==1)
                        {
                            wheresql += " itc.categoryname ,";
                        }
                        wheresql += " DATE(mn.createdate) desc, COUNT(IF(cd.isException!=1,TRUE,NULL)) DESC ";
                        fieldssql = " SUM(IF(cd.isException!=1,cd.price,0))    amount,"
                                + " COUNT(IF(cd.isException=1,TRUE,NULL))    excepnum,"
                                + " COUNT(IF(cd.isException!=1,TRUE,NULL))    `sellnum`,"
                                + " DATE(mn.createdate)    createdate,"
                                + "	itc.categoryname";
                    }
                    sql = "select " + fieldssql + " from " + fromsql + " where " + wheresql;
                    var rslList = db.Database.SqlQuery<StatisticItemBean>(sql);
                    return rslList.ToList();
                }
            }
            catch (Exception e)
            {
                log.Error("QueryStatisticItem error! msg=" + e.Message);
                throw;
            }
        }
    }
}
