using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.More.TableManage
{
    public partial class ExportForm : DevExpress.XtraEditors.XtraForm
    {
        public ExportForm(DataTable dt)
        {
            InitializeComponent();
            MyControl.More.TableManage.ExportControl export = new MyControl.More.TableManage.ExportControl(dt);
            export.Dock = DockStyle.Fill;
            this.Controls.Add(export);
        }
    }
}
