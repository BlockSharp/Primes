using System;
using System.Numerics;

namespace Primes
{
    public static class Tools
    {
        public static byte[] ToInvByteArray(this BigInteger i)
        {
            byte[] b = i.ToByteArray();
            Array.Reverse(b);
            return b;
        }
    }
}