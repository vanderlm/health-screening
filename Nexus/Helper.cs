using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nexus
{
    class Helper
    {
        private static DataTable _dtSettings;

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool turnon);
        private const int SW_MAXIMIZE = 3;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(int hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(int hWnd, StringBuilder title, int size);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        public static bool SystemSetup(string str)
        {
            try
            {
                if (!String.IsNullOrEmpty(str))
                {
                    string queryString = "SELECT [seItem],[seValue],[seType] FROM [Nexus].[dbo].[Settings]";
                    string connectionString = str;
                    DataTable dataTable = new DataTable();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(queryString, connection);
                        connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(dataTable);
                        connection.Close();
                        _dtSettings = dataTable;
                    }
                }
                else
                {
                    _dtSettings = new DataTable();
                    _dtSettings.Columns.Add("seItem", typeof(string));
                    _dtSettings.Columns.Add("seValue", typeof(string));
                    _dtSettings.Columns.Add("seType", typeof(string));

                    var MyIni = new IniFile(GetAppPath() + "\\Settings.ini");

                    if (!MyIni.KeyExists("Banner", "DISPLAY"))
                    {
                        MyIni.Write("Banner", "WELCOME TO THE GUELPH GLASS PLANT", "DISPLAY");
                        _dtSettings.Rows.Add("Banner", "WELCOME TO THE GUELPH GLASS PLANT", "DISPLAY");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("Banner", MyIni.Read("Banner", "DISPLAY"), "DISPLAY");
                    }

                    if (!MyIni.KeyExists("Message", "DISPLAY"))
                    {
                        MyIni.Write("Message", "To prevent the spread of novel coronavirus (COVID-19) in our community and reduce the risk of exposure to our staff and visitors, we are conducting a simple screening questionnaire. Your participation is important to help us take precautionary measures to protect you and everyone in this building. Thank you for your time.", "DISPLAY");
                        _dtSettings.Rows.Add("Message", "To prevent the spread of novel coronavirus (COVID-19) in our community and reduce the risk of exposure to our staff and visitors, we are conducting a simple screening questionnaire. Your participation is important to help us take precautionary measures to protect you and everyone in this building. Thank you for your time.", "DISPLAY");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("Message", MyIni.Read("Message", "DISPLAY"), "DISPLAY");
                    }

                    if (!MyIni.KeyExists("Connection", "SQLSERVER"))
                    {
                        MyIni.Write("Connection", "", "SQLSERVER");
                        _dtSettings.Rows.Add("Connection", "", "SQLSERVER");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("Connection", MyIni.Read("Connection", "SQLSERVER"), "SQLSERVER");
                    }

                    if (!MyIni.KeyExists("qryQueue", "SQLSERVER"))
                    {
                        MyIni.Write("qryQueue", "[GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[vw_Live_Front]", "SQLSERVER");
                        _dtSettings.Rows.Add("qryQueue", "[GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[vw_Live_Front]", "SQLSERVER");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("qryQueue", MyIni.Read("qryQueue", "SQLSERVER"), "SQLSERVER");
                    }

                    if (!MyIni.KeyExists("TempActive", "INPUTS"))
                    {
                        MyIni.Write("TempActive", "1", "INPUTS");
                        _dtSettings.Rows.Add("TempActive", "1", "INPUTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("TempActive", MyIni.Read("TempActive", "INPUTS"), "INPUTS");
                    }

                    if (!MyIni.KeyExists("TempPath", "INPUTS"))
                    {
                        MyIni.Write("TempPath", @"C:\Data\SeekScan", "INPUTS");
                        _dtSettings.Rows.Add("TempPath", @"C:\Data\SeekScan", "INPUTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("TempPath", MyIni.Read("TempPath", "INPUTS"), "INPUTS");
                    }

                    if (!MyIni.KeyExists("TempProgram", "INPUTS"))
                    {
                        MyIni.Write("TempProgram", @"C:\Program Files\Seek Thermal\Seek Scan\SeekScan.exe", "INPUTS");
                        _dtSettings.Rows.Add("TempProgram", @"C:\Program Files\Seek Thermal\Seek Scan\SeekScan.exe", "INPUTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("TempProgram", MyIni.Read("TempProgram", "INPUTS"), "INPUTS");
                    }

                    if (!MyIni.KeyExists("TempProcess", "INPUTS"))
                    {
                        MyIni.Write("TempProcess", "SeekScan", "INPUTS");
                        _dtSettings.Rows.Add("TempProcess", "SeekScan", "INPUTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("TempProcess", MyIni.Read("TempProcess", "INPUTS"), "INPUTS");
                    }

                    if (!MyIni.KeyExists("TempUpper", "INPUTS"))
                    {
                        MyIni.Write("TempUpper", "37.8", "INPUTS");
                        _dtSettings.Rows.Add("TempUpper", "37.8", "INPUTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("TempUpper", MyIni.Read("TempUpper", "INPUTS"), "INPUTS");
                    }

                    if (!MyIni.KeyExists("TempLower", "INPUTS"))
                    {
                        MyIni.Write("TempLower", "30", "INPUTS");
                        _dtSettings.Rows.Add("TempLower", "30", "INPUTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("TempLower", MyIni.Read("TempLower", "INPUTS"), "INPUTS");
                    }

                    if (!MyIni.KeyExists("NetDir", "ALERTS"))
                    {
                        MyIni.Write("NetDir", @"F:", "ALERTS");
                        _dtSettings.Rows.Add("NetDir", @"F:", "ALERTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("NetDir", MyIni.Read("NetDir", "ALERTS"), "ALERTS");
                    }

                    if (!MyIni.KeyExists("NetPath", "ALERTS"))
                    {
                        MyIni.Write("NetPath", "\\\\10.42.36.49\\outbox", "ALERTS");
                        _dtSettings.Rows.Add("NetPath", "\\\\10.42.36.49\\outbox", "ALERTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("NetPath", MyIni.Read("NetPath", "ALERTS"), "ALERTS");
                    }

                    if (!MyIni.KeyExists("NetUser", "ALERTS"))
                    {
                        MyIni.Write("NetUser", "guev8900\\DLS pass_word$1", "ALERTS");
                        _dtSettings.Rows.Add("NetUser", "guev8900\\DLS pass_word$1", "ALERTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("NetUser", MyIni.Read("NetUser", "ALERTS"), "ALERTS");
                    }

                    if (!MyIni.KeyExists("AdminEmail", "ALERTS"))
                    {
                        MyIni.Write("AdminEmail", "Mark.Vanderlaan@owenscorning.com;", "ALERTS");
                        _dtSettings.Rows.Add("AdminEmail", "Mark.Vanderlaan@owenscorning.com;", "ALERTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("AdminEmail", MyIni.Read("AdminEmail", "ALERTS"), "ALERTS");
                    }

                    if (!MyIni.KeyExists("AlertEmail", "ALERTS"))
                    {
                        MyIni.Write("AlertEmail", "Mark.Vanderlaan@owenscorning.com;", "ALERTS");
                        _dtSettings.Rows.Add("AlertEmail", "Mark.Vanderlaan@owenscorning.com;", "ALERTS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("AlertEmail", MyIni.Read("AlertEmail", "ALERTS"), "ALERTS");
                    }

                    if (!MyIni.KeyExists("AccessEnabled", "ACCESS"))
                    {
                        MyIni.Write("AccessEnabled", "0", "ACCESS");
                        _dtSettings.Rows.Add("AccessEnabled", "0", "ACCESS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("AccessEnabled", MyIni.Read("AccessEnabled", "ACCESS"), "ACCESS");
                    }


                    if (!MyIni.KeyExists("DataqSdkEnabled", "ACCESS"))
                    {
                        MyIni.Write("DataqSdkEnabled", "0", "ACCESS");
                        _dtSettings.Rows.Add("DataqSdkEnabled", "0", "ACCESS");
                    }
                    else
                    {
                        _dtSettings.Rows.Add("DataqSdkEnabled", MyIni.Read("DataqSdkEnabled", "ACCESS"), "ACCESS");
                    }
                }

            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: SystemSetupINI; " + ex.Message, 0);
                MessageBox.Show("Error: SystemSetupINI; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }

            return true;
        }

        public static string GetSetting(string seItem, string seType)
        {
            try
            {
                DataRow row = _dtSettings.Select("seType = '" + seType + "' AND seItem = '" + seItem + "'").FirstOrDefault();

                if (row != null)
                    return row["seValue"].ToString();
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: GetSetting; " + ex.Message, -1);
                MessageBox.Show("Error: GetSetting; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return "";
        }

        public static void WriteSetting(string seItem, string seValue, string seType)
        {
            try
            {
                foreach (DataRow dr in _dtSettings.Rows)
                {
                    if (dr["seItem"].ToString() == seItem && dr["seType"].ToString() == seType)
                    {
                        dr["seValue"] = seValue;
                    }
                }

                var MyIni = new IniFile(GetAppPath() + "\\Settings.ini");
                if (MyIni.KeyExists(seItem, seType))
                {
                    MyIni.Write(seItem, seValue, seType);
                }

                string connectionString = GetSetting("Connection", "SQLSERVER");
                string queryString = "UPDATE [Nexus].[dbo].[Settings] SET [seValue] = '@sev'";
                queryString += " WHERE [seType] = '" + seType + "' AND [seItem] = '" + seItem + "'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@sev", seValue);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: WriteSetting; " + ex.Message, -1);
                MessageBox.Show("Error: WriteSetting; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static void KillProcessAndChildren(string n)
        {
            try
            {
                Process[] processlist = Process.GetProcesses();

                foreach (Process theprocess in processlist)
                {
                    if (theprocess.ProcessName.Contains(n))
                    {
                        Process proc = Process.GetProcessById(theprocess.Id);
                        proc.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: KillProcessAndChildren; " + ex.Message, 0);
                MessageBox.Show("Error: KillProcessAndChildren; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static bool ProcessCheck(string n)
        {
            try
            {
                Process[] processlist = Process.GetProcesses();
                foreach (Process theprocess in processlist)
                {
                    if (theprocess.ProcessName.Trim().Equals(n.Trim()))
                    {
                        //Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: GetProcess; " + ex.Message, 0);
                MessageBox.Show("Error: GetProcess; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return false;
        }

        public static void GetMainWindowHandle(string ProcessName, int WindSize)
        {
            try
            {
                var list = Process.GetProcessesByName(ProcessName);

                List<IntPtr> windowList = new List<IntPtr>();

                foreach (Process process in list)
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    //windowList.Add(ThisProcess.MainWindowHandle);
                    {
                        foreach (ProcessThread processThread in process.Threads)
                        {
                            EnumThreadWindows(processThread.Id,
                             (hWnd, lParam) =>
                             {
                                 //Check if Window is Visible or not.
                                 if (!IsWindowVisible((int)hWnd))
                                     return true;

                                 //Get the Window's Title.
                                 StringBuilder title = new StringBuilder(256);
                                 GetWindowText((int)hWnd, title, 256);

                                 //Check if Window has Title.
                                 if (title.Length == 0)
                                     return true;

                                 //windowList.Add(hWnd);

                                 WindowFinder(ProcessName, title.ToString(), WindSize);

                                 return true;
                             }, IntPtr.Zero);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: GetMainWindowHandle; " + ex.Message, 0);
                MessageBox.Show("Error: GetMainWindowHandle; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            //return windowList;
        }

        public static void DoProcess(string cmd, string argv, int wind)
        {
            try
            {
                Process p = new Process();
                //p.StartInfo.UseShellExecute = false;
                //p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = cmd;
                p.StartInfo.Arguments = $" {argv}";
                //p.StartInfo.CreateNoWindow = true;
                if (wind == 1)
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                else if (wind == 2)
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                else
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                p.Start();
                //p.WaitForExit();
                //string output = p.StandardOutput.ReadToEnd();
                p.Dispose();
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: DoProcess; " + ex.Message, 0);
                //MessageBox.Show("Error: DoProcess; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static void WindowFinder(string ProcessName, string WindowName, int WindSize)
        {
            try
            {
                var list = Process.GetProcessesByName(ProcessName);

                List<IntPtr> windowList = new List<IntPtr>();

                foreach (Process process in list)
                {
                    if (process.MainWindowHandle != IntPtr.Zero)
                    //windowList.Add(ThisProcess.MainWindowHandle);
                    {
                        foreach (ProcessThread processThread in process.Threads)
                        {
                            EnumThreadWindows(processThread.Id,
                             (hWnd, lParam) =>
                             {
                                 //Check if Window is Visible or not.
                                 if (!IsWindowVisible((int)hWnd))
                                     return true;

                                 //Get the Window's Title.
                                 StringBuilder title = new StringBuilder(256);
                                 GetWindowText((int)hWnd, title, 256);

                                 //Check if Window has Title.
                                 if (title.Length == 0)
                                     return true;

                                 //windowList.Add(hWnd);

                                 if (title.ToString() == WindowName)
                                     switchWindow(hWnd, WindSize);

                                 return true;
                             }, IntPtr.Zero);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: WindowFinder; " + ex.Message, 0);
                MessageBox.Show("Error: WindowFinder; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            //return windowList;
        }

        public static void switchWindow(IntPtr ProcWindow, int WindSize)
        {
            try
            {
                ShowWindow(ProcWindow, WindSize);
                SetForegroundWindow(ProcWindow);
                //MoveWindow(ProcWindow, 0, 0, 1920, 1080, true);
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: switchWindow; " + ex.Message, 0);
                MessageBox.Show("Error: switchWindow; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static void SystemLogTXT(string str, int msg)
        {
            try
            {
                if (Directory.Exists(GetAppPath() + @"\System_Logs") == false)
                    Directory.CreateDirectory(GetAppPath() + @"\System_Logs");

                string FILE_NAME = GetAppPath() + @"\System_Logs\System_Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                System.IO.StreamWriter objWriter = new System.IO.StreamWriter(FILE_NAME, true);

                objWriter.WriteLine("[" + DateTime.Now.ToLongTimeString().ToString() + "] " + str);
                objWriter.Close();

                string ea = "";
                if (msg == 0)
                    ea = Helper.GetSetting("AdminEmail", "ALERTS");
                else if (msg == 1)
                    ea = Helper.GetSetting("AlertEmail", "ALERTS");

                if (ea != "")
                    Message(ea, "Nexus Alert", str);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: SystemLogTXT; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static void SystemLogSQL(string str, int msg)
        {
            try
            {
                string connectionString = GetSetting("Connection", "SQLSERVER");

                string queryString = "INSERT INTO [Nexus].[dbo].[SystemLog] ([sUser],[sNode],[sPC],[sMessage])";
                queryString += " VALUES (@sUser, @sNode, @sPC, @sMessage)";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@sUser", Environment.UserName);
                    command.Parameters.AddWithValue("@sNode", "Main");
                    command.Parameters.AddWithValue("@sPC", Environment.MachineName);
                    command.Parameters.AddWithValue("@sMessage", str);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                string ea = "";
                if (msg == 0)
                    ea = Helper.GetSetting("AdminEmail", "ALERTS");
                else if (msg == 1)
                    ea = Helper.GetSetting("AlertEmail", "ALERTS");

                if (ea != "")
                    Message(ea, "Nexus Alert", str);
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: SystemLogSQL; " + ex.Message, 0);
                MessageBox.Show("Error: SystemLogSQL; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static int DataLog(string[] str, string acc)
        {
            try
            {
                bool res = true;

                if (Directory.Exists(GetAppPath() + @"\Data_Logs") == false)
                    Directory.CreateDirectory(GetAppPath() + @"\Data_Logs");

                string FILE_NAME = GetAppPath() + @"\Data_Logs\Data_Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                System.IO.StreamWriter objWriter = new System.IO.StreamWriter(FILE_NAME, true);

                string tc = DateTime.Now.ToString("yyMMddHHmmss");

                string connectionString = GetSetting("Connection", "SQLSERVER");
                string queryString = "INSERT INTO [Nexus].[dbo].[DataLog] ([dBadge],[dName],[dType],[dValue])";
                queryString += " VALUES (@dBadge, @dName, @dType, @dValue)";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    for (int i = 0; i < str.Length - 3; i++)
                    {
                        string d = "";
                        string s = "";

                        if (i == 0)
                        {
                            d = "Result";
                            s = str[str.Length - 1];

                            objWriter.WriteLine(tc + "," + str[0] + "," + str[str.Length - 3] + ",Result," + str[str.Length - 1]);
                        }
                        else
                        {
                            d = "Response " + i.ToString();
                            s = str[i];

                            objWriter.WriteLine(tc + "," + str[0] + "," + str[str.Length - 3] + ",Response " + i.ToString() + "," + s);

                            if (s == "1")
                            {
                                Helper.SystemLogSQL("Question Screen Test #" + i.ToString() + "; Response Violation; " + str[str.Length - 3], -1);
                                Helper.SystemLogSQL(str[str.Length - 3] + " Access Denied", 1);
                                res = false;
                            }

                        }

                        command.Parameters.AddWithValue("@dBadge", str[0]);
                        command.Parameters.AddWithValue("@dName", str[str.Length - 3]);
                        command.Parameters.AddWithValue("@dType", d);
                        command.Parameters.AddWithValue("@dValue", s);
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }


                objWriter.Close();

                if (str[str.Length - 1] == "INCOMPLETE")
                {
                    Helper.SystemLogSQL("Temperature Screen Test Result; " + str[str.Length - 1] + "; " + str[str.Length - 3], -1);
                    res = false;
                }

                if (str[str.Length - 1] == "FAIL")
                {
                    Helper.SystemLogSQL("Temperature Screen Test Result; " + str[str.Length - 1] + "; " + str[str.Length - 3], -1);
                    Helper.SystemLogSQL(str[str.Length - 3] + " Access Denied", 1);
                    res = false;
                }

                if (acc == "1")
                {
                    if (res == true)
                        AccessUpdate(str[0], 1);
                    else
                        AccessUpdate(str[0], 0);
                }

                if (res == true)
                    return 2;

            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: DataLog; " + ex.Message, 0);
                MessageBox.Show("Error: DataLog; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return 1;
        }

        public static string GetAccess(string card)
        {
            string acc = "";
            try
            {
                //string queryString = "SELECT [LNAME],[FNAME],[CARDNO],[DESCRP] FROM [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[VW_BADGE] t1 LEFT JOIN [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[CLEAR] t2 ON t1.CLEAR_COD = t2.ID WHERE CARDNO = @CardNum";
                string queryString = "SELECT [DESCRP] FROM [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[BADGE_NEXUS] WHERE CARDNO = @CardNum";

                string connectionString = GetSetting("Connection", "SQLSERVER");
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@CardNum", card);
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter(command);
                    //da.Fill(dataTable);
                    acc = (string)command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: GetAccess; " + ex.Message, 0);
                MessageBox.Show("Error: GetAccess; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return acc;
        }

        public static void AccessUpdate(string card, int typ)
        {
            try
            {
                string connectionString = GetSetting("Connection", "SQLSERVER");

                //string queryString = "UPDATE [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[BADGE_CC] SET [CLEAR_COD] = t1.[ID]";
                //queryString += " FROM (SELECT [ID] FROM [GUESECURITY\\SQLEXPRESS].[PWNT].[dbo].[CLEAR] WHERE [DESCRP] = @Clear) AS t1";
                //queryString += " WHERE CARDNO = @CardNum";

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    SqlCommand command = new SqlCommand(queryString, connection);
                //    command.Parameters.AddWithValue("@CardNum", card);
                //    command.Parameters.AddWithValue("@Clear", access);
                //    connection.Open();
                //    command.ExecuteNonQuery();
                //    connection.Close();
                //}

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("Nexus_Access", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CardNum", card);
                    command.Parameters.AddWithValue("@accType", typ);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: AccessUpdate; " + ex.Message, 0);
                MessageBox.Show("Error: AccessUpdate; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static string GetAppPath()
        {
            int i;
            string strAppPath = "";
            try
            {
                strAppPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                i = strAppPath.Length - 1;
                while (strAppPath.Substring(i, 1) != @"\")
                    i = i - 1;
                strAppPath = strAppPath.Substring(0, i);
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: GetAppPath; " + ex.Message, 0);
                MessageBox.Show("Error: GetAppPath; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return strAppPath;
        }

        public static void Message(string address, string subject, string body)
        {
            try
            {
                string filePath = Helper.GetSetting("NetDir", "ALERTS") + "\\NEXUS_" + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + ".csv";

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
                string delimiter = ",";
                string[][] output = new string[][] { new string[] { address, subject, body } };
                int length = output.GetLength(0);
                StringBuilder sb = new StringBuilder();
                for (int index = 0; index < length; index++)
                    sb.AppendLine(string.Join(delimiter, output[index]));
                File.AppendAllText(filePath, sb.ToString());
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: Message; " + ex.Message, -1);
                //MessageBox.Show("Error: Message; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static void FileClean()
        {
            try
            {
                foreach (string f in Directory.EnumerateFiles(GetSetting("TempPath", "INPUTS"), "*.csv", System.IO.SearchOption.AllDirectories))
                {
                    FileInfo fi = new FileInfo(f);
                    if (fi.LastAccessTime < DateTime.Now.AddDays(-1))
                        fi.Delete();
                }
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: FileClean; " + ex.Message, -1);
                //MessageBox.Show("Error: FileClean; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        public static DataTable GetDataTabletFromCSVFile(string csv_file_path, string[] colFields)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;

                    if (colFields.Length == 0)
                        colFields = csvReader.ReadFields();

                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }

                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemLogTXT("Error: GetDataTabletFromCSVFile; " + ex.Message, 0);
                MessageBox.Show("Error: GetDataTabletFromCSVFile; " + ex.Message, "Nexus", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            return csvData;
        }
    }
}
