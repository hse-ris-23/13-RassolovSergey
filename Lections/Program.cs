using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lections
{
    delegate int Handler(int x, int res);

    internal class Program
    {
        // Функция - перебора массива
        static int ForEach(int[] mas, Handler h, int res)
        {
            for (int i = 0; i < mas.Length; i++)
            {
                res = h(mas[i], res);
            }
            return res;
        }

        // Функция - счетчик отрицательных эл.
        static int CountNegativ(int x, int count)
        {
            if (x < 0) { count++; }
            return count;
        }

        // Функция - счетчик положительных эл.
        static int CountPositiv(int x, int count)
        {
            if (x > 0) { count++; }
            return count;
        }

        // Функиця - поиска минимального эл.
        static int Min(int x, int minimum)
        {
            if (x < minimum) { minimum = x; }
            return minimum;
        }

        // Функиця - поиска максимального эл.
        static int Max(int x, int maximum)
        {
            if (x > maximum) { maximum = x; }
            return maximum;
        }

        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3, 0, -1, 12, -10, 4 };

            int res = 0;
            Console.WriteLine($"Кол-во отрицательных элементов = {ForEach(arr, CountNegativ, res)}");

            res = 0;
            Console.WriteLine($"Кол-во положительных элементов = {ForEach(arr, CountPositiv, res)}");

            int min = arr[0];
            Console.WriteLine($"Минимальный элемент: {ForEach(arr, Min, min)}");

            int max = arr[0];
            Console.WriteLine($"Максимальный элемент: {ForEach(arr, Max, max)}");

        }
    }
}
