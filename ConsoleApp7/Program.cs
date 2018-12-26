using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp35
{
    delegate int PlusOrMinus(int param1, int param2);

    class Program
    {

        static int Plus(int p1, int p2)
        {
            return p1 + p2;
        }

        static int Minus(int p1, int p2)
        {
            return p1 - p2;
        }

        //метод, принимающий разработанный делегат
        static void PlusOrMinusMethod(int a, int b, PlusOrMinus PlusOrMinusParam)
        {
            int Result = PlusOrMinusParam(a, b);
            Console.WriteLine(Result.ToString());
        }

        //обобщенный делегат
        static void PlusOrMinusMethodFunc(string str, int a, int b, Func<int, int, int> PlusOrMinusParam)
        {
            int Result = PlusOrMinusParam(a, b);
            Console.WriteLine(str + Result.ToString());
        }


        static void Main(string[] args)
        {

            Console.WriteLine("ЧАСТЬ 1: делегаты");
            int a = 10;
            int b = 25;

            Console.Write("\nСложение: ");
            PlusOrMinusMethod(a, b, Plus);
            Console.Write("Вычитание: ");
            PlusOrMinusMethod(a, b, Minus);

            PlusOrMinus p = new PlusOrMinus(Plus);
            Console.Write("\nПередаем в качестве параметра-делегата метод Плюс: ");
            PlusOrMinusMethod(a, b, p);

            Console.Write("Передаем в качестве параметра-делегата лямбда-выражение a-b: ");
            PlusOrMinusMethod(a, b, (x, y) => x - y);

            Console.WriteLine("\nИспользование обощенного делегата Func<>: ");

            PlusOrMinusMethodFunc("Создание экземпляра делегата на основе метода Минус: ", a, b, Minus);

            PlusOrMinusMethodFunc("Создание экземпляра делегата на основе лямбда-выражения a+b: ", a, b, (x, y) => x + y);



            Console.WriteLine("\nЧАСТЬ 2: рефлексия");
            Roudure obj = new Roudure(10);

            Type t = obj.GetType();
            Console.WriteLine("\nТип: ");
            Console.WriteLine($"Пространство имён {t.Namespace}\nИмя: {t.FullName}\nСборка: {t.AssemblyQualifiedName}");
            Console.WriteLine("\nКонструкторы:");
            foreach (var x in t.GetConstructors())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("\nПоля:");
            foreach (var x in t.GetFields())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("\nСвойства:");
            foreach (var x in t.GetProperties())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("\nМетоды:");
            foreach (var x in t.GetMethods())
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("\nСвойства с атрибутами:");
            foreach (var x in t.GetProperties())
            {
                if (x.GetCustomAttributes(typeof(NewAttribute), false).Length > 0)
                {
                    Console.WriteLine(x + " " + NewAttribute.Description);
                }
            }
            string method = "ToString";
            Console.WriteLine($"\nВызов метода {method} с помощью рефлексии:");
            object result = t.InvokeMember(method, System.Reflection.BindingFlags.InvokeMethod, null, obj, null);
            Console.WriteLine(result);


            Console.ReadLine();

        }

        public class Roudure
        {
            public double rad = 0;
            public Roudure(double r) { rad = r; }

            public double square { get { return rad * rad * 3.14159; } }
            [NewAttribute("Диаметр окружности")]
            public double diameter { get { return 2 * rad; } }
            [NewAttribute("Длина окружности")]
            public double length { get { return diameter * 3.14159; } }

            public override string ToString()
            {
                return $"Радиус окружности: {rad}.\nПлощадь внутри окружности: {square}.\nДлина окружности: {length}.";
            }

        }




        public class NewAttribute : Attribute
        {
            public NewAttribute() { }
            public NewAttribute(string DescriptionParam)
            {
                Description = DescriptionParam;
            }
            public static string Description { get; set; }
        }
    }
}
