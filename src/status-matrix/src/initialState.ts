import { State } from "./state/state";
import { StatusMatrixModel } from "./status-matrix/models/status-matrix";

export const initialState: State = {
    currentSelected: {
        id: '00000000-0000-0000-0000-000000000000',
        name: 'New Status Matrix',
        statuses: [
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
            { status: 0, name: 'none', displayName: 'None' },
        ],
    },
    statusMatrices: [],
    error: ''
}
