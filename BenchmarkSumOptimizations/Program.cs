using BenchmarkDotNet.Running;
using System;

namespace BenchmarkSumOptimizations
{
    /*
     * To Run In Command Line 
     * dotnet run -p BenchmarkSumOptimizations.csproj -c Release
     * 
     */

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START");

            var summary = BenchmarkRunner.Run<SumSenarios>();

            Console.WriteLine("END");

        }
    }
}
