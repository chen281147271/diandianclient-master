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
    
    public partial class menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public menu()
        {
            this.menu1 = new HashSet<menu>();
        }
    
        public int menukey { get; set; }
        public Nullable<int> parentMenukey { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string icon { get; set; }
        public string url { get; set; }
        public Nullable<int> orderNo { get; set; }
        public Nullable<int> type { get; set; }
        public Nullable<int> level { get; set; }
        public Nullable<int> enable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<menu> menu1 { get; set; }
        public virtual menu menu2 { get; set; }
    }
}
