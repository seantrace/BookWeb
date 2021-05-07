using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWeb.Client.Pages.Utilities
{
    public interface IAlphabetProvider
    {
        IEnumerable<char> GetAlphabet();
        IEnumerable<char> GetNumbersAlphabet();
    }

    public class EnglishAlphabetProvider : IAlphabetProvider
    {
        public IEnumerable<char> GetAlphabet()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                yield return c;
            }
        }

        public IEnumerable<char> GetNumbersAlphabet()
        {
            for (char c = '0'; c <= '9'; c++)
            {
                yield return c;
            }
            for (char c = 'A'; c <= 'Z'; c++)
            {
                yield return c;
            }
        }


    }
}
