using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BIZFoodController
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizSPInfoController");
        //private DianDianEntities db = new DianDianEntities();
        private string FoodUrl = "http://app.diandiancaidan.com/shop/api.do";

        //菜品列表
        public List<v_category_items> GetFoodList(int itemCategoryKey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var foodList = db.v_category_items.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (itemCategoryKey != 0)
                {
                    foodList = foodList.Where(p => p.itemcategorykey == itemCategoryKey);
                }
                return foodList.ToList();
            }
            catch (Exception e)
            {
                log.Error("GetFoodList error. msg=" + e.Message);
                throw;
            }

        }
        //菜品分类
        public List<item_category> GetFoodFL()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                return db.item_category.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
            }
            catch (Exception e)
            {
                log.Error("GetFoodFL error. msg=" + e.Message);
                throw;
            }
        }

        //添加分类
        public void AddItemCategory(string categoryName, string code, int orderNo, int enable)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                item_category ic = new item_category();
                ic.name = categoryName;
                ic.itemCategoryCode = code;
                ic.orderNo = orderNo;
                ic.enable = enable;

                db.item_category.Add(ic);
                db.SaveChanges();

            } catch (Exception e)
            {
                log.Error("AddItemCategory error. msg=" + e.Message);
                throw;
            }
        }

        //修改分类
        public void EditItemCategory(int itemCategoryKey, string categoryName, string code, int orderNo, int enable)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                item_category ic = db.item_category.Find(itemCategoryKey);
                ic.name = categoryName;
                ic.itemCategoryCode = code;
                ic.orderNo = orderNo;
                ic.enable = enable;

                db.item_category.Attach(ic);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(ic);
                stateEntity.SetModifiedProperty("name");
                stateEntity.SetModifiedProperty("itemCategoryCode");
                stateEntity.SetModifiedProperty("orderNo");
                stateEntity.SetModifiedProperty("enable");
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("EditItemCategory error. msg=" + e.Message);
                throw;
            }
        }

        //分类详细信息
        public item_category GetCategoryDetail(int itemcategorykey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                item_category rsl = new item_category();
                List<item_category> rslList = db.item_category.Where(p => p.shopkey == Properties.Settings.Default.shopkey && p.itemcategorykey == itemcategorykey).ToList();
                if (rslList.Count > 0)
                {
                    rsl = rslList.First();
                }
                return rsl;
            }
            catch (Exception e)
            {
                log.Error("GetCategoryDetail error. msg=" + e.Message);
                throw;
            }
        }

        //删除分类
        public void DelItemCategory(int itemCategoryKey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                item_category ic = new item_category();
                ic.itemcategorykey = itemCategoryKey;

                db.item_category.Attach(ic);
                db.item_category.Remove(ic);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DelItemCategory error. msg=" + e.Message);
                throw;
            }
        }

        //菜品详细信息
        public item GetItemDetail(string itemkey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                item rsl = new item();
                List<item> rslList = db.item.Where(p => p.shopkey == Properties.Settings.Default.shopkey && p.itemkey.Equals(itemkey)).ToList();
                if (rslList.Count > 0)
                {
                    rsl = rslList.First();
                }
                return rsl;
            }
            catch (Exception e)
            {
                log.Error("GetItemDetail error. msg=" + e.Message);
                throw;
            }
        }

        //22. 添加更新菜品接口
        public void SaveItem(int itemkey, string name, string code, decimal discountPrice, decimal agioprice,
            int itemType, string imgs, int minnum, int isStandard, int isSet, int isPrint,
            int selebyunit, string unit, int ispayagio, int ismust, string introduce)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                item bean = db.item.Find(itemkey);
                if (bean == null)
                {
                    bean = new item();
                }

            } catch (Exception e)
            {
                log.Error("SaveItem error. msg=" + e.Message);
                throw;
            }
        }

        public List<item_standard> QueryStandards(int itemkey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                return db.item_standard.Where(p => p.itemKey == itemkey).ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryStandards error. msg=" + e.Message);
                throw;
            }
        }
        //23. 修改菜品菜品规格接口
        public void EditItemStandards( int standardkey, int itemkey, string standardName, decimal price, sbyte state)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                if(standardkey != 0)
                {
                    item_standard ic = db.item_standard.Find(standardkey);
                    ic.itemKey = itemkey;
                    ic.standardname = standardName;
                    ic.sprice = price;
                    ic.state = state;

                    db.item_standard.Attach(ic);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(ic);
                    stateEntity.SetModifiedProperty("itemKey");
                    stateEntity.SetModifiedProperty("standardname");
                    stateEntity.SetModifiedProperty("sprice");
                    stateEntity.SetModifiedProperty("state");
                    db.SaveChanges();
                }
                else
                {
                    item_standard ic = new item_standard();
                    ic.itemKey = itemkey;
                    ic.standardname = standardName;
                    ic.sprice = price;
                    ic.state = state;

                    db.item_standard.Add(ic);
                    db.SaveChanges();
                }                
            }
            catch (Exception e)
            {
                log.Error("EditItemStandards error. msg=" + e.Message);
                throw;
            }
        }
        public void DeleteItemStandards(int standardkey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                item_standard ic = new item_standard();
                ic.standardkey = standardkey;

                db.item_standard.Attach(ic);
                db.item_standard.Remove(ic);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DeleteItemStandards error. msg=" + e.Message);
                throw;
            }
        }

        public List<storage_crude> QueryCrude()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                return db.storage_crude.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryCrude error. msg=" + e.Message);
                throw;
            }
        }
        public List<v_item_crude> QueryItemCrude(int itemkey, int guigeid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                return db.v_item_crude.Where(p => p.itemkey == itemkey && p.guigeid == guigeid).ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryItemCrude error. msg=" + e.Message);
                throw;
            }
        }
        public void SaveItemCrude(int sid , int itemkey, int guigeid, int crudeid, int num, sbyte type)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                if (sid != 0)
                {
                    storage_item_crude ic = db.storage_item_crude.Find(sid);
                    ic.num = num;
                    ic.type = type;

                    db.storage_item_crude.Attach(ic);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(ic);
                    stateEntity.SetModifiedProperty("num");
                    stateEntity.SetModifiedProperty("type");
                    db.SaveChanges();
                }
                else
                {
                    storage_item_crude ic = new storage_item_crude();
                    ic.itemkey = itemkey;
                    ic.guigeid = guigeid;
                    ic.crudeid = crudeid;
                    ic.num = num;
                    ic.type = type;

                    db.storage_item_crude.Add(ic);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error("SaveItemCrude error. msg=" + e.Message);
                throw;
            }
        }
        public void DeleteItemCrude(int sid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                storage_item_crude ic = new storage_item_crude();
                ic.sid = sid;

                db.storage_item_crude.Attach(ic);
                db.storage_item_crude.Remove(ic);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DeleteItemStandards error. msg=" + e.Message);
                throw;
            }
        }

        public void ChangeState(int itemKey, int state){
            try
            {
                DianDianEntities db = new DianDianEntities();
                item ic = db.item.Find(itemKey);
                ic.state = state;

                db.item.Attach(ic);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(ic);
                stateEntity.SetModifiedProperty("state");
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("ChangeState error. msg=" + e.Message);
                throw;
            }
        }

        //24. 删除菜品菜品接口
        public void DelItem(int itemkey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                item ic = new item();
                ic.itemkey = itemkey;

                db.item.Attach(ic);
                db.item.Remove(ic);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DelItem error. msg=" + e.Message);
                throw;
            }
        }

        public void QueryTuijianList()
        {

        }

        public void SaveTuijian()
        {

        }

        public void DeleteTuijian()
        {

        }
    }
}
