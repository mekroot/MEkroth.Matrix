import { StatusMatrixModel } from "../status-matrix/models/status-matrix";

export interface State {
    currentSelected: StatusMatrixModel
    statusMatrices: StatusMatrixModel[]
    error: string;
}