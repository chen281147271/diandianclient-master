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
    
    public partial class cf_detail
    {
        public string cfdetailkey { get; set; }
        public string cfmainkey { get; set; }
        public Nullable<int> memberkey { get; set; }
        public Nullable<int> num { get; set; }
        public Nullable<int> itemkey { get; set; }
        public Nullable<int> type { get; set; }
        public string createDate { get; set; }
        public string completeDate { get; set; }
        public Nullable<int> isComplete { get; set; }
        public Nullable<int> mgrkey { get; set; }
        public Nullable<int> interactionrecordkey1 { get; set; }
        public Nullable<int> interactionrecordkey2 { get; set; }
        public Nullable<int> isSong { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> isException { get; set; }
        public string exceptionRemark { get; set; }
        public Nullable<int> excptionNum { get; set; }
        public Nullable<int> guigeid { get; set; }
        public string guigename { get; set; }
        public string exceptionRemarkStr { get; set; }
        public string zuofa { get; set; }
        public string songcaiyuanyin { get; set; }
        public Nullable<decimal> youhui { get; set; }
        public Nullable<decimal> weight { get; set; }
    
        public virtual interaction_record interaction_record { get; set; }
        public virtual interaction_record interaction_record1 { get; set; }
        public virtual item item { get; set; }
    }
}