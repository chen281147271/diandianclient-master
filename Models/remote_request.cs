
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
    
public partial class remote_request
{

    public int sid { get; set; }

    public string request_url { get; set; }

    public string request_param { get; set; }

    public string request_type { get; set; }

    public Nullable<System.DateTime> create_time { get; set; }

    public Nullable<System.DateTime> deal_time { get; set; }

    public Nullable<int> deal_flag { get; set; }

    public string result_message { get; set; }

    public Nullable<int> result_code { get; set; }

}

}
