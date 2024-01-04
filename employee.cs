using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Willprint_Reservation_System
{
    public partial class employee : Form
    {
        public employee()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string text = textBox3.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                textBox3.Text = string.Join("", text.Where(char.IsDigit));
                textBox3.SelectionStart = textBox3.Text.Length;
            }
        }
    }
    
}
