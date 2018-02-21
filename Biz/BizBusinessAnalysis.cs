using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizBusinessAnalysis
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizBusinessAnalysis");
        private DianDianEntities db = new DianDianEntities();

        //查询营业额
        decimal QueryTotleTurnover(DateTime sdate, DateTime edate)
        {
            decimal rsl = db.dd_shop_account.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                && Convert.ToDateTime(p.createdate) >= sdate && Convert.ToDateTime(p.createdate) <= edate).Sum(p => p.money).Value;

            return rsl;
        }
        //查询业务笔数
        int QueryRecordNums(DateTime sdate, DateTime edate)
        {
            return db.dd_shop_account.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                && Convert.ToDateTime(p.createdate) >= sdate && Convert.ToDateTime(p.createdate) <= edate).Count();
        }

        //按类型分类统计
        RecordGroupTotleBean QueryRecordGroupByType(DateTime sdate, DateTime edate)
        {
            List<dd_shop_account> recordList = db.dd_shop_account.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                && Convert.ToDateTime(p.createdate) >= sdate && Convert.ToDateTime(p.createdate) <= edate).ToList();
            var rslList = recordList.GroupBy(p => p.type);
            RecordGroupTotleBean rslbean = new RecordGroupTotleBean();
            rslbean.totleCount = recordList.Count();
            rslbean.sumMoney = recordList.Sum(p => p.money).Value;
            rslbean.groupList = new List<RecordGroupBean>();

            foreach(var rsl in rslList)
            {
                RecordGroupBean bean = new RecordGroupBean();
                bean.keyName = BillType2Name(rsl.Key);
                bean.sumMoney = rsl.Sum(p => p.money).Value;
                bean.recList = rsl.ToList();
                rslbean.groupList.Add(bean);
            }

            return rslbean;
        }
        //按支付方式分类统计
        RecordGroupTotleBean QueryRecordGroupByPayType(DateTime sdate, DateTime edate)
        {
            List<dd_shop_account> recordList = db.dd_shop_account.Where(p => p.shopkey == Properties.Settings.Default.shopkey
                && Convert.ToDateTime(p.createdate) >= sdate && Convert.ToDateTime(p.createdate) <= edate).ToList();
            var rslList = recordList.GroupBy(p => p.paytype);
            RecordGroupTotleBean rslbean = new RecordGroupTotleBean();
            rslbean.totleCount = recordList.Count();
            rslbean.sumMoney = recordList.Sum(p => p.money).Value;
            rslbean.groupList = new List<RecordGroupBean>();

            foreach (var rsl in rslList)
            {
                RecordGroupBean bean = new RecordGroupBean();
                bean.keyName = BillPayType2Name(rsl.Key.Value);
                bean.sumMoney = rsl.Sum(p => p.money).Value;
                bean.recList = rsl.ToList();
                rslbean.groupList.Add(bean);
            }

            return rslbean;
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

        public string BillPayType2Name(int paytype)
        {
            string name = "";
            switch (paytype)
            {
                case 1:
                    name = "微信支付";
                    break;
                case 2:
                    name = "支付宝支付";
                    break;
                case 3:
                    name = "会员卡支付";
                    break;
                default:
                    name = "未知";
                    break;
            }
            return name;

        }
    }
}
