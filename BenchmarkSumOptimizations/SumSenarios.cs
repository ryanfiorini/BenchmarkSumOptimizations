using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;

namespace BenchmarkSumOptimizations
{
    [MemoryDiagnoser]
    public class SumSenarios
    {
        private int[] array = Enumerable.Range(0, 100).ToArray();

        public SumSenarios()
        {
            //var _ = SumOdd();
            //_ = SumOdd_Bit();
            //_ = SumOdd_BranchFree();
            //_ = SumOdd_BranchFree_Parallel();
            //_ = SumOdd_BranchFree_Parallel_NoMul();
        }

        [Benchmark]
        public int SumOdd()
        {
            int counter = 0;
            for (int i = 0; i < array.Length; i++)
            {
                var element = array[i];
                if (element % 2 != 0)
                    counter += element;
            }

            return counter;
        }

        [Benchmark]
        public int SumOdd_Bit()
        {
            int counter = 0;
            for (int i = 0; i < array.Length; i++)
            {
                var element = array[i];
                if ((element & 1) == 1)
                    counter += element;
            }

            return counter;
        }

        [Benchmark]
        public int SumOdd_BranchFree()
        {
            int counter = 0;
            for (int i = 0; i < array.Length; i++)
            {
                var element = array[i];
                var odd = element & 1;
                counter += (odd * element);
            }

            return counter;
        }

        [Benchmark]
        public int SumOdd_BranchFree_Parallel()
        {
            int counterA = 0;
            int counterB = 0;

            for (int i = 0; i < array.Length; i+=2)
            {
                var elementA = array[i];
                var elementB = array[i + 1];

                var oddA = elementA & 1;
                var oddB = elementB & 1;

                counterA += (oddA * elementA);
                counterB += (oddB * elementB);
            }

            return counterA + counterB;
        }

        [Benchmark]
        public int SumOdd_BranchFree_Parallel_NoMul()
        {
            int counterA = 0;
            int counterB = 0;

            for (int i = 0; i < array.Length; i += 2)
            {
                var elementA = array[i];
                var elementB = array[i + 1];

                counterA += (elementA << (elementA & 1)) - elementA;
                counterB += (elementB << (elementB & 1)) - elementB;
            }

            return counterA + counterB;
        }

        //[Benchmark]
        //private unsafe int SumOdd_BranchFree_Parallel_NoChecks()
        //{
        //    int counterA = 0;
        //    int counterB = 0;

        //    fixed (int* data = &array[0])
        //    {
        //        var p = (int*)data;

        //        for (int i = 0; i < array.Length; i += 2)
        //        {
        //            counterA += (p[0] & 1) * p[0];
        //            counterB += (p[1] & 1) * p[1];

        //            p += 2;
        //        }
        //    }

        //    return counterA + counterB;
        //}

    }
}
