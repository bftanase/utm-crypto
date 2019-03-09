using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bft_crypto.Models
{
    public class CryptoRequestViewModel
    {
        [Required]
        [RegularExpression("[a-zA-Z\\s]+")]
        public string EncryptionKey { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z\\s]+")]
        public string ProcessedText { get; set; }

    }
}
