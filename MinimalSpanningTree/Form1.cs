using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
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


        Graph graph;
        int? zaznaczony;
        int? zaznaczony2;
        List<Edge> spr;
        List<int> spr_wierzcholki;
        bool rysuj_animacje;
        Bitmap b1;
        int ilosc_wierzcholkow;

        string cykl_w_stringu;
        bool drawGrid;
        static int gridCellWidth;
        public Form1()
        {
            InitializeComponent();

            spr = new List<Edge>();
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

            gridCellWidth = panel1.Width / 10;


        }
       


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var x = new Task(() =>
            {
                rysuj_bitmape();
                e.Graphics.DrawImage(b1, 0, 0);
            });
            x.Start();
            Task.WaitAll(x);
        }
        public void rysuj_bitmape()
        {

            g.Clear(Color.White);
            if (drawGrid)
            {
                for (int i = 1; i < 10; i++)
                {
                    g.DrawLine(p5, 0, i * gridCellWidth, panel1.Width, i * gridCellWidth);
                    g.DrawLine(p5, i * gridCellWidth, 0, i * gridCellWidth, panel1.Height);
                }

            }
            if (graph == null) return;
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               8,
               FontStyle.Regular,
               GraphicsUnit.Point);

            int[,] co = graph.coPoints;

            for (int i = 0; i < graph.N; i++)
            {

                for (int j = 0; j < i + 1; j++)
                {
                    if (graph.Nodes[i, j])
                    {
                        if (i == zaznaczony || j == zaznaczony) g.DrawLine(p2, co[i, 0] + 5, co[i, 1] + 5, co[j, 0] + 5, co[j, 1] + 5);
                        else if (i == zaznaczony2 || j == zaznaczony2) g.DrawLine(p3, co[i, 0] + 5, co[i, 1] + 5, co[j, 0] + 5, co[j, 1] + 5);
                        else g.DrawLine(p1, co[i, 0] + 5, co[i, 1] + 5, co[j, 0] + 5, co[j, 1] + 5);

                        g.DrawString(Convert.ToString(i), font, b3,
                            (co[i, 0]+ co[j, 0])/2, (co[i, 1]+ co[j, 1])/2);

                    }
                }
            }




            for (int i = 0; i < graph.N; i++)
            {
                if (i == zaznaczony) g.FillEllipse(b2, co[i, 0], co[i, 1], 10, 10);
                else if (i == zaznaczony2) g.FillEllipse(b3, co[i, 0], co[i, 1], 10, 10);
                else g.FillEllipse(b, co[i, 0], co[i, 1], 10, 10);
            }



            if (zaznaczony != null) return;
            //FontFamily fontFamily = new FontFamily("Arial");
            //Font font = new Font(
            //   fontFamily,
            //   8,
            //   FontStyle.Regular,
            //   GraphicsUnit.Point);

            for (int i = 0; i < graph.N; i++)
            {
                g.DrawString(Convert.ToString(i), font, b2, co[i, 0], co[i, 1] - 15);
            }

            foreach (Edge w in spr)
            {
                g.DrawLine(p4, co[w.wierzcholekA, 0] + 5, co[w.wierzcholekA, 1] + 5, co[w.wierzcholekB, 0] + 5, co[w.wierzcholekB, 1] + 5);
            }
            // zaznaczony = null;
        }

        private void generateButton_Click(object sender, EventArgs e)
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
            graph = new Graph(ilosc_wierzcholkow, prawdopodobienstwo);
            int i = 0;

            using (GenerateForm gen = new GenerateForm())
            {
                if (gen.ShowDialog() == DialogResult.OK)
                {
                    i = gen.k;
                }
                else if (gen.DialogResult == DialogResult.Cancel)
                {
                    graph = null;
                    return;
                }
            }
            graph.coPoints = null;
            switch (i)
            {
                case 0:
                    graph.wyznacz_wspolrzedne_los(panel1.Width, panel1.Height);
                    break;

                case 1:
                    graph.wyznacz_wspolrzedne_kolo(panel1.Width, panel1.Height);
                    break;
                case 2:
                    if (graph.N > 80)
                    {
                        graph = null;
                        MessageBox.Show("Enter node size less that 80");
                        return;
                    }
                    else graph.wyznacz_wspolrzedne_siatka(panel1.Width);
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
            if (graph == null) return;
            else if (e.Button != MouseButtons.Left) return;
            else if (!panel1.Bounds.Contains(e.X + panel1.Left, e.Y + panel1.Top)) return;
            //time.Start();

            int mx = e.X;
            int my = e.Y;

            for (int i = 0; i < graph.N; i++)
            {

                if (Math.Abs(mx - graph.coPoints[i, 0]) < 10 && Math.Abs(my - graph.coPoints[i, 1]) < 10)
                {
                    zaznaczony = i;
                    break;
                }
            }

            if (zaznaczony == null) return;


            graph.coPoints[Convert.ToInt32(zaznaczony), 0] = mx;
            graph.coPoints[Convert.ToInt32(zaznaczony), 1] = my;



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
                if (graph == null)
                {
                    graph = new Graph(1, 0);
                    graph.coPoints = new int[1, 2];
                    graph.coPoints[0, 0] = e.X;
                    graph.coPoints[0, 1] = e.Y;
                }
                else
                {
                    bool usun = false;

                    for (int i = 0; i < graph.N; i++)
                    {
                        if (Math.Abs(e.X - graph.coPoints[i, 0]) < 10 && Math.Abs(e.Y - graph.coPoints[i, 1]) < 10)
                        {
                            //MessageBox.Show(Convert.ToString(i));
                            if (graph.N == 1)
                            {
                                graph = null;
                                return;
                            }
                            usun = true;
                            graph.RemoveNode(i);
                            int[,] temp = new int[graph.N, 2];
                            int k = 0;
                            for (int j = 0; j < graph.N; j++)
                            {
                                if (j == i) k = 1;
                                temp[j, 0] = graph.coPoints[j + k, 0];
                                temp[j, 1] = graph.coPoints[j + k, 1];

                            }
                            graph.coPoints = temp;

                            break;
                        }
                    }
                    if (!usun)
                    {
                        graph.AddNode();
                        int[,] temp = new int[graph.N, 2];
                        for (int i = 0; i < graph.N - 1; i++)
                        {
                            temp[i, 0] = graph.coPoints[i, 0];
                            temp[i, 1] = graph.coPoints[i, 1];
                        }
                        temp[graph.N - 1, 0] = e.X;
                        temp[graph.N - 1, 1] = e.Y;
                        graph.coPoints = temp;
                    }

                }
                panel1.Refresh();
                return;
            }
            if (e.Button != MouseButtons.Right) return;
            if (graph == null) return;
            int mx = e.X;
            int my = e.Y;

            for (int i = 0; i < graph.N; i++)
            {

                if (Math.Abs(mx - graph.coPoints[i, 0]) < 10 && Math.Abs(my - graph.coPoints[i, 1]) < 10)
                {
                    if (zaznaczony2 != null)
                    {
                        cykl_w_stringu = "";
                        graph.Nodes[Convert.ToInt32(zaznaczony2), i] = !graph.Nodes[Convert.ToInt32(zaznaczony2), i];
                        graph.Nodes[i, Convert.ToInt32(zaznaczony2)] = !graph.Nodes[i, Convert.ToInt32(zaznaczony2)];
                        zaznaczony2 = null;
                        panel1.Refresh();
                        graph.CalculatedDeg();
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
            graph.Connected = false;
            if (spr_wierzcholki.Count == graph.N)
            {
                graph.Connected = true;
                return;
            }
            // for (int i = sprawdzone.; i < grafik.n; i++)
            // {

            for (int j = 1; j < graph.N; j++)
            {
                if (graph.Nodes[ostatni, j] && !spr_wierzcholki.Contains(j))
                {
                    Edge temp = new Edge();
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
            if (graph == null) return;
            spr.Clear();
            spr_wierzcholki.Clear();
            panel1.Refresh();
            string help = "and Eulerian";
            for (int i = 0; i < graph.N; i++)
            {
                if (graph.Deg[i] == 0)
                {
                    MessageBox.Show("Graph is not connected - W" + i + " is not connected");
                    return;
                }
                else if (graph.Deg[i] % 2 == 1)
                {
                    help = "but not Eulerian - click Repair";
                }
            }
            rysuj_animacje = true;
            spr_wierzcholki.Add(0);
            sprawdz_spojnosc(0);

            if (graph.Connected)
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

       
        // ścieżka
        private void button5_Click(object sender, EventArgs e)
        {
            if (graph == null) return;
            graph.Connected = false;
            spr_wierzcholki.Add(0);
            sprawdz_spojnosc(0);
            spr.Clear();
            spr_wierzcholki.Clear();

            if (!graph.Connected)
            {
                MessageBox.Show("Graph is not connected ");
                return;
            }

            int temp_sum = 0;
            for (int i = 0; i < graph.N; i++)
            {
                temp_sum += graph.Deg[i];
            }


            int aktualny = Convert.ToInt32(zaznaczony2);
            spr_wierzcholki.Add(aktualny);
            zaznaczony2 = null;
            panel1.Refresh();
            List<int> cykl = new List<int>();
            //grafik.wierzcholki;


            Graph pomocniczy = klonuj_graf(graph);

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
                for (int i = 0; i < pomocniczy.N; i++)
                {
                    if (pomocniczy.Nodes[aktualny, i])
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
                            pomocniczy.Nodes[aktualny, i] = false;
                            pomocniczy.Nodes[i, aktualny] = false;
                            pomocniczy.CalculatedDeg();
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
                        pomocniczy.Nodes[aktualny, temp] = false;
                        pomocniczy.Nodes[temp, aktualny] = false;
                        pomocniczy.CalculatedDeg();
                        aktualny = temp;
                    }
                    else break;
                    // }

                }

            }
            rysuj_animacje = true;
            for (int i = 0; i < cykl.Count - 1; i++)
            {
                Edge temp = new Edge();
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
            pomocniczy.CalculatedDeg();
            for (int i = 0; i < pomocniczy.N; i++)
            {
                if (pomocniczy.Deg[i] > 0)
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

        private void Trajan_rek(int start, Graph g, List<Trajan_node> odwiedzone)
        {
            for (int j = 0; j < g.N; j++)
            {
                if (g.Nodes[start, j])
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
            for (int i = 0; i < g.N; i++)
            {
                if (g.Nodes[i, start])
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

        private List<int> Trajan(Graph g, int start)
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
                if (g.Nodes[t.numer, start])
                {
                    if (t.NumerDFS == t.Low)
                    {
                        mosty.Add(t.numer);
                        wiadomosc += t.numer + System.Environment.NewLine;

                    }
                    else if (g.Deg[t.numer] == 1) mosty.Add(t.numer);
                }

            }
            return mosty;
        }
        private Graph klonuj_graf(Graph g)
        {
            Graph nowy = new Graph(0, 0);

            nowy.N = g.N;
            nowy.Nodes = new bool[g.N, g.N];
            nowy.Deg = new int[nowy.N];
            for (int i = 0; i < g.N; i++)
            {
                for (int k = 0; k < g.N; k++)
                {
                    nowy.Nodes[i, k] = graph.Nodes[i, k];
                    nowy.Nodes[k, i] = graph.Nodes[k, i];
                }
                nowy.Deg[i] = g.Deg[i];
            }

            return nowy;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            graph = null;
            zaznaczony2 = null;
            panel1.Refresh();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            drawGrid ^= true;
            panel1.Refresh();
        }


    }
}
