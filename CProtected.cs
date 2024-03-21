namespace TypeCheckVsAbstract
{
    public abstract class CProtected
    {

        public static class Tags
        {
            public const int A = 0;

            public const int B = 1;
        }

        internal class _A : CProtected
        {
            internal _A()
            {
                Tag = Tags.A;
            }
        }

        internal static readonly _A _a = new _A();

        public static CProtected A
        {
            get
            {
                return _a;
            }
        }

        internal class B : CProtected
        {
            public int Item { get; }
            internal B(int item)
            {
                Item = item;
                Tag = Tags.B;
            }
        }

        public int Tag { get; protected set; }
    }
}
