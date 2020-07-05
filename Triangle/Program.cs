using System;
using System.Collections.Generic;
using System.Linq;
namespace Triangle
{
    class Program
    {
        //store all methods to check a triangle and their names
        static Dictionary<Func<float[], bool>, string> triangleChecks = new Dictionary<Func<float[], bool>, string>
        {
            //Can easily classify by number of equal sides method unneccesary
            { Triangle => Triangle.Distinct().Count() == 1, "an Equilateral"},
            { Triangle => Triangle.Distinct().Count() == 2, "an Isocales"},
            { Triangle => Triangle.Distinct().Count() == 3, "a Scalene"},

            { IsRightAngle, "a Right Angle"}
        };
        static void Main(string[] args)
        {
            
            do
            {
                Console.WriteLine(Environment.NewLine+"Enter three values seperated by a space");
                string input = Console.ReadLine();
                if (input == null)
                {
                    break;
                }
                string[] splitInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (splitInput.Length != 3)
                {
                    Console.WriteLine("Three values expected");
                    continue;
                }
                /* using an array rather then a struct as there is only one triangle to think about.
                * converting to floats any failure in parsing automatically gives it 0 an invalid length
                */
                float i;
                float[] floats = Array.ConvertAll(splitInput, s => float.TryParse(s, out i) ? i : 0);

                //To be a valid length it must be greater then 0
                if (floats.Any(x => x <= 0))
                {
                    Console.WriteLine("invalid length(s) entered");
                    continue;
                }
                Console.WriteLine();
                //Execute all property checks and print out true ones
                foreach(var x in triangleChecks)
                {
                    if (x.Key.Invoke(floats))
                    {
                        Console.WriteLine("The triangle is {0}",x.Value);
                    }
                }
            }
            while (true);
        }
        /// <summary>
        /// two sides square sum to another side squared
        /// </summary>
        /// <param name="triangle">a float array of size 3 </param>
        /// <returns>true if the tirangle is a right angle</returns>
        public static bool IsRightAngle(float[] triangle)
        {
           for(int i =0; i< triangle.Length;i++)
            {
                if(Math.Pow(triangle[i],2) == Math.Pow(triangle[(i+1)%triangle.Length],2) + Math.Pow(triangle[(i + 2) % triangle.Length],2))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
