using System.Diagnostics;

namespace TPL.V._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pathToDirectory = textBox1.Text;
            double syncCatalogSize = 0.0;
            Stopwatch syncStopWatch = new();
            syncStopWatch.Start();
            syncCatalogSize = synchronSizeOfFolder(pathToDirectory, ref syncCatalogSize);
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new();
            double parallelCatalogSize = 0.0;
            parallelStopWatch.Start();
            parallelCatalogSize = parallelSizeOfFolder(pathToDirectory, ref parallelCatalogSize);
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            label2.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}" +
                $"\nNo parallel size: {syncCatalogSize}" +
                $"\nParallel size: {parallelCatalogSize}" +
                $"\nEqualSize: " + (syncCatalogSize == parallelCatalogSize);
        }
        double synchronSizeOfFolder(string folder, ref double catalogSize)
        {
            try
            {
                DirectoryInfo direct = new(folder);
                DirectoryInfo[] masDirect = direct.GetDirectories();
                FileInfo[] file = direct.GetFiles();
                foreach (FileInfo fi in file)
                    catalogSize += fi.Length;
                foreach (DirectoryInfo dir in masDirect)
                    synchronSizeOfFolder(dir.FullName, ref catalogSize);
                return Math.Round((double)(catalogSize / 1024 / 1024 / 1024), 1);
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("Директория не найдена. Ошибка: " + ex.Message);
                return 0;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Отсутствует доступ. Ошибка: " + ex.Message);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Ошибка: " + ex.Message);
                return 0;
            }
        }
        double parallelSizeOfFolder(string folder, ref double catalogSize)
        {
            try
            {
                DirectoryInfo direct = new(folder);
                DirectoryInfo[] masDirect = direct.GetDirectories();
                FileInfo[] file = direct.GetFiles();
                foreach (FileInfo fi in file)
                    catalogSize += fi.Length;
                double size = 0.0;
                Parallel.ForEach(masDirect, (dir) => parallelSizeOfFolder(dir.FullName, ref size));
                catalogSize += size;
                return Math.Round((double)(catalogSize / 1024 / 1024 / 1024), 1);
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("Директория не найдена. Ошибка: " + ex.Message);
                return 0;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Отсутствует доступ. Ошибка: " + ex.Message);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Ошибка: " + ex.Message);
                return 0;
            }
        }
    }
}