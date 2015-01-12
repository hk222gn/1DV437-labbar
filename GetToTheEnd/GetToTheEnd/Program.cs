#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using GetToTheEnd.Controller;
#endregion

namespace GetToTheEnd
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new MainController())
                game.Run();
        }
    }
#endif
}
