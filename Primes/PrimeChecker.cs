using System.Numerics;
using System.Security.Cryptography;

namespace Primes
{
    public static class PrimeChecker
    {
        /// <summary>
        /// First known primes
        /// </summary>
        public static readonly int[] FirstPrimes =
        {
            /*2,*/ 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
            103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 
            223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337,
            347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461
        };

        /// <summary>
        /// Generate low level prime
        /// </summary>
        /// <param name="size">size of low level prime</param>
        /// <returns></returns>
        public static BigInteger GetLowLevelPrime(int size)
        {
            Start:
            var pc = RandomFromRange(BigInteger.Pow(2, size - 1) + 1, BigInteger.Pow(2, size) - 1);
            foreach (var divisor in FirstPrimes)
                if (pc % divisor == 0 && BigInteger.Pow(divisor, 2) <= pc)
                    goto Start;
            return pc;
        }

        /// <summary>
        /// Get random number within range
        /// </summary>
        /// <param name="min">minimum value</param>
        /// <param name="max">maximum value</param>
        /// <returns>random BigInteger</returns>
        public static BigInteger RandomFromRange(BigInteger min, BigInteger max)
        {
            var bytes = new byte[max.GetByteCount()];
            using var rng = new RNGCryptoServiceProvider();

            while (true)
            {
                rng.GetBytes(bytes);
                bytes[^1] &= 0x7F; // force sign bit to positive
                bytes[0] |= 1; // force last bit to 1

                var random = new BigInteger(bytes);
                if (random <= max && random >= min) return random;
            }
        }

        /// <summary>
        /// Determines whether biginteger is a prime
        /// </summary>
        /// <param name="n">a biginteger</param>
        /// <param name="k">the number of tests to be executed</param>
        /// <returns></returns>
        public static bool IsPrime(this BigInteger n, long k = 128)
        {
            if (n == 2 || n == 3) return true;
            if (n <= 1 || n % 2 == 0) return false;

            BigInteger s = 0, r = n - 1;
            while ((r & 1) == 0)
            {
                s += 1;
                r /= 2;
            }

            for (int i = 0; i < k; i++)
            {
                BigInteger a = RandomFromRange(0, n - 1), x = BigInteger.ModPow(a, r, n);

                if (x != 1 && x != n - 1)
                {
                    var j = BigInteger.One;

                    while (j < s && x != n - 1)
                    {
                        x = BigInteger.ModPow(x, 2, n);
                        if (x == 1) return false;
                        j += 1;
                    }

                    if (x != n - 1) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if a biginteger is a prime using the MillerRabin test
        /// From: https://www.geeksforgeeks.org/how-to-generate-large-prime-numbers-for-rsa-algorithm/
        /// </summary>
        /// <param name="mrc">Number to check</param>
        /// <returns>True or false</returns>
        public static bool IsPrimeMillerRabin(BigInteger mrc)
        {
            int maxDevisionsByTwo = 0;
            BigInteger ec = mrc - BigInteger.One;

            while (ec % 2 == 0)
            {
                ec >>= 1;
                maxDevisionsByTwo++;
            }

            //Assert
            if (BigInteger.Pow(2, maxDevisionsByTwo) * ec != mrc - BigInteger.One)
                return false;

            bool TrialComposite(BigInteger roundTester)
            {
                if (BigInteger.ModPow(roundTester, ec, mrc) == BigInteger.One)
                    return false;

                for (int i = 0; i < maxDevisionsByTwo; i++)
                {
                    if (BigInteger.ModPow(roundTester, BigInteger.Pow(2, i) * ec, mrc) == mrc - BigInteger.One)
                        return false;
                }

                return true;
            }

            //Set number of trials here
            int numberOfRabinTrials = 20;
            for (int i = 0; i < numberOfRabinTrials; i++)
            {
                BigInteger round_tester = RandomFromRange(2, mrc);
                if (TrialComposite(round_tester))
                    return false;
            }

            return true;
        }
    }
}