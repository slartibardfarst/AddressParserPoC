using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC
{
    internal class RulesManager
    {
        private List<ParsingRule> _rules;

        public RulesManager()
        {
            _rules = new List<ParsingRule>();
        }

        public void BuildRulesTree(string allRules)
        {
            string[] rules = allRules.Trim().Split(new char[] { ';' });
            foreach (var rule in rules)
            {
                if (string.IsNullOrEmpty(rule))
                    continue;

                string[] ruleParts = rule.Split(new char[] { ':' });
                if (ruleParts.Length != 2)
                    throw new Exception("bad rule");

                AddRule(ruleParts[0].Trim(), ruleParts[1].Trim());
            }
        }

        private void AddRule(string name, string definition)
        {
            var rule = FindOrAddRuleByName(name);

            if (RuleDefinitionIsPred(definition))
                rule.AddPred(definition);
            else
            {
                //rule definitino is a sequence of other rules names
                string[] sequenceItems = definition.Split(new char[] { ',' });
                List<ParsingRule> newRuleDefinition = new List<ParsingRule>();
                foreach (var sequenceItemName in sequenceItems)
                    newRuleDefinition.Add(FindOrAddRuleByName(sequenceItemName.Trim()));

                rule.AddDefinition(newRuleDefinition);
            }

        }

        private ParsingRule FindOrAddRuleByName(string name)
        {
            foreach (var rule in _rules)
                if (rule.Name == name)
                    return rule;

            var newRule = new ParsingRule(name);
            _rules.Add(newRule);
            return newRule;
        }


        private bool RuleDefinitionIsPred(string definition)
        {
            return definition.Contains("pred");
            //return (definition.Substring(0, 4).ToLower() == "pred");
        }


        public void Expand(string rootName)
        {
            var root = FindOrAddRuleByName(rootName);
            ExpandAllRecursive(root);
        }

        //private void ExpandAllRecursive(ParsingRule rule)
        //{
        //    if (rule.Definitions != null)
        //    {
        //        foreach (var definition in rule.Definitions)
        //        {
        //            Console.WriteLine();
        //            foreach (var step in definition)
        //                ExpandAllRecursive(step);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var pred in rule.Predicates)
        //            Console.Write("{0}, ", pred.name);
        //    }
        //}

        private void ExpandAllRecursive(ParsingRule rule)
        {
            if (rule.Definitions != null)
            {
                foreach (var definition in rule.Definitions)
                {
                    foreach (var step in definition)
                    {
                        if (step.Predicates != null)
                            Console.Write("{0}, ", step.Name);
                        else
                        {
                            ExpandAllRecursive(step);
                        }
                    }
                }
            }
        }

    }
}
