using System;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;//.Regex;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OOP_Lab2_1
{
    public partial class Form1 : Form
    {
        //Regex newReg = new Regex(pattern, RegexOptions.None);

        ToolStripLabel dateLabel1;
        ToolStripLabel timeLabel1;
        ToolStripLabel infoLabel1;
        ToolStripLabel infoLabel2;
        ToolStripLabel stateLabel2;
        Timer timer;
        List<Discipline> listDisciplines;

        public string State = "";

        public Form1(Form2 f2)
        {
            InitializeComponent();

            string N = f2.textBox1.Text;
            string S = f2.textBox2.Text;
            string O = f2.textBox3.Text;
        }

        public Form1()
        {
            InitializeComponent();

            infoLabel1 = new ToolStripLabel();
            infoLabel1.Text = "Текущие дата и время:";
            dateLabel1 = new ToolStripLabel();
            timeLabel1 = new ToolStripLabel();
            stateLabel2 = new ToolStripLabel();

            statusStrip1.Items.Add(infoLabel1);
            statusStrip1.Items.Add(dateLabel1);
            statusStrip1.Items.Add(timeLabel1);

            timer = new Timer() { Interval = 1000 };
            timer.Tick += timer_Tick;
            timer.Start();


            infoLabel2 = new ToolStripLabel();
            infoLabel2.Text = $"Последнее действие: "; // {State};";

            statusStrip1.Items.Add(infoLabel2);
            statusStrip1.Items.Add(stateLabel2);
            if (File.Exists(@"F:\OOP\Discipline.xml"))
            {
                listDisciplines = ReadData();
            }
            else
            {
                listDisciplines = new List<Discipline>();
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            dateLabel1.Text = DateTime.Now.ToLongDateString();
            timeLabel1.Text = DateTime.Now.ToLongTimeString();

            stateLabel2.Text = State;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label7.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label8.Text = trackBar2.Value.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }


        public string SpecStr = "ПОИТ";
        private void radioButton1_CheckedChanged(object sender, EventArgs e)//ПОИТ
        {
            if (radioButton1.Checked)
            {
                SpecStr = radioButton1.Text;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)//ИСИТ
        {
            if (radioButton2.Checked)
            {
                SpecStr = radioButton2.Text;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)//ПОИБМС
        {
            if (radioButton3.Checked)
            {
                SpecStr = radioButton3.Text;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)//ДЭИВИ
        {
            if (radioButton4.Checked)
            {
                SpecStr = radioButton4.Text;
            }
        }

        //List<Discipline> list = new List<Discipline>();


        private void button1_Click(object sender, EventArgs e)
        {
            string filename = @"F:\OOP\Discipline.xml";

            Discipline discipline = new Discipline();

            Lector lector = new Lector();
            lector.Surname = FIO.Surname;
            lector.Name = FIO.Name;
            lector.Otch = FIO.Otch;

            discipline.Name = textBox2.Text;
            discipline.Pulpit = textBox3.Text;
            discipline.Spec = SpecStr;
            discipline.Sem = (int)numericUpDown1.Value;
            discipline.Kurs = (int)numericUpDown2.Value;
            discipline.NumOfLec = trackBar1.Value;
            discipline.NumOfLab = trackBar2.Value;
            discipline.ControlType = comboBox1.Text;

            discipline.lector = lector;

            discipline.lector.Name = FIO.Name;
            discipline.lector.Surname = FIO.Surname;
            discipline.lector.Otch = FIO.Otch;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(discipline);
            if (!Validator.TryValidateObject(discipline, context, results, true))
            {
                foreach (var error in results)
                {
                    MessageBox.Show(error.ErrorMessage);
                }
            }
            else 
            { 
                listDisciplines.Add(discipline);
                Prototype clone = discipline.Clone();
                //list.Add((Discipline)clone);
                
                State = "Добавление";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            label7.Text = "0";
            label8.Text = "0";
            comboBox1.Text = "Экзамен";

            State = "Очистка";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;

            

            foreach (Discipline discipline in listDisciplines)
            {
                textBox1.Text += $"Название дисциплины -- {discipline.Name}; \r\n";
                textBox1.Text += $"Кафедра -- {discipline.Pulpit}; \r\n";
                textBox1.Text += $"Специальность -- {discipline.Spec}; \r\n";
                textBox1.Text += $"Семестр -- {discipline.Sem}; \r\n";
                textBox1.Text += $"Курс -- {discipline.Kurs}; \r\n";
                textBox1.Text += $"Количество лекций -- {discipline.NumOfLec}; \r\n";
                textBox1.Text += $"Количество лаб -- {discipline.NumOfLab}; \r\n";
                textBox1.Text += $"Тип контроля -- {discipline.ControlType}; \r\n";

                textBox1.Text += $"Лектор: {discipline.lector.Surname}";
                textBox1.Text += $" {discipline.lector.Name}";
                textBox1.Text += $" {discipline.lector.Otch}\r\n\r\n";
            }

            State = "Вывод";
        }

        private static List<Discipline> ReadData()
        {
            string filename = @"F:\OOP\Discipline.xml";     

            List<Discipline> deserializeDisciplines = new List<Discipline>();
            deserializeDisciplines = XmlSerializeWrapper.Deserialize<List<Discipline>>(filename);//.ToString();
            return deserializeDisciplines;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
            //this.textBox1.Text = f.textBox1.Text;
        }

        public void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox2.Items.Add = FIO.Surname + ' ' + FIO.Name + ' ' + FIO.Otch;


        }

        public void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Discipline discipline = new Discipline();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Блинов А.Г. \n2021");
            
        }

        private void скрытьПанельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(скрытьПанельToolStripMenuItem.Text=="Скрыть панель")
            {
                скрытьПанельToolStripMenuItem.Text = "Показать панель";
                toolStrip1.Visible = false;
            }
           else if(скрытьПанельToolStripMenuItem.Text == "Показать панель")
            {
                скрытьПанельToolStripMenuItem.Text = "Скрыть панель";
                toolStrip1.Visible = true;
            }
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.list = listDisciplines;
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Singletone.Design(this);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.list = listDisciplines;
            f.ShowDialog();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            string filename = @"F:\OOP\Discipline.xml";

            Discipline discipline = new Discipline();

            Lector lector = new Lector();
            lector.Surname = FIO.Surname;
            lector.Name = FIO.Name;
            lector.Otch = FIO.Otch;

            discipline.Name = textBox2.Text;
            discipline.Pulpit = textBox3.Text;
            discipline.Spec = SpecStr;
            discipline.Sem = (int)numericUpDown1.Value;
            discipline.Kurs = (int)numericUpDown2.Value;
            discipline.NumOfLec = trackBar1.Value;
            discipline.NumOfLab = trackBar2.Value;
            discipline.ControlType = comboBox1.Text;

            discipline.lector = lector;

            discipline.lector.Name = FIO.Name;
            discipline.lector.Surname = FIO.Surname;
            discipline.lector.Otch = FIO.Otch;

            listDisciplines.Add(discipline);
            Prototype clone = discipline.Clone();
            listDisciplines.Add((Discipline)clone);
            XmlSerializeWrapper.Serialize(listDisciplines, filename);

            MessageBox.Show("Информация добавлена в файл");
            State = "Сохранение";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string filename = @"F:\OOP\Discipline.xml";
            XmlSerializeWrapper.Serialize(listDisciplines, filename);

            MessageBox.Show("Информация добавлена в файл");
            State = "Сохранение";
        }
    }
}
