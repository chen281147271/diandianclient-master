using DianDianClient.Models;
using System;
using System.Collections.Generic;
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
            //本地业务
            return db.dd_shop_windows.Where(p => p.shopid == Properties.Settings.Default.shopkey).ToList();
        }

        //15. 添加修改档口
        public void UpdateStall(int shopid, int id, string name, string desc, string printname, int printnum, int isdefault, int status, int isprintexcep, int isyicaiyidan)
        {
            //本地业务

            //异步通知服务器
            remote_request rr = new remote_request();
            rr.create_time = DateTime.Now;
            rr.deal_flag = 0;
            rr.request_type = "GET";
            rr.request_url = WindowsUrl;
            rr.request_param = "m=update&shopid=" + shopid;
            rr.request_param = "&shopid=" + shopid;
            rr.request_param = "&id=" + id;
            rr.request_param = "&name=" + name;
            rr.request_param = "&desc=" + desc;
            rr.request_param = "&printname=" + printname;
            rr.request_param = "&printnum=" + printnum;
            rr.request_param = "&isdefault=" + isdefault;
            rr.request_param = "&status=" + status;
            rr.request_param = "&isprintexcep=" + isprintexcep;
            rr.request_param = "&isyicaiyidan=" + isyicaiyidan;

            db.remote_request.Add(rr);
            db.SaveChanges();
        }

        //16. 删除档口接口
        public void Delwindow(int id)
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


        //25. 获取商户餐桌列表接口
        public List<table_pos> GetTableList()
        {
            return db.table_pos.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
        }

        //26. 获取商户区域列表接口
        public List<dd_table_floor> GetFloorList()
        {
            return db.dd_table_floor.Where(p => p.shopkey == Properties.Settings.Default.shopkey).ToList();
        }

        //27. 添加修改区域管理接口
        public void SaveFloor()
        {

        }

        //28. 删除区域管理接口
        public void DelFloor()
        {

        }

        //29. 区域状态修改接口
        public void SavTable()
        {

        }
    }
}
