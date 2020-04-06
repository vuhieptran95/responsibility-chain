export interface Threshold {
    metricStatusId: number;
    metricId: number;
    upperBound?: number;
    lowerBound?: number;
    upperBoundOperator?: string;
    lowerBoundOperator?: string;
    isRange: boolean;
    value: string | null;
    metricStatusName: string;
}

export interface Metric {
    id: number;
    name: string;
    valueType: string;
    unit: string;
    order: number;
    tool: string;
    toolOrder: number;
    selectValues: string;
    thresholds: Threshold[];
}

export interface MetricsGroup {
    tool: string;
    toolOrder: number;
    metrics: Metric[];
}

export const defaultThreshold: Threshold = {
    metricStatusId: 0,
    metricId: 0,
    upperBound: undefined,
    lowerBound: undefined,
    upperBoundOperator: undefined,
    lowerBoundOperator: undefined,
    isRange: true,
    value: null,
    metricStatusName: "",
}

export const defaultMetric: Metric = {
    id: 0,
    name: "",
    valueType: "",
    unit: "",
    order: 0,
    tool: "",
    toolOrder: 0,
    selectValues: "",
    thresholds: [],
}



