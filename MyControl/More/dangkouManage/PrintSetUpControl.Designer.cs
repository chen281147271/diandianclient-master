namespace DianDianClient.MyControl.More.dangkouManage
{
    partial class PrintSetUpControl
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
            DevExpress.Utils.ContextButton contextButton1 = new DevExpress.Utils.ContextButton();
            DevExpress.Utils.ContextButton contextButton2 = new DevExpress.Utils.ContextButton();
            DevExpress.Utils.ContextButton contextButton3 = new DevExpress.Utils.ContextButton();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition3 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan1 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.sPrint = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.Paperwidth = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.cishu = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.isdefault = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.status = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.id = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sPrint
            // 
            this.sPrint.Caption = "打印机名称";
            this.sPrint.FieldName = "sPrint";
            this.sPrint.Name = "sPrint";
            this.sPrint.OptionsColumn.ShowCaption = true;
            this.sPrint.Visible = true;
            this.sPrint.VisibleIndex = 0;
            // 
            // Paperwidth
            // 
            this.Paperwidth.Caption = "纸张宽度";
            this.Paperwidth.FieldName = "Paperwidth";
            this.Paperwidth.Name = "Paperwidth";
            this.Paperwidth.OptionsColumn.ShowCaption = true;
            this.Paperwidth.Visible = true;
            this.Paperwidth.VisibleIndex = 1;
            // 
            // cishu
            // 
            this.cishu.Caption = "打印次数";
            this.cishu.FieldName = "cishu";
            this.cishu.Name = "cishu";
            this.cishu.OptionsColumn.ShowCaption = true;
            this.cishu.Visible = true;
            this.cishu.VisibleIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1077, 557);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 43);
            this.gridControl1.MainView = this.tileView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1071, 511);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileView1});
            // 
            // tileView1
            // 
            this.tileView1.Appearance.GroupText.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.tileView1.Appearance.GroupText.Options.UseForeColor = true;
            this.tileView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.sPrint,
            this.Paperwidth,
            this.cishu,
            this.isdefault,
            this.status,
            this.id});
            this.tileView1.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.LightGray;
            contextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Right;
            contextButton1.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
            contextButton1.AppearanceNormal.Font = new System.Drawing.Font("微软雅黑", 18F);
            contextButton1.AppearanceNormal.ForeColor = System.Drawing.Color.White;
            contextButton1.AppearanceNormal.Options.UseFont = true;
            contextButton1.AppearanceNormal.Options.UseForeColor = true;
            contextButton1.Id = new System.Guid("3fddd98a-3764-49f1-9257-69ac041d2adc");
            contextButton1.ImageOptions.Image = global::DianDianClient.Properties.Resources.kaishishiyong;
            contextButton1.Name = "contextButton1";
            contextButton1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            contextButton1.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            contextButton2.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Right;
            contextButton2.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
            contextButton2.AppearanceNormal.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            contextButton2.AppearanceNormal.ForeColor = System.Drawing.Color.Transparent;
            contextButton2.AppearanceNormal.Options.UseFont = true;
            contextButton2.AppearanceNormal.Options.UseForeColor = true;
            contextButton2.Id = new System.Guid("2f1d2bcc-abf8-4bfc-9d38-60d96c48bdd0");
            contextButton2.Name = "contextButton3";
            contextButton2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            contextButton2.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            contextButton3.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Right;
            contextButton3.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
            contextButton3.AppearanceNormal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(128)))), ((int)(((byte)(63)))));
            contextButton3.AppearanceNormal.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            contextButton3.AppearanceNormal.ForeColor = System.Drawing.Color.White;
            contextButton3.AppearanceNormal.Options.UseBackColor = true;
            contextButton3.AppearanceNormal.Options.UseFont = true;
            contextButton3.AppearanceNormal.Options.UseForeColor = true;
            contextButton3.Id = new System.Guid("785298ec-ccb6-4371-a7e7-948ab62a8f27");
            contextButton3.ImageOptions.Image = global::DianDianClient.Properties.Resources.printtest;
            contextButton3.Name = "contextButton2";
            contextButton3.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            this.tileView1.ContextButtons.Add(contextButton1);
            this.tileView1.ContextButtons.Add(contextButton2);
            this.tileView1.ContextButtons.Add(contextButton3);
            this.tileView1.GridControl = this.gridControl1;
            this.tileView1.Name = "tileView1";
            this.tileView1.OptionsTiles.GroupTextPadding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.tileView1.OptionsTiles.IndentBetweenGroups = 0;
            this.tileView1.OptionsTiles.IndentBetweenItems = 0;
            this.tileView1.OptionsTiles.ItemSize = new System.Drawing.Size(414, 220);
            this.tileView1.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List;
            this.tileView1.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileView1.OptionsTiles.Padding = new System.Windows.Forms.Padding(0);
            this.tileView1.OptionsTiles.RowCount = 0;
            tableColumnDefinition1.Length.Value = 82D;
            tableColumnDefinition2.Length.Value = 77D;
            tableColumnDefinition3.Length.Value = 231D;
            this.tileView1.TileColumns.Add(tableColumnDefinition1);
            this.tileView1.TileColumns.Add(tableColumnDefinition2);
            this.tileView1.TileColumns.Add(tableColumnDefinition3);
            tableRowDefinition1.Length.Value = 105D;
            tableRowDefinition2.Length.Value = 99D;
            this.tileView1.TileRows.Add(tableRowDefinition1);
            this.tileView1.TileRows.Add(tableRowDefinition2);
            tableSpan1.ColumnSpan = 2;
            this.tileView1.TileSpans.Add(tableSpan1);
            tileViewItemElement1.Column = this.sPrint;
            tileViewItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement1.Text = "sPrint";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.Column = this.Paperwidth;
            tileViewItemElement2.ColumnIndex = 1;
            tileViewItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement2.RowIndex = 1;
            tileViewItemElement2.Text = "Paperwidth";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.Column = this.cishu;
            tileViewItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement3.RowIndex = 1;
            tileViewItemElement3.Text = "cishu";
            tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileView1.TileTemplate.Add(tileViewItemElement1);
            this.tileView1.TileTemplate.Add(tileViewItemElement2);
            this.tileView1.TileTemplate.Add(tileViewItemElement3);
            this.tileView1.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.tileView1_ContextButtonClick);
            this.tileView1.ContextButtonCustomize += new DevExpress.XtraGrid.Views.Tile.TileViewContextButtonCustomizeEventHandler(this.tileView1_ContextButtonCustomize);
            // 
            // isdefault
            // 
            this.isdefault.Caption = "isdefault";
            this.isdefault.FieldName = "isdefault";
            this.isdefault.Name = "isdefault";
            // 
            // status
            // 
            this.status.Caption = "status";
            this.status.FieldName = "status";
            this.status.Name = "status";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "moremore", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.NullText = "点击显示更多";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_save);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1071, 34);
            this.flowLayoutPanel1.TabIndex = 1;
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
            this.btn_save.Location = new System.Drawing.Point(962, 3);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(106, 29);
            this.btn_save.TabIndex = 28;
            this.btn_save.Text = "新增";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // id
            // 
            this.id.Caption = "id";
            this.id.FieldName = "id";
            this.id.Name = "id";
            // 
            // PrintSetUpControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PrintSetUpControl";
            this.Size = new System.Drawing.Size(1077, 557);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btn_save;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Tile.TileView tileView1;
        private DevExpress.XtraGrid.Columns.TileViewColumn sPrint;
        private DevExpress.XtraGrid.Columns.TileViewColumn Paperwidth;
        private DevExpress.XtraGrid.Columns.TileViewColumn cishu;
        private DevExpress.XtraGrid.Columns.TileViewColumn isdefault;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.TileViewColumn status;
        private DevExpress.XtraGrid.Columns.TileViewColumn id;
    }
}
