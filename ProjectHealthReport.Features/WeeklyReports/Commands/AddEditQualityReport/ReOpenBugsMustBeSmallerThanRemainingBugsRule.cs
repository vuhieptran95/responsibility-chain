// using System;
// using System.ComponentModel.DataAnnotations;
// using System.Threading.Tasks;
//
// namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditQualityReport
// {
//     public class ReOpenBugsMustBeSmallerThanRemainingBugsRule :
//         IAsyncHandler<AddQualityReportCommand, AddEditQualityReportDto>,
//         IAsyncHandler<EditQualityReportCommand, AddEditQualityReportDto>
//     {
//         public Task<AddEditQualityReportDto> HandleAsync(AddQualityReportCommand command, Func<AddQualityReportCommand, Task<AddEditQualityReportDto>> next)
//         {
//             ValidateReOpenBugsAgainstRemainingBugs(command.ReOpenBugs, command.RemainingBugs);
//
//             return next(command);
//         }
//
//         public Task<AddEditQualityReportDto> HandleAsync(EditQualityReportCommand command, Func<EditQualityReportCommand, Task<AddEditQualityReportDto>> next)
//         {
//             ValidateReOpenBugsAgainstRemainingBugs(command.ReOpenBugs, command.RemainingBugs);
//
//             return next(command);
//         }
//
//         private static void ValidateReOpenBugsAgainstRemainingBugs(int reOpenBugs, int remainingBugs)
//         {
//             if (reOpenBugs > remainingBugs)
//             {
//                 throw new ValidationException("Re-open bugs must be smaller than Remaining bugs");
//             }
//         }
//     }
// }
