namespace DianDianClient.MyControl.More.StaffManage
{
    partial class ZhiWeiEidtControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Txt_rolename = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chk_shanghu = new DevExpress.XtraEditors.CheckEdit();
            this.chk_yingye = new DevExpress.XtraEditors.CheckEdit();
            this.chk_huiyuan = new DevExpress.XtraEditors.CheckEdit();
            this.chk_caiping = new DevExpress.XtraEditors.CheckEdit();
            this.chk_zhuowei = new DevExpress.XtraEditors.CheckEdit();
            this.chk_yuangong = new DevExpress.XtraEditors.CheckEdit();
            this.chk_canzhuo = new DevExpress.XtraEditors.CheckEdit();
            this.chk_dingdan = new DevExpress.XtraEditors.CheckEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_rolename.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_shanghu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_yingye.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_huiyuan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_caiping.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_zhuowei.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_yuangong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_canzhuo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_dingdan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.Txt_rolename, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_save, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(460, 451);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Txt_rolename
            // 
            this.Txt_rolename.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Txt_rolename.Location = new System.Drawing.Point(141, 28);
            this.Txt_rolename.Name = "Txt_rolename";
            this.Txt_rolename.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_rolename.Properties.Appearance.Options.UseFont = true;
            this.Txt_rolename.Size = new System.Drawing.Size(223, 26);
            this.Txt_rolename.TabIndex = 21;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "不能为空";
            this.dxValidationProvider1.SetValidationRule(this.Txt_rolename, conditionValidationRule1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(61, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "职位名称";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.chk_shanghu, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.chk_yingye, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.chk_huiyuan, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.chk_caiping, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.chk_zhuowei, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.chk_yuangong, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.chk_canzhuo, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.chk_dingdan, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(141, 85);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(316, 322);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // chk_shanghu
            // 
            this.chk_shanghu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_shanghu.Location = new System.Drawing.Point(186, 269);
            this.chk_shanghu.Name = "chk_shanghu";
            this.chk_shanghu.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_shanghu.Properties.Appearance.Options.UseFont = true;
            this.chk_shanghu.Properties.Caption = "商户设置";
            this.chk_shanghu.Size = new System.Drawing.Size(102, 23);
            this.chk_shanghu.TabIndex = 7;
            // 
            // chk_yingye
            // 
            this.chk_yingye.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_yingye.Location = new System.Drawing.Point(28, 269);
            this.chk_yingye.Name = "chk_yingye";
            this.chk_yingye.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_yingye.Properties.Appearance.Options.UseFont = true;
            this.chk_yingye.Properties.Caption = "营业详情";
            this.chk_yingye.Size = new System.Drawing.Size(102, 23);
            this.chk_yingye.TabIndex = 6;
            // 
            // chk_huiyuan
            // 
            this.chk_huiyuan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_huiyuan.Location = new System.Drawing.Point(186, 188);
            this.chk_huiyuan.Name = "chk_huiyuan";
            this.chk_huiyuan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_huiyuan.Properties.Appearance.Options.UseFont = true;
            this.chk_huiyuan.Properties.Caption = "会员管理";
            this.chk_huiyuan.Size = new System.Drawing.Size(102, 23);
            this.chk_huiyuan.TabIndex = 5;
            // 
            // chk_caiping
            // 
            this.chk_caiping.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_caiping.Location = new System.Drawing.Point(28, 188);
            this.chk_caiping.Name = "chk_caiping";
            this.chk_caiping.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_caiping.Properties.Appearance.Options.UseFont = true;
            this.chk_caiping.Properties.Caption = "菜品管理";
            this.chk_caiping.Size = new System.Drawing.Size(102, 23);
            this.chk_caiping.TabIndex = 4;
            // 
            // chk_zhuowei
            // 
            this.chk_zhuowei.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_zhuowei.Location = new System.Drawing.Point(186, 108);
            this.chk_zhuowei.Name = "chk_zhuowei";
            this.chk_zhuowei.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_zhuowei.Properties.Appearance.Options.UseFont = true;
            this.chk_zhuowei.Properties.Caption = "桌位结算";
            this.chk_zhuowei.Size = new System.Drawing.Size(102, 23);
            this.chk_zhuowei.TabIndex = 3;
            // 
            // chk_yuangong
            // 
            this.chk_yuangong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_yuangong.Location = new System.Drawing.Point(28, 108);
            this.chk_yuangong.Name = "chk_yuangong";
            this.chk_yuangong.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_yuangong.Properties.Appearance.Options.UseFont = true;
            this.chk_yuangong.Properties.Caption = "员工管理";
            this.chk_yuangong.Size = new System.Drawing.Size(102, 23);
            this.chk_yuangong.TabIndex = 2;
            // 
            // chk_canzhuo
            // 
            this.chk_canzhuo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_canzhuo.Location = new System.Drawing.Point(186, 28);
            this.chk_canzhuo.Name = "chk_canzhuo";
            this.chk_canzhuo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_canzhuo.Properties.Appearance.Options.UseFont = true;
            this.chk_canzhuo.Properties.Caption = "餐桌管理";
            this.chk_canzhuo.Size = new System.Drawing.Size(102, 23);
            this.chk_canzhuo.TabIndex = 1;
            // 
            // chk_dingdan
            // 
            this.chk_dingdan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chk_dingdan.Location = new System.Drawing.Point(28, 28);
            this.chk_dingdan.Name = "chk_dingdan";
            this.chk_dingdan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_dingdan.Properties.Appearance.Options.UseFont = true;
            this.chk_dingdan.Properties.Caption = "订单管理";
            this.chk_dingdan.Size = new System.Drawing.Size(102, 23);
            this.chk_dingdan.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(61, 235);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "功能权限";
            // 
            // btn_save
            // 
            this.btn_save.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.btn_save.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_save.Appearance.Options.UseBackColor = true;
            this.btn_save.Appearance.Options.UseFont = true;
            this.btn_save.Appearance.Options.UseForeColor = true;
            this.btn_save.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tableLayoutPanel1.SetColumnSpan(this.btn_save, 2);
            this.btn_save.Location = new System.Drawing.Point(177, 416);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(106, 29);
            this.btn_save.TabIndex = 22;
            this.btn_save.Text = "保存";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // ZhiWeiEidtControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ZhiWeiEidtControl";
            this.Size = new System.Drawing.Size(460, 451);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_rolename.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chk_shanghu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_yingye.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_huiyuan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_caiping.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_zhuowei.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_yuangong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_canzhuo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chk_dingdan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit Txt_rolename;
        private DevExpress.XtraEditors.CheckEdit chk_shanghu;
        private DevExpress.XtraEditors.CheckEdit chk_yingye;
        private DevExpress.XtraEditors.CheckEdit chk_huiyuan;
        private DevExpress.XtraEditors.CheckEdit chk_caiping;
        private DevExpress.XtraEditors.CheckEdit chk_zhuowei;
        private DevExpress.XtraEditors.CheckEdit chk_yuangong;
        private DevExpress.XtraEditors.CheckEdit chk_canzhuo;
        private DevExpress.XtraEditors.CheckEdit chk_dingdan;
        private DevExpress.XtraEditors.SimpleButton btn_save;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
    }
}
