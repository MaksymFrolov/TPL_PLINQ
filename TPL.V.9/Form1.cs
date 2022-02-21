using System.Diagnostics;

namespace TPL.V._9
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
            string pathToImage = textBox3.Text;
            Stopwatch syncStopWatch = new();
            syncStopWatch.Start();
            synchronOfFolder(pathOut, pathIn, pathToImage);
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new();
            parallelStopWatch.Start();
            parallelOfFolder(pathIn, pathOut, pathToImage);
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            label3.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}";
        }
        void synchronOfFolder(string folderOut, string folderIn,string folderImage)
        {
            try
            {
                DirectoryInfo direct = new(folderOut);
                FileInfo[] file = direct.GetFiles();
                Bitmap img = new (folderImage);
                foreach (FileInfo fi in file)
                {
                    Bitmap bit = new (fi.FullName);
                    for (int i = 0; i < bit.Width; i++)
                        for (int j = 0; j < bit.Height; j++)
                        {
                            Color pixel1 = img.GetPixel(i, j);
                            Color pixel2 = bit.GetPixel(i, j);
                            int r, g, b;
                            if (pixel1.R + pixel2.R > 255)
                                r = 255;
                            else r = pixel1.R + pixel2.R;
                            if (pixel1.B + pixel2.B > 255)
                                b = 255;
                            else b = pixel1.B + pixel2.B;
                            if (pixel1.G + pixel2.G > 255)
                                g = 255;
                            else g = pixel1.G + pixel2.G;
                            bit.SetPixel(i, j, Color.FromArgb(r, g, b));
                        }
                    bit.Save(folderIn + "\\sync" + fi.Name);
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
        void parallelOfFolder(string folderOut, string folderIn, string folderImage)
        {
            try
            {
                DirectoryInfo direct = new(folderOut);
                FileInfo[] file = direct.GetFiles();
                Parallel.ForEach(file, fi =>
                {
                    Bitmap img = new (folderImage);
                    Bitmap bit = new (fi.FullName);
                    for (int i = 0; i < bit.Width; i++)
                        for (int j = 0; j < bit.Height; j++)
                        {
                            Color pixel1 = img.GetPixel(i, j);
                            Color pixel2 = bit.GetPixel(i, j);
                            int r, g, b;
                            if (pixel1.R + pixel2.R > 255)
                                r = 255;
                            else r = pixel1.R + pixel2.R;
                            if (pixel1.B + pixel2.B > 255)
                                b = 255;
                            else b = pixel1.B + pixel2.B;
                            if (pixel1.G + pixel2.G > 255)
                                g = 255;
                            else g = pixel1.G + pixel2.G;
                            bit.SetPixel(i, j, Color.FromArgb(r, g, b));
                        }
                    bit.Save(folderIn + "\\parallel" + fi.Name);
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