using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nexus
{
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                if (Helper.GetSetting("AccessEnabled", "ACCESS") == "1")
                {
                    cmdAccEnableCard.Enabled = true;
                    cmdAccEnable.Text = "Disable Global Card Control";
                }
                else
                {
                    cmdAccEnableCard.Enabled = false;
                    cmdAccEnable.Text = "Enable Global Card Control";
                }

                TblLoad();
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: frmAdmin_Load; " + ex.Message, 0);
                MessageBox.Show("Error: frmAdmin_Load; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdLoad_Click_1(object sender, EventArgs e)
        {
            TblLoad();
        }

        private void TblLoad()
        {

            try
            {
                string queryString = "";
                cmdAccEnableCard.Visible = false;
                cmdAccEnable.Visible = false;


                if (optSystem.Checked == true)
                {
                    queryString = "SELECT TOP 100 * FROM [Nexus].[dbo].[SystemLog] ORDER BY [sDateCreated] DESC";
                }
                else if (optData.Checked == true)
                {
                    queryString = "SELECT TOP 100 * FROM [Nexus].[dbo].[DataLog] ORDER BY [dDateCreated] DESC";
                }
                else if (optCard.Checked == true)
                {
                    queryString = "SELECT [LNAME],[FNAME],[CARDNO],[DESCRP],[LAST_ACC],[ACTIVE] FROM [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[BADGE_NEXUS] ORDER BY[LNAME]";
                    cmdAccEnableCard.Visible = true;
                    cmdAccEnable.Visible = true;
                }
                else if (optQuestions.Checked == true)
                {
                    queryString = "SELECT * FROM [Nexus].[dbo].[Questions] ORDER BY [qID], [qOrder]";
                }
                else if (optSettings.Checked == true)
                {
                    queryString = "SELECT * FROM [Nexus].[dbo].[Settings] ORDER BY [seType],[seItem]";
                }

                if (queryString != "")
                {
                    DataTable dataTable = new DataTable();
                    dataTable = GetTable(queryString);
                    dgvData.DataSource = dataTable;
                    dgvData.Columns[1].Visible = false;
                    dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    if (dgvData.Rows.Count > 0)
                        dgvData.Rows[0].Selected = true;
                }

            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: cmdLoad; " + ex.Message, 0);
                MessageBox.Show("Error: cmdLoad; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void cmdAccEnableCard_Click_1(object sender, EventArgs e)
        {
            try
            {
                int selectedrowindex = dgvData.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvData.Rows[selectedrowindex];

                if (selectedRow.Cells[5].Value.ToString() == "Y")
                {
                    Helper.SystemLogSQL(selectedRow.Cells[2].Value.ToString() + " - " + selectedRow.Cells[0].Value.ToString() + "; Card Control Disabled", 1);
                    Helper.AccessUpdate(selectedRow.Cells[2].Value.ToString(), 0);
                }
                else
                {
                    Helper.SystemLogSQL(selectedRow.Cells[2].Value.ToString() + " - " + selectedRow.Cells[0].Value.ToString() + "; Card Control Enabled", 1);
                    Helper.AccessUpdate(selectedRow.Cells[2].Value.ToString(), 1);
                }

                MessageBox.Show("Card Updated", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                TblLoad();
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: cmdAccEnableCard; " + ex.Message, 0);
                MessageBox.Show("Error: cmdAccEnableCard; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void cmdAccEnable_Click_1(object sender, EventArgs e)
        {
            try
            { 
                string connectionString = Helper.GetSetting("Connection", "SQLSERVER");
                string queryString;
                string queryString1 = "";

                if (cmdAccEnable.Text == "Enable Global Card Control")
                {
                    Helper.SystemLogSQL("Global Card Control Enabled", 1);

                    queryString1 = "UPDATE [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[BADGE_NEXUS] SET [ACTIVE] = 'Y'";

                    queryString = "UPDATE [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[BADGE_NEXUS]";
                    queryString += " SET[LAST_ACC] = LA, [ACTIVE] = AC";
                    queryString += " FROM (SELECT[CARDNO] AS CD, MAX([EVNT_DAT]) AS LA, CASE WHEN DATEDIFF(hour, MAX([EVNT_DAT]), GETDATE()) < 14 THEN 'Y' ELSE 'N' END AS AC";
                    queryString += " FROM [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[TRANSACTION_INFO_Temp] WHERE[LOCATION] = 'Front Office Door' GROUP BY[CARDNO]) AS t1";
                    queryString += " WHERE [CARDNO] = CD";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(queryString, connection);
                        SqlCommand command1 = new SqlCommand("Nexus_Status", connection);
                        SqlCommand command2 = new SqlCommand(queryString1, connection);
                        command1.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command2.ExecuteNonQuery();
                        command.ExecuteNonQuery();
                        command1.ExecuteNonQuery();
                        connection.Close();
                    }

                    Helper.WriteSetting("AccessEnabled", "1", "ACCESS");
                    cmdAccEnable.Text = "Disable Global Card Control";
                    cmdAccEnableCard.Enabled = true;
                }
                else
                {
                    Helper.SystemLogSQL("Global Card Control Disabled", 1);

                    queryString = "UPDATE [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[BADGE_NEXUS] SET [ACTIVE] = 'O'";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(queryString, connection);
                        SqlCommand command1 = new SqlCommand("Nexus_Status", connection);
                        command1.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.ExecuteNonQuery();
                        command1.ExecuteNonQuery();
                        connection.Close();
                    }
                    Helper.WriteSetting("AccessEnabled", "0", "ACCESS");
                    cmdAccEnable.Text = "Enable Global Card Control";
                    cmdAccEnableCard.Enabled = false;
                }

                TblLoad();
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: cmdAccEnable; " + ex.Message, 0);
                MessageBox.Show("Error: cmdAccEnable; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        private DataTable GetTable(string queryString)
        {
            string connectionString = Helper.GetSetting("Connection", "SQLSERVER"); 
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dataTable);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: GetTable; " + ex.Message, 0);
                MessageBox.Show("Error: GetTable; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return dataTable;
        }
    }
}

