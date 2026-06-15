using MediatR;
using Seph.Principal.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Auth.Commands.Register
{
    public sealed record RegisterCommand(string FullName, string Email, string Password)
       : IRequest<ResponseWrapper<string>>;
}
