using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Auth.Commands.GoogleLogin
{
    public sealed record GoogleLoginCommand(
        string IdToken,
        string DeviceId,
        string IpAddress
    ) : IRequest<ResponseWrapper<AuthResponseDto>>;
}
