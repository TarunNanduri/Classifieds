using System;
using Classifieds.App.Models;
using Classifieds.App.Services.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Classifieds.App.Api.Controllers
{
    [Route("classifieds/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpPost]
        public IActionResult ReportAdvertisement([FromBody] Report report)
        {
            try
            {
                var newReport = _reportRepository.ReportAdvertisement(report);
                if (newReport == null) return BadRequest("Advertisement already deleted");
                _reportRepository.Add(newReport);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}