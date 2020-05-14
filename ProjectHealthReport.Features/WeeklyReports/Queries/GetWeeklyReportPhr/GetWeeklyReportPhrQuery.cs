using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MessagePack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ProjectHealthReport.Features.Projects.Queries.GetProjectCaching;
using ResponsibilityChain;
using ResponsibilityChain.Business;
using ResponsibilityChain.Business.Executions;
using ResponsibilityChain.Business.RequestContexts;

namespace ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr
{
    public partial class GetWeeklyReportPhrQuery : Request<GetWeeklyReportPhrQuery.Dto>
    {
        public int ProjectId { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public int NumberOfWeek { get; set; } = 4;
        public int NumberOfWeekNotShowClosedItem { get; set; } = 2;

        public Expression<Func<Project, bool>> ResourceFilter { get; set; } = p => true;

        public class Handler : IExecution<GetWeeklyReportPhrQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly IMemoryCache _cache;

            public Handler(ReportDbContext dbContext, IMapper mapper, IMediator mediator, IMemoryCache cache)
            {
                _dbContext = dbContext;
                _mapper = mapper;
                _mediator = mediator;
                _cache = cache;
            }

            public async Task HandleAsync(GetWeeklyReportPhrQuery request)
            {
                var yearWeek = TimeHelper.CalculateYearWeek(request.Year, request.Week);

                var project = await _dbContext.Projects.AsNoTracking().Where(request.ResourceFilter).FirstAsync(p => p.Id == request.ProjectId);

                var cache = (await _mediator.SendAsync(new GetProjectCachingQuery()
                {
                    ProjectId = request.ProjectId,
                })).GetResponse();

                project.SetCollections(_mapper.Map<IEnumerable<QualityReport>>(cache.QualityReports),
                    _mapper.Map<IEnumerable<BacklogItem>>(cache.BacklogItems),
                    _mapper.Map<IEnumerable<Status>>(cache.Statuses));

                var listProject = new List<Project> {project};

                var dto = listProject
                    .Select(p => new Dto
                    {
                        ProjectId = p.Id,
                        ProjectName = p.Name,
                        DodRequired = p.DodRequired,
                        SelectedWeek = request.Week,
                        SelectedYear = request.Year,
                        NumberOfWeek = request.NumberOfWeek,
                        NumberOfWeekNotShowClosedItem = request.NumberOfWeekNotShowClosedItem,
                        Status = p.Statuses.Where(s => s.YearWeek == yearWeek)
                            .Select(s => _mapper.Map<Dto.StatusDto>(s))
                            .FirstOrDefault(),
                        BacklogItem = p.BacklogItems.Where(b => b.YearWeek == yearWeek)
                            .Select(b => _mapper.Map<Dto.BacklogItemDto>(b))
                            .FirstOrDefault(),
                        BacklogItemListReadOnly = p.BacklogItems
                            .Where(x => x.YearWeek < yearWeek)
                            .OrderBy(x => x.YearWeek)
                            .Select(b => _mapper.Map<Dto.BacklogItemDto>(b)).ToList(),
                        QualityReport = p.QualityReports.Where(q => q.YearWeek == yearWeek)
                            .Select(q => _mapper.Map<Dto.QualityReportDto>(q))
                            .FirstOrDefault(),
                        QualityReportListReadOnly = p.QualityReports
                            .Where(x => x.YearWeek < yearWeek)
                            .OrderBy(x => x.YearWeek)
                            .Select(q => _mapper.Map<Dto.QualityReportDto>(q)).ToList(),
                    })
                    .Single();

                if (dto.BacklogItemListReadOnly.Count > 0)
                {
                    GetWeeklyReportPhrHelper.CalculateBacklogItemsRemaining(dto.BacklogItemListReadOnly);

                    var initialRecord = dto.BacklogItemListReadOnly.Take(1).FirstOrDefault();
                    var firstWeekRecord = dto.BacklogItemListReadOnly.Skip(1).Take(1).FirstOrDefault();

                    var listItemsSatisfied = dto.BacklogItemListReadOnly.TakeLast(dto.NumberOfWeek).ToList();
                    if (listItemsSatisfied.Contains(firstWeekRecord) && !listItemsSatisfied.Contains(initialRecord))
                    {
                        listItemsSatisfied.Insert(0, initialRecord);
                    }

                    dto.BacklogItemListReadOnly = listItemsSatisfied.Distinct().ToList();
                }

                if (dto.QualityReportListReadOnly.Count > 0)
                {
                    GetWeeklyReportPhrHelper.CalculateNewAndRemainingBugs(dto.QualityReportListReadOnly);

                    dto.QualityReportListReadOnly =
                        dto.QualityReportListReadOnly.TakeLast(dto.NumberOfWeek).ToList();
                }

                var yearWeeksToGetDod =
                    TimeHelper.GetYearWeeksOfXRecentWeeksStartFrom(request.Year, request.Week, request.NumberOfWeek);
                yearWeeksToGetDod.Add(yearWeek);

                var listMetricInDb = await _dbContext.DoDReports
                    .Include(r => r.Metric)
                    .Where(r => r.ProjectId == request.ProjectId && yearWeeksToGetDod.Contains(r.YearWeek))
                    .Select(r => new Dto.MetricDto()
                    {
                       ProjectId  = r.ProjectId,
                       Id = r.MetricId,
                       Name = r.Metric.Name,
                       Tool = r.Metric.Tool,
                       ToolOrder = r.Metric.ToolOrder,
                       Order = r.Metric.Order,
                       Unit = r.Metric.Unit,
                       ValueType = r.Metric.ValueType,
                       Value = r.Value,
                       SelectValues = r.Metric.SelectValues,
                       YearWeek = r.YearWeek,
                    })
                    .ToListAsync();

                var metrics = listMetricInDb
                    .GroupBy(m => new
                    {
                        m.Id, m.Name, m.Order, m.SelectValues, m.Unit, m.ProjectId, m.ValueType, m.Tool, m.ToolOrder
                    }).Select(g => new Dto.MetricDto()
                    {
                        Id = g.Key.Id,
                        Name = g.Key.Name,
                        Order = g.Key.Order,
                        Tool = g.Key.Tool,
                        ToolOrder = g.Key.ToolOrder,
                        ValueType = g.Key.ValueType,
                        SelectValues = g.Key.SelectValues,
                        Unit = g.Key.Unit,
                        ProjectId = g.Key.ProjectId,
                        YearWeekValues = g.Select(m => new Dto.YearWeekValue()
                            {YearWeek = m.YearWeek, Value = m.Value}).ToList()
                    })
                    .OrderBy(m => m.ToolOrder)
                    .ThenBy(m => m.Order)
                    .ToList();

                var toolGroups = metrics.GroupBy(m => m.Tool).Select(g => new
                {
                    Tool = g.Key, Count = g.Count(), Metrics = g.ToList()
                }).ToList();

                toolGroups.ForEach(tg =>
                {
                    tg.Metrics.ForEach(m =>
                    {
                        if (tg.Metrics.IndexOf(m) == 0)
                        {
                            m.Count = tg.Count;
                        }

                        var listMetricsYearWeek = m.YearWeekValues.Select(ywv => ywv.YearWeek);
                        var listYearWeekValuesToAdd = yearWeeksToGetDod.Except(listMetricsYearWeek).Select(yw =>
                            new Dto.YearWeekValue
                            {
                                YearWeek = yw,
                                Value = null
                            });
                        m.YearWeekValues.AddRange(listYearWeekValuesToAdd);
                        m.YearWeekValues = m.YearWeekValues.OrderByDescending(ywv => ywv.YearWeek).ToList();
                    });
                });

                dto.Metrics = toolGroups.SelectMany(t => t.Metrics, (t, m) => m).ToList();

                request.Response = dto;
            }
        }
    }
}