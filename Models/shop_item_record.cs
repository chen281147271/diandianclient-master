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
    
    public partial class shop_item_record
    {
        public int shopitemrecordkey { get; set; }
        public Nullable<int> shopkey { get; set; }
        public Nullable<int> itemkey { get; set; }
        public Nullable<int> sellNum { get; set; }
        public Nullable<int> recommentNum { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<int> excepnum { get; set; }
        public Nullable<int> isexception { get; set; }
        public Nullable<int> addnum { get; set; }
        public Nullable<int> guigeid { get; set; }
    
        public virtual item item { get; set; }
        public virtual shop shop { get; set; }
    }
}