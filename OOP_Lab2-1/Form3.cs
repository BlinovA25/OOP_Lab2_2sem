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
    public partial class Form3 : Form
    {
        ToolStripLabel stateLabel1;
        public string State = "";
        public List<Discipline> list;

        public Form3()
        {
            InitializeComponent();

            stateLabel1 = new ToolStripLabel();
            //stateLabel1.Text = State;
        }
       
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void поЛекторуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "Поиск по лектору";
        }

        private void поСеместруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "Поиск по семестру";
        }

        private void поКурсуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "Поиск по курсу";
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = String.Empty;
            

            var N = from Discipline d in list
                    where d.lector.Surname.IndexOf(textBoxName.Text, StringComparison.OrdinalIgnoreCase) >= 0 //
                    select d;

            var S = N;
            if (textBox3.Text != "")
            {
                S = from Discipline d in N
                        where d.Sem == Convert.ToInt32(textBox3.Text)
                        select d;
            }

            var K = S;
            if (textBox4.Text != "")
            {
                K = from Discipline d in S
                    where d.Kurs == Convert.ToInt32(textBox4.Text)
                    select d;
            }

            foreach (Discipline disc in K)
            {
                    textBox2.Text += disc.ToString() + "\r\n";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
