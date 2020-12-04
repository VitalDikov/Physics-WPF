using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Planets3
{
    class MyVector
    {
        public double X { get; set; }
        public double Y { get; set; }

        public MyVector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static MyVector operator +(MyVector v1, MyVector v2)
        {
            return new MyVector(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static MyVector operator -(MyVector v1, MyVector v2)
        {
            return new MyVector(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static double operator *(MyVector v1, MyVector v2)
        {
            return (v1.X - v2.X)*(v1.X - v2.X) + (v1.Y - v2.Y) * (v1.Y - v2.Y);
        }
        public static MyVector operator *(double val, MyVector v)
        {
            return new MyVector(val*v.X, val*v.Y);
        }

        public double Abs()
        {
            return X * X + Y * Y;
        }
    }

    




   
}
