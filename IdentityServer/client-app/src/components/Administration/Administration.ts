export interface User {
    role: string;
    username: string;
    policies: Policy[];
    scopes: string[];
}

export interface Policy {
    id: string;
    name: string;
}