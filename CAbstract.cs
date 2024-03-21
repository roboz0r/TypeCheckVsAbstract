namespace TypeCheckVsAbstract
{
    public abstract class CAbstract
    {

        public static class Tags
        {
            public const int A = 0;

            public const int B = 1;
        }

        internal class _A : CAbstract
        {
            public override int Tag => Tags.A;
            internal _A()
            {
            }
        }

        internal static readonly _A _a = new _A();

        public static CAbstract A
        {
            get
            {
                return _a;
            }
        }

        internal class B : CAbstract
        {
            public override int Tag => Tags.B;
            public int Item { get; }
            internal B(int item)
            {
                Item = item;
            }
        }

        public abstract int Tag { get; }
    }
}
