namespace DianDianClient.MyForm.TableSettlement
{
    partial class SaleForm
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
            this.saleControlcs1 = new DianDianClient.MyControl.TableSettlement.SaleControlcs();
            this.SuspendLayout();
            // 
            // saleControlcs1
            // 
            this.saleControlcs1.BackColor = System.Drawing.Color.White;
            this.saleControlcs1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saleControlcs1.Location = new System.Drawing.Point(0, 0);
            this.saleControlcs1.Name = "saleControlcs1";
            this.saleControlcs1.Price = 100.11M;
            this.saleControlcs1.Size = new System.Drawing.Size(912, 370);
            this.saleControlcs1.TabIndex = 0;
            this.saleControlcs1.Load += new System.EventHandler(this.saleControlcs1_Load);
            // 
            // SaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 370);
            this.Controls.Add(this.saleControlcs1);
            this.Name = "SaleForm";
            this.Text = "SaleForm";
            this.ResumeLayout(false);

        }

        #endregion

        public MyControl.TableSettlement.SaleControlcs saleControlcs1;
    }
}