namespace DianDianClient.MyControl.MemberManagement
{
    partial class AddDetaileControl
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
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Btn_save = new DevExpress.XtraEditors.SimpleButton();
            this.Txt_money = new DevExpress.XtraEditors.TextEdit();
            this.Txt_rmoney = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_name = new DevExpress.XtraEditors.TextEdit();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_money.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_rmoney.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.Btn_save, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Txt_money, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Txt_rmoney, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Txt_name, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(333, 311);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Btn_save
            // 
            this.Btn_save.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.Btn_save.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_save.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_save.Appearance.Options.UseBackColor = true;
            this.Btn_save.Appearance.Options.UseFont = true;
            this.Btn_save.Appearance.Options.UseForeColor = true;
            this.Btn_save.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tableLayoutPanel1.SetColumnSpan(this.Btn_save, 2);
            this.Btn_save.Location = new System.Drawing.Point(108, 253);
            this.Btn_save.Name = "Btn_save";
            this.Btn_save.Size = new System.Drawing.Size(116, 35);
            this.Btn_save.TabIndex = 16;
            this.Btn_save.Text = "保存";
            this.Btn_save.Click += new System.EventHandler(this.Btn_save_Click);
            // 
            // Txt_money
            // 
            this.Txt_money.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Txt_money.Location = new System.Drawing.Point(102, 179);
            this.Txt_money.Name = "Txt_money";
            this.Txt_money.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_money.Properties.Appearance.Options.UseFont = true;
            this.Txt_money.Size = new System.Drawing.Size(145, 26);
            this.Txt_money.TabIndex = 5;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "不能为空";
            this.dxValidationProvider1.SetValidationRule(this.Txt_money, conditionValidationRule1);
            // 
            // Txt_rmoney
            // 
            this.Txt_rmoney.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Txt_rmoney.Location = new System.Drawing.Point(102, 102);
            this.Txt_rmoney.Name = "Txt_rmoney";
            this.Txt_rmoney.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_rmoney.Properties.Appearance.Options.UseFont = true;
            this.Txt_rmoney.Size = new System.Drawing.Size(145, 26);
            this.Txt_rmoney.TabIndex = 4;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "不能为空";
            this.dxValidationProvider1.SetValidationRule(this.Txt_rmoney, conditionValidationRule2);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "活动名称：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "赠送金额：";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "充值金额：";
            // 
            // Txt_name
            // 
            this.Txt_name.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Txt_name.Location = new System.Drawing.Point(102, 25);
            this.Txt_name.Name = "Txt_name";
            this.Txt_name.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_name.Properties.Appearance.Options.UseFont = true;
            this.Txt_name.Size = new System.Drawing.Size(145, 26);
            this.Txt_name.TabIndex = 3;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "不能为空";
            this.dxValidationProvider1.SetValidationRule(this.Txt_name, conditionValidationRule3);
            // 
            // AddDetaileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AddDetaileControl";
            this.Size = new System.Drawing.Size(333, 311);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_money.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_rmoney.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit Txt_name;
        private DevExpress.XtraEditors.TextEdit Txt_money;
        private DevExpress.XtraEditors.TextEdit Txt_rmoney;
        private DevExpress.XtraEditors.SimpleButton Btn_save;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
    }
}
