using ClinicSystem.DataType.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.DataType.Base
{
    public class SaveResult<T> where T : new()
    {
        public SaveResult(bool successed, T item, OperationType operation)
        {
            Successed = successed;
            Item = item;
            Operation = operation;
        }
        public bool Successed { get; set; }
        public T Item { get; set; }
        public OperationType Operation { get; set; }
    }
}
