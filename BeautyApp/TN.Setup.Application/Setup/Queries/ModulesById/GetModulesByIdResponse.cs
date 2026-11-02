using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Setup.Application.Setup.Queries.ModulesById
{
    public record GetModulesByIdResponse
    (
            string id,
            string name,
            string? role,
            string? targetUrl,
            bool isActive

    );
}
