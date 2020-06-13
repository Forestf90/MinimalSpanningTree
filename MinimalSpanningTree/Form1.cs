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
        Pen pGreen;
        Pen pRed;
        Pen pBlue;
        Pen pViolet;
        Pen pGrey;
        Brush bBlack;
        Brush bRed;
        Brush bBlue;
        Brush bViolet;


        Graph graph;
        int? selectedMove;
        int? selectedChange;
        List<Edge> algorithmEdges;
        List<int> algorithmNodes;
        bool drawAnimation;
        Bitmap b1;
        int nodesNumber;

        Edge editedEdge;

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
            pGreen = new Pen(Color.LawnGreen, 2);
            pRed = new Pen(Color.Red, 2);
            pBlue = new Pen(Color.Blue, 2);
            pViolet = new Pen(Color.DarkViolet, 2);
            pGrey = new Pen(Color.LightGray, 2);
            bBlack = new SolidBrush(Color.Black);
            bRed = new SolidBrush(Color.Red);
            bBlue = new SolidBrush(Color.Blue);
            bViolet = new SolidBrush(Color.DarkViolet);
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


            for (int i = 0; i < graph.N; i++)
            {
                g.DrawString(Convert.ToString(i), font, bRed, co[i, 0], co[i, 1] - 15);
            }

            foreach (Edge w in algorithmEdges)
            {
                g.DrawLine(pViolet, co[w.NodeOne, 0] + 5, co[w.NodeOne, 1] + 5, co[w.NodeTwo, 0] + 5, co[w.NodeTwo, 1] + 5);
            }

            foreach (Edge e in graph.Edges)
            {
                g.DrawString(Convert.ToString(e.Cost), font, bBlue, (co[e.NodeOne, 0] + co[e.NodeTwo, 0]) / 2 - font.Height / 2,
                            (co[e.NodeOne, 1] + co[e.NodeTwo, 1]) / 2 - font.Height / 2);
            }

            if(editedEdge!= null)
            {
                g.DrawLine(pViolet, co[editedEdge.NodeOne, 0] + 5, co[editedEdge.NodeOne, 1] + 5,
                    co[editedEdge.NodeTwo, 0] + 5, co[editedEdge.NodeTwo, 1] + 5);
                g.FillEllipse(bViolet, co[editedEdge.NodeOne, 0], co[editedEdge.NodeOne, 1], 10, 10);
                g.FillEllipse(bViolet, co[editedEdge.NodeTwo, 0], co[editedEdge.NodeTwo, 1], 10, 10);
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            int propability = trackBar1.Value;
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

            graph = new Graph(nodesNumber, propability);
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
                editedEdge = null;
                groupBox3.Visible = false;
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
                    bool delete = false;

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
                            delete = true;
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
                    if (!delete)
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
            editedEdge = null;
            groupBox3.Visible = false;

            for (int i = 0; i < graph.N; i++)
            {

                if (Math.Abs(mx - graph.CoPoints[i, 0]) < 10 && Math.Abs(my - graph.CoPoints[i, 1]) < 10)
                {
                    if (selectedChange != null)
                    {
                        int selChangeInt = Convert.ToInt32(selectedChange);
                        if (graph.Nodes[selChangeInt, i])
                        {
                            Edge toRemove = null;
                            foreach (Edge edg in graph.Edges)
                            {
                                if(edg.NodeOne == selChangeInt && edg.NodeTwo == i
                                    || edg.NodeOne == i && edg.NodeTwo == selChangeInt)
                                {
                                    toRemove = edg;
                                    break;
                                }
                            }
                            graph.Edges.Remove(toRemove);
                        } 
                        else
                        {
                            graph.Edges.Add(new Edge(i, selChangeInt, new Random().Next(1,20)));
                        }
                        graph.Nodes[selChangeInt, i] = !graph.Nodes[selChangeInt, i];
                        graph.Nodes[i, selChangeInt] = !graph.Nodes[i, selChangeInt];
                        selectedChange = null;
                        panel1.Refresh();
                        graph.CalculatedDeg();
                        return;
                    }
                    else
                    {
                        selectedChange = i;
                        panel1.Refresh();
                        return;
                    }


                }

            }
            selectedChange = null;

        }


        public void checkConectivity(int last)
        {
            graph.Connected = false;
            if (algorithmNodes.Count == graph.N)
            {
                graph.Connected = true;
                return;
            }

            for (int j = 1; j < graph.N; j++)
            {
                if (graph.Nodes[last, j] && !algorithmNodes.Contains(j))
                {
                    Edge temp = new Edge();
                    temp.NodeOne = last;
                    temp.NodeTwo = j;
                    algorithmEdges.Add(temp);
                    algorithmNodes.Add(j);

                    if (drawAnimation)
                    {
                        panel1.Refresh();
                        System.Threading.Thread.Sleep(500);
                    }

                    checkConectivity(j);
                }
            }
        }



        private void conectivityButton_Click(object sender, EventArgs e)
        {
            if (graph == null) return;
            editedEdge = null;
            groupBox3.Visible = false;
            algorithmEdges.Clear();
            algorithmNodes.Clear();
            panel1.Refresh();
            drawAnimation = true;
            algorithmNodes.Add(0);
            checkConectivity(0);

            if (graph.Connected)
            {
                MessageBox.Show("Graph is connected");
            }
            else
            {
                MessageBox.Show("Graph is not connected ");
                drawAnimation = false;
                algorithmNodes.Clear();
            }
        }

       
        private void mstButton_Click(object sender, EventArgs e)
        {
            if (graph == null) return;
            editedEdge = null;
            groupBox3.Visible = false;
            graph.Connected = false;
            algorithmNodes.Clear();
            algorithmNodes.Add(0);
            algorithmEdges.Clear();
            drawAnimation = false;
            checkConectivity(0);
            algorithmEdges.Clear();
            algorithmNodes.Clear();

            if (!graph.Connected)
            {
                MessageBox.Show("Graph is not connected ");
                return;
            }

            selectedChange = null;
            panel1.Refresh();

            List<Edge> mstEdges = new List<Edge>();
            List<Edge> clonedEdges = graph.Edges.OrderBy(edge => edge.Cost).ToList();
            DisjointSet ds = new DisjointSet(graph.N);
            ds.MakeSet(graph.N);

            int index = 0;

            while (mstEdges.Count != graph.N - 1)
            {
                Edge next = clonedEdges[index++];


                int x = ds.Find(next.NodeOne);
                int y = ds.Find(next.NodeTwo);

 
                if (x != y)
                {
                    mstEdges.Add(next);
                    ds.Union(x, y);
                }
            }


            drawAnimation = true;
            String massage = "Minimal spanning tree edges:" + Environment.NewLine;
            double totalCost = 0;
            foreach (Edge edg in mstEdges)
            {
                algorithmEdges.Add(edg);
                panel1.Refresh();
                System.Threading.Thread.Sleep(500);
                massage += edg.GetString() + Environment.NewLine;
                totalCost += edg.Cost;
            }
            massage += "Total cost: " + totalCost;
            drawAnimation = false;

            MessageBox.Show(massage, "Spanning Tree");
            algorithmEdges.Clear();
            algorithmNodes.Clear();
        }



        private void clearButton_Click(object sender, EventArgs e)
        {
            graph = null;
            selectedChange = null;
            editedEdge = null;
            groupBox3.Visible = false;
            panel1.Refresh();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            drawGrid ^= true;
            panel1.Refresh();
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs) e;
            if (graph == null || mouse.Button != MouseButtons.Left) return;
            int x = mouse.X;
            int y = mouse.Y;
            int[,] pos = graph.CoPoints;

            foreach(Edge ed in graph.Edges)
            {
                int l1 = ed.NodeOne;
                int l2 = ed.NodeTwo;

                double distance = Math.Abs((pos[l2,0] - pos[l1,0]) * (pos[l1, 1] - y) - (pos[l1, 0] -x) 
                    * (pos[l2, 1] - pos[l1, 1])) / Math.Sqrt(Math.Pow(pos[l2, 0] - pos[l1, 0], 2) 
                    + Math.Pow(pos[l2, 1] - pos[l1, 1], 2));

                if(distance <5)
                {
                    groupBox3.Visible = true;
                    textBox2.Text = ed.Cost.ToString();
                    editedEdge = ed;
                    panel1.Refresh();
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String text = textBox2.Text;

            try
            {
                int newCost = Int32.Parse(text);
                if(newCost>0 && newCost <= 20)
                {
                    editedEdge.Cost = newCost;
                }
            }
            catch
            {

            }

            editedEdge = null;
            groupBox3.Visible = false;
            panel1.Refresh();
        }
    }
}
