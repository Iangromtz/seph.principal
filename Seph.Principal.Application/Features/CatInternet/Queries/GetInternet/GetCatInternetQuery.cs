using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.CatArea.DTOs;
using Seph.Principal.Application.Features.CatInternet.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.CatInternet.Queries.GetInternet
{
    public sealed record GetCatInternetQuery()
     : IRequest<ResponseWrapper<IReadOnlyList<CatInternetDto>>>;
}

