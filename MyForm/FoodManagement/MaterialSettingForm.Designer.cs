namespace DianDianClient
{
    partial class MaterialSettingForm
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
            this.materialSetting1 = new DianDianClient.MyControl.MaterialSetting();
            this.SuspendLayout();
            // 
            // materialSetting1
            // 
            this.materialSetting1.BackColor = System.Drawing.Color.White;
            this.materialSetting1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialSetting1.Location = new System.Drawing.Point(0, 0);
            this.materialSetting1.Name = "materialSetting1";
            this.materialSetting1.Size = new System.Drawing.Size(858, 603);
            this.materialSetting1.TabIndex = 0;
            // 
            // MaterialSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 603);
            this.Controls.Add(this.materialSetting1);
            this.DoubleBuffered = true;
            this.Name = "MaterialSettingForm";
            this.Text = "Form4";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form4_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private MyControl.MaterialSetting materialSetting1;
    }
}