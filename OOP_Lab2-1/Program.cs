using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace OOP_Lab2_1
{
    public interface IFactory
    {
        IProperty setProperty();
    }

    public interface IProperty
    {
        string Property { get; }
    }

    public class FirstSem : IProperty
    {
        public string Property => "1 семестр";
    }

    public class SecondSem : IProperty
    {
        public string Property => "2 семестр";
    }

    public class FirstSemFactory : IFactory
    {
        public IProperty setProperty()
        {
            return new FirstSem();
        }
    }

    public class NotSallingFactory : IFactory
    {
        public IProperty setProperty()
        {
            return new SecondSem();
        }
    }

    public sealed class Singletone
    {
        private static Singletone instance;
        private Singletone() { }
        private static Singletone GetInstance()
        {
            return instance ?? (instance = new Singletone());
        }
        public static void Design(Form form)
        {
            form.BackColor = Color.LightSeaGreen;
        }
    }

    public interface Prototype
    {
        Prototype Clone();
    }


    public class Lector 
    {
        public string Surname;
        public string Name;
        public string Otch;

        public string Pulpit;
    }

    [Serializable]
    [XmlRoot(Namespace = "OOP_Lab2_1")]
    [XmlType("Disc")]
    public class Discipline : Prototype
    {
        [Required]
        [StringLength(15, MinimumLength = 2)]
        public string Name { get; set; }
        public string Pulpit;// { get; set; }
        public string Spec;
        public int Sem;
        [Required]
        [Range(1,4)]
        public int Kurs { get; set; }
        public int NumOfLec;
        public int NumOfLab;
        public string ControlType;
        //[XmlIgnoreAttribute]
        public Lector lector;

        public Discipline()
        {

        }

        public Discipline(string Name, string Spec, int Sem, int Kurs, int NumOfLec, int NumOfLab, string ControlType, Lector lector)
        {
            this.Name = Name;
            this.Spec = Spec;
            this.Sem = Sem;
            this.Kurs = Kurs;
            this.NumOfLec = NumOfLec;
            this.NumOfLab = NumOfLab;
            this.ControlType = ControlType;
            this.lector = lector;
        }

        public Prototype Clone()
        {
            return new Discipline(this.Name, this.Spec, this.Sem, this.Kurs, this.NumOfLec, this.NumOfLab, this.ControlType, this.lector);
        }
        public override String ToString()
        {
            return String.Format("{0, -15}  {1}  {2}", lector.Surname, Sem, Kurs);
        }
    }

    public static class XmlSerializeWrapper
    {
        public static void Serialize<T>(T obj, string filename)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(fs, obj);
            }
        }

        public static T Deserialize<T>(string filename)
        {
            T obj;
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                obj = (T)serializer.Deserialize(fs);
            }
            return obj;
        }

    }

    public static class FIO 
    {
        public static string Surname;
        public static string Name;
        public static string Otch;
        public static string Pulpit;


    }
        static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
