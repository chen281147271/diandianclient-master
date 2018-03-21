using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.More
{
    public partial class ExportForm : Form
    {
        public ExportForm(DataTable dt)
        {
            InitializeComponent();
            MyControl.More.ExportControl export = new MyControl.More.ExportControl(dt);
            export.Dock = DockStyle.Fill;
            this.Controls.Add(export);
        }
    }
}
