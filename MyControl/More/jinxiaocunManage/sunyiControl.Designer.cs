namespace DianDianClient.MyControl.More.jinxiaocunManage
{
    partial class sunyiControl
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
            this.createdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.crudename = new DevExpress.XtraGrid.Columns.GridColumn();
            this.person = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.unit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.num = new DevExpress.XtraGrid.Columns.GridColumn();
            this.type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mgncPager1 = new DianDianClient.MyControl.FoodManagement.MgncPager();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.de_stime = new DevExpress.XtraEditors.DateEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.de_etime = new DevExpress.XtraEditors.DateEdit();
            this.btn_search = new DevExpress.XtraEditors.SimpleButton();
            this.btn_add = new DevExpress.XtraEditors.SimpleButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.Txt_yuanliaoname = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_shangpinName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_yuanliaoType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_shangpinType = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.de_stime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_stime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_etime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_etime.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_yuanliaoname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_shangpinName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_yuanliaoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_shangpinType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // createdate
            // 
            this.createdate.Caption = "创建时间";
            this.createdate.FieldName = "createdate";
            this.createdate.Name = "createdate";
            this.createdate.Visible = true;
            this.createdate.VisibleIndex = 2;
            // 
            // crudename
            // 
            this.crudename.Caption = "商品名";
            this.crudename.FieldName = "crudename";
            this.crudename.Name = "crudename";
            this.crudename.Visible = true;
            this.crudename.VisibleIndex = 1;
            // 
            // person
            // 
            this.person.Caption = "登记人";
            this.person.FieldName = "person";
            this.person.Name = "person";
            this.person.Visible = true;
            this.person.VisibleIndex = 0;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.createdate,
            this.person,
            this.crudename,
            this.unit,
            this.num,
            this.type,
            this.reason});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView1_CustomColumnDisplayText);
            // 
            // unit
            // 
            this.unit.Caption = "单位";
            this.unit.FieldName = "unit";
            this.unit.Name = "unit";
            this.unit.Visible = true;
            this.unit.VisibleIndex = 3;
            // 
            // num
            // 
            this.num.Caption = "数量";
            this.num.FieldName = "num";
            this.num.Name = "num";
            this.num.Visible = true;
            this.num.VisibleIndex = 4;
            // 
            // type
            // 
            this.type.Caption = "类型";
            this.type.FieldName = "type";
            this.type.Name = "type";
            this.type.Visible = true;
            this.type.VisibleIndex = 5;
            // 
            // reason
            // 
            this.reason.Caption = "原因";
            this.reason.FieldName = "reason";
            this.reason.Name = "reason";
            this.reason.Visible = true;
            this.reason.VisibleIndex = 6;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 87);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1145, 412);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mgncPager1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1151, 542);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // mgncPager1
            // 
            this.mgncPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mgncPager1.Location = new System.Drawing.Point(3, 505);
            this.mgncPager1.Name = "mgncPager1";
            this.mgncPager1.Size = new System.Drawing.Size(1145, 34);
            this.mgncPager1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1145, 78);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Controls.Add(this.de_stime);
            this.flowLayoutPanel2.Controls.Add(this.label5);
            this.flowLayoutPanel2.Controls.Add(this.de_etime);
            this.flowLayoutPanel2.Controls.Add(this.btn_search);
            this.flowLayoutPanel2.Controls.Add(this.btn_add);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 42);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1139, 33);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 21);
            this.label4.TabIndex = 19;
            this.label4.Text = "登记日期：";
            // 
            // de_stime
            // 
            this.de_stime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.de_stime.EditValue = null;
            this.de_stime.Location = new System.Drawing.Point(99, 4);
            this.de_stime.Name = "de_stime";
            this.de_stime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.de_stime.Properties.Appearance.Options.UseFont = true;
            this.de_stime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_stime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_stime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.de_stime.Properties.EditFormat.FormatString = "";
            this.de_stime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.de_stime.Properties.Mask.EditMask = "";
            this.de_stime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.de_stime.Size = new System.Drawing.Size(181, 26);
            this.de_stime.TabIndex = 23;
            this.de_stime.EditValueChanged += new System.EventHandler(this.de_stime_EditValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(286, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 21);
            this.label5.TabIndex = 21;
            this.label5.Text = "-";
            // 
            // de_etime
            // 
            this.de_etime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.de_etime.EditValue = null;
            this.de_etime.Location = new System.Drawing.Point(309, 4);
            this.de_etime.Name = "de_etime";
            this.de_etime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.de_etime.Properties.Appearance.Options.UseFont = true;
            this.de_etime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_etime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.de_etime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.de_etime.Properties.EditFormat.FormatString = "";
            this.de_etime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.de_etime.Properties.Mask.EditMask = "";
            this.de_etime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.de_etime.Size = new System.Drawing.Size(181, 26);
            this.de_etime.TabIndex = 24;
            this.de_etime.EditValueChanged += new System.EventHandler(this.de_etime_EditValueChanged);
            // 
            // btn_search
            // 
            this.btn_search.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_search.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.btn_search.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_search.Appearance.Options.UseBackColor = true;
            this.btn_search.Appearance.Options.UseFont = true;
            this.btn_search.Appearance.Options.UseForeColor = true;
            this.btn_search.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_search.Location = new System.Drawing.Point(496, 3);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(106, 28);
            this.btn_search.TabIndex = 25;
            this.btn_search.Text = "查询";
            this.btn_search.Click += new System.EventHandler(this.Btn_query_Click);
            // 
            // btn_add
            // 
            this.btn_add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_add.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.btn_add.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_add.Appearance.Options.UseBackColor = true;
            this.btn_add.Appearance.Options.UseFont = true;
            this.btn_add.Appearance.Options.UseForeColor = true;
            this.btn_add.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_add.Location = new System.Drawing.Point(608, 3);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(106, 28);
            this.btn_add.TabIndex = 26;
            this.btn_add.Text = "添加";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label7);
            this.flowLayoutPanel1.Controls.Add(this.Txt_yuanliaoname);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.txt_shangpinName);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.cbo_yuanliaoType);
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.txt_shangpinType);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1139, 33);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 21);
            this.label7.TabIndex = 2;
            this.label7.Text = "原料名：";
            // 
            // Txt_yuanliaoname
            // 
            this.Txt_yuanliaoname.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Txt_yuanliaoname.Location = new System.Drawing.Point(83, 3);
            this.Txt_yuanliaoname.Name = "Txt_yuanliaoname";
            this.Txt_yuanliaoname.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_yuanliaoname.Properties.Appearance.Options.UseFont = true;
            this.Txt_yuanliaoname.Size = new System.Drawing.Size(177, 26);
            this.Txt_yuanliaoname.TabIndex = 18;
            this.Txt_yuanliaoname.EditValueChanged += new System.EventHandler(this.Txt_yuanliaoname_EditValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(266, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "商品名：";
            // 
            // txt_shangpinName
            // 
            this.txt_shangpinName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_shangpinName.Location = new System.Drawing.Point(346, 3);
            this.txt_shangpinName.Name = "txt_shangpinName";
            this.txt_shangpinName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_shangpinName.Properties.Appearance.Options.UseFont = true;
            this.txt_shangpinName.Size = new System.Drawing.Size(177, 26);
            this.txt_shangpinName.TabIndex = 27;
            this.txt_shangpinName.EditValueChanged += new System.EventHandler(this.txt_shangpinName_EditValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(529, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 21;
            this.label2.Text = "原料分类：";
            // 
            // cbo_yuanliaoType
            // 
            this.cbo_yuanliaoType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbo_yuanliaoType.Location = new System.Drawing.Point(625, 3);
            this.cbo_yuanliaoType.Name = "cbo_yuanliaoType";
            this.cbo_yuanliaoType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_yuanliaoType.Properties.Appearance.Options.UseFont = true;
            this.cbo_yuanliaoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbo_yuanliaoType.Size = new System.Drawing.Size(162, 26);
            this.cbo_yuanliaoType.TabIndex = 28;
            this.cbo_yuanliaoType.SelectedIndexChanged += new System.EventHandler(this.cbo_yuanliaoType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(793, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 21);
            this.label6.TabIndex = 25;
            this.label6.Text = "商品分类：";
            // 
            // txt_shangpinType
            // 
            this.txt_shangpinType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_shangpinType.Location = new System.Drawing.Point(889, 3);
            this.txt_shangpinType.Name = "txt_shangpinType";
            this.txt_shangpinType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_shangpinType.Properties.Appearance.Options.UseFont = true;
            this.txt_shangpinType.Size = new System.Drawing.Size(177, 26);
            this.txt_shangpinType.TabIndex = 26;
            this.txt_shangpinType.EditValueChanged += new System.EventHandler(this.txt_shangpinType_EditValueChanged);
            // 
            // sunyiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "sunyiControl";
            this.Size = new System.Drawing.Size(1151, 542);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.de_stime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_stime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_etime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.de_etime.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_yuanliaoname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_shangpinName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbo_yuanliaoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_shangpinType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn createdate;
        private DevExpress.XtraGrid.Columns.GridColumn crudename;
        private DevExpress.XtraGrid.Columns.GridColumn person;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn unit;
        private DevExpress.XtraGrid.Columns.GridColumn num;
        private DevExpress.XtraGrid.Columns.GridColumn type;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private FoodManagement.MgncPager mgncPager1;
        private DevExpress.XtraGrid.Columns.GridColumn reason;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraEditors.TextEdit Txt_yuanliaoname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txt_shangpinType;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.DateEdit de_stime;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.DateEdit de_etime;
        private DevExpress.XtraEditors.SimpleButton btn_search;
        private DevExpress.XtraEditors.SimpleButton btn_add;
        private DevExpress.XtraEditors.TextEdit txt_shangpinName;
        private DevExpress.XtraEditors.ComboBoxEdit cbo_yuanliaoType;
    }
}
