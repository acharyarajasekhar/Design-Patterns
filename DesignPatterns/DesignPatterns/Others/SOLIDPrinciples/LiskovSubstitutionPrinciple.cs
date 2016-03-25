using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.SOLIDPrinciples
{
    public class Rectangle
    {
        protected int iWidth;
        protected int iHeight;
        public int Width
        {
            get { return iWidth; }
        }
        public int Height
        {
            get { return iHeight; }
        }

        public virtual void SetWidth(int width)
        {
            iWidth = width;
        }
        public virtual void SetHeight(int height)
        {
            iHeight = height;
        }
        public int getArea()
        {
            return iWidth * iHeight;
        }
    }

    public class Square : Rectangle  // In an "is a" relationship, the derived class is clearly a
    //kind of the base class
    {
        public override void SetWidth(int width)
        {
            iWidth = width;
            iHeight = width;
        }

        public override void SetHeight(int height)
        {
            iHeight = height;
            iWidth = height;
        }
    }

    public class LiskovSubstitutionPrinciple
    {
        public void AreaOfRectangle()
        {
            Rectangle r = RectangleFactory(); // Returns the rectangle type object
            r.SetWidth(7);
            r.SetHeight(3);
            r.getArea();
        }

        private Rectangle RectangleFactory()
        {
            return new Square();
        }

        public Quadrilaterals QuadrilateralsFactory()
        {
            return new Square1();
        }
        public void AreaOfQuadrilateral()
        {
            Quadrilaterals r = QuadrilateralsFactory(); // Returns the Quadrilaterals type object
            r.Height = 7;
            r.Width = 3;
            r.getArea();
        } 

    }

    public class Quadrilaterals
    {
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }
        public int getArea()
        {
            return Height * Width;
        }
    }

    public class Rectangle1 : Quadrilaterals
    {

        public override int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }
        public override int Height
        {
            get { return base.Height; }
            set { base.Height = value; }
        }

    }

    public class Square1 : Quadrilaterals  // In an "is a" relationship, the derived class is clearly a
    //kind of the base class
    {
        public override int Height
        {
            get { return base.Height; }
            set { SetWidthAndHeight(value); }
        }

        public override int Width
        {
            get { return base.Width; }
            set { SetWidthAndHeight(value); }
        }

        private void SetWidthAndHeight(int value)
        {
            base.Height = value;
            base.Width = value;
        }
    } 
}
