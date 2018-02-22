using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BIZFoodController
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizSPInfoController");
        private DianDianEntities db = new DianDianEntities();
        private string FoodUrl = "http://app.diandiancaidan.com/shop/api.do";

        //菜品列表
        public List<item> GetFoodList()
        {
            return db.item.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
        }
        //菜品分类
        public List<item_category> GetFoodFL()
        {
            return db.item_category.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
        }

        //添加分类
        public void AddItemCategory(int agio)
        {
            item_category ic = new item_category();
        }

        //修改分类
        public void EditItemCategory()
        {

        }

        //分类详细信息
        public item_category GetCategoryDetail(string itemcategorykey)
        {
            item_category rsl = new item_category();
            List<item_category> rslList = db.item_category.Where(p => p.shopkey == Properties.Settings.Default.shopkey && p.itemcategorykey.Equals(itemcategorykey)).ToList();
            if(rslList.Count > 0)
            {
                rsl = rslList.First();
            }
            return rsl;
        }

        //删除分类
        public void DelItemCategory()
        {

        }

        //菜品详细信息
        public item GetItemDetail(string itemkey)
        {
            item rsl = new item();
            List<item> rslList = db.item.Where(p => p.shopkey == Properties.Settings.Default.shopkey && p.itemkey.Equals(itemkey)).ToList();
            if (rslList.Count > 0)
            {
                rsl = rslList.First();
            }
            return rsl;
        }

        //22. 添加更新菜品接口
        public void SaveItem()
        {

        }

        //23. 修改菜品菜品规格接口
        public void EditItemStandards()
        {

        }

        //24. 删除菜品菜品接口
        public void DelItem()
        {

        }
    }
}
