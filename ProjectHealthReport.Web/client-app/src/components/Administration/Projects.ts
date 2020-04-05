import moment from "moment";

export interface Project{
    id: number;
    name: string;
    code: string;
    division: string;
    keyAccountManager: string;
    kam: string;
    deliveryResponsibleName: string;
    pic: string;
    dodRequired: boolean;
    phrRequired: boolean;
    dmrRequired: boolean;
    dmrRequiredFrom: string;
    dmrRequiredTo: any;
    projectStateType: string;
    statuses: string[];
    currentStatuses: string[];
    projectStateTypeId: number;
}
export interface FilteredItem{
    field: any;
    value: any;
}
export interface Payload{
    projects: Project[];
}
export interface FormattedStatus{
    color: string;
    text: string;
    tooltip: string;
}

export const defaultFormattedStatus: FormattedStatus = {color: "", text: "", tooltip: ""};

export const defaultPayloadValue: Payload = {
    projects: [
        {
            id: 0,
            name: "",
            code: "",
            division: "",
            keyAccountManager: "",
            kam: "",
            deliveryResponsibleName: "",
            pic: "",
            phrRequired: false,
            dodRequired: false,
            projectStateType: "",
            statuses: [],
            currentStatuses: [],
            projectStateTypeId: 0,
            dmrRequired: true,
            dmrRequiredFrom: moment().format("YYYY-MM-DD"),
            dmrRequiredTo: null,
        }
    ]
};