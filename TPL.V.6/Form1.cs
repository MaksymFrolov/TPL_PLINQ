using System.Diagnostics;

namespace TPL.V._6
{
    public partial class Form1 : Form
    {
        int[,] matrix1, matrix2, syncMatrix, parallelMatrix;
        const int size = 1000;
        public Form1()
        {
            InitializeComponent();
            matrix1 = new int[size, size];
            matrix2 = new int[size, size];
            syncMatrix = new int[size, size];
            parallelMatrix = new int[size, size];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    int rand = rnd.Next(-100, 101);
                    matrix1[i, j] = rand;
                    matrix2[i, j] = rand;
                }
            Stopwatch syncStopWatch = new();
            syncStopWatch.Start();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                        syncMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
            syncStopWatch.Stop();
            Stopwatch parallelStopWatch = new();
            parallelStopWatch.Start();
            Parallel.For(0, size, i =>
            {
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                        parallelMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
            });
            parallelStopWatch.Stop();
            double syncTime = syncStopWatch.Elapsed.TotalMilliseconds;
            double parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
            label1.Text = $"No Parallel: {syncTime}" +
                $"\nParallel: {parallelTime}" +
                $"\nk = {syncTime / parallelTime}" +
                $"\nis Equal: {isEqual()}";
        }
        bool isEqual()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (syncMatrix[i, j] != parallelMatrix[i, j])
                        return false;
            return true;
        }
    }
}