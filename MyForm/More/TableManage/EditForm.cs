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
    public partial class EditForm : Form
    {
        public EditForm(List<Models.dd_table_floor> list, string peopleNum, string tfuwu, string floorName, string floorid, string tableposkey, string isRoom, string tableNo, string tableName)
        {
            InitializeComponent();
            MyControl.More.TableManage.EditControl edit = new MyControl.More.TableManage.EditControl(list, peopleNum, tfuwu, floorName, floorid, tableposkey, isRoom, tableNo, tableName);
            edit.Dock = DockStyle.Fill;
            this.Controls.Add(edit);
            edit.MyEvent += CloseEvent;
        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
