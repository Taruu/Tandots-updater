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
            //действия при запуске
            InitializeComponent();
            path = Properties.Settings.Default.directory;
            textBox1.Text = path;
            URL = "http://www.tandots.ru/LA.zip";
            Testdiv();
            serverVer();
            label3.Text = Properties.Settings.Default.ver;
        }
        //узнаем версию
        private void UpdateVer()
        {
            using (var client = new WebClient())
            using (var stream = client.OpenRead("http://www.tandots.ru/ver.txt"))
            using (var reader = new StreamReader(stream))
            VersionС = reader.ReadToEnd();
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
            path = Properties.Settings.Default.directory;
            pathDic = @path;

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click (object sender, EventArgs e)
        {
            InstConfigDiv();
            if (Directory.Exists(@pathDic + @DivGame + @"\.minecaft") & Directory.Exists(@pathDic + @DivGame + @"\Launcher") & Directory.Exists(@pathDic + @DivGame + @"\TLauncher") & File.Exists(@pathDic + @DivGame + @"tlauncher.args") & File.Exists(@pathDic + @DivGame + @"start.exe"))
            {
                MessageBox.Show("Все есть");
            }
            else
            {
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
            label1.Text = "Загрузка "+Convert.ToString(progressBar1.Value)+"%";
            
        }

        // действия после скачивание архива
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            label1.Text = "Распоковка";
            Zip();


        }

        /// <summary>
        /// 
        /// </summary>
        private void Zip()
        {
            InstConfigDiv();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = pathDic + DivGame + @"\TL.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "";
            Process.Start(startInfo);
        }
        private void Delite()
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




        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            URL = "https://downloader.disk.yandex.ru/disk/d6f2da80cbb6a2b7b10366d2886d997c725a04b12e9eab2c4828908405e014d4/5b294070/PchyKDV67Ffgsy8bWN3BKg3kWRwoAHfFSCbze3jPe6QhtJ7K7R11-6iQeZVCFBmzdU36KCoie11gtLCH1Eim3w%3D%3D?uid=0&filename=LA.zip&disposition=attachment&hash=EukMpZERjc1%2BT4d4UCcmILyBpSaLY7we4lqIRRbRHs8%3D%3A&limit=0&content_type=application%2Fx-zip-compressed&fsize=331501514&hid=2ee0bb473f524c20965ddc45f9909225&media_type=compressed&tknv=v2";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            URL = "http://www.tandots.ru/LA.zip";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(FBD.SelectedPath);
                path = FBD.SelectedPath;
                textBox1.Text = FBD.SelectedPath;
                path = textBox1.Text;
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
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Zip();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delite();
        }
    }

}
