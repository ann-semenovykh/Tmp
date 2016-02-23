using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawingPanel
{
    /// <summary>
    /// LineConnector
    /// </summary>
    [Serializable]
    public class PetriLine : BaseObject
    {
        private LineCap _starCap;
        private LineCap _endCap;
        private CConnectionPoint _FromCP;
        private CConnectionPoint _ToCP;
        private bool _Moving;
        private bool _MoveFrom;
        private double propXl,propXm, propYl,propYm,propX1l,propX1m,propY1l,propY1m;
        public PetriLine(DrawingPanel panel, CConnectionPoint From, CConnectionPoint To,int x,int y,int x1,int y1)
            : base(panel)
        {
            this._FromCP = From;
            From.Connectors.Add(this);
            this._ToCP = To;
            To.Connectors.Add(this);
            this.X = x;
            this.Y = y;
            this.X1 =x1 ;
            this.Y1 = y1 ;
            this.bIsLine = true;
            this.bSelected = true;
            this.endCap = LineCap.ArrowAnchor;
            this.starCap = LineCap.NoAnchor;
            //this.endCap = LineCap.Square;
            this.endMoveRedim();
            this.bCanRotate = false;
            this._Moving = false;
            this._MoveFrom = false;
            mult = 1;
            changeprop();
        }
        private void changeprop()
        {
            propXl = (this.X - _FromCP.getX());
            propXm = (_FromCP.getX1() - this.X);
            propX1l = (this.X1 - _ToCP.getX());
            propX1m = (_ToCP.getX1() - this.X1);
            propYl = (this.Y - _FromCP.getY());
            propYm = (_FromCP.getY1() - this.Y);
            propY1l = (this.Y1 - _ToCP.getY());
            propY1m = (_ToCP.getY1() - this.Y1);
        }
        public int mult { set; get; }
        public void IncMult()
        {
            mult++;
            MultChange();
        }
        public void DecMult()
        {
            if (mult > 1)
                mult--;
            MultChange();
        }
       

        [CategoryAttribute("Line Appearance"), DescriptionAttribute("Line Start Cap")]
        public LineCap starCap
        {
            get
            {
                return _starCap;
            }
            set
            {
                _starCap = value;
            }
        }

        [CategoryAttribute("Line Appearance"), DescriptionAttribute("Line End Cap")]
        public LineCap endCap
        {
            get
            {
                return _endCap;
            }
            set
            {
                _endCap = value;
            }
        }

        [CategoryAttribute("1"), DescriptionAttribute("PetriLine")]
        public string ObjectType
        {
            get
            {
                return "PetriLine";
            }
        }

        public CConnectionPoint FromCP
        {
            get { return _FromCP; }
            set
            {
                if (_FromCP != null)
                    _FromCP.Connectors.Remove(this);
                _FromCP = value;
                _FromCP.Connectors.Add(this);
                RefreshPosition();
            }
        }

        public CConnectionPoint ToCP
        {
            get { return _ToCP; }
            set
            {
                if (_ToCP != null)
                    _ToCP.Connectors.Remove(this);
                _ToCP = value;
                _ToCP.Connectors.Add(this);
                RefreshPosition();
            }
        }

        public bool Moving
        {
            get { return _Moving; }
            set { _Moving = value; }
        }

        public bool MoveFrom
        {
            get { return _MoveFrom; }
            set { _MoveFrom = value; }
        }

        //public override BaseObject Copy()
        //{
        //    PetriLine newE = new PetriLine(drawingPanel, _FromCP, _ToCP, _FromCP.getX() + _FromCP.Width / 2, _FromCP.getY() + _FromCP.Width / 2, _ToCP.getX() + _FromCP.Width / 2, _ToCP.getY() + _FromCP.Width / 2);
        //    newE.penColor = this.penColor;
        //    newE.penWidth = this.penWidth;
        //    newE.fillColor = this.fillColor;
        //    newE.filled = this.filled;
        //    newE.dashstyle = this.dashstyle;
        //    newE.bIsLine = this.bIsLine;
        //    newE.alpha = this.alpha;
        //    //
        //    newE.starCap = this.starCap;
        //    newE.endCap = this.endCap;

        //    return newE;
        //}

        public override void Delete()
        {
            if (mult > 1) DecMult();
            else
            {
                _FromCP.Connectors.Remove(this);
                _ToCP.Connectors.Remove(this);
                DecMult();
                base.Delete();
            }
        }

        public void RefreshPosition()
        {
            X = (int)((_FromCP.getX1()*propXl+_FromCP.getX()*propXm)/(propXm+propXl) );
            Y = (int)((_FromCP.getY1() * propYl + _FromCP.getY()*propYm) / (propYl + propYm) );
            X1 = (int)((_ToCP.getX1() * propX1l + _ToCP.getX()*propX1m) / (propX1m + propX1l));
            Y1 = (int)((_ToCP.getY1() * propY1l + _ToCP.getY()*propY1m) / (propY1m + propY1l));

            changeprop();
            endMoveRedim();
        }
        public override void redim(int x, int y, InteractionType Type)
        {
            base.redim(x, y, Type);
            changeprop();
        }
        public override void endMoveRedim()
        {
            base.endMoveRedim();
            changeprop();
        }
        public override void CopyFrom(BaseObject ele)
        {
            this.copyStdProp(ele, this);
            this.FromCP = ((PetriLine)ele).FromCP;
            this.ToCP = ((PetriLine)ele).ToCP;
            this.endCap = ((PetriLine)ele).endCap;
            this.starCap = ((PetriLine)ele).starCap;
        }

        public override void Select()
        {
            this.undoEle = this.Copy();
        }


        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddLine((this.getX() + dx) * zoom, (this.getY() + dy) * zoom, (this.getX1() + dx) * zoom, (this.getY1() + dy) * zoom);
        }
        public void MultChange()
        {
            if (mult >1)
                Name = "[" + mult.ToString() + "]";
            else Name = "";
            
        }
        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom) * 4);
            myPen.DashStyle = this.dashStyle;
            myPen.StartCap = this.starCap;
            myPen.EndCap = this.endCap;
            myPen.Color = this.Trasparency(this.penColor, this.alpha);
            if (this.bSelected)
            {
                myPen.Color = Color.Red;
                myPen.Color = this.Trasparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
                g.DrawEllipse(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, 3, 3);
            }
            myPen.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 + dx) * zoom, (this.Y1 + dy) * zoom);
            myPen.Dispose();
            if (Name != null)
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Near;

                Font font = new Font("Arial", 8 * zoom);
                SizeF size = g.MeasureString(Name, font);
                if ((X1 - X) * zoom > size.Width)
                    size.Width = (X1 - X) * zoom;
                g.DrawString(Name, font, new SolidBrush(Color.Black), new PointF(((this.X + this.X1) / 2 + dx) * zoom, ((this.Y + this.Y1) / 2 + dy) * zoom));
                stringFormat.Dispose();
            }
        }

       
    }
}
