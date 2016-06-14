using DrawingPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TriadNSim.Transformer
{
    [Serializable]
    public class TransformationRule
    {
        public ArrayList leftPart=new ArrayList();
        public ArrayList rightPart=new ArrayList();
        public string Name;
        public TransformationRule()
        {
        }

    }
}
