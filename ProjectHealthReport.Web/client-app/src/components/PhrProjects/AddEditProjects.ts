export interface Project {
    id: number;
    name: string;
    code: string;
    division: string;
    keyAccountManager: string;
    deliveryResponsibleName: string;
    projectState: string;
    backlogItem: BacklogItem;
    projectAccesses: ProjectAccess[];
    platformVersion: string;
    jiraLink: string;
    sourceCodeLink: string;
    projectStartDate: string;
    projectEndDate: string | null;
    note: string;
}

interface BacklogItem {
    id: number;
    itemsAdded: number | null;
    storyPointsAdded: number | null;
}

export interface ProjectAccess {
    id: number;
    projectId: number;
    email: string;
    role: string;
}

export const defaultProject: Project = {
    id: 0,
    name: "",
    code: "",
    division: "",
    keyAccountManager: "",
    deliveryResponsibleName: "",
    projectState: "",
    platformVersion: "",
    jiraLink: "",
    sourceCodeLink: "",
    projectStartDate: "",
    projectEndDate: null,
    note: "",
    backlogItem: {
        id: 0,
        itemsAdded: null,
        storyPointsAdded: null
    },
    projectAccesses: []
}