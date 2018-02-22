using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Models
{
    class GetBillResponseBean
    {
        public string code { get; set; }
        public string pageinfo { get; set; }
        public string results { get; set; }
        public string msg { get; set; }
    }

    class GetBillPageInfoResponseBean
    {
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int pageStartRow { get; set; }
        public string queryStr { get; set; }
        public int totalPages { get; set; }
        public int totalRows { get; set; }
    }

    class GetBillResultResponseBean
    {
        public string addtime { get; set; }
        public string amount { get; set; }
        public string billno { get; set; }
        public string cfmainkey { get; set; }
        public string cfmealkey { get; set; }
        public string createdate { get; set; }
        public string customremark { get; set; }
        public string exceptionamount { get; set; }
        public string exceptionamountal { get; set; }
        public string exceptionamountun { get; set; }
        public string exceptionplan { get; set; }
        public string exceptionremark { get; set; }
        public string exceptiontime { get; set; }
        public string introduce { get; set; }
        public string iscomplete { get; set; }
        public string isconfirm { get; set; }
        public string isexception { get; set; }
        public string issong { get; set; }
        public string memberkey { get; set; }
        public string orderno { get; set; }
        public string paydate { get; set; }
        public string paytype { get; set; }
        public string peoplenum { get; set; }
        public string realpay { get; set; }
        public string remark { get; set; }
        public string sctime { get; set; }
        public string serialno { get; set; }
        public string shopkey { get; set; }
        public string state { get; set; }
        public string tablename { get; set; }
        public string tableno { get; set; }
        public string type { get; set; }
        public string users { get; set; }
        public string youhui { get; set; }
    }
}
