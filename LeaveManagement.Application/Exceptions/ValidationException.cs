using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace LeaveManagement.Application.Exceptions
{
    public class ValidationException:ApplicationException, IEnumerable
    {
        public List<string> Errors { get; set; }=new List<string>();

        public ValidationException(ValidationResult validationException)
        {
            foreach (var error in validationException.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
