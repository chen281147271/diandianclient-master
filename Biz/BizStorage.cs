using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizStorage
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizStorage");

        public List<storage_crude> QueryCrude(string crudename, int genreid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var crudeList = db.storage_crude.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (crudename.Equals(crudename))
                {
                    crudeList = crudeList.Where(p => p.crudename.Contains(crudename));
                }
                if(genreid != 0)
                {
                    crudeList = crudeList.Where(p => p.genreid == genreid);
                }
                return crudeList.ToList();
            }
            catch (Exception e)
            {

                log.Error("QueryCrude error. msg=" + e.Message);
                throw;
            }
        }

        public void SaveCrude(int crudeid, int genreid, string crudename, string unit)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                storage_crude crude = db.storage_crude.Find(crudeid);
                if (crude == null)
                {
                    crude = new storage_crude();
                    crude.createdate = DateTime.Now;
                    crude.genreid = genreid;
                    crude.crudename = crudename;
                    crude.unit = unit;
                    crude.shopkey = Properties.Settings.Default.shopkey;
                    crude.state = 0;

                    db.storage_crude.Add(crude);
                }
                else
                {
                    crude.genreid = genreid;
                    crude.crudename = crudename;
                    crude.unit = unit;

                    db.storage_crude.Attach(crude);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(crude);
                    stateEntity.SetModifiedProperty("genreid");
                    stateEntity.SetModifiedProperty("crudename");
                    stateEntity.SetModifiedProperty("unit");
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("SaveCrude error. msg=" + e.Message);
                throw;
            }
        }

        public void DelCrude(int crudeid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                storage_crude crude = db.storage_crude.Find(crudeid);
                crude.state = 1;

                db.storage_crude.Attach(crude);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(crude);
                stateEntity.SetModifiedProperty("state");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DelCrude error. msg=" + e.Message);
                throw;
            }
        }

        public List<storage_genre> QueryGenre()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                return db.storage_genre.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryGenre error. msg=" + e.Message);
                throw;
            }
        }

        public void SaveGenre(int genreId, string genrename)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                storage_genre genre = db.storage_genre.Find(genreId);
                if (genre == null)
                {
                    genre = new storage_genre();
                    genre.createdate = DateTime.Now;
                    genre.genrename = genrename;
                    genre.shopkey = Properties.Settings.Default.shopkey;
                    genre.state = 1;
                    genre.orderno = 999;

                    db.storage_genre.Add(genre);
                }
                else
                {
                    genre.genrename = genrename;

                    db.storage_genre.Attach(genre);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(genre);
                    stateEntity.SetModifiedProperty("genrename");
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("SaveGenre error. msg=" + e.Message);
                throw;
            }
        }

        public void DelGenre(int genreid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                storage_genre genre = new storage_genre
                {
                    genreid = genreid
                };
                db.storage_genre.Attach(genre);
                db.storage_genre.Remove(genre);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DelGenre error. msg=" + e.Message);
                throw;
            }
        }

        public List<v_stock_crude> QueryStock(string itemname, string categoryname, string crudename, int genreid, DateTime sdate, DateTime edate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var stockList = db.v_stock_crude.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!crudename.Equals(""))
                {
                    stockList = stockList.Where(p => p.crudename.Contains(crudename));
                }
                if(genreid != 0)
                {
                    stockList = stockList.Where(p => p.genreid == genreid);
                }
                if(sdate != null)
                {
                    stockList = stockList.Where(p => p.validate >= sdate);
                }
                if(sdate != null)
                {
                    stockList = stockList.Where(p => p.validate <= edate);
                }

                return stockList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryStock error. msg=" + e.Message);
                throw;
            }
        }

        public void StockEstimateClear()
        {
            try
            {
                /*
                DianDianEntities db = new DianDianEntities();
                storage_genre genre = db.storage_genre.Find(genreId);

                genre.genrename = genrename;

                db.storage_genre.Attach(genre);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(genre);
                stateEntity.SetModifiedProperty("genrename");
                */
            }
            catch (Exception e)
            {
                log.Error("StockEstimateClear error. msg=" + e.Message);
                throw;
            }
        }

        public void StockModifyValidate(int crudeid, DateTime validate, DateTime changedate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var stock = db.storage_stock.Where(p => p.crudeid == crudeid && p.validate == validate).FirstOrDefault();
                if(stock != null)
                {
                    stock.validate = changedate;
                    if(stock.backdate != null)
                    {
                        stock.backdate = changedate;
                    }

                    db.storage_stock.Attach(stock);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(stock);
                    stateEntity.SetModifiedProperty("genrename");
                }
                else
                {
                    log.Error("StockModifyValidate error, msg = can not find record");
                }
                
            }
            catch (Exception e)
            {
                log.Error("StockModifyValidate error. msg=" + e.Message);
                throw;
            }
        }

        public void QueryDepotIn(string itemname, DateTime validate, DateTime sdate, DateTime edate, string dutyperson, string deliveryman ,string categoryname)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var depotInList = db.v_depotin_crude.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!itemname.Equals(""))
                {

                }
            }
            catch (Exception e)
            {
                log.Error("QueryDepotIn error. msg=" + e.Message);
                throw;
            }
        }

        public void QueryDepotDetail(int depotinid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var detailList = db.v_depotin_crude.Where(p => p.depotinid == depotinid);
            }
            catch (Exception e)
            {
                log.Error("QueryDepotDetail error. msg=" + e.Message);
                throw;
            }
        }

        public void QueryDepotOut()
        {
            try
            {

            }
            catch (Exception e)
            {
                log.Error("QueryDepotOut error. msg=" + e.Message);
                throw;
            }
        }

        public void AddDepotIn()
        {
            try
            {

            }
            catch (Exception e)
            {
                log.Error("AddDepotIn error. msg=" + e.Message);
                throw;
            }
        }

        public void QueryLossOrSpillInfo()
        {
            try
            {

            }
            catch (Exception e)
            {
                log.Error("QueryLossOrSpillInfo error. msg=" + e.Message);
                throw;
            }
        }

        public void AddLossOrSpillInfo()
        {
            try
            {

            }
            catch (Exception e)
            {
                log.Error("AddLossOrSpillInfo error. msg=" + e.Message);
                throw;
            }
        }
    }
}
