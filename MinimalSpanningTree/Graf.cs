using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSpanningTree
{
    class Graf
    {
            public bool[,] wierzcholki;
            public int n;
            public int prawdopodobienstwo;
            public int[] deg;
            public bool spojny = false;
            public Graf(int na, int prawdo)
            {
                n = na;
                prawdopodobienstwo = prawdo;
                wierzcholki = new bool[n, n];
                polaczenia(prawdo);
            }

            public void polaczenia(int p)
            {
                Random rnd = new Random();

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        int a = rnd.Next(100);
                        if (a > 0 && a < p) wierzcholki[i, j] = true;
                        else wierzcholki[i, j] = false;
                    }

                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        wierzcholki[i, j] = wierzcholki[j, i];
                    }

                }

                wylicz_stopien();
            }


            public void wylicz_stopien()
            {
                deg = new int[n];

                for (int i = 0; i < n; i++)
                {
                    int suma = 0;

                    for (int j = 0; j < n; j++)
                    {
                        suma += Convert.ToInt32(wierzcholki[i, j]);
                    }
                    deg[i] = suma;
                }
            }

            public void dodaj_w()
            {
                n = n + 1;
                bool[,] temp = new bool[n, n];

                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1; j++)
                    {
                        temp[i, j] = wierzcholki[i, j];
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    temp[n - 1, i] = false;
                    temp[i, n - 1] = false;
                }
                wierzcholki = temp;
                wylicz_stopien();

            }

            public void odejmij_w(int ktory)
            {

                bool[,] temp = new bool[n - 1, n - 1];
                n = n - 1;
                int ii = 0, jj = 0;
                for (int i = 0; i < n; i++)
                {

                    if (ktory == i) ii = 1;
                    for (int j = 0; j < n; j++)
                    {

                        if (ktory == j) jj = 1;
                        temp[i, j] = wierzcholki[i + ii, j + jj];
                    }
                    jj = 0;
                }

                wierzcholki = temp;

                wylicz_stopien();
        }
    }
  
}
