using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nexus
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            try
            {
                string queryString = "SELECT [sId] FROM [Nexus].[dbo].[SystemLog]";

                string connectionString = Helper.GetSetting("Connection", "SQLSERVER");
                connectionString = connectionString.Substring(0, Strings.InStr(connectionString, "ID=") + 2);
                connectionString = connectionString + txtUser.Text + "; password=" + txtPassword.Text + ";";

                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    DataTable dt = new DataTable();
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dataTable);
                    connection.Close();
                }

                if (dataTable.Rows.Count > 0)
                {
                    Helper.SystemLogTXT("Admin Access", 0);
                    frmAdmin f = new frmAdmin();
                    f.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error: Connection Failed; Please Try Again.", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: Login Connect; " + ex.Message, 0);
                MessageBox.Show("Error: Login Connect; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: Login Cancel; " + ex.Message, 0);
                MessageBox.Show("Error: Login Cancel; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
