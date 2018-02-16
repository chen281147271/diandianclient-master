using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace DianDianClient
{
    public partial class MainForm : OfficeForm
    {
        public MainForm()
        {
            InitializeComponent();

            comboBoxEx1.Items.AddRange(new object[] { eStyle.Office2013, eStyle.OfficeMobile2014, eStyle.Office2010Blue,
                eStyle.Office2010Silver, eStyle.Office2010Black, eStyle.VisualStudio2010Blue, eStyle.VisualStudio2012Light, 
                eStyle.VisualStudio2012Dark, eStyle.Office2007Blue, eStyle.Office2007Silver, eStyle.Office2007Black});
            comboBoxEx1.SelectedIndex = 0;
            foreach (DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins)
                comboBoxEdit1.Properties.Items.Add(skin.SkinName);

        }
        private void comboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            defaultLookAndFeel1.LookAndFeel.SkinName = comboBoxEdit1.EditValue.ToString();
        }

        private void switchButton1_ValueChanged(object sender, EventArgs e)
        {
            sideNav1.EnableClose = switchButton1.Value;
            UpdateSideNavDock();
        }

        private void switchButton2_ValueChanged(object sender, EventArgs e)
        {
            sideNav1.EnableMaximize = switchButton2.Value;
            UpdateSideNavDock();
        }

        private void switchButton3_ValueChanged(object sender, EventArgs e)
        {
            sideNav1.EnableSplitter = switchButton3.Value;
            UpdateSideNavDock();
        }

        /// <summary>
        /// Updates the Dock property of SideNav control since when Close/Maximize/Splitter functionality is enabled
        /// the Dock cannot be set to fill since control needs ability to resize itself.
        /// </summary>
        private void UpdateSideNavDock()
        {
            if (sideNav1.EnableClose || sideNav1.EnableMaximize || sideNav1.EnableSplitter)
            {
                if (sideNav1.Dock != DockStyle.Left)
                {
                    sideNav1.Dock = DockStyle.Left;
                    sideNav1.Width = this.ClientRectangle.Width - 32;
                    ToastNotification.Close(this); // Closes any toast messages if already open
                    ToastNotification.Show(this, "With current settings SideNav control must be able to resize itself so its Dock is set to Left.", 4000);
                }
            }
            else if (sideNav1.Dock != DockStyle.Fill)
            {
                sideNav1.Dock = DockStyle.Fill;
                ToastNotification.Close(this); // Closes any toast messages if already open
                ToastNotification.Show(this, "SideNav control Dock is set to Fill.", 2000);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedItem == null) return;
            eStyle style = (eStyle)comboBoxEx1.SelectedItem;
            if (styleManager1.ManagerStyle != style)
                styleManager1.ManagerStyle = style;
        }

        private void labelX13_MarkupLinkClick(object sender, MarkupLinkClickEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.devcomponents.com/kb2/?p=1687");
        }
    }
}