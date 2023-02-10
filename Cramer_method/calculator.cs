using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aproximation
{
    internal class calculator
    {
        private static double[,] _DataXandY;
        public static double[,] _Calculation_Table;
        private static int _DegreeOfFunc;
        private static double[] _NumbersForFunction;
        private static double[,] _TestsMatrix;//Проврека на уменьшенение вычислительной работы
        private static int CountX;
        private static double[] _Function_numbers;
        //Test
        private static double[,] _TestMatrixForX;
        private static double[,] _TestMatrixForY;
        //

        private static int ForX;
        private static int ForY;

        private static double _equal;
        private static int _Index;//передвижение по массиву с расчетными числами для функции(_NumbersOfFunction)
        private static int _ForX;//какой столбец нужно заменить на y,xy,xy^2....
        //Создание матрицы для решения МНК
        internal static void Set_XandY(string[] X, string[] Y, int Degree)
        {
            int forX = 0;
            int forY = 1;
            int forX_1 = 0;
            int forY_1 = 0;
            double sumX = 0;
            double sumY = 0;
            _NumbersForFunction = new double[Degree+1];
            _Function_numbers = new double[Degree];
            _DegreeOfFunc = Degree;
            CountX = X.Length;

            _DataXandY = new double[2, X.Length];
            if (_DegreeOfFunc > 2)
            {
                int i = 2;
                int SumToDegree = 0;
                while (i != _DegreeOfFunc)
                {
                    SumToDegree += 3;
                    i++;
                }
                _TestsMatrix = new double[4 + SumToDegree, 1];

                //Test
                int l = 2;
                int n = 0;
                while (l != _DegreeOfFunc)
                {
                    n++;
                    l++;
                }
                _TestMatrixForX = new double[_DegreeOfFunc + n, 1];
                _TestMatrixForY = new double[_DegreeOfFunc, 1];
                //
            }

            else
            {
                _TestsMatrix = new double[4, 1];
                _TestMatrixForX = new double[_DegreeOfFunc, 1];
                _TestMatrixForY = new double[_DegreeOfFunc, 1];
            }

            int numberX = 0;
            int DegreeX = 1;
            int indexForX = 0;

            while (numberX != _TestMatrixForX.GetLength(0))
            {
                sumX = 0;
                foreach (var x in X)
                {
                    sumX += Math.Pow(double.Parse(x), DegreeX);
                }
                _TestMatrixForX[indexForX, 0] = sumX;
                indexForX++;
                DegreeX++;
                numberX++;
            }
            int numberY = 0;
            int DegreeY = 0;
            int indexForY = 0;

            while (numberY != Degree)
            {
                sumY = 0;
                if (numberY == 0)
                {
                    foreach (var y in Y)
                    {
                        sumY += double.Parse(y);
                    }
                }

                else
                {
                    for (int i = 0; i < Y.Length; i++)
                    {
                        sumY += Math.Pow(double.Parse(X[i]), DegreeY) * double.Parse(Y[i]);
                    }
                }
                _TestMatrixForY[indexForY, 0] = sumY;
                indexForY++;
                numberY++;
                DegreeY++;
            }
            SLAE_Solution ();
        }
        
        //Сделать матрицу для икса и игрика отдельно. Должно упрстить поиск и выбор данных для постановки задачи.
        private static void SLAE_Solution()
        {
            int Count = 0;
            double[,] TableForSolution;
            if (_DegreeOfFunc == 2)
            {
                TableForSolution = new double[2, 2];
            }

            else
            {
                int k = 1;
                while (k != _DegreeOfFunc - 1)
                {
                    k++;
                }
                TableForSolution = new double[_DegreeOfFunc, _DegreeOfFunc + k];
            }

            while (Count != _DegreeOfFunc + 1)//Добовлял +1(Вернуть если не счиатет)
            {
                if (Count == 0)
                {
                    ForX = 2;
                    ForY = 0;
                    for (int i = 0; i < TableForSolution.GetLength(0); i++)
                    {
                        if (i != 0)
                        {
                            ForX--;
                            ForY++;
                        }
                        int CoordinateForX = (_DegreeOfFunc - ForX);
                        int CoordinateForY = ForY;
                        int CountForMatrix = 0;
                        bool work = true;
                        int k = 0;

                        for (int j = 0; j < TableForSolution.GetLength(1); j++)
                        {
                            if (CountForMatrix != _DegreeOfFunc)
                            {
                                if (CountForMatrix == _DegreeOfFunc - 1 && i == 0)
                                {
                                    TableForSolution[i, j] = CountX;
                                }

                                else
                                {
                                    TableForSolution[i, j] = _TestMatrixForX[CoordinateForX, 0];
                                    CoordinateForX--;
                                }
                                CountForMatrix++;
                            }

                            //else if(work==true)
                            //{
                            //    TableForSolution[i, j] = _TestMatrixForY[CoordinateForY, 0];
                            //    work = false;
                            //}

                            else
                            {
                                TableForSolution[i, j] = TableForSolution[i, k];
                                k++;
                            }
                        }
                    }
                    Count++;
                }

                else
                {
                    //ForX = 2;
                    //ForY = 0;
                    if (_ForX == 0)
                    {
                        ForX = 3;
                        ForY = 0;
                    }
                    else
                    {
                        ForX = 2;
                        ForY = 0;
                    }

                    for (int i = 0; i < TableForSolution.GetLength(0); i++)
                    {
                        if (i != 0)
                        {
                            ForX--;
                            ForY++;
                        }
                        int CoordinateForX = (_DegreeOfFunc - ForX);
                        int CoordinateForY = ForY;
                        int CountForMatrix = 0;
                        bool work = true;
                        int k = 0;

                        for (int j = 0; j < TableForSolution.GetLength(1); j++)
                        {
                            //if (_ForX == j && work==true)
                            //{
                            //    TableForSolution[i, j] = _TestMatrixForY[CoordinateForY, 0];
                            //    work=false;
                            //    CountForMatrix++;

                            //}

                            if (CountForMatrix != _DegreeOfFunc)
                            {
                                if (_ForX == j)
                                {
                                    TableForSolution[i, j] = _TestMatrixForY[CoordinateForY, 0];
                                    if (_ForX == 1)
                                    {
                                        CoordinateForX--;
                                    }
                                }

                                else if (CountForMatrix == _DegreeOfFunc - 1 && i == 0)
                                {
                                    TableForSolution[i, j] = CountX;
                                }

                                else
                                {
                                    TableForSolution[i, j] = _TestMatrixForX[CoordinateForX, 0];
                                    CoordinateForX--;
                                }
                                CountForMatrix++;
                            }

                            else
                            {
                                TableForSolution[i, j] = TableForSolution[i, k];
                                k++;
                            }
                        }
                    }
                    _ForX++;
                    Count++;
                }

                //Расчетный модуль для нахождения коэфф. функции.
                int CountCalculator = 0;
                
                _equal = 0;
                int q = 0;
                int z = 0;
                int p;
                int t;
                bool wr = true;
                if (_DegreeOfFunc == 2)
                {
                    p = 0;
                    t = 1;
                }

                else
                {
                    p = 1;
                    t = 0;
                }

                while (CountCalculator != TableForSolution.GetLength(1) + p)
                {
                    if (q < _DegreeOfFunc)
                    {
                        double Multiplication = 1;
                        while (z != TableForSolution.GetLength(0))
                        {
                            Multiplication *= TableForSolution[z, q];
                            z++;
                            q++;
                        }
                        _equal += Multiplication;
                        t++;
                        z = 0;
                        q = 0;
                        q += t;
                        CountCalculator++;
                    }

                    if (wr != false && q == _DegreeOfFunc)
                    {
                        q--;
                        t--;
                        wr = false;
                    }

                    if (wr == false)
                    {
                        double Multiplication = 1;

                        while (z != TableForSolution.GetLength(0))
                        {
                            Multiplication *= TableForSolution[z, q];
                            z++;
                            q--;
                        }
                        _equal -= Multiplication;
                        t++;
                        z = 0;
                        q = 0;
                        q += t;
                        CountCalculator++;
                    }
                }
                _NumbersForFunction[_Index] = _equal;
                _Index++;
            }
            int mn = 0;
            int constant = 0;
            int index = 1;
            while (mn < _Function_numbers.Length)
            {
                _Function_numbers[mn] = _NumbersForFunction[index] / _NumbersForFunction[constant];
                index++;
                mn++;
            }

          }
    }
}
