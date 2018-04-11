using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Models
{
    class GetBillResponseBean
    {
        public string code { get; set; }
        public GetBillPageInfoResponseBean pageinfo { get; set; }
        public List<GetBillResultResponseBean> results { get; set; }
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
        public decimal? amount { get; set; }
        public string billno { get; set; }
        public string cfmainkey { get; set; }
        public string cfmealkey { get; set; }
        public string createdate { get; set; }
        public string customremark { get; set; }
        public decimal? exceptionamount { get; set; }
        public decimal? exceptionamountal { get; set; }
        public decimal? exceptionamountun { get; set; }
        public string exceptionplan { get; set; }
        public string exceptionremark { get; set; }
        public string exceptiontime { get; set; }
        public string introduce { get; set; }
        public int? iscomplete { get; set; }
        public int? isconfirm { get; set; }
        public int? isexception { get; set; }
        public int? issong { get; set; }
        public int? memberkey { get; set; }
        public string orderno { get; set; }
        public string paydate { get; set; }
        public string paytype { get; set; }
        public int? peoplenum { get; set; }
        public decimal? realpay { get; set; }
        public string remark { get; set; }
        public string sctime { get; set; }
        public int? serialno { get; set; }
        public int? shopkey { get; set; }
        public int? state { get; set; }
        public string tablename { get; set; }
        public int? tableno { get; set; }
        public int? type { get; set; }
        //public string users { get; set; }
        public decimal? youhui { get; set; }

        public List<GetBillResultDetailBean> detail { get; set; }
        public GetBillResultMainBean main { get; set; }
        public GetBillResultMealBean meal { get; set; }
        public List<GetBillResultTipsBean> tips { get; set; }
        public List<GetBillResultUserBean> users { get; set; }
    }
    class GetBillResultDetailBean
    {
        public string cfdetailkey { get; set; }
        public string cfmainkey { get; set; }
        public string completedate { get; set; }
        public string createdate { get; set; }
        public string exceptionremark { get; set; }
        public string exceptionremarkstr { get; set; }
        public int? excptionnum { get; set; }
        public int? guigeid { get; set; }
        public string guigename { get; set; }
        public string interactionrecordkey1 { get; set; }
        public string interactionrecordkey2 { get; set; }
        public int? iscomplete { get; set; }
        public int? isexception { get; set; }
        public int? issong { get; set; }
        public int? itemkey { get; set; }
        public int? memberkey { get; set; }
        public int? mgrkey { get; set; }
        public int? num { get; set; }
        public decimal? price { get; set; }
        public string songcaiyuanyin { get; set; }
        public int? type { get; set; }
        public decimal? weight { get; set; }
        public decimal? youhui { get; set; }
        public string zuofa { get; set; }

    }

    class GetBillResultMainBean
    {
        public decimal? amount { get; set; }
        public string billno { get; set; }
        public string cfmainkey { get; set; }
        public string cfmealkey { get; set; }
        public string createdate { get; set; }
        public string customremark { get; set; }
        public decimal? exceptionamount { get; set; }
        public decimal? exceptionamountal { get; set; }
        public decimal? exceptionamountun { get; set; }
        public string exceptionplan { get; set; }
        public string exceptionremark { get; set; }
        public string exceptiontime { get; set; }
        public string introduce { get; set; }
        public int? iscomplete { get; set; }
        public int? isconfirm { get; set; }
        public int? isexception { get; set; }
        public int? issong { get; set; }
        public int? memberkey { get; set; }
        public string orderno { get; set; }
        public string paydate { get; set; }
        public string paytype { get; set; }
        public int? peoplenum { get; set; }
        public decimal? realpay { get; set; }
        public string remark { get; set; }
        public string sctime { get; set; }
        public int? serialno { get; set; }
        public int? shopkey { get; set; }
        public int? state { get; set; }
        public int subtype { get; set; }
        public int? tableno { get; set; }
        public int? type { get; set; }
        public decimal? youhui { get; set; }
        public decimal? zexception { get; set; }
    }

    class GetBillResultMealBean
    {
        public string cfmealkey { get; set; }
        public string createdate { get; set; }
        public string exceptionplan { get; set; }
        public string exceptionremark { get; set; }
        public int? iscomplete { get; set; }
        public int? isfree { get; set; }
        public int? isrecomment { get; set; }
        public int? issign { get; set; }
        public string jiucanno { get; set; }
        public string merge { get; set; }
        public string paymemberkey { get; set; }
        public int? peoples { get; set; }
        public string recommentmemberkey { get; set; }
        public string redpackagecode { get; set; }
        public int? serialno { get; set; }
        public int? shopkey { get; set; }
        public string shopuserid { get; set; }
        public int? tableno { get; set; }
        public string usehbcode { get; set; }
        public decimal? zexception { get; set; }
    }

    class GetBillResultTipsBean
    {
        public string addtime { get; set; }
        public string cfmealkey { get; set; }
        public decimal? feiyong { get; set; }
        public string id { get; set; }
        public string orderno { get; set; }
        public string paytime { get; set; }
        public int? shopkey { get; set; }
        public int? state { get; set; }
        public string uuid { get; set; }

    }

    class GetBillResultUserBean
    {
        public int? comenums { get; set; }
        public string usericon { get; set; }
        public int? userid { get; set; }
        public string username { get; set; }
    }
}
