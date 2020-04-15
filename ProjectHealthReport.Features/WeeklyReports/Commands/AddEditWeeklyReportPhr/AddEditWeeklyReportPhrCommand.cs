﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MessagePack;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Domains.Mappings;
using ProjectHealthReport.Features.Projects.Queries.GetProjectCaching;
using ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Commands.AddEditWeeklyReportPhr
{
    public class AddEditWeeklyReportPhrCommand : IRequest<int>, IMapFrom<object>
    {
        public GetWeeklyReportPhrQuery.Dto Report { get; set; }

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

            public override async Task HandleAsync(AddEditWeeklyReportPhrCommand request)
            {
                var currentYearWeek =
                    TimeHelper.CalculateYearWeek(request.Report.SelectedYear, request.Report.SelectedWeek);

                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var project = await _dbContext.Projects.AsNoTracking().FirstAsync(p => p.Id == request.Report.ProjectId);
                        var data = await _mediator.SendAsync(new GetProjectCachingQuery()
                        {
                            ProjectId = request.Report.ProjectId
                        });
                        
                        var cache = MessagePackSerializer.Deserialize<GetProjectCachingQuery.WeeklyData>(data.Bytes,
                            MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block));
                
                        project.SetCollections(_mapper.Map<IEnumerable<QualityReport>>(cache.QualityReports), 
                            _mapper.Map<IEnumerable<BacklogItem>>(cache.BacklogItems),
                            _mapper.Map<IEnumerable<Status>>(cache.Statuses));

                        _dbContext.Projects.Attach(project);

                        var s = request.Report.Status;
                        var status = new Status(s.Id, request.Report.ProjectId, s.StatusColor, s.ProjectStatus,
                            s.RetrospectiveFeedBack, s.MilestoneDate, s.Milestone, currentYearWeek);
                        project.AddEditStatus(status);

                        var i = request.Report.BacklogItem;
                        var item = new BacklogItem(i.Id, request.Report.ProjectId, i.Sprint, i.ItemsAdded,
                            i.StoryPointsAdded, i.ItemsDone, i.StoryPointsDone, currentYearWeek);
                        project.AddEditBacklogItem(item);

                        var q = request.Report.QualityReport;
                        var report = new QualityReport(q.Id, request.Report.ProjectId, q.CriticalBugs, q.MajorBugs,
                            q.MinorBugs, q.DoneBugs, q.ReOpenBugs, currentYearWeek);
                        project.AddEditQualityReport(report);

                        await _dbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        request.Response = 1;
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public int Response { get; set; }
    }
}