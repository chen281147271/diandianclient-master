
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
    
public partial class table_member
{

    public int tablememberkey { get; set; }

    public Nullable<int> type { get; set; }

    public Nullable<int> memberkey { get; set; }

    public Nullable<int> shopkey { get; set; }

    public Nullable<int> tableNo { get; set; }

    public Nullable<System.DateTime> createDate { get; set; }

    public string jiucanno { get; set; }



    public virtual member member { get; set; }

    public virtual shop shop { get; set; }

}

}
