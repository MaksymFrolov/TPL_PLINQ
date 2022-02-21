using System.Diagnostics;

namespace TPL.V._4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pathOut = textBox1.Text;
            string pathIn = textBox2.Text;
            Stopwatch syncStopWatch = new();
            syncStopWatch.Start();
            synchronOfFolder(pathOut, pathIn);
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new();
            parallelStopWatch.Start();
            parallelOfFolder(pathIn, pathOut);
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            label3.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}";
        }
        void synchronOfFolder(string folderOut, string folderIn)
        {
            try
            {
                DirectoryInfo direct = new(folderOut);
                FileInfo[] file = direct.GetFiles();
                foreach (FileInfo fi in file)
                {
                    Image img = Image.FromFile(fi.FullName);
                    if (img != null)
                    {
                        img.RotateFlip(RotateFlipType.Rotate270FlipX);
                        img.Save(folderIn + "\\sync" + fi.Name);
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("Директория не найдена. Ошибка: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Отсутствует доступ. Ошибка: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Ошибка: " + ex.Message);
            }
        }
        void parallelOfFolder(string folderOut, string folderIn)
        {
            try
            {
                DirectoryInfo direct = new(folderOut);
                FileInfo[] file = direct.GetFiles();
                Parallel.ForEach(file, (fi) =>
                {
                    Image img = Image.FromFile(fi.FullName);
                    if (img != null)
                    {
                        img.RotateFlip(RotateFlipType.Rotate270FlipX);
                        img.Save(folderIn + "\\parallel" + fi.Name);
                    }
                });
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("Директория не найдена. Ошибка: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Отсутствует доступ. Ошибка: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Ошибка: " + ex.Message);
            }
        }
    }
}