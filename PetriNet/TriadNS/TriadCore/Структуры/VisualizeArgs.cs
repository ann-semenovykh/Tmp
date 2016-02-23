using System;
using System.Collections.Generic;
using System.Text;
using TriadCore.Структуры;

namespace TriadCore
{
    public class VisualizeArgs : EventArgs
    {
        public VisualizeArgs()//Node node,object n)
        {
           
            Nodes=new Dictionary<Node,int>();
            EndModeling = false;
        }
        public Dictionary<Node, int> Nodes; 
        public Node CurNode { get; set; }
        public int Num { get; set; }
        public bool EndModeling { get; set; }
    }
}
