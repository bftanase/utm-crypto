using bft_crypto.Models;
using BftCryptoCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace bft_crypto.Controllers
{
    public class CryptoController : Controller
    {
        private readonly ICryptoService _cryptoService;

        public CryptoController(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Process(CryptoRequestViewModel cryptoRequest, string cryptoAction)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", cryptoRequest);
            }

            ModelState.Clear();

            if (cryptoAction == "encrypt")
            {
                cryptoRequest.ProcessedText =
                    _cryptoService.Encrypt(cryptoRequest.EncryptionKey, cryptoRequest.ProcessedText);
            }
            else
            {
                cryptoRequest.ProcessedText =
                    _cryptoService.Decrypt(cryptoRequest.EncryptionKey, cryptoRequest.ProcessedText);
            }

            return View("Index", cryptoRequest);
        }
    }
}