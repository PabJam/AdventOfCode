namespace Utils
{
    public static class MathUtils
    {
        /// <summary>
        /// Chinese Remainder Theorem
        /// </summary>
        /// <param name="div">All different divisors</param>
        /// <param name="rem">All different remainders</param>
        /// <returns>smallest possible x so that x % div = rem for each div and rem </returns>
        /// <exception cref="ArgumentException"></exception>
        public static long CRT(long[] div, long[] rem)
        {
            if (div.Length != rem.Length) { throw new ArgumentException("div array and rem array must be of same length"); }
            if (div.Length == 0) { return 0; }
            // TODO check if div values dont share any common divisors (are coprime)
            long prod = div[0];

            for (int i = 1; i < div.Length; i++)
            {
                prod *= div[i];
            }

            long result = 0;
            for (int i = 0; i < div.Length; i++)
            {
                long pp = prod / div[i];
                result += rem[i] * ModInv(pp, div[i]) * pp;
            }
            return result % prod;
        }

        /// <summary>
        /// Greatest Common Devisor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static long GCD(long a, long b)
        {
            long _a;
            while (a > 0)
            {
                _a = a;
                a = b % a;
                b = _a;
            }
            return b;
        }

        /// <summary>
        /// Extended Euclidean algorithm also finds integer coefficients x and y such that: ax + by = gcd(a, b) 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static long GCDExtended(long a, long b, out long x, out long y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }

            long _x, _y;
            long gcd = GCDExtended(b % a, a, out _x, out _y);
            x = _y - (b / a) * _x;
            y = _x;

            return gcd;
        }

        // Returns modulo inverse of a  
        // with respect to m using 
        // extended Euclid Algorithm.  
        // Refer below post for details: 
        // https://www.geeksforgeeks.org/ 
        // multiplicative-inverse-under-modulo-m/ 
        public static long ModInv(long a, long m)
        {
            long gcd = GCDExtended(a, m, out long x, out long y);
            if (gcd != 1) { throw new Exception("Modular inverse does not exist"); }
            return (x % m + m) % m;
        }

        // Returns a Unique Value for each combination of a and b
        public static long PerfectlyHashThem(int a, int b)
        {
            var A = (ulong)(a >= 0 ? 2 * (long)a : -2 * (long)a - 1);
            var B = (ulong)(b >= 0 ? 2 * (long)b : -2 * (long)b - 1);
            var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
            return a < 0 && b < 0 || a >= 0 && b >= 0 ? C : -C - 1;
        }
    }
}
