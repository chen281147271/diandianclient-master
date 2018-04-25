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
    public partial class SaleForm : DevExpress.XtraEditors.XtraForm
    {
        public SaleForm(decimal price, string FoodName)
        {
            InitializeComponent();
            saleControlcs1.MyCloseEvents += MyCloseEvents;
            saleControlcs1.Price = price;
            saleControlcs1.Lab_FoodNamePrice.Text = FoodName + "   单价:" + price.ToString();
        }
        private void MyCloseEvents(){
            this.Close();
            }

        private void saleControlcs1_Load(object sender, EventArgs e)
        {

        }
    }
}
