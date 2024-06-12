
using LabWork13;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lection_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix("Matrix 1", 5);
            Matrix m2 = new Matrix("Matrix 2", 10);

            Jornal j1 = new Jornal();
            Jornal j2 = new Jornal();

            m1.createNumber += j1.WriteRecord; // Подписка
            m2.createNumber += j2.WriteRecord; // Подписка
            m1.createZero += j1.WriteZero;
            m2.createZero += j2.WriteZero;


            //// СПОСОБ №2 - Анонимные методы (Без журналов) - используются редко
            //m1.createZero += delegate { Console.WriteLine($"Сгенерирован 0 в матрице {m1.Name}"); };
            //m2.createZero += delegate { Console.WriteLine($"Сгенерирован 0 в матрице {m2.Name}"); };

            //// Способ №3 - Лямбда -выражение
            //m1.createZero += (object source, MatrEventArgs args) => { Console.WriteLine($"Сгенерирован 0 в матрице {m1.Name}"); };
            //m2.createZero += (object source, MatrEventArgs args) => { Console.WriteLine($"Сгенерирован 0 в матрице {m2.Name}"); };


            // Формируется и печатается матрица №1
            Console.WriteLine("Матрица №1: Matrix 1");
            m1.GenerateMatrix();
            m1.PrintMatrix();


            // Формируется и печатается матрица №2
            Console.WriteLine("Матрица №2: Matrix 2");
            m2.GenerateMatrix();
            m2.PrintMatrix();

            //// Печатаем журнал событий №1
            //Console.WriteLine($"\nПечать журнала для {m1.Name}, {m2.Name}: \n");
            //j1.Printjornal();

            //// Печатаем журнал событий №2
            //Console.WriteLine($"\nПечать журнала с нулями для {m1.Name}, {m2.Name}: \n");
            //j2.Printjornal();
        }
    }
}
