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
    
    public partial class shop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public shop()
        {
            this.cf_main = new HashSet<cf_main>();
            this.cf_meal = new HashSet<cf_meal>();
            this.income_record = new HashSet<income_record>();
            this.item = new HashSet<item>();
            this.item_category = new HashSet<item_category>();
            this.item_recomment = new HashSet<item_recomment>();
            this.member = new HashSet<member>();
            this.post = new HashSet<post>();
            this.post1 = new HashSet<post>();
            this.qr_code = new HashSet<qr_code>();
            this.qr_code_batch = new HashSet<qr_code_batch>();
            this.redpacket_record = new HashSet<redpacket_record>();
            this.shop_car = new HashSet<shop_car>();
            this.table_member = new HashSet<table_member>();
            this.shop_income_record = new HashSet<shop_income_record>();
            this.shop_item_record = new HashSet<shop_item_record>();
            this.shop_oper_log = new HashSet<shop_oper_log>();
            this.shop_info_record = new HashSet<shop_info_record>();
            this.table_pos = new HashSet<table_pos>();
            this.shop_income_clean = new HashSet<shop_income_clean>();
        }
    
        public int shopkey { get; set; }
        public string shopCode { get; set; }
        public string name { get; set; }
        public Nullable<int> shopType { get; set; }
        public string thumb { get; set; }
        public Nullable<int> place { get; set; }
        public string introduce { get; set; }
        public string phone { get; set; }
        public Nullable<int> provinceId { get; set; }
        public Nullable<int> cityId { get; set; }
        public Nullable<int> areaId { get; set; }
        public string detailAddr { get; set; }
        public Nullable<int> enable { get; set; }
        public Nullable<int> orderNo { get; set; }
        public Nullable<int> isDel { get; set; }
        public string openAccountBank { get; set; }
        public string bankNo { get; set; }
        public string bankRealName { get; set; }
        public Nullable<int> isPrint { get; set; }
        public string printer { get; set; }
        public Nullable<int> autoRecvOrder { get; set; }
        public Nullable<int> autoPrint { get; set; }
        public Nullable<int> isAudit { get; set; }
        public string contact { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public string editName { get; set; }
        public string editThumb { get; set; }
        public string editDetailAddr { get; set; }
        public string editPhone { get; set; }
        public string editContact { get; set; }
        public string editBankRealName { get; set; }
        public string editOpenAccountBank { get; set; }
        public string editBankNo { get; set; }
        public string qrCode { get; set; }
        public Nullable<int> islocktable { get; set; }
        public Nullable<int> isposition { get; set; }
        public string bgimg { get; set; }
        public string tastes { get; set; }
        public Nullable<int> isPayOnline { get; set; }
        public string longitude { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string street { get; set; }
        public string address { get; set; }
        public Nullable<decimal> averageprice { get; set; }
        public string signatory { get; set; }
        public string fixer { get; set; }
        public string latitude { get; set; }
        public string banktype { get; set; }
        public string managetype { get; set; }
        public Nullable<decimal> signrate { get; set; }
        public Nullable<decimal> t0rate { get; set; }
        public Nullable<decimal> cardrate { get; set; }
        public Nullable<int> validitydate { get; set; }
        public string license { get; set; }
        public string character { get; set; }
        public string licenseaddr { get; set; }
        public string licupdatedate { get; set; }
        public string asdate { get; set; }
        public string legaler { get; set; }
        public string legaleridcard { get; set; }
        public string legaleridvalidity { get; set; }
        public string legalerphone { get; set; }
        public Nullable<int> agentid { get; set; }
        public string bankcode { get; set; }
        public string bankname { get; set; }
        public string chaintype { get; set; }
        public string isshowrecom { get; set; }
        public string reason { get; set; }
        public string ispaycleartable { get; set; }
        public string istell { get; set; }
        public Nullable<int> isdian { get; set; }
        public Nullable<sbyte> ist0 { get; set; }
        public string openid { get; set; }
        public string creditcardno { get; set; }
        public Nullable<decimal> quickrate { get; set; }
        public Nullable<sbyte> isserver { get; set; }
        public string zbankname { get; set; }
        public string zbankcode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cf_main> cf_main { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cf_meal> cf_meal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<income_record> income_record { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item> item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item_category> item_category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<item_recomment> item_recomment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<member> member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<post> post { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<post> post1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qr_code> qr_code { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qr_code_batch> qr_code_batch { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<redpacket_record> redpacket_record { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shop_car> shop_car { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<table_member> table_member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shop_income_record> shop_income_record { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shop_item_record> shop_item_record { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shop_oper_log> shop_oper_log { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shop_info_record> shop_info_record { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<table_pos> table_pos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shop_income_clean> shop_income_clean { get; set; }
    }
}
