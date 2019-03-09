using System.Net.Mime;

namespace BftCryptoCore.Services
{
    public interface ICryptoService
    {
        string Encrypt(string key, string text);
        string Decrypt(string key, string text);
    }
}