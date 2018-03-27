using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizEmployee
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizEmployee");

        public List<v_memberrole> QueryEmployee(string name,int isDel=-1)
        {
            try {
                DianDianEntities db = new DianDianEntities();
                var userList = db.v_memberrole.Where(p => p.shopkey == Properties.Settings.Default.shopkey);
                if (!name.Equals("")) {
                    userList = userList.Where(p => p.name.Contains(name));
                }
                if (isDel != -1)
                {
                    userList = userList.Where(p=>p.isDel==isDel);
                }
                return userList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryEmployee error. msg=" + e.Message);
                throw;
            }
        }

        public List<sys_role> QueryPostion(string isdel="")
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                if (isdel == "") {
                    return db.sys_role.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
                }
                else
                {
                    return db.sys_role.Where(p => p.shopkey == Properties.Settings.Default.shopkey && p.isdel ==isdel).ToList();
                }
            }
            catch (Exception e)
            {
                log.Error("QueryPostion error. msg=" + e.Message);
                throw;
            }
        }

        public List<v_emp_floor> QueryEmployeeFloorList(int memberkey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                return db.v_emp_floor.Where(p => p.memberkey == memberkey).ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryEmployeeFloorList error. msg=" + e.Message);
                throw;
            }
        }

        public void SaveEmployee(int memberkey, string name, int posid, List<int>floorList)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var mem = db.member.Find(memberkey);
                if(mem == null)
                {
                    mem = new member();
                    mem.name = name;
                    mem.role = posid;
                    mem.isDel = 0;
                    db.member.Add(mem);
                }
                else
                {
                    mem.name = name;
                    mem.role = posid;

                    db.member.Attach(mem);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(mem);
                    stateEntity.SetModifiedProperty("name");
                    stateEntity.SetModifiedProperty("role");
                }
                var delList = db.dd_emp_floor.Where(p => p.memberkey == memberkey && !floorList.Contains(p.floorid.Value)).ToList();
                foreach(var item in delList)
                {
                    db.dd_emp_floor.Attach(item);
                    db.dd_emp_floor.Remove(item);
                }
                foreach(int floorid in floorList)
                {
                    dd_emp_floor fl = db.dd_emp_floor.Where(p => p.floorid == floorid && p.memberkey == memberkey).FirstOrDefault();
                    if(fl == null)
                    {
                        fl = new dd_emp_floor();
                        fl.floorid = floorid;
                        fl.memberkey = memberkey;

                        db.dd_emp_floor.Add(fl);
                    }
                    else
                    {
                        fl.floorid = floorid;
                        fl.memberkey = memberkey;

                        db.dd_emp_floor.Attach(fl);
                        var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(fl);
                        stateEntity.SetModifiedProperty("floorid");
                        stateEntity.SetModifiedProperty("memberkey");
                    }
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("SaveEmployee error. msg=" + e.Message);
                throw;
            }
        }
        public List<sys_right> QuerySysRight(int sysroleid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                return db.sys_right.Where(o => o.sysroleid == sysroleid).ToList();
            }
            catch(Exception e)
            {
                log.Error("QuerySysRight error. msg=" + e.Message);
                throw;
            }
        }
        //订单管理 134
        //餐桌管理 137
        //桌位结算 159
        //菜品管理 133
        //营业详情 144
        //商户设置 147
        //员工管理 148
        //会员管理 150
        public void SavePosition(int posId, string posName, List<int> rightList)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var pos = db.sys_role.Find(posId);
                if(pos == null)
                {
                    pos = new sys_role();
                    pos.rolename = posName;
                    pos.createtime = DateTime.Now.ToString();
                    pos.isdel = "0";
                    pos.state = "1";
                    pos.shopkey = Properties.Settings.Default.shopkey;


                    db.sys_role.Add(pos);
                }
                else
                {
                    pos.rolename = posName;

                    db.sys_role.Attach(pos);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(pos);
                    stateEntity.SetModifiedProperty("rolename");
                }
                var delList = db.sys_right.Where(p => p.sysroleid == pos.sysroleid& !rightList.Contains(p.menuid.Value)).ToList();
                foreach(var item in delList)
                {
                    db.sys_right.Attach(item);
                    db.sys_right.Remove(item);
                }
                db.SaveChanges();
                foreach (int menuId in rightList)
                {
                    var item = db.sys_right.Where(p => p.menuid == menuId && p.sysroleid == pos.sysroleid).FirstOrDefault();
                    if(item == null)
                    {
                        item = new sys_right();
                        item.menuid = menuId;
                        item.sysroleid = Convert.ToInt32(pos.sysroleid);
                        item.userid = 1000;
                        item.type = "menu1";
                        item.optime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        db.sys_right.Add(item);
                    }
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("SavePosition error. msg=" + e.Message);
                throw;
            }
        }

        public void DelEmployee(int memberkey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var mem = db.member.Find(memberkey);
                mem.isDel = 1;

                db.member.Attach(mem);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(mem);
                stateEntity.SetModifiedProperty("isDel");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DelEmployee error. msg=" + e.Message);
                throw;
            }
        }

        public void DelPositon(int posid)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var rl = db.sys_role.Find(posid);
                rl.isdel = "1";

                db.sys_role.Attach(rl);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(rl);
                stateEntity.SetModifiedProperty("isdel");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("DelPositon error. msg=" + e.Message);
                throw;
            }
        }

        public void EnabelEmployee(int memberkey, int enable)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var mem = db.member.Find(memberkey);
                mem.enable = enable;

                db.member.Attach(mem);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(mem);
                stateEntity.SetModifiedProperty("enable");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("EnabelEmployee error. msg=" + e.Message);
                throw;
            }
        }

        public void EnabelPosition(int posid, string state)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var rl = db.sys_role.Find(posid);
                rl.state = state;

                db.sys_role.Attach(rl);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(rl);
                stateEntity.SetModifiedProperty("state");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("EnabelPosition error. msg=" + e.Message);
                throw;
            }
        }

        public void BindWeixin()
        {
            try
            {
            }
            catch (Exception e)
            {
                log.Error("BindWeixin error. msg=" + e.Message);
                throw;
            }
        }

        public void GeneraEmployeeBarcode()
        {
            try
            {
            }
            catch (Exception e)
            {
                log.Error("GeneraEmployeeBarcode error. msg=" + e.Message);
                throw;
            }
        }
    }
}
