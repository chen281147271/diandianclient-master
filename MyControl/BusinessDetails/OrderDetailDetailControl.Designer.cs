namespace DianDianClient.MyControl.BusinessDetails
{
    partial class OrderDetailDetailControl
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
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.name = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.num = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Lab_TableNo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Caption = "name";
            this.name.FieldName = "name";
            this.name.Name = "name";
            this.name.Visible = true;
            this.name.VisibleIndex = 0;
            // 
            // num
            // 
            this.num.Caption = "num";
            this.num.FieldName = "num";
            this.num.Name = "num";
            this.num.Visible = true;
            this.num.VisibleIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 48);
            this.gridControl1.MainView = this.tileView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(991, 404);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileView1});
            // 
            // tileView1
            // 
            this.tileView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.name,
            this.num});
            this.tileView1.GridControl = this.gridControl1;
            this.tileView1.Name = "tileView1";
            this.tileView1.OptionsTiles.GroupTextPadding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.tileView1.OptionsTiles.IndentBetweenGroups = 0;
            this.tileView1.OptionsTiles.IndentBetweenItems = 0;
            this.tileView1.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List;
            this.tileView1.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileView1.OptionsTiles.Padding = new System.Windows.Forms.Padding(0);
            this.tileView1.OptionsTiles.RowCount = 0;
            this.tileView1.TileColumns.Add(tableColumnDefinition1);
            this.tileView1.TileColumns.Add(tableColumnDefinition2);
            this.tileView1.TileRows.Add(tableRowDefinition1);
            tileViewItemElement1.Column = this.name;
            tileViewItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement1.Text = "name";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.Column = this.num;
            tileViewItemElement2.ColumnIndex = 1;
            tileViewItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement2.Text = "num";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileView1.TileTemplate.Add(tileViewItemElement1);
            this.tileView1.TileTemplate.Add(tileViewItemElement2);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Lab_TableNo, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(997, 455);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Lab_TableNo
            // 
            this.Lab_TableNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Lab_TableNo.AutoSize = true;
            this.Lab_TableNo.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_TableNo.Location = new System.Drawing.Point(451, 3);
            this.Lab_TableNo.Name = "Lab_TableNo";
            this.Lab_TableNo.Size = new System.Drawing.Size(94, 38);
            this.Lab_TableNo.TabIndex = 1;
            this.Lab_TableNo.Text = "X号桌";
            // 
            // OrderDetailDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "OrderDetailDetailControl";
            this.Size = new System.Drawing.Size(997, 455);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Tile.TileView tileView1;
        private DevExpress.XtraGrid.Columns.TileViewColumn name;
        private DevExpress.XtraGrid.Columns.TileViewColumn num;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label Lab_TableNo;
    }
}
