using System;
using System.Collections;

namespace Proj1
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            #region 1st part
            /*
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
            */
            #endregion

            Point p = new Point(1, 1);
            Console.WriteLine(p);

            p.Change(2, 2);
            Console.WriteLine(p);

            Object o = p;
            Console.WriteLine(o);

            ((Point)o).Change(3, 3);
            Console.WriteLine(o);

            /*
             Finally, I want to call the Change method to update the
             fields in the boxed Point object. However, Object (the type of the variable o) doesn’t know anything
             about the Change method, so I must first cast o to a Point. Casting o to a Point unboxes o and
             copies the fields in the boxed Point to a temporary Point on the thread’s stack! The m_x and m_y
             fields of this temporary point are changed to 3 and 3, but the boxed Point isn’t affected by this call to
             Change. When WriteLine is called the fourth time, (2, 2) is displayed again. Many developers do
             not expect this.
             Some languages, such as C++/CLI, let you change the fields in a boxed value type, but C# does not.
             However, you can fool C# into allowing this by using an interface. The following code is a modified
             version of the previous code:
            */

            // Interface defining a Change method

            // Boxes p, changes the boxed object and discards it

            ((IChangeBoxedPoint)p).Change(4, 4);
            Console.WriteLine(p);

            //Changes the boxed object and shows it

            ((IChangeBoxedPoint)o).Change(5, 5);
            Console.WriteLine(o);

            /*
             In the last example, the boxed Point referred to by o is cast to an IChangeBoxedPoint. No
             boxing is necessary here because o is already a boxed Point. Then Change is called, which does
             change the boxed Point’s m_x and m_y fields. The interface method Change has allowed me to
             change the fields in a boxed Point object! Now, when WriteLine is called, it displays (5, 5) as
             expected. The purpose of this whole example is to demonstrate how an interface method is able to
             modify the fields of a boxed value type. In C#, this isn’t possible without using an interface method. 
             */

        }
        //struct Point
        //{
        //    public Int32 x, y;
        //}

        internal interface IChangeBoxedPoint
        {
            void Change(Int32 x, Int32 y);
        }

        internal struct Point : IChangeBoxedPoint
        {
            private Int32 m_x, m_y;

            public Point(Int32 x, Int32 y)
            {
                m_x = x;
                m_y = y;
            }

            public void Change(Int32 x, Int32 y)
            {
                m_x = x;
                m_y = y;
            }

            public override string ToString()
            {
                return String.Format("({0},{1})", m_x.ToString(), m_y.ToString());
            }
        }
    }
}
