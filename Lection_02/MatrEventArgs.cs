using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork13
{
    // Класс - конверт для передачи информации
    internal class MatrEventArgs: EventArgs
    {
        public int Number { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public MatrEventArgs(int number, int row, int column)
        {
            Number = number;
            Row = row;
            Column = column;
        }
    }
}
