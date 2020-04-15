export interface Report {
    projectId: number;
    projectName: string;
    dodRequired: boolean;
    status: Status;
    backlogItem: BacklogItem;
    qualityReport: QualityReport;
    dodRecords: DodRecord[];
    years: number[];
    weeks: number[];
    numberOfWeeks: number[];
    numberOfWeekNotShowClosedItems: number[];
    statuses: string[];
    additionalInfoStatues: string[];
    selectedYear: number;
    selectedWeek: number;
    numberOfWeek: number;
    numberOfWeekNotShowClosedItem: number;
    firstWorkingDateOfWeek: string;
    lastWorkingDateOfWeek: string;
    backlogItemListReadOnly: BacklogItem[];
    qualityReportListReadOnly: QualityReport[];
    metrics: DodMetric[];
}

export interface Status {
    id: number;
    statusColor: string;
    projectStatus: string;
    retrospectiveFeedBack: string;
    milestoneDate: string | null;
    milestone: string;
    week: number;
    year: number;
    yearWeek: number;
}

export interface BacklogItem {
    id: number;
    sprint: number | null;
    itemsAdded: number;
    storyPointsAdded: number | null;
    itemsDone: number;
    storyPointsDone: number | null;
    itemsRemaining: number;
    storyPointsRemaining: number;
    yearWeek: number;
    week: number;
    year: number;
}

export interface QualityReport {
    id: number;
    newBugs: number;
    criticalBugs: number;
    majorBugs: number;
    minorBugs: number;
    doneBugs: number;
    remainingBugs: number;
    reOpenBugs: number;
    yearWeek: number;
    week: number;
    year: number;
}

export interface AdditionalInfo {
    id: number;
    issueId: number;
    item: string;
    impact: string;
    action: string;
    openedYearWeek: number;
    status: string;
    week: number;
    year: number;
    yearWeek: number;
}

export interface Issue {
    id: number;
    issueId: number;
}

export interface Metric {
    id: number;
    name: string;
    valueType: string;
    unit: string;
    tool: string;
    selected: boolean;
    thresholds: Threshold[];
}

export interface MetricStatus {
    id: number;
    name: string;
    thresholds: Threshold[];
}

export interface Threshold {
    id: number;
    metricStatusId: number;
    metricId: number;
    upperBound: number | null;
    lowerBound: number | null;
    upperBoundOperator: string | null;
    lowerBoundOperator: string | null;
    isRange: boolean;
    value: string;
    metricValueType: string | null;
    metricStatusName: string;
}

export interface DodRecord{
    projectId: number;
    metricId: number;
    yearWeek: number;
    value: null | string;
    linkToReport: null | string;
    reportFileName: null | string;
}

export interface DodMetric {
    projectId: number;
    id: number;
    count: number;
    name: string;
    tool: string;
    toolOrder: number;
    order: number;
    linkToReport: string | null;
    reportFileName: string | null;
    unit: string | null;
    valueType: string;
    selectValues: string | null;
    value: string | null;
    yearWeek: number;
    selected: boolean;
    yearWeekValues: YearWeekValue[];
    thresholds: Threshold[];
}

export interface YearWeekValue {
    yearWeek: number;
    value: string | null;
    isEdited: boolean;
    class: string | null;
}

export interface DodLink{
    yearWeek: number;
    linkToReport: null | string;
    reportFileName: null | string;
}


export const defaultReport: Report = {
    projectId: 0,
    projectName: "",
    status: {
        id: 0,
        statusColor: "GREEN",
        projectStatus: "",
        retrospectiveFeedBack: "",
        milestoneDate: null,
        milestone: "",
        yearWeek: 0,
        week: 0,
        year: 0
    },
    backlogItem: {
        id: 0,
        sprint: null,
        itemsAdded: 0,
        storyPointsAdded: 0,
        itemsDone: 0,
        storyPointsDone: 0,
        itemsRemaining: 0,
        storyPointsRemaining: 0,
        yearWeek: 0,
        week: 0,
        year: 0
    },
    qualityReport: {
        id: 0,
        newBugs: 0,
        criticalBugs: 0,
        majorBugs: 0,
        minorBugs: 0,
        doneBugs: 0,
        remainingBugs: 0,
        reOpenBugs: 0,
        yearWeek: 0,
        week: 0,
        year: 0
    },
    dodRecords: [],
    dodRequired: false,
    years: [],
    weeks: [],
    numberOfWeeks: [],
    numberOfWeekNotShowClosedItems: [],
    statuses: [],
    additionalInfoStatues: [],
    selectedYear: 0,
    selectedWeek: 0,
    numberOfWeek: 0,
    numberOfWeekNotShowClosedItem: 0,
    firstWorkingDateOfWeek: "",
    lastWorkingDateOfWeek: "",
    backlogItemListReadOnly: [],
    qualityReportListReadOnly: [],
    metrics: []
}