
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
    
public partial class dd_withdraw_record
{

    public int withdrawid { get; set; }

    public Nullable<decimal> cash { get; set; }

    public Nullable<System.DateTime> addtime { get; set; }

    public Nullable<int> memberkey { get; set; }

    public Nullable<int> shopkey { get; set; }

    public string type { get; set; }

    public string state { get; set; }

    public Nullable<int> auditmember { get; set; }

    public string comm { get; set; }

    public Nullable<System.DateTime> audittime { get; set; }

}

}
