using System.Diagnostics;

namespace TPL.V._11
{
    public partial class Form1 : Form
    {
        int[] nums, syncNumsLow, syncNumsHigh, parallelNumsLow, parallelNumsHigh;
        const int size = 100000000;
        public Form1()
        {
            InitializeComponent();
            nums = new int[size];
            syncNumsLow = new int[size];
            syncNumsHigh = new int[size];
            parallelNumsLow = new int[size];
            parallelNumsHigh = new int[size];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            for (int i = 0; i < size; i++)
                nums[i] = rnd.Next(-100, 101);
            Stopwatch syncStopWatch = new();
            int x = Convert.ToInt32(textBox1.Text);
            syncStopWatch.Start();
            for (int i = 0; i < size; i++)
            {
                if (nums[i] > x)
                    syncNumsLow[i] = nums[i];
                else syncNumsHigh[i] = nums[i];
            }
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new();
            parallelStopWatch.Start();
            Parallel.For(0, size, i =>
            {
                if (nums[i] > x)
                    parallelNumsLow[i] = nums[i];
                else parallelNumsHigh[i] = nums[i];
            });
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            label2.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}" +
                $"\nis Equal: {isEqual()}";
        }
        bool isEqual()
        {
            for (int i = 0; i < size; i++)
                if (syncNumsLow[i] != parallelNumsLow[i] || syncNumsHigh[i] != parallelNumsHigh[i])
                    return false;
            return true;
        }
    }
}