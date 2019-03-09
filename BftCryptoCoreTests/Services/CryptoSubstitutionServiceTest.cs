using System;
using System.Collections.Generic;
using System.Text;
using BftCryptoCore.Services;
using Xunit;

namespace BftCryptoCoreTests.Services
{
    public class CryptoSubstitutionServiceTest
    {
        [Fact]
        public void EncryptionTest()
        {
            var inputText = "IN CRIPTOGRAFIE NICI O REGULA NU ESTE ABSOLUTA";
            var encryptionKey = "TESTARESISTEM";

            var expectedResult = "OHDXCOFMGQCZUOVDHOXODGDCVQIKZDHIDVBMVDZYBGKIMZ";

            var cryptoService = new CryptoSubstitutionService();
            Assert.Equal(expectedResult, cryptoService.Encrypt(encryptionKey, inputText));
        }

        [Fact]
        public void DecryptionTest()
        {
            var inputText = "OHDXCOFMGQCZUOVDHOXODGDCVQIKZDHIDVBMVDZYBGKIMZ";
            var encryptionKey = "TESTARESISTEM";

            var expectedResult = "IN CRIPTOGRAFIE NICI O REGULA NU ESTE ABSOLUTA";

            var cryptoService = new CryptoSubstitutionService();
            Assert.Equal(expectedResult, cryptoService.Decrypt(encryptionKey, inputText));
        }
    }
}
