using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aproximation
{
    class Program
    {
        static void Main(string[] args)
        {
            Converter conv = new Converter();
            string x = "-0,8 -0,3 0,2 0,7 0,9 0,5";
            string y = "0,29 0,23 0,19 -0,21 0,5 0,19";
            int degree = 3;
            conv.ChangeType(x, y, degree);
        }
    }
}
