using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimalSpanningTree
{
    public partial class GenerateForm : Form
    {
        public int k;
        public GenerateForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            k = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            k = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            k = 2;
        }
    }
}
