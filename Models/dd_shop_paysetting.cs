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
    
    public partial class dd_shop_paysetting
    {
        public int id { get; set; }
        public Nullable<int> shopkey { get; set; }
        public Nullable<int> payway { get; set; }
        public string wxsign { get; set; }
        public string wxdeskey { get; set; }
        public string merchantNo { get; set; }
        public string addtime { get; set; }
        public Nullable<decimal> t0 { get; set; }
        public Nullable<decimal> t1 { get; set; }
        public string creditcardno { get; set; }
        public string alisign { get; set; }
        public string alideskey { get; set; }
        public string queryKey { get; set; }
        public Nullable<bool> isdel { get; set; }
        public Nullable<sbyte> isquick { get; set; }
        public Nullable<decimal> quick { get; set; }
        public string msg { get; set; }
    }
}
