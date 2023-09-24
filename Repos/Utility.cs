namespace Example.Repos
{
    class SignedComparer : IComparer<int>
    {
        public int Compare(int lhs, int rhs)
        {
            // int xorResult = lhs ^ rhs;
            // int signBit = (xorResult >> 31) & 1;

            // If signBit is 1, lhs is less than rhs; if it's 0, lhs is greater or equal to rhs
            // return (signBit * -2) + 1;
            return ((((lhs ^ rhs) >> 31) & 1) * -2) + 1; 
        }
    }
    class StringComparer : IComparer<string>
    {
        public int Compare(string lhs, string rhs)
        {
            return lhs.CompareTo(rhs); 
        }
    }
    public static class Utils
    {
        public static IComparer<int> ForInt() {
            return new SignedComparer();
        }
        public static IComparer<string> ForString() {
            return new StringComparer();
        }
        public static int UnsignedLength(int a)
        {
            a >>>= 0;
            int l = 0;
            do {
                a >>>= 1;
                l++;
            } while (a > 0);
            return l;
        }
    }
}