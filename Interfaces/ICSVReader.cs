using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICSVReader
    {
        IEnumerable<SalesReportDto> ReadFile(Stream stream);

    }
}
