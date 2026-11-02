using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Authentication.Application.Authentication.Commands.AddUser
{
   public record AddUserRequest
    (
      string UserName,
        string Email,
        string Password,
        List<string>? companyIds,
        List<string> rolesId

    );
}
