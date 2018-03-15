namespace DianDianClient.MyControl.MemberManagement
{
    partial class RechargeControl
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
            this.Txt_songmoney = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.Lab_money = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Txt_money = new DevExpress.XtraEditors.TextEdit();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Btn_query = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_songmoney.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_money.Properties)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.Txt_songmoney, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Lab_money, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Txt_money, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 348);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Txt_songmoney
            // 
            this.Txt_songmoney.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Txt_songmoney.Location = new System.Drawing.Point(96, 204);
            this.Txt_songmoney.Name = "Txt_songmoney";
            this.Txt_songmoney.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_songmoney.Properties.Appearance.Options.UseFont = true;
            this.Txt_songmoney.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.Txt_songmoney.Size = new System.Drawing.Size(186, 26);
            this.Txt_songmoney.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "充值金额:";
            // 
            // Lab_money
            // 
            this.Lab_money.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Lab_money.AutoSize = true;
            this.Lab_money.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_money.Location = new System.Drawing.Point(96, 33);
            this.Lab_money.Name = "Lab_money";
            this.Lab_money.Size = new System.Drawing.Size(55, 21);
            this.Lab_money.TabIndex = 1;
            this.Lab_money.Text = "label2";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(32, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "余额：";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "赠送金额:";
            // 
            // Txt_money
            // 
            this.Txt_money.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Txt_money.Location = new System.Drawing.Point(96, 117);
            this.Txt_money.Name = "Txt_money";
            this.Txt_money.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_money.Properties.Appearance.Options.UseFont = true;
            this.Txt_money.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.Txt_money.Size = new System.Drawing.Size(186, 26);
            this.Txt_money.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.Btn_query);
            this.flowLayoutPanel1.Controls.Add(this.simpleButton1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(112, 283);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(244, 43);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // Btn_query
            // 
            this.Btn_query.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_query.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.Btn_query.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_query.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_query.Appearance.Options.UseBackColor = true;
            this.Btn_query.Appearance.Options.UseFont = true;
            this.Btn_query.Appearance.Options.UseForeColor = true;
            this.Btn_query.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Btn_query.Location = new System.Drawing.Point(3, 3);
            this.Btn_query.Name = "Btn_query";
            this.Btn_query.Size = new System.Drawing.Size(116, 35);
            this.Btn_query.TabIndex = 14;
            this.Btn_query.Text = "充值";
            this.Btn_query.Click += new System.EventHandler(this.Btn_query_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(104)))), ((int)(((byte)(18)))));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.simpleButton1.Location = new System.Drawing.Point(125, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(113, 35);
            this.simpleButton1.TabIndex = 15;
            this.simpleButton1.Text = "取消";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // RechargeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RechargeControl";
            this.Size = new System.Drawing.Size(468, 348);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_songmoney.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_money.Properties)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Lab_money;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit Txt_songmoney;
        private DevExpress.XtraEditors.TextEdit Txt_money;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraEditors.SimpleButton Btn_query;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}
