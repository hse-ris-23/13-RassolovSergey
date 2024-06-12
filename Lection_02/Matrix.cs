using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork13
{
    // Делегат: ( Источник | Необходимая информация )
    delegate void MatrixHandler(object source, MatrEventArgs args);

    // Класс - Мтрица
    internal class Matrix
    {
        int[,] matr;                // Матрица (из массивов)
        Random rnd = new Random();  // ДСЧ
        public string Name { get; set; }            // Свойство - Name
        public int GetSize => matr.GetLength(0);    // Метод получения - Длины


        // Событие №1
        public event MatrixHandler createNumber;

        // Событие №2
        public event MatrixHandler createZero;

        // Конструктор ( Имя, Размер)
        public Matrix(string name, int size)
        {
            Name = name;
            matr = new int[size, size];
        }


        // Метод создания события
        public void OnCreateNumber (object source, MatrEventArgs args)
        {
            if ( createNumber != null)
            {
                createNumber(this, args);
            }
        }

        // 
        public void OnCreateZero(object source, MatrEventArgs args)
        {
            if (createZero != null)
            {
                createZero(source, args);
            }
        }

        // Метод генерации Матрицы
        public void GenerateMatrix()
        {
            for (int i = 0; i < GetSize; i++)
            {
                for (int j = 0; j < GetSize; j++)
                {
                    matr[i, j] = rnd.Next(-10, 10);
                    OnCreateNumber(this, new MatrEventArgs(matr[i, j], i, j));
                    if (matr[i, j] == 0)
                    {
                        OnCreateZero(this, new MatrEventArgs(matr[i, j], i, j));
                    }
                }
            }
        }

        // Метод - вывода матрицы
        public void PrintMatrix()
        {
            for (int i = 0; i < GetSize; i++)
            {
                for (int j = 0; j < GetSize; j++)
                {
                    Console.Write($"{matr[i,j],4}");
                }
                Console.WriteLine();
            }
        }
    }
}
