using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Nexus
{
    public partial class frmMain : Form
    {

        private string _TempPath;

        private DateTime _endTime;
        private DateTime _tempTime = DateTime.Now;

        private DataTable _queue;
        private DataTable _questions;

        private string _person = "";

        private int _day;

        private int _step = 0;
        //0 - Identity
        //100 - Questions
        //200 - Temperature
        //300 - Complete

        private int _sw1 = 0;
        private int _sw2 = 0;

        private string[] _response = new string[0];

        private bool _sdk = true;

        System.Windows.Forms.Timer t = null;
        System.Windows.Forms.Timer q = null;
        System.Windows.Forms.Timer s = null;


        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            { 

                Helper.SystemLogTXT("System Start", 0);
                this.WindowState = FormWindowState.Maximized;

                if (Directory.Exists(Helper.GetSetting("NetDir", "ALERTS")) == false)
                {
                    Helper.DoProcess("net", @"use " + Helper.GetSetting("NetDir", "ALERTS") + " " + Helper.GetSetting("NetPath", "ALERTS") + " /user:" + Helper.GetSetting("NetUser", "ALERTS"), 1);
                }

                if (Helper.ProcessCheck(Helper.GetSetting("TempProcess", "INPUTS")) == false)
                {
                    Helper.DoProcess(Helper.GetSetting("TempProgram", "INPUTS"), "", 1);
                }

                dgvResults.Columns.Add("col1", "Person");

                t = new System.Windows.Forms.Timer();
                t.Interval = 1000;
                t.Tick += new EventHandler(t_Tick);

                q = new System.Windows.Forms.Timer();
                q.Interval = 1000;
                q.Tick += new EventHandler(q_Tick);

                s = new System.Windows.Forms.Timer();
                s.Interval = 100;
                s.Tick += new EventHandler(s_Tick);

   
                lblTitle.Text = Helper.GetSetting("Banner", "DISPLAY");
                lblMessage.Text = Helper.GetSetting("Message", "DISPLAY");
                _TempPath = Helper.GetSetting("TempPath", "INPUTS");

                _day = DateTime.Now.Day;

                if (Helper.GetSetting("TempActive", "INPUTS") != "1") 
                    lblGetTemp.Visible = false;

                var filepath = Helper.GetAppPath() + "\\Questions.csv";
                if (!File.Exists(filepath))
                    using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write)))
                    {
                        writer.WriteLine("Header,Question,Response1,Response2");
                        writer.WriteLine("-,Enter Questions and Responses in CSV File within Program Directory,-,-");
                    }

                string[] cols = new string[0];
                _questions = Helper.GetDataTabletFromCSVFile(Helper.GetAppPath() + "\\Questions.csv", cols);
                Array.Resize<string>(ref _response, _questions.Rows.Count + 4);

                if (Helper.GetSetting("DataqSdkEnabled", "ACCESS") == "1")
                    DataSdkSetup();

                q.Enabled = true;
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: FormLoad; " + ex.Message, 0);
                MessageBox.Show("Error: FormLoad; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void DataSdkSetup()
        {
            try
            {
                short[] slist = new short[8];
                slist[0] = 0;
                slist[1] = 1;

                axDataqSdk1.DeviceDriver = "DI300NT.DLL";
                axDataqSdk1.DeviceID = "5EEC8A1F";
                axDataqSdk1.ADChannelCount = 2;
                axDataqSdk1.ADChannelList(slist);
                axDataqSdk1.SampleRate = 1000;
                axDataqSdk1.EventPoint = 1;
                axDataqSdk1.Start();
                s.Enabled = _sdk;
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: DataSdkSetup; " + ex.Message, 0);
                MessageBox.Show("Error: DataSdkSetup; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }    
        }

        private void DisplayUpdate(int typ)
        {
            try
            {
                t.Enabled = false;
                lblQuestionHeader.Text = "";
                lblQuestion.Text = "";
                lblResponse1.Text = "";
                lblResponse2.Text = "";
                lblQuestionHeader.ForeColor = Color.White;

                var temp = Helper.GetSetting("TempActive", "INPUTS");

                if (typ != -999)
                {
                    if (_step == 201)
                    {
                        if (typ == 1)
                        {
                            _step = 300;
                        }
                        else
                        {
                            _step = 200;
                        }
                    }
                    else if (_questions.Rows.Count - 1 + 100 == _step && Convert.ToInt32(temp) == 1 && typ > -1)
                    {
                        _response[_step - 99] = typ.ToString();
                        _step = 200;
                    }
                    else if (_questions.Rows.Count - 1 + 100 == _step && typ > -1)
                    {
                        _response[_step - 99] = typ.ToString();
                        _step = 300;
                    }
                    else if (_step > 100 && _step < 200 && typ == -1)
                    {
                        _step -= 1;
                    }
                    else if (_step >= 100 && _step < 200 && typ > -1)
                    {
                        _response[_step - 99] = typ.ToString();
                        _step += 1;
                    }
                    else if (_step == 100 && typ == -1)
                    {
                        _step = 0;
                    }
                    if (_step == 0 && typ == 1 && dgvQueue.Rows.Count > 0)
                    {
                        _step = 100;
                        int selectedrowindex = dgvQueue.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgvQueue.Rows[selectedrowindex];
                        var cleaned = selectedRow.Cells[1].Value.ToString().Replace("\0", string.Empty);
                        _response[0] = cleaned;
                        _person = selectedRow.Cells[0].Value.ToString();
                        _response[_response.Length - 3] = _person;
                        Helper.SystemLogSQL("User Selected; " + _person, -1);
                        q.Enabled = false;
                    }
                    else if (_step == 0)
                    {
                        UserSelect();
                        q.Enabled = true;
                    }
                    else if (_step == 301)
                    {
                        _step = 0;
                        _person = "";
                    }
                }

                if (_step == 0)
                {
                    if (dgvQueue.Rows.Count != 0)
                    {
                        int selectedrowindex = dgvQueue.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dgvQueue.Rows[selectedrowindex];
                        lblQuestionHeader.Text = "SELF-DECLARATION IDENTITY";
                        lblQuestion.Text = "Hello, are you " + selectedRow.Cells[0].Value.ToString() + "?";
                        lblResponse1.Text = "Yes";
                        lblResponse2.Text = "No";
                    }
                }
                else if (_step >= 100 && _step < 200)
                {
                    if (_questions.Rows.Count != 0)
                    {
                        lblQuestionHeader.Text = _questions.Rows[_step - 100][0].ToString().ToUpper();
                        lblQuestion.Text = _questions.Rows[_step - 100][1].ToString();
                        lblResponse1.Text = _questions.Rows[_step - 100][2].ToString();
                        lblResponse2.Text = _questions.Rows[_step - 100][3].ToString();
                    }
                }
                else if (_step == 200)
                {
                    if (Helper.ProcessCheck(Helper.GetSetting("TempProcess", "INPUTS")) == false)
                        Helper.DoProcess(Helper.GetSetting("TempProgram", "INPUTS"), "", 0);

                    Helper.GetMainWindowHandle(Helper.GetSetting("TempProcess", "INPUTS"), 1);

                    lblQuestionHeader.Text = "TEMPERATURE SCREENING";
                    lblQuestion.Text = "Please complete you temperature screening by standing on the designated space.";
                    lblResponse1.Text = "Waiting for result...";

                    dgvQueue.Enabled = false;
                    lblResponse1.Enabled = false;
                    lblResponse2.Enabled = false;

                    _endTime = DateTime.Now;
                    t.Enabled = true;
                }
                else if (_step == 300)
                {
                    lblQuestionHeader.Text = "SCREENING COMPLETE";
                    lblQuestion.Text = "Thank you for completing your daily employee health screening.";
                    var acc = Helper.GetSetting("AccessEnabled", "ACCESS");
                    int pStatus = Helper.DataLog(_response, acc);
                    AddResult(_person, pStatus);
                    _step = 301;
                    q.Enabled = true;
                    _endTime = DateTime.Now;
                }

            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: QuestionUpdate; " + ex.Message, 0);
                MessageBox.Show("Error: QuestionUpdate; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void s_Tick(object sender, EventArgs e)
        {
            try
            {
                if (axDataqSdk1.AvailableData > 0)
                {
                    object V;
                    short[,] intArray;
                    V = axDataqSdk1.GetData();

                    intArray = (short[,])V;

                    if (intArray[0, 0] == 32764)
                        _sw1 += 1;
                    else
                        _sw1 = 0;

                    if (_sw1 > 5)
                    {
                        _sw1 = 0;
                        DisplayUpdate(1);
                    }

                    if (intArray[1, 0] == 32764)
                        _sw2 += 1;
                    else
                        _sw2 = 0;

                    if (_sw2 > 5)
                    {
                        _sw2 = 0;
                        DisplayUpdate(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: s_TimerTick; " + ex.Message, 0);
                MessageBox.Show("Error: s_TimerTick; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void q_Tick(object sender, EventArgs e)
        {
            try
            {
                TimeSpan timeLeft = _endTime.Subtract(DateTime.Now);
                if (_step == 301 && timeLeft.Seconds < -5)
                {
                    GetQueue();
                    _step = 0;
                    _person = "";
                    DisplayUpdate(-999);
                }
                else if (_step == 0 && Convert.ToInt32(timeLeft.Seconds) % 5 == 0)
                {
                    bool t = false;

                    if (dgvQueue.Rows.Count == 0)
                        t = true;
                    else if (dgvQueue.CurrentCell.RowIndex == 0)
                        t = true;
                    else if (timeLeft.Seconds < -5)
                        t = true;

                    if (t == true)
                    {
                        _endTime = DateTime.Now;
                        GetQueue();
                    }

                    DisplayUpdate(-999);
                }

                if (_day != DateTime.Now.Day)
                {
                    _day = DateTime.Now.Day;
                    Helper.FileClean();
                }
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: q_TimerTick; " + ex.Message, 0);
                MessageBox.Show("Error: q_TimerTick; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void t_Tick(object sender, EventArgs e)
        {
            try
            {

                var filepath = _TempPath + "\\" + DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString("00")
                      + "\\" + DateTime.Now.Month.ToString("00") + "." + DateTime.Now.Day.ToString("00")
                      + "\\" + DateTime.Now.Month.ToString("00") + "." + DateTime.Now.Day.ToString("00") + "_FaceTemps.csv";

                if (File.GetLastWriteTime(filepath) > _endTime)
                {
                    t.Enabled = false;

                    string td = "";
                    string ti = "";
                    string tr = "";
                    string ts = "";

                    if (File.Exists(filepath))
                    {
                        var lastModified = System.IO.File.GetLastWriteTime(filepath);
                        if (lastModified > _tempTime)
                        {
                            _tempTime = lastModified;
                            DataTable dt = new DataTable();
                            string[] cols = new string[] { "Timestamp", "SkinTemperature", "BodyTemperature", "ThresholdTemp", "BodyTempOffset", "BodyTempAdaptive", "BodyTempSetting", "Disposition" };
                            dt = Helper.GetDataTabletFromCSVFile(filepath, cols);

                            DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
                            ti = lastRow["Timestamp"].ToString();
                            td = lastRow["Disposition"].ToString();
                            tr = lastRow["BodyTemperature"].ToString();
                            ts = lastRow["ThresholdTemp"].ToString();
                        }
                    }

                    _response[_response.Length - 2] = ti;
                    _response[_response.Length - 1] = td;

                    //Helper.DataLog(_response);

                    Helper.GetMainWindowHandle("Nexus", 3);

                    var tu = Helper.GetSetting("TempUpper", "INPUTS");
                    var tl = Helper.GetSetting("TempLower", "INPUTS");

                    if (Convert.ToDouble(tu) != Convert.ToDouble(ts))
                        Helper.SystemLogSQL("Temperature Setpoint Does Not Match Upper Limit; " + tu.ToString() + "; " + ts.ToString(), 1);

                    if (Convert.ToDouble(tr) < Convert.ToDouble(tl))
                        Helper.SystemLogSQL("Temperature Readind Less Than Lower Limit; " + tl.ToString() + "; " + tr.ToString(), 1);


                    if (td == "PASS")
                    {
                        lblQuestionHeader.ForeColor = Color.Green;
                        Helper.SystemLogSQL("Temperature Screen Test Pass; " + _person, -1);
                    }
                    else if (td == "FAIL")
                    {
                        lblQuestionHeader.ForeColor = Color.Red;
                        Helper.SystemLogSQL("Temperature Screen Test Failed; " + _person, -1);
                    }
                    else
                    {
                        lblQuestionHeader.ForeColor = Color.Red;
                    }

                    lblQuestionHeader.Text = "TEMPERATURE TEST COMPLETED";

                    lblQuestion.Text = "Please confirm test result to proceed.";
                    lblResponse1.Text = "Yes - Record a Complete Test";
                    lblResponse2.Text = "No - Restart Test";

                    lblResponse1.Enabled = true;
                    lblResponse2.Enabled = true;
                    dgvQueue.Enabled = true;
                    _step = 201;
                }

                TimeSpan timeLeft = _endTime.Subtract(DateTime.Now);
                if (timeLeft.Seconds < -20)
                {
                    t.Enabled = false;

                    Helper.GetMainWindowHandle("Nexus", 3);

                    Helper.SystemLogSQL("Temperature Screen Test Incomplete; " + _person, -1);

                    lblQuestionHeader.Text = "TEMPERATURE TEST RESULT COULD NOT BE FOUND";
                    lblQuestionHeader.ForeColor = Color.Red;
                    lblQuestion.Text = "Please confirm test result to proceed.";
                    lblResponse1.Text = "Yes - Record an Incomplete Test";
                    lblResponse2.Text = "No - Restart Test";

                    _response[_response.Length - 2] = "";
                    _response[_response.Length - 1] = "INCOMPLETE";

                    //Helper.DataLog(_response);

                    lblResponse1.Enabled = true;
                    lblResponse2.Enabled = true;
                    dgvQueue.Enabled = true;
                    _step = 201;
                }
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: t_TimerTick; " + ex.Message, 0);
                MessageBox.Show("Error: t_TimerTick; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void GetQueue()
        {
            try
            {
                string queryString = "SELECT TOP 5 [NAME],[CARD] FROM " + Helper.GetSetting("qryQueue", "SQLSERVER") + " WHERE [EVENT_DATE] > '" + DateTime.Now.AddMinutes(-1500).ToString() + "' GROUP BY [NAME],[CARD] ORDER BY MAX([EVENT_DATE]) DESC";
                string connectionString = Helper.GetSetting("Connection", "SQLSERVER");
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dataTable);
                    connection.Close();
                    _queue = dataTable;
                    dgvQueue.DataSource = _queue;
                    dgvQueue.Columns[1].Visible = false;
                    dgvQueue.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dgvQueue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    if (dgvQueue.Rows.Count > 0)
                        dgvQueue.Rows[0].Selected = true;
                }

            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: GetQueue; " + ex.Message, 0);
                //MessageBox.Show("Error: GetQueue; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void AddResult(string per, int status)
        {
            try
            {
                dgvResults.Rows.Insert(0, new string[] { per });

                if (status == 1)
                    dgvResults.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                else if (status == 2)
                    dgvResults.Rows[0].DefaultCellStyle.ForeColor = Color.Green;
                else
                    dgvResults.Rows[0].DefaultCellStyle.ForeColor = Color.White;

                if (dgvResults.Rows.Count > 3)
                    dgvResults.Rows.RemoveAt(dgvResults.Rows.Count - 1);

                dgvResults.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvResults.ClearSelection();

            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: AddResult; " + ex.Message, 0);
                MessageBox.Show("Error: AddResult; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                axDataqSdk1.Stop();
                Helper.SystemLogTXT("System Shutdown", 0);
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: frmMain_FormClosing; " + ex.Message, 0);
                MessageBox.Show("Error: frmMain_FormClosing; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void UserSelect()
        {
            try
            {
                if (dgvQueue.CurrentRow != null)
                {
                    if (dgvQueue.CurrentRow.Index == dgvQueue.Rows.Count - 1)
                    {
                        GetQueue();
                    }
                    else
                    {
                        dgvQueue.CurrentCell = dgvQueue.Rows[Math.Min(dgvQueue.CurrentRow.Index + 1, dgvQueue.Rows.Count - 1)].Cells[dgvQueue.CurrentCell.ColumnIndex];
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: KeyDown; " + ex.Message, 0);
                MessageBox.Show("Error: KeyDown; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.T)
                {
                    DisplayUpdate(-1);
                }
                else if (e.KeyCode == Keys.Y)
                {
                    DisplayUpdate(1);
                }
                else if (e.KeyCode == Keys.N)
                {
                    DisplayUpdate(0);
                }
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: KeyDown; " + ex.Message, 0);
                MessageBox.Show("Error: KeyDown; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void dgvQueue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _step = 0;
            DisplayUpdate(-999);
        }

        private void lblResponse1_Click(object sender, EventArgs e)
        {
            DisplayUpdate(1);
        }

        private void lblResponse2_Click(object sender, EventArgs e)
        {
            DisplayUpdate(0);
        }

        private void lblQuestion_Paint(object sender, PaintEventArgs e)
        {
            //ControlPaint.DrawBorder(e.Graphics, lblQuestion.DisplayRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void lblGetTemp_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, lblGetTemp.DisplayRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void lblGetTemp_Click(object sender, EventArgs e)
        {
            try
            {
                //Helper.KillProcessAndChildren(Helper.GetSetting("TempProcess", "INPUTS"));

                Process[] _proceses = null;
                _proceses = Process.GetProcessesByName(Helper.GetSetting("TempProcess", "INPUTS"));
                foreach (Process proces in _proceses)
                {
                    proces.Kill();
                }
                System.Threading.Thread.Sleep(1000);
                Helper.DoProcess(Helper.GetSetting("TempProgram", "INPUTS"), "", 0);
                Helper.GetMainWindowHandle(Helper.GetSetting("TempProcess", "INPUTS"), 1);
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: lblGetTemp; " + ex.Message, 0);
                MessageBox.Show("Error: lblGetTemp; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void lblAdmin_Click(object sender, EventArgs e)
        {
            try
            {

                //string message, title, defaultValue;
                //string myValue;

                //message = "Enter Password";
                //title = "Nexus";
                //defaultValue = "";

                //myValue = Interaction.InputBox(message, title, defaultValue, 100, 100);
                //if (myValue == "safe")
                //{
                //    Helper.SystemLogTXT("Admin Access", 0);
                //    frmAdmin f = new frmAdmin();
                //    f.Show();
                //}
                //else if (myValue != "")
                //{
                //    MessageBox.Show("Access Denied. Please Try Again.", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                //}

                frmLogin f = new frmLogin();
                f.Show();

            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: lblAdmin; " + ex.Message, 0);
                MessageBox.Show("Error: lblAdmin; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void lblAdmin_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, lblAdmin.DisplayRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void lblQuit_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void axDataqSdk1_NewData(object sender, AxDATAQSDKLib._DDataqSdkEvents_NewDataEvent e)
        {

        }

        private void axDataqSdk1_ControlError(object sender, AxDATAQSDKLib._DDataqSdkEvents_ControlErrorEvent e)
        {
            try
            {
                _sdk = false;
                axDataqSdk1.Stop();
                MessageBox.Show("Error: axDataqSdk1_ControlError;", "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                Helper.SystemLogTXT("Error: axDataqSdk1_ControlError; " + ex.Message, 0);
                MessageBox.Show("Error: axDataqSdk1_ControlError; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void lblQuit_Click(object sender, EventArgs e)
        {

        }
    }


}

