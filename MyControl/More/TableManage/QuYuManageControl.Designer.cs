namespace DianDianClient.MyControl.More.TableManage
{
    partial class QuYuManageControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.floorid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.floorname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.orderno = new DevExpress.XtraGrid.Columns.GridColumn();
            this.createdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.str_state = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRadioGroup1 = new DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup();
            this.ffuwu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.operation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_fanhui = new DevExpress.XtraEditors.SimpleButton();
            this.btn_addquyu = new DevExpress.XtraEditors.SimpleButton();
            this.mgncPager1 = new DianDianClient.MyControl.FoodManagement.MgncPager();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1031, 490);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 43);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRadioGroup1,
            this.repositoryItemButtonEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1025, 404);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.floorid,
            this.floorname,
            this.orderno,
            this.createdate,
            this.str_state,
            this.ffuwu,
            this.operation});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // floorid
            // 
            this.floorid.Caption = "floorid";
            this.floorid.FieldName = "floorid";
            this.floorid.Name = "floorid";
            // 
            // floorname
            // 
            this.floorname.Caption = "区域名称";
            this.floorname.FieldName = "floorname";
            this.floorname.Name = "floorname";
            this.floorname.Visible = true;
            this.floorname.VisibleIndex = 0;
            // 
            // orderno
            // 
            this.orderno.Caption = "顺序";
            this.orderno.FieldName = "orderno";
            this.orderno.Name = "orderno";
            this.orderno.Visible = true;
            this.orderno.VisibleIndex = 1;
            // 
            // createdate
            // 
            this.createdate.Caption = "创建时间";
            this.createdate.FieldName = "createdate";
            this.createdate.Name = "createdate";
            this.createdate.Visible = true;
            this.createdate.VisibleIndex = 2;
            // 
            // str_state
            // 
            this.str_state.Caption = "状态";
            this.str_state.ColumnEdit = this.repositoryItemRadioGroup1;
            this.str_state.FieldName = "str_state";
            this.str_state.Name = "str_state";
            this.str_state.Visible = true;
            this.str_state.VisibleIndex = 3;
            // 
            // repositoryItemRadioGroup1
            // 
            this.repositoryItemRadioGroup1.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "禁用"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "启用")});
            this.repositoryItemRadioGroup1.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            this.repositoryItemRadioGroup1.Name = "repositoryItemRadioGroup1";
            this.repositoryItemRadioGroup1.EditValueChanged += new System.EventHandler(this.repositoryItemRadioGroup1_EditValueChanged);
            // 
            // ffuwu
            // 
            this.ffuwu.Caption = "服务费";
            this.ffuwu.FieldName = "ffuwu";
            this.ffuwu.Name = "ffuwu";
            this.ffuwu.Visible = true;
            this.ffuwu.VisibleIndex = 4;
            // 
            // operation
            // 
            this.operation.Caption = "操作";
            this.operation.ColumnEdit = this.repositoryItemButtonEdit1;
            this.operation.Name = "operation";
            this.operation.Visible = true;
            this.operation.VisibleIndex = 5;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "编辑", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "删除", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btn_fanhui);
            this.flowLayoutPanel1.Controls.Add(this.btn_addquyu);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1025, 34);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btn_fanhui
            // 
            this.btn_fanhui.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_fanhui.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.btn_fanhui.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_fanhui.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_fanhui.Appearance.Options.UseBackColor = true;
            this.btn_fanhui.Appearance.Options.UseFont = true;
            this.btn_fanhui.Appearance.Options.UseForeColor = true;
            this.btn_fanhui.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_fanhui.Location = new System.Drawing.Point(916, 3);
            this.btn_fanhui.Name = "btn_fanhui";
            this.btn_fanhui.Size = new System.Drawing.Size(106, 35);
            this.btn_fanhui.TabIndex = 20;
            this.btn_fanhui.Text = "返回";
            this.btn_fanhui.Click += new System.EventHandler(this.btn_fanhui_Click);
            // 
            // btn_addquyu
            // 
            this.btn_addquyu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_addquyu.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.btn_addquyu.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_addquyu.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_addquyu.Appearance.Options.UseBackColor = true;
            this.btn_addquyu.Appearance.Options.UseFont = true;
            this.btn_addquyu.Appearance.Options.UseForeColor = true;
            this.btn_addquyu.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_addquyu.Location = new System.Drawing.Point(804, 3);
            this.btn_addquyu.Name = "btn_addquyu";
            this.btn_addquyu.Size = new System.Drawing.Size(106, 35);
            this.btn_addquyu.TabIndex = 21;
            this.btn_addquyu.Text = "添加区域";
            this.btn_addquyu.Click += new System.EventHandler(this.btn_addquyu_Click);
            // 
            // mgncPager1
            // 
            this.mgncPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mgncPager1.Location = new System.Drawing.Point(3, 453);
            this.mgncPager1.Name = "mgncPager1";
            this.mgncPager1.Size = new System.Drawing.Size(1025, 34);
            this.mgncPager1.TabIndex = 2;
            // 
            // QuYuManageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "QuYuManageControl";
            this.Size = new System.Drawing.Size(1031, 490);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRadioGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton btn_fanhui;
        private DevExpress.XtraEditors.SimpleButton btn_addquyu;
        private DevExpress.XtraGrid.Columns.GridColumn floorid;
        private DevExpress.XtraGrid.Columns.GridColumn floorname;
        private DevExpress.XtraGrid.Columns.GridColumn orderno;
        private DevExpress.XtraGrid.Columns.GridColumn createdate;
        private DevExpress.XtraGrid.Columns.GridColumn str_state;
        private DevExpress.XtraEditors.Repository.RepositoryItemRadioGroup repositoryItemRadioGroup1;
        private DevExpress.XtraGrid.Columns.GridColumn ffuwu;
        private DevExpress.XtraGrid.Columns.GridColumn operation;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private FoodManagement.MgncPager mgncPager1;
    }
}
