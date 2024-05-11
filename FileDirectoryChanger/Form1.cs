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
        string tus = "";
        Keys key;
        globalKeyboardHook klavyeDinleyicisi = new globalKeyboardHook();

        bool closeState = false;
        bool showState = false;
        void handleKeyPress(object sender, KeyEventArgs e)
        {
            //Yapýlmasýný istediðiniz kodlar burada yer alacak
            //Burasý tuþa basýldýðý an çalýþýr

            if (tus == null)
            {
                MessageBox.Show("Atanacak Tuþ Belirtilmedi Lütfen Configten Belirtiniz");
                this.Show();
            }
            else if (e.KeyCode == key)
            {

                DirectoryInfo klasor = new DirectoryInfo(SourceFolder);
                var dosyalar = klasor.GetFiles();

                // Dosyalarý son deðiþtirilme tarihine göre sýrala
                var siraliDosyalar = dosyalar.OrderByDescending(f => f.LastWriteTime);

                // En son deðiþtirilen dosyayý al
                var enSonDosya = siraliDosyalar.FirstOrDefault();
                string hedefDosyaYolu = Path.Combine(DestinationFolder, enSonDosya.Name);
                try
                {
                    File.Copy(enSonDosya.FullName, hedefDosyaYolu);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;

                }
                if (new FileInfo(hedefDosyaYolu).Length == enSonDosya.Length)
                    MessageBox.Show("Directory Change SuccessFull");
                else
                {
                    MessageBox.Show("Bir Sorun Oluþmuþ Olabilir Manuel Olarak Kontrol Etmenizi Tavsiye Ederiz");
                }

            }
            else
            {

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

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void çýkýþToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeState = true;
            Application.Exit();
        }
        public void ConfigureHook()
        {
            klavyeDinleyicisi = new globalKeyboardHook();
            XmlDocument oku = new XmlDocument();
            oku.Load(Application.StartupPath + @"\Config.xml");
            XmlNode tuþ = oku.GetElementsByTagName("Key")[0];
            XmlNode SourceFolderNode = oku.GetElementsByTagName("SourceFolder")[0];
            XmlNode DestinationFolderNode = oku.GetElementsByTagName("DestinationFolder")[0];
            SourceFolder = SourceFolderNode.InnerText;
            DestinationFolder = DestinationFolderNode.InnerText;
            Enum.TryParse(tuþ.InnerText, out key);
            klavyeDinleyicisi.HookedKeys.Add(key);
            klavyeDinleyicisi.KeyUp += new KeyEventHandler(handleKeyPress);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //basýldýktan sonra ikinci olarak burasý çalýþýr
 
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "Test Servis";

            // oturum açýldýktan sonra tetiklenecek bir tetikleyici oluþturun
            td.Principal.RunLevel = TaskRunLevel.Highest;
            td.Triggers.AddNew(TaskTriggerType.Logon);
            // Tetik her tetiklendiðinde Not Defteri'ni baþlatacak bir eylem 
            td.Actions.Add(new ExecAction(Application.StartupPath + @"\Sesa.exe", Application.StartupPath + @"\Sesa.txt", null));
            // Görevi kök klasöre kaydedin
            if (!ts.RootFolder.AllTasks.Any(t => t.Name == "Sesa Task"))
            {
                MessageBox.Show("");
                try
                {
                    ts.RootFolder.RegisterTaskDefinition(@"Sesa Task", td);
                    MessageBox.Show("Görev Zamanlayýcý Kaydedildi");
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
                MessageBox.Show("Boþ Býrakýlamaz");
            }
            else
            {
                try
                {
                    Keys key;
                    Enum.TryParse(comboBox1.Text, out key);

                    XmlDocument doc = new XmlDocument();
                    XmlElement Ayarlar = doc.CreateElement("Settings");
                    doc.AppendChild(Ayarlar);
                    XmlElement SourceFolder = doc.CreateElement("SourceFolder");
                    Ayarlar.AppendChild(SourceFolder);
                    XmlElement DestinationFolder = doc.CreateElement("DestinationFolder");
                    Ayarlar.AppendChild(DestinationFolder);

                    XmlElement Key = doc.CreateElement("Key");
                    Ayarlar.AppendChild(Key);


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
