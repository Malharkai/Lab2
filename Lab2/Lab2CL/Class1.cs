using System;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Lab2CL
{
    public abstract class Shape 
    { 
        public abstract Vector3 Center { get; } 
        public abstract float Area { get; } 

        public static Shape GenerateShape()
        {
            Random random = new Random();
            int r = random.Next(1, 8);
            
            switch (r)
            {
                case 1:
                    return new Circle(new Vector2(random.Next(1,100),random.Next(1,100)), random.Next(1,100));                   
                case 2:
                    return new Rectangle(new Vector2((random.Next(1,100)), random.Next(1,100)), new Vector2((random.Next(1,100)), (random.Next(1,100))));
                case 3:
                    return new Rectangle(new Vector2(random.Next(1,100), random.Next(1,100)), random.Next(1,100));
                case 4:
                    return new Triangle(new Vector2(random.Next(1, 100), random.Next(1, 100)), new Vector2(random.Next(1, 100), random.Next(1, 100)), new Vector2(random.Next(1, 100), random.Next(1, 100)));
                case 5:
                    return new Cuboid(new Vector3(random.Next(1, 100), random.Next(1, 100), random.Next(1, 100)), new Vector3(random.Next(1, 100), random.Next(1, 100), random.Next(1, 100)));
                case 6:
                    return new Cuboid(new Vector3(random.Next(1, 100), random.Next(1, 100), random.Next(1, 100)), random.Next(1, 100));
                case 7:
                    return new Sphere(new Vector3(random.Next(1, 100), random.Next(1, 100), random.Next(1, 100)), random.Next(1, 100));
                default:
                    return null;
            }            
        }
        public static Shape GenerateShape(Vector3 center)
        {
            Random random = new Random();
            int r = random.Next(1, 8);

            switch (r)
            {
                case 1:
                    return new Circle(new Vector2(center.X, center.Y), random.Next(1, 100));
                case 2:
                    return new Rectangle(new Vector2(center.X, center.Y), new Vector2(random.Next(1, 100), random.Next(1, 100)));
                case 3:
                    return new Rectangle(new Vector2(center.X, center.Y), random.Next(1, 100));
                case 4:
                    Vector2 p1 = new Vector2(random.Next(1, 100), random.Next(1, 100));
                    Vector2 p2 = new Vector2(random.Next(1, 100), random.Next(1, 100));
                    return new Triangle(p1, p2, new Vector2(center.X * 3 - p1.X - p2.X , center.Y * 3 - p1.Y - p2.Y));
                case 5:
                    return new Cuboid(center, new Vector3(random.Next(1, 100), random.Next(1, 100), random.Next(1, 100)));
                case 6:
                    return new Cuboid(center, random.Next(1, 100));
                case 7:
                    return new Sphere(center, random.Next(1, 100));
                default:
                    return null;
            }


        }
    }

    public abstract class Shape2D : Shape
    {
        float x;
        float y;
        public abstract float Circumference 
        {
            get;
        }
    }

    public abstract class Shape3D : Shape
    {
        float x;
        float y;
        float z;
        public abstract float Volume
        {
            get;
        }
    }
    public class Circle : Shape2D
    {
        private Vector2 center;
        private float radius;

        public Circle (Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public override float Circumference => (float)(radius * 2 * Math.PI);

        public override Vector3 Center => new Vector3(center.X , center.Y, 0.0f);

        public override float Area => (float)(Math.Pow(radius, 2) * Math.PI);

        public override string ToString()
        {
            return $"Circle @({center.X:0.00}, {center.Y:0.00}): r = {radius:0.00}";
        }
    }
    public class Rectangle : Shape2D
    {
        private Vector2 center;
        private Vector2 size;
        private readonly bool _isSquare = false;

        public Rectangle(Vector2 center, Vector2 size)
        {
            this.center = center;
            this.size = size;
        }

        public Rectangle(Vector2 center, float width)
        {
            _isSquare = true;
            this.center = center;
            size = new Vector2(width);

        }

        public bool IsSquare
        {
            get => _isSquare;
            
        }
        public override float Circumference => size.X * 2 + size.Y * 2;

        public override Vector3 Center => new Vector3(center.X, center.Y, 0.0f);

        public override float Area => size.X * size.Y;
        
        public override string ToString()
        {
            if (IsSquare)
            {
                return $"Square @({center.X:0.00}, {center.Y:0.00}): w = {size.X:0.00}, h = {size.Y:0.00}";
            }
            else
                return $"Rectangle @({center.X:0.00}, {center.Y:0.00}): w = {size.X:0.00}, h = {size.Y:0.00}";
        }
    }
    public class Triangle : Shape2D
    {
        private Vector2 p1;
        private Vector2 p2;
        private Vector2 p3;
        private float a;
        private float b;
        private float c;
        
        
        private float s = 0;
        public Triangle(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;

            a = Vector2.Distance(p1, p2);
            b = Vector2.Distance(p2, p3);
            c = Vector2.Distance(p3, p1);
            s = (float)1 / 2 * (a + b + c);
        }

        

        public override float Circumference => (float)(a + b + c);
        public override Vector3 Center => new Vector3((p1.X + p2.X + p3.X) / 3f, (p1.Y + p2.Y + p2.Y) / 3f, 0f);

        public override float Area => (float)Math.Sqrt(s * (s - a) * (s - b) * (s - c));

        public override string ToString()
        {
            return $"Triangle @({Center.X:0.00}, {Center.Y:0.00}): p1{p1:0.00}, p2{p2:0.00}, p3{p3:0.00}";
        }
    }
    public class Sphere : Shape3D
    {
        Vector3 center;
        float radius;
        public Sphere(Vector3 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public override float Volume => (float)(4/3 * Math.PI * radius);

        public override Vector3 Center => center;

        public override float Area => (float)Math.Pow((4 * Math.PI * radius) , 2);

        public override string ToString()
        {            
            return $"Sphere @({center.X:0.00}, {center.Y:0.00}, {center.Z:0.00}): r = {radius:0.00}";
        }
    }
    public class Cuboid : Shape3D
    {
        private float width;
        private Vector3 center;        
        private Vector3 size;
        private readonly bool _isCube = false;

        private float h;
        private float w;
        private float l;

        public Cuboid(Vector3 center, Vector3 size)
        {
            
            this.center = center;
            this.size = size;

            h = size.X;
            w = size.Y;
            l = size.Z;
        }

        public Cuboid(Vector3 center, float width)
        {
            _isCube = true;
            this.center = center;
            this.width = width;
            size = new Vector3(width);
            h = size.X;
            w = size.Y;
            l = size.Z;
        }

        public bool IsCube
        {
            get => _isCube;
        }

        public override float Volume => h * w * l;

        public override Vector3 Center => center;

        public override float Area => 2*(l*w + l*h + w*h);

        public override string ToString()
        {
            if (IsCube)
            {
                return $"Cube @({center.X:0.00}, {center.Y:0.00}, {center.Z:0.00}): w = {width:0.00}, h = {width:0.00}, l = {width:0.00}";
            }
            else
                return $"Cuboid @({center.X:0.00}, {center.Y:0.00}, {center.Z:0.00}): w = {w:0.00}, h = {h:0.00}, l = {l:0.00}";
        }
    }
}
