using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawingPanel
{
    [Serializable]
    public class PetriMark:BaseObject
    {
        private CConnectionPoint _CP;
        public int mult;
        public PetriMark(DrawingPanel panel, CConnectionPoint cp)
            : base(panel)
        {
            this._CP = cp;
            _CP.Marks.Add(this);
            this.X = cp.getX() + _CP.Width / 2;//cp.Owner.getX()+(cp.Owner.getX1()-cp.Owner.getX()-cp.Width)/2;
            this.Y = cp.getY() + _CP.Width / 2;//cp.Owner.getY()+(cp.Owner.getY1() - cp.Owner.getY()-cp.Width) / 2 ;
            this.bIsLine = false;
            this.bSelected = true;
            this.bIsMark = true;
            mult = 1;
        }
        public string ObjectType
        {
            get
            {
                return "PetriMark";
            }
        }
        public CConnectionPoint CP
        {
            get { return _CP; }
            set
            {
                if (_CP != null)
                    _CP.Marks.Remove(this);
                _CP = value;
                _CP.Marks.Add(this);
                RefreshPosition();
            }
        }
        public void RefreshPosition()
        {
            X = _CP.getX() + _CP.Width / 2;
            Y =_CP.getY()+  _CP.Width / 2;
            endMoveRedim();
        }
        public void IncMult()
        {
            mult++;
            MultChange();
        }
        public void DecMult()
        {
            if (mult > 0)
                mult--;
            MultChange();
        }
        public void MultChange()
        {
            if (mult > 1)
                Name = "[" + mult.ToString() + "]";
            else Name = "";

        }
        public override void Delete()
        {
            
            {
                _CP.Marks.Remove(this);
                
                base.Delete();
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

                    Font font = new Font("Arial", 16 * zoom);
                    SizeF size = g.MeasureString(Name, font);
                    g.DrawString(Name, font, new SolidBrush(Color.Blue), new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom - 12, size.Width, size.Height), stringFormat);
                    font.Dispose();
                    stringFormat.Dispose();
                }
            }
        }
    }
}
