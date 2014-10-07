using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC.Try3
{
    public class TokenParser
    {
        public int Arity { get; private set; }

        private Func<string, AddressEx, bool> f1;
        private Func<string, string, AddressEx, bool> f2;
        private Func<string, string, string, AddressEx, bool> f3;

        public TokenParser(Func<string, AddressEx, bool> f)
        {
            f1 = f;
            Arity = 1;
        }

        public TokenParser(Func<string, string, AddressEx, bool> f)
        {
            f2 = f;
            Arity = 2;
        }

        public TokenParser(Func<string, string, string, AddressEx, bool> f)
        {
            f3 = f;
            Arity = 3;
        }


        public bool IsTokenMatch(string[] tokens, AddressEx acc, ref int i)
        {
            bool result = false;

            switch (Arity)
            {
                case 1:
                    result = f1(tokens[i], acc);
                    if (result)
                        i += 1;
                    break;

                case 2:
                    result = f2(tokens[i], tokens[i + 1], acc);
                    if (result)
                        i += 2;
                    break;

                case 3:
                    result = f3(tokens[i], tokens[i + 1], tokens[i + 2], acc);
                    if (result)
                        i += 3;
                    break;

                default:
                    throw new Exception("no function available");
            }

            return result;
        }
    }
}
