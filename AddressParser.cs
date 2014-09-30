using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC
{
    class AddressParser
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
        private string _rules = @"address : a,b,c;
                                         b: b1;
                                         b: b2;

                                         a: pred(a,1);
                                         b1: pred(b1,1);
                                         b2: pred(b2,1);
                                         c: pred(c,1);
                                 ";


        private RulesManager _rulesManager;

        public AddressParser()
        {
            _rulesManager = new RulesManager();
            _rulesManager.BuildRulesTree(_rules);
            _rulesManager.Expand("address");
            //_rulesManager.ExpandRulesTree(rulesTree);
        }

        public AddressEx ParseAddressString(string addressString)
        {
            throw new NotImplementedException();
            List<AddressEx> candidates = new List<AddressEx>();
            List<string> tokenizedAddressString = TokenizeAddressString(addressString);
            
            //foreach rule in rulesList.Select(numTokensInRule == tokens.Count()
            //{
            //if(rule.IsMatch(tokenizedAddressString)
            //   candidates.Add(rule)
            //}
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
