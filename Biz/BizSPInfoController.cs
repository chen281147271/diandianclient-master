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
        //private DianDianEntities db = new DianDianEntities();

        private String WindowsUrl = "http://app.diandiancaidan.com/back/windows.do";

        //14. 档口列表接口
        public List<dd_shop_windows> StallsList()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
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
                DianDianEntities db = new DianDianEntities();
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
                DianDianEntities db = new DianDianEntities();
                //本地业务
                dd_shop_windows window = new dd_shop_windows();
                window.windowid = id;

                db.dd_shop_windows.Attach(window);
                db.dd_shop_windows.Remove(window);

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
                DianDianEntities db = new DianDianEntities();
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
                DianDianEntities db = new DianDianEntities();
                return db.dd_table_floor.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
            }
            catch (Exception e)
            {
                log.Error("GetFloorList error. msg=" + e.Message);
                throw;
            }
        }

        //27. 添加修改区域管理接口
        public void SaveFloor(int floorid, string floorname, int orderno, decimal fuwu)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_table_floor floor = db.dd_table_floor.Find(floorid);
                if(floor == null)
                {
                    floor = new dd_table_floor();
                    floor.createdate = DateTime.Now;
                    floor.ffuwu = fuwu;
                    floor.floorname = floorname;
                    floor.orderno = orderno;
                    floor.isdel = 0;
                    floor.shopkey = Properties.Settings.Default.shopkey;
                    floor.state = 1;

                    db.dd_table_floor.Add(floor);
                }
                else
                {                    
                    floor.ffuwu = fuwu;
                    floor.floorname = floorname;
                    floor.orderno = orderno;

                    db.dd_table_floor.Attach(floor);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(floor);
                    stateEntity.SetModifiedProperty("ffuwu");
                    stateEntity.SetModifiedProperty("floorname");
                    stateEntity.SetModifiedProperty("orderno");
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("SaveFloor error. msg=" + e.Message);
                throw;
            }
        }

        //28. 删除区域管理接口
        public void DelFloor(int floorid)
        {
            try {
                DianDianEntities db = new DianDianEntities();
                dd_table_floor floor = db.dd_table_floor.Find(floorid);
                floor.isdel = 1;

                db.dd_table_floor.Attach(floor);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(floor);
                stateEntity.SetModifiedProperty("isdel");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DelFloor error. msg=" + e.Message);
                throw;
            }
        }

        //29. 区域状态修改接口
        public void SavFloor(int floorid, sbyte state)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_table_floor floor = db.dd_table_floor.Find(floorid);
                floor.state = state;

                db.dd_table_floor.Attach(floor);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(floor);
                stateEntity.SetModifiedProperty("state");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("SavTable error. msg=" + e.Message);
                throw;
            }
        }

        public void AddTable(int isRoom, int tableNo, string tableName, int peoplenum, decimal tfuwu, int floorid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var table = db.table_pos.Where(p => p.shopkey == Properties.Settings.Default.shopkey && p.tableNo == tableNo).FirstOrDefault();
                if(table == null)
                {
                    table = new table_pos();
                    table.tableNo = tableNo;
                    table.isRoom = isRoom;
                    if(isRoom == 0)
                    {
                        table.tableName = tableNo + "号桌";
                    }
                    else
                    {
                        table.tableName = tableName;
                    }
                    table.peopleNum = peoplenum;
                    table.floorid = floorid;
                    table.tfuwu = tfuwu;

                    db.table_pos.Add(table);
                }
                else
                {
                    ModifyTable(table.tableposkey, isRoom, tableNo, tableName, peoplenum, tfuwu, floorid);
                    EnbaleTable(table.tableposkey, 1);
                    DelTable(table.tableposkey, 0);
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("AddTable error. msg=" + e.Message);
                throw;
            }
        }

        public void ModifyTable(int tableposkey, int isRoom, int tableNo, string tableName, int peoplenum, decimal tfuwu, int floorid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var table = db.table_pos.Find(tableposkey);
                table.tableNo = tableNo;
                table.isRoom = isRoom;
                if (isRoom == 0)
                {
                    table.tableName = tableNo + "号桌";
                }
                else
                {
                    table.tableName = tableName;
                }
                table.peopleNum = peoplenum;
                table.floorid = floorid;
                table.tfuwu = tfuwu;

                db.table_pos.Attach(table);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(table);
                stateEntity.SetModifiedProperty("tableNo");
                stateEntity.SetModifiedProperty("isRoom");
                stateEntity.SetModifiedProperty("tableName");
                stateEntity.SetModifiedProperty("peopleNum");
                stateEntity.SetModifiedProperty("floorid");
                stateEntity.SetModifiedProperty("tfuwu");
                stateEntity.SetModifiedProperty("tableNo");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("ModifyTable error. msg=" + e.Message);
                throw;
            }
        }

        public void EnbaleTable(int tableposkey, int enableFlag) {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var table = db.table_pos.Find(tableposkey);

                table.enable = enableFlag;
                db.table_pos.Attach(table);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(table);
                stateEntity.SetModifiedProperty("enable");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("EnbaleTable error. msg=" + e.Message);
                throw;
            }
        }

        public void DelTable(int tableposkey, int delFlag) {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var table = db.table_pos.Find(tableposkey);

                table.isDel = delFlag;
                db.table_pos.Attach(table);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(table);
                stateEntity.SetModifiedProperty("isDel");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DelTable error. msg=" + e.Message);
                throw;
            }
        }
        public List<dd_shop_payway> QueryPayWay()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var paywayList = db.dd_shop_payway.Where(p => p.shopid == Properties.Settings.Default.shopkey && p.isdel == 0);
                return paywayList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryPayWay error. msg=" + e.Message);
                throw;
            }
        }

        public void SaveShopInfo(string shoppic, string shopname, string shopphone, string contact)
        {

        }
    }
}
