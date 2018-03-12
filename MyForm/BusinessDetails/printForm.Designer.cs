namespace DianDianClient.MyForm.BusinessDetails
{
    partial class printForm
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
            this.printControl21 = new DianDianClient.MyControl.BusinessDetails.MyPrintControl();
            this.SuspendLayout();
            // 
            // printControl21
            // 
            this.printControl21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printControl21.Location = new System.Drawing.Point(0, 0);
            this.printControl21.Name = "printControl21";
            this.printControl21.Size = new System.Drawing.Size(919, 421);
            this.printControl21.TabIndex = 0;
            // 
            // printForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 421);
            this.Controls.Add(this.printControl21);
            this.Name = "printForm";
            this.Text = "printForm";
            this.ResumeLayout(false);

        }

        #endregion

        public MyControl.BusinessDetails.MyPrintControl printControl21;
    }
}