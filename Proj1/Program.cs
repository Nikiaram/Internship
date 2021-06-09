using System;
using System.Collections;

namespace Proj1
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            //Boxing and Unboxing Value Types

            //Declare a value type.

            ArrayList a = new ArrayList();

            Point p; // Allocate a Point (not in the heap).

            for (int i = 0; i < 10; i++)
            {
                p.x = p.y = i;
                a.Add(p); // Box the value type and add the
                          // reference to the Arraylist.
            }

            Point p1 = (Point)a[0];

            Int32 v = 5; // Create an unboxed value type variable.
            Object o = v; // o refers to a boxed Int32 containing 5.
            v = 123; // Changes the unboxed value to 123
            Console.WriteLine(v + ", " + (Int32)o); // Displays "123, 5"
            Console.WriteLine(v + ", " + o);

        }
        struct Point
        {
            public Int32 x, y;
        }
    }
}
