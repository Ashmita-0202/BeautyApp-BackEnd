using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Authentication.Application.Authentication.Commands.Register.RequestCommandMapper
{
    public static class RegisterRequestMapper
    {
        public static RegisterCommand ToCommand(this RegisterRequest request)
        {
            return new RegisterCommand(request.username,request.email, request.password, request.IsBeautician);
        }
    }
}
