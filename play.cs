using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAddressParserPoC
{
    class El
    {
        public string name;
        public List<El> children;
        public bool terminal;

        public El(string n, bool t)
        {
            name = n;
            terminal = t;
            children = new List<El>();
        }
    }
    class Play
    {
        public Play()
        {
        }


        internal void Go()
        {
            El root = BuildTree();
            PrintPaths(root, new List<string>());
        }

        private void PrintPaths(El root, List<string> acc)
        {
            if (root.terminal)
                acc.Add(root.name);

            if (root.children.Count == 0)
                Console.WriteLine(string.Join(", ", acc));

            foreach (var child in root.children)
            {
                List<string> copy = new List<string>(acc);
                PrintPaths(child, copy);
            }
        }

        //private El BuildTree()
        //{
        //    var b = new El("b", true);
        //    var c = new El("c", false);
        //    var d = new El("d", true);
        //    var e = new El("e", true);
        //    var f = new El("f", true);

        //    d.children.Add(f);
        //    e.children.Add(f);
        //    c.children.Add(d);
        //    c.children.Add(e);
        //    b.children.Add(c);

        //    return b;
        //}

        private El BuildTree()
        {
            var a = new El("a", true);

            var b1 = new El("b1", true);
            var b2 = new El("b2", true);
            var b3 = new El("b3", true);
            var b4 = new El("b4", true);

            var c1 = new El("c1", true);
            var c2 = new El("c2", true);
            var c3 = new El("c3", true);
            var c4 = new El("c4", true);

            a.children.Add(b1);
            a.children.Add(b2);

            b1.children.Add(b3);
            b2.children.Add(b4);

            b3.children.Add(c1);
            b3.children.Add(c2);
            b4.children.Add(c3);
            b4.children.Add(c4);

            return a;
        }

    }
}
