﻿using System;
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
        public MaterialSettingForm()
        {
            InitializeComponent();

        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            strSelected = materialSetting1.strSelected;
        }
    }
}
