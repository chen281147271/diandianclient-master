namespace DianDianClient.MyControl.MemberManagement
{
    partial class MemberQueryControl
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.addtime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.isvalid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.realname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.telno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cardno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.money = new DevExpress.XtraGrid.Columns.GridColumn();
            this.birthday = new DevExpress.XtraGrid.Columns.GridColumn();
            this.expirydate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.cardid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mgncPager1 = new DianDianClient.MyControl.FoodManagement.MgncPager();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_name = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_phone = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.dt_stime = new DevExpress.XtraEditors.DateEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.dt_etime = new DevExpress.XtraEditors.DateEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_query = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_phone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_stime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_stime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_etime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_etime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mgncPager1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1130, 487);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 47);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1124, 390);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.addtime,
            this.isvalid,
            this.realname,
            this.telno,
            this.cardno,
            this.money,
            this.birthday,
            this.expirydate,
            this.gridColumn1,
            this.cardid});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // addtime
            // 
            this.addtime.AppearanceCell.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addtime.AppearanceCell.Options.UseFont = true;
            this.addtime.AppearanceHeader.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addtime.AppearanceHeader.Options.UseFont = true;
            this.addtime.Caption = "添加日期";
            this.addtime.FieldName = "addtime";
            this.addtime.Name = "addtime";
            this.addtime.Visible = true;
            this.addtime.VisibleIndex = 0;
            // 
            // isvalid
            // 
            this.isvalid.Caption = "是否有效";
            this.isvalid.FieldName = "isvalid";
            this.isvalid.Name = "isvalid";
            this.isvalid.Visible = true;
            this.isvalid.VisibleIndex = 1;
            // 
            // realname
            // 
            this.realname.Caption = "姓名";
            this.realname.FieldName = "realname";
            this.realname.Name = "realname";
            this.realname.Visible = true;
            this.realname.VisibleIndex = 2;
            // 
            // telno
            // 
            this.telno.Caption = "电话";
            this.telno.FieldName = "telno";
            this.telno.Name = "telno";
            this.telno.Visible = true;
            this.telno.VisibleIndex = 3;
            // 
            // cardno
            // 
            this.cardno.Caption = "会员卡号";
            this.cardno.FieldName = "cardno";
            this.cardno.Name = "cardno";
            this.cardno.Visible = true;
            this.cardno.VisibleIndex = 4;
            // 
            // money
            // 
            this.money.Caption = "余额";
            this.money.FieldName = "money";
            this.money.Name = "money";
            this.money.Visible = true;
            this.money.VisibleIndex = 5;
            // 
            // birthday
            // 
            this.birthday.Caption = "生日";
            this.birthday.FieldName = "birthday";
            this.birthday.Name = "birthday";
            // 
            // expirydate
            // 
            this.expirydate.Caption = "失效日期";
            this.expirydate.FieldName = "expirydate";
            this.expirydate.Name = "expirydate";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "操作";
            this.gridColumn1.ColumnEdit = this.repositoryItemButtonEdit1;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "会员详情", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "余额充值", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // cardid
            // 
            this.cardid.Caption = "cardid";
            this.cardid.FieldName = "cardid";
            this.cardid.Name = "cardid";
            // 
            // mgncPager1
            // 
            this.mgncPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mgncPager1.Location = new System.Drawing.Point(3, 443);
            this.mgncPager1.Name = "mgncPager1";
            this.mgncPager1.Size = new System.Drawing.Size(1124, 41);
            this.mgncPager1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.Txt_name);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.Txt_phone);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.dt_stime);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.dt_etime);
            this.flowLayoutPanel1.Controls.Add(this.simpleButton1);
            this.flowLayoutPanel1.Controls.Add(this.simpleButton2);
            this.flowLayoutPanel1.Controls.Add(this.Btn_query);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1124, 38);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "姓名：";
            // 
            // Txt_name
            // 
            this.Txt_name.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Txt_name.Location = new System.Drawing.Point(84, 3);
            this.Txt_name.Name = "Txt_name";
            this.Txt_name.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.Txt_name.Properties.Appearance.Options.UseFont = true;
            this.Txt_name.Size = new System.Drawing.Size(98, 34);
            this.Txt_name.TabIndex = 1;
            this.Txt_name.EditValueChanged += new System.EventHandler(this.Txt_name_EditValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.label2.Location = new System.Drawing.Point(188, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "电话：";
            // 
            // Txt_phone
            // 
            this.Txt_phone.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Txt_phone.Location = new System.Drawing.Point(269, 3);
            this.Txt_phone.Name = "Txt_phone";
            this.Txt_phone.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.Txt_phone.Properties.Appearance.Options.UseFont = true;
            this.Txt_phone.Size = new System.Drawing.Size(98, 34);
            this.Txt_phone.TabIndex = 3;
            this.Txt_phone.EditValueChanged += new System.EventHandler(this.Txt_phone_EditValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.label3.Location = new System.Drawing.Point(373, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 28);
            this.label3.TabIndex = 4;
            this.label3.Text = "注册日期：";
            // 
            // dt_stime
            // 
            this.dt_stime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dt_stime.EditValue = null;
            this.dt_stime.Location = new System.Drawing.Point(496, 3);
            this.dt_stime.Name = "dt_stime";
            this.dt_stime.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.dt_stime.Properties.Appearance.Options.UseFont = true;
            this.dt_stime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_stime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_stime.Size = new System.Drawing.Size(133, 34);
            this.dt_stime.TabIndex = 5;
            this.dt_stime.EditValueChanged += new System.EventHandler(this.dt_stime_EditValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.label4.Location = new System.Drawing.Point(635, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 28);
            this.label4.TabIndex = 6;
            this.label4.Text = "到";
            // 
            // dt_etime
            // 
            this.dt_etime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dt_etime.EditValue = null;
            this.dt_etime.Location = new System.Drawing.Point(674, 3);
            this.dt_etime.Name = "dt_etime";
            this.dt_etime.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.dt_etime.Properties.Appearance.Options.UseFont = true;
            this.dt_etime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_etime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_etime.Size = new System.Drawing.Size(133, 34);
            this.dt_etime.TabIndex = 7;
            this.dt_etime.EditValueChanged += new System.EventHandler(this.dt_etime_EditValueChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(813, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(107, 31);
            this.simpleButton1.TabIndex = 8;
            this.simpleButton1.Text = "添加会员";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(926, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(106, 32);
            this.simpleButton2.TabIndex = 9;
            this.simpleButton2.Text = "添加规则";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // Btn_query
            // 
            this.Btn_query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_query.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.Btn_query.Appearance.Options.UseFont = true;
            this.Btn_query.Location = new System.Drawing.Point(1038, 3);
            this.Btn_query.Name = "Btn_query";
            this.Btn_query.Size = new System.Drawing.Size(75, 34);
            this.Btn_query.TabIndex = 10;
            this.Btn_query.Text = "查询";
            this.Btn_query.Click += new System.EventHandler(this.Btn_query_Click);
            // 
            // MemberQueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MemberQueryControl";
            this.Size = new System.Drawing.Size(1130, 487);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_phone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_stime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_stime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_etime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_etime.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn addtime;
        private DevExpress.XtraGrid.Columns.GridColumn isvalid;
        private DevExpress.XtraGrid.Columns.GridColumn realname;
        private DevExpress.XtraGrid.Columns.GridColumn telno;
        private DevExpress.XtraGrid.Columns.GridColumn cardno;
        private DevExpress.XtraGrid.Columns.GridColumn money;
        private DevExpress.XtraGrid.Columns.GridColumn birthday;
        private DevExpress.XtraGrid.Columns.GridColumn expirydate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn cardid;
        private FoodManagement.MgncPager mgncPager1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit Txt_name;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit Txt_phone;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.DateEdit dt_stime;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.DateEdit dt_etime;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton Btn_query;
    }
}
