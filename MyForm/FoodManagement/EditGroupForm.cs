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
        int GroupID;
        public EditGroupForm(int GroupID,int itemCategoryCode)
        {
            this.GroupID =GroupID;
            InitializeComponent();
            MyControl.FoodManagement.EditGroupControl editGroup = new MyControl.FoodManagement.EditGroupControl(this.GroupID, itemCategoryCode);
            editGroup.Dock = DockStyle.Fill;
            editGroup.MyEvent += CloseEvent;
            this.Controls.Add(editGroup);
            //IniData();
        }
        private void CloseEvent()
        {
            this.Close();
        }
        //private void IniData()
        //{
        //    var a=BIZFood.GetCategoryDetail(GroupID);
        //    editGroupControl1.gridView1.SetRowCellValue(editGroupControl1.rowHandle_GroupNo, "Value", GroupID);
        //}
    }
}
