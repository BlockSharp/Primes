using System;
using System.Numerics;
using System.Security.Cryptography;

namespace Primes
{
    /// <summary>
    /// This class is able to generate RSA parameters from 2 primes using math
    /// With help from Wikipedia: https://en.wikipedia.org/wiki/RSA_(cryptosystem)
    /// (c) 2020 maurictg and job79
    /// </summary>
    public static class RSAGenerator
    {
        /// <summary>
        /// This function generates RSAParameters from 2 primes!!!
        /// </summary>
        /// <param name="p">The first prime</param>
        /// <param name="q">The second prime</param>
        /// <returns>RSAParameters used in the RsaCSP</returns>
        public static RSAParameters GenerateKey(BigInteger p, BigInteger q)
        {
            /*
             * prime = an integer that's only dividable by 1 and itselves
             * co-prime = two integers that are only dividable by 1 (GCD(a,b) = 1)
             */
            
            //1. Choose 2 big primes => p and q
            
            //2. Compute n = pq
            var n = p * q;
            
            //3. Compute λ(n) where λ is Carmichael's totient function.
            //Since n = pq, λ(n) = lcm(λ(p),λ(q)),
            //and since p and q are prime, λ(p) = φ(p) = p − 1
            //and likewise λ(q) = q − 1.
            //Hence λ(n) = lcm(p − 1, q − 1). 
            var λn = Mathematics.LCM(p - 1, q - 1);
            
            //4. Choose an integer e such that 1 < e < λ(n) and gcd(e, λ(n)) = 1; that is, e and λ(n) are coprime. 
            //e having a short bit-length and small Hamming weight results in more efficient encryption  – the most commonly chosen value for e is 2^16 + 1 = 65,537. The smallest (and fastest) possible value for e is 3,
            //but such a small value for e has been shown to be less secure in some settings
            var e = new BigInteger(65537); //Also RsaCryptoServiceProvider uses this preselected value.
            
            //Check if e is correct, just for sure
            if(!PrimeChecker.AreCoPrime(e, λn))
                throw new ArgumentException("The value e is wrongly chosen.");
            
            //5. Determine d as d ≡ e−1 (mod λ(n)); that is, d is the modular multiplicative inverse of e modulo λ(n). 
            //This means: solve for d the equation d⋅e ≡ 1 (mod λ(n));
            //d can be computed efficiently by using the Extended Euclidean algorithm,
            //since, thanks to e and λ(n) being coprime, said equation is a form of Bézout's identity, where d is one of the coefficients.
            var d = Mathematics.ModInverse(e, λn);
            
            //Now the other private parameters
            var dp = d % (p - 1);
            var dq = d % (q - 1);
            var invQ = Mathematics.ModInverse(q, p);

            return new RSAParameters
            {
                D = d.ToInvByteArray(),
                DP = dp.ToInvByteArray(),
                DQ = dq.ToInvByteArray(),
                Exponent = e.ToInvByteArray(),
                InverseQ = invQ.ToInvByteArray(),
                Modulus = n.ToInvByteArray(),
                P = p.ToInvByteArray(),
                Q = q.ToInvByteArray()
            };
        }
    }
}