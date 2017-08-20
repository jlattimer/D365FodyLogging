using System.Threading;

namespace D365FodyLogging
{
    class OtherStuff
    {
        [Time]
        public static void DoWork()
        {
            // Something else in a different class
            Thread.Sleep(5000);
        }
    }
}