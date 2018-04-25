namespace DianDianClient.MyControl
{
    partial class TopBarControl
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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement7 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement8 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem7 = new DevExpress.Utils.ToolTipTitleItem();
            this.tileNavPane1 = new DevExpress.XtraBars.Navigation.TileNavPane();
            this.tileNavCategory3 = new DevExpress.XtraBars.Navigation.TileNavCategory();
            this.tileNavItem_voice = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItem_pw = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItem_print = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.tileNavItem_style = new DevExpress.XtraBars.Navigation.TileNavItem();
            this.navbtn_min = new DevExpress.XtraBars.Navigation.NavButton();
            this.navbtn_max = new DevExpress.XtraBars.Navigation.NavButton();
            this.navbtn_quit = new DevExpress.XtraBars.Navigation.NavButton();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tileNavPane1)).BeginInit();
            this.SuspendLayout();
            // 
            // tileNavPane1
            // 
            this.tileNavPane1.Buttons.Add(this.tileNavCategory3);
            this.tileNavPane1.Buttons.Add(this.navbtn_min);
            this.tileNavPane1.Buttons.Add(this.navbtn_max);
            this.tileNavPane1.Buttons.Add(this.navbtn_quit);
            // 
            // tileNavCategory1
            // 
            this.tileNavPane1.DefaultCategory.Name = "tileNavCategory1";
            this.tileNavPane1.DefaultCategory.OwnerCollection = null;
            // 
            // 
            // 
            this.tileNavPane1.DefaultCategory.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            this.tileNavPane1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tileNavPane1.Location = new System.Drawing.Point(0, 0);
            this.tileNavPane1.Margin = new System.Windows.Forms.Padding(0);
            this.tileNavPane1.Name = "tileNavPane1";
            this.tileNavPane1.Size = new System.Drawing.Size(1140, 40);
            this.tileNavPane1.TabIndex = 1;
            this.tileNavPane1.Text = "tileNavPane1";
            this.tileNavPane1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tileNavPane1_MouseDown);
            this.tileNavPane1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tileNavPane1_MouseMove);
            this.tileNavPane1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tileNavPane1_MouseUp);
            // 
            // tileNavCategory3
            // 
            this.tileNavCategory3.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.tileNavCategory3.Caption = "";
            this.tileNavCategory3.Glyph = global::DianDianClient.Properties.Resources.more;
            this.tileNavCategory3.Items.AddRange(new DevExpress.XtraBars.Navigation.TileNavItem[] {
            this.tileNavItem_voice,
            this.tileNavItem_pw,
            this.tileNavItem_print,
            this.tileNavItem_style});
            this.tileNavCategory3.Name = "tileNavCategory3";
            this.tileNavCategory3.OwnerCollection = null;
            toolTipTitleItem4.Text = "更多\r\n";
            superToolTip4.Items.Add(toolTipTitleItem4);
            this.tileNavCategory3.SuperTip = superToolTip4;
            // 
            // 
            // 
            this.tileNavCategory3.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            // 
            // tileNavItem_voice
            // 
            this.tileNavItem_voice.Caption = "";
            this.tileNavItem_voice.Name = "tileNavItem_voice";
            this.tileNavItem_voice.OwnerCollection = this.tileNavCategory3.Items;
            toolTipTitleItem1.Text = "声音";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.tileNavItem_voice.SuperTip = superToolTip1;
            // 
            // 
            // 
            this.tileNavItem_voice.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement1.Text = "";
            tileItemElement2.Text = "声音开关";
            this.tileNavItem_voice.Tile.Elements.Add(tileItemElement1);
            this.tileNavItem_voice.Tile.Elements.Add(tileItemElement2);
            this.tileNavItem_voice.Tile.Name = "tileBarItem1";
            this.tileNavItem_voice.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.tileNavItem_voice_ElementClick);
            // 
            // tileNavItem_pw
            // 
            this.tileNavItem_pw.Caption = "";
            this.tileNavItem_pw.Name = "tileNavItem_pw";
            this.tileNavItem_pw.OwnerCollection = this.tileNavCategory3.Items;
            toolTipTitleItem2.Text = "修改密码";
            superToolTip2.Items.Add(toolTipTitleItem2);
            this.tileNavItem_pw.SuperTip = superToolTip2;
            // 
            // 
            // 
            this.tileNavItem_pw.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement3.Image = global::DianDianClient.Properties.Resources.pw;
            tileItemElement3.Text = "";
            tileItemElement4.Text = "修改密码";
            this.tileNavItem_pw.Tile.Elements.Add(tileItemElement3);
            this.tileNavItem_pw.Tile.Elements.Add(tileItemElement4);
            this.tileNavItem_pw.Tile.Name = "tileBarItem2";
            this.tileNavItem_pw.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.tileNavItem_pw_ElementClick);
            // 
            // tileNavItem_print
            // 
            this.tileNavItem_print.Caption = "";
            this.tileNavItem_print.Name = "tileNavItem_print";
            this.tileNavItem_print.OwnerCollection = this.tileNavCategory3.Items;
            toolTipTitleItem3.Text = "打印机";
            superToolTip3.Items.Add(toolTipTitleItem3);
            this.tileNavItem_print.SuperTip = superToolTip3;
            // 
            // 
            // 
            this.tileNavItem_print.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement5.Image = global::DianDianClient.Properties.Resources.print;
            tileItemElement5.Text = "";
            tileItemElement6.Text = "打印机设置";
            this.tileNavItem_print.Tile.Elements.Add(tileItemElement5);
            this.tileNavItem_print.Tile.Elements.Add(tileItemElement6);
            this.tileNavItem_print.Tile.ItemSize = DevExpress.XtraBars.Navigation.TileBarItemSize.Wide;
            this.tileNavItem_print.Tile.Name = "tileBarItem1";
            this.tileNavItem_print.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.tileNavItem_print_ElementClick);
            // 
            // tileNavItem_style
            // 
            this.tileNavItem_style.Caption = "";
            this.tileNavItem_style.Name = "tileNavItem_style";
            this.tileNavItem_style.OwnerCollection = this.tileNavCategory3.Items;
            // 
            // 
            // 
            this.tileNavItem_style.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement7.Image = global::DianDianClient.Properties.Resources.style;
            tileItemElement7.Text = "";
            tileItemElement8.Text = "风格设置";
            this.tileNavItem_style.Tile.Elements.Add(tileItemElement7);
            this.tileNavItem_style.Tile.Elements.Add(tileItemElement8);
            this.tileNavItem_style.Tile.Name = "tileBarItem1";
            // 
            // navbtn_min
            // 
            this.navbtn_min.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.navbtn_min.Caption = null;
            this.navbtn_min.Glyph = global::DianDianClient.Properties.Resources.min;
            this.navbtn_min.Name = "navbtn_min";
            toolTipTitleItem5.Text = "最小化";
            superToolTip5.Items.Add(toolTipTitleItem5);
            this.navbtn_min.SuperTip = superToolTip5;
            this.navbtn_min.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.navbtn_min_ElementClick);
            // 
            // navbtn_max
            // 
            this.navbtn_max.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.navbtn_max.Caption = null;
            this.navbtn_max.Glyph = global::DianDianClient.Properties.Resources.max;
            this.navbtn_max.Name = "navbtn_max";
            toolTipTitleItem6.Text = "最大化";
            superToolTip6.Items.Add(toolTipTitleItem6);
            this.navbtn_max.SuperTip = superToolTip6;
            this.navbtn_max.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.navbtn_max_ElementClick);
            // 
            // navbtn_quit
            // 
            this.navbtn_quit.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.navbtn_quit.Caption = null;
            this.navbtn_quit.Glyph = global::DianDianClient.Properties.Resources.quit;
            this.navbtn_quit.Name = "navbtn_quit";
            toolTipTitleItem7.Text = "退出";
            superToolTip7.Items.Add(toolTipTitleItem7);
            this.navbtn_quit.SuperTip = superToolTip7;
            this.navbtn_quit.ElementClick += new DevExpress.XtraBars.Navigation.NavElementClickEventHandler(this.navbtn_quit_ElementClick);
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            // 
            // TopBarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tileNavPane1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TopBarControl";
            this.Size = new System.Drawing.Size(1140, 39);
            ((System.ComponentModel.ISupportInitialize)(this.tileNavPane1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.TileNavPane tileNavPane1;
        private DevExpress.XtraBars.Navigation.TileNavCategory tileNavCategory3;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItem_voice;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItem_pw;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItem_print;
        private DevExpress.XtraBars.Navigation.NavButton navbtn_min;
        private DevExpress.XtraBars.Navigation.NavButton navbtn_max;
        private DevExpress.XtraBars.Navigation.NavButton navbtn_quit;
        private DevExpress.XtraBars.Navigation.TileNavItem tileNavItem_style;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}
