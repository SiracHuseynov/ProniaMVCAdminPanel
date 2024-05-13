using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaMVCProject.Business.Exceptions
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException(string? message) : base(message)
        {
        }
    }
}
