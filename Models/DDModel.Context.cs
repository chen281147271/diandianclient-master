﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DianDianEntities : DbContext
    {
        public DianDianEntities()
            : base("name=DianDianEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<allot_adjunct> allot_adjunct { get; set; }
        public virtual DbSet<allot_agent> allot_agent { get; set; }
        public virtual DbSet<allot_agent_qrcode> allot_agent_qrcode { get; set; }
        public virtual DbSet<allot_agent_qrcode_num> allot_agent_qrcode_num { get; set; }
        public virtual DbSet<allot_clear_zyhfw> allot_clear_zyhfw { get; set; }
        public virtual DbSet<allot_statistics> allot_statistics { get; set; }
        public virtual DbSet<allot_tixian> allot_tixian { get; set; }
        public virtual DbSet<bc_shop> bc_shop { get; set; }
        public virtual DbSet<bc_shop_account> bc_shop_account { get; set; }
        public virtual DbSet<cf_detail> cf_detail { get; set; }
        public virtual DbSet<cf_member> cf_member { get; set; }
        public virtual DbSet<comment_keyword> comment_keyword { get; set; }
        public virtual DbSet<dd_actives> dd_actives { get; set; }
        public virtual DbSet<dd_agent_balancetype> dd_agent_balancetype { get; set; }
        public virtual DbSet<dd_bank_code> dd_bank_code { get; set; }
        public virtual DbSet<dd_bind_cards> dd_bind_cards { get; set; }
        public virtual DbSet<dd_book_order_items> dd_book_order_items { get; set; }
        public virtual DbSet<dd_card_charges> dd_card_charges { get; set; }
        public virtual DbSet<dd_card_tongji> dd_card_tongji { get; set; }
        public virtual DbSet<dd_chargecar_rule> dd_chargecar_rule { get; set; }
        public virtual DbSet<dd_coupons> dd_coupons { get; set; }
        public virtual DbSet<dd_false_data> dd_false_data { get; set; }
        public virtual DbSet<dd_item_isgood> dd_item_isgood { get; set; }
        public virtual DbSet<dd_mem_card> dd_mem_card { get; set; }
        public virtual DbSet<dd_mem_coupons> dd_mem_coupons { get; set; }
        public virtual DbSet<dd_member_shopcord> dd_member_shopcord { get; set; }
        public virtual DbSet<dd_msg_imgurl> dd_msg_imgurl { get; set; }
        public virtual DbSet<dd_offline_error> dd_offline_error { get; set; }
        public virtual DbSet<dd_order_refund> dd_order_refund { get; set; }
        public virtual DbSet<dd_pay_channels> dd_pay_channels { get; set; }
        public virtual DbSet<dd_pay_orders> dd_pay_orders { get; set; }
        public virtual DbSet<dd_platform_log> dd_platform_log { get; set; }
        public virtual DbSet<dd_qrcode_printaudit> dd_qrcode_printaudit { get; set; }
        public virtual DbSet<dd_qrpay_balance> dd_qrpay_balance { get; set; }
        public virtual DbSet<dd_rate_actives> dd_rate_actives { get; set; }
        public virtual DbSet<dd_shop_apply_loan> dd_shop_apply_loan { get; set; }
        public virtual DbSet<dd_shop_paysetting> dd_shop_paysetting { get; set; }
        public virtual DbSet<dd_shop_payway> dd_shop_payway { get; set; }
        public virtual DbSet<dd_shop_sendmsg> dd_shop_sendmsg { get; set; }
        public virtual DbSet<dd_shop_service_msg> dd_shop_service_msg { get; set; }
        public virtual DbSet<dd_shop_weixin> dd_shop_weixin { get; set; }
        public virtual DbSet<dd_shop_wmcode> dd_shop_wmcode { get; set; }
        public virtual DbSet<dd_shop_wxpay> dd_shop_wxpay { get; set; }
        public virtual DbSet<dd_shopaddr_change> dd_shopaddr_change { get; set; }
        public virtual DbSet<dd_sign_accounts> dd_sign_accounts { get; set; }
        public virtual DbSet<dd_sign_meals> dd_sign_meals { get; set; }
        public virtual DbSet<dd_sys_consts> dd_sys_consts { get; set; }
        public virtual DbSet<dd_tuijian> dd_tuijian { get; set; }
        public virtual DbSet<dd_user_sendmsg> dd_user_sendmsg { get; set; }
        public virtual DbSet<dd_window_item> dd_window_item { get; set; }
        public virtual DbSet<dd_withdraw_record> dd_withdraw_record { get; set; }
        public virtual DbSet<dd_wx_memcard> dd_wx_memcard { get; set; }
        public virtual DbSet<dd_zyhfw_false> dd_zyhfw_false { get; set; }
        public virtual DbSet<elm_shop_refreshtoken> elm_shop_refreshtoken { get; set; }
        public virtual DbSet<income_record> income_record { get; set; }
        public virtual DbSet<interaction_record> interaction_record { get; set; }
        public virtual DbSet<item_category_map> item_category_map { get; set; }
        public virtual DbSet<item_comment> item_comment { get; set; }
        public virtual DbSet<item_recomment> item_recomment { get; set; }
        public virtual DbSet<item_set> item_set { get; set; }
        public virtual DbSet<item_standard> item_standard { get; set; }
        public virtual DbSet<member> member { get; set; }
        public virtual DbSet<member_notice> member_notice { get; set; }
        public virtual DbSet<member_role> member_role { get; set; }
        public virtual DbSet<menu> menu { get; set; }
        public virtual DbSet<mt_shop_state> mt_shop_state { get; set; }
        public virtual DbSet<mt_shop_token> mt_shop_token { get; set; }
        public virtual DbSet<pay_bill> pay_bill { get; set; }
        public virtual DbSet<post> post { get; set; }
        public virtual DbSet<post_power> post_power { get; set; }
        public virtual DbSet<power> power { get; set; }
        public virtual DbSet<print_info> print_info { get; set; }
        public virtual DbSet<print_main> print_main { get; set; }
        public virtual DbSet<qr_code> qr_code { get; set; }
        public virtual DbSet<qr_code_batch> qr_code_batch { get; set; }
        public virtual DbSet<redpacket_record> redpacket_record { get; set; }
        public virtual DbSet<redpacket_rules> redpacket_rules { get; set; }
        public virtual DbSet<remark_tag> remark_tag { get; set; }
        public virtual DbSet<remote_request> remote_request { get; set; }
        public virtual DbSet<role> role { get; set; }
        public virtual DbSet<role_power> role_power { get; set; }
        public virtual DbSet<shop> shop { get; set; }
        public virtual DbSet<shop_car> shop_car { get; set; }
        public virtual DbSet<shop_income_clean> shop_income_clean { get; set; }
        public virtual DbSet<shop_income_record> shop_income_record { get; set; }
        public virtual DbSet<shop_info_record> shop_info_record { get; set; }
        public virtual DbSet<shop_item_record> shop_item_record { get; set; }
        public virtual DbSet<shop_oper_log> shop_oper_log { get; set; }
        public virtual DbSet<shop_serial> shop_serial { get; set; }
        public virtual DbSet<sys_common_ctype> sys_common_ctype { get; set; }
        public virtual DbSet<sys_common_type> sys_common_type { get; set; }
        public virtual DbSet<sys_menu> sys_menu { get; set; }
        public virtual DbSet<sys_oper_log> sys_oper_log { get; set; }
        public virtual DbSet<sys_role> sys_role { get; set; }
        public virtual DbSet<t_document> t_document { get; set; }
        public virtual DbSet<table_member> table_member { get; set; }
        public virtual DbSet<turn_user_record> turn_user_record { get; set; }
        public virtual DbSet<wb_openuser> wb_openuser { get; set; }
        public virtual DbSet<allot_agent_edit> allot_agent_edit { get; set; }
        public virtual DbSet<dd_shop_chain> dd_shop_chain { get; set; }
        public virtual DbSet<v_dd_member_shopcord> v_dd_member_shopcord { get; set; }
        public virtual DbSet<v_cfdetailitem> v_cfdetailitem { get; set; }
        public virtual DbSet<v_orderitem> v_orderitem { get; set; }
        public virtual DbSet<allot_account_false> allot_account_false { get; set; }
        public virtual DbSet<allot_account> allot_account { get; set; }
        public virtual DbSet<v_category_items> v_category_items { get; set; }
        public virtual DbSet<dd_adjunct_edit> dd_adjunct_edit { get; set; }
        public virtual DbSet<dd_areas> dd_areas { get; set; }
        public virtual DbSet<dd_printers> dd_printers { get; set; }
        public virtual DbSet<dd_shop_paycode> dd_shop_paycode { get; set; }
        public virtual DbSet<dd_tuijian_link> dd_tuijian_link { get; set; }
        public virtual DbSet<dd_user> dd_user { get; set; }
        public virtual DbSet<dd_zbank_code> dd_zbank_code { get; set; }
        public virtual DbSet<elm_shop_id> elm_shop_id { get; set; }
        public virtual DbSet<shop_edit> shop_edit { get; set; }
        public virtual DbSet<sys_right> sys_right { get; set; }
        public virtual DbSet<dd_printerqueue> dd_printerqueue { get; set; }
        public virtual DbSet<dd_shop_account> dd_shop_account { get; set; }
        public virtual DbSet<v_dd_pocket_users> v_dd_pocket_users { get; set; }
        public virtual DbSet<dd_card_userecord> dd_card_userecord { get; set; }
        public virtual DbSet<dd_shop_windows> dd_shop_windows { get; set; }
        public virtual DbSet<dd_table_floor> dd_table_floor { get; set; }
        public virtual DbSet<v_memberrole> v_memberrole { get; set; }
        public virtual DbSet<dd_emp_floor> dd_emp_floor { get; set; }
        public virtual DbSet<v_emp_floor> v_emp_floor { get; set; }
        public virtual DbSet<storage_crude> storage_crude { get; set; }
        public virtual DbSet<storage_depot> storage_depot { get; set; }
        public virtual DbSet<storage_depotin> storage_depotin { get; set; }
        public virtual DbSet<storage_depotin_info> storage_depotin_info { get; set; }
        public virtual DbSet<storage_depotout> storage_depotout { get; set; }
        public virtual DbSet<storage_depotout_info> storage_depotout_info { get; set; }
        public virtual DbSet<storage_genre> storage_genre { get; set; }
        public virtual DbSet<storage_item_crude> storage_item_crude { get; set; }
        public virtual DbSet<storage_lossorspill> storage_lossorspill { get; set; }
        public virtual DbSet<v_storagelossorspill_crude> v_storagelossorspill_crude { get; set; }
        public virtual DbSet<storage_shop_depot> storage_shop_depot { get; set; }
        public virtual DbSet<v_item_crude> v_item_crude { get; set; }
        public virtual DbSet<v_depotout_crude> v_depotout_crude { get; set; }
        public virtual DbSet<storage_stock> storage_stock { get; set; }
        public virtual DbSet<v_stock_crude> v_stock_crude { get; set; }
        public virtual DbSet<v_dd_shop_users> v_dd_shop_users { get; set; }
        public virtual DbSet<v_dd_shop_users_count> v_dd_shop_users_count { get; set; }
        public virtual DbSet<v_depotin_crude> v_depotin_crude { get; set; }
        public virtual DbSet<v_shop_depot> v_shop_depot { get; set; }
        public virtual DbSet<cf_main> cf_main { get; set; }
        public virtual DbSet<cf_meal> cf_meal { get; set; }
        public virtual DbSet<dd_book_orders> dd_book_orders { get; set; }
        public virtual DbSet<v_cfmainaccount> v_cfmainaccount { get; set; }
        public virtual DbSet<v_cfmainmeal> v_cfmainmeal { get; set; }
        public virtual DbSet<v_cf_member> v_cf_member { get; set; }
        public virtual DbSet<dd_shop_signusers> dd_shop_signusers { get; set; }
        public virtual DbSet<v_cfmembermeal> v_cfmembermeal { get; set; }
        public virtual DbSet<v_mem_card_shop> v_mem_card_shop { get; set; }
        public virtual DbSet<v_card_record_shop> v_card_record_shop { get; set; }
        public virtual DbSet<table_pos> table_pos { get; set; }
        public virtual DbSet<item> item { get; set; }
        public virtual DbSet<item_category> item_category { get; set; }
        public virtual DbSet<v_crude_genre> v_crude_genre { get; set; }
        public virtual DbSet<dd_meal_tips> dd_meal_tips { get; set; }
    
        public virtual int ClearItemSyncFlag()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ClearItemSyncFlag");
        }
    
        public virtual ObjectResult<Nullable<decimal>> GetSumSaleMoney(string itemname, string categoryname, Nullable<System.DateTime> sdate, Nullable<System.DateTime> edate)
        {
            var itemnameParameter = itemname != null ?
                new ObjectParameter("itemname", itemname) :
                new ObjectParameter("itemname", typeof(string));
    
            var categorynameParameter = categoryname != null ?
                new ObjectParameter("categoryname", categoryname) :
                new ObjectParameter("categoryname", typeof(string));
    
            var sdateParameter = sdate.HasValue ?
                new ObjectParameter("sdate", sdate) :
                new ObjectParameter("sdate", typeof(System.DateTime));
    
            var edateParameter = edate.HasValue ?
                new ObjectParameter("edate", edate) :
                new ObjectParameter("edate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("GetSumSaleMoney", itemnameParameter, categorynameParameter, sdateParameter, edateParameter);
        }
    
        public virtual int ClearCategorySyncFlag()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ClearCategorySyncFlag");
        }
    
        public virtual int ClearCrudeSyncFlag()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ClearCrudeSyncFlag");
        }
    
        public virtual int ClearMemberSyncFlag()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ClearMemberSyncFlag");
        }
    
        public virtual int ClearSysRoleSyncFlag()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ClearSysRoleSyncFlag");
        }
    
        public virtual int ClearTablePosSyncFlag()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ClearTablePosSyncFlag");
        }
    }
}
