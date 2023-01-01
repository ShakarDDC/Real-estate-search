using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_5_Miracle
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProperty ap = new AddProperty();
            this.Hide();
            ap.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showAllProperty sp = new showAllProperty();
            this.Hide();
            sp.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchProperty sp = new searchProperty();
            this.Hide();
            sp.ShowDialog();
            this.Close();
        }
    }
}
