import { StatusMatrixModel, StatusModel } from '../../status-matrix/models/status-matrix';
import { CHANGE_STATUS, CLEAR_ERRORS, CREATE_NEW_STATUS_MATRIX, ChangeStatus, DELETE_STATUS_MATRIX_FAILURE, DELETE_STATUS_MATRIX_SUCCESS, DeleteStatusMatrixSuccess, EDIT_STATUS_MATRIX, LOAD_STATUS_MATRICES_FAILURE, LOAD_STATUS_MATRICES_SUCCESS, LoadStatusMatricesSuccess, SAVED_STATUS_MATRIX_FAILURE, SAVED_STATUS_MATRIX_SUCCESS, SHOW_ERRORS } from './status-matrix-actions';
import { IActionHandler } from '@aurelia/state';
import { State } from '../state';

export function editStatusMatrixHandler(currentState, action) : IActionHandler<State> {
    console.log("edit", action);
    return action.type === EDIT_STATUS_MATRIX ? { ...currentState, currentSelected: action.selected, name: action.selected.name } : currentState;
}

export function clearName(currentState, action): IActionHandler<State> {
    return action.type === CLEAR_ERRORS ? { ...currentState, name: '' } : currentState;
}

export function clearError(currentState, action): IActionHandler<State> {
    return action.type === CLEAR_ERRORS ? { ...currentState, error: '' } : currentState;
}

export function showError(currentState, action): IActionHandler<State> {
    return action.type === SHOW_ERRORS ? { ...currentState, error: action.error } : currentState;
}

export function createdNewStatusMatrixHandler(currentState, action): IActionHandler<State>
{
    return action.type === CREATE_NEW_STATUS_MATRIX ? { ...currentState, currentSelected: emptyStatusMatrixModel }: currentState;
}

export function deleteStatusMatrixSuccessHandler(currentState, action): IActionHandler<State> {
    if(action.type !== DELETE_STATUS_MATRIX_SUCCESS) 
    {
        return currentState;
    }
    
    let deleteAction = <DeleteStatusMatrixSuccess>action;
    let removedId = deleteAction.id.replace('\"', '');

    for(var i = 0; i <  currentState.statusMatrices.length; i++){ 
    
        if (currentState.statusMatrices[i].id === removedId) { 
            currentState.statusMatrices.splice(i, 1); 
        }
    }

    return { ...currentState, statusMatrices: [...currentState.statusMatrices] };
}

export function deleteStatusMatrixFailureHandler(currentState, action): IActionHandler<State> {
    return action.type === DELETE_STATUS_MATRIX_FAILURE ? { ...currentState, error: action.error } : currentState;
}

export function savedStatusMatrixSuccessHandler(currentState, action): IActionHandler<State> {
    if (action.type === SAVED_STATUS_MATRIX_SUCCESS) {
        let currentIndex = currentState.statusMatrices.findIndex(sm => sm.id === action.saved.id);
    
        if(currentIndex > -1) 
        {
            let newStatusMatrices = {...currentState.statusMatrices };
            newStatusMatrices[currentIndex] = action.saved;
            return {...currentState, statusMatrices: newStatusMatrices };
        }

        let newArray = [...currentState.statusMatrices, <StatusMatrixModel>action.saved];

        return{...currentState, statusMatrices: newArray }
    }
    return currentState;
}

export function savedStatusMatrixFailureHandler(currentState, action): IActionHandler<State> {
    return action.type === SAVED_STATUS_MATRIX_FAILURE ? { ...currentState, error: action.error } : currentState;
}

export function changeStatusHandler(currentState, action): IActionHandler<State> {
    if (action.type !== CHANGE_STATUS) {
        return currentState;
    }

    let currentAction = <ChangeStatus>action;
    let currentStatusMatrix = <StatusMatrixModel>currentState.currentSelected;
    let statuses = [...currentStatusMatrix.statuses];

    if (currentStatusMatrix.statuses[currentAction.position].status == 3) {
        statuses[currentAction.position] = getStatusViewModel(0); 
    } else {
        let status = currentStatusMatrix.statuses[currentAction.position].status + 1;
        statuses[currentAction.position] = getStatusViewModel(status);
    }

    currentStatusMatrix.statuses = statuses;
    return {...currentState, currentSelected: <StatusMatrixModel>currentStatusMatrix }
}

export function loadedStatusMatrixSuccessHandler(currentState, action): IActionHandler<State> {
    if(action.type !== LOAD_STATUS_MATRICES_SUCCESS) {
        return currentState;
    }

    let loadAction = <LoadStatusMatricesSuccess>action;
    let currentStatusMatrix = <StatusMatrixModel[]>loadAction.statusMatrices;

    return { ...currentState, statusMatrices: currentStatusMatrix };   
}

export function loadedStatusMatrixFailureHandler(currentState, action): IActionHandler<State> {
    return action.type === LOAD_STATUS_MATRICES_FAILURE ? { ...currentState, error: action.error } : currentState;   
}

export const emptyStatusMatrixModel: StatusMatrixModel = {
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
}


function getStatusViewModel(status: number) : StatusModel
{
    switch(status) 
    {
        case 0:
            return { status: 0, name: 'none', displayName: 'None' };
        case 1:
            return { status: 1, name: 'ok', displayName: 'OK'};
        case 2: 
            return { status: 2, name: 'warning', displayName: 'Warning'};
        case 3:
            return { status: 3, count: 5, name: 'error', displayName: 'Error'};
        default:
            throw new Error("Invalid status");
    }
}


