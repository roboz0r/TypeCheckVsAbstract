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
        private static int expected;

        [GlobalSetup]
        public void Setup()
        {
            var rng = new Random(1234);
            millionTC = new C[1_000_000];
            millionAbs = new CAbstract[1_000_000];
            millionPro = new CProtected[1_000_000];
            expected = 0;

            for (int i = 0; i < millionTC.Length; i++)
            {
                var x = rng.Next();
                if (x % 2 == 0)
                {
                    millionTC[i] = C.A;
                    millionAbs[i] = CAbstract.A;
                    millionPro[i] = CProtected.A;
                    expected++;
                }
                else
                {
                    millionTC[i] = new C.B(x);
                    millionAbs[i] = new CAbstract.B(x);
                    millionPro[i] = new CProtected.B(x);
                    expected += x;
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
        public void TypeCheck2()
        {
            var x = 0;
            foreach (var item in millionTC)
            {
                switch (item.Tag)
                {
                    case C.Tags.A:
                        x++;
                        break;
                    case C.Tags.B:
                        var b = (C.B)item;
                        x += b.Item;
                        break;
                    default:
                        throw new Exception($"Unexpected Tag {item.Tag}");
                }
            }
            if (x != expected)
            {
                throw new Exception($"{nameof(TypeCheck2)}: Expected {expected} but got {x}");
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
        public void Abstract2()
        {
            var x = 0;
            foreach (var item in millionAbs)
            {
                switch (item.Tag)
                {
                    case CAbstract.Tags.A:
                        x++;
                        break;
                    case CAbstract.Tags.B:
                        var b = (CAbstract.B)item;
                        x += b.Item;
                        break;
                    default:
                        throw new Exception($"Unexpected Tag {item.Tag}");
                }
            }
            if (x != expected)
            {
                throw new Exception($"{nameof(Abstract2)}: Expected {expected} but got {x}");
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

        [Benchmark]
        public void Protected2()
        {
            var x = 0;
            foreach (var item in millionPro)
            {
                switch (item.Tag)
                {
                    case CProtected.Tags.A:
                        x++;
                        break;
                    case CProtected.Tags.B:
                        var b = (CProtected.B)item;
                        x += b.Item;
                        break;
                    default:
                        throw new Exception($"Unexpected Tag {item.Tag}");
                }
            }
            if (x != expected)
            {
                throw new Exception($"{nameof(Protected2)}: Expected {expected} but got {x}");
            }
        }
    }
}
