﻿using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Queries
{
    public class ExportPdfWeeklyReportPhrQuery : IRequest<byte[]>
    {
        public string HtmlContent { get; set; }

        public class Handler : ExecutionHandlerBase<ExportPdfWeeklyReportPhrQuery, byte[]>
        {
            private readonly IConverter _converter;

            public Handler(IConverter converter)
            {
                _converter = converter;
            }

            public override Task<byte[]> HandleAsync(ExportPdfWeeklyReportPhrQuery request)
            {
                var doc = new HtmlToPdfDocument()
                {
                    GlobalSettings =
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            HtmlContent = request.HtmlContent,
                            WebSettings = {DefaultEncoding = "utf-8"},
                        }
                    }
                };

                var pdf = _converter.Convert(doc);

                return Task.FromResult(pdf);
            }
        }
    }
}