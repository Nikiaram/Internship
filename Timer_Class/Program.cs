using System;
using System.Threading;

namespace Timer_Class
{
    public static class Program
    {

        public static void Main()
        {
            //The problem is in ways of using compiler switches,
            //because according to the book there should be a 
            //difference between them, but actualy I could not find it

            #region 1st way without using any special compiler switches

            // The problem is that the method is not called just once!
            /*
               From the book
               Compile this code from the command prompt without using any special compiler switches. When
               you run the resulting executable file, you’ll see that the TimerCallback method is called just once!
            */

            //Managed heap
            //A C D F H
            //NextObjPtr
            //•
            //Roots:
            //  Fields & variables
            //www.it - ebooks.info
            // Create a Timer object that knows to call our TimerCallback
            // method once every 2000 milliseconds.
            //Timer t = new Timer(TimerCallback, null, 0, 2000);
            // Wait for the user to hit <Enter>
            //Console.ReadLine();

            #endregion

            #region 2nd way specify the C# compiler's debug 

            //No difference between 1st and 2nd way
            /*
             From the book
             Do see this, just recompile the program from a command prompt, but this time, specify the C#
             compiler’s /debug switch. When you run the resulting executable file, you’ll now see that the
             TimerCallback method is called repeatedly! Note, the C# compiler’s /optimize+ compiler switch
             turns optimizations back on so this compiler switch should not be specified when performing this
             experiment. 
             */
            // Create a Timer object that knows to call our TimerCallback
            // method once every 2000 milliseconds.
            //Timer t = new Timer(TimerCallback, null, 0, 2000);
            // Wait for the user to hit <Enter>
            Console.ReadLine();
            // Refer to t after ReadLine (this gets optimized away)
            //t = null;

            #endregion

            #region 3rd way 

            // No difference between 1st, 2nd and 3rd way of writing the code

            /* From the book
              if you compile this (without the /debug+ switch) and run the resulting executable file,
              you’ll see that the TimerCallback method is still called just once.
             */
            Timer t = new Timer(TimerCallback, null, 0, 2000);
            // Wait for the user to hit <Enter>
            Console.ReadLine();
            // Refer to t after ReadLine (t will survive GCs until Dispose returns)
            t.Dispose();

            #endregion
        }

        private static void TimerCallback(Object o)
        {
            // Display the date/time when this method got called.
            Console.WriteLine("In TimerCallback: " + DateTime.Now);
            // Force a garbage collection to occur for this demo.
            GC.Collect();
        }

        /* No problem
        //Finalizer
        internal sealed class SomeType
        {
            // This is the Finalize method
            ~SomeType()
            {
                // The code here is inside the Finalize method
            }
        }
        */
    }
    #region GC Notifications
    /* No problem
    public static class GCNotification
    {
        private static Action<Int32> s_gcDone = null; // The event's field

        public static event Action<Int32> GCDone
        {
            add
            {
                // If there were no registered delegates before, start reporting notifications now
                if (s_gcDone == null) { new GenObject(0); new GenObject(2); }
                s_gcDone += value;
            }
            remove { s_gcDone -= value; }
        }
        private sealed class GenObject
        {
            private Int32 m_generation;
            public GenObject(Int32 generation) { m_generation = generation; }
            ~GenObject()
            { // This is the Finalize method
              // If this object is in the generation we want (or higher),
              // notify the delegates that a GC just completed
                if (GC.GetGeneration(this) >= m_generation)
                {
                    Action<Int32> temp = Volatile.Read(ref s_gcDone);
                    if (temp != null) temp(m_generation);
                }
                // Keep reporting notifications if there is at least one delegated registered,
                // the AppDomain isn't unloading, and the process isn’t shutting down
                if ((s_gcDone != null)
                && !AppDomain.CurrentDomain.IsFinalizingForUnload()
                && !Environment.HasShutdownStarted)
                {
                    // For Gen 0, create a new object; for Gen 2, resurrect the object
                    // & let the GC call Finalize again the next time Gen 2 is GC'd
                    if (m_generation == 0) new GenObject(0);
                    else GC.ReRegisterForFinalize(this);
                }
                else { /* Let the objects go away */
}
//}
//}
//}
//}
#endregion
