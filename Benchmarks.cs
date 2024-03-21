using System;

using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace TypeCheckVsAbstract
{
    public class Benchmarks
    {
        private static C[] millionTC;
        private static CAbstract[] millionAbs;
        private static CProtected[] millionPro;

        [GlobalSetup]
        public void Setup()
        {
            var rng = new Random(1234);
            millionTC = new C[1_000_000];
            millionAbs = new CAbstract[1_000_000];
            millionPro = new CProtected[1_000_000];

            for (int i = 0; i < millionTC.Length; i++)
            {
                var x = rng.Next();
                if (x % 2 == 0)
                {
                    millionTC[i] = C.A;
                    millionAbs[i] = CAbstract.A;
                    millionPro[i] = CProtected.A;
                }
                else
                {
                    millionTC[i] = new C.B(x);
                    millionAbs[i] = new CAbstract.B(x);
                    millionPro[i] = new CProtected.B(x);
                }
            }
        }

        [Benchmark]
        public void TypeCheck()
        {
            var x = 0;
            foreach (var item in millionTC)
            {
                if (item.Tag == 0)
                {
                    x++;
                }
                else
                {
                    x--;
                }
            }
        }

        [Benchmark]
        public void Abstract()
        {
            var x = 0;
            foreach (var item in millionAbs)
            {
                if (item.Tag == 0)
                {
                    x++;
                }
                else
                {
                    x--;
                }
            }
        }

        [Benchmark]
        public void Protected()
        {
            var x = 0;
            foreach (var item in millionPro)
            {
                if (item.Tag == 0)
                {
                    x++;
                }
                else
                {
                    x--;
                }
            }
        }
    }
}
