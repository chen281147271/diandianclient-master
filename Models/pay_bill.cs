//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DianDianClient.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class pay_bill
    {
        public int payrecordkey { get; set; }
        public Nullable<int> memberkey { get; set; }
        public string billNo { get; set; }
        public Nullable<int> state { get; set; }
        public Nullable<int> type { get; set; }
        public Nullable<int> tableNo { get; set; }
        public string cfmealkey { get; set; }
        public string cfmainkey { get; set; }
        public string cfmealkeyTarget { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public string introduce { get; set; }
        public string remark { get; set; }
        public string cfMainStr { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<int> couponid { get; set; }
        public string transaction_id { get; set; }
        public Nullable<bool> paytype { get; set; }
        public string payuser { get; set; }
        public Nullable<sbyte> cardtype { get; set; }
    
        public virtual member member { get; set; }
    }
}
