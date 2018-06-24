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
        private string URL;
        private string path;
        private string pathDic;
        private string DivGame = @"\tandots";
        private string VersionС;
        private string VersionS;

        public Form1()
        {
            InitializeComponent();
            InstConfigDiv();
            Testdiv();
            textBox1.Text = path;
            URL = "http://www.tandots.ru/TL.exe";
            UpdateVer();
            serverVer();
            label3.Text = "Версия клиента: " + Properties.Settings.Default.ver;
            //UpdateApp();
        }
        //узнаем версию
        private void UpdateVer()
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead("http://www.tandots.ru/ver.txt"))
            using (var reader = new StreamReader(stream))
                VersionS = reader.ReadToEnd();
        }
        //узнаем версию сервера
        private void serverVer()
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead("http://www.tandots.ru/ver.txt"))
            using (var reader = new StreamReader(stream))
                label4.Text = "Версия сервера: " + reader.ReadToEnd();
        }

        //создание новой папки
        private void Testdiv()
        {
            Directory.CreateDirectory(path + @"\tandots");

        }
        //прогрузка конфигов
        private void InstConfigDiv()
        {
            VersionС = Properties.Settings.Default.ver;
            path = Properties.Settings.Default.directory;
            pathDic = @path;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //действия при запуске
            InitializeComponent();
            InstConfigDiv();
            Testdiv();
            textBox1.Text = path;
            URL = "http://www.tandots.ru/TL.exe";
            UpdateVer();
            serverVer();
            label3.Text = "Версия клиента: " + Properties.Settings.Default.ver;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateClient();
        }
        private void UpdateClient()
        {
            InstConfigDiv();
            if(System.IO.Directory.Exists(path + DivGame + @"\.minecraft"))
            {
                DeliteUpdate();
                if(System.IO.File.Exists(path + DivGame + @"\TL.exe"))
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

        void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = "Загрузка " + Convert.ToString(progressBar1.Value) + "%";

        }

        // действия после скачивание архива
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            label1.Text = "Скачивание завершено";
            Zip();
            VersionС = VersionS;
            Properties.Settings.Default.ver = VersionС;
            Properties.Settings.Default.Save();

        }
        //действия с zip
        private void Zip()
        {
            label1.Text = "Распоковка";
            InstConfigDiv();
            Directory.SetCurrentDirectory(path + DivGame);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = true;
            startInfo.FileName = pathDic + DivGame + @"\TL.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = "";
            Process.Start(startInfo);
        }
        //удаление фалов перед обнавлением
        private void DeliteUpdate()
        {
            SortedSet<string> filesToIgnore = new SortedSet<string>(StringComparer.OrdinalIgnoreCase) {
                "servers.dat",
                "resourcepacks",
                "saves",
                "config"
            };
            foreach (var filePath in Directory.EnumerateFiles(path + DivGame + @"\.minecraft"))
            {
                string fileName = Path.GetFileName(filePath);
                if (!filesToIgnore.Contains(fileName))
                    File.Delete(filePath);
            }

            foreach (var dirPath in Directory.EnumerateDirectories(path + DivGame + @"\.minecraft"))
            {
                string DirName = Path.GetFileName(dirPath);
                if (!filesToIgnore.Contains(DirName))
                    Directory.Delete(@dirPath, true);
            }

            //Directory.GetDirectories

        }
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



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            URL = "http://taruu.ru/TL.exe";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            URL = "http://www.tandots.ru/TL.exe";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(FBD.SelectedPath + " ?");
                path = FBD.SelectedPath;
                textBox1.Text = path;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            path = @textBox1.Text;
            Properties.Settings.Default.directory = textBox1.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Сохранено");
            InstConfigDiv();
            Testdiv();

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
                MessageBox.Show("Все есть");
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
            textBox1.Text = "12313123";
        }
    }

}
