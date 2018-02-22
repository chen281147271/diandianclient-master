using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DianDianClient.MyForm.TableSettlement
{
    public partial class TuiCaiGrid : Form
    {
        DataTable MenuTable;//菜单
        string TableNo;//桌号
        string EatNo;//用餐编号
        private MyControl.TableSettlement.TuiCaiGridControl tuiCaiGridControl;
        public TuiCaiGrid()
        {
            InitializeComponent();
            tuiCaiGridControl = new MyControl.TableSettlement.TuiCaiGridControl(TableNo, EatNo, MenuTable);
            tuiCaiGridControl.Dock = DockStyle.Fill;
            this.Controls.Add(tuiCaiGridControl);
        }
    }
}
