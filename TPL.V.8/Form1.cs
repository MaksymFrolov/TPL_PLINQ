using System.Diagnostics;

namespace TPL.V._8
{
    public partial class Form1 : Form
    {
        int[] nums;
        double[] syncNums, parallelNums;
        const int size = 100000000;
        public Form1()
        {
            InitializeComponent();
            nums = new int[size];
            syncNums = new double[size];
            parallelNums = new double[size];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            for (int i = 0; i < size; i++)
                nums[i] = rnd.Next(-100, 101);
            Stopwatch syncStopWatch = new();
            int x = Convert.ToInt32(textBox2.Text);
            syncStopWatch.Start();
            for (int i = 0; i < size; i++)
                syncNums[i] = Math.Pow(nums[i], x);
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new();
            int N = Convert.ToInt32(textBox1.Text);
            List<Task> tasks = new();
            parallelStopWatch.Start();
            for (int i = 0; i < N; i++)
            {
                Task task = new (i =>
                {
                    for (int j = (int)i * size / N; j < ((int)i + 1) * size / N; j++)
                    {
                        parallelNums[j] = Math.Pow(nums[j], x);
                    }
                }, i);
                task.Start();
                tasks.Add(task);
            }
            foreach (Task task in tasks)
                if (!task.IsCompleted)
                    task.Wait();
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            label3.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}" +
                $"\nis Equal: {isEqual()}";
        }
        bool isEqual()
        {
            for (int i = 0; i < size; i++)
                if (syncNums[i] != parallelNums[i])
                    return false;
            return true;
        }
    }
}