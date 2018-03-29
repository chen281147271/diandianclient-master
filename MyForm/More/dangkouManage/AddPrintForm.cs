using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DianDianClient.MyForm.More.dangkouManage
{
    public partial class AddPrintForm : Form
    {
        public AddPrintForm()
        {
            InitializeComponent();
            MyControl.More.dangkouManage.AddPrintControl addPrint = new MyControl.More.dangkouManage.AddPrintControl();
            addPrint.Dock = DockStyle.Fill;
            addPrint.MyEnent += CloseEvent;
            this.Controls.Add(addPrint);
        }
        private void CloseEvent()
        {
            this.Close();
        }
    }
}
