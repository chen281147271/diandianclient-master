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

        public class QueryDepotOutResult
        {
            public List<v_depotout_crude> depotoutList { get; set; }
            public decimal salemoney { get; set; }
            public decimal buymoney { get; set; }
        }
        //查询原料
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
                if (genreid != 0)
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

        //保存原料
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
        //删除原料
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
        //原料分类
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
        //保存原料分类
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
        //删除原料分类
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

        //库存
        public List<v_stock_crude> QueryStock(string itemname, string categoryname, string crudename, int genreid, DateTime sdate, DateTime edate)
        {
            try
            {
                bool includeItem = false;
                DianDianEntities db = new DianDianEntities();
                var itemList = db.v_category_items.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!itemname.Equals(""))
                {
                    itemList = itemList.Where(p => p.itemName.Contains(itemname));
                    includeItem = true;
                }
                if (!categoryname.Equals(""))
                {
                    itemList = itemList.Where(p => p.categoryName.Contains(categoryname));
                    includeItem = true;
                }
                var itemcrudeList = itemList.Select(p => p.itemkey).Distinct().ToList();
                var includeCrudeList = db.v_item_crude.Where(p => itemcrudeList.Contains(p.itemkey)).Select(p => p.crudeid).ToList();

                var stockList = db.v_stock_crude.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!crudename.Equals(""))
                {
                    stockList = stockList.Where(p => p.crudename.Contains(crudename));
                }
                if (genreid != 0)
                {
                    stockList = stockList.Where(p => p.genreid == genreid);
                }
                if (sdate != null)
                {
                    stockList = stockList.Where(p => p.validate >= sdate);
                }
                if (sdate != null)
                {
                    stockList = stockList.Where(p => p.validate <= edate);
                }
                if (includeItem)
                {
                    stockList = stockList.Where(p => includeCrudeList.Contains(p.crudeid));
                }
                return stockList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryStock error. msg=" + e.Message);
                throw;
            }
        }
        //估清
        public void StockEstimateClear()
        {
            try
            {
                //DateTime.Now.Date
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
        //修改有效期
        public void StockModifyValidate(int crudeid, DateTime validate, DateTime changedate)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var stock = db.storage_stock.Where(p => p.crudeid == crudeid && p.validate == validate).FirstOrDefault();
                if (stock != null)
                {
                    stock.validate = changedate;
                    if (stock.backdate != null)
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
        //入库
        public List<v_depotin_crude> QueryDepotIn(string itemname, DateTime validate, DateTime sdate, DateTime edate, string dutyperson, string deliveryman, string categoryname)
        {
            try
            {
                bool includeItem = false;
                DianDianEntities db = new DianDianEntities();
                var itemList = db.v_category_items.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!itemname.Equals(""))
                {
                    itemList = itemList.Where(p => p.itemName.Contains(itemname));
                    includeItem = true;
                }
                if (!categoryname.Equals(""))
                {
                    itemList = itemList.Where(p => p.categoryName.Contains(categoryname));
                    includeItem = true;
                }
                var itemcrudeList = itemList.Select(p => p.itemkey).Distinct().ToList();
                var includeCrudeList = db.v_item_crude.Where(p => itemcrudeList.Contains(p.itemkey)).Select(p => p.crudeid).ToList();


                var depotInList = db.v_depotin_crude.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (validate != null)
                {
                    depotInList = depotInList.Where(p => p.validity <= validate);
                }
                if (sdate != null)
                {
                    depotInList = depotInList.Where(p => p.productiondate >= sdate);
                }
                if (edate != null)
                {
                    depotInList = depotInList.Where(p => p.productiondate <= edate);
                }
                if (!dutyperson.Equals(""))
                {
                    depotInList = depotInList.Where(p => p.dutyperson.Contains(dutyperson));
                }
                if (!deliveryman.Equals(""))
                {
                    depotInList = depotInList.Where(p => p.deliveryman.Contains(deliveryman));
                }
                if (includeItem)
                {
                    depotInList = depotInList.Where(p => includeCrudeList.Contains(p.crudeid));
                }

                return depotInList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryDepotIn error. msg=" + e.Message);
                throw;
            }
        }
        //入库明细
        public List<v_depotin_crude> QueryDepotDetail(int depotinid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var detailList = db.v_depotin_crude.Where(p => p.depotinid == depotinid);

                return detailList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryDepotDetail error. msg=" + e.Message);
                throw;
            }
        }
        //出库/总表
        public QueryDepotOutResult QueryDepotOut(string itemname, string categoryname, string crudename, int genreid, DateTime sdate, DateTime edate)
        {
            try
            {
                bool includeItem = false;
                QueryDepotOutResult resultbean = new QueryDepotOutResult();
                resultbean.salemoney = 0;
                DianDianEntities db = new DianDianEntities();
                var itemList = db.v_category_items.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!itemname.Equals(""))
                {
                    itemList = itemList.Where(p => p.itemName.Contains(itemname));
                    includeItem = true;
                }
                if (!categoryname.Equals(""))
                {
                    itemList = itemList.Where(p => p.categoryName.Contains(categoryname));
                    includeItem = true;
                }
                var itemcrudeList = itemList.Select(p => p.itemkey).Distinct().ToList();
                var includeCrudeList = db.v_item_crude.Where(p => itemcrudeList.Contains(p.itemkey)).Select(p => p.crudeid).ToList();

                var stockList = db.v_depotout_crude.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!crudename.Equals(""))
                {
                    stockList = stockList.Where(p => p.crudename.Contains(crudename));
                }
                if (genreid != 0)
                {
                    stockList = stockList.Where(p => p.genreid == genreid);
                }
                if (sdate != null)
                {
                    stockList = stockList.Where(p => p.createdate >= sdate);
                }
                if (sdate != null)
                {
                    stockList = stockList.Where(p => p.createdate <= edate);
                }
                if (includeItem)
                {
                    stockList = stockList.Where(p => includeCrudeList.Contains(p.crudeid));
                }
                resultbean.depotoutList = stockList.ToList();
                var rsl = db.GetSumSaleMoney(itemname, categoryname, sdate, edate).FirstOrDefault();
                if(rsl != null)
                {
                    resultbean.salemoney = rsl.Value;
                }
                var rsl2 = db.storage_depotin.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (sdate != null)
                {
                    rsl2 = rsl2.Where(p => p.createdate >= sdate);
                }
                if (sdate != null)
                {
                    rsl2 = rsl2.Where(p => p.createdate <= edate);
                }
                if(rsl2 != null)
                {
                    resultbean.buymoney = rsl2.Sum(p => p.cost).Value;
                }               
                
                return resultbean;
            }
            catch (Exception e)
            {
                log.Error("QueryDepotOut error. msg=" + e.Message);
                throw;
            }
        }
        
        //添加入库
        public void AddDepotIn(int depotId, DateTime createdate, decimal cost, string dutyperson, string deliveryman, string deliveryphone, string driver, string platenum)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();

                var depotin = db.storage_depotin.Find(depotId);
                if(depotin == null)
                {
                    depotin = new storage_depotin();
                    depotin.createdate = createdate;
                    depotin.cost = cost;
                    depotin.dutyperson = dutyperson;
                    depotin.deliveryman = deliveryman;
                    depotin.deliveryphone = deliveryphone;
                    depotin.driver = driver;
                    depotin.platenum = platenum;

                    db.storage_depotin.Add(depotin);
                }
                else
                {
                    depotin.createdate = createdate;
                    depotin.cost = cost;
                    depotin.dutyperson = dutyperson;
                    depotin.deliveryman = deliveryman;
                    depotin.deliveryphone = deliveryphone;
                    depotin.driver = driver;
                    depotin.platenum = platenum;

                    db.storage_depotin.Attach(depotin);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(depotin);
                    stateEntity.SetModifiedProperty("createdate");
                    stateEntity.SetModifiedProperty("cost");
                    stateEntity.SetModifiedProperty("dutyperson");
                    stateEntity.SetModifiedProperty("deliveryman");
                    stateEntity.SetModifiedProperty("deliveryphone");
                    stateEntity.SetModifiedProperty("driver");
                    stateEntity.SetModifiedProperty("platenum");
                }
                
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("AddDepotIn error. msg=" + e.Message);
                throw;
            }
        }
        //添加入库详情
        public void AddDepotInInfo(int genreid, int crudeid, decimal cost, int num, DateTime? validity, DateTime? productiondate,
            DateTime? backdate, String maker, string remarks, string supplier )
        {
            try
            {
                DianDianEntities db = new DianDianEntities();

                storage_depotin_info deportininfo = new storage_depotin_info();
                deportininfo.crudeid = crudeid;
                deportininfo.cost = cost;
                deportininfo.num = num;
                deportininfo.validity = validity;
                deportininfo.productiondate = productiondate;
                deportininfo.backdate = backdate;
                deportininfo.maker = maker;
                deportininfo.remarks = remarks;
                deportininfo.supplier = supplier;

                db.storage_depotin_info.Add(deportininfo);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("AddDepotInInfo error. msg=" + e.Message);
                throw;
            }
        }
        //损益
        public List<v_storagelossorspill_crude> QueryLossOrSpillInfo(string itemname, string categoryname, string crudename, int genreid, DateTime sdate, DateTime edate)
        {
            try
            {
                bool includeItem = false;
                DianDianEntities db = new DianDianEntities();
                var itemList = db.v_category_items.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!itemname.Equals(""))
                {
                    itemList = itemList.Where(p => p.itemName.Contains(itemname));
                    includeItem = true;
                }
                if (!categoryname.Equals(""))
                {
                    itemList = itemList.Where(p => p.categoryName.Contains(categoryname));
                    includeItem = true;
                }
                var itemcrudeList = itemList.Select(p => p.itemkey).Distinct().ToList();
                var includeCrudeList = db.v_item_crude.Where(p => itemcrudeList.Contains(p.itemkey)).Select(p => p.crudeid).ToList();

                var stockList = db.v_storagelossorspill_crude.Where(p => p.depotid == Properties.Settings.Default.depotid);
                if (!crudename.Equals(""))
                {
                    stockList = stockList.Where(p => p.crudename.Contains(crudename));
                }
                if (genreid != 0)
                {
                    stockList = stockList.Where(p => p.genreid == genreid);
                }
                if (sdate != null)
                {
                    stockList = stockList.Where(p => p.createdate >= sdate);
                }
                if (sdate != null)
                {
                    stockList = stockList.Where(p => p.createdate <= edate);
                }
                if (includeItem)
                {
                    stockList = stockList.Where(p => includeCrudeList.Contains(p.crudeid));
                }
                return stockList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryLossOrSpillInfo error. msg=" + e.Message);
                throw;
            }
        }
        //添加损益
        public void AddLossOrSpillInfo(int crudeid, int num, sbyte type, string reason)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                storage_lossorspill los = new storage_lossorspill();
                los.createdate = DateTime.Now;
                los.crudeid = crudeid;
                los.depotid = Properties.Settings.Default.depotid;
                los.num = num;
                los.person = BizLoginController.username;
                los.reason = reason;
                los.shopkey = Properties.Settings.Default.shopkey;
                los.type = type;

                db.storage_lossorspill.Add(los);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("AddLossOrSpillInfo error. msg=" + e.Message);
                throw;
            }
        }
    }
}
