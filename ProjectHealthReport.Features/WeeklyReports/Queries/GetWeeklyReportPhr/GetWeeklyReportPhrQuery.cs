using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHealthReport.Domains.Domains;
using ProjectHealthReport.Domains.Helpers;
using ResponsibilityChain;
using ResponsibilityChain.Business.Executions;

namespace ProjectHealthReport.Features.WeeklyReports.Queries.GetWeeklyReportPhr
{
    public partial class GetWeeklyReportPhrQuery : IRequest<GetWeeklyReportPhrQuery.Dto>
    {
        public int ProjectId { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public int NumberOfWeek { get; set; } = 4;
        public int NumberOfWeekNotShowClosedItem { get; set; } = 2;

        public Expression<Func<Project, bool>> ResourceFilter { get; set; } = p => true;

        public class Handler : ExecutionHandlerBase<GetWeeklyReportPhrQuery, Dto>
        {
            private readonly ReportDbContext _dbContext;
            private readonly IMapper _mapper;

            public Handler(ReportDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public override async Task<Dto> HandleAsync(GetWeeklyReportPhrQuery request)
            {
                var yearWeek = TimeHelper.CalculateYearWeek(request.Year, request.Week);
                var yearWeekToDisplayClosedItems =
                    TimeHelper.GetYearWeeksOfXRecentWeeksStartFrom(request.Year, request.Week,
                        request.NumberOfWeekNotShowClosedItem);
                var lastWeek = TimeHelper.GetYearWeeksOfXRecentWeeksStartFrom(request.Year, request.Week, 1)
                    .FirstOrDefault();

                var dto = await _dbContext.Projects
                    .Where(request.ResourceFilter)
                    .Where(p => p.Id == request.ProjectId)
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
                        AdditionalInfos = p.AdditionalInfos
                            .Where(a => a.YearWeek == yearWeek)
                            .OrderBy(a => a.YearWeek)
                            .SelectMany(a => a.AdditionalInfoIssues, (a, i) => new Dto.AdditionalInfoDto
                            {
                                Id = a.Id,
                                YearWeek = a.YearWeek,
                                Action = i.Issue.Action,
                                Impact = i.Issue.Impact,
                                Item = i.Issue.Item,
                                IssueId = i.Issue.Id,
                                Status = i.Status,
                                OpenedYearWeek = i.Issue.OpenedYearWeek,
                            }).ToList(),
                        AdditionalInfoListWithEditableStatus = p.AdditionalInfos
                            .Where(a => a.YearWeek == lastWeek)
                            .OrderBy(a => a.YearWeek)
                            .SelectMany(a => a.AdditionalInfoIssues, (a, i) => new Dto.AdditionalInfoDto
                            {
                                Id = a.Id,
                                YearWeek = a.YearWeek,
                                Action = i.Issue.Action,
                                Impact = i.Issue.Impact,
                                Item = i.Issue.Item,
                                IssueId = i.Issue.Id,
                                Status = i.Status,
                                OpenedYearWeek = i.Issue.OpenedYearWeek
                            })
                            .Where(a => a.Status == "Open")
                            .ToList(),
                        AdditionalInfoListReadOnly = p.AdditionalInfos
                            .Where(x => yearWeekToDisplayClosedItems.Contains(x.YearWeek))
                            .OrderBy(x => x.YearWeek)
                            .SelectMany(a => a.AdditionalInfoIssues, (a, i) => new Dto.AdditionalInfoDto
                            {
                                Id = a.Id,
                                YearWeek = a.YearWeek,
                                Action = i.Issue.Action,
                                Impact = i.Issue.Impact,
                                Item = i.Issue.Item,
                                IssueId = i.Issue.Id,
                                Status = i.Status,
                                OpenedYearWeek = i.Issue.OpenedYearWeek
                            })
                            .Where(a => a.Status == "Closed")
                            .ToList()
                    })
                    .SingleAsync();
                
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

                GetWeeklyReportPhrHelper.PopulateAdditionalInfoItemsOfThisWeekBasedOnLastWeek(dto);

                var yearWeeksToGetDod =
                    TimeHelper.GetYearWeeksOfXRecentWeeksStartFrom(request.Year, request.Week, request.NumberOfWeek);
                yearWeeksToGetDod.Add(yearWeek);

                var listMetricInDb = await _dbContext.DoDReports
                    .Include(r => r.Metric)
                    .Where(r => r.ProjectId == request.ProjectId && yearWeeksToGetDod.Contains(r.YearWeek))
                    .Select(r => _mapper.Map<Dto.MetricDto>(r))
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

                return dto;
            }
        }
    }
}