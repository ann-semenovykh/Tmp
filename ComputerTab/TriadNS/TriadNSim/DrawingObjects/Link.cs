using System;
using DrawingPanel;

namespace TriadNSim
{
    [Serializable]
    public class Link : Line
    {
        public Link(DrawingPanel.DrawingPanel panel, CConnectionPoint From, CConnectionPoint To)
            : base(panel, From, To)
        {
            penColor = panel.CreationPenColor;
            penWidth = panel.CreationPenWidth;
        }
        public Link(DrawingPanel.DrawingPanel panel, CConnectionPoint From, CConnectionPoint To,int x,int y,int x1,int y1)
            : base(panel, From, To,x,y,x1,y1)
        {
            penColor = panel.CreationPenColor;
            penWidth = panel.CreationPenWidth;
        }
        public Polus PolusFrom { set; get; }
        public Polus PolusTo { set; get; }
    }
}
