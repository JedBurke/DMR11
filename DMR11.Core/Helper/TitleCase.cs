using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core.Helper
{    
    public class WordConverter
    {
        public static string ToTitleCase(string input)
        {
            return ToTitleCase(input, null);
        }

        public static string ToTitleCase(string input, string[] excludedWords)
        {
            if (string.IsNullOrEmpty(input))
                throw new NullReferenceException();

            var titleCasedString = new StringBuilder();

            var splitWords = input.Split(' ');

            for (var wordIndex = 0; wordIndex < splitWords.Length; wordIndex++)
            {
                // Drop the casing.
                var currentWord = splitWords[wordIndex].ToLowerInvariant();

                // Capitialize the first word, last word, and words which aren't excluded.
                if (wordIndex == 0 || wordIndex == splitWords.Length -1 || (excludedWords != null && Array.IndexOf(excludedWords, currentWord) == -1))
                {
                    currentWord = CapitalizeWord(currentWord);
                }

                // Todo: Don't capitalize a single-lettered word which isn't I.

                // Append a space to the title-cased sentence with all words except the first one.
                if (wordIndex > 0)
                {
                    titleCasedString.Append(" ");
                }

                titleCasedString.Append(currentWord);
            }

            return titleCasedString.ToString();
        }

        public static string CapitalizeWord(string input)
        {
            string capitalizedWord = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                capitalizedWord = input;
            }
            else
            {
                if (input.Length == 1)
                {
                    capitalizedWord = input.ToUpperInvariant();
                }
                else
                {
                    capitalizedWord = string.Concat(input[0].ToString().ToUpperInvariant(), input.Substring(1));
                }
            }

            return capitalizedWord;

        }

        public static readonly string[] ExclusionsJpn = new string[] {
            ""
        };

        public static readonly string[] ExclusionsEng = new string[] {
            // Articles
            "a",
            "an",
            "the",

            // Conjunctions
            "and",

            // Prepositions
            "aboard",
            "about",
            "above",
            "across",
            "after",
            "against",
            "along",
            "amid",
            "among",
            "anti",
            "around",
            "as",
            "at",            
            "before",
            "behind",
            "below",
            "beneath",
            "beside",
            "besides",
            "between",
            "beyond",
            "but",
            "by",
            "concerning",
            "considering",
            "despite",
            "down",
            "during",
            "except",
            "excepting",
            "excluding",
            "following",
            "for",
            "from",
            "in",
            "inside",
            "into",
            "like",
            "minus",
            "near",
            "of",
            "off",
            "on",
            "onto",
            "opposite",
            "outside",
            "over",
            "past",
            "per",
            "plus",
            "regarding",
            "round",
            "save",
            "since",
            "than",
            "through",
            "to",
            "toward",
            "towards",
            "under",
            "underneath",
            "unlike",
            "until",
            "up",
            "upon",
            "versus",
            "via",
            "with",
            "within",
            "without"
        };

    }
}
