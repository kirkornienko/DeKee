using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace PlainSample
{
    public static class Extensions
    {
        public static void R(Action action)
        {
            try
            {
                action();
            }
            catch(Exception ex)
            {
                WriteLine(ex.ToString());
            }
        }
    }
}
