using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Models;

namespace LeaveManagement.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
      public Task<bool> SendEmail(Email email);
    }
}
