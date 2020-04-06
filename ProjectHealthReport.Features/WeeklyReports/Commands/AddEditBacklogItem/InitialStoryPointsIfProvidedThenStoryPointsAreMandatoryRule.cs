// using System;
// using System.ComponentModel.DataAnnotations;
// using System.Threading.Tasks;
//
// namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditBacklogItem
// {
//     public class InitialStoryPointsIfProvidedThenStoryPointsAreMandatoryRule : IAsyncHandler<AddBacklogItemCommand, AddEditBacklogItemDto>, IAsyncHandler<EditBacklogItemCommand, AddEditBacklogItemDto>
//     {
//         private readonly ReportDbContext _dbContext;
//
//         public InitialStoryPointsIfProvidedThenStoryPointsAreMandatoryRule(ReportDbContext dbContext)
//         {
//             _dbContext = dbContext;
//         }
//
//         public async Task<AddEditBacklogItemDto> HandleAsync(AddBacklogItemCommand command, Func<AddBacklogItemCommand, Task<AddEditBacklogItemDto>> next)
//         {
//             await ValidateStoryPointBasedOnInitialStoryPoint(command);
//
//             return await next(command);
//         }
//
//         public async Task<AddEditBacklogItemDto> HandleAsync(EditBacklogItemCommand command, Func<EditBacklogItemCommand, Task<AddEditBacklogItemDto>> next)
//         {
//             await ValidateStoryPointBasedOnInitialStoryPoint(command);
//
//             return await next(command);
//         }
//
//         private async Task ValidateStoryPointBasedOnInitialStoryPoint(object command)
//         {
//             int projectId = 0;
//             int? storyPointsAdded = null;
//             int? storyPointsDone = null;
//
//             if (command is AddBacklogItemCommand)
//             {
//                 var addCommand = (AddBacklogItemCommand)command;
//
//                 projectId = addCommand.ProjectId;
//                 storyPointsAdded = addCommand.StoryPointsAdded;
//                 storyPointsDone = addCommand.StoryPointsDone;
//             }
//
//             if (command is EditBacklogItemCommand)
//             {
//                 var editCommand = (EditBacklogItemCommand)command;
//                 projectId = editCommand.ProjectId;
//                 storyPointsAdded = editCommand.StoryPointsAdded;
//                 storyPointsDone = editCommand.StoryPointsDone;
//             }
//
//             var initialBacklogItems = await _dbContext.BacklogItems.FirstOrDefaultAsync(b => (b.YearWeek % 100) == 0 && b.ProjectId == projectId);
//             if (initialBacklogItems.StoryPointsAdded != null)
//             {
//                 if (!storyPointsAdded.HasValue || !storyPointsDone.HasValue)
//                 {
//                     throw new ValidationException("If Story Points are provided at the Initializing phase, all fields which are related to story points must be filled!");
//                 }
//             }
//         }
//     }
// }
