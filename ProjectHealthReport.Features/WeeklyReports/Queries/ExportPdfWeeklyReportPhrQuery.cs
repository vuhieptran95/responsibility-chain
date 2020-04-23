using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.WeeklyReports.Queries
{
    public class ExportPdfWeeklyReportPhrQuery : Request<byte[]>
    {
        public string HtmlContent { get; set; }

        public class Handler : IExecution<ExportPdfWeeklyReportPhrQuery, byte[]>
        {
            private readonly IConverter _converter;

            public Handler(IConverter converter)
            {
                _converter = converter;
            }

            public Task HandleAsync(ExportPdfWeeklyReportPhrQuery request)
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

                request.Response = (pdf);
                
                return Task.CompletedTask;
            }
        }
    }
}