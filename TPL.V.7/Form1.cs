using System.Diagnostics;

namespace TPL.V._7
{
    public partial class Form1 : Form
    {
        const int size = 10000000;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch syncStopWatch = new();
            double syncSum = 0.0;
            syncStopWatch.Start();
            for (int i = 0; i < size; i++)
                syncSum += Math.Sqrt(i);
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new();
            object monitor = new();
            double parallelSum = 0.0;
            parallelStopWatch.Start();
            Parallel.For(0, size,
                () => 0.0,
                (i, loop, localvalue) => localvalue += Math.Sqrt(i),
                localvalue =>
                {
                    lock (monitor)
                        parallelSum += localvalue;
                });
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            syncSum=Math.Round(syncSum, 1);
            parallelSum = Math.Round(parallelSum, 1);
            label1.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}" +
                $"\nNo parallel sum: {syncSum}" +
                $"\nParallel sum: {parallelSum}" +
                $"\nEqual: {syncSum == parallelSum}";
        }
    }
}