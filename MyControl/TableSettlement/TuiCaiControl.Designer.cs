namespace DianDianClient.MyControl.TableSettlement
{
    partial class TuiCaiControl
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
            this.textBox2 = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.strNumber = new System.Windows.Forms.Label();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.btn_qualityproblem = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ordererror = new DevExpress.XtraEditors.SimpleButton();
            this.btn_material = new DevExpress.XtraEditors.SimpleButton();
            this.btn_submit = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.strNumber, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.spinEdit1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_qualityproblem, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_ordererror, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_material, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_submit, 2, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(639, 335);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.textBox2, 2);
            this.textBox2.Location = new System.Drawing.Point(193, 186);
            this.textBox2.Name = "textBox2";
            this.textBox2.Properties.AutoHeight = false;
            this.textBox2.Size = new System.Drawing.Size(248, 43);
            this.textBox2.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(134, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "解决方案";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数   量 ";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "问   题";
            // 
            // strNumber
            // 
            this.strNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.strNumber.AutoSize = true;
            this.strNumber.Location = new System.Drawing.Point(447, 35);
            this.strNumber.Name = "strNumber";
            this.strNumber.Size = new System.Drawing.Size(53, 12);
            this.strNumber.TabIndex = 6;
            this.strNumber.Text = "已点3个 ";
            // 
            // spinEdit1
            // 
            this.spinEdit1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.spinEdit1, 2);
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(193, 20);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.AutoHeight = false;
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinEdit1.Size = new System.Drawing.Size(248, 43);
            this.spinEdit1.TabIndex = 14;
            // 
            // btn_qualityproblem
            // 
            this.btn_qualityproblem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_qualityproblem.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.btn_qualityproblem.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_qualityproblem.Appearance.Options.UseBackColor = true;
            this.btn_qualityproblem.Appearance.Options.UseFont = true;
            this.btn_qualityproblem.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_qualityproblem.Location = new System.Drawing.Point(193, 96);
            this.btn_qualityproblem.Name = "btn_qualityproblem";
            this.btn_qualityproblem.Size = new System.Drawing.Size(121, 56);
            this.btn_qualityproblem.TabIndex = 15;
            this.btn_qualityproblem.Text = "质量问题";
            this.btn_qualityproblem.Click += new System.EventHandler(this.btn_qualityproblem_Click);
            // 
            // btn_ordererror
            // 
            this.btn_ordererror.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_ordererror.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.btn_ordererror.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ordererror.Appearance.Options.UseBackColor = true;
            this.btn_ordererror.Appearance.Options.UseFont = true;
            this.btn_ordererror.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_ordererror.Location = new System.Drawing.Point(320, 96);
            this.btn_ordererror.Name = "btn_ordererror";
            this.btn_ordererror.Size = new System.Drawing.Size(121, 56);
            this.btn_ordererror.TabIndex = 16;
            this.btn_ordererror.Text = "订单错误";
            this.btn_ordererror.Click += new System.EventHandler(this.btn_ordererror_Click);
            // 
            // btn_material
            // 
            this.btn_material.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_material.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.btn_material.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_material.Appearance.Options.UseBackColor = true;
            this.btn_material.Appearance.Options.UseFont = true;
            this.btn_material.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btn_material.Location = new System.Drawing.Point(447, 96);
            this.btn_material.Name = "btn_material";
            this.btn_material.Size = new System.Drawing.Size(121, 56);
            this.btn_material.TabIndex = 17;
            this.btn_material.Text = "原料不足";
            this.btn_material.Click += new System.EventHandler(this.btn_material_Click);
            // 
            // btn_submit
            // 
            this.btn_submit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_submit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(147)))), ((int)(((byte)(47)))));
            this.btn_submit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.Appearance.Options.UseBackColor = true;
            this.btn_submit.Appearance.Options.UseFont = true;
            this.btn_submit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tableLayoutPanel1.SetColumnSpan(this.btn_submit, 2);
            this.btn_submit.Location = new System.Drawing.Point(195, 264);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(244, 56);
            this.btn_submit.TabIndex = 18;
            this.btn_submit.Text = "确定";
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // TuiCaiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TuiCaiControl";
            this.Size = new System.Drawing.Size(639, 335);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBox2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.TextEdit textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label strNumber;
        public DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraEditors.SimpleButton btn_qualityproblem;
        private DevExpress.XtraEditors.SimpleButton btn_ordererror;
        private DevExpress.XtraEditors.SimpleButton btn_material;
        private DevExpress.XtraEditors.SimpleButton btn_submit;
    }
}
