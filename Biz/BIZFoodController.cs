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

        public class ItemCrudeInfo
        {
            public int crudeid { get; set; }
            public int guigeid { get; set; }
            public int crudenum { get; set; }
        }

        public class ItemTCInfo
        {
            public int itemid { get; set; }
            public int guigeid { get; set; }
            public int itemnum { get; set; }
        }

        public class itemTuijian
        {
            public int itemkey { get; set; }
            public int guigeid { get; set; }
            public string name { get; set; }
            public string thumb { get; set; }
            public decimal price { get; set; }
            public int num { get; set; }
        }
        //菜品列表
        public List<v_category_items> GetFoodList(int itemCategoryKey,int itemIsDel=-1)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var foodList = db.v_category_items.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (itemCategoryKey != 0)
                {
                    foodList = foodList.Where(p => p.itemcategorykey == itemCategoryKey);
                }
                if (itemIsDel != -1)
                {
                    foodList = foodList.Where(p => p.itemIsDel == itemIsDel);
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
                return db.item_category.Where(p => p.shopkey == Properties.Settings.Default.shopkey&&p.isDel == 0).ToList();
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
                ic.syncFlag = 1;

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
                ic.syncFlag = 1;

                db.item_category.Attach(ic);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(ic);
                stateEntity.SetModifiedProperty("name");
                stateEntity.SetModifiedProperty("itemCategoryCode");
                stateEntity.SetModifiedProperty("orderNo");
                stateEntity.SetModifiedProperty("enable");
                stateEntity.SetModifiedProperty("syncFlag");
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
        public void SaveItem(int itemkey, string name, string code, double discountPrice, double agioprice,
            int itemType, string imgs, int minnum, int isStandard, int isSet, int isPrint,
            sbyte selebyunit, string unit, sbyte ispayagio, sbyte ismust, string introduce,
            List<ItemCrudeInfo> crudeList, List<ItemTCInfo>tcList)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                item bean = db.item.Find(itemkey);
                if (bean == null)
                {
                    bean = new item();
                    bean.name = name;
                    bean.itemCode = code;
                    bean.discountPrice = discountPrice;
                    bean.agioprice = agioprice;
                    bean.isDel = 0;
                    bean.itemcategorykey = itemType;
                    bean.imgs = imgs;
                    bean.minnum = minnum;
                    bean.isStandard = isStandard;
                    bean.isSet = isSet;
                    bean.isprint = isPrint;
                    bean.selebyunit = selebyunit;
                    bean.unit = unit;
                    bean.ispayagio = ispayagio;
                    bean.ismust = ismust;
                    bean.introduce = introduce;
                    bean.syncFlag = 1;

                    db.item.Add(bean);                    
                }
                else
                {
                    bean.name = name;
                    bean.discountPrice = discountPrice;
                    bean.agioprice = agioprice;
                    bean.isDel = 0;
                    bean.itemcategorykey = itemType;
                    bean.imgs = imgs;
                    bean.minnum = minnum;
                    bean.isStandard = isStandard;
                    bean.isSet = isSet;
                    bean.isprint = isPrint;
                    bean.selebyunit = selebyunit;
                    bean.unit = unit;
                    bean.ispayagio = ispayagio;
                    bean.ismust = ismust;
                    bean.introduce = introduce;
                    bean.syncFlag = 1;

                    db.item.Attach(bean);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(bean);
                    stateEntity.SetModifiedProperty("name");
                    stateEntity.SetModifiedProperty("discountPrice");
                    stateEntity.SetModifiedProperty("agioprice");
                    stateEntity.SetModifiedProperty("isDel");
                    stateEntity.SetModifiedProperty("itemcategorykey");
                    stateEntity.SetModifiedProperty("imgs");
                    stateEntity.SetModifiedProperty("minnum");
                    stateEntity.SetModifiedProperty("isStandard");
                    stateEntity.SetModifiedProperty("isprint");
                    stateEntity.SetModifiedProperty("selebyunit");
                    stateEntity.SetModifiedProperty("unit");
                    stateEntity.SetModifiedProperty("ispayagio");
                    stateEntity.SetModifiedProperty("ismust");
                    stateEntity.SetModifiedProperty("introduce");
                    stateEntity.SetModifiedProperty("syncFlag");
                }

                
                List<int> keepList = new List<int>();
                foreach (var crude in crudeList)
                {
                    storage_item_crude ic = db.storage_item_crude.Where(p => p.itemkey == bean.itemkey 
                        && p.crudeid == crude.crudeid && p.guigeid == crude.guigeid).FirstOrDefault();
                    if(ic != null)
                    {
                        ic.num = crude.crudenum;

                        db.storage_item_crude.Attach(ic);
                        var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(ic);
                        stateEntity.SetModifiedProperty("num");

                        keepList.Add(ic.sid);
                    }
                    else
                    {
                        ic = new storage_item_crude();
                        ic.itemkey = bean.itemkey;
                        ic.crudeid = crude.crudeid;
                        ic.num = crude.crudenum;
                        ic.guigeid = crude.guigeid;
                        ic.type = 0;

                        db.storage_item_crude.Add(ic);
                    }                    
                }
                var delList = db.storage_item_crude.Where(p => !keepList.Contains(p.sid)).ToList();
                foreach(var delbean in delList)
                {
                    db.storage_item_crude.Attach(delbean);
                    db.storage_item_crude.Remove(delbean);
                }

                
                if (isSet == 1)
                {
                    var setkeepList = new List<int>();
                    foreach (var tc in tcList)
                    {
                        item_set itemSet = db.item_set.Where(p =>p.tcItemKey == bean.itemkey 
                            && p.itemkey == tc.itemid && p.guigeid == tc.guigeid).FirstOrDefault();
                        if(itemSet != null)
                        {
                            itemSet.itemnum = tc.itemnum;

                            db.item_set.Attach(itemSet);
                            var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(itemSet);
                            stateEntity.SetModifiedProperty("itemnum");

                            setkeepList.Add(itemSet.itemsetkey);
                        }
                        else
                        {
                            itemSet = new item_set();
                            itemSet.tcItemKey = bean.itemkey;
                            itemSet.itemkey = tc.itemid;
                            itemSet.guigeid = tc.guigeid;
                            itemSet.itemnum = tc.itemnum;

                            db.item_set.Add(itemSet);
                        }
                    }

                    var setDelList = db.item_set.Where(p => !setkeepList.Contains(p.itemsetkey)).ToList();
                    foreach (var delbean in delList)
                    {
                        db.storage_item_crude.Attach(delbean);
                        db.storage_item_crude.Remove(delbean);
                    }
                }
                db.SaveChanges();
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
                ic.syncFlag = 1;

                db.item.Attach(ic);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(ic);
                stateEntity.SetModifiedProperty("state");
                stateEntity.SetModifiedProperty("syncFlag");
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
                ic = db.item.Find(itemkey);
                if (ic != null)
                {
                    ic.isDel = 1;
                    db.item.Attach(ic);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(ic);
                    stateEntity.SetModifiedProperty("isDel");
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error("DelItem error. msg=" + e.Message);
                throw;
            }
        }

        public List<dd_tuijian> QueryTuijianList()
        {
            try
            {
                int shopkey = Properties.Settings.Default.shopkey;
                DianDianEntities db = new DianDianEntities();
                var tjList = db.dd_tuijian.Where(p => p.shopkey == shopkey);

                return tjList.ToList();
            }
            catch(Exception e)
            {
                log.Error("QueryTuijianList error. msg=" + e.Message);
                throw;
            }
        }

        public List<dd_tuijian_link> QueryTuijianLinkList(int tuijianid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();

                return db.dd_tuijian_link.Where(p => p.tuijianid == tuijianid).ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryTuijianLinkList error. msg=" + e.Message);
                throw;
            }
        }

        public void SaveTuijian(int tjkey, string items, int afternum, string liyou, List<itemTuijian>tuijianList)
        {
            try
            {
                int shopkey = Properties.Settings.Default.shopkey;
                DianDianEntities db = new DianDianEntities();

                var tjbean = db.dd_tuijian.Find(tjkey);
                if(tjbean != null)
                {
                    tjbean.afternum = afternum;
                    tjbean.items = items;
                    tjbean.liyou = liyou;

                    db.dd_tuijian.Attach(tjbean);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(tjbean);
                    stateEntity.SetModifiedProperty("afternum");
                    stateEntity.SetModifiedProperty("items");
                    stateEntity.SetModifiedProperty("liyou");
                    db.SaveChanges();

                    List<int> keepList = new List<int>();
                    foreach(var tuijian in tuijianList)
                    {
                        var tjLink = db.dd_tuijian_link.Where(p => p.itemkey == tuijian.itemkey && p.guigeid == tuijian.guigeid && p.tuijianid == tjbean.tjid).FirstOrDefault();
                        if(tjLink == null)
                        {
                            tjLink = new dd_tuijian_link();
                            tjLink.guigeid = tuijian.guigeid;
                            tjLink.itemkey = tuijian.itemkey;
                            tjLink.name = tuijian.name;
                            tjLink.num = tuijian.num;
                            tjLink.price = tuijian.price;
                            tjLink.thumb = tuijian.thumb;
                            tjLink.tuijianid = tjbean.tjid;
                            db.dd_tuijian_link.Add(tjLink);
                        }
                        else
                        {
                            tjLink.name = tuijian.name;
                            tjLink.num = tuijian.num;
                            tjLink.price = tuijian.price;
                            tjLink.thumb = tuijian.thumb;
                            db.dd_tuijian_link.Attach(tjLink);
                            var stateEntity2 = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(tjLink);
                            stateEntity.SetModifiedProperty("name");
                            stateEntity.SetModifiedProperty("num");
                            stateEntity.SetModifiedProperty("price");
                            stateEntity.SetModifiedProperty("thumb");
                            db.SaveChanges();
                            keepList.Add(tjLink.sid);
                        }
                    }
                    var delList = db.dd_tuijian_link.Where(p => !keepList.Contains(p.sid)).ToList();
                    foreach(var delid in delList)
                    {
                        var delbean = db.dd_tuijian_link.Find(delid);
                        db.dd_tuijian_link.Attach(delbean);
                        db.dd_tuijian_link.Remove(delbean);
                        db.SaveChanges();
                    }
                    
                }
                else
                {
                    tjbean = new dd_tuijian();
                    tjbean.afternum = afternum;
                    tjbean.createdate = DateTime.Now;
                    tjbean.items = items;
                    tjbean.liyou = liyou;
                    tjbean.peoplenum = 1;
                    tjbean.timesbyday = -1;
                    tjbean.shopkey = shopkey;
                    tjbean.operater = BizLoginController.userid;

                    db.dd_tuijian.Add(tjbean);
                    db.SaveChanges();

                    foreach(var tuijian in tuijianList)
                    {
                        dd_tuijian_link tdl = new dd_tuijian_link();
                        tdl.guigeid = tuijian.guigeid;
                        tdl.itemkey = tuijian.itemkey;
                        tdl.name = tuijian.name;
                        tdl.num = tuijian.num;
                        tdl.price = tuijian.price;
                        tdl.thumb = tuijian.thumb;
                        tdl.tuijianid = tjbean.tjid;
                        db.dd_tuijian_link.Add(tdl);
                    }
                    db.SaveChanges();
                }
                
            }
            catch (Exception e)
            {
                log.Error("SaveTuijian error. msg=" + e.Message);
                throw;
            }
        }

        public void DeleteTuijian(int tjkey)
        {
            DianDianEntities db = new DianDianEntities();

            var delList = db.dd_tuijian_link.Where(p => p.sid == tjkey).ToList();
            foreach (var delid in delList)
            {
                var delbean = db.dd_tuijian_link.Find(delid);
                db.dd_tuijian_link.Attach(delbean);
                db.dd_tuijian_link.Remove(delbean);
                db.SaveChanges();
            }

            dd_tuijian tj = new dd_tuijian();
            tj.tjid = tjkey;

            db.dd_tuijian.Attach(tj);
            db.dd_tuijian.Remove(tj);
            db.SaveChanges();
        }
        public void TaoCanFood(int itemkey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                MyModels.selected_category_items.list.Clear();
                var a = db.item_set.Where(p => p.itemkey == itemkey);
                if (a.Count() > 0)
                {
                    foreach (var b in a)
                    {
                        insert_selected_category_items_list(b.itemnum.Value, b.tcItemKey.Value, b.guigeid.Value);
                    }
                }
                else
                {
                    var c = db.item_set.Where(p => p.tcItemKey == itemkey);
                    foreach(var b in c)
                    {
                        insert_selected_category_items_list(b.itemnum.Value, b.itemkey.Value, b.guigeid.Value);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("TaoCanFoodID error. msg=" + e.Message);
                throw;
            }
        }
        private void insert_selected_category_items_list(int itemnum, int itemkey,int guigeid)
        {
            System.Drawing.Bitmap bm = Properties.Resources._1;
            MyModels.selected_category_items._selected_category_items _Selected_Category_Items = new MyModels.selected_category_items._selected_category_items();
            var c = GetFoodList(0, 0).Where(p => p.itemkey == itemkey).FirstOrDefault();
            _Selected_Category_Items.itemcategorykey = c.itemcategorykey.Value;
            _Selected_Category_Items.itemImgs = bm;
            _Selected_Category_Items.itemKey = c.itemkey.Value;
            _Selected_Category_Items.itemName = c.itemName;
            _Selected_Category_Items.num = itemnum;
            _Selected_Category_Items.sprice = Convert.ToDecimal(c.price);
            _Selected_Category_Items.standardkey = guigeid;
            var d = QueryStandards(c.itemkey.Value).Find(p => p.standardkey == guigeid);
            string standardname = (d==null)?"":d.standardname;
            _Selected_Category_Items.standardname = standardname;
            MyModels.selected_category_items.list.Add(_Selected_Category_Items);
        }
        public int FindMax_itemCategoryCode()
        {
            DianDianEntities db = new DianDianEntities();
            return (Convert.ToInt32(db.item_category.Where(p => p.shopkey == Properties.Settings.Default.shopkey).Max(p => p.itemCategoryCode)) + 1);
        }
        public int FindMax_itemCode()
        {
            DianDianEntities db = new DianDianEntities();
            return (Convert.ToInt32(db.item.Where(p => p.shopkey == Properties.Settings.Default.shopkey).Max(p => p.itemCode)) + 1);
        }
    }
}
