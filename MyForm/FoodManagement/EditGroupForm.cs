using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
namespace DianDianClient.MyForm.FoodManagement
{
    public partial class EditGroupForm : DevExpress.XtraEditors.XtraForm
    {
        public string GroupID;
        public EditGroupForm(string GroupID)
        {
            this.GroupID = GroupID;
            InitializeComponent();
            IniData();
        }
        private void IniData()
        {
            editGroupControl1.gridView1.SetRowCellValue(editGroupControl1.rowHandle_GroupNo, "Value", GroupID);
        }
    }
}
