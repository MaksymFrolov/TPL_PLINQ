using System.Diagnostics;

namespace TPL.V._1
{
    public partial class Form1 : Form
    {
        int[,] numbers;
        const int size = 20000;
        public Form1()
        {
            InitializeComponent();
            numbers = new int[size, size];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new ();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    numbers[i, j] = rnd.Next(-100, 101);
            Stopwatch syncStopWatch = new ();
            syncStopWatch.Start();
            int syncMax = 0;
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (numbers[i, j] > syncMax)
                        syncMax = numbers[i, j];
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new ();
            int parallelMax = 0;
            parallelStopWatch.Start();
            Parallel.For(0, size, i =>
            {
                for (int j = 0; j < size; j++)
                    if (numbers[i, j] > parallelMax)
                        parallelMax = numbers[i, j];
            });
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            label1.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}" +
                $"\nNo parallel max: {syncMax}" +
                $"\nParallel max: {parallelMax}";
        }
    }
}