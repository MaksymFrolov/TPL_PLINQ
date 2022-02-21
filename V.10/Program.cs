using System.Diagnostics;

const int size = 100000000;
int[] numbers = new int[size];
Random rnd = new();
for (int i = 0; i < size; i++)
    numbers[i] = rnd.Next(-20, 20);
var syncGroup = from n in numbers
                where n > 0
                orderby n descending
                group n by n into g
                select new { Value = g.Key, Count = g.Count() };
Console.WriteLine("TASK 10\n\nLINQ: ");
Stopwatch syncStopWatch = new();
syncStopWatch.Start();
foreach (var group in syncGroup)
    Console.WriteLine($"{group.Value} : {group.Count}");
syncStopWatch.Stop();
var parallelGroup = from n in numbers.AsParallel()
                    where n < 0
                    orderby n descending
                    group n by n into g
                    select new { Value = g.Key, Count = g.Count() };
Stopwatch parallelStopWatch = new();
parallelStopWatch.Start();
Console.WriteLine("\nPLINQ: ");
foreach (var group in parallelGroup)
    Console.WriteLine($"{group.Value} : {group.Count}");
parallelStopWatch.Stop();
double syncTime = syncStopWatch.Elapsed.TotalMilliseconds,
       parallelTime = parallelStopWatch.Elapsed.TotalMilliseconds;
Console.WriteLine($"\nLINQ: {syncTime}" +
    $"\nPLINQ: {parallelTime}" +
    $"\nk = {syncTime / parallelTime}" +
    $"\nNumber of unique negative numbers LINQ: {syncGroup.Count()}" +
    $"\nNumber of unique negative numbers PLINQ: {parallelGroup.Count()}");