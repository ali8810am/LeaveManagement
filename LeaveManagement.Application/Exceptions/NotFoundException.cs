using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Exceptions
{
    public class NotFoundException:ApplicationException
    {
        public NotFoundException(string message,object key): base($"{message} and {key} was not found")
        {
            
        }
    }
}
