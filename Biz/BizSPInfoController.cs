using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizSPInfoController
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizSPInfoController");
        private DianDianEntities db = new DianDianEntities();

        private String WindowsUrl = "http://app.diandiancaidan.com/back/windows.do";

        //14. 档口列表接口
        public List<dd_shop_windows> StallsList()
        {
            try
            {
                //本地业务
                return db.dd_shop_windows.Where(p => p.shopid == Properties.Settings.Default.shopkey).ToList();
            }
            catch (Exception e)
            {
                log.Error("StallsList error. msg=" + e.Message);
                throw;
            }
        }

        //15. 添加修改档口
        public void UpdateStall(int shopid, int id, string name, string desc, string printname, int printnum, bool isdefault, int status, bool isprintexcep, bool isyicaiyidan)
        {
            try
            {
                //本地业务
                dd_shop_windows window = db.dd_shop_windows.FirstOrDefault(p => p.windowid == id);
                if(window == null)
                {
                    window = new dd_shop_windows();
                    window.shopid = shopid;
                    window.windowname = name;
                    window.windowdesc = desc;
                    window.printname = printname;
                    window.printnum = printnum;
                    window.isdefault = isdefault;
                    window.status = status;
                    window.isyicaiyidan = isyicaiyidan;
                    window.isprintexcep = isprintexcep;
                    db.dd_shop_windows.Add(window);
                }
                else
                {
                    window.shopid = shopid;
                    window.windowname = name;
                    window.windowdesc = desc;
                    window.printname = printname;
                    window.printnum = printnum;
                    window.isdefault = isdefault;
                    window.status = status;
                    window.isyicaiyidan = isyicaiyidan;
                    window.isprintexcep = isprintexcep;

                    db.dd_shop_windows.Attach(window);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(window);
                    stateEntity.SetModifiedProperty("shopid");
                    stateEntity.SetModifiedProperty("name");
                    stateEntity.SetModifiedProperty("desc");
                    stateEntity.SetModifiedProperty("printname");
                    stateEntity.SetModifiedProperty("printnum");
                    stateEntity.SetModifiedProperty("isdefault");
                    stateEntity.SetModifiedProperty("status");
                    stateEntity.SetModifiedProperty("isyicaiyidan");
                    stateEntity.SetModifiedProperty("isprintexcep");

                }                
                
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("UpdateStall error. msg=" + e.Message);
                throw;
            }
        }

        //16. 删除档口接口
        public void Delwindow(int id)
        {
            try
            {
                //本地业务

                //异步通知服务器
                remote_request rr = new remote_request();
                rr.create_time = DateTime.Now;
                rr.deal_flag = 0;
                rr.request_type = "GET";
                rr.request_url = WindowsUrl;
                rr.request_param = "m=update&id=" + id;

                db.remote_request.Add(rr);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("Delwindow error. msg=" + e.Message);
                throw;
            }
        }


        //25. 获取商户餐桌列表接口
        public List<table_pos> GetTableList()
        {
            try
            {
                return db.table_pos.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
            }
            catch (Exception e)
            {
                log.Error("GetTableList error. msg=" + e.Message);
                throw;
            }
        }

        //26. 获取商户区域列表接口
        public List<dd_table_floor> GetFloorList()
        {
            try
            {
                return db.dd_table_floor.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
            }
            catch (Exception e)
            {
                log.Error("GetFloorList error. msg=" + e.Message);
                throw;
            }
        }

        //27. 添加修改区域管理接口
        public void SaveFloor()
        {
            try
            {

            }
            catch (Exception e)
            {
                log.Error("SaveFloor error. msg=" + e.Message);
                throw;
            }
        }

        //28. 删除区域管理接口
        public void DelFloor()
        {
            try {

            }
            catch (Exception e)
            {
                log.Error("DelFloor error. msg=" + e.Message);
                throw;
            }
        }

        //29. 区域状态修改接口
        public void SavTable()
        {
            try
            {

            }
            catch (Exception e)
            {
                log.Error("SavTable error. msg=" + e.Message);
                throw;
            }
        }
    }
}
