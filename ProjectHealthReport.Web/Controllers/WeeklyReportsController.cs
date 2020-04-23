using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Divisions.Commands;
using ProjectHealthReport.Features.Divisions.Queries;
using ProjectHealthReport.Features.WeeklyReports.Commands.AddEditWeeklyReportPhr;
using ProjectHealthReport.Features.WeeklyReports.Queries;
using ProjectHealthReport.Features.WeeklyReports.Queries.GetGeneratedWeeklyReportPhr;
using ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr;
using ProjectHealthReport.Web.Helpers;
using ProjectHealthReport.Web.Models;
using ResponsibilityChain.Business;

namespace ProjectHealthReport.Web.Controllers
{
    [Authorize]
    public class WeeklyReportsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WeeklyReportsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [Route("api/WeeklyReports/phr")]
        [ApiExceptionFilter]
        [HttpGet]
        public async Task<IActionResult> AddEditWeeklyReport([FromQuery] GetWeeklyReportPhrQuery weeklyReportQuery)
        {
            var dto = await _mediator.SendAsync(weeklyReportQuery);

            return Ok(dto);
        }

        [Route("api/WeeklyReports/phr")]
        [ApiExceptionFilter]
        [HttpPost]
        public async Task<IActionResult> AddEditWeeklyReport([FromBody] AddEditWeeklyReportPhrCommand command)
        {
            await _mediator.SendAsync(command);

            return Ok();
        }
        
        [Route("api/WeeklyReports/dmr/{name}/{yearWeek}")]
        [ApiExceptionFilter]
        [HttpGet]
        public async Task<ActionResult> GetDivisionWeeklyReport(string name, int yearWeek)
        {
            var dto = await _mediator.SendAsync(new GetDivisionWeeklyReportQuery()
                {DivisionName = name, SelectedYearWeek = yearWeek});

            return Ok(dto);
        }

        [Route("api/WeeklyReports/dmr")]
        [ApiExceptionFilter]
        [HttpPost]
        public async Task<ActionResult> AddEditDivisonWeeklyReport([FromBody] AddEditDivisionWeeklyReportCommand command)
        {
            var dto = await _mediator.SendAsync(command);

            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneratedWeeklyReport([FromQuery] GetGeneratedWeeklyReportPhrQuery query)
        {
            try
            {
                var dto = await _mediator.SendAsync(query);

                return View(dto);
            }
            catch (Exception ex)
            {
                return View("Error",
                    new ErrorViewModel() {Message = ex.Message, Type = ex.GetType().Name});
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExportPdfWeeklyReport([FromQuery] GetGeneratedWeeklyReportPhrQuery query)
        {
            try
            {
                var dto = await _mediator.SendAsync(query);
                var fileName =
                    $"{dto.WeeklyReport.SelectedYear}_{dto.WeeklyReport.SelectedWeek}_ProjectHealthReport_{dto.Project.Name}.pdf";

                var viewHtml = await this.RenderViewAsync("GetGeneratedWeeklyReportPDF", dto, fileName);


                var pdf = await _mediator.SendAsync(new ExportPdfWeeklyReportPhrQuery {HtmlContent = viewHtml.Content});

                return File(pdf, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel {Message = ex.Message, Type = ex.GetType().Name});
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPdfsZip([FromQuery] string zipFile)
        {
            var data = Convert.FromBase64String(zipFile);
            var filePath = Encoding.UTF8.GetString(data);
            var fileName = filePath.Split('\\')[^1];

            var zip = await System.IO.File.ReadAllBytesAsync(filePath);
            System.IO.File.Delete(filePath);

            return File(zip, "application/zip", fileName);
        }

        [ApiExceptionFilter]
        [HttpPost]
        public async Task<IActionResult> ExportMassPdfs([FromBody] ExportMassPdfsCommand command)
        {
            var tempFolder = CommonHelper.CreateTempReportsFolder();

            var listViewHtmlTask = new List<Task<RenderedView>>();
            foreach (var report in command.Reports)
            {
                var dto = await _mediator.SendAsync(new GetGeneratedWeeklyReportPhrQuery()
                {
                    Week = TimeHelper.CalculateWeek(report.YearWeek), Year = TimeHelper.CalculateYear(report.YearWeek),
                    ProjectId = report.ProjectId,
                    NumberOfWeek = command.ViewSettings.NumberOfWeek,
                    NumberOfWeekNotShowClosedItem = command.ViewSettings.NumberOfWeekNotShowClosedItem
                });
                var fileName =
                    $"{dto.WeeklyReport.SelectedYear}_{dto.WeeklyReport.SelectedWeek}_ProjectHealthReport_{dto.Project.Name}.pdf";
                var taskViewHtml = this.RenderViewAsync("GetGeneratedWeeklyReportPDF", dto, fileName);

                listViewHtmlTask.Add(taskViewHtml);
            }

            await Task.WhenAll(listViewHtmlTask);

            var listFile = new List<ReportFile>();
            foreach (var task in listViewHtmlTask)
            {
                var viewHtml = await task;
                var pdf = await _mediator.SendAsync(new ExportPdfWeeklyReportPhrQuery
                    {HtmlContent = viewHtml.Content});

                listFile.Add(new ReportFile {Name = viewHtml.Name, Content = pdf});
            }

            var listFileTask = new List<Task>();
            foreach (var file in listFile)
            {
                var task = System.IO.File.WriteAllBytesAsync(Path.Join(tempFolder, file.Name), file.Content);
                listFileTask.Add(task);
            }

            await Task.WhenAll(listFileTask);

            var zipFile = tempFolder + ".zip";

            ZipFile.CreateFromDirectory(tempFolder, zipFile);

            Directory.Delete(tempFolder, true);

            return Created("", zipFile);
        }

        private class ReportFile
        {
            public string Name { get; set; }
            public byte[] Content { get; set; }
        }
    }
}