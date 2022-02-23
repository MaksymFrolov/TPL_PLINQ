using System.Diagnostics;
using System.Text;

namespace TPL.V._10
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
            double syncLenght;
            Stopwatch syncStopWatch = new();
            syncStopWatch.Start();
            syncLenght = synchronLenghtOfFolder(pathToDirectory);
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new();
            double parallelLenght;
            parallelStopWatch.Start();
            parallelLenght = parallelLenghtOfFolder(pathToDirectory);
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            label2.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}" +
                $"\nNo parallel lenght: {syncLenght}" +
                $"\nParallel lenght: {parallelLenght}" +
                $"\nEqualLenght: " + (syncLenght == parallelLenght);
        }
        double synchronLenghtOfFolder(string folder)
        {
            try
            {
                DirectoryInfo direct = new(folder);
                FileInfo[] file = direct.GetFiles();
                double lenght = 0.0;
                foreach (FileInfo fi in file)
                    using (StreamReader stream = new (fi.FullName))
                    {
                        StringBuilder strbuild=new StringBuilder();
                        string[] textMass;
                        while (stream.EndOfStream != true)
                            strbuild.Append(stream.ReadLine());
                        textMass = strbuild.ToString().Split(' ');
                        lenght += textMass.Length;
                    };
                return lenght;
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("���������� �� �������. ������: " + ex.Message);
                return 0.0;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("����������� ������. ������: " + ex.Message);
                return 0.0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������� ������. ������: " + ex.Message);
                return 0.0;
            }
        }
        double parallelLenghtOfFolder(string folder)
        {
            try
            {
                DirectoryInfo direct = new(folder);
                FileInfo[] file = direct.GetFiles();
                double lenght = 0.0;
                Parallel.ForEach(file, fi =>
                {
                    using (StreamReader stream = new StreamReader(fi.FullName))
                    {
                        StringBuilder strbuild = new StringBuilder();
                        string[] textMass;
                        while (stream.EndOfStream != true)
                            strbuild.Append(stream.ReadLine());
                        textMass = strbuild.ToString().Split(' ');
                        lenght += textMass.Length;
                    };
                });
                return lenght;
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("���������� �� �������. ������: " + ex.Message);
                return 0.0;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("����������� ������. ������: " + ex.Message);
                return 0.0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��������� ������. ������: " + ex.Message);
                return 0.0;
            }
        }
    }
}