﻿using System;
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
    public partial class inventory : Form
    {
        public inventory()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
