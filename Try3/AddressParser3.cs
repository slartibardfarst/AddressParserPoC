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
        private static readonly char[] _space = {' '};
        private static StandardAbbreviations _abbr = new StandardAbbreviations();


        private void DefineAddressPatterns()
        {
            TokenParser street_no_1 = new TokenParser(TokenParsers.StreetNumber_1);
            TokenParser pre_dir_1 = new TokenParser(TokenParsers.Predirectional_1);
            TokenParser street_1 = new TokenParser(TokenParsers.Street_1);
            TokenParser street_2 = new TokenParser(TokenParsers.Street_2);
            TokenParser suffix_1 = new TokenParser(TokenParsers.Suffix_1);
            TokenParser city_1 = new TokenParser(TokenParsers.City_1);
            TokenParser city_2 = new TokenParser(TokenParsers.City_2);
            TokenParser state_1 = new TokenParser(TokenParsers.State_1);
            TokenParser zip_1 = new TokenParser(TokenParsers.Zip_1);
            TokenParser dir_1 = new TokenParser(TokenParsers.Dir_1);
            TokenParser unit_desc_1 = new TokenParser(TokenParsers.UnitDesc_1);
            TokenParser unit_value_1 = new TokenParser(TokenParsers.UnitValue_1);

            _patterns = new List<Pattern>
            {
               new Pattern (new List<TokenParser> {street_no_1, street_1, suffix_1, city_1, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_1, suffix_1, city_2, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_1, city_1, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_1, city_2, state_1, zip_1 }),

               new Pattern (new List<TokenParser> {street_no_1, street_1, suffix_1, unit_value_1, city_2, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_2, unit_value_1, city_2, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_2, suffix_1, unit_value_1, city_2, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_2, unit_value_1, city_1, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, pre_dir_1, street_1, city_1, state_1, zip_1 }),

               new Pattern (new List<TokenParser> {street_no_1, dir_1, street_1, suffix_1, city_1, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {city_1, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {city_2, state_1, zip_1 }),
               new Pattern (new List<TokenParser> {street_no_1, street_1, suffix_1, unit_desc_1, unit_value_1, city_1, state_1, zip_1 }),
            };
        }



        public AddressParser3()
        {
            DefineAddressPatterns();
        }

        public AddressEx ParseAddress(string addressString)
        {
            var tokens = BreakStringIntoTokens(addressString);
            var matches = FindAllParseMatches(tokens, _patterns);
            var result = ChooseBestMatch(matches);

            return result;
        }

        private string[] BreakStringIntoTokens(string addressString)
        {
            var items = addressString.Split(new char[] { ' ', ',' });

            List<string> nonNullTokens = new List<string>();
            foreach (var token in items)
            {
                var t = token.Trim();
                if (!string.IsNullOrEmpty(t))
                    nonNullTokens.Add(t);
            }

            return nonNullTokens.ToArray();
        }

        private List<AddressEx> FindAllParseMatches(string[] currAddressTokens, List<Pattern> patterns)
        {
            List<AddressEx> patternMatches = new List<AddressEx>();
            foreach (var pattern in patterns)
            {
                AddressEx match;
                if (IsPatternMatch(pattern, currAddressTokens, out match))
                    patternMatches.Add(match);
            }

            return patternMatches;
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

            var scoredCandidates = ScoreParseCandidates(patternMatches);
            scoredCandidates = BreakScoringTies(scoredCandidates);

            scoredCandidates.Sort((a, b) => b.Item1.CompareTo(a.Item1));

            return scoredCandidates[0].Item2;
        }



        public List<Tuple<int, AddressEx>> ScoreParseCandidates(List<AddressEx> candidates)
        {
            var result = new List<Tuple<int, AddressEx>>();
            foreach (var currCandidate in candidates)
                result.Add(new Tuple<int, AddressEx>(GetParseCandidateScrore(currCandidate), currCandidate));

            return result;
        }

        private int GetParseCandidateScrore(AddressEx a)
        {
            //every candidate starts with a perfect score of 100.
            //problems with the parse reduce that value;
            int score = 100;

            if (string.IsNullOrEmpty(a.street_suffix))
                score -= 10;

            if (a.street.Contains(" "))
            {
                var streetNameParts = a.street.Split(_space);
                if (streetNameParts.Length > 1 && _abbr.AllStreetSuffixes.Contains(streetNameParts[streetNameParts.Length - 1].Trim().ToLower()))
                {
                    score -= 20;
                }
            }

            if (_abbr.AllDirectionals.Contains(a.street.ToLower()))
                score -= 20;


            return score;
        }        
        
        private List<Tuple<int, AddressEx>> BreakScoringTies(List<Tuple<int, AddressEx>> scoredCandidates)
        {
            //not implemented
            return scoredCandidates;
        }

    }
}
