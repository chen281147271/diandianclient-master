namespace DianDianClient.MyForm.TableSettlement
{
    partial class PayBillForm
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
            this.payBillControl1 = new DianDianClient.MyControl.TableSettlement.PayBillControl();
            this.SuspendLayout();
            // 
            // payBillControl1
            // 
            this.payBillControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.payBillControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.payBillControl1.Location = new System.Drawing.Point(0, 0);
            this.payBillControl1.Name = "payBillControl1";
            this.payBillControl1.Size = new System.Drawing.Size(832, 561);
            this.payBillControl1.TabIndex = 0;
            // 
            // PayBillForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 561);
            this.Controls.Add(this.payBillControl1);
            this.Name = "PayBillForm";
            this.Text = "PayBillForm";
            this.ResumeLayout(false);

        }

        #endregion

        public MyControl.TableSettlement.PayBillControl payBillControl1;
    }
}