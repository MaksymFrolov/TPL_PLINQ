using System.Diagnostics;

const int size = 10000000;
int[] numbers = new int[size];
Random rnd = new();
for (int i = 0; i < size; i++)
    numbers[i] = rnd.Next(0, 15);
var syncGroup = from n in numbers
                orderby n
                group n by Factorial(n) into g
                select new { Value = g.Key, Count = g.Count() };
Console.WriteLine("TASK 6\n\nLINQ: ");
Stopwatch syncStopWatch = new();
syncStopWatch.Start();
foreach (var group in syncGroup)
    Console.WriteLine($"{group.Value} : {group.Count}");
syncStopWatch.Stop();
var parallelGroup = from n in numbers.AsParallel()
                    orderby n
                    group n by Factorial(n) into g
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
    $"\nNumber of unique numbers LINQ: {syncGroup.Count()}" +
    $"\nNumber of unique numbers PLINQ: {parallelGroup.Count()}");

static int Factorial(int number)
{
    int result = 1;
    for (int i = 1; i <= number; i++)
        result *= i;
    return result;
}