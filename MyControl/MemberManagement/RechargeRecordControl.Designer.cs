namespace DianDianClient.MyControl.MemberManagement
{
    partial class RechargeRecordControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.usetime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.shopkey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.consume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dt_stime = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.dt_etime = new DevExpress.XtraEditors.DateEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.Btn_query = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_return = new DevExpress.XtraEditors.SimpleButton();
            this.mgncPager1 = new DianDianClient.MyControl.FoodManagement.MgncPager();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_stime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_stime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_etime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_etime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 310);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 30);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1084, 237);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.usetime,
            this.shopkey,
            this.consume,
            this.type});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // usetime
            // 
            this.usetime.Caption = "消费时间";
            this.usetime.FieldName = "usetime";
            this.usetime.Name = "usetime";
            this.usetime.Visible = true;
            this.usetime.VisibleIndex = 0;
            // 
            // shopkey
            // 
            this.shopkey.Caption = "消费商铺";
            this.shopkey.FieldName = "shopkey";
            this.shopkey.Name = "shopkey";
            this.shopkey.Visible = true;
            this.shopkey.VisibleIndex = 1;
            // 
            // consume
            // 
            this.consume.Caption = "金额";
            this.consume.FieldName = "consume";
            this.consume.Name = "consume";
            this.consume.Visible = true;
            this.consume.VisibleIndex = 2;
            // 
            // type
            // 
            this.type.Caption = "消费/充值";
            this.type.FieldName = "type";
            this.type.Name = "type";
            this.type.Visible = true;
            this.type.VisibleIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.dt_stime);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.dt_etime);
            this.flowLayoutPanel1.Controls.Add(this.radioGroup1);
            this.flowLayoutPanel1.Controls.Add(this.Btn_query);
            this.flowLayoutPanel1.Controls.Add(this.Btn_return);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1084, 21);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "账单日期";
            // 
            // dt_stime
            // 
            this.dt_stime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dt_stime.EditValue = null;
            this.dt_stime.Location = new System.Drawing.Point(83, 6);
            this.dt_stime.Name = "dt_stime";
            this.dt_stime.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dt_stime.Properties.Appearance.Options.UseFont = true;
            this.dt_stime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_stime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_stime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dt_stime.Size = new System.Drawing.Size(172, 28);
            this.dt_stime.TabIndex = 1;
            this.dt_stime.EditValueChanged += new System.EventHandler(this.dateEdit1_EditValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(261, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "到";
            // 
            // dt_etime
            // 
            this.dt_etime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dt_etime.EditValue = null;
            this.dt_etime.Location = new System.Drawing.Point(293, 6);
            this.dt_etime.Name = "dt_etime";
            this.dt_etime.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dt_etime.Properties.Appearance.Options.UseFont = true;
            this.dt_etime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_etime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dt_etime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dt_etime.Size = new System.Drawing.Size(172, 28);
            this.dt_etime.TabIndex = 3;
            this.dt_etime.EditValueChanged += new System.EventHandler(this.dateEdit2_EditValueChanged);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioGroup1.Location = new System.Drawing.Point(471, 5);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "全部"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "充值"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "消费")});
            this.radioGroup1.Size = new System.Drawing.Size(158, 31);
            this.radioGroup1.TabIndex = 4;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // Btn_query
            // 
            this.Btn_query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_query.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.Btn_query.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_query.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_query.Appearance.Options.UseBackColor = true;
            this.Btn_query.Appearance.Options.UseFont = true;
            this.Btn_query.Appearance.Options.UseForeColor = true;
            this.Btn_query.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Btn_query.Location = new System.Drawing.Point(635, 3);
            this.Btn_query.Name = "Btn_query";
            this.Btn_query.Size = new System.Drawing.Size(113, 35);
            this.Btn_query.TabIndex = 13;
            this.Btn_query.Text = "查询";
            this.Btn_query.Click += new System.EventHandler(this.Btn_query_Click);
            // 
            // Btn_return
            // 
            this.Btn_return.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_return.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.Btn_return.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_return.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_return.Appearance.Options.UseBackColor = true;
            this.Btn_return.Appearance.Options.UseFont = true;
            this.Btn_return.Appearance.Options.UseForeColor = true;
            this.Btn_return.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Btn_return.Location = new System.Drawing.Point(754, 3);
            this.Btn_return.Name = "Btn_return";
            this.Btn_return.Size = new System.Drawing.Size(113, 35);
            this.Btn_return.TabIndex = 14;
            this.Btn_return.Text = "返回";
            this.Btn_return.Click += new System.EventHandler(this.Btn_Return_Click);
            // 
            // mgncPager1
            // 
            this.mgncPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mgncPager1.Location = new System.Drawing.Point(3, 273);
            this.mgncPager1.Name = "mgncPager1";
            this.mgncPager1.Size = new System.Drawing.Size(1084, 34);
            this.mgncPager1.TabIndex = 3;
            // 
            // RechargeRecordControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RechargeRecordControl";
            this.Size = new System.Drawing.Size(1090, 310);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_stime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_stime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_etime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_etime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraGrid.Columns.GridColumn usetime;
        private DevExpress.XtraGrid.Columns.GridColumn shopkey;
        private DevExpress.XtraGrid.Columns.GridColumn consume;
        private DevExpress.XtraGrid.Columns.GridColumn type;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dt_stime;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dt_etime;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.SimpleButton Btn_query;
        private DevExpress.XtraEditors.SimpleButton Btn_return;
        private FoodManagement.MgncPager mgncPager1;
    }
}
