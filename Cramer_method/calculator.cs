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
            _NumbersForFunction = new double[X.Length];
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
                _Calculation_Table = new double[4 + SumToDegree, X.Length + 1];
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
                _Calculation_Table = new double[4, X.Length + 1];
                _TestsMatrix = new double[4, 1];
                _TestMatrixForX = new double[_DegreeOfFunc, 1];
                _TestMatrixForY = new double[_DegreeOfFunc, 1];
            }

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
                _Calculation_Table[forY, forY_1] = double.Parse(y);
                forY_1++;
                sumY += double.Parse(y);

            }
            _Calculation_Table[forX, forX_1] = sumX;
            _Calculation_Table[forY, forY_1] = sumY;
            _TestsMatrix[forX, 0] = sumX;
            _TestsMatrix[forY, 0] = sumY;
            //Test
            _TestMatrixForX[0, 0] = sumX;
            _TestMatrixForY[0, 0] = sumY;
            //
            Count();
        }
        //Заполнение матрицы для дальнейшего  решения СЛАУ
        private static void Count()
        {
            int height = _Calculation_Table.GetLength(0);
            int width = _Calculation_Table.GetLength(1);
            int Degree_of_Number = 1;
            double sum = 0;
            bool work = true;
            int x = 0;
            int y = 0;
            //Test
            int CountX = 1;
            int CountY = 1;
            //

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
                        _TestsMatrix[i, 0] = sum;
                        //Test
                        if (work != false || Degree_of_Number == 0)
                        {
                            _TestMatrixForX[CountX, 0] = sum;
                            CountX++;
                        }

                        else if (work == false)
                        {
                            _TestMatrixForY[CountY, 0] = sum;
                            CountY++;
                        }
                        //
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
            SLAE_Solution();
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
                                    if (_ForX == 1)//Костыль
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
                int p = 0;
                _equal = 0;
                int q = 0;
                int t = 0;
                int z = 0;
                bool wr = true;
                while (CountCalculator != TableForSolution.GetLength(1) + 1)
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