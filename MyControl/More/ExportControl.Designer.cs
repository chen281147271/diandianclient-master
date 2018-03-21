namespace DianDianClient.MyControl.More
{
    partial class ExportControl
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
            DevExpress.Utils.CheckContextButton checkContextButton1 = new DevExpress.Utils.CheckContextButton();
            DevExpress.Utils.CheckContextButton checkContextButton2 = new DevExpress.Utils.CheckContextButton();
            DevExpress.Utils.CheckContextButton checkContextButton3 = new DevExpress.Utils.CheckContextButton();
            DevExpress.XtraPrinting.BarCode.QRCodeGenerator qrCodeGenerator1 = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Btn_Export = new DevExpress.XtraEditors.SimpleButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Pic_zhuotie1 = new DevExpress.XtraEditors.PictureEdit();
            this.Pic_zhuotie3 = new DevExpress.XtraEditors.PictureEdit();
            this.Pic_zhuotie2 = new DevExpress.XtraEditors.PictureEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.barCodeControl1 = new DevExpress.XtraEditors.BarCodeControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_zhuotie1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_zhuotie3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_zhuotie2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Btn_Export, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(874, 369);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Btn_Export
            // 
            this.Btn_Export.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Btn_Export.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.Btn_Export.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Export.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_Export.Appearance.Options.UseBackColor = true;
            this.Btn_Export.Appearance.Options.UseFont = true;
            this.Btn_Export.Appearance.Options.UseForeColor = true;
            this.Btn_Export.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Btn_Export.Location = new System.Drawing.Point(384, 332);
            this.Btn_Export.Name = "Btn_Export";
            this.Btn_Export.Size = new System.Drawing.Size(106, 34);
            this.Btn_Export.TabIndex = 19;
            this.Btn_Export.Text = "导出";
            this.Btn_Export.Click += new System.EventHandler(this.Btn_Export_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.radioGroup1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(868, 34);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "使用方式";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(83, 3);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.Appearance.Options.UseFont = true;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "桌贴"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "桌牌")});
            this.radioGroup1.Properties.ItemsLayout = DevExpress.XtraEditors.RadioGroupItemsLayout.Flow;
            this.radioGroup1.Size = new System.Drawing.Size(266, 31);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.Pic_zhuotie1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Pic_zhuotie3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Pic_zhuotie2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 43);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(868, 283);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // Pic_zhuotie1
            // 
            this.Pic_zhuotie1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Pic_zhuotie1.Location = new System.Drawing.Point(3, 43);
            this.Pic_zhuotie1.Name = "Pic_zhuotie1";
            this.Pic_zhuotie1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Pic_zhuotie1.Properties.Appearance.Options.UseBackColor = true;
            this.Pic_zhuotie1.Properties.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            checkContextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
            checkContextButton1.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
            checkContextButton1.Caption = null;
            checkContextButton1.Id = new System.Guid("4a9bb2d6-31b3-425f-9da9-cfc0128f311b");
            checkContextButton1.Name = "checkContextButton1";
            checkContextButton1.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            this.Pic_zhuotie1.Properties.ContextButtons.Add(checkContextButton1);
            this.Pic_zhuotie1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.Pic_zhuotie1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.Pic_zhuotie1.Size = new System.Drawing.Size(283, 217);
            this.Pic_zhuotie1.TabIndex = 4;
            this.Pic_zhuotie1.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.Pic_zhuotie1_ContextButtonClick);
            this.Pic_zhuotie1.Click += new System.EventHandler(this.Pic_zhuotie1_Click);
            // 
            // Pic_zhuotie3
            // 
            this.Pic_zhuotie3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Pic_zhuotie3.Location = new System.Drawing.Point(581, 43);
            this.Pic_zhuotie3.Name = "Pic_zhuotie3";
            this.Pic_zhuotie3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Pic_zhuotie3.Properties.Appearance.Options.UseBackColor = true;
            this.Pic_zhuotie3.Properties.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            checkContextButton2.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
            checkContextButton2.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
            checkContextButton2.Caption = null;
            checkContextButton2.Id = new System.Guid("4a9bb2d6-31b3-425f-9da9-cfc0128f311b");
            checkContextButton2.Name = "checkContextButton1";
            checkContextButton2.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            this.Pic_zhuotie3.Properties.ContextButtons.Add(checkContextButton2);
            this.Pic_zhuotie3.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.Pic_zhuotie3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.Pic_zhuotie3.Size = new System.Drawing.Size(283, 217);
            this.Pic_zhuotie3.TabIndex = 3;
            this.Pic_zhuotie3.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.Pic_zhuotie3_ContextButtonClick);
            this.Pic_zhuotie3.Click += new System.EventHandler(this.Pic_zhuotie3_Click);
            // 
            // Pic_zhuotie2
            // 
            this.Pic_zhuotie2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Pic_zhuotie2.Location = new System.Drawing.Point(292, 43);
            this.Pic_zhuotie2.Name = "Pic_zhuotie2";
            this.Pic_zhuotie2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Pic_zhuotie2.Properties.Appearance.Options.UseBackColor = true;
            this.Pic_zhuotie2.Properties.ContextButtonOptions.BottomPanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            checkContextButton3.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Bottom;
            checkContextButton3.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Center;
            checkContextButton3.Caption = null;
            checkContextButton3.Id = new System.Guid("4a9bb2d6-31b3-425f-9da9-cfc0128f311b");
            checkContextButton3.Name = "checkContextButton1";
            checkContextButton3.Visibility = DevExpress.Utils.ContextItemVisibility.Visible;
            this.Pic_zhuotie2.Properties.ContextButtons.Add(checkContextButton3);
            this.Pic_zhuotie2.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.Pic_zhuotie2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.Pic_zhuotie2.Size = new System.Drawing.Size(283, 217);
            this.Pic_zhuotie2.TabIndex = 2;
            this.Pic_zhuotie2.ContextButtonClick += new DevExpress.Utils.ContextItemClickEventHandler(this.Pic_zhuotie2_ContextButtonClick);
            this.Pic_zhuotie2.Click += new System.EventHandler(this.Pic_zhuotie2_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(396, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "选择模板";
            // 
            // barCodeControl1
            // 
            this.barCodeControl1.AutoModule = true;
            this.barCodeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barCodeControl1.BackColor = System.Drawing.Color.White;
            this.barCodeControl1.Location = new System.Drawing.Point(3, 352);
            this.barCodeControl1.Name = "barCodeControl1";
            this.barCodeControl1.Padding = new System.Windows.Forms.Padding(10, 2, 10, 0);
            this.barCodeControl1.ShowText = false;
            this.barCodeControl1.Size = new System.Drawing.Size(392, 395);
            qrCodeGenerator1.Version = DevExpress.XtraPrinting.BarCode.QRCodeVersion.Version1;
            this.barCodeControl1.Symbology = qrCodeGenerator1;
            this.barCodeControl1.TabIndex = 20;
            // 
            // ExportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ExportControl";
            this.Size = new System.Drawing.Size(874, 369);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_zhuotie1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_zhuotie3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_zhuotie2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevExpress.XtraEditors.PictureEdit Pic_zhuotie2;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.PictureEdit Pic_zhuotie1;
        private DevExpress.XtraEditors.PictureEdit Pic_zhuotie3;
        private DevExpress.XtraEditors.SimpleButton Btn_Export;
        private DevExpress.XtraEditors.BarCodeControl barCodeControl1;
    }
}
