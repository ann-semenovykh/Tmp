using System;
using System.Collections.Generic;
using System.Text;

namespace TriadCore
{
    public class VisualizeArgs:EventArgs
    {
        public  VisualizeArgs(Node node,object n)
        {
            CurNode = node;
            Num = n;
        }
        public Node CurNode {get;private set; }
        public object Num { get; private set; }
    }
}
