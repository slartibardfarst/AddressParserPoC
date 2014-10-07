using NewAddressParserPoC.Try3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC
{
    public class AddressParser3
    {
        private List<Pattern> _patterns;


        private void DefineAddressPatterns()
        {
            TokenParser street_no_1 = new TokenParser(TokenParsers.StreetNumber_1);
            TokenParser street_1 = new TokenParser(TokenParsers.Street_1);
            TokenParser suffix_1 = new TokenParser(TokenParsers.Suffix_1);
            TokenParser city_1 = new TokenParser(TokenParsers.City_1);
            TokenParser city_2 = new TokenParser(TokenParsers.City_2);
            TokenParser state_1 = new TokenParser(TokenParsers.State_1);
            TokenParser zip_1 = new TokenParser(TokenParsers.Zip_1);
            TokenParser dir_1 = new TokenParser(TokenParsers.Dir_1);

            _patterns = new List<Pattern>
            {
               new Pattern (new List<TokenParser> {street_no_1, street_1, suffix_1, city_1, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_1, suffix_1, city_2, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_1, city_1, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_1, city_2, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, dir_1, street_1, suffix_1, city_1, state_1, zip_1 })
            };
        }



        public AddressParser3()
        {
            DefineAddressPatterns();
        }

        public AddressEx ParseAddress(string addressString)
        {
            string[] tokens = BreakStringIntoTokens(addressString);

            List<AddressEx> patternMatches = new List<AddressEx>();
            foreach (var pattern in _patterns)
            {
                AddressEx match;
                if (IsPatternMatch(pattern, tokens, out match))
                    patternMatches.Add(match);
            }

            var result = ChooseBestMatch(patternMatches);
            return result;
        }

        private string[] BreakStringIntoTokens(string addressString)
        {
            var result = addressString.Split(new char[] { ' ', ',' });
            return result;
        }

        private bool IsPatternMatch(Pattern pattern, string[] tokens, out AddressEx match)
        {
            match = null;

            if (tokens.Length != pattern.NumTokensInPattern)
                return false;

            var i = 0;
            var result = new AddressEx();

            foreach (var tokenParser in pattern.PatternItems)
            {
                if (!tokenParser.IsTokenMatch(tokens, result, ref i))
                {
                    return false;
                }
            }

            if (null != result)
                result.RebuildAddressLine();

            match = result;
            return true;
        }


        private AddressEx ChooseBestMatch(List<AddressEx> patternMatches)
        {
            if (patternMatches.Count == 0)
                return null;

            if (patternMatches.Count == 1)
                return patternMatches[0];

            if (patternMatches.Count == 2)
            {
                if (string.IsNullOrEmpty(patternMatches[0].street_suffix) &&
                    !string.IsNullOrEmpty(patternMatches[1].street_suffix))
                    return patternMatches[1];

                if (string.IsNullOrEmpty(patternMatches[1].street_suffix) &&
                    !string.IsNullOrEmpty(patternMatches[0].street_suffix))
                    return patternMatches[0];

            }

            throw new NotImplementedException("can't handle multiple matches");
        }


    }
}
