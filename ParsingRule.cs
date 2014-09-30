using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC
{
    public class ParsingRule
    {
        private List<List<ParsingRule>> _definitions;
        private List<Predicate> _predicates;

        public string Name { get; private set; }
        public List<List<ParsingRule>> Definitions { get { return _definitions; } }
        public List<Predicate> Predicates { get { return _predicates; } }

        public ParsingRule()
        {
            _definitions = null;
            _predicates = null;
        }

        public ParsingRule(string name) :this()
        {
            Name = name;
        }

        internal void AddDefinition(List<ParsingRule> newRuleDefinition)
        {
            if(_predicates != null)
                throw new Exception("Rule cannot have both a definition and a predicate");

            if(_definitions == null)
                _definitions = new List<List<ParsingRule>>();

            _definitions.Add(newRuleDefinition);
        }

        internal void AddPred(string definition)
        {
            if (_definitions != null)
                throw new Exception("Rule cannot have both a definition and a predicate");

            if (_predicates == null)
                _predicates = new List<Predicate>();

            var p = new Predicate(definition);
            _predicates.Add(p);
        }
    }
}