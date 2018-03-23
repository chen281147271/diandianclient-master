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
    public partial class QuYuEditForm : Form
    {
        public QuYuEditForm(string QuYuName, string QuYuNo, string tfuwu, int floorid, int OpType)
        {
            InitializeComponent();
            MyControl.More.TableManage.QuYuEditControl quYuEdit = new MyControl.More.TableManage.QuYuEditControl(QuYuName,QuYuNo,tfuwu,floorid,OpType);
            quYuEdit.Dock = DockStyle.Fill;
            this.Controls.Add(quYuEdit);
            quYuEdit.Myevent += CloseEvent;
        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
