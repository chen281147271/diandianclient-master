namespace DianDianClient.MyControl.OrderManagement
{
    partial class OrderProcessingControl
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
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition4 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraEditors.TableLayout.TableSpan tableSpan1 = new DevExpress.XtraEditors.TableLayout.TableSpan();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement4 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement5 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.tableNo = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.createDate = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.orderNo = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.state = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.money = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.cfmainkey = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.orderDetailDetailControl1 = new DianDianClient.MyControl.BusinessDetails.OrderDetailDetailControl();
            this.mgncPager1 = new DianDianClient.MyControl.FoodManagement.MgncPager();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableNo
            // 
            this.tableNo.Caption = "tileViewColumn1";
            this.tableNo.FieldName = "tableNo";
            this.tableNo.Name = "tableNo";
            this.tableNo.Visible = true;
            this.tableNo.VisibleIndex = 0;
            // 
            // createDate
            // 
            this.createDate.Caption = "下单时间";
            this.createDate.FieldName = "createDate";
            this.createDate.Name = "createDate";
            this.createDate.OptionsColumn.ShowCaption = true;
            this.createDate.Visible = true;
            this.createDate.VisibleIndex = 1;
            // 
            // orderNo
            // 
            this.orderNo.Caption = "订单编号";
            this.orderNo.FieldName = "orderNo";
            this.orderNo.Name = "orderNo";
            this.orderNo.OptionsColumn.ShowCaption = true;
            this.orderNo.Visible = true;
            this.orderNo.VisibleIndex = 2;
            // 
            // state
            // 
            this.state.Caption = "订单状态 ";
            this.state.FieldName = "state";
            this.state.Name = "state";
            this.state.OptionsColumn.ShowCaption = true;
            this.state.Visible = true;
            this.state.VisibleIndex = 3;
            // 
            // money
            // 
            this.money.Caption = "订单金额";
            this.money.FieldName = "amount";
            this.money.Name = "money";
            this.money.OptionsColumn.ShowCaption = true;
            this.money.Visible = true;
            this.money.VisibleIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.orderDetailDetailControl1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.mgncPager1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1095, 546);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 3);
            this.gridControl1.MainView = this.tileView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(815, 501);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileView1});
            this.gridControl1.DataSourceChanged += new System.EventHandler(this.gridControl1_DataSourceChanged);
            // 
            // tileView1
            // 
            this.tileView1.Appearance.GroupText.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.tileView1.Appearance.GroupText.Options.UseForeColor = true;
            this.tileView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.tableNo,
            this.createDate,
            this.orderNo,
            this.state,
            this.money,
            this.cfmainkey});
            this.tileView1.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.LightGray;
            contextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Right;
            contextButton1.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
            contextButton1.AppearanceNormal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            contextButton1.AppearanceNormal.Font = new System.Drawing.Font("微软雅黑", 18F);
            contextButton1.AppearanceNormal.ForeColor = System.Drawing.Color.White;
            contextButton1.AppearanceNormal.Options.UseBackColor = true;
            contextButton1.AppearanceNormal.Options.UseFont = true;
            contextButton1.AppearanceNormal.Options.UseForeColor = true;
            contextButton1.Id = new System.Guid("3fddd98a-3764-49f1-9257-69ac041d2adc");
            contextButton1.ImageOptions.Image = global::DianDianClient.Properties.Resources.prints;
            contextButton1.Name = "contextButton1";
            contextButton1.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            contextButton2.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Right;
            contextButton2.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
            contextButton2.AppearanceNormal.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            contextButton2.AppearanceNormal.ForeColor = System.Drawing.Color.Transparent;
            contextButton2.AppearanceNormal.Options.UseFont = true;
            contextButton2.AppearanceNormal.Options.UseForeColor = true;
            contextButton2.Caption = "23";
            contextButton2.Id = new System.Guid("2f1d2bcc-abf8-4bfc-9d38-60d96c48bdd0");
            contextButton2.Name = "contextButton3";
            contextButton3.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Right;
            contextButton3.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
            contextButton3.AppearanceNormal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(128)))), ((int)(((byte)(63)))));
            contextButton3.AppearanceNormal.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            contextButton3.AppearanceNormal.ForeColor = System.Drawing.Color.White;
            contextButton3.AppearanceNormal.Options.UseBackColor = true;
            contextButton3.AppearanceNormal.Options.UseFont = true;
            contextButton3.AppearanceNormal.Options.UseForeColor = true;
            contextButton3.Id = new System.Guid("785298ec-ccb6-4371-a7e7-948ab62a8f27");
            contextButton3.ImageOptions.Image = global::DianDianClient.Properties.Resources.confirm;
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
            tableColumnDefinition2.Length.Value = 117D;
            tableColumnDefinition3.Length.Value = 122D;
            tableColumnDefinition4.Length.Value = 53D;
            this.tileView1.TileColumns.Add(tableColumnDefinition1);
            this.tileView1.TileColumns.Add(tableColumnDefinition2);
            this.tileView1.TileColumns.Add(tableColumnDefinition3);
            this.tileView1.TileColumns.Add(tableColumnDefinition4);
            tableRowDefinition1.Length.Value = 67D;
            tableRowDefinition2.Length.Value = 66D;
            this.tileView1.TileRows.Add(tableRowDefinition1);
            this.tileView1.TileRows.Add(tableRowDefinition2);
            tableSpan1.RowSpan = 2;
            this.tileView1.TileSpans.Add(tableSpan1);
            tileViewItemElement1.Column = this.tableNo;
            tileViewItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement1.Text = "tableNo";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.Column = this.createDate;
            tileViewItemElement2.ColumnIndex = 1;
            tileViewItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement2.Text = "createDate";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.Column = this.orderNo;
            tileViewItemElement3.ColumnIndex = 1;
            tileViewItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement3.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement3.RowIndex = 1;
            tileViewItemElement3.Text = "orderNo";
            tileViewItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement4.Column = this.state;
            tileViewItemElement4.ColumnIndex = 2;
            tileViewItemElement4.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement4.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement4.Text = "state";
            tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement5.Column = this.money;
            tileViewItemElement5.ColumnIndex = 2;
            tileViewItemElement5.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement5.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement5.RowIndex = 1;
            tileViewItemElement5.Text = "money";
            tileViewItemElement5.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileView1.TileTemplate.Add(tileViewItemElement1);
            this.tileView1.TileTemplate.Add(tileViewItemElement2);
            this.tileView1.TileTemplate.Add(tileViewItemElement3);
            this.tileView1.TileTemplate.Add(tileViewItemElement4);
            this.tileView1.TileTemplate.Add(tileViewItemElement5);
            this.tileView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.tileView1_FocusedRowChanged);
            // 
            // cfmainkey
            // 
            this.cfmainkey.Caption = "cfmainkey";
            this.cfmainkey.FieldName = "cfmainkey";
            this.cfmainkey.Name = "cfmainkey";
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
            // orderDetailDetailControl1
            // 
            this.orderDetailDetailControl1.BackColor = System.Drawing.Color.White;
            this.orderDetailDetailControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderDetailDetailControl1.Location = new System.Drawing.Point(824, 3);
            this.orderDetailDetailControl1.Name = "orderDetailDetailControl1";
            this.tableLayoutPanel1.SetRowSpan(this.orderDetailDetailControl1, 2);
            this.orderDetailDetailControl1.Size = new System.Drawing.Size(268, 540);
            this.orderDetailDetailControl1.TabIndex = 1;
            // 
            // mgncPager1
            // 
            this.mgncPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mgncPager1.Location = new System.Drawing.Point(3, 510);
            this.mgncPager1.Name = "mgncPager1";
            this.mgncPager1.Size = new System.Drawing.Size(815, 33);
            this.mgncPager1.TabIndex = 2;
            // 
            // OrderProcessingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OrderProcessingControl";
            this.Size = new System.Drawing.Size(1095, 546);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Tile.TileView tileView1;
        private DevExpress.XtraGrid.Columns.TileViewColumn tableNo;
        private DevExpress.XtraGrid.Columns.TileViewColumn createDate;
        private DevExpress.XtraGrid.Columns.TileViewColumn orderNo;
        private DevExpress.XtraGrid.Columns.TileViewColumn state;
        private DevExpress.XtraGrid.Columns.TileViewColumn money;
        private DevExpress.XtraGrid.Columns.TileViewColumn cfmainkey;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        public BusinessDetails.OrderDetailDetailControl orderDetailDetailControl1;
        private FoodManagement.MgncPager mgncPager1;
    }
}
