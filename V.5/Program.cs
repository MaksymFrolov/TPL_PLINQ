using System.Diagnostics;

Stopwatch syncStopWatch = new();
Stopwatch parallelStopWatch = new();
const int size = 100000000;
int[] numbers = new int[size];
Random rnd = new();
for (int i = 0; i < size; i++)
    numbers[i] = rnd.Next(-100, 101);
syncStopWatch.Start();
double syncAverage = numbers.Average();
syncStopWatch.Stop();
parallelStopWatch.Start();
double parallelAverage = numbers.AsParallel().Average();
parallelStopWatch.Stop();
double syncTime = syncStopWatch.Elapsed.TotalMilliseconds,
       parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
Console.WriteLine($"TASK 5\n\nLINQ: {syncTime}" +
    $"\nPLINQ: {parallelTime}" +
    $"\nk = {syncTime / parallelTime}" +
    $"\nAverage LINQ: {syncAverage}" +
    $"\nAverage PLINQ: {parallelAverage}");