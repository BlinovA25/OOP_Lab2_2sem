using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Lab2_1
{
    public partial class Form2 : Form
    {
        public static string Surname, LName, Otch;

        public Form2()
        {
            InitializeComponent();
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        //Lector lector = new Lector();

        private void button1_Click(object sender, EventArgs e)
        {
            FIO.Surname = this.textBox1.Text;
            FIO.Name = this.textBox2.Text;
            FIO.Otch = this.textBox3.Text;

            MessageBox.Show($"{FIO.Surname} {FIO.Name} {FIO.Otch}");


            this.Hide();
        }
    }
}
