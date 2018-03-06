using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizPromotionActivities
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizPromotionActivities");

        public List<dd_coupons> QueryActivities()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var activityList = db.dd_coupons.Where(p => p.shopid == Properties.Settings.Default.shopkey);
                
                return activityList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryActivities error. msg=" + e.Message);
                throw;
            }
        }

        public void AddActivity(string cname, DateTime sdate, DateTime edate, string istogether, string memtogether, int isonlineok, int ifmoney, int okjian, string extendway, decimal minmoney)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_coupons activity = new dd_coupons
                {
                    addtime = DateTime.Now.ToString("yyyy-MM-dd HH::ss:mm"),
                    cname = cname,
                    sdate = sdate.ToString("yyyy-MM-dd"),
                    edate = edate.ToString("yyyy-MM-dd"),
                    shopid = Properties.Settings.Default.shopkey,
                    istogether = istogether,
                    memtogether = memtogether,
                    isonlineok = isonlineok,
                    ifmoney = ifmoney,
                    okjian = okjian,
                    extendway = extendway,
                    minmoney = minmoney
                };


                db.dd_coupons.Add(activity);
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                log.Error("AddActivity error. msg=" + e.Message);
                throw;
            }
        }

        public void EditActivity(int activityId,string cname, DateTime sdate, DateTime edate, string istogether, string memtogether, int isonlineok, int ifmoney, int okjian, string extendway, decimal minmoney)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var activity = db.dd_coupons.Find(activityId);
                activity.cname = cname;
                activity.sdate = sdate.ToString("yyyy-MM-dd");
                activity.edate = edate.ToString("yyyy-MM-dd");
                activity.shopid = Properties.Settings.Default.shopkey;
                activity.istogether = istogether;
                activity.memtogether = memtogether;
                activity.isonlineok = isonlineok;
                activity.ifmoney = ifmoney;
                activity.okjian = okjian;
                activity.extendway = extendway;
                activity.minmoney = minmoney;

                db.dd_coupons.Attach(activity);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(activity);
                stateEntity.SetModifiedProperty("cname");
                stateEntity.SetModifiedProperty("sdate");
                stateEntity.SetModifiedProperty("edate");
                stateEntity.SetModifiedProperty("shopid");
                stateEntity.SetModifiedProperty("istogether");
                stateEntity.SetModifiedProperty("memtogether");
                stateEntity.SetModifiedProperty("isonlineok");
                stateEntity.SetModifiedProperty("ifmoney");
                stateEntity.SetModifiedProperty("okjian");
                stateEntity.SetModifiedProperty("extendway");
                stateEntity.SetModifiedProperty("minmoney");
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                log.Error("EditActivity error. msg=" + e.Message);
                throw;
            }
        }

        public void DisableActivity(int activityId)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var activity = db.dd_coupons.Find(activityId);
                //activity.state = false;

                db.dd_coupons.Attach(activity);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(activity);
                //stateEntity.SetModifiedProperty("state");
                db.SaveChanges();
                
            }
            catch (Exception e)
            {
                log.Error("FreezeSignUser error. msg=" + e.Message);
                throw;
            }
        }
    }
}
