
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DrawingPanel
{
    [Serializable]
    public class DynamicObject:BaseObject
    {
        private CConnectionPoint _CP;
        public int mult;
        public DynamicObject(DrawingPanel panel, CConnectionPoint cp)
            : base(panel)
        {
            this._CP = cp;
            _CP.DynamicObjects.Add(this);
            this.X = cp.getX() + _CP.Width / 2;//cp.Owner.getX()+(cp.Owner.getX1()-cp.Owner.getX()-cp.Width)/2;
            this.Y = cp.getY() + _CP.Width / 2;//cp.Owner.getY()+(cp.Owner.getY1() - cp.Owner.getY()-cp.Width) / 2 ;
            this.bIsLine = false;
            this.bSelected = true;
            this.bIsDynamicOb = true;
            mult = 1;
        }
        public virtual string ObjectType
        {
            get
            {
                return "DynamicObject";
            }
        }
        public virtual CConnectionPoint CP
        {
            get { return _CP; }
            set
            {
                if (_CP != null)
                    _CP.DynamicObjects.Remove(this);
                _CP = value;
                _CP.DynamicObjects.Add(this);
                RefreshPosition();
            }
        }
        public virtual void RefreshPosition()
        {
            X = _CP.getX() + _CP.Width / 2;
            Y =_CP.getY()+  _CP.Width / 2;
            endMoveRedim();
        }
        public virtual void IncMult()
        {
            mult++;
            MultChange();
        }
        public virtual void DecMult()
        {
            if (mult > 0)
                mult--;
            MultChange();
        }
        public virtual void MultChange()
        {
            if (mult > 1)
                Name = "[" + mult.ToString() + "]";
            else Name = "";

        }
        public virtual void Delete()
        {
            
            {
                _CP.DynamicObjects.Remove(this);
                
                base.Delete();
            }
        }
        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            
        }
        
    }
}
