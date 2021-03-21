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

    public class SecondSemFactory : IFactory
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

    public class PulpitAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string Pulpit = value.ToString();
                if (Pulpit == "ИСИТ" && Pulpit == "ПОИТ" && Pulpit == "Мат" && Pulpit == "Физ" && Pulpit == "ДЭиВИ")
                    return true;
                else
                    this.ErrorMessage = "Список возможных названий кафедр: ИСИТ, ПОИТ, ДЭиВИ, Мат, Физ.";
            }
            return false;
        }
    }


    public class Lector 
    {
        [RegularExpression(@"^[А - Я][а - я]*$", ErrorMessage = "Фамилия введена неправильно(скорее всего со строчной буквы или латинским алфавитом).")]
        public string Surname;
        [RegularExpression(@"^[А - Я]", ErrorMessage = "Имя введено неправильно(необходимо ввести хотя бы одну букву и первая буква должна быть заглавной).")]
        public string Name;
        [RegularExpression(@"^[А - Я]", ErrorMessage = "Отчество введено неправильно(необходимо ввести хотя бы одну букву и первая буква должна быть заглавной).")]
        public string Otch;

        public string Pulpit;

        public Lector() 
        { }

        LectorBuilder builder;
        public Lector(LectorBuilder builder)
        {
            this.builder = builder;
        }
        public void Construct()
        {
            builder.setSurname();
            builder.setName();
            builder.setOtch();
        }
    }

    [Serializable]
    [XmlRoot(Namespace = "OOP_Lab2_1")]
    [XmlType("Disc")]
    public class Discipline : Prototype // ValidationAttribute, 
    {
        [Required]
        [StringLength(15, MinimumLength = 2)]
        public string Name { get; set; }
        [Pulpit]
        public string Pulpit;
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
            return String.Format("{0, -15} {1, -15} {2,-3}  {3,-3} {4,-3} {5}", lector.Surname, Name, Sem, Kurs, NumOfLec, ControlType);
        }
    }


    //builder
    public abstract class LectorBuilder
    {
        public abstract void setSurname();
        public abstract void setName();
        public abstract void setOtch();
        public abstract Lector GetResult();
    }

    public class ConcreteLectorBuilder : LectorBuilder
    {
        Lector lector = new Lector();

        public override void setSurname()
        {
            lector.Surname = "Пацей";
        }

        public override void setName()
        {
            lector.Name = "Н";
        }

        public override void setOtch()
        {
            lector.Otch = "В";
        }
        public override Lector GetResult()
        {
            return lector;
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
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read))
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
