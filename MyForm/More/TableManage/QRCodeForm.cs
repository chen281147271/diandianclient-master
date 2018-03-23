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
    public partial class QRCodeForm : Form
    {
        public QRCodeForm(int tableposkey,int QRCode)
        {
            InitializeComponent();
            MyControl.More.TableManage.QRCodeControl qRCode = new MyControl.More.TableManage.QRCodeControl(tableposkey, QRCode);
            qRCode.Dock = DockStyle.Fill;
            this.Controls.Add(qRCode);
        }
    }
}
