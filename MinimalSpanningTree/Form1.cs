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
        Pen pBlack;
        Pen pGreen;
        Pen pRed;
        Pen pBlue;
        Pen pViolet;
        Pen pGrey;
        Brush bBlack;
        Brush bRed;
        Brush bBlue;
        Brush bGrey;


        Graph graph;
        int? selectedMove;
        int? selectedChange;
        List<Edge> algorithmEdges;
        List<int> algorithmNodes;
        bool drawAnimation;
        Bitmap b1;
        int nodesNumber;

        string TO_DELETE;
        bool drawGrid;
        static int gridCellWidth;
        public Form1()
        {
            InitializeComponent();

            algorithmEdges = new List<Edge>();
            algorithmNodes = new List<int>();

            b1 = new Bitmap(panel1.Width, panel1.Height);
            b1.SetResolution(97, 97);
            g = Graphics.FromImage(b1);
            pBlack = new Pen(Color.Black, 5);
            pGreen = new Pen(Color.LawnGreen, 2);
            pRed = new Pen(Color.Red, 2);
            pBlue = new Pen(Color.Blue, 2);
            pViolet = new Pen(Color.DarkViolet, 2);
            pGrey = new Pen(Color.LightGray, 2);
            bBlack = new SolidBrush(Color.Black);
            bRed = new SolidBrush(Color.Red);
            bBlue = new SolidBrush(Color.Blue);
            bGrey = new SolidBrush(Color.LightGray);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode = InterpolationMode.High;

            gridCellWidth = panel1.Width / 10;


        }
       


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var x = new Task(() =>
            {
                drawBitmap();
                e.Graphics.DrawImage(b1, 0, 0);
            });
            x.Start();
            Task.WaitAll(x);
        }
        public void drawBitmap()
        {

            g.Clear(Color.White);
            if (drawGrid)
            {
                for (int i = 1; i < 10; i++)
                {
                    g.DrawLine(pGrey, 0, i * gridCellWidth, panel1.Width, i * gridCellWidth);
                    g.DrawLine(pGrey, i * gridCellWidth, 0, i * gridCellWidth, panel1.Height);
                }

            }
            if (graph == null) return;

            int[,] co = graph.CoPoints;

            for (int i = 0; i < graph.N; i++)
            {

                for (int j = 0; j < i + 1; j++)
                {
                    if (graph.Nodes[i, j])
                    {
                        if (i == selectedMove || j == selectedMove) g.DrawLine(pRed, co[i, 0] + 5, co[i, 1] + 5, co[j, 0] + 5, co[j, 1] + 5);
                        else if (i == selectedChange || j == selectedChange) g.DrawLine(pBlue, co[i, 0] + 5, co[i, 1] + 5, co[j, 0] + 5, co[j, 1] + 5);
                        else g.DrawLine(pGreen, co[i, 0] + 5, co[i, 1] + 5, co[j, 0] + 5, co[j, 1] + 5);

                    }
                }
            }




            for (int i = 0; i < graph.N; i++)
            {
                if (i == selectedMove) g.FillEllipse(bRed, co[i, 0], co[i, 1], 10, 10);
                else if (i == selectedChange) g.FillEllipse(bBlue, co[i, 0], co[i, 1], 10, 10);
                else g.FillEllipse(bBlack, co[i, 0], co[i, 1], 10, 10);
            }



            if (selectedMove != null) return;
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               8,
               FontStyle.Regular,
               GraphicsUnit.Point);

   
            foreach(Edge e in graph.Edges)
            {
                g.DrawString(Convert.ToString(e.Cost), font, bBlue, (co[e.NodeOne, 0] + co[e.NodeTwo, 0]) / 2 - font.Height / 2,
                            (co[e.NodeOne, 1] + co[e.NodeTwo, 1]) / 2 - font.Height / 2);
            }

            for (int i = 0; i < graph.N; i++)
            {
                g.DrawString(Convert.ToString(i), font, bRed, co[i, 0], co[i, 1] - 15);
            }

            foreach (Edge w in algorithmEdges)
            {
                g.DrawLine(pViolet, co[w.NodeOne, 0] + 5, co[w.NodeOne, 1] + 5, co[w.NodeTwo, 0] + 5, co[w.NodeTwo, 1] + 5);
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            int prawdopodobienstwo = trackBar1.Value;
            try
            {
                nodesNumber = Convert.ToInt32(textBox1.Text);
                if (nodesNumber < 1)
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

            TO_DELETE = "";
            graph = new Graph(nodesNumber, prawdopodobienstwo);
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
            graph.CoPoints = null;
            switch (i)
            {
                case 0:
                    graph.RandomCoordinate(panel1.Width, panel1.Height);
                    break;

                case 1:
                    graph.CircleCoordinate(panel1.Width, panel1.Height);
                    break;
                case 2:
                    if (graph.N > 80)
                    {
                        graph = null;
                        MessageBox.Show("Enter node size less that 80");
                        return;
                    }
                    else graph.GridCoordinate(panel1.Width);
                    break;

            }

            selectedChange = null;
            algorithmEdges.Clear();
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

                if (Math.Abs(mx - graph.CoPoints[i, 0]) < 10 && Math.Abs(my - graph.CoPoints[i, 1]) < 10)
                {
                    selectedMove = i;
                    break;
                }
            }

            if (selectedMove == null) return;


            graph.CoPoints[Convert.ToInt32(selectedMove), 0] = mx;
            graph.CoPoints[Convert.ToInt32(selectedMove), 1] = my;



            panel1.Refresh();
            System.Threading.Thread.Sleep(95);

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            selectedMove = null;
            panel1.Invalidate();
            panel1.Update();

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            algorithmEdges.Clear();
            algorithmNodes.Clear();

            if (e.Button == MouseButtons.Middle)
            {
                TO_DELETE = "";
                selectedChange = null;
                if (graph == null)
                {
                    graph = new Graph(1, 0);
                    graph.CoPoints = new int[1, 2];
                    graph.CoPoints[0, 0] = e.X;
                    graph.CoPoints[0, 1] = e.Y;
                }
                else
                {
                    bool usun = false;

                    for (int i = 0; i < graph.N; i++)
                    {
                        if (Math.Abs(e.X - graph.CoPoints[i, 0]) < 10 && Math.Abs(e.Y - graph.CoPoints[i, 1]) < 10)
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
                                temp[j, 0] = graph.CoPoints[j + k, 0];
                                temp[j, 1] = graph.CoPoints[j + k, 1];

                            }
                            graph.CoPoints = temp;

                            break;
                        }
                    }
                    if (!usun)
                    {
                        graph.AddNode();
                        int[,] temp = new int[graph.N, 2];
                        for (int i = 0; i < graph.N - 1; i++)
                        {
                            temp[i, 0] = graph.CoPoints[i, 0];
                            temp[i, 1] = graph.CoPoints[i, 1];
                        }
                        temp[graph.N - 1, 0] = e.X;
                        temp[graph.N - 1, 1] = e.Y;
                        graph.CoPoints = temp;
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

                if (Math.Abs(mx - graph.CoPoints[i, 0]) < 10 && Math.Abs(my - graph.CoPoints[i, 1]) < 10)
                {
                    if (selectedChange != null)
                    {
                        TO_DELETE = "";
                        graph.Nodes[Convert.ToInt32(selectedChange), i] = !graph.Nodes[Convert.ToInt32(selectedChange), i];
                        graph.Nodes[i, Convert.ToInt32(selectedChange)] = !graph.Nodes[i, Convert.ToInt32(selectedChange)];
                        selectedChange = null;
                        panel1.Refresh();
                        graph.CalculatedDeg();
                        return;
                    }
                    else
                    {
                        selectedChange = i;
                        panel1.Refresh();
                        //grafik.wylicz_stopien();
                        //wypisz_macierz();
                        return;
                    }


                }

            }
            selectedChange = null;

        }


        public void checkConectivity(int ostatni)
        {
            //if (grafik == null) return;
            graph.Connected = false;
            if (algorithmNodes.Count == graph.N)
            {
                graph.Connected = true;
                return;
            }
            // for (int i = sprawdzone.; i < grafik.n; i++)
            // {

            for (int j = 1; j < graph.N; j++)
            {
                if (graph.Nodes[ostatni, j] && !algorithmNodes.Contains(j))
                {
                    Edge temp = new Edge();
                    temp.NodeOne = ostatni;
                    temp.NodeTwo = j;
                    algorithmEdges.Add(temp);
                    algorithmNodes.Add(j);

                    if (drawAnimation)
                    {
                        panel1.Refresh();
                        System.Threading.Thread.Sleep(500);
                    }

                    checkConectivity(j);
                    // return;
                    //  f.g.DrawLine(ppp, f.wsp_punktow[j, 0]),);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (graph == null) return;
            algorithmEdges.Clear();
            algorithmNodes.Clear();
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
            drawAnimation = true;
            algorithmNodes.Add(0);
            checkConectivity(0);

            if (graph.Connected)
            {
                MessageBox.Show("Graf jest spójny " + help);
            }
            else
            {
                MessageBox.Show("Graph is not connected ");
                
            }// spr.Clear();
            drawAnimation = false;
            algorithmNodes.Clear();

        }

       
        // ścieżka
        private void button5_Click(object sender, EventArgs e)
        {
            if (graph == null) return;
            graph.Connected = false;
            algorithmNodes.Add(0);
            checkConectivity(0);
            algorithmEdges.Clear();
            algorithmNodes.Clear();

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


            int aktualny = Convert.ToInt32(selectedChange);
            algorithmNodes.Add(aktualny);
            selectedChange = null;
            panel1.Refresh();
            List<int> cykl = new List<int>();
            //grafik.wierzcholki;


            Graph pomocniczy = cloneGraph(graph);

            List<int> mosty = new List<int>();
            //List<int> ciag = new List<int>();
            //cykl.Add(aktualny);
            int startowy = aktualny;
            int ostatni = 100000;
            while (true)
            {
                //MessageBox.Show(Convert.ToString(aktualny));
                mosty.Clear();
                //mosty = Trajan(pomocniczy, aktualny);
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
            drawAnimation = true;
            for (int i = 0; i < cykl.Count - 1; i++)
            {
                Edge temp = new Edge();
                temp.NodeOne = cykl[i];
                temp.NodeTwo = cykl[i + 1];
                algorithmEdges.Add(temp);

                panel1.Refresh();
                System.Threading.Thread.Sleep(500);

            }

            drawAnimation = false;

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
            TO_DELETE = aa;
            if (czy_euler) MessageBox.Show(aa);
            else
            {
                TO_DELETE = jednak_nie;
                MessageBox.Show(jednak_nie);
            }
            algorithmEdges.Clear();
            algorithmNodes.Clear();
        }




        private Graph cloneGraph(Graph g)
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
            selectedChange = null;
            panel1.Refresh();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            drawGrid ^= true;
            panel1.Refresh();
        }


    }
}
