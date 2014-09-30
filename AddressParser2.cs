using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC
{
    class Sequence
    {
        public List<GrammarRule> sequence;

        public Sequence()
        { }

        public Sequence(List<GrammarRule> s)
        {
            sequence = s;
        }

        internal Sequence Clone()
        {
            Sequence result = new Sequence();
            if (null != sequence)
            {
                result.sequence = new List<GrammarRule>();
                foreach (var rule in sequence)
                    result.sequence.Add(rule.Clone());
            }

            return result;
        }

        internal void AddStep(GrammarRule grammarRule)
        {
            if (null == sequence)
                sequence = new List<GrammarRule>();

            sequence.Add(grammarRule);
        }
    }

    class GrammarRule
    {
        public string name;
        public List<Sequence> definitions;
        private string p;

        public GrammarRule(string n)
        {
            name = n;
        }

        internal void AddDefinitionSequence(Sequence sequence)
        {
            if (null == definitions)
                definitions = new List<Sequence>();

            definitions.Add(sequence);
        }

        internal GrammarRule Clone()
        {
            GrammarRule clone = new GrammarRule(name);
            if (null != definitions)
                foreach (var sequence in definitions)
                    clone.AddDefinitionSequence(sequence.Clone());

            return clone;
        }
    }

    class Node
    {
        public string Name { get; private set; }
        public List<Node> Children { get; private set; }

        public Node(string n)
        {
            Name = n;
        }

        public void AddChild(Node child)
        {
            if (null == Children)
                Children = new List<Node>();

            Children.Add(child);
        }


        public Node Clone()
        {
            Node result = new Node(Name);
            if (null != Children)
                foreach (var child in Children)
                    result.AddChild(child.Clone());

            return result;
        }

        internal void SetNextNodes(List<Node> nextNodes)
        {
            if (null == Children)
            {
                Children = nextNodes;
            }
            else
            {
                foreach (var child in Children)
                    child.SetNextNodes(nextNodes);
            }
        }
    }


    class AddressParser2
    {
        //        private string _rules = @"address : address_line, city, state, zip;
        //
        //                                  address_line : street_no, street_name, street_suffix;
        //
        //                                  street_no : pred(IsStreetNo, 1);
        //
        //                                  street_name : pred(IsStreetName,1);
        //                                  street_name : pred(IsStreetName,2);
        //
        //                                  street_suffix : pred(IsStreetSuffix,1);
        //
        //                                  city : pred(IsCity,1);
        //
        //                                  state : pred(IsState,1);
        //
        //                                  zip : pred(IsZip,1);
        //                                 ";
        //        private string _rules = @"address : a,b,c;
        //                                         b: b1;
        //                                         b: b2;
        //
        //                                         a: pred(a,1);
        //                                         b1: pred(b1,1);
        //                                         b2: pred(b2,1);
        //                                         c: pred(c,1);
        //                                 ";

//        private string _rules = @"address : a,b,c;
//                                         b: b1;
//                                         b: b2;
//                                 ";

        private string _rules = @"address : a,b,c;
                                  address : a,c;
                                         b: b1;
                                         b: b2;
                                         c: c1;
                                         c: c2;

                                 ";




        private RulesManager _rulesManager;

        public AddressParser2()
        {
            List<Node> definitions = LoadRuleDefinitions(_rules);
            List<Node> rules = BuildRulesFromDefinitions(definitions);
            PrintPaths(rules[0], null);
        }

        private void PrintPaths(Node root, List<string> acc)
        {
            if (null == acc)
                acc = new List<string>();

            acc.Add(root.Name);

            if (null == root.Children)
                Console.WriteLine(string.Join(", ", acc));
            else
            {
                foreach (var child in root.Children)
                {
                    List<string> copy = new List<string>(acc);
                    PrintPaths(child, copy);
                }
            }
        }


        private List<Node> BuildRulesFromDefinitions(List<Node> definitions)
        {
            List<Node> rules = CloneRules(definitions);
            bool didSubstitution;

            bool done = false;
            do
            {
                foreach (var rule in rules)
                    done |= Substitute(definitions, rule);
            } while (!done);

            return rules;
        }

        private bool Substitute(List<Node> definitions, Node parent)
        {
            bool didSubstitution = false;

            if (null == parent.Children)
                return false;

            for (int i = 0; i < parent.Children.Count; i++)
            {
                Node sub;
                if (FindDefinition(definitions, parent.Children[i], out sub))
                {
                    List<Node> nextNodes = parent.Children[i].Children;
                    sub.SetNextNodes(nextNodes);
                    //parent.Children[i] = sub;
                    parent.Children.RemoveAt(i);
                    foreach (var newChild in sub.Children)
                        parent.AddChild(newChild);
                    didSubstitution = true;
                }

                didSubstitution |= Substitute(definitions, parent.Children[i]);
            }

            return didSubstitution;
        }

        private bool FindDefinition(List<Node> definitions, Node node, out Node sub)
        {
            foreach (var def in definitions)
                if (def.Name.Equals(node.Name))
                {
                    sub = def.Clone();
                    return true;
                }
            sub = null;
            return false;
        }

        private GrammarRule FindDefinitionByName(List<GrammarRule> definitions, string name)
        {
            foreach (var rule in definitions)
                if (rule.name == name)
                    return rule;

            return null;
        }

        private List<Node> CloneRules(List<Node> rules)
        {
            List<Node> result = new List<Node>();
            foreach (var rule in rules)
                result.Add(rule.Clone());

            return result;
        }

        private List<Node> LoadRuleDefinitions(string allRules)
        {
            List<Node> result = new List<Node>();

            string[] rules = allRules.Trim().Split(new char[] { ';' });
            foreach (var rule in rules)
            {
                if (string.IsNullOrEmpty(rule))
                    continue;

                string[] ruleParts = rule.Split(new char[] { ':' });
                if (ruleParts.Length != 2)
                    throw new Exception("bad rule");

                var ruleName = ruleParts[0].Trim();
                Node ruleNode = FindExistingRule(result, ruleName);
                if (null == ruleNode)
                {
                    ruleNode = new Node(ruleName);
                    result.Add(ruleNode);
                }

                var seqStart = CreateSequence(ruleParts[1]);
                ruleNode.AddChild(seqStart);
            }

            return result;
        }

        private Node CreateSequence(string sequenceString)
        {
            Node first = null;
            Node curr = null;

            foreach (var step in sequenceString.Split(new char[] { ',' }))
            {
                Node node = new Node(step);
                if (null == first)
                {
                    first = node;
                    curr = first;
                }
                else
                {
                    curr.AddChild(node);
                    curr = node;
                }
            }

            return first;
        }

        private Node FindExistingRule(List<Node> rulesList, string name)
        {
            foreach (var rule in rulesList)
                if (rule.Name == name)
                    return rule;

            return null;
        }

        private Node CreateRule(string name, string sequenceString)
        {
            Node root = new Node(name.Trim());

            Node curr = root;
            foreach (var step in sequenceString.Split(new char[] { ',' }))
            {
                Node node = new Node(step);
                curr.AddChild(node);
                curr = node;
            }

            return root;
        }

        public AddressEx ParseAddressString(string addressString)
        {
            throw new NotImplementedException();
        }



        private object ExpandRulesTree(ParsingRule rulesTreeRoot)
        {
            throw new NotImplementedException();
        }

        private List<string> TokenizeAddressString(string addressString)
        {
            throw new NotImplementedException();
        }

    }
}
