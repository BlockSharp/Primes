using System.Numerics;
using System;
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
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
            31, 37, 41, 43, 47, 53, 59, 61, 67,
            71, 73, 79, 83, 89, 97, 101, 103,
            107, 109, 113, 127, 131, 137, 139,
            149, 151, 157, 163, 167, 173, 179,
            181, 191, 193, 197, 199, 211, 223,
            227, 229, 233, 239, 241, 251, 257,
            263, 269, 271, 277, 281, 283, 293,
            307, 311, 313, 317, 331, 337, 347, 349
        };

        /// <summary>
        /// Get low level prime
        /// </summary>
        /// <param name="n">bitsize</param>
        /// <returns>BigInteger</returns>
        public static BigInteger GetLowLevelPrime(int n)
        {
            while (true)
            {
                //Obtain a random number
                BigInteger pc = RandomFromRange(BigInteger.Pow(2, n - 1) + 1, BigInteger.Pow(2, n) - 1);
                bool okay = false;
                foreach (var divisor in FirstPrimes)  {
                    if (pc % divisor == 0 && BigInteger.Pow(divisor, 2) <= pc) {
                        okay = true;
                        break;
                    }
                }
                
                if (!okay)
                    return pc;

                //Deze constructie is vaag
                //Want als okay true is, blijft ie voor altijd in deze functie haken
            }
        }
        
        /// <summary>
        /// Get random number within range
        /// </summary>
        /// <param name="start">The value from</param>
        /// <param name="stop">The value unto</param>
        /// <returns>Random BigInteger</returns>
        public static BigInteger RandomFromRange(BigInteger start, BigInteger stop) {
            byte[] bytes = stop.ToByteArray();
            BigInteger res;
            using var rng = new RNGCryptoServiceProvider();

            do {
                rng.GetBytes(bytes);
                bytes [^1] &= 0x7F; //force sign bit to positive
                res = new BigInteger(bytes);
            } while (res >= stop && res <= start);

            return res;
        }

        /// <summary>
        /// Checks if a biginteger is a prime
        /// </summary>
        /// <param name="n">The biginteger</param>
        /// <param name="k">The number of tests to be executed</param>
        /// <returns>bool</returns>
        public static bool IsPrime(BigInteger n, long k = 128)
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
                BigInteger a = RandomFromRange(0, n - 1),
                    x = BigInteger.ModPow(a, r ,n);

                if (x != 1 && x != n - 1)
                {
                    BigInteger j = 1;
                    
                    while (j < s && x != n - 1)
                    {
                        x = BigInteger.ModPow(x,2,n);
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
        /// <param name="mrc"></param>
        /// <returns></returns>
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
            };

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