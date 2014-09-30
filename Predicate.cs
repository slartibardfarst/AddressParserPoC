using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewAddressParserPoC
{
    public class Predicate
    {
        private static Regex _predRegex = new Regex(@"pred\((\w+),\s*(\d+)\)");

        public string name;
        public int numParams;
        public Func<string, bool> fs;
        public Func<string, string, bool> _fss;
        public Func<string, string, string, bool> _fsss;

        public Predicate(string definition)
        {
            var match = _predRegex.Match(definition);
            if(!match.Success)
                throw new Exception("bad defn");

            name = match.Groups[1].Value;
            numParams = int.Parse(match.Groups[2].Value);
        }
    }
}
