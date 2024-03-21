namespace TypeCheckVsAbstract
{
    public abstract class C
    {

        public static class Tags
        {
            public const int A = 0;

            public const int B = 1;
        }

        internal class _A : C
        {
            internal _A()
            {
            }
        }

        internal static readonly _A _a = new _A();

        public static C A
        {
            get
            {
                return _a;
            }
        }

        internal class B : C
        {
            public int Item { get; }
            internal B(int item)
            {
                Item = item;
            }
        }

        public int Tag
        {
            get
            {
                return (this is B) ? 1 : 0;
            }
        }
    }
}
