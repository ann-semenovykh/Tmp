using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrawingPanel;
using System.Drawing;

namespace TriadNSim.SimulationModel.PetriNetModel
{
    [Serializable]
    class PetriMark: DynamicObject
    {
        public PetriMark(DrawingPanel.DrawingPanel dp,CConnectionPoint cp):base(dp,cp)
        {

        }
        public override string ObjectType
        {
            get
            {
                return "PetriMark";
            }
        }
        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            MultChange();
            if (mult > 0)
            {
                System.Drawing.Pen myPen = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom) * 4);
                myPen.DashStyle = this.dashStyle;

                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                myPen.Color = this.Trasparency(this.penColor, this.alpha);
                if (this.bSelected)
                {
                    myPen.Color = Color.Red;
                    myPen.Color = this.Trasparency(myPen.Color, 120);
                    myPen.Width = myPen.Width + 1;
                    g.DrawEllipse(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, 3, 3);
                }
                g.DrawEllipse(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, zoom * 6, zoom * 6);
                g.FillEllipse(myBrush, (this.X + dx) * zoom, (this.Y + dy) * zoom, zoom * 6, zoom * 6);
                myPen.Dispose();
                if (Name != null)
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Near;

                    Font font = new Font("Arial", 10 * zoom);
                    SizeF size = g.MeasureString(Name, font);
                    g.DrawString(Name, font, new SolidBrush(Color.Black), new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom - 12, size.Width, size.Height), stringFormat);
                    font.Dispose();
                    stringFormat.Dispose();
                }
            }
        }
    }
}
