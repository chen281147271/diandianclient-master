namespace DianDianClient
{
    partial class EditGroupForm
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
            this.editGroupControl1 = new DianDianClient.MyControl.EditGroupControl();
            this.SuspendLayout();
            // 
            // editGroupControl1
            // 
            this.editGroupControl1.BackColor = System.Drawing.SystemColors.Control;
            this.editGroupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editGroupControl1.Location = new System.Drawing.Point(0, 0);
            this.editGroupControl1.Name = "editGroupControl1";
            this.editGroupControl1.Size = new System.Drawing.Size(814, 590);
            this.editGroupControl1.TabIndex = 0;
            // 
            // EditGroupForm
            // 
            this.ClientSize = new System.Drawing.Size(814, 590);
            this.Controls.Add(this.editGroupControl1);
            this.Name = "EditGroupForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MyControl.EditGroupControl editGroupControl1;
    }
}