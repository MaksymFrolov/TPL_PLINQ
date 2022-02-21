using System.Diagnostics;

namespace TPL.V._5
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
            Stopwatch syncStopWatch = new ();
            int syncSum = 0;
            syncStopWatch.Start();
            for (int i = 0; i < size; i++)
                syncSum += i;
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new ();
            object monitor = new ();
            int parallelSum = 0;
            parallelStopWatch.Start();
            Parallel.For(0, size,
                () => 0,
                (i, loop, localvalue) => localvalue += i,
                localvalue =>
                {
                    lock (monitor)
                        parallelSum += localvalue;
                });
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            label1.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}" +
                $"\nNo parallel sum: {syncSum}" +
                $"\nParallel sum: {parallelSum}" +
                $"\nEqual: {syncSum==parallelSum}";
        }
    }
}