using System;
using DrawingPanel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TriadNSim
{
    [Serializable]
    public class PetriLink: PetriLine
    {
        public PetriLink(DrawingPanel.DrawingPanel panel, CConnectionPoint From, CConnectionPoint To,int x,int y,int x1,int y1)
            : base(panel, From, To,x,y,x1,y1)
        {
            penColor = panel.CreationPenColor;
            penWidth = panel.CreationPenWidth;
            Name = "";

        }

        public Polus PolusFrom { set; get; }
        public Polus PolusTo { set; get; }
        //public override void Draw(Graphics g, int dx, int dy, float zoom)
        //{
        //}

    }
}
