using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Checkers.View;

namespace Checkers
{
    public class Program
    {
        public static void Main()
        {
            new CheckersGameManager().RunCheckersApp();
        }
    }
}
