using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Primes.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BigInteger p = PrimeGenerator.GetPrime1(1024);
            BigInteger q = PrimeGenerator.GetPrime1(1024);

            var rsaparams = RSAGenerator.GenerateKey(p, q);
            using var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(rsaparams);

            Console.Clear();
            Console.WriteLine("KEYSIZE: "+csp.KeySize);
        }
    }
}