using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aproximation
{
    internal class Converter
    {
        public void ChangeType(string x, string y, int degree)
        {
            string[] X = x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] Y = y.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            calculator.Set_XandY(X, Y, degree);
        }
    }
}
