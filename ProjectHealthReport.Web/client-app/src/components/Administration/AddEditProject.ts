import moment from "moment";

export interface Project{
    id: number;
    name: string;
    code: string;
    division: string;
    phrRequired: boolean;
    dmrRequired: boolean;
    dodRequired: boolean;
    dmrRequiredFrom: string;
    dmrRequiredTo: any;
    projectStateTypeId: number;
    keyAccountManager: string;
    deliveryResponsibleName?: string;
    projectStartDate: string;
    projectEndDate: any;
}

export const defaultProjectValue: Project = {
    dodRequired: false,
    id: 0,
    name: "",
    code: "",
    division: "",
    phrRequired: false,
    dmrRequired: true,
    dmrRequiredFrom: moment().format("YYYY-MM-DD"),
    dmrRequiredTo: null,
    projectStateTypeId: 0,
    keyAccountManager: "",
    deliveryResponsibleName: undefined,
    projectStartDate: "",
    projectEndDate: null
};
