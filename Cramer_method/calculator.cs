using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cramer_method
{
    internal class calculator
    {
        private static double[,] _DataXandY;
        public static double[,] _Calculation_Table;
        private static int _DegreeOfFunc;

        internal static void Set_XandY(string[] X, string[] Y, int Degree)
        {
            int forX = 0;
            int forY = 1;
            int forX_1 = 0;
            int forY_1 = 0;
            double sumX = 0;
            double sumY = 0;
            _DegreeOfFunc = Degree;

            _DataXandY = new double[2, X.Length];
            _Calculation_Table = new double[2 + (Degree + 2), X.Length + 1];

            foreach (var x in X)
            {
                _DataXandY[forX, forX_1] = double.Parse(x);
                _Calculation_Table[forX, forX_1] = double.Parse(x);
                forX_1++;
                sumX += double.Parse(x);

            }

            foreach (var y in Y)
            {
                _DataXandY[forY, forY_1] = double.Parse(y);
                _Calculation_Table[forY, forY_1] = double.Parse(y);//Convert.ToDouble
                forY_1++;
                sumY += double.Parse(y);

            }
            _Calculation_Table[forX, forX_1] = sumX;
            _Calculation_Table[forY, forY_1] = sumY;
            Count();
        }

        private static void Count()
        {
            int height = _Calculation_Table.GetLength(0);
            int width = _Calculation_Table.GetLength(1);
            int Degree_of_Number = 1;
            double sum = 0;
            bool work = true;
            int x = 0;
            int y = 0;

            for (int i = 2; i < height; i++)
            {
                if (i >= 2)
                {
                    Degree_of_Number++;
                }

                for (int j = 0; j < width; j++)
                {
                    if (j < _DataXandY.GetLength(1) & work)
                    {
                        _Calculation_Table[i, j] = Math.Pow(_DataXandY[x, j], Degree_of_Number);
                        sum += Math.Pow(_DataXandY[x, j], Degree_of_Number);


                        if (Degree_of_Number == _DegreeOfFunc + 1 & j + 1 == _DataXandY.GetLength(1))
                        {
                            work = false;
                            Degree_of_Number = 0;
                        }
                    }

                    else if (j == _DataXandY.GetLength(1))
                    {
                        _Calculation_Table[i, j] = sum;
                        sum = 0;

                        if (x == _DataXandY.GetLength(1))
                        {
                            x = 0;
                        }
                    }

                    else if (i >= _DegreeOfFunc + 1 & j != _DataXandY.GetLength(1))
                    {
                        _Calculation_Table[i, j] = (Math.Pow(_DataXandY[y, x], Degree_of_Number) * _DataXandY[y + 1, x]);
                        sum += (Math.Pow(_DataXandY[y, x], Degree_of_Number) * _DataXandY[y + 1, x]);
                        x++;
                    }
                }

            }
        }
    }
}
