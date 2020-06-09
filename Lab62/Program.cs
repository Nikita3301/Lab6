using System;
using System.IO;
using System.Text;

namespace Lab62
{
    interface IWorker
    {
        void Delete();
        void Output();
    }
    public abstract class Coworker : IWorker
    {
        private string PIB;
        private string Position;

        public string Pib
        {
            get { return PIB; }
            set { PIB = value; }
        }

        public string position
        {
            get { return Position; }
            set { Position = value; }
        }

        

        public void Add(string Pib, string position, string date, int hours, string project )
        {
            Console.WriteLine(
                "\nЯкщо ви бажаєте зберегти змiни то натиснiть Enter, якщо нi, то будь-яку iншу клавiшу.");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                using (StreamWriter f = new StreamWriter(
                    "C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt",
                    true))
                    f.WriteLine("{0,-31}{1,-16}{2,-15}{3,-5}{4,-20}", Pib, position, date, hours, project);
                Console.WriteLine("Змiни збережено\n");
            }
            else
            {
                Console.WriteLine("\nЗмiни не збережено\n");
            }
        }

        
        public void Edit(string Pib, string position, string date, int hours, string project, int number, int length, string[] str)
        {
            
            Console.WriteLine("\nЗбереження змін - Enter, відміна - будь-яка інша клавіша.");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                using (StreamWriter f = new StreamWriter("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt"))
                    for (int i = 0; i < length; i++)
                    {
                        if (i != number - 1) f.WriteLine(str[i]);
                        else
                            f.WriteLine("{0,-31}{1,-16}{2,-15}{3,-5}{4,-20}", Pib, position, date,
                                hours, project);
                    }

                Console.WriteLine("Змiни збережено\n");
            }
            else
            {
                Console.WriteLine("\nЗмiни не збережено\n");
            }
        }

        public abstract void Delete();

        public abstract void Output();

    }

    class Workday : Coworker
    {
        private string Date;
        private int Hours;
        private string Project;

        public string date
        {
            get { return Date; }
            set { Date = value; }
        }

        public int hours
        {
            get { return Hours; }
            set { Hours = value; }
        }

        public string project
        {
            get { return Project; }
            set { Project = value; }
        }

        public Workday()
        {
        }
        public void Calculation()
        {
            Workday calc = new Workday();
            int length = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt").Length;
            string[] str = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt");
            Console.WriteLine("{0,-31}{1,-16}{2,-24}{3,-12}{4,-10}", "ПІБ","Посада","Дата","Роб. годин","Год. на проекті");
            for (int i = 0; i < length; i++)
            {
                calc.Pib = str[i].Substring(0, 30);
                calc.date = str[i].Substring(47, 10);
                var d1 = Convert.ToDateTime(str[i].Substring(47, 10));
                for (int j = 0; j < length; j++)
                {
                    if (str[j].Substring(66, 20) == str[i].Substring(66, 20))
                    {
                        var d2 = Convert.ToDateTime(str[j].Substring(47,10));
                        TimeSpan t = d2 - d1;
                        var hours = t.TotalHours;
                        var days = t.Days;
                        var date1 = Convert.ToDateTime(str[i].Substring(47, 14)).ToShortDateString();
                        var date2= Convert.ToDateTime(str[j].Substring(47, 14)).ToShortDateString();
                        var workhours = days * Convert.ToInt32(str[i].Substring(61, 4));
                        if (d2>d1)
                        {Console.WriteLine("{0,-31}{1,-16}{2,-11}- {3,-13}{4,-15}{5,-10}",str[i].Substring(0,30),str[i].Substring(31,15),date1,date2,workhours,hours);}
                    }
                }
            }
            
            
        }

        public override void Output()
        {
            int length = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt").Length;
            string[] str = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt");
            Console.WriteLine("{0,-31}{1,-16}{2,-15}{3,-5}{4,-20}", "ПІБ","Посада","Дата","Год.","Назва проекту");
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(str[i]);
            }
        }
        private static int Checknum(string number1)
        {
            int result = 0;
            while (!int.TryParse(number1, out result))
            {
                Console.Write("Введіть номер ще раз:");
                number1 = Console.ReadLine();
            }

            result = Convert.ToInt32(number1);
            return result;
        }
        public override void Delete()
        {
            int length = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt").Length;
            Console.WriteLine("Номер рядку:");
            int number = Checknum(Console.ReadLine());
            while (number > length || number <= 0)
            {
                Console.WriteLine("Номер рядку не може бути менше нуля або більше " + (length));
                number = Checknum(Console.ReadLine());
            }

            string[] str = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt");
            Console.WriteLine("\nЗбереження змін - Enter, відміна - будь-яка інша клавіша.");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                using (StreamWriter f = new StreamWriter("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt"))
                    for (int i = 0; i < length; i++)
                    {
                        if (i != number - 1) f.WriteLine(str[i]);
                    }

                Console.WriteLine("Змiни збережено\n");
            }
            else
            {
                Console.WriteLine("\nЗмiни не збережено\n");
            }
        }
    }
    class Program
    {
        private static string Checkdate(string date)
        {
            DateTime dateTime;
            while (!DateTime.TryParse(date, out dateTime))
            {
                Console.WriteLine("Невiрне значення");
                Console.Write("Дата(дд.мм.рр):");
                date = Console.ReadLine();
            }

            dateTime = Convert.ToDateTime(date);
            date = dateTime.ToShortDateString();
            return date;
        }

        private static int Checkhours(string hours)
        {
            int result = 0;
            while (!int.TryParse(hours, out result))
            {
                Console.WriteLine("Невiрне значення");
                Console.Write("Введіть кількість годин ще раз:");
                hours = Console.ReadLine();
            }
            result = Convert.ToInt32(hours);
            return result;
        }
        private static int Checknum(string number1)
        {
            int result = 0;
            while (!int.TryParse(number1, out result))
            {
                Console.Write("Введіть номер ще раз:");
                number1 = Console.ReadLine();
            }

            result = Convert.ToInt32(number1);
            return result;
        }

        static void Main(string[] args)
        {
            //Coworker cw = new Coworker();
            Workday wd = new Workday();
            
            while (true)
            {
                Console.OutputEncoding = Encoding.Unicode;
                Console.WriteLine("\nВибір режиму роботи: ");
                Console.WriteLine("Додавання записiв - Enter");
                Console.WriteLine("Редагування записiв - E");
                Console.WriteLine("Знищення записiв - Delete");
                Console.WriteLine("Виведення iнформацiї з файла на екран - Tab");
                Console.WriteLine("Індивідуальне завдання - С");
                ConsoleKeyInfo choice;
                choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.Enter)
                {
                    Console.InputEncoding = Encoding.Unicode;
                    Workday add = new Workday();
                    Console.WriteLine("Введіть ПІБ:");
                    string pib = Console.ReadLine();
                    while (string.IsNullOrEmpty(pib))
                    {
                        Console.WriteLine("Введіть ПІБ ще раз:");
                        pib = Console.ReadLine();
                    }

                    add.Pib = pib;
                    Console.WriteLine("Введіть посаду:");
                    string position = Console.ReadLine();
                    while (string.IsNullOrEmpty(position))
                    {
                        Console.WriteLine("Введіть посаду ще раз:");
                        position = Console.ReadLine();
                    }

                    add.position = position;
                    Console.WriteLine("Введіть дату:");
                    string date = Console.ReadLine();
                    add.date = Checkdate(date);
                    Console.WriteLine("Введіть кількість годин:");
                    string hours = Console.ReadLine();
                    add.hours = Checkhours(hours);
                    Console.WriteLine("Введіть назву проекту:");
                    string project = Console.ReadLine();
                    while (string.IsNullOrEmpty(project))
                    {
                        Console.WriteLine("Введіть назву проекту ще раз:");
                        project = Console.ReadLine();
                    }

                    add.project = project;
                    

                    wd.Add(add.Pib,add.position,add.date,add.hours,add.project);
                }

                if (choice.Key == ConsoleKey.E)
                {
                    Workday edit = new Workday();
                    int length = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt").Length;
                    Console.WriteLine("Номер рядку:");
                    int number = Checknum(Console.ReadLine());
                    while (number > length || number <= 0)
                    {
                        Console.WriteLine("Номер рядку не може бути менше нуля або більше " + length );
                        number = Checknum(Console.ReadLine());
                    }

                    string[] str = File.ReadAllLines("C:\\Users\\s\\RiderProjects\\Lab62\\Coworker.txt");
                    string line = str[number - 1];
                    edit.Pib = line.Substring(0, 30);
                    edit.position = line.Substring(31, 15);
                    edit.date = line.Substring(47, 13);
                    edit.hours = Convert.ToInt32(line.Substring(61, 4));
                    edit.project = line.Substring(67, 20);
                    Console.WriteLine("Введiть номер елементу стовпчика, який потрібно змінити: ");
                    int number1 = Checknum(Console.ReadLine());
                    while (number1 > 5 || number1 <= 0)
                    {
                        Console.WriteLine("Номер стовпчика не може бути менше нуля або більше п'яти");
                        number1 = Checknum(Console.ReadLine());
                    }

                    if (number1 == 1)
                    {
                        Console.WriteLine("Введіть ПІБ:");
                        string pib = Console.ReadLine();
                        while (string.IsNullOrEmpty(pib))
                        {
                            Console.WriteLine("Введіть ПІБ ще раз:");
                            pib = Console.ReadLine();
                        }

                        edit.Pib = pib;
                    }

                    if (number1 == 2)
                    {
                        Console.WriteLine("Введіть посаду:");
                        string position = Console.ReadLine();
                        while (string.IsNullOrEmpty(position))
                        {
                            Console.WriteLine("Введіть посаду ще раз:");
                            position = Console.ReadLine();
                        }

                        edit.position = position;
                    }

                    if (number1 == 3)
                    {
                        Console.WriteLine("Введіть дату:");
                        string date = Console.ReadLine();
                        edit.date = Checkdate(date);
                    }

                    if (number1 == 4)
                    {
                        Console.WriteLine("Введіть кількість годин:");
                        string hours = Console.ReadLine();
                        edit.hours = Checkhours(hours);
                    }

                    if (number1 == 5)
                    {
                        Console.WriteLine("Введіть назву проекту:");
                        string project = Console.ReadLine();
                        while (string.IsNullOrEmpty(project))
                        {
                            Console.WriteLine("Введіть назву проекту ще раз:");
                            project = Console.ReadLine();
                        }

                        edit.project = project;
                    }
                    wd.Edit(edit.Pib,edit.position,edit.date,edit.hours,edit.project,number,length, str);
                }

                if (choice.Key == ConsoleKey.Delete)
                {
                    
                    wd.Delete();
                }

                if (choice.Key == ConsoleKey.Tab)
                {
                    
                    wd.Output();
                }

                if (choice.Key == ConsoleKey.C)
                {
                    wd.Calculation();
                }

            }
        }

    }
}