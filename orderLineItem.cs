using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Willprint_Reservation_System
{
    public partial class orderLineItem : Form
    {
        public orderLineItem()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string text = textBox4.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                textBox4.Text = string.Join("", text.Where(char.IsDigit));
                textBox4.SelectionStart = textBox4.Text.Length;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string text = textBox2.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                textBox2.Text = string.Join("", text.Where(char.IsDigit));
                textBox2.SelectionStart = textBox2.Text.Length;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
