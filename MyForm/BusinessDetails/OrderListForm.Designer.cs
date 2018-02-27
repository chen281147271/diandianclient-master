namespace DianDianClient.MyForm.BusinessDetails
{
    partial class OrderListForm
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
            this.orderListControl1 = new DianDianClient.MyControl.BusinessDetails.OrderListControl();
            this.SuspendLayout();
            // 
            // orderListControl1
            // 
            this.orderListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderListControl1.Location = new System.Drawing.Point(0, 0);
            this.orderListControl1.Name = "orderListControl1";
            this.orderListControl1.Size = new System.Drawing.Size(889, 529);
            this.orderListControl1.TabIndex = 0;
            // 
            // OrderListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 529);
            this.Controls.Add(this.orderListControl1);
            this.Name = "OrderListForm";
            this.Text = "OrderListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MyControl.BusinessDetails.OrderListControl orderListControl1;
    }
}