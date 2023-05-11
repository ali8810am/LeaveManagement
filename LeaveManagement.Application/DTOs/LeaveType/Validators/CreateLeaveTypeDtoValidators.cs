using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class CreateLeaveTypeDtoValidators:AbstractValidator<CreateLeaveTypeDto>
    {
        public CreateLeaveTypeDtoValidators()
        {
            Include(new ILeaveTypeDtoValidator());

        }
    }
}
