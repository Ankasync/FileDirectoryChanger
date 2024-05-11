using Microsoft.Win32.TaskScheduler;
using System.Runtime.InteropServices;
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
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        bool closeState = false;
        bool showState = false;

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

        private void g�sterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ��k��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeState = true;
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "Test Servis";

            // oturum a��ld�ktan sonra tetiklenecek bir tetikleyici olu�turun
            td.Principal.RunLevel = TaskRunLevel.Highest;
            td.Triggers.AddNew(TaskTriggerType.Logon);
            // Tetik her tetiklendi�inde Not Defteri'ni ba�latacak bir eylem 
            td.Actions.Add(new ExecAction(Application.StartupPath + @"\Sesa.exe", Application.StartupPath + @"\Sesa.txt", null));
            // G�revi k�k klas�re kaydedin
            if (!ts.RootFolder.AllTasks.Any(t => t.Name == "Sesa Task"))
            {
                MessageBox.Show("");
                try
                {
                    ts.RootFolder.RegisterTaskDefinition(@"Sesa Task", td);
                    MessageBox.Show("G�rev Zamanlay�c� Kaydedildi");
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
                    timer1.Start();

                }

            }
            else
            {
                timer1.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DestinationFolder.Text == "" || comboBox1.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Bo� B�rak�lamaz");
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
                    DestinationFolder.InnerText = this.DestinationFolder.Text;
                    SourceFolder.InnerText = this.SourceFolder.Text;
                    doc.Save(Application.StartupPath + @"\Config.xml");
                    this.Close();
                    timer1.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            XmlDocument oku = new XmlDocument();
            oku.Load(Application.StartupPath + @"\Config.xml");
            XmlNode tu� = oku.GetElementsByTagName("Key")[0];
            XmlNode SourceFolder = oku.GetElementsByTagName("SourceFolder")[0];
            XmlNode DestinationFolder = oku.GetElementsByTagName("DestinationFolder")[0];
            Keys key;
            Enum.TryParse(tu�.InnerText, out key);
            if (tu�.InnerText == null)
            {
                MessageBox.Show("Atanacak Tu� Belirtilmedi L�tfen Configten Belirtiniz");
            }
            else
            {

                if (GetAsyncKeyState(key) == -32767)
                {

                    DirectoryInfo klasor = new DirectoryInfo(SourceFolder.InnerText);
                    var dosyalar = klasor.GetFiles();

                    // Dosyalar� son de�i�tirilme tarihine g�re s�rala
                    var siraliDosyalar = dosyalar.OrderByDescending(f => f.LastWriteTime);

                    // En son de�i�tirilen dosyay� al
                    var enSonDosya = siraliDosyalar.FirstOrDefault();
                    string hedefDosyaYolu = Path.Combine(DestinationFolder.InnerText, enSonDosya.Name);
                    try
                    {
                        File.Copy(enSonDosya.FullName, hedefDosyaYolu);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                        
                    }
                    if(new FileInfo(hedefDosyaYolu).Length == enSonDosya.Length)
                    MessageBox.Show("Directory Change SuccessFull");
                    else
                    {
                        MessageBox.Show("Bir Sorun Olu�mu� Olabilir Manuel Olarak Kontrol Etmenizi Tavsiye Ederiz");
                    }
                }

            }
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string directory = dialog.SelectedPath + @"\";
            DestinationFolder.Text = directory;
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string directory = dialog.SelectedPath + @"\";
            SourceFolder.Text = directory;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
