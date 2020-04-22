using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    /// <summary>
    /// An exception for geometrical mistakes.
    /// </summary>
    class GeometricalException : ApplicationException
    {
        public GeometricalException(string txt) : base(txt) { }
    }
    /// <summary>
    /// Basic class for calculating square and checking other geometrical properties of the figures.
    /// </summary>
    public static class Figure
    {
        static public double GetSquare(params double[] sides)
        {
            if (!IsSidesCorrect(sides))
            {
                throw new GeometricalException("Радиусы или стороны должны быть положительными!");
            }
            switch (sides.Length)
            {
                case 1:
                    return GetSquareCircle(sides[0]);
                case 2:
                    return GetSquareEllipse(sides[0], sides[1]);
                case 3:
                    if(IsTriangle(sides[0], sides[1], sides[2]))
                    {
                        return GetSquareTriangle(sides[0], sides[1], sides[2]);
                    }
                    else { return 0; }       
                default:
                    throw new GeometricalException("Фигура неизвестна!");
            }
        }
        /// <summary>
        /// Checks if any element of an array is lesser then zero.
        /// </summary>
        /// <param name="s">An array of radiuses or sides.</param>
        /// <returns>True if all sides are positive numbers.</returns>
        private static bool IsSidesCorrect(double[] s)
        {
            for(int i=0; i<s.Length; i++)
            {
                if(s[i] <= 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Checks if a triangle can be constructed.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool IsTriangle(double a, double b, double c)
        {
            if (a + b <= c || a + c <= b || b + c <= a)
            {
                throw new GeometricalException($"Треугольника со сторонами {a}, {b}, {c} не существует");
            }
            return true;
        }
        /// <summary>
        /// Returns square of a circle.
        /// </summary>
        /// <param name="r">Radius of a circle</param>
        /// <returns></returns>
        private static double GetSquareCircle(double r)
        {
            return Math.PI * r * r;
        }
        /// <summary>
        /// Returns square of an ellipse. 
        /// </summary>
        /// <param name="a">Major axis</param>
        /// <param name="b">Minor axis</param>
        /// <returns></returns>
        private static double GetSquareEllipse(double a, double b)
        {
            return Math.PI * a * b;
        }
        /// <summary>
        /// Returns square of a triangle calculated by Heron's formula.
        /// </summary>
        /// <param name="a">Side 1</param>
        /// <param name="b">Side 2</param>
        /// <param name="c">Side 3</param>
        /// <returns></returns>
        private static double GetSquareTriangle(double a, double b, double c)
        {
            double p = (a + b + c) / 2.0;
            return Math.Sqrt(p*(p-a)*(p-b)*(p-c));
        }
        /// <summary>
        /// Checks if a triangle is rectangular by Pythagorean inverse theorem
        /// </summary>
        /// <param name="sides">Array of 3 elements</param>
        /// <returns>True if it's rectangular</returns>
        public static bool IsRectangular(params double[] sides)
        {
            if (sides.Length != 3)
            {
                throw new GeometricalException("Некорректное число сторон треугольника.");
            }
            if (!IsTriangle(sides[0], sides[1], sides[2]))
            {
                return false;
            }
            //Наибольшую сторона (гипотенуза) ставится в начало массива
            for (int i = 1; i < sides.Length; i++)
            {
                if(sides[i] > sides[0])
                {
                    double k = sides[0];
                    sides[0] = sides[i];
                    sides[i] = k;
                }
            }
            return sides[1] * sides[1] + sides[2] * sides[2] == sides[0] * sides[0];
        }
    }
}
