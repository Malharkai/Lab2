using Lab2CL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml.Schema;

namespace Lab2Main
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>();
            List<float> areas = new List<float>();
            List<float> shape3Dv = new List<float>();
            float tCircums = 0;
            float totalArea = 0;

            for (int i = 0; i < 20; i++)
            {
                shapes.Add(Shape.GenerateShape());   
            }

            foreach (Shape aShape in shapes)
            {
                if (aShape is Triangle)
                {
                    Triangle t = (Triangle)aShape;
                    tCircums =+ t.Circumference;
                }
                if (aShape is Shape3D)
                {
                    Shape3D aShape3D = (Shape3D)aShape;
                    shape3Dv.Add(aShape3D.Volume);
                }
                areas.Add(aShape.Area);
                totalArea =+ aShape.Area;
                Console.WriteLine(aShape);
                   
            }

            float avarageArea = totalArea / shapes.Count();
            
            Console.WriteLine($"The average area is: {avarageArea:0.00}");
            Console.WriteLine($"Sum of the circumference of all triangles: {tCircums:0.00}");
            Console.WriteLine($"Largest volume! : {shape3Dv.Max():0.00}");
        }
    }
}
