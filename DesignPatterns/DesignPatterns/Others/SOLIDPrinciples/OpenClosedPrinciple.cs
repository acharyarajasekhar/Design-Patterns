using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.SOLIDPrinciples
{
    interface IShape_WithoutOCP { }

    class RectangleShape_WithoutOCP : IShape_WithoutOCP
    {
        public double Height { get; set; }
        public double Width { get; set; }
    }

    class Circle_WithoutOCP : IShape_WithoutOCP
    {
        public double Radius { get; set; }
    }

    class AreaCalculator_WithoutOCP
    {
        public double TotalArea(IShape_WithoutOCP[] arrShapes)
        {
            double area = 0;
            foreach (var objShape in arrShapes)
            {
                if (objShape is RectangleShape_WithoutOCP)
                {
                    var obj = objShape as RectangleShape_WithoutOCP;
                    area += obj.Height * obj.Width;
                }
                else if (objShape is Circle_WithoutOCP)
                {
                    var obj = objShape as Circle_WithoutOCP;
                    area += obj.Radius * obj.Radius * Math.PI;
                }
            }
            return area;
        }
    }


    interface IShape
    {
        double Area();
    }

    class RectangleShape : IShape
    {
        public double Height { get; set; }
        public double Width { get; set; }

        public double Area()
        {
            return Height * Width;
        }
    }

    class Circle : IShape
    {
        public double Radius { get; set; }

        public double Area()
        {
            return Radius * Radius * Math.PI;
        }
    }

    class AreaCalculator
    {
        public double TotalArea(IShape[] arrShapes)
        {
            double area = 0;
            foreach (var objShape in arrShapes)
            {
                area += objShape.Area();
            }
            return area;
        }
    }
}
