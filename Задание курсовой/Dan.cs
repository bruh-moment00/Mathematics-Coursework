using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задание_курсовой
{
    class Dan
    {
        static public double[,] A;
        static public double[] C;
        static public double[] X;
        static public int m;
        static public double h;
        static int my;
        static public double[] Y;
        static public double[] Y_Sorted;
        static public double[] ChartX;

        static public void Start_A(int rnd1, int rnd2)
        {
            A = new double[m, m];
            Random rnd = new Random();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                    A[i, j] = rnd.Next(rnd1, rnd2);
            }
        }

        static public void Start_C(int rnd1, int rnd2)
        {
            C = new double[m];
            Random rnd = new Random();
            for (int i = 0; i < m; i++)
            {
                C[i] = rnd.Next(rnd1, rnd2);
            }
        }

        static public void Start_X(double b)
        {
            X = new double[m];
            double P = 1;

            double B = b;

            double S = 0;
            for (int i = 0; i < m; i++)
            {
                S += A[i, i];
            }

            for (int i = 0; i < m; i++)
            {
                double temp_prod = 1;
                for (int k = 0; k < m; k++)
                    temp_prod *= 1 / (B * A[k, i] + Math.Pow(C[i], 2));
                X[i] = (S * C[i]) / P * temp_prod;
                P = X[i];
            }
        }

        static void Gauss(double[,] a, double[] b, double[] x, int n)
        {
            for (int i = 0; i < n; i++)
            {
                double max = Math.Abs(a[i, i]);
                int r = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(a[j, i]) > max)
                    {
                        max = Math.Abs(a[j, i]);
                        r = j;
                    }
                }
                double c;
                for (int j = 0; j < n; j++)
                {
                    c = a[i, j];
                    a[i, j] = a[r, j];
                    a[r, j] = c;
                }
                c = b[i];
                b[i] = b[r];
                b[r] = c;
                for (int l = i + 1; l < n; l++)
                {
                    double m = a[l, i] / a[i, i];
                    for (int j = i; j < n; j++)
                    {
                        a[l, j] = a[l, j] - m * a[i, j];
                    }
                    b[l] = b[l] - m * b[i];
                }
            }
            if (a[n - 1, n - 1] != 0)
            {
                for (int i = n - 1; i > -1; i--)
                {
                    double s = 0;
                    for (int j = i + 1; j < n; j++)
                    {
                        s = s + a[i, j] * x[j];
                    }
                    x[i] = (b[i] - s) / a[i, i];
                }
            }
            else
            {
                Console.WriteLine("Решений нет");
            }
        }


        static double[,] MethodObr(double[,] a, int n)
        {
            double[,] a1 = new double[n, n], y = new double[n, n];
            double[] x = new double[n], b = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        b[j] = 1;
                    }
                    else
                    {
                        b[j] = 0;
                    }
                }

                for (int l = 0; l < n; l++)
                {
                    for (int m = 0; m < n; m++)
                    {
                        a1[l, m] = a[l, m];
                    }
                }
                Gauss(a1, b, x, n);
                for (int j = 0; j < n; j++)
                {
                    y[j, i] = x[j];
                }
            }
            return y;
        }


        static double Kanon(int n, double z, double[] x, double[] y)
        {
            double m = 0;
            double[] A = new double[n];
            double[,] c = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= n - 1; j++)
                {
                    c[i, j] = Math.Pow(x[i], (n - 1) - j);
                }

            }
            double[,] c1 = MethodObr(c, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i] = c1[i, j] * y[j];
                    m += A[i] * Math.Pow(z, (n - 1) - i);
                }
            }
            return m;
        }

        static public void Start_Y()
        {
            my = Convert.ToInt32((m - 1) / h + 1);
            Y = new double[my];
            double[] indexMas = new double[X.Length];
            for (int i = 0; i < indexMas.Length; i++)
            {
                indexMas[i] = i + 1;
            }

            double T = indexMas[0], A = indexMas.Length;

            int k = 0;
            for (double i = T; i <= A; i += h)
            {
                double Res = Kanon(X.Length, i, indexMas, X);
                Y[k] = double.Parse(String.Format("{0:f5}", Res));
                k++;
            }


            ChartX = new double[Y.Length];
            double temp = 0;
            for(int i = 0; i < Y.Length; i++)
            {
                ChartX[i] = temp;
                temp += h;
            }
        }

        static void Swap(ref double x, ref double y)
        {
            var t = x;
            x = y;
            y = t;
        }

        static int Partition(double[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    Swap(ref array[pivot], ref array[i]);
                }
            }

            pivot++;
            Swap(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        public static double[] Quick_Sort(double[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            Quick_Sort(array, minIndex, pivotIndex - 1);
            Quick_Sort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        public static double[] Quick_Sort(double[] array)
        {
            return Quick_Sort(array, 0, array.Length - 1);
        }
    }
    
}
