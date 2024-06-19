using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork13
{
    internal class Jornal
    {
        List<string> jornal = new List<string>();


        // Обработчик №1
        public void WriteRecord(object source, MatrEventArgs args)
        {
            jornal.Add($"В матрицe {((Matrix)source).Name} записано число {args.Number} в строку {args.Row}, в столбец {args.Column}");
        }

        // Обработчик №2
        public void WriteZero(object source, MatrEventArgs args)
        {
            jornal.Add($"В матрицe {((Matrix)source).Name} записан 0 в строку {args.Row}, в столбец {args.Column}");
        }
        public void Printjornal()
        {
            if (jornal.Count == 0)
            {
                Console.WriteLine("Журнал пуст!");
                return;
            }
            foreach (var item in jornal)
            {
                Console.WriteLine(item);
            }
        }
    }
}
