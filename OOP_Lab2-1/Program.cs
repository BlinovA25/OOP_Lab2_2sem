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
using NuGet.Protocol.Plugins;

namespace OOP_Lab2_1
{
    //4 лаба
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
        private Singletone()
        { }
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


    //5 лаба(структурные паттерны и паттерны поведения)
    //State 
    public enum KursState
    {
        first = 1,
        second,
        third,
        forth
    }

    public class Kurs
    {
        public KursState State { get; set; }

        public Kurs(KursState st)
        {
            State = st;
        }

        public KursState More(KursState State)
        {
            if (State == KursState.first)
            {
                MessageBox.Show("1 курс -> 2 курс");
                State = KursState.second;
                return State;
            }
            else if (State == KursState.second)
            {
                MessageBox.Show("2 курс -> 3 курс");
                State = KursState.third;
                return State;
            }
            else if (State == KursState.third)
            {
                MessageBox.Show("3 курс -> 4 курс");
                State = KursState.forth;
                return State;
            }
            else
            {
                return State;
            }
        }
        public KursState Less(KursState State)
        {
            if (State == KursState.forth)
            {
                MessageBox.Show("4 курс -> 3 курс");
                State = KursState.third;
                return State;
            }
            else if (State == KursState.third)
            {
                MessageBox.Show("3 курс -> 2 курс");
                State = KursState.second;
                return State;
            }
            else if (State == KursState.second)
            {
                MessageBox.Show("2 курс -> 1 курс");
                State = KursState.first;
                return State;
            }
            else
            {
                return State;
            }
        }
    }

    //Decorator
    abstract class Subject
    {
        public Subject(string n)
        {
            this.Name = n;
        }
        public string Name { get; protected set; }
        public abstract int NumOfLessons();
    }

    class OOPSubject : Subject
    {
        public OOPSubject() : base("ООП")
        { }
        public override int NumOfLessons()
        {
            return 20;
        }
    }
    class DBSubject : Subject
    {
        public DBSubject() : base("Базы данных")
        { }
        public override int NumOfLessons()
        {
            return 20;
        }
    }

    abstract class SubjectDecorator : Subject
    {
        protected Subject subject;
        public SubjectDecorator(string n, Subject subject) : base(n)
        {
            this.subject = subject;
        }
    }

    class FirstSemSubject : SubjectDecorator
    {
        public FirstSemSubject(Subject subject) : base(subject.Name + " первый семестр", subject)
        { }

        public override int NumOfLessons()
        {
            return subject.NumOfLessons() + 8;
        }
    }

    class SecondSemSubject : SubjectDecorator
    {
        public SecondSemSubject(Subject subject) : base(subject.Name + " второй семестр", subject)
        { }

        public override int NumOfLessons()
        {
            return subject.NumOfLessons() + 10;
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
        { }

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
        public abstract void ToString();
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
        public override void ToString()
        {
            MessageBox.Show($"{lector.Surname} {lector.Name} {lector.Otch}");


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

            //паттерн Command
            //Invoker invoker = new Invoker();
            //Receiver receiver = new Receiver();
            //ConcreteCommand command = new ConcreteCommand(receiver);
            //invoker.SetCommand(command);
            //invoker.Run();
        }
    }
}
