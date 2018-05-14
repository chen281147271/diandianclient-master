namespace DianDianClient.MyForm
{
    partial class StarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions customHeaderButtonImageOptions1 = new DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions customHeaderButtonImageOptions2 = new DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions customHeaderButtonImageOptions3 = new DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions customHeaderButtonImageOptions4 = new DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions customHeaderButtonImageOptions5 = new DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions customHeaderButtonImageOptions6 = new DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions customHeaderButtonImageOptions7 = new DevExpress.XtraBars.Docking.CustomHeaderButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::DianDianClient.MyForm.WaitForm1), true, true);
            this.navigationPane1 = new DevExpress.XtraBars.Navigation.NavigationPane();
            this.nav_order = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.nav_table = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.nav_yingye = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.nav_vip = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.nav_food = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.nav_more = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.nav_activity = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.navigationPane1)).BeginInit();
            this.navigationPane1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // navigationPane1
            // 
            this.navigationPane1.Controls.Add(this.nav_order);
            this.navigationPane1.Controls.Add(this.nav_table);
            this.navigationPane1.Controls.Add(this.nav_yingye);
            this.navigationPane1.Controls.Add(this.nav_vip);
            this.navigationPane1.Controls.Add(this.nav_food);
            this.navigationPane1.Controls.Add(this.nav_more);
            this.navigationPane1.Controls.Add(this.nav_activity);
            this.navigationPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPane1.Location = new System.Drawing.Point(0, 0);
            this.navigationPane1.Margin = new System.Windows.Forms.Padding(0);
            this.navigationPane1.Name = "navigationPane1";
            this.navigationPane1.PageProperties.ShowCollapseButton = false;
            this.navigationPane1.PageProperties.ShowExpandButton = false;
            this.navigationPane1.PageProperties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.Image;
            this.navigationPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.nav_order,
            this.nav_table,
            this.nav_yingye,
            this.nav_vip,
            this.nav_food,
            this.nav_activity,
            this.nav_more});
            this.navigationPane1.RegularSize = new System.Drawing.Size(1275, 776);
            this.navigationPane1.SelectedPage = this.nav_table;
            this.navigationPane1.Size = new System.Drawing.Size(1275, 776);
            this.navigationPane1.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Expanded;
            this.navigationPane1.TabIndex = 0;
            this.navigationPane1.Text = "navigationPane1";
            this.navigationPane1.StateChanged += new DevExpress.XtraBars.Navigation.StateChangedEventHandler(this.navigationPane1_StateChanged);
            this.navigationPane1.SelectedPageChanged += new DevExpress.XtraBars.Navigation.SelectedPageChangedEventHandler(this.navigationPane1_SelectedPageChanged);
            // 
            // nav_order
            // 
            this.nav_order.BackgroundPadding = new System.Windows.Forms.Padding(0);
            this.nav_order.Caption = "订单管理";
            customHeaderButtonImageOptions1.Image = global::DianDianClient.Properties.Resources.refresh_32x32;
            this.nav_order.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("Button", false, customHeaderButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "从数据库重新拉取数据", -1, true, null, true, false, true, serializableAppearanceObject1, null, -1)});
            this.nav_order.ImageOptions.Image = global::DianDianClient.Properties.Resources.order;
            this.nav_order.Name = "nav_order";
            this.nav_order.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_order.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_order.Properties.ShowMode = DevExpress.XtraBars.Navigation.ItemShowMode.Image;
            this.nav_order.Size = new System.Drawing.Size(1196, 719);
            this.nav_order.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.nav_order_CustomButtonClick);
            // 
            // nav_table
            // 
            this.nav_table.BackgroundPadding = new System.Windows.Forms.Padding(0);
            this.nav_table.Caption = "桌位结算";
            customHeaderButtonImageOptions2.Image = global::DianDianClient.Properties.Resources.refresh_32x321;
            this.nav_table.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("Button", false, customHeaderButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "从数据库重新拉取数据", -1, true, null, true, false, true, serializableAppearanceObject2, null, -1)});
            this.nav_table.ImageOptions.Image = global::DianDianClient.Properties.Resources.zwjs;
            this.nav_table.Margin = new System.Windows.Forms.Padding(0);
            this.nav_table.Name = "nav_table";
            this.nav_table.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_table.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_table.Size = new System.Drawing.Size(1196, 719);
            this.nav_table.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.nav_table_CustomButtonClick);
            // 
            // nav_yingye
            // 
            this.nav_yingye.BackgroundPadding = new System.Windows.Forms.Padding(0);
            this.nav_yingye.Caption = "营业详情";
            customHeaderButtonImageOptions3.Image = global::DianDianClient.Properties.Resources.refresh_32x322;
            this.nav_yingye.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("Button", false, customHeaderButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "从数据库重新拉取数据", -1, true, null, true, false, true, serializableAppearanceObject3, null, -1)});
            this.nav_yingye.ImageOptions.Image = global::DianDianClient.Properties.Resources.yyxq;
            this.nav_yingye.Name = "nav_yingye";
            this.nav_yingye.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_yingye.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_yingye.Size = new System.Drawing.Size(1196, 719);
            this.nav_yingye.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.nav_yingye_CustomButtonClick);
            // 
            // nav_vip
            // 
            this.nav_vip.BackgroundPadding = new System.Windows.Forms.Padding(0);
            this.nav_vip.Caption = "会员管理";
            customHeaderButtonImageOptions4.Image = global::DianDianClient.Properties.Resources.refresh_32x323;
            this.nav_vip.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("Button", false, customHeaderButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "从数据库重新拉取数据", -1, true, null, true, false, true, serializableAppearanceObject4, null, -1)});
            this.nav_vip.ImageOptions.Image = global::DianDianClient.Properties.Resources.hygl;
            this.nav_vip.Name = "nav_vip";
            this.nav_vip.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_vip.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_vip.Size = new System.Drawing.Size(1196, 719);
            this.nav_vip.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.nav_vip_CustomButtonClick);
            // 
            // nav_food
            // 
            this.nav_food.BackgroundPadding = new System.Windows.Forms.Padding(0);
            this.nav_food.Caption = "菜品管理";
            customHeaderButtonImageOptions5.Image = global::DianDianClient.Properties.Resources.refresh_32x324;
            this.nav_food.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("Button", false, customHeaderButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "从数据库重新拉取数据", -1, true, null, true, false, true, serializableAppearanceObject5, null, -1)});
            this.nav_food.ImageOptions.Image = global::DianDianClient.Properties.Resources.cpgl;
            this.nav_food.Name = "nav_food";
            this.nav_food.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_food.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_food.Size = new System.Drawing.Size(1196, 719);
            this.nav_food.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.nav_food_CustomButtonClick);
            // 
            // nav_more
            // 
            this.nav_more.BackgroundPadding = new System.Windows.Forms.Padding(0);
            this.nav_more.Caption = "更多选项";
            customHeaderButtonImageOptions6.Image = global::DianDianClient.Properties.Resources.reset_32x32;
            this.nav_more.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("返回", false, customHeaderButtonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "返回", -1, true, null, true, false, true, serializableAppearanceObject6, null, -1)});
            this.nav_more.ImageOptions.Image = global::DianDianClient.Properties.Resources.gdxx;
            this.nav_more.Name = "nav_more";
            this.nav_more.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_more.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_more.Size = new System.Drawing.Size(1196, 719);
            this.nav_more.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.nav_more_CustomButtonClick);
            // 
            // nav_activity
            // 
            this.nav_activity.BackgroundPadding = new System.Windows.Forms.Padding(0);
            this.nav_activity.Caption = "活动管理";
            customHeaderButtonImageOptions7.Image = global::DianDianClient.Properties.Resources.refresh_32x325;
            this.nav_activity.CustomHeaderButtons.AddRange(new DevExpress.XtraBars.Docking2010.IButton[] {
            new DevExpress.XtraBars.Docking.CustomHeaderButton("Button", false, customHeaderButtonImageOptions7, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "从数据库重新拉取数据", -1, true, null, true, false, true, serializableAppearanceObject7, null, -1)});
            this.nav_activity.ImageOptions.Image = global::DianDianClient.Properties.Resources.hdgl;
            this.nav_activity.Name = "nav_activity";
            this.nav_activity.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_activity.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            this.nav_activity.Size = new System.Drawing.Size(1196, 719);
            this.nav_activity.CustomButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.nav_activity_CustomButtonClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.navigationPane1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1275, 776);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // StarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 776);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.navigationPane1)).EndInit();
            this.navigationPane1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.NavigationPane navigationPane1;
        private DevExpress.XtraBars.Navigation.NavigationPage nav_order;
        private DevExpress.XtraBars.Navigation.NavigationPage nav_table;
        private DevExpress.XtraBars.Navigation.NavigationPage nav_yingye;
        private DevExpress.XtraBars.Navigation.NavigationPage nav_vip;
        private DevExpress.XtraBars.Navigation.NavigationPage nav_food;
        private DevExpress.XtraBars.Navigation.NavigationPage nav_more;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private DevExpress.XtraBars.Navigation.NavigationPage nav_activity;
    }
}