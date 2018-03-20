namespace DianDianClient.MyControl.MemberManagement
{
    partial class ProtocolCustomerControl
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
            DevExpress.XtraGrid.GridFormatRule gridFormatRule1 = new DevExpress.XtraGrid.GridFormatRule();
            DevExpress.XtraEditors.FormatConditionRuleValue formatConditionRuleValue1 = new DevExpress.XtraEditors.FormatConditionRuleValue();
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
            this.state = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.addtime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.telno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.maxusenums = new DevExpress.XtraGrid.Columns.GridColumn();
            this.maxprice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.signnums = new DevExpress.XtraGrid.Columns.GridColumn();
            this.signmoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.operate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
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
            this.Btn_query = new DevExpress.XtraEditors.SimpleButton();
            this.mgncPager1 = new DianDianClient.MyControl.FoodManagement.MgncPager();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
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
            // state
            // 
            this.state.Caption = "状态";
            this.state.ColumnEdit = this.repositoryItemRadioGroup1;
            this.state.FieldName = "state";
            this.state.Name = "state";
            this.state.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.state.Visible = true;
            this.state.VisibleIndex = 7;
            this.state.Width = 137;
            // 
            // repositoryItemRadioGroup1
            // 
            this.repositoryItemRadioGroup1.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "冻结"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "正常")});
            this.repositoryItemRadioGroup1.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
            this.repositoryItemRadioGroup1.EditValueChanged += new System.EventHandler(this.repositoryItemRadioGroup1_EditValueChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mgncPager1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1047, 502);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 49);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.repositoryItemRadioGroup1});
            this.gridControl1.Size = new System.Drawing.Size(1041, 409);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.id,
            this.addtime,
            this.name,
            this.telno,
            this.maxusenums,
            this.maxprice,
            this.signnums,
            this.signmoney,
            this.state,
            this.operate});
            gridFormatRule1.Column = this.state;
            gridFormatRule1.Name = "Format0";
            gridFormatRule1.Rule = formatConditionRuleValue1;
            this.gridView1.FormatRules.Add(gridFormatRule1);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // id
            // 
            this.id.Caption = "id";
            this.id.FieldName = "id";
            this.id.Name = "id";
            // 
            // addtime
            // 
            this.addtime.Caption = "日期";
            this.addtime.FieldName = "addtime";
            this.addtime.Name = "addtime";
            this.addtime.Visible = true;
            this.addtime.VisibleIndex = 0;
            this.addtime.Width = 118;
            // 
            // name
            // 
            this.name.Caption = "姓名";
            this.name.FieldName = "name";
            this.name.Name = "name";
            this.name.Visible = true;
            this.name.VisibleIndex = 1;
            this.name.Width = 118;
            // 
            // telno
            // 
            this.telno.Caption = "电话";
            this.telno.FieldName = "telno";
            this.telno.Name = "telno";
            this.telno.Visible = true;
            this.telno.VisibleIndex = 2;
            this.telno.Width = 118;
            // 
            // maxusenums
            // 
            this.maxusenums.Caption = "次数上限";
            this.maxusenums.FieldName = "maxusenums";
            this.maxusenums.Name = "maxusenums";
            this.maxusenums.Visible = true;
            this.maxusenums.VisibleIndex = 3;
            this.maxusenums.Width = 118;
            // 
            // maxprice
            // 
            this.maxprice.Caption = "金额上限";
            this.maxprice.FieldName = "maxprice";
            this.maxprice.Name = "maxprice";
            this.maxprice.Visible = true;
            this.maxprice.VisibleIndex = 4;
            this.maxprice.Width = 118;
            // 
            // signnums
            // 
            this.signnums.Caption = "签单次数";
            this.signnums.FieldName = "signnums";
            this.signnums.Name = "signnums";
            this.signnums.Visible = true;
            this.signnums.VisibleIndex = 5;
            this.signnums.Width = 79;
            // 
            // signmoney
            // 
            this.signmoney.Caption = "签单总额";
            this.signmoney.FieldName = "signmoney";
            this.signmoney.Name = "signmoney";
            this.signmoney.Visible = true;
            this.signmoney.VisibleIndex = 6;
            this.signmoney.Width = 76;
            // 
            // operate
            // 
            this.operate.Caption = "操作";
            this.operate.ColumnEdit = this.repositoryItemButtonEdit1;
            this.operate.FieldName = "state";
            this.operate.Name = "operate";
            this.operate.Visible = true;
            this.operate.VisibleIndex = 8;
            this.operate.Width = 141;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "修改", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "清帐", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
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
            this.flowLayoutPanel1.Controls.Add(this.Btn_query);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1041, 40);
            this.flowLayoutPanel1.TabIndex = 3;
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
            this.dt_stime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
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
            this.dt_etime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
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
            this.simpleButton1.Text = "添加客户";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // Btn_query
            // 
            this.Btn_query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_query.Appearance.Font = new System.Drawing.Font("微软雅黑", 15.75F);
            this.Btn_query.Appearance.Options.UseFont = true;
            this.Btn_query.Location = new System.Drawing.Point(926, 3);
            this.Btn_query.Name = "Btn_query";
            this.Btn_query.Size = new System.Drawing.Size(75, 34);
            this.Btn_query.TabIndex = 10;
            this.Btn_query.Text = "查询";
            this.Btn_query.Click += new System.EventHandler(this.Btn_query_Click);
            // 
            // mgncPager1
            // 
            this.mgncPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mgncPager1.Location = new System.Drawing.Point(3, 464);
            this.mgncPager1.Name = "mgncPager1";
            this.mgncPager1.Size = new System.Drawing.Size(1041, 35);
            this.mgncPager1.TabIndex = 4;
            // 
            // ProtocolCustomerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProtocolCustomerControl";
            this.Size = new System.Drawing.Size(1047, 502);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn id;
        private DevExpress.XtraGrid.Columns.GridColumn addtime;
        private DevExpress.XtraGrid.Columns.GridColumn name;
        private DevExpress.XtraGrid.Columns.GridColumn telno;
        private DevExpress.XtraGrid.Columns.GridColumn maxusenums;
        private DevExpress.XtraGrid.Columns.GridColumn maxprice;
        private DevExpress.XtraGrid.Columns.GridColumn signnums;
        private DevExpress.XtraGrid.Columns.GridColumn signmoney;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
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
        private DevExpress.XtraEditors.SimpleButton Btn_query;
        private FoodManagement.MgncPager mgncPager1;
        private DevExpress.XtraGrid.Columns.GridColumn state;
        private DevExpress.XtraGrid.Columns.GridColumn operate;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
    }
}
