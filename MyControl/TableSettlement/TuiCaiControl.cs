using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DianDianClient.MyControl.TableSettlement
{
    public partial class TuiCaiControl : UserControl
    {
        string WhoClick = "";
        private TuiCaiDetaile tuiCaiDetaile1;
        public delegate void CloseEvents();
        public event CloseEvents MyCloseEvents;

        public TuiCaiDetaile TuiCaiDetaile1 { get => tuiCaiDetaile1; set => tuiCaiDetaile1 = value; }

        public TuiCaiControl()
        {
            InitializeComponent();
            IniImage();
        }
        private void IniImage()
        {
          //  this.btn_submit.BackgroundImage = Properties.Resources.submit;
            AllDisable();

            //this.btn_material.FlatStyle = FlatStyle.Flat;
            //this.btn_material.FlatAppearance.BorderSize = 0;

            //this.btn_ordererror.FlatStyle = FlatStyle.Flat;
            //this.btn_ordererror.FlatAppearance.BorderSize = 0;

            //this.btn_qualityproblem.FlatStyle = FlatStyle.Flat;
            //this.btn_qualityproblem.FlatAppearance.BorderSize = 0;

            //this.btn_submit.FlatStyle = FlatStyle.Flat;
            //this.btn_submit.FlatAppearance.BorderSize = 0;

        }

        private void btn_qualityproblem_Click(object sender, EventArgs e)
        {
            AllDisable();
            // this.btn_qualityproblem.BackgroundImage = Properties.Resources.qualityproblem_select;
            this.btn_qualityproblem.Appearance.BackColor = System.Drawing.Color.FromArgb(243, 147, 47);
            WhoClick = "qualityproblemClick";
        }

        private void btn_ordererror_Click(object sender, EventArgs e)
        {
            AllDisable();
            // this.btn_ordererror.BackgroundImage = Properties.Resources.ordererror_select;
            this.btn_ordererror.Appearance.BackColor = System.Drawing.Color.FromArgb(243, 147, 47);
            WhoClick = "ordererrorClick";
        }

        private void btn_material_Click(object sender, EventArgs e)
        {
            AllDisable();
            //this.btn_material.BackgroundImage = Properties.Resources.material_select;
            this.btn_material.Appearance.BackColor = System.Drawing.Color.FromArgb(243, 147, 47);
            WhoClick = "materialClick";
        }
        private void AllDisable()
        {
            //this.btn_material.BackgroundImage = Properties.Resources.material;
            //this.btn_ordererror.BackgroundImage = Properties.Resources.ordererror;
            //this.btn_qualityproblem.BackgroundImage = Properties.Resources.qualityproblem;

            this.btn_material.Appearance.BackColor = System.Drawing.Color.FromArgb(222, 222, 222);
            this.btn_ordererror.Appearance.BackColor = System.Drawing.Color.FromArgb(222, 222, 222);
            this.btn_qualityproblem.Appearance.BackColor = System.Drawing.Color.FromArgb(222, 222, 222);

            this.btn_material.Tag = false;
            this.btn_ordererror.Tag = false;
            this.btn_qualityproblem.Tag = false;

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                switch (WhoClick)
                {
                    case "qualityproblemClick":
                        tuiCaiDetaile1.Reason = "qualityproblemClick";
                        break;
                    case "ordererrorClick":
                        tuiCaiDetaile1.Reason = "ordererrorClick";
                        break;
                    case "materialClick":
                        tuiCaiDetaile1.Reason = "materialClick";
                        break;
                    default:
                        MessageBox.Show("请选择异常原因");
                        break;
                }
                if (tuiCaiDetaile1.Reason.Length > 0)
                {
                    tuiCaiDetaile1.Number = spinEdit1.Text;
                    tuiCaiDetaile1.Solution = textBox2.Text;
                    MyCloseEvents?.Invoke();
                }
            }
            catch
            {

            }
        }
        public struct TuiCaiDetaile
        {
            //退菜数量
            public string Number;
            //解决方案
            public string Solution;
            //原因
            public string Reason;
        };


    }
}
