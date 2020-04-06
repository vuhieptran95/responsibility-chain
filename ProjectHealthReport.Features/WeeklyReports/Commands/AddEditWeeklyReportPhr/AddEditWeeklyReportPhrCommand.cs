using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Common.Mappings;
using ProjectHealthReport.Features.DoDs.AddEditDoDReport;
using ProjectHealthReport.Features.WeeklyReports.Commands.AddEditAdditionalInfo;
using ProjectHealthReport.Features.WeeklyReports.Commands.AddEditBacklogItem;
using ProjectHealthReport.Features.WeeklyReports.Commands.AddEditQualityReport;
using ProjectHealthReport.Features.WeeklyReports.Commands.AddEditStatus;
using ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditWeeklyReportPhr
{
    public class AddEditWeeklyReportPhrCommand : IRequest<int>, IMapFrom<object>
    {
        public GetWeeklyReportPhrQuery.Dto Report { get; set; }
        public List<IssueRemovedId> IssueRemovedIds { get; set; }

        public class IssueRemovedId
        {
            public int Id { get; set; }
            public int IssueId { get; set; }
        }

        public class Handler : ExecutionHandlerBase<AddEditWeeklyReportPhrCommand, int>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public Handler(ReportDbContext dbContext, IMapper mapper, IMediator mediator)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _mediator = mediator;
            }

            public override async Task<int> HandleAsync(AddEditWeeklyReportPhrCommand request)
            {
                var currentYearWeek =
                    TimeHelper.CalculateYearWeek(request.Report.SelectedYear, request.Report.SelectedWeek);

                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        if (request.Report.Status.Id == 0)
                        {
                            var addStatusCommand = _mapper.Map<AddStatusCommand>(request.Report);
                            _mapper.Map(request.Report.Status, addStatusCommand);

                            await _mediator.SendAsync(addStatusCommand);
                        }
                        else
                        {
                            var editStatusCommand = _mapper.Map<EditStatusCommand>(request.Report);
                            _mapper.Map(request.Report.Status, editStatusCommand);

                            await _mediator.SendAsync(editStatusCommand);
                        }

                        if (request.Report.BacklogItem.Id == 0)
                        {
                            var addBacklogItemCommand = _mapper.Map<AddBacklogItemCommand>(request.Report);
                            _mapper.Map(request.Report.BacklogItem, addBacklogItemCommand);

                            await _mediator.SendAsync(addBacklogItemCommand);
                        }
                        else
                        {
                            var editBacklogItemCommand = _mapper.Map<EditBacklogItemCommand>(request.Report);
                            _mapper.Map(request.Report.BacklogItem, editBacklogItemCommand);

                            await _mediator.SendAsync(editBacklogItemCommand);
                        }

                        if (request.Report.QualityReport.Id == 0)
                        {
                            var addQualityReportCommand = _mapper.Map<AddQualityReportCommand>(request.Report);
                            _mapper.Map(request.Report.QualityReport, addQualityReportCommand);

                            await _mediator.SendAsync(addQualityReportCommand);
                        }
                        else
                        {
                            var editQualityReportCommand = _mapper.Map<EditQualityReportCommand>(request.Report);
                            _mapper.Map(request.Report.QualityReport, editQualityReportCommand);

                            await _mediator.SendAsync(editQualityReportCommand);
                        }

                        if (request.Report.DodRequired)
                        {
                            var dodReports = request.Report.DodRecords.Where(r => r.Value != null).ToList();

                            await _mediator.SendAsync(new EditDoDReportCommand()
                                {DodReports = _mapper.Map<List<EditDoDReportCommand.DoDReportDto>>(dodReports)});
                        }

                        foreach (var ai in request.Report.AdditionalInfos)
                        {
                            if (ai.Id == 0 || ai.YearWeek != currentYearWeek)
                            {
                                var addCommand = _mapper.Map<AddAdditionalInfoCommand>(request.Report);
                                _mapper.Map(ai, addCommand);

                                await _mediator.SendAsync(addCommand);
                            }
                            else
                            {
                                var editCommand = _mapper.Map<EditAdditionalInfoCommand>(request.Report);
                                _mapper.Map(ai, editCommand);

                                await _mediator.SendAsync(editCommand);
                            }
                        }

                        foreach (var issueIds in request.IssueRemovedIds)
                        {
                            var removeCommand = new RemoveAdditionalInfoCommand()
                            {
                                Id = issueIds.Id,
                                IssueId = issueIds.IssueId
                            };

                            await _mediator.SendAsync(removeCommand);
                        }

                        await transaction.CommitAsync();

                        return 1;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto.DoDReportDto, EditDoDReportCommand.DoDReportDto>();
            
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto, AddStatusCommand>()
                .ForMember(des => des.YearWeek,
                    opt => opt.MapFrom(src => TimeHelper.CalculateYearWeek(src.SelectedYear, src.SelectedWeek)));
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto.StatusDto, AddStatusCommand>();
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto, EditStatusCommand>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForMember(des => des.YearWeek,
                    opt => opt.MapFrom(src => TimeHelper.CalculateYearWeek(src.SelectedYear, src.SelectedWeek)));
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto.StatusDto, EditStatusCommand>();

            profile.CreateMap<GetWeeklyReportPhrQuery.Dto, AddBacklogItemCommand>()
                .ForMember(des => des.YearWeek,
                    opt => opt.MapFrom(src => TimeHelper.CalculateYearWeek(src.SelectedYear, src.SelectedWeek)));
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto.BacklogItemDto, AddBacklogItemCommand>();
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto, EditBacklogItemCommand>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForMember(des => des.YearWeek,
                    opt => opt.MapFrom(src => TimeHelper.CalculateYearWeek(src.SelectedYear, src.SelectedWeek)));
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto.BacklogItemDto, EditBacklogItemCommand>();

            profile.CreateMap<GetWeeklyReportPhrQuery.Dto, AddQualityReportCommand>()
                .ForMember(des => des.YearWeek,
                    opt => opt.MapFrom(src => TimeHelper.CalculateYearWeek(src.SelectedYear, src.SelectedWeek)));
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto.QualityReportDto, AddQualityReportCommand>();
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto, EditQualityReportCommand>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForMember(des => des.YearWeek,
                    opt => opt.MapFrom(src => TimeHelper.CalculateYearWeek(src.SelectedYear, src.SelectedWeek)));
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto.QualityReportDto, EditQualityReportCommand>();

            profile.CreateMap<GetWeeklyReportPhrQuery.Dto, AddAdditionalInfoCommand>()
                .ForMember(des => des.YearWeek,
                    opt => opt.MapFrom(src => TimeHelper.CalculateYearWeek(src.SelectedYear, src.SelectedWeek)));
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto.AdditionalInfoDto, AddAdditionalInfoCommand>();
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto, EditAdditionalInfoCommand>()
                .ForMember(des => des.Id, opt => opt.Ignore())
                .ForMember(des => des.YearWeek,
                    opt => opt.MapFrom(src => TimeHelper.CalculateYearWeek(src.SelectedYear, src.SelectedWeek)));
            profile.CreateMap<GetWeeklyReportPhrQuery.Dto.AdditionalInfoDto, EditAdditionalInfoCommand>();
        }
    }
}