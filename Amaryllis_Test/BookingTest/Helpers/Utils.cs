using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;

namespace BookingTest.Helpers
{
    public class Utils
    {
        public static void WaitForElement(Func<bool> condition, TimeSpan timeout, int cycletime = 500)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (stopwatch.Elapsed < timeout)
            {

                    if (condition())
                    {
                        return;
                    }
    
                Thread.Sleep(cycletime);
            }

            throw new TimeoutException();
        }
    }
}
