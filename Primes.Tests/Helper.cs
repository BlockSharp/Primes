using System.Security.Cryptography;

namespace Primes.Tests
{
    public static class Helper
    {
        public static RSACryptoServiceProvider CreateCSPFromPrimes()
        {
            var p = PrimeGenerator.GetPrime1(512);
            var q = PrimeGenerator.GetPrime1(512);
            
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(RSAGenerator.GenerateKey(p, q));

            return csp;
        }
    }
}