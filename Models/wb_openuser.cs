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
    
    public partial class wb_openuser
    {
        public int openid { get; set; }
        public Nullable<int> userid { get; set; }
        public string openuuid { get; set; }
        public Nullable<System.DateTime> regtime { get; set; }
        public string opentype { get; set; }
        public string oauth_token { get; set; }
        public string oauth_token_secret { get; set; }
        public string unionid { get; set; }
        public Nullable<bool> isdian { get; set; }
    }
}