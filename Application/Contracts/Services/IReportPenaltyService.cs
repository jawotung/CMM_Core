using Application.Models.Responses;
using Application.Models.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Services
{
    public interface IReportPenaltyService
    {
        Task<ReturnStatusData<ReturnDownload>> GenerateReport(Client clientInfo);
    }
}
