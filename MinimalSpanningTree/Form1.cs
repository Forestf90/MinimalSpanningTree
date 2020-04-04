using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimalSpanningTree
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p;
        Pen p1;
        Pen p2;
        Pen p3;
        Pen p4;
        Pen p5;
        Brush b;
        Brush b2;
        Brush b3;


        public int[,] wsp_punktow;
        Graf grafik;
        int? zaznaczony;
        int? zaznaczony2;
        List<Krawedz> spr;
        List<int> spr_wierzcholki;
        bool rysuj_animacje;
        Bitmap b1;
        int ilosc_wierzcholkow;

        string cykl_w_stringu;
        bool siatka;
        static int range_panelu;
        public Form1()
        {
            InitializeComponent();

            spr = new List<Krawedz>();
            spr_wierzcholki = new List<int>();

            b1 = new Bitmap(panel1.Width, panel1.Height);
            b1.SetResolution(97, 97);
            g = Graphics.FromImage(b1);
            p = new Pen(Color.Black, 5);
            p1 = new Pen(Color.LawnGreen, 2);
            p2 = new Pen(Color.Red, 2);
            p3 = new Pen(Color.Blue, 2);
            p4 = new Pen(Color.DarkViolet, 2);
            p5 = new Pen(Color.LightGray, 2);
            b = new SolidBrush(Color.Black);
            b2 = new SolidBrush(Color.Red);
            b3 = new SolidBrush(Color.Blue);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode = InterpolationMode.High;

            range_panelu = panel1.Width / 10;


        }
        public void wyznacz_wspolrzedne_los()
        {


            wsp_punktow = new int[grafik.n, 2];

            Random rnd = new Random();
            for (int i = 0; i < grafik.n; i++)
            {
                int x = rnd.Next(10, panel1.Width - 15);
                int y = rnd.Next(20, panel1.Height - 20);
                wsp_punktow[i, 0] = x;
                wsp_punktow[i, 1] = y;

            }

        }

        public void wyznacz_wspolrzedne_kolo()
        {


            wsp_punktow = new int[grafik.n, 2];


            int centrumx = panel1.Width / 2;
            int centrumy = panel1.Height / 2;

            int x = 0;
            int y = 0;
            int srednica = (panel1.Height / 3);
            int i = 0;
            for (double angle = 0.0f; angle <= (2.0f * Math.PI); angle += ((Math.PI * 2.0f) / grafik.n))
            {
                if (i == grafik.n) break;
                x = Convert.ToInt32(srednica * Math.Sin(angle)) + centrumx;
                y = Convert.ToInt32(srednica * Math.Cos(angle)) + centrumy;
                wsp_punktow[i, 0] = x;
                wsp_punktow[i, 1] = y;
                i++;

            }
        }

        public void wyznacz_wspolrzedne_siatka()
        {

            if (grafik.n > 80)
            {
                grafik = null;
                MessageBox.Show("Rysujac na siatce podaj liczbe wierzchołków mniejszą od 80");
                return;
            }
            wsp_punktow = new int[grafik.n, 2];

            Random rnd = new Random();
            for (int i = 0; i < grafik.n; i++)
            {
                int x = rnd.Next(1, 10);
                int y = rnd.Next(1, 10);
                bool bylo = false;
                x = x * (panel1.Width / 10) - 5;
                y = y * (panel1.Width / 10) - 5;
                for (int j = 0; j < i; j++)
                {

                    if (x == wsp_punktow[j, 0] && y == wsp_punktow[j, 1])
                    {
                        i = i - 1;
                        bylo = true;
                        break;
                    }

                }

                if (!bylo)
                {
                    wsp_punktow[i, 0] = x;
                    wsp_punktow[i, 1] = y;
                }

            }

        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //if (grafik == null) return;
            var x = new Task(() =>
            {
                rysuj_bitmape();
                e.Graphics.DrawImage(b1, 0, 0);
            });
            //rysuj_bitmape();
            //e.Graphics.DrawImage(b1, 0, 0);
            x.Start();
            Task.WaitAll(x);

        }
        public void rysuj_bitmape()
        {

            g.Clear(Color.White);
            if (siatka)
            {
                for (int i = 1; i < 10; i++)
                {
                    g.DrawLine(p5, 0, i * range_panelu, panel1.Width, i * range_panelu);
                    g.DrawLine(p5, i * range_panelu, 0, i * range_panelu, panel1.Height);
                }

            }
            if (grafik == null) return;
            for (int i = 0; i < grafik.n; i++)
            {

                for (int j = 0; j < i + 1; j++)
                {
                    if (grafik.wierzcholki[i, j])
                    {
                        if (i == zaznaczony || j == zaznaczony) g.DrawLine(p2, wsp_punktow[i, 0] + 5, wsp_punktow[i, 1] + 5, wsp_punktow[j, 0] + 5, wsp_punktow[j, 1] + 5);
                        else if (i == zaznaczony2 || j == zaznaczony2) g.DrawLine(p3, wsp_punktow[i, 0] + 5, wsp_punktow[i, 1] + 5, wsp_punktow[j, 0] + 5, wsp_punktow[j, 1] + 5);
                        else g.DrawLine(p1, wsp_punktow[i, 0] + 5, wsp_punktow[i, 1] + 5, wsp_punktow[j, 0] + 5, wsp_punktow[j, 1] + 5);
                    }
                }
            }




            for (int i = 0; i < grafik.n; i++)
            {
                if (i == zaznaczony) g.FillEllipse(b2, wsp_punktow[i, 0], wsp_punktow[i, 1], 10, 10);
                else if (i == zaznaczony2) g.FillEllipse(b3, wsp_punktow[i, 0], wsp_punktow[i, 1], 10, 10);
                else g.FillEllipse(b, wsp_punktow[i, 0], wsp_punktow[i, 1], 10, 10);
            }



            if (zaznaczony != null) return;
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               8,
               FontStyle.Regular,
               GraphicsUnit.Point);

            for (int i = 0; i < grafik.n; i++)
            {
                g.DrawString(Convert.ToString(i), font, b2, wsp_punktow[i, 0], wsp_punktow[i, 1] - 15);
            }

            foreach (Krawedz w in spr)
            {
                g.DrawLine(p4, wsp_punktow[w.wierzcholekA, 0] + 5, wsp_punktow[w.wierzcholekA, 1] + 5, wsp_punktow[w.wierzcholekB, 0] + 5, wsp_punktow[w.wierzcholekB, 1] + 5);
            }
            // zaznaczony = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int prawdopodobienstwo = trackBar1.Value;
            try
            {
                ilosc_wierzcholkow = Convert.ToInt32(textBox1.Text);
                if (ilosc_wierzcholkow < 1)
                {
                    MessageBox.Show("Enter a number greater than 0");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Invalid data entered");
                textBox1.Text = "";
                return;
            }
            //ilosc_wierzcholkow = Convert.ToInt32(textBox1.Text);

            cykl_w_stringu = "";
            grafik = new Graf(ilosc_wierzcholkow, prawdopodobienstwo);
            int i = 0;

            using (GenerateForm gen = new GenerateForm())
            {
                if (gen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    i = gen.k;
                }
                else if (gen.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    grafik = null;
                    return;
                }
            }
            wsp_punktow = null;
            switch (i)
            {
                case 0:
                    wyznacz_wspolrzedne_los();
                    break;

                case 1:
                    wyznacz_wspolrzedne_kolo();
                    break;
                case 2:
                    wyznacz_wspolrzedne_siatka();
                    break;

            }

            //wyznacz_wspolrzedne();
            zaznaczony2 = null;
            spr.Clear();
            panel1.Refresh();
            textBox1.Text = "";


        }

      

      

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = Convert.ToString(trackBar1.Value);
        }






        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (grafik == null) return;
            else if (e.Button != MouseButtons.Left) return;
            else if (!panel1.Bounds.Contains(e.X + panel1.Left, e.Y + panel1.Top)) return;
            //time.Start();

            int mx = e.X;
            int my = e.Y;

            for (int i = 0; i < grafik.n; i++)
            {

                if (Math.Abs(mx - wsp_punktow[i, 0]) < 10 && Math.Abs(my - wsp_punktow[i, 1]) < 10)
                {
                    zaznaczony = i;
                    break;
                }
            }

            if (zaznaczony == null) return;


            wsp_punktow[Convert.ToInt32(zaznaczony), 0] = mx;
            wsp_punktow[Convert.ToInt32(zaznaczony), 1] = my;



            panel1.Refresh();
            System.Threading.Thread.Sleep(95);

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            zaznaczony = null;
            panel1.Invalidate();
            panel1.Update();

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            spr.Clear();
            spr_wierzcholki.Clear();

            if (e.Button == MouseButtons.Middle)
            {
                cykl_w_stringu = "";
                zaznaczony2 = null;
                if (grafik == null)
                {
                    grafik = new Graf(1, 0);
                    wsp_punktow = new int[1, 2];
                    wsp_punktow[0, 0] = e.X;
                    wsp_punktow[0, 1] = e.Y;
                    grafik.prawdopodobienstwo = 0;
                }
                else
                {
                    bool usun = false;

                    for (int i = 0; i < grafik.n; i++)
                    {
                        if (Math.Abs(e.X - wsp_punktow[i, 0]) < 10 && Math.Abs(e.Y - wsp_punktow[i, 1]) < 10)
                        {
                            //MessageBox.Show(Convert.ToString(i));
                            if (grafik.n == 1)
                            {
                                grafik = null;
                                return;
                            }
                            usun = true;
                            grafik.odejmij_w(i);
                            int[,] temp = new int[grafik.n, 2];
                            int k = 0;
                            for (int j = 0; j < grafik.n; j++)
                            {
                                if (j == i) k = 1;
                                temp[j, 0] = wsp_punktow[j + k, 0];
                                temp[j, 1] = wsp_punktow[j + k, 1];

                            }
                            wsp_punktow = temp;

                            break;
                        }
                    }
                    if (!usun)
                    {
                        grafik.dodaj_w();
                        int[,] temp = new int[grafik.n, 2];
                        for (int i = 0; i < grafik.n - 1; i++)
                        {
                            temp[i, 0] = wsp_punktow[i, 0];
                            temp[i, 1] = wsp_punktow[i, 1];
                        }
                        temp[grafik.n - 1, 0] = e.X;
                        temp[grafik.n - 1, 1] = e.Y;
                        wsp_punktow = temp;
                    }

                }
                panel1.Refresh();
                return;
            }
            if (e.Button != MouseButtons.Right) return;
            if (grafik == null) return;
            int mx = e.X;
            int my = e.Y;

            for (int i = 0; i < grafik.n; i++)
            {

                if (Math.Abs(mx - wsp_punktow[i, 0]) < 10 && Math.Abs(my - wsp_punktow[i, 1]) < 10)
                {
                    if (zaznaczony2 != null)
                    {
                        cykl_w_stringu = "";
                        grafik.wierzcholki[Convert.ToInt32(zaznaczony2), i] = !grafik.wierzcholki[Convert.ToInt32(zaznaczony2), i];
                        grafik.wierzcholki[i, Convert.ToInt32(zaznaczony2)] = !grafik.wierzcholki[i, Convert.ToInt32(zaznaczony2)];
                        zaznaczony2 = null;
                        panel1.Refresh();
                        grafik.wylicz_stopien();
                        return;
                    }
                    else
                    {
                        zaznaczony2 = i;
                        panel1.Refresh();
                        //grafik.wylicz_stopien();
                        //wypisz_macierz();
                        return;
                    }


                }

            }
            zaznaczony2 = null;

        }


        public void sprawdz_spojnosc(int ostatni)
        {
            //if (grafik == null) return;
            grafik.spojny = false;
            if (spr_wierzcholki.Count == grafik.n)
            {
                grafik.spojny = true;
                return;
            }
            // for (int i = sprawdzone.; i < grafik.n; i++)
            // {

            for (int j = 1; j < grafik.n; j++)
            {
                if (grafik.wierzcholki[ostatni, j] && !spr_wierzcholki.Contains(j))
                {
                    Krawedz temp = new Krawedz();
                    temp.wierzcholekA = ostatni;
                    temp.wierzcholekB = j;
                    spr.Add(temp);
                    spr_wierzcholki.Add(j);

                    if (rysuj_animacje)
                    {
                        panel1.Refresh();
                        System.Threading.Thread.Sleep(500);
                    }

                    sprawdz_spojnosc(j);
                    // return;
                    //  f.g.DrawLine(ppp, f.wsp_punktow[j, 0]),);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (grafik == null) return;
            spr.Clear();
            spr_wierzcholki.Clear();
            panel1.Refresh();
            string help = "and Eulerian";
            for (int i = 0; i < grafik.n; i++)
            {
                if (grafik.deg[i] == 0)
                {
                    MessageBox.Show("Graph is not connected - W" + i + " is not connected");
                    return;
                }
                else if (grafik.deg[i] % 2 == 1)
                {
                    help = "but not Eulerian - click Repair";
                }
            }
            rysuj_animacje = true;
            spr_wierzcholki.Add(0);
            sprawdz_spojnosc(0);

            if (grafik.spojny)
            {
                MessageBox.Show("Graf jest spójny " + help);
            }
            else
            {
                MessageBox.Show("Graph is not connected ");
                
            }// spr.Clear();
            rysuj_animacje = false;
            spr_wierzcholki.Clear();

        }

        //naprawa grafu
        private void button2_Click(object sender, EventArgs e)
        {
            if (grafik == null) return;
            //spr_wierzcholki = null;
            zaznaczony2 = null;
            Random rnd = new Random();
            for (int i = 0; i < grafik.n; i++)
            {
                if (grafik.deg[i] == 0)
                {
                    int ile = rnd.Next(grafik.n) + 0;

                    for (int j = 0; j < ile; j++)
                    {
                        int gdzie = i;
                        while (i == gdzie) { gdzie = rnd.Next(grafik.n) + 0; }
                        grafik.wierzcholki[i, gdzie] = true;
                        grafik.wierzcholki[gdzie, i] = true;
                        grafik.wylicz_stopien();
                    }
                    // grafik.wylicz_stopien();
                }
            }

            if (grafik.n == 2)
            {
                panel1.Refresh();
                return;
            }
            while (true)
            {
                int? pierwszy = null;
                for (int i = 0; i < grafik.n; i++)
                {


                    if (grafik.deg[i] % 2 == 1)
                    {
                        if (pierwszy == null)
                        {
                            pierwszy = i;
                        }
                        else
                        {
                            if (grafik.wierzcholki[Convert.ToInt32(pierwszy), i] && (grafik.deg[i] == 1
                                || grafik.deg[Convert.ToInt32(pierwszy)] == 1)) continue;

                            if (Convert.ToInt32(pierwszy) == i) continue;
                            grafik.wierzcholki[Convert.ToInt32(pierwszy), i] ^= true;
                            grafik.wierzcholki[i, Convert.ToInt32(pierwszy)] ^= true;
                            grafik.wylicz_stopien();
                            pierwszy = null;
                        }

                    }

                }

                if (pierwszy == null) break;
                else
                {
                    int temp;

                    temp = rnd.Next(grafik.n) + 0;
                    if (grafik.wierzcholki[Convert.ToInt32(pierwszy), temp] && (grafik.deg[temp] == 1
                        || grafik.deg[Convert.ToInt32(pierwszy)] == 1)) continue;

                    if (Convert.ToInt32(pierwszy) == temp) continue;

                    grafik.wierzcholki[Convert.ToInt32(pierwszy), temp] ^= true;
                    grafik.wierzcholki[temp, Convert.ToInt32(pierwszy)] ^= true;
                    grafik.wylicz_stopien();

                }
            }

            grafik.wylicz_stopien();
            panel1.Refresh();

        }
        // ścieżka
        private void button5_Click(object sender, EventArgs e)
        {
            if (grafik == null) return;
            grafik.spojny = false;
            spr_wierzcholki.Add(0);
            sprawdz_spojnosc(0);
            spr.Clear();
            spr_wierzcholki.Clear();

            if (!grafik.spojny)
            {
                MessageBox.Show("Graph is not connected ");
                return;
            }

            int temp_sum = 0;
            for (int i = 0; i < grafik.n; i++)
            {
                temp_sum += grafik.deg[i];
            }


            int aktualny = Convert.ToInt32(zaznaczony2);
            spr_wierzcholki.Add(aktualny);
            zaznaczony2 = null;
            panel1.Refresh();
            List<int> cykl = new List<int>();
            //grafik.wierzcholki;


            Graf pomocniczy = klonuj_graf(grafik);

            List<int> mosty = new List<int>();
            //List<int> ciag = new List<int>();
            //cykl.Add(aktualny);
            int startowy = aktualny;
            int ostatni = 100000;
            while (true)
            {
                //MessageBox.Show(Convert.ToString(aktualny));
                mosty.Clear();
                mosty = Trajan(pomocniczy, aktualny);
                cykl.Add(aktualny);
                bool final = false;
                bool byl_most = false; // w  sumie to wlasnie nie bylo
                for (int i = 0; i < pomocniczy.n; i++)
                {
                    if (pomocniczy.wierzcholki[aktualny, i])
                    {
                        /*
                       // if (pomocniczy.deg[i] > 1)
                        if(!mosty.Contains(i))
                        {
                            if (pomocniczy.deg[i] == 2 && aktualny!=startowy)
                            {
                                if (pomocniczy.wierzcholki[startowy, i] == true && pomocniczy.deg[startowy]==1)
                                {
                                    //mosty.Add(i);
                                    //byl_most = true;
                                    ostatni = i;
                                    final = true;
                                    continue;
                                }
                            }
                            byl_most = true;
                            //cykl.Add(aktualny);
                            pomocniczy.wierzcholki[aktualny, i] = false;
                            pomocniczy.wierzcholki[i, aktualny] = false;
                            pomocniczy.wylicz_stopien();
                            aktualny = i;
                            break;
                        }
                        else
                        {
                            mosty.Add(i);
                        }
                        */
                        if (!mosty.Contains(i))
                        {
                            pomocniczy.wierzcholki[aktualny, i] = false;
                            pomocniczy.wierzcholki[i, aktualny] = false;
                            pomocniczy.wylicz_stopien();
                            aktualny = i;
                            byl_most = true;
                            break;
                        }
                    }
                }
                if (!byl_most)
                {
                    /*
                    if (!mosty.Any())
                    {
                        if (final)
                        {
                            cykl.Add(ostatni);
                            cykl.Add(startowy);
                            pomocniczy.wierzcholki[ostatni, aktualny] = false;
                            pomocniczy.wierzcholki[aktualny, ostatni] = false;
                            pomocniczy.wierzcholki[ostatni, startowy] = false;
                            pomocniczy.wierzcholki[startowy, ostatni] = false;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    
                    }
                    else
                    {
                        */
                    if (mosty.Any())
                    {
                        int temp = mosty.First();
                        //cykl.Add(temp);
                        pomocniczy.wierzcholki[aktualny, temp] = false;
                        pomocniczy.wierzcholki[temp, aktualny] = false;
                        pomocniczy.wylicz_stopien();
                        aktualny = temp;
                    }
                    else break;
                    // }

                }

            }
            rysuj_animacje = true;
            for (int i = 0; i < cykl.Count - 1; i++)
            {
                Krawedz temp = new Krawedz();
                temp.wierzcholekA = cykl[i];
                temp.wierzcholekB = cykl[i + 1];
                spr.Add(temp);

                panel1.Refresh();
                System.Threading.Thread.Sleep(500);

            }

            rysuj_animacje = false;

            string aa = "";

            foreach (int k in cykl)
            {
                // aa += Convert.ToString(k.wierzcholekA) + "-->" + Convert.ToString(k.wierzcholekB)+Environment.NewLine;
                if (aa == "") aa = Convert.ToString(k);
                else aa += "->" + k;
            }

            bool czy_euler = true;
            pomocniczy.wylicz_stopien();
            for (int i = 0; i < pomocniczy.n; i++)
            {
                if (pomocniczy.deg[i] > 0)
                {
                    czy_euler = false;
                    break;
                }
            }

            if (cykl[0] == cykl[cykl.Count - 1]) aa = "Cykl Eulera: " + aa;
            else aa = "Graf nie posiada cyklu Eulera lecz jest grafem pol-eulerowskim poniewaz posiada sciezke Eulera" +
                    System.Environment.NewLine + "Sciezka Eulera :" + aa;
            
            string jednak_nie = "";
            jednak_nie = "The graph does not have an euler cycle";
            cykl_w_stringu = aa;
            if (czy_euler) MessageBox.Show(aa);
            else
            {
                cykl_w_stringu = jednak_nie;
                MessageBox.Show(jednak_nie);
            }
            spr.Clear();
            spr_wierzcholki.Clear();
        }

        private void Trajan_rek(int start, Graf g, List<Trajan_node> odwiedzone)
        {
            for (int j = 0; j < g.n; j++)
            {
                if (g.wierzcholki[start, j])
                {
                    bool contains = false;
                    foreach (Trajan_node t in odwiedzone)
                    {
                        if (t.numer == j)
                        {
                            contains = true;
                            break;
                        }
                    }
                    if (!contains)
                    {
                        Trajan_node temp = new Trajan_node();
                        temp.rodzic = start;

                        temp.NumerDFS = odwiedzone.Count + 1;
                        temp.numer = j;
                        odwiedzone.Add(temp);
                        Trajan_rek(j, g, odwiedzone);
                    }


                }
            }

            List<int> krawedz_wtorna = new List<int>();
            var start_node = odwiedzone.First(x => x.numer == start);
            Trajan_node start_n = start_node;
            List<int> Low = new List<int>();
            for (int i = 0; i < g.n; i++)
            {
                if (g.wierzcholki[i, start])
                {
                    var temp = odwiedzone.First(x => x.numer == i);
                    Trajan_node xxx = temp;
                    if (xxx.rodzic == start)
                    {
                        Low.Add(xxx.Low);
                    }
                    else if (start_n.rodzic == xxx.numer)
                    {
                        continue;
                    }
                    else { Low.Add(xxx.NumerDFS); }
                }
            }
            Low.Add(start_n.NumerDFS);
            start_n.Low = Low.Min();
        }

        private List<int> Trajan(Graf g, int start)
        {
            List<int> mosty = new List<int>();
            List<Trajan_node> odwiedzone = new List<Trajan_node>();

            Trajan_node temp = new Trajan_node();
            temp.numer = start;
            temp.NumerDFS = 1;
            odwiedzone.Add(temp);
            Trajan_rek(start, g, odwiedzone);
            string wiadomosc = "";
            foreach (Trajan_node t in odwiedzone)
            {
                if (g.wierzcholki[t.numer, start])
                {
                    if (t.NumerDFS == t.Low)
                    {
                        mosty.Add(t.numer);
                        wiadomosc += t.numer + System.Environment.NewLine;

                    }
                    else if (g.deg[t.numer] == 1) mosty.Add(t.numer);
                }

            }
            return mosty;
        }
        private Graf klonuj_graf(Graf g)
        {
            Graf nowy = new Graf(0, 0);

            nowy.n = g.n;
            nowy.wierzcholki = new bool[g.n, g.n];
            nowy.deg = new int[nowy.n];
            for (int i = 0; i < g.n; i++)
            {
                for (int k = 0; k < g.n; k++)
                {
                    nowy.wierzcholki[i, k] = grafik.wierzcholki[i, k];
                    nowy.wierzcholki[k, i] = grafik.wierzcholki[k, i];
                }
                nowy.deg[i] = g.deg[i];
            }

            return nowy;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            grafik = null;
            zaznaczony2 = null;
            panel1.Refresh();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            siatka ^= true;
            panel1.Refresh();
        }


    }
}
