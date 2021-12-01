using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace EventExample
{
    class Boy
    {
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Jump!");
        }
    }
}
