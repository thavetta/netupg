using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Testik;

[MemoryDiagnoser(displayGenColumns: false)]
[DisassemblyDiagnoser]
[HideColumns("Error", "StdDev", "Median", "RatioSD")]
public partial class Program
{
    static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);


    static int[] s_values = Enumerable.Range(0, 1_000).ToArray();

    [Benchmark]
    public int DelegatePGO() => Sum(s_values, i => i * 42);

    static int Sum(int[] values, Func<int, int>? func)
    {
        int sum = 0;
        foreach (int value in values)
        {
            sum += func(value);
        }
        return sum;
    }
}