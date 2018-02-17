using DianDianClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Biz
{
    class BizBillController
    {
        log4net.ILog log = log4net.LogManager.GetLogger("BizBillController");
        private DianDianEntities db = new DianDianEntities();
        

        //获取订单
        public void GetBillList()
        {

        }

        //确认接单
        public void SetPrintState()
        {
        }

        //退单
        public void Tuicai()
        {

        }

        //开桌
        public void BeginToUse()
        {

        }

        //清桌
        public void ClearTable()
        {

        }

        //餐桌列表
        public void GetTablePosList()
        {

        }

        //转台
        public void ChangeTable()
        {

        }

        //合桌
        public void MergeTable()
        {

        }

        //10. 菜品打折接口
        public void Dazhe()
        {

        }

        //11. 餐桌订单优惠接口
        public void Saveyouhui()
        {

        }

        //12. 买单接口
        public void Maidan()
        {

        }

        //13. 下单接口
        public void SubmitBill()
        {

        }

    }
}
