using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nexus
{

    static class Program
    {

        [STAThread]
        static void Main()
        {      
            if (Helper.SystemSetup(System.Configuration.ConfigurationSettings.AppSettings["SystemSetup"]) == true)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
            else
            {
                MessageBox.Show("Error: System Setup Failure", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }        

        }
    }
}
