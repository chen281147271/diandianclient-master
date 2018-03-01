namespace DianDianClient.MyControl.BusinessDetails
{
    partial class FoodStatisticsControl
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
            this.MaterialName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaterialType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaterialNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mgncPager1 = new DianDianClient.MyControl.FoodManagement.MgncPager();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.toggleSwitch2 = new DevExpress.XtraEditors.ToggleSwitch();
            this.toggleSwitch3 = new DevExpress.XtraEditors.ToggleSwitch();
            this.toggleSwitch4 = new DevExpress.XtraEditors.ToggleSwitch();
            this.toggleSwitch5 = new DevExpress.XtraEditors.ToggleSwitch();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch5.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(950, 381);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(3, 53);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(944, 285);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MaterialName,
            this.MaterialType,
            this.MaterialNumber,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // MaterialName
            // 
            this.MaterialName.Caption = "活动名称";
            this.MaterialName.FieldName = "ActivityName";
            this.MaterialName.Name = "MaterialName";
            this.MaterialName.Visible = true;
            this.MaterialName.VisibleIndex = 0;
            // 
            // MaterialType
            // 
            this.MaterialType.Caption = "发放量";
            this.MaterialType.FieldName = "GrantCount";
            this.MaterialType.Name = "MaterialType";
            this.MaterialType.Visible = true;
            this.MaterialType.VisibleIndex = 1;
            // 
            // MaterialNumber
            // 
            this.MaterialNumber.Caption = "使用量";
            this.MaterialNumber.FieldName = "UesCount";
            this.MaterialNumber.Name = "MaterialNumber";
            this.MaterialNumber.Visible = true;
            this.MaterialNumber.VisibleIndex = 2;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "使用率";
            this.gridColumn1.FieldName = "Usage rate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 3;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "创建时间";
            this.gridColumn2.FieldName = "CreatTime";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "截止时间";
            this.gridColumn3.FieldName = "Deadline";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 5;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "活动规则";
            this.gridColumn4.FieldName = "Rules ";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            // 
            // mgncPager1
            // 
            this.mgncPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mgncPager1.Location = new System.Drawing.Point(3, 344);
            this.mgncPager1.Name = "mgncPager1";
            this.mgncPager1.Size = new System.Drawing.Size(944, 34);
            this.mgncPager1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(944, 44);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.dateEdit1);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.dateEdit2);
            this.flowLayoutPanel1.Controls.Add(this.toggleSwitch1);
            this.flowLayoutPanel1.Controls.Add(this.toggleSwitch2);
            this.flowLayoutPanel1.Controls.Add(this.toggleSwitch3);
            this.flowLayoutPanel1.Controls.Add(this.toggleSwitch4);
            this.flowLayoutPanel1.Controls.Add(this.toggleSwitch5);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(121, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(700, 34);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "查询日期：";
            // 
            // dateEdit1
            // 
            this.dateEdit1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(99, 3);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateEdit1.Properties.Appearance.Options.UseFont = true;
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEdit1.Properties.EditFormat.FormatString = "";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit1.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Custom;
            this.dateEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEdit1.Size = new System.Drawing.Size(181, 28);
            this.dateEdit1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(286, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "-";
            // 
            // dateEdit2
            // 
            this.dateEdit2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(309, 3);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateEdit2.Properties.Appearance.Options.UseFont = true;
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEdit2.Properties.EditFormat.FormatString = "";
            this.dateEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateEdit2.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateEdit2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Custom;
            this.dateEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEdit2.Size = new System.Drawing.Size(181, 28);
            this.dateEdit2.TabIndex = 4;
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Location = new System.Drawing.Point(496, 3);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.OffText = "统计";
            this.toggleSwitch1.Properties.OnText = "单日";
            this.toggleSwitch1.Size = new System.Drawing.Size(114, 25);
            this.toggleSwitch1.TabIndex = 5;
            // 
            // toggleSwitch2
            // 
            this.toggleSwitch2.Location = new System.Drawing.Point(3, 37);
            this.toggleSwitch2.Name = "toggleSwitch2";
            this.toggleSwitch2.Properties.OffText = "单日";
            this.toggleSwitch2.Properties.OnText = "统计";
            this.toggleSwitch2.Size = new System.Drawing.Size(114, 25);
            this.toggleSwitch2.TabIndex = 6;
            // 
            // toggleSwitch3
            // 
            this.toggleSwitch3.Location = new System.Drawing.Point(123, 37);
            this.toggleSwitch3.Name = "toggleSwitch3";
            this.toggleSwitch3.Properties.OffText = "单日";
            this.toggleSwitch3.Properties.OnText = "统计";
            this.toggleSwitch3.Size = new System.Drawing.Size(114, 25);
            this.toggleSwitch3.TabIndex = 7;
            // 
            // toggleSwitch4
            // 
            this.toggleSwitch4.Location = new System.Drawing.Point(243, 37);
            this.toggleSwitch4.Name = "toggleSwitch4";
            this.toggleSwitch4.Properties.OffText = "单日";
            this.toggleSwitch4.Properties.OnText = "统计";
            this.toggleSwitch4.Size = new System.Drawing.Size(114, 25);
            this.toggleSwitch4.TabIndex = 8;
            // 
            // toggleSwitch5
            // 
            this.toggleSwitch5.Location = new System.Drawing.Point(363, 37);
            this.toggleSwitch5.Name = "toggleSwitch5";
            this.toggleSwitch5.Properties.OffText = "单日";
            this.toggleSwitch5.Properties.OnText = "统计";
            this.toggleSwitch5.Size = new System.Drawing.Size(114, 25);
            this.toggleSwitch5.TabIndex = 9;
            // 
            // FoodStatisticsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FoodStatisticsControl";
            this.Size = new System.Drawing.Size(950, 381);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch5.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MyControl.FoodManagement.MgncPager mgncPager1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn MaterialName;
        private DevExpress.XtraGrid.Columns.GridColumn MaterialType;
        private DevExpress.XtraGrid.Columns.GridColumn MaterialNumber;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch2;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch3;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch4;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch5;
    }
}
