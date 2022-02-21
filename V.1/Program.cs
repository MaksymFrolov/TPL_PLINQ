using System.Diagnostics;

const int size = 100000000;
int[] numbers = new int[size];
Random rnd = new();
for (int i = 0; i < size; i++)
    numbers[i] = rnd.Next(-100, 101);
Stopwatch syncStopWatch = new();
syncStopWatch.Start();
int syncCount = (from n in numbers
                 where n == 0
                 select n
                 ).Count();
syncStopWatch.Stop();
Stopwatch parallelStopWatch = new();
parallelStopWatch.Start();
int parallelCount = (from n in numbers.AsParallel()
                     where n == 0
                     select n
                 ).Count();
parallelStopWatch.Stop();
double syncTime = syncStopWatch.Elapsed.TotalMilliseconds,
       parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
Console.WriteLine($"TASK 1\n\nLINQ: {syncTime}" +
    $"\nPLINQ: {parallelTime}" +
    $"\nk = {syncTime / parallelTime}" +
    $"\nNumber of zeros LINQ: {syncCount}" +
    $"\nNumber of zeros PLINQ: {parallelCount}");