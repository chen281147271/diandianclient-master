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
    public partial class AddTableForm : DevExpress.XtraEditors.XtraForm
    {
        public AddTableForm(List<Models.dd_table_floor> list)
        {
            InitializeComponent();
            MyControl.More.TableManage.AddTableControl addTable = new MyControl.More.TableManage.AddTableControl(list);
            addTable.Dock = DockStyle.Fill;
            this.Controls.Add(addTable);
            addTable.MyEvent += CloserEvent;
        }
        private void CloserEvent()
        {
            this.Close();
        }
    }
}
