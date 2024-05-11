using GlobalHookExample;
using Microsoft.Win32.TaskScheduler;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
namespace FileDirectoryChanger
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        
        }
        string SourceFolder = "";
        string DestinationFolder = "";
 
        Keys key;
        globalKeyboardHook KayboardListener = new globalKeyboardHook();

        bool closeState = false;
        bool showState = false;
        void handleKeyPress(object sender, KeyEventArgs e)
        {
            //Yapýlmasýný istediðiniz kodlar burada yer alacak
            //Burasý tuþa basýldýðý an çalýþýr

  
            if (e.KeyCode == key)
            {

                DirectoryInfo klasor = new DirectoryInfo(SourceFolder);
                var files = klasor.GetFiles();

                // Dosyalarý son deðiþtirilme tarihine göre sýrala
                var sequentialFiles = files.OrderByDescending(f => f.LastWriteTime);

                // En son deðiþtirilen dosyayý al
                var lastFile = sequentialFiles.FirstOrDefault();
                string DestinationFolderPath = Path.Combine(DestinationFolder, lastFile.Name);
                try
                {
                    File.Copy(lastFile.FullName, DestinationFolderPath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;

                }
                if (new FileInfo(DestinationFolderPath).Length == lastFile.Length)
                    MessageBox.Show("Directory Change SuccessFull");
                else
                {
                    MessageBox.Show("A Problem Might Occur We Recommend You Check Manually");
                }

            }
            e.Handled = false;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!showState)
            {
                this.Hide();

            }
            else
            {
                showState = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closeState) e.Cancel = true;
            this.Hide();

        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void HideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeState = true;
            Application.Exit();
        }
        public void ConfigureHook()
        {
            KayboardListener = new globalKeyboardHook();
            XmlDocument Read = new XmlDocument();
            Read.Load(Application.StartupPath + @"\Config.xml");
            XmlNode KeyNode = Read.GetElementsByTagName("Key")[0];
            XmlNode SourceFolderNode = Read.GetElementsByTagName("SourceFolder")[0];
            XmlNode DestinationFolderNode = Read.GetElementsByTagName("DestinationFolder")[0];
            SourceFolder = SourceFolderNode.InnerText;
            DestinationFolder = DestinationFolderNode.InnerText;
            Enum.TryParse(KeyNode.InnerText, out key);
            KayboardListener.HookedKeys.Add(key);
            KayboardListener.KeyUp += new KeyEventHandler(handleKeyPress);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
  
 
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "FileDirectoryChanger Service";
            td.Principal.RunLevel = TaskRunLevel.Highest;
            td.Triggers.AddNew(TaskTriggerType.Logon);
            td.Actions.Add(new ExecAction(Application.StartupPath + @"\FileDirectoryChanger.exe", Application.StartupPath + @"\FileDirectoryChanger.txt", null));
            if (!ts.RootFolder.AllTasks.Any(t => t.Name == "FileDirectoryChanger Task"))
            {
                MessageBox.Show("");
                try
                {
                    ts.RootFolder.RegisterTaskDefinition(@"FileDirectoryChanger Task", td);
                    MessageBox.Show("Task Scheduler Saved");
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message);
                }

            }
            if (!File.Exists(Application.StartupPath + @"\Config.xml"))
            {
                MessageBox.Show("Select Key And Directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                showState = true;
                this.Show();
                if (File.Exists(Application.StartupPath + @"\Config.xml"))
                {
                    ConfigureHook();

                }

            }
            else
            {
                ConfigureHook();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DestinationFoldertxt.Text == "" || comboBox1.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Cannot Be Left Blank");
            }
            else
            {
                try
                {
                    Keys key;
                    Enum.TryParse(comboBox1.Text, out key);
                    XmlDocument doc = new XmlDocument();
                    XmlElement Settings = doc.CreateElement("Settings");
                    doc.AppendChild(Settings);
                    XmlElement SourceFolder = doc.CreateElement("SourceFolder");
                    Settings.AppendChild(SourceFolder);
                    XmlElement DestinationFolder = doc.CreateElement("DestinationFolder");
                    Settings.AppendChild(DestinationFolder);

                    XmlElement Key = doc.CreateElement("Key");
                    Settings.AppendChild(Key);


                    Key.InnerText = key.ToString();
                    DestinationFolder.InnerText = this.DestinationFoldertxt.Text;
                    SourceFolder.InnerText = this.SourceFoldertxt.Text;
                    doc.Save(Application.StartupPath + @"\Config.xml");

                    ConfigureHook();

                    this.Close();
            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

     

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string directory = dialog.SelectedPath + @"\";
            DestinationFoldertxt.Text = directory;
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string directory = dialog.SelectedPath + @"\";
            SourceFoldertxt.Text = directory;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
