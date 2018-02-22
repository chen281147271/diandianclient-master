using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizMemberCard
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizBillController");

        public List<dd_mem_card> QueryMembers(string name, string tel, DateTime sdate, DateTime edate)
        {
            DianDianEntities db = new DianDianEntities();

            List<dd_mem_card> rslList = db.dd_mem_card.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
            if (!name.Equals(""))
            {
                rslList = rslList.Where(p => p.realname.Equals(name)).ToList();
            }
            if (!tel.Equals(""))
            {
                rslList = rslList.Where(p => p.telno.Equals(tel)).ToList();
            }

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            if (sdate != null)
            {
                long timeStamp = (long)(sdate - startTime).TotalSeconds;
                rslList = rslList.Where(p => p.addtime >= timeStamp).ToList();
            }
            if(edate != null)
            {
                long timeStamp = (long)(edate - startTime).TotalSeconds;
                rslList = rslList.Where(p => p.addtime <= timeStamp).ToList();
            }
            db.Dispose();
            return rslList;
        }

        public int AddMember(string cardNo, string name, string tel, string birth, int sex, decimal money, decimal songmoney)
        {
            if (cardNo.Equals(""))
            {
                cardNo = "" + Properties.Settings.Default.shopkey + DateTime.Now.ToString("yyyyMMddHHmmssffff");
            }
            dd_mem_card newMember = new dd_mem_card();
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            newMember.addtime = (int)(DateTime.Now - startTime).TotalSeconds;
            newMember.birthday = birth;
            newMember.cardno = cardNo;
            newMember.expirydate = (int)(DateTime.Now.AddYears(1) - startTime).TotalSeconds;
            newMember.isdian = true;
            newMember.isvalid = 0;
            newMember.jifen = 0;
            newMember.money = money;
            newMember.pintaimoney = 0;
            newMember.realname = name;
            newMember.sex = sex;
            newMember.shopkey = Properties.Settings.Default.shopkey;
            newMember.songmoney = songmoney;
            newMember.telno = tel;
            newMember.yiling = 0;

            DianDianEntities db = new DianDianEntities();
            db.dd_mem_card.Add(newMember);
            db.SaveChanges();
            db.Dispose();

            return 0;
        }

        public int Rechange(int cardId, decimal money, decimal songmoney)
        {
            DianDianEntities db = new DianDianEntities();
            dd_mem_card card = db.dd_mem_card.Find(cardId);
            if(card == null)
            {
                return 1; //查找不到
            }
            card.money = card.money + money;
            card.songmoney = card.songmoney + songmoney;

            db.dd_mem_card.Attach(card);
            var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(card);
            stateEntity.SetModifiedProperty("money");
            stateEntity.SetModifiedProperty("songmoney");
            db.SaveChanges();

            db.Dispose();

            return 0;
        }

        public List<dd_card_userecord> QueryCardUseRecord(int cardId, int type, DateTime sdate, DateTime edate)
        {
            DianDianEntities db = new DianDianEntities();
            var recList = db.dd_card_userecord
                .Where(p => p.cardid == cardId && Convert.ToDateTime(p.addtime) >= sdate && Convert.ToDateTime(p.addtime) <= edate);
            if(type == 1 || type == 0)
            {
                recList = recList.Where(p => p.type == type);
            }
            recList = recList.OrderByDescending(p => p.addtime);

            return recList.ToList();
        }

        public void QueryMemberRoles()
        {

        }
    }
}
