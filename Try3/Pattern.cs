using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC.Try3
{
    class Pattern
    {
        public int NumTokensInPattern {get; private set;}
        public List<TokenParser> PatternItems { get; private set; }

        public Pattern(List<TokenParser> patternItems)
        {
            PatternItems = patternItems;

            int acc = 0;
            foreach (var tp in patternItems)
                acc += tp.Arity;

            NumTokensInPattern = acc;
        }
    }
}
