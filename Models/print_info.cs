
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
    
public partial class print_info
{

    public int printinfokey { get; set; }

    public Nullable<int> printmainkey { get; set; }

    public Nullable<int> type { get; set; }

    public Nullable<int> name { get; set; }

    public Nullable<int> address { get; set; }



    public virtual print_main print_main { get; set; }

}

}
