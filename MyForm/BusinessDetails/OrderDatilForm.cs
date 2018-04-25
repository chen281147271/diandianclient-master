using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.BusinessDetails
{
    public partial class OrderDatilForm : DevExpress.XtraEditors.XtraForm
    {
        public OrderDatilForm(object data)
        {
            MyControl.BusinessDetails.OrderDetailControl orderDetailControl = new MyControl.BusinessDetails.OrderDetailControl(data);
            orderDetailControl.Dock = DockStyle.Fill;
            this.Controls.Add(orderDetailControl);
            InitializeComponent();
        }
    }
}
