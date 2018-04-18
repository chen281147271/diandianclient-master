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
    public partial class MaterialSettingForm : DevExpress.XtraEditors.XtraForm
    {
        public string strSelected="";
        MyControl.FoodManagement.MaterialSetting material;
        public List<Models.v_item_crude> list_itemcrude;
        string str_guigeNo;
        public MaterialSettingForm(List<Models.v_item_crude> list_itemcrude)
        {
           // this.str_guigeNo = str_guigeNo;
            this.list_itemcrude = list_itemcrude;
            InitializeComponent();
            MyControl.FoodManagement.MaterialSetting material = new MyControl.FoodManagement.MaterialSetting(list_itemcrude);
            this.material = material;
            material.Dock = DockStyle.Fill;
            material.MyEvent += CloseEvent;
            material._MyEvent += _CloseEvent;
            this.Controls.Add(material);
        }
        private void CloseEvent()
        {
            strSelected = material.strSelected;
            this.list_itemcrude = material.list_itemcrude;
            this.Close();
        }
        private void _CloseEvent()
        {
            strSelected = str_guigeNo;
            this.Close();
        }
    }
}
