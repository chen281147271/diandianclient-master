using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizPrinter
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizBillController");

        public List<dd_printers> QueryPrinters(int useState, string printername)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var printerList = db.dd_printers.Where(p => p.shopid == Properties.Settings.Default.shopkey);
                if(useState == 0 || useState == 1)
                {
                    printerList = printerList.Where(p => p.status == useState);
                }
                if (!printername.Equals(""))
                {
                    printerList = printerList.Where(p => p.printername.Equals(printername));
                }
                return printerList.ToList();
            }
            catch (Exception e)
            {
                log.Error("QueryPrinters error. msg=" + e.Message);
                throw;
            }

        }

        public void AddPrinter(string printername, int shopid, string pstatus, int psize, int pbites, int isdefault)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_printers printer = QueryPrinters(2, printername).FirstOrDefault();
                if(printer == null)
                {
                    printer = new dd_printers();
                    printer.printername = printername;
                    printer.status = 0;
                    printer.shopid = shopid;
                    printer.pstatus = pstatus;
                    printer.psize = 58;
                    printer.pbites = 19200;
                    printer.isdefault = isdefault;

                    db.dd_printers.Add(printer);
                    db.SaveChanges();
                }
                else
                {
                    printer.pstatus = pstatus;
                    db.dd_printers.Attach(printer);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(printer);
                    stateEntity.SetModifiedProperty("pstatus");
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error("AddPrinter error. msg=" + e.Message);
                throw;
            }
        }
        
        public void delPrinter()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                db.Database.ExecuteSqlCommand("delete from dd_printers where pstatus =='-1'");
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("delPrinter error. msg=" + e.Message);
                throw;
            }
        }

        public void downPrinter()
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                db.Database.ExecuteSqlCommand("update dd_printers set pstatus ='-1' where pstatus <>'1'");
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("downPrinter error. msg=" + e.Message);
                throw;
            }
        }

        public void setPrinter(string name, int status)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var printer = db.dd_printers.Where(p => p.printername.Equals("name")).FirstOrDefault();
                if(printer != null)
                {
                    printer.status = status;
                    db.dd_printers.Attach(printer);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(printer);
                    stateEntity.SetModifiedProperty("pstatus");
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                log.Error("setPrinter error. msg=" + e.Message);
                throw;
            }
        }

        public void setAutoConfrim(int status)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_user user = db.dd_user.Where(p => p.shopid == Properties.Settings.Default.shopkey).FirstOrDefault();
                if(user != null)
                {
                    user.autoconfirm = status;
                    db.dd_user.Attach(user);
                    var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(user);
                    stateEntity.SetModifiedProperty("autoconfirm");
                    db.SaveChanges();
                }
                //db.Database.ExecuteSqlCommand("update dd_user set autoconfirm="+status+" where shopid="+ Properties.Settings.Default.shopkey);
                
            }
            catch (Exception e)
            {
                log.Error("setAutoConfrim error. msg=" + e.Message);
                throw;
            }
        }

        public void setdprint(string dprinter)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_user user = db.dd_user.Where(p => p.shopid == Properties.Settings.Default.shopkey).FirstOrDefault();
                user.dprinter = dprinter;
                db.dd_user.Attach(user);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(user);
                stateEntity.SetModifiedProperty("dprinter");

                db.Database.ExecuteSqlCommand("update dd_printers set isdefault=0");

                var printer = db.dd_printers.Where(p => p.printername.Equals("dprinter")).FirstOrDefault();
                printer.isdefault = 1;
                db.dd_printers.Attach(printer);
                var stateEntity2 = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(printer);
                stateEntity2.SetModifiedProperty("isdefault");

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("setdprint error. msg=" + e.Message);
                throw;
            }
        }

        public void setdcomm(string dcomm)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_user user = db.dd_user.Where(p => p.shopid == Properties.Settings.Default.shopkey).FirstOrDefault();
                user.dcomm = dcomm;
                db.dd_user.Attach(user);
                var stateEntity = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager.GetObjectStateEntry(user);
                stateEntity.SetModifiedProperty("dcomm");
                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("setdcomm error. msg=" + e.Message);
                throw;
            }
        }

        public List<dd_printerqueue> getPrintQueue(string cfmainkey)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                var printList = db.dd_printerqueue.Where(p => p.cfmainkey.Equals(cfmainkey));

                return printList.ToList();
            }
            catch (Exception e)
            {
                log.Error("getPrintQueue error. msg=" + e.Message);
                throw;
            }
        }

        public void addPrintQueue(string printname, string pcontent, string cfmainkey, int printtype)
        {
            try
            {
                DianDianEntities db = new DianDianEntities();
                dd_printerqueue printquene = new dd_printerqueue();
                printquene.printername = printname;
                printquene.pcontent = pcontent;
                printquene.cfmainkey = cfmainkey;
                printquene.printtype = printtype;
                printquene.status = 0;
                printquene.addtime = DateTime.Now;

                db.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("addPrintQueue error. msg=" + e.Message);
                throw;
            }
        }

    }
}
