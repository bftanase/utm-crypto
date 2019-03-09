using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace BftCryptoCore.Services
{
    public class CryptoSubstitutionService : ICryptoService
    {
        private readonly List<char> _alphabet;
        private readonly string spaceReplacement = "Q";

        public CryptoSubstitutionService()
        {
            _alphabet = new List<char>();

            // construim alfabetul. Suportam doar caractere mari de la A-Z
            var aUpper = Convert.ToUInt16('A');
            var zUpper = Convert.ToUInt16('Z');

            for (var i = aUpper; i <= zUpper; i++)
            {
                _alphabet.Add(Convert.ToChar(i));
            }
        }

        public string Encrypt(string key, string text)
        {
            // stergere whitespace si eliminare duplicate
            var keyCharacters = Regex.Replace(key, @"\s+", "").ToUpper().Distinct().ToList();
            // inlocuim spatiile cu un grup de litere cu sanse minime de aparitie
            // naturala intr-un text
            text = Regex.Replace(text, @"\s+", spaceReplacement).ToUpper();

            var cipherMap = BuildCipherMap(keyCharacters);

            var result = string.Concat(text.Select(x => cipherMap[x]));
            return result;
        }

        /// <summary>
        /// construieste corespondenta caracterelor pentru cifrare
        /// </summary>
        /// <param name="keyCharacters"></param>
        /// <returns></returns>
        private Dictionary<char, char> BuildCipherMap(List<char> keyCharacters)
        {
            var cipherMap = new Dictionary<char, char>();
            var alphabetCopy = _alphabet.ToList();

            // eliminam caracterele ce se regasesc in cheie
            foreach (var k in keyCharacters)
            {
                alphabetCopy.Remove(k);
            }

            // combinam caractelere din cheie cu restul alfabetului si inversam lista (partea de permutare)
            keyCharacters.AddRange(alphabetCopy);
            keyCharacters.Reverse();

            for (var i = 0; i < keyCharacters.Count; i++)
                cipherMap.Add(_alphabet[i], keyCharacters[i]);

            return cipherMap;
        }

        public string Decrypt(string key, string text)
        {
            // stergere whitespace si eliminare duplicate
            var keyCharacters = Regex.Replace(key, @"\s+", "").ToUpper().Distinct().ToList();
            text = text.ToUpper();

            var cipherMap = BuildCipherMap(keyCharacters);
            var reverseCipherMap = cipherMap.ToDictionary(x => x.Value, x => x.Key);

            var result = string.Concat(text.Select(x => reverseCipherMap[x]));

            return result.Replace(spaceReplacement, " ");
        }
    }
}