using System.Diagnostics;

Stopwatch syncStopWatch = new();
Stopwatch parallelStopWatch = new();
const int size = 100000000;
int[] numbers = new int[size];
Random rnd = new();
for (int i = 0; i < size; i++)
    numbers[i] = rnd.Next(-100, 101);
syncStopWatch.Start();
int syncSum = numbers.Sum();
syncStopWatch.Stop();
parallelStopWatch.Start();
int parallelSum = numbers.AsParallel().Sum();
parallelStopWatch.Stop();
double syncTime = syncStopWatch.Elapsed.TotalMilliseconds,
       parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
Console.WriteLine($"TASK 4\n\nLINQ: {syncTime}" +
    $"\nPLINQ: {parallelTime}" +
    $"\nk = {syncTime / parallelTime}" +
    $"\nSum LINQ: {syncSum}" +
    $"\nSum PLINQ: {parallelSum}");