namespace DianDianClient.MyControl.FoodManagement
{
    partial class EditMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditMenu));
            DevExpress.Utils.ContextButton contextButton1 = new DevExpress.Utils.ContextButton();
            DevExpress.Utils.ContextButton contextButton2 = new DevExpress.Utils.ContextButton();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.FoodGroup = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.navigationPane1 = new DevExpress.XtraBars.Navigation.NavigationPane();
            this.navigationPage1 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Txt_FoodName = new DevExpress.XtraEditors.TextEdit();
            this.navigationPage2 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_tuijian = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_Import = new DevExpress.XtraEditors.SimpleButton();
            this.btn_export = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_addclass = new DevExpress.XtraEditors.SimpleButton();
            this.btn_addfood = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.tileView2 = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.tileViewColumn1 = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_guqing = new DevExpress.XtraEditors.SimpleButton();
            this.btn_shangjia = new DevExpress.XtraEditors.SimpleButton();
            this.btn_all = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationPane1)).BeginInit();
            this.navigationPane1.SuspendLayout();
            this.navigationPage1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_FoodName.Properties)).BeginInit();
            this.navigationPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // FoodGroup
            // 
            this.FoodGroup.Caption = "FoodGroup";
            this.FoodGroup.FieldName = "FoodGroupName";
            this.FoodGroup.Name = "FoodGroup";
            this.FoodGroup.Visible = true;
            this.FoodGroup.VisibleIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.gridControl1.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.tileView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(812, 612);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileView1});
            // 
            // tileView1
            // 
            this.tileView1.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tileView1.ContextButtonOptions.BottomPanelPadding = new System.Windows.Forms.Padding(10);
            this.tileView1.GridControl = this.gridControl1;
            this.tileView1.Name = "tileView1";
            this.tileView1.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.tileView1_ContextButtonClick);
            this.tileView1.ContextButtonCustomize += new DevExpress.XtraGrid.Views.Tile.TileViewContextButtonCustomizeEventHandler(this.tileView1_ContextButtonCustomize);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.navigationPane1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(980, 618);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // navigationPane1
            // 
            this.navigationPane1.Controls.Add(this.navigationPage1);
            this.navigationPane1.Controls.Add(this.navigationPage2);
            this.navigationPane1.Dock = System.Windows.Forms.DockStyle.Right;
            this.navigationPane1.ItemOrientation = System.Windows.Forms.Orientation.Vertical;
            this.navigationPane1.Location = new System.Drawing.Point(933, 3);
            this.navigationPane1.Name = "navigationPane1";
            this.navigationPane1.PageProperties.ShowExpandButton = false;
            this.navigationPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navigationPage1,
            this.navigationPage2});
            this.navigationPane1.RegularSize = new System.Drawing.Size(331, 612);
            this.navigationPane1.SelectedPage = this.navigationPage1;
            this.navigationPane1.Size = new System.Drawing.Size(44, 612);
            this.navigationPane1.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed;
            this.navigationPane1.TabIndex = 5;
            this.navigationPane1.Text = "操作";
            this.navigationPane1.Visible = false;
            // 
            // navigationPage1
            // 
            this.navigationPage1.Caption = "筛选";
            this.navigationPage1.Controls.Add(this.tableLayoutPanel2);
            this.navigationPage1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("navigationPage1.ImageOptions.Image")));
            this.navigationPage1.Name = "navigationPage1";
            this.navigationPage1.Size = new System.Drawing.Size(269, 552);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(269, 552);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(3, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 464);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "根据状态";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(6, 14);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "全部"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "只显示上架"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "只显示估清")});
            this.radioGroup1.Size = new System.Drawing.Size(260, 138);
            this.radioGroup1.TabIndex = 3;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "根据菜名";
            // 
            // Txt_FoodName
            // 
            this.Txt_FoodName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txt_FoodName.Location = new System.Drawing.Point(3, 3);
            this.Txt_FoodName.Name = "Txt_FoodName";
            this.Txt_FoodName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_FoodName.Properties.Appearance.Options.UseFont = true;
            this.Txt_FoodName.Size = new System.Drawing.Size(99, 26);
            this.Txt_FoodName.TabIndex = 1;
            this.Txt_FoodName.EditValueChanged += new System.EventHandler(this.Txt_FoodName_EditValueChanged);
            // 
            // navigationPage2
            // 
            this.navigationPage2.Caption = "操作";
            this.navigationPage2.Controls.Add(this.groupBox7);
            this.navigationPage2.Controls.Add(this.groupBox5);
            this.navigationPage2.Controls.Add(this.groupBox4);
            this.navigationPage2.Controls.Add(this.groupBox3);
            this.navigationPage2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("navigationPage2.ImageOptions.Image")));
            this.navigationPage2.Name = "navigationPage2";
            this.navigationPage2.Size = new System.Drawing.Size(269, 552);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.simpleButton1);
            this.groupBox7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox7.Location = new System.Drawing.Point(6, 411);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(263, 74);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "刷新";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(0, 25);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(266, 47);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "刷新";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.Location = new System.Drawing.Point(9, 331);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(263, 74);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "推荐设置";
            // 
            // btn_tuijian
            // 
            this.btn_tuijian.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_tuijian.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tuijian.Appearance.Options.UseFont = true;
            this.btn_tuijian.Location = new System.Drawing.Point(625, 6);
            this.btn_tuijian.Name = "btn_tuijian";
            this.btn_tuijian.Size = new System.Drawing.Size(74, 28);
            this.btn_tuijian.TabIndex = 0;
            this.btn_tuijian.Text = "推荐设置";
            this.btn_tuijian.Click += new System.EventHandler(this.btn_tuijian_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.radioGroup1);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(6, 167);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(263, 158);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "导入导出";
            // 
            // btn_Import
            // 
            this.btn_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Import.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Import.Appearance.Options.UseFont = true;
            this.btn_Import.Location = new System.Drawing.Point(305, 6);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(74, 28);
            this.btn_Import.TabIndex = 0;
            this.btn_Import.Text = "导入";
            // 
            // btn_export
            // 
            this.btn_export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_export.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_export.Appearance.Options.UseFont = true;
            this.btn_export.Location = new System.Drawing.Point(385, 6);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(74, 28);
            this.btn_export.TabIndex = 1;
            this.btn_export.Text = "导出";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 158);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "新增";
            // 
            // btn_addclass
            // 
            this.btn_addclass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_addclass.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addclass.Appearance.Options.UseFont = true;
            this.btn_addclass.Location = new System.Drawing.Point(625, 416);
            this.btn_addclass.Name = "btn_addclass";
            this.btn_addclass.Size = new System.Drawing.Size(163, 47);
            this.btn_addclass.TabIndex = 0;
            this.btn_addclass.Text = "新增分类";
            this.btn_addclass.Click += new System.EventHandler(this.btn_addclass_Click);
            // 
            // btn_addfood
            // 
            this.btn_addfood.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_addfood.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addfood.Appearance.Options.UseFont = true;
            this.btn_addfood.Location = new System.Drawing.Point(625, 488);
            this.btn_addfood.Name = "btn_addfood";
            this.btn_addfood.Size = new System.Drawing.Size(163, 47);
            this.btn_addfood.TabIndex = 1;
            this.btn_addfood.Text = "新增菜品";
            this.btn_addfood.Click += new System.EventHandler(this.btn_addfood_Click);
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(3, 35);
            this.gridControl2.MainView = this.tileView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(99, 574);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileView2});
            // 
            // tileView2
            // 
            this.tileView2.Appearance.ItemFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tileView2.Appearance.ItemFocused.ForeColor = System.Drawing.Color.Black;
            this.tileView2.Appearance.ItemFocused.Options.UseBackColor = true;
            this.tileView2.Appearance.ItemFocused.Options.UseForeColor = true;
            this.tileView2.Appearance.ItemNormal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tileView2.Appearance.ItemNormal.ForeColor = System.Drawing.Color.White;
            this.tileView2.Appearance.ItemNormal.Options.UseBackColor = true;
            this.tileView2.Appearance.ItemNormal.Options.UseForeColor = true;
            this.tileView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.FoodGroup,
            this.tileViewColumn1});
            this.tileView2.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            contextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
            contextButton1.Id = new System.Guid("2cec6bfd-cc59-44fb-8436-00919669c25e");
            contextButton1.ImageOptions.Image = global::DianDianClient.Properties.Resources.itemedit;
            contextButton1.Name = "contextButton1";
            contextButton2.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
            contextButton2.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far;
            contextButton2.Id = new System.Guid("7e109b8f-e6e3-4905-acf8-c0baa2a400d9");
            contextButton2.ImageOptions.Image = global::DianDianClient.Properties.Resources.sort;
            contextButton2.Name = "contextButton2";
            this.tileView2.ContextButtons.Add(contextButton1);
            this.tileView2.ContextButtons.Add(contextButton2);
            this.tileView2.GridControl = this.gridControl2;
            this.tileView2.Name = "tileView2";
            this.tileView2.OptionsTiles.GroupTextPadding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.tileView2.OptionsTiles.IndentBetweenGroups = 0;
            this.tileView2.OptionsTiles.IndentBetweenItems = 0;
            this.tileView2.OptionsTiles.ItemSize = new System.Drawing.Size(248, 88);
            this.tileView2.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List;
            this.tileView2.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileView2.OptionsTiles.Padding = new System.Windows.Forms.Padding(0);
            this.tileView2.OptionsTiles.RowCount = 0;
            tableColumnDefinition1.Length.Value = 161D;
            this.tileView2.TileColumns.Add(tableColumnDefinition1);
            tableRowDefinition1.Length.Value = 26D;
            this.tileView2.TileRows.Add(tableRowDefinition1);
            tileViewItemElement1.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tileViewItemElement1.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement1.Column = this.FoodGroup;
            tileViewItemElement1.Text = "FoodGroup";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.tileView2.TileTemplate.Add(tileViewItemElement1);
            this.tileView2.ItemClick += new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.tileView2_ItemClick);
            this.tileView2.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.tileView2_ContextButtonClick);
            this.tileView2.ContextButtonCustomize += new DevExpress.XtraGrid.Views.Tile.TileViewContextButtonCustomizeEventHandler(this.tileView2_ContextButtonCustomize);
            // 
            // tileViewColumn1
            // 
            this.tileViewColumn1.Caption = "tileViewColumn1";
            this.tileViewColumn1.FieldName = "FoodGroupID";
            this.tileViewColumn1.Name = "tileViewColumn1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_addfood);
            this.panel1.Controls.Add(this.btn_addclass);
            this.panel1.Controls.Add(this.btn_Import);
            this.panel1.Controls.Add(this.btn_shangjia);
            this.panel1.Controls.Add(this.btn_export);
            this.panel1.Controls.Add(this.btn_guqing);
            this.panel1.Controls.Add(this.btn_tuijian);
            this.panel1.Controls.Add(this.btn_all);
            this.panel1.Controls.Add(this.gridControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(114, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 612);
            this.panel1.TabIndex = 6;
            // 
            // btn_guqing
            // 
            this.btn_guqing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_guqing.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_guqing.Appearance.Options.UseFont = true;
            this.btn_guqing.Location = new System.Drawing.Point(545, 6);
            this.btn_guqing.Name = "btn_guqing";
            this.btn_guqing.Size = new System.Drawing.Size(74, 28);
            this.btn_guqing.TabIndex = 2;
            this.btn_guqing.Text = "估清";
            this.btn_guqing.Click += new System.EventHandler(this.btn_guqing_Click);
            // 
            // btn_shangjia
            // 
            this.btn_shangjia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_shangjia.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_shangjia.Appearance.Options.UseFont = true;
            this.btn_shangjia.Location = new System.Drawing.Point(465, 6);
            this.btn_shangjia.Name = "btn_shangjia";
            this.btn_shangjia.Size = new System.Drawing.Size(74, 28);
            this.btn_shangjia.TabIndex = 3;
            this.btn_shangjia.Text = "上架";
            this.btn_shangjia.Click += new System.EventHandler(this.btn_shangjia_Click);
            // 
            // btn_all
            // 
            this.btn_all.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_all.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_all.Appearance.Options.UseFont = true;
            this.btn_all.Location = new System.Drawing.Point(705, 6);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(74, 28);
            this.btn_all.TabIndex = 4;
            this.btn_all.Text = "全部";
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.Txt_FoodName, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.gridControl2, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(105, 612);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // EditMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "EditMenu";
            this.Size = new System.Drawing.Size(980, 618);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navigationPane1)).EndInit();
            this.navigationPane1.ResumeLayout(false);
            this.navigationPage1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_FoodName.Properties)).EndInit();
            this.navigationPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Tile.TileView tileView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Tile.TileView tileView2;
        private DevExpress.XtraGrid.Columns.TileViewColumn FoodGroup;
        private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn1;
        private DevExpress.XtraBars.Navigation.NavigationPane navigationPane1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage2;
        private System.Windows.Forms.GroupBox groupBox5;
        private DevExpress.XtraEditors.SimpleButton btn_tuijian;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.SimpleButton btn_Import;
        private DevExpress.XtraEditors.SimpleButton btn_export;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton btn_addclass;
        private DevExpress.XtraEditors.SimpleButton btn_addfood;
        private System.Windows.Forms.GroupBox groupBox7;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit Txt_FoodName;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btn_shangjia;
        private DevExpress.XtraEditors.SimpleButton btn_guqing;
        private DevExpress.XtraEditors.SimpleButton btn_all;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}
