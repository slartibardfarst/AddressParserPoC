using NewAddressParserPoC.Try3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC
{
    class AddressParser3
    {
        private List<Pattern> _patterns;
        public AddressParser3()
        {
            TokenParser street_no_1 = new TokenParser(TokenParserImplementations.StreetNumber_1);
            TokenParser street_1 = new TokenParser(TokenParserImplementations.Street_1);
            TokenParser suffix_1 = new TokenParser(TokenParserImplementations.Suffix_1);
            TokenParser city_1 = new TokenParser(TokenParserImplementations.City_1);
            TokenParser city_2 = new TokenParser(TokenParserImplementations.City_2);
            TokenParser state_1 = new TokenParser(TokenParserImplementations.State_1);
            TokenParser zip_1 = new TokenParser(TokenParserImplementations.Zip_1);

            _patterns = new List<Pattern>
            {
               new Pattern (new List<TokenParser> {street_no_1, street_1, suffix_1, city_1, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_1, suffix_1, city_2, state_1, zip_1 })
            };
        }


        public void Go()
        {
            //List<string> tokens = new List<string> { "123", "Main", "Street", "Seattle", "WA", "90210" };
            List<string> tokens = new List<string> { "123", "Main", "Street", "Port", "Orchard", "WA", "90210" };

            foreach (var pattern in _patterns)
            {
                AddressEx result = PatternMatch(pattern, tokens);
            }
        }

        public AddressEx PatternMatch(Pattern pattern, List<string> tokens)
        {
            if (tokens.Count != pattern.NumTokensInPattern)
                return null;

            var i = 0;
            var result = new AddressEx();

            foreach (var tp in pattern.PatternItems)
            {
                if (!tp.TryParse(tokens, result, ref i))
                {
                    return null;
                }
            }

            return result;
        }

        private AddressEx MergeAddresses(List<AddressEx> elements)
        {
            return null;
        }
    }
}
