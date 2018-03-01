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
            try
            {
                DianDianEntities db = new DianDianEntities();
                var cardList = db.dd_mem_card.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!name.Equals(""))
                {
                    cardList = cardList.Where(p => p.realname.Equals(name));
                }
                if (!tel.Equals(""))
                {
                    cardList = cardList.Where(p => p.telno.Equals(tel));
                }

                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                if (sdate != null)
                {
                    long timeStamp = (long)(sdate - startTime).TotalSeconds;
                    cardList = cardList.Where(p => p.addtime >= timeStamp);
                }
                if (edate != null)
                {
                    long timeStamp = (long)(edate - startTime).TotalSeconds;
                    cardList = cardList.Where(p => p.addtime <= timeStamp);
                }
                return cardList.ToList();
            }
            catch(Exception e)
            {
                log.Error("QueryMembers error. msg=" + e.Message);
                throw;
            }            
        }

        public void AddMember(string cardNo, string name, string tel, string birth, int sex, decimal money, decimal songmoney)
        {
            try {
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
            }
            catch (Exception e)
            {
                log.Error("AddMember error. msg=" + e.Message);
                throw;
            }
            
        }

        public int Rechange(int cardId, decimal money, decimal songmoney)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_mem_card card = db.dd_mem_card.Find(cardId);
                if (card == null)
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
                
                return 0;
            }
            catch (Exception e)
            {
                log.Error("AddMember error. msg=" + e.Message);
                throw;
            }
        }

        public List<dd_card_userecord> QueryCardUseRecord(int cardId, int type, DateTime sdate, DateTime edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var recList = db.dd_card_userecord
                    .Where(p => p.cardid == cardId && Convert.ToDateTime(p.addtime) >= sdate && Convert.ToDateTime(p.addtime) <= edate);
                if (type == 1 || type == 0)
                {
                    recList = recList.Where(p => p.type == type);
                }
                recList = recList.OrderByDescending(p => p.addtime);

                return recList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryCardUseRecord error. msg=" + e.Message);
                throw;
            }
        }

        public List<dd_chargecar_rule> QueryMemberRules()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var rolesList = db.dd_chargecar_rule.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                return rolesList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryMemberRules error. msg=" + e.Message);
                throw;
            }
        }

        public void AddMemberRule(string title, int money, int songmoney)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_chargecar_rule rule = new dd_chargecar_rule
                {
                    addtime = DateTime.Now.ToString("yyyy-MM-dd HH::ss:mm"),
                    money = money + songmoney,
                    realmoney = money,
                    rname = title,
                    shopkey = Properties.Settings.Default.shopkey,
                    userid = BizLoginController.userid
                };

                db.dd_chargecar_rule.Add(rule);
                db.SaveChanges();
                
            }
            catch(Exception e)
            {
                log.Error("AddMemberRule error. msg=" + e.Message);
                throw;
            }
        }

        public void EditMemberRule(int roleId, string title, int money, int songmoney)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var rule = db.dd_chargecar_rule.Find(roleId);
                rule.money = money + songmoney;
                rule.realmoney = money;
                rule.rname = title;
                rule.userid = BizLoginController.userid;

                db.dd_chargecar_rule.Attach(rule);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(rule);
                stateEntity.SetModifiedProperty("money");
                stateEntity.SetModifiedProperty("realmoney");
                stateEntity.SetModifiedProperty("rname");
                stateEntity.SetModifiedProperty("userid");
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                log.Error("EditMemberRule error. msg=" + e.Message);
                throw;
            }
        }

        public void DeleteMemberRule(int ruleId)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_chargecar_rule rule = new dd_chargecar_rule
                {
                    ruleid = ruleId
                };
                db.dd_chargecar_rule.Attach(rule);
                db.dd_chargecar_rule.Remove(rule);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                log.Error("DeleteMemberRule error. msg=" + e.Message);
                throw;
            }
        }

        //查询在线用户
        public void QueryOnlineUser()
        {

        }

        //群发
        public void MassMessage()
        {
            
        }

        public List<dd_shop_signusers> QuerySignUser(string name, string tel, DateTime sdate, DateTime edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var userList = db.dd_shop_signusers.Where(p => p.shopkey == Properties.Settings.Default.shopkey);

                if (!name.Equals(""))
                {
                    userList = userList.Where(p => p.name.Equals(name));
                }
                if (!tel.Equals(""))
                {
                    userList = userList.Where(p => p.telno.Equals(tel));
                }
                
                if (sdate != null)
                {
                    userList = userList.Where(p => Convert.ToDateTime(p.addtime) >= sdate);
                }
                if (edate != null)
                {
                    userList = userList.Where(p => Convert.ToDateTime(p.addtime) <= edate);
                }
                return userList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QuerySignUser error. msg=" + e.Message);
                throw;
            }
        }

        public void AddSignUser(string name, string tel, int maxusernums, decimal maxprice)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_shop_signusers user = new dd_shop_signusers
                {
                    addtime = DateTime.Now.ToString("yyyy-MM-dd HH::ss:mm"),
                    maxprice = maxprice,
                    maxusenums = maxusernums,
                    name = name,
                    shopkey = Properties.Settings.Default.shopkey,
                    signmoney = 0,
                    signnums = 0,
                    state = 1,
                    telno = tel
                };


                db.dd_shop_signusers.Add(user);
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                log.Error("AddSignUser error. msg=" + e.Message);
                throw;
            }
        }

        public void EditSignUser(int signuserId, string name, string tel, int maxusernums, decimal maxprice)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var user = db.dd_shop_signusers.Find(signuserId);
                user.maxprice = maxprice;
                user.maxusenums = maxusernums;
                user.name = name;
                user.telno = tel;

                db.dd_shop_signusers.Attach(user);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(user);
                stateEntity.SetModifiedProperty("maxprice");
                stateEntity.SetModifiedProperty("maxusenums");
                stateEntity.SetModifiedProperty("name");
                stateEntity.SetModifiedProperty("telno");
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                log.Error("EditSignUser error. msg=" + e.Message);
                throw;
            }
        }

        public void FreezeSignUser(int signuserId)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var user = db.dd_shop_signusers.Find(signuserId);
                user.state = 0;

                db.dd_shop_signusers.Attach(user);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(user);
                stateEntity.SetModifiedProperty("state");
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                log.Error("FreezeSignUser error. msg=" + e.Message);
                throw;
            }
        }

        public void ClearBillSignUser(int signuserId)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var user = db.dd_shop_signusers.Find(signuserId);
                user.signmoney = 0;
                user.signnums = 0;

                db.dd_shop_signusers.Attach(user);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(user);
                stateEntity.SetModifiedProperty("signmoney");
                stateEntity.SetModifiedProperty("signnums");
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                log.Error("ClearBillSignUser error. msg=" + e.Message);
                throw;
            }
        }
    }
}
