using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace DevZLauncher
{
    public partial class AZ_DevZ_Launcher : Form
    {
        private Process serverProcess;
        private Process clientProcess;
        private string ptpNotSet = "You have not set the 'Path To Profile' Directory!";


        public AZ_DevZ_Launcher()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void SaveClientSettings()
        {
            Properties.Settings.Default.Client_PTE = Client_PTE_TextBox.Text;
            Properties.Settings.Default.Client_SIP = Client_SIP_TextBox.Text;
            Properties.Settings.Default.Client_SP = Client_SP_TextBox.Text;
            Properties.Settings.Default.Client_SPass = Client_SPass_TextBox.Text;
            Properties.Settings.Default.Client_Mods = Client_Mods_TextBox.Text;
            string clientParams = "";

            if (Client_NS_CBox.Checked) clientParams += "-nosplash ";
            if (Client_NPause_CBox.Checked) clientParams += "-nopause ";
            if (Client_NB_CBox.Checked) clientParams += "-noBenchmark ";
            if (Client_SD_CBox.Checked) clientParams += "-scriptDebug ";
            if (Client_W_CBox.Checked) clientParams += "-window ";

            Properties.Settings.Default.Client_Params = clientParams;

            Properties.Settings.Default.Save();
        }

        private void SaveModSettings()
        {
            Properties.Settings.Default.Mod_MSD = Mod_MSD_TextBox.Text;
            Properties.Settings.Default.Mod_OCD = Mod_OCD_TextBox.Text;
            Properties.Settings.Default.Mod_OSD = Mod_OSD_TextBox.Text;
            string modParams = "";

            if (Mod_PTS_CBox.Checked) modParams += "-pts ";
            if (Mod_PTC_CBox.Checked) modParams += "-ptc ";
            if (Mod_RRA_CBox.Checked) modParams += "-F ";
            if (Mod_CMF_CBox.Checked) modParams += "-G ";
            if (Mod_DP_CBox.Checked) modParams += "-P ";
            if (Mod_AUP_CBox.Checked) modParams += "-U ";

            Properties.Settings.Default.Mod_Params = modParams;

            Properties.Settings.Default.Save();
        }

        private void SaveServerSettings()
        {
            Properties.Settings.Default.Server_PTE = Server_PTE_TextBox.Text;
            Properties.Settings.Default.Server_PTP = Server_PTP_TextBox.Text;
            Properties.Settings.Default.Server_PTC = Server_PTC_TextBox.Text;
            Properties.Settings.Default.Server_PTBE = Server_PTBE_TextBox.Text;
            Properties.Settings.Default.Server_Mods = Server_Mods_TextBox.Text;
            Properties.Settings.Default.Server_Port = Server_Port_TextBox.Text;
            Properties.Settings.Default.Server_CPUC = Server_CPUC_TextBox.Text;
            string serverParams = "";

            if (Server_DL_CBox.Checked) serverParams += "-dologs ";
            if (Server_AL_CBox.Checked) serverParams += "-adminlogs ";
            if (Server_NetL_CBox.Checked) serverParams += "-netlog ";
            if (Server_FC_CBox.Checked) serverParams += "-freezecheck ";
            if (Server_NFP_CBox.Checked) serverParams += "-noFilePatching ";
            if (Server_Mods_CBox.Checked) serverParams += "-mods ";

            Properties.Settings.Default.Server_Params = serverParams;

            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            //Loading Client Settings
            Client_PTE_TextBox.Text = Properties.Settings.Default.Client_PTE;
            Client_SIP_TextBox.Text = Properties.Settings.Default.Client_SIP;
            Client_SP_TextBox.Text = Properties.Settings.Default.Client_SP;
            Client_SPass_TextBox.Text = Properties.Settings.Default.Client_SPass;
            Client_Mods_TextBox.Text = Properties.Settings.Default.Client_Mods;
            var clientParams = Properties.Settings.Default.Client_Params.Split(' ');

            foreach(var option in clientParams)
            {
                switch (option)
                {
                    case "-nosplash":
                        Client_NS_CBox.Checked = true;
                        break;
                    case "-nopause":
                        Client_NPause_CBox.Checked = true;
                        break;
                    case "-noBenchmark":
                        Client_NB_CBox.Checked = true;
                        break;
                    case "-scriptDebug":
                        Client_SD_CBox.Checked = true;
                        break;
                    case "-window":
                        Client_W_CBox.Checked = true;
                        break;
                }
            }


            //Loading Mod Settings
            Mod_MSD_TextBox.Text = Properties.Settings.Default.Mod_MSD;
            Mod_OCD_TextBox.Text = Properties.Settings.Default.Mod_OCD;
            Mod_OSD_TextBox.Text = Properties.Settings.Default.Mod_OSD;
            var modParams = Properties.Settings.Default.Mod_Params.Split(' ');

            foreach(var option in modParams)
            {
                switch (option)
                {
                    case "-pts":
                        Mod_PTS_CBox.Checked = true;
                        break;
                    case "-ptc":
                        Mod_PTC_CBox.Checked = true;
                        break;
                    case "-F":
                        Mod_RRA_CBox.Checked = true;
                        break;
                    case "-G":
                        Mod_CMF_CBox.Checked = true;
                        break;
                    case "-P":
                        Mod_DP_CBox.Checked = true;
                        break;
                    case "-U":
                        Mod_AUP_CBox.Checked = true;
                        break;
                }
            }

            //Loading Server Settings
            Server_PTE_TextBox.Text = Properties.Settings.Default.Server_PTE;
            Server_PTP_TextBox.Text = Properties.Settings.Default.Server_PTP;
            Server_PTC_TextBox.Text = Properties.Settings.Default.Server_PTC;
            Server_PTBE_TextBox.Text = Properties.Settings.Default.Server_PTBE;
            Server_Mods_TextBox.Text = Properties.Settings.Default.Server_Mods;
            Server_Port_TextBox.Text = Properties.Settings.Default.Server_Port;
            Server_CPUC_TextBox.Text = Properties.Settings.Default.Server_CPUC;
            var serverParams = Properties.Settings.Default.Server_Params.Split(' ');

            foreach(var option in serverParams)
            {
                switch (option)
                {
                    case "-dologs":
                        Server_DL_CBox.Checked = true;
                        break;
                    case "-adminlogs":
                        Server_AL_CBox.Checked = true;
                        break;
                    case "-netlog":
                        Server_NetL_CBox.Checked = true;
                        break;
                    case "-freezecheck":
                        Server_FC_CBox.Checked = true;
                        break;
                    case "-noFilePatching":
                        Server_NFP_CBox.Checked = true;
                        break;
                    case "-mods":
                        Server_Mods_CBox.Checked = true;
                        break;
                }
            }
        }

        private void StartServer()
        {
            var ServerParamaters = $" -config={Server_PTC_TextBox.Text} -Port={Server_Port_TextBox.Text} -profiles={Server_PTP_TextBox.Text}";

            if (Server_PTBE_TextBox.Text != "") ServerParamaters += $" -BEpath={Server_PTBE_TextBox.Text}";

            if (Server_CPUC_TextBox.Text != "") ServerParamaters += $" -cpuCount={Server_CPUC_TextBox.Text}";

            if (Server_Mods_TextBox.Text != "" && Server_Mods_CBox.Checked) ServerParamaters += $" -mod={Server_Mods_TextBox.Text}";

            if (Server_DL_CBox.Checked) ServerParamaters += " -dologs";
            if (Server_AL_CBox.Checked) ServerParamaters += " -adminlogs";
            if (Server_NetL_CBox.Checked) ServerParamaters += " -netlog";
            if (Server_FC_CBox.Checked) ServerParamaters += " -freezecheck";
            if (Server_NFP_CBox.Checked) ServerParamaters += " -noFilePatching";

            serverProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Server_PTE_TextBox.Text,
                    Arguments = ServerParamaters,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            serverProcess.Start();

            ControlPropertyThreadSafe(Server_Start_Btn, "Text", "STOP SERVER");
            ControlPropertyThreadSafe(Server_UFSS_Btn, "Text", "STOP SERVER AND CLIENT");
        }

        private void StartClient()
        {
            var ClientParamaters = "";

            //Connect to Server Paramaters
            if (Client_SIP_TextBox.Text != "" && Client_SP_TextBox.Text != "")
                ClientParamaters += $" -connect={Client_SIP_TextBox.Text} -port={Client_SP_TextBox.Text}";
            if (Client_SPass_TextBox.Text != "" && Client_SIP_TextBox.Text != "" && Client_SP_TextBox.Text != "")
                ClientParamaters += $" -password={Client_SPass_TextBox.Text}";

            //Mods paramaters
            if (Client_Mods_TextBox.Text != "")
                ClientParamaters += $" -mod={Client_Mods_TextBox.Text}";

            //Checkbox Paramaters
            if (Client_NS_CBox.Checked) ClientParamaters += $" -nosplash";
            if (Client_NPause_CBox.Checked) ClientParamaters += $" -nopause";
            if (Client_NB_CBox.Checked) ClientParamaters += $" -noBenchmark";
            if (Client_SD_CBox.Checked) ClientParamaters += $" -scriptDebug=true";
            if (Client_W_CBox.Checked) ClientParamaters += $" -window";

            clientProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Client_PTE_TextBox.Text,
                    Arguments = ClientParamaters,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            clientProcess.Start();

            ControlPropertyThreadSafe(Client_SC_Btn, "Text", "STOP CLIENT");
        }

        private void PackAddon(string sourceDir, string outputDir)
        {

            var outputDirSplit = outputDir.Split('\\');
            if (outputDirSplit[outputDirSplit.Length - 1] != "Addons")
                outputDir += "\\Addons";

            var makePBOParamaters = $"/C makepbo -A";

            //Checkbox Paramaters
            if (Mod_RRA_CBox.Checked) makePBOParamaters += $" -F";
            if (Mod_CMF_CBox.Checked) makePBOParamaters += $" -G";
            if (Mod_DP_CBox.Checked) makePBOParamaters += $" -P";
            if (Mod_AUP_CBox.Checked) makePBOParamaters += $" -U";

            if (Directory.GetFiles(@""+sourceDir, "config.cpp").Length == 0)
            {
                string[] subdirEntries = Directory.GetDirectories(@"" + sourceDir);
                foreach (string subDir in subdirEntries)
                {
                    LoadSubDirs(subDir, makePBOParamaters, outputDir);
                }
            }
            else
            {
                makePBOParamaters += $" \"{sourceDir}\" \"{outputDir}\"";
                Process.Start("cmd.exe", makePBOParamaters);
            }
        }

        private void LoadSubDirs(string dir, string makePBOParams, string outputDir)
        {
            if(Directory.GetFiles(dir, "config.cpp").Length != 0)
            {
                makePBOParams += $" \"{dir.ToString()}\" \"{outputDir}\"";
                Process.Start("cmd.exe", makePBOParams);
                return;
            }
            string[] subDirEntries = Directory.GetDirectories(dir);
            foreach (string subDir in subDirEntries)
            {
                LoadSubDirs(subDir, makePBOParams, outputDir);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (Server_PTP_TextBox.Text != "")
                {
                    string ptpPath = Server_PTP_TextBox.Text;
                    DirectoryInfo ptADMLog = new DirectoryInfo(@"" + ptpPath);
                    var admLogFiles = ptADMLog.GetFiles("DayZServer_x64" + "*.ADM").OrderByDescending(f => f.LastWriteTime).First();
                    Process.Start(admLogFiles.FullName);
                }
                else
                {
                    MessageBox.Show(ptpNotSet, "Warning!");
                }
            }
            catch
            {
                MessageBox.Show("There is no ADM files in the given directory!", "Warning!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (Server_PTP_TextBox.Text != "")
                {
                    string ptpPath = Server_PTP_TextBox.Text;
                    DirectoryInfo ptCLog = new DirectoryInfo(@"" + ptpPath);
                    var cLogFiles = ptCLog.GetFiles("crash" + "*.log").OrderByDescending(f => f.LastWriteTime).First();
                    Process.Start(cLogFiles.FullName);
                }
                else
                {
                    MessageBox.Show(ptpNotSet, "Warning!");
                }
            }
            catch
            {
                MessageBox.Show("There is no 'crash.log' in the given directory!", "Warning!");
            }
        }

        private void Server_Port_Label_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Server_CPUC_Label_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Server_FC_CBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Server_DL_CBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Server_AL_CBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Server_NFP_CBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ServerPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Client_PTE_Btn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog clientExePath = new OpenFileDialog())
            {
                clientExePath.InitialDirectory = "C:\\";
                clientExePath.Filter = "DayZ|DayZ_BE.exe";
                clientExePath.FilterIndex = 1;
                clientExePath.RestoreDirectory = true;

                if(clientExePath.ShowDialog() == DialogResult.OK) Client_PTE_TextBox.Text = clientExePath.FileName;
            }
        }

        private void Client_MSD_Btn_Click(object sender, EventArgs e)
        {
            FolderSelectDialog fsd = new FolderSelectDialog("Mod Source Folder");
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                Mod_MSD_TextBox.Text = fsd.FileName;
            }
            
        }

        private void Mod_OCD_Btn_Click(object sender, EventArgs e)
        {
            FolderSelectDialog fsd = new FolderSelectDialog("Client Output Directory");
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                Mod_OCD_TextBox.Text = fsd.FileName;
            }
        }

        private void Mod_OSD_Btn_Click(object sender, EventArgs e)
        {
            FolderSelectDialog fsd = new FolderSelectDialog("Server Output Directory");
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                Mod_OSD_TextBox.Text = fsd.FileName;
            }
        }

        private void Server_PTE_Btn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog serverExePath = new OpenFileDialog())
            {
                serverExePath.InitialDirectory = "C:\\";
                serverExePath.Filter = "DayZServer|DayZServer_x64.exe";
                serverExePath.FilterIndex = 1;
                serverExePath.RestoreDirectory = true;

                if (serverExePath.ShowDialog() == DialogResult.OK) Server_PTE_TextBox.Text = serverExePath.FileName;
            }
        }

        private void Server_PTP_Btn_Click(object sender, EventArgs e)
        {
            FolderSelectDialog fsd = new FolderSelectDialog("Server Profile Directory");
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                Server_PTP_TextBox.Text = fsd.FileName;
            }
        }
        private void Server_PTC_Btn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog serverCfgPath = new OpenFileDialog())
            {
                serverCfgPath.InitialDirectory = "C:\\";
                serverCfgPath.Filter = "serverDZ|serverDZ.cfg";
                serverCfgPath.FilterIndex = 1;
                serverCfgPath.RestoreDirectory = true;

                if (serverCfgPath.ShowDialog() == DialogResult.OK) Server_PTC_TextBox.Text = serverCfgPath.SafeFileName;
            }
        }

        private void Server_PTBE_Btn_Click(object sender, EventArgs e)
        {
            FolderSelectDialog fsd = new FolderSelectDialog("Server Profile Directory");
            if (fsd.ShowDialog(IntPtr.Zero))
            {
                Server_PTBE_TextBox.Text = fsd.FileName;
            }
        }

        private void Client_SS_Btn_Click(object sender, EventArgs e)
        {
            SaveClientSettings();
        }

        private void Mod_SS_Btn_Click(object sender, EventArgs e)
        {
            SaveModSettings();
        }

        private void Server_SS_Btn_Click(object sender, EventArgs e)
        {
            SaveServerSettings();
        }

        private void Server_CS_Btn_Click(object sender, EventArgs e)
        {
            if (serverProcess != null)
            {
                try
                {
                    serverProcess.Kill();
                }
                catch { };

                Server_Start_Btn.Text = "START SERVER";
                Server_UFSS_Btn.Text = "UPDATE FILES AND START SERVER";
                serverProcess = null;
            }
            else
            {
                var serverThread = new Thread(new ThreadStart(StartServer));
                serverThread.Start();
            }
        }

        private void Mod_OSF_Btn_Click(object sender, EventArgs e)
        {
            if (Mod_MSD_Label.Text != "")
            {
                string msdPath = Mod_MSD_TextBox.Text;
                Process.Start("Explorer.exe", @"" + msdPath);
            }
            else
            {
                MessageBox.Show("Mod Source Directory has not been set!", "Warning!");
            }
        }

        private void Mod_OCO_Btn_Click(object sender, EventArgs e)
        {
            if (Mod_OCD_TextBox.Text != "")
            {
                string ocdPath = Mod_OSD_TextBox.Text;
                Process.Start("Explorer.exe", @"" + ocdPath);
            }
            else
            {
                MessageBox.Show("Client Output Directory has not been set!", "Warning!");
            }
        }

        private void Mod_OSO_Btn_Click(object sender, EventArgs e)
        {
            if (Mod_OSD_TextBox.Text != "")
            {
                string osdPath = Mod_OSD_TextBox.Text;
                Process.Start("Explorer.exe", @"" + osdPath);
            }
            else
            {
                MessageBox.Show("Server Output Directory has not been set!", "Warning!");
            }
        }

        private void Server_OLF_Btn_Click(object sender, EventArgs e)
        {
            if (Server_PTP_TextBox.Text != "")
            {
                string ptpPath = Server_PTP_TextBox.Text;
                Process.Start("Explorer.exe", @"" + ptpPath);
            }
            else
            {
                MessageBox.Show(ptpNotSet, "Warning!");
            }
        }

        private void Server_LRPT_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Server_PTP_TextBox.Text != "")
                {
                    string ptpPath = Server_PTP_TextBox.Text;
                    DirectoryInfo ptRTP = new DirectoryInfo(@"" + ptpPath);
                    var rtpFiles = ptRTP.GetFiles("DayZServer_x64" + "*.RPT").OrderByDescending(f => f.LastWriteTime).First();
                    Process.Start(rtpFiles.FullName);
                }
                else
                {
                    MessageBox.Show(ptpNotSet, "Warning!");
                }
            }
            catch
            {
                MessageBox.Show("There is no RPT Files in the given directort!", "Warning!");
            }
        }

        private void Server_LSL_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Server_PTP_TextBox.Text != "")
                {
                    string ptpPath = Server_PTP_TextBox.Text;
                    DirectoryInfo ptSLog = new DirectoryInfo(@"" + ptpPath);
                    var sLogFiles = ptSLog.GetFiles("script" + "*.log").OrderByDescending(f => f.LastWriteTime).First();
                    Process.Start(sLogFiles.FullName);
                }
                else
                {
                    MessageBox.Show(ptpNotSet, "Warning!");
                }
            }
            catch
            {
                MessageBox.Show("There is no 'script.log' in the given directory!", "Warning!");
            }
        }

        private void Server_LCL_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Server_PTP_TextBox.Text != "")
                {
                    string ptpPath = Server_PTP_TextBox.Text;
                    DirectoryInfo ptSCLog = new DirectoryInfo(@"" + ptpPath);
                    var scLogFiles = ptSCLog.GetFiles("server_console" + "*.log").OrderByDescending(f => f.LastWriteTime).First();
                    Process.Start(scLogFiles.FullName);
                }
                else
                {
                    MessageBox.Show(ptpNotSet, "Warning!");
                }
            }
            catch
            {
                MessageBox.Show("There is no 'server_console.log' in the given directory!", "Warning!");
            }
        }

        private delegate void ControlMethodThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void ControlMethodThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new ControlMethodThreadSafeDelegate
                        (ControlMethodThreadSafe),
                    new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.InvokeMethod,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }

        private delegate void ControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void ControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new ControlPropertyThreadSafeDelegate
                        (ControlPropertyThreadSafe),
                    new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }

        private void Server_Start_Btn_Click(object sender, EventArgs e)
        {
            if (serverProcess != null || clientProcess != null)
            {
                try
                {
                    serverProcess.Kill();
                }
                catch { };

                Server_Start_Btn.Text = "START SERVER";
                Server_UFSS_Btn.Text = "UPDATE FILES AND START SERVER";
                serverProcess = null;

                try
                {
                    foreach (Process proc in Process.GetProcessesByName("DayZ_x64"))
                    {
                        proc.Kill();
                    }
                }
                catch { }
                Client_SC_Btn.Text = "START CLIENT";
                clientProcess = null;
            }
            else
            {
                if (Mod_PTC_CBox.Checked)
                    PackAddon(Mod_MSD_TextBox.Text, Mod_OCD_TextBox.Text);
                if (Mod_PTS_CBox.Checked)
                    PackAddon(Mod_MSD_TextBox.Text, Mod_OSD_TextBox.Text);

                var serverThread = new Thread(new ThreadStart(StartServer));
                serverThread.Start();
            }
        }

        private void Client_SC_Btn_Click(object sender, EventArgs e)
        {
            if (clientProcess != null)
            {
                try
                {
                    foreach (Process proc in Process.GetProcessesByName("DayZ_x64"))
                    {
                        proc.Kill();
                    }
                }
                catch { }
                Client_SC_Btn.Text = "START CLIENT";
                clientProcess = null;
            }
            else
            {
                var clientThread = new Thread(new ThreadStart(StartClient));
                clientThread.Start();
            }
        }

        private void Mod_PA_Btn_Click(object sender, EventArgs e)
        {
            if (Mod_PTC_CBox.Checked)
                PackAddon(Mod_MSD_TextBox.Text, Mod_OCD_TextBox.Text);
            if (Mod_PTS_CBox.Checked)
                PackAddon(Mod_MSD_TextBox.Text, Mod_OSD_TextBox.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Config
        }
        private void AZ_DevZ_Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
