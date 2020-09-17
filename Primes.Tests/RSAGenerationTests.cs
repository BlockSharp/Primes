using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace Primes.Tests
{
    public class RSAGenerationTests
    {
        [Fact]
        public void TestKeyGeneration()
        {
            //Arrange, Act
            var csp = Helper.CreateCSPFromPrimes();
            
            //Assert
            Assert.False(csp.PublicOnly);
            Assert.InRange(csp.KeySize, 128, 4096);
        }
        

        [Fact]
        public void TestEncryptionAndDecryption()
        {
            using var csp = Helper.CreateCSPFromPrimes();
            
            var pubKey = csp.ExportCspBlob(false);
            using var csp2 = new RSACryptoServiceProvider();
            csp2.ImportCspBlob(pubKey);
            
            string input = "Hello, world!";
            byte[] enc = csp2.Encrypt(Encoding.UTF8.GetBytes(input), false);

            byte[] dec = csp.Decrypt(enc, false);
            string output = Encoding.UTF8.GetString(dec);
            Assert.Equal(input, output);
        }

        [Fact]
        public void TestSigning()
        {
            using var csp = Helper.CreateCSPFromPrimes();
            byte[] data = Encoding.UTF8.GetBytes("hello, world");
            byte[] sign = csp.SignData(data, SHA256.Create());

            var pubKey = csp.ExportCspBlob(false);
            using var csp2 = new RSACryptoServiceProvider();
            csp2.ImportCspBlob(pubKey);
            
            Assert.True(csp2.VerifyData(data, SHA256.Create(), sign));
        }
    }
}