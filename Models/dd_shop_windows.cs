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
    
    public partial class dd_shop_windows
    {
        public int windowid { get; set; }
        public string windowname { get; set; }
        public string windowdesc { get; set; }
        public Nullable<int> shopid { get; set; }
        public string addtime { get; set; }
        public Nullable<int> status { get; set; }
        public string printname { get; set; }
        public Nullable<bool> printnum { get; set; }
        public Nullable<bool> isdefault { get; set; }
        public Nullable<int> porder { get; set; }
        public Nullable<bool> isyicaiyidan { get; set; }
        public Nullable<bool> isprintexcep { get; set; }
    }
}