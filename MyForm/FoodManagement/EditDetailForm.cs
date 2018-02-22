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
    public partial class EditDetailForm : DevExpress.XtraEditors.XtraForm
    {
        public string FoodID = "";
        public EditDetailForm(string FoodID)
        {
            InitializeComponent();
            this.FoodID = FoodID;
            IniData();
        }
        /// <summary>
        /// 初始化EditDetail
        /// </summary>
        private void IniData()
        {
            editDetail1.gridView1.SetRowCellValue(editDetail1.rowHandle_CommodityNo, "Value", FoodID);
        }
    }
}
