using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;











namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        //сайт с новостями
        private string NewsSite = "http://tandots.ru/news.htm";
        //первая ссылка
        private string URL1 = "http://www.tandots.ru/TL.exe";
        //вторая
        private string URL2 = "http://taruu.ru/tandots/TL.exe";
        private string URL;
        //название папки
        private string DivGame = @"\tandots";

        private string path;
        private string pathDic;
        private string VersionС;
        private string VersionS;
        private string TandotsUp;

        public Form1()
        {
            InitializeComponent();
            InstConfigDiv();
            Testdiv();
            pathtextbox.Text = path;
            URL = URL1;
            serverVer();
            label3.Text = "Версия клиента: " + Properties.Settings.Default.ver;
            webBrowser1.Navigate("http://tandots.ru/news.html");
            webBrowser1.Refresh();
            TandotsU();
        }
          

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateClient();
        }
        
        //progress bar
        void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Загрузка " + Convert.ToString(progressBar1.Value) + "%";

        }
        
  
        



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            URL = "http://taruu.ru/tandots/TL.exe";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            URL = "http://www.tandots.ru/TL.exe";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChooseFolder();
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        // save config
        private void button4_Click(object sender, EventArgs e)
        {
            SaveConfig();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            InstConfigDiv();
            Directory.SetCurrentDirectory(path + DivGame);
            ProcessStartInfo startbat= new ProcessStartInfo();
            startbat.CreateNoWindow = false;
            startbat.UseShellExecute = false;
            startbat.FileName = pathDic + DivGame + @"\TL.exe";
            startbat.WindowStyle = ProcessWindowStyle.Hidden;
            startbat.Arguments = "";
            Process.Start(startbat);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InstConfigDiv();
            if (VersionС != VersionS & !System.IO.Directory.Exists(path + DivGame + @"\.minecraft") & !System.IO.Directory.Exists(path + DivGame + @"\TLauncher") & !System.IO.File.Exists(path + DivGame + @"\start.exe"))
            {
                MessageBox.Show("Устаревшая версия");
                UpdateClient();
            }
            else
            {
                label1.Text = "Запуск";
                Start();
                this.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            pathtextbox.Text = "12313123";
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.tandots.ru");
        }
        //Действия крч:
        //создание новой папки
        private void Testdiv()
        {
            Directory.CreateDirectory(path + @"\tandots");

        }
        //версия приложеня
        private void TandotsU()
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead("http://www.tandots.ru/Launcher/verapp.txt"))
            using (var reader = new StreamReader(stream))
                TandotsUp = reader.ReadToEnd();
            this.Text = "Tandots updater ver: " + TandotsUp;


        }
        //узнаем версию сервера
        private void serverVer()
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead("http://www.tandots.ru/Launcher/TL/ver.txt"))
            using (var reader = new StreamReader(stream))
                VersionS = reader.ReadToEnd();
            label4.Text = "Версия сервера: " + VersionS;

        }
        //прогрузка конфигов
        private void InstConfigDiv()
        {
            VersionС = Properties.Settings.Default.ver;
            path = Properties.Settings.Default.directory;
            pathDic = @path;

        }
        //удаление
        private void UpdateClient()
        {
            InstConfigDiv();
            if (System.IO.Directory.Exists(path + DivGame + @"\.minecraft"))
            {
                DeliteUpdate();
                if (System.IO.File.Exists(path + DivGame + @"\TL.exe"))
                {
                    File.Delete(path + DivGame + @"\TL.exe");
                }
                DownloadZip();
            }
            else
            {
                if (System.IO.File.Exists(path + DivGame + @"\TL.exe"))
                {
                    File.Delete(path + DivGame + @"\TL.exe");
                }
                DownloadZip();

            }

        }
        //скачивание
        private void DownloadZip()
        {
            InstConfigDiv();

            WebClient webClient = new WebClient();

            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFileAsync(new Uri(URL), pathDic + @"\tandots" + @"\TL.exe");

        }
        //делаем папки
        private void CretDiv()
        {
            Directory.CreateDirectory(path + DivGame + @"\.minecraft\home\Forge-1.7\mods");
            Directory.CreateDirectory(path + DivGame + @"\Launcher");
            Directory.CreateDirectory(path + DivGame + @"\minecraft\versions");
            Directory.CreateDirectory(path + DivGame + @"\minecraft\home\Forge-1.7");
            Directory.CreateDirectory(path + DivGame + @"\minecraft\home\1.10");

        }
        // действия после скачивание архива
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            label1.Text = "Скачивание завершено";
            Zip();
            VersionС = VersionS;
            Properties.Settings.Default.ver = VersionС;
            Properties.Settings.Default.Save();
            label3.Text = "Версия клиента: " + Properties.Settings.Default.ver;

        }

        //действия с zip
        private void Zip()
        {
            label1.Text = "Распаковка";
            InstConfigDiv();
            Directory.SetCurrentDirectory(path + DivGame);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = pathDic + DivGame + @"\TL.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = "";
            Process.Start(startInfo);
            label1.Text = "Готово к игре";
        }
        //удаление фалов перед обнавлением
        private void DeliteUpdate()
        {
            SortedSet<string> filesToIgnore = new SortedSet<string>(StringComparer.OrdinalIgnoreCase) {
                "servers.dat",
                "resourcepacks",
                "saves",
                "config",
                "server-resource-packs",
                "logs",
                ""
            };
            foreach (var filePath in Directory.EnumerateFiles(path + DivGame + @"\.minecraft\home"))
            {
                string fileName = Path.GetFileName(filePath);
                if (!filesToIgnore.Contains(fileName))
                    File.Delete(filePath);
            }

            foreach (var dirPath in Directory.EnumerateDirectories(path + DivGame + @"\.minecraft\home"))
            {
                string DirName = Path.GetFileName(dirPath);
                if (!filesToIgnore.Contains(DirName))
                    Directory.Delete(@dirPath, true);
            }

            //Directory.GetDirectories

        }
        //че стартуем?
        private void Start()
        {
            InstConfigDiv();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = pathDic + DivGame + @"\start.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "";
            Process.Start(startInfo);

        }
        //save config
        private void SaveConfig()
        {
            path = pathtextbox.Text;
            if (path == "")
            {
                MessageBox.Show("Выберети папку!");
                ChooseFolder();

            }
            Directory.CreateDirectory(path);
            Properties.Settings.Default.directory = path;
            Properties.Settings.Default.Save();
            MessageBox.Show("Сохранено");
            InstConfigDiv();
            Testdiv();

        }
        //выбор папки
        public void ChooseFolder()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pathtextbox.Text = folderBrowserDialog1.SelectedPath;
                path = folderBrowserDialog1.SelectedPath;
            }
        }
    }

}
