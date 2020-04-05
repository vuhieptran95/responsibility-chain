export interface User{
    username: string;
    email: string;
    role: string;
    division: string;
}

export const defaultUser: User= {
    email: "",
    username: "",
    role: "",
    division: ""
};