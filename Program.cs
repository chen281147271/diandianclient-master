using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DianDianClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();

            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MyForm.LoginForm login = new MyForm.LoginForm();
            login.WindowState = FormWindowState.Maximized;
            login.ShowDialog();
            if (login.DialogResult == DialogResult.OK)
            {
                Application.Run(new MyForm.StarForm());
            }
            else
            {
                return;
            }
        }
    }
}