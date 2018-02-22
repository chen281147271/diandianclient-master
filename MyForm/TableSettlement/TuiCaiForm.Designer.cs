namespace DianDianClient.MyForm.TableSettlement
{
    partial class TuiCai
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
            this.TuiCaiControl1 = new DianDianClient.MyControl.TableSettlement.TuiCaiControl();
            this.SuspendLayout();
            // 
            // TuiCaiControl1
            // 
            this.TuiCaiControl1.BackColor = System.Drawing.Color.White;
            this.TuiCaiControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TuiCaiControl1.Location = new System.Drawing.Point(0, 0);
            this.TuiCaiControl1.Name = "TuiCaiControl1";
            this.TuiCaiControl1.Size = new System.Drawing.Size(734, 317);
            this.TuiCaiControl1.TabIndex = 0;
            // 
            // TuiCai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 317);
            this.Controls.Add(this.TuiCaiControl1);
            this.Name = "TuiCai";
            this.Text = "TuiCai";
            this.ResumeLayout(false);

        }

        #endregion

        public MyControl.TableSettlement.TuiCaiControl TuiCaiControl1;
    }
}