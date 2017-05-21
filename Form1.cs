using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace busy_city
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            city.lng = Convert.ToDouble(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            city.lati = Convert.ToDouble(textBox2.Text);
        }
    }
}
