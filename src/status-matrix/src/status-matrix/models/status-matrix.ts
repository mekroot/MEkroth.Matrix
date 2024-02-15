export interface StatusMatrixModel {
    id: string,
    name: string,
    statuses: StatusModel[],
}

export interface StatusModel {
    status: number, 
    name: string, 
    displayName: string,
    count?: number,
}

