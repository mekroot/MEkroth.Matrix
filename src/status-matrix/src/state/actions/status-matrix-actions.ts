import { StatusMatrixModel } from "../../status-matrix/models/status-matrix";

export const CHANGE_STATUS = 'change';
export const CLEAR_ERRORS = 'clear errors';
export const SHOW_ERRORS = 'show errors';
export const CLEAR_NAME = 'clear name';
export const CREATE_NEW_STATUS_MATRIX = 'create new status matrix';
export const DELETE_STATUS_MATRIX_SUCCESS = 'delete status matrix success';
export const DELETE_STATUS_MATRIX_FAILURE = 'delete status matrix failure';
export const EDIT_STATUS_MATRIX= 'edit';
export const LOAD_STATUS_MATRICES_SUCCESS = 'load status matrices success';
export const LOAD_STATUS_MATRICES_FAILURE = 'load status matrices failure';
export const SAVED_STATUS_MATRIX_SUCCESS = 'saved status matrix success';
export const SAVED_STATUS_MATRIX_FAILURE = 'saved status matrix failure';


export type ChangeStatus = { type: 'change', position: number }
export type CreateNewStatusMatrix = { type: 'create new status matrix' }

export type DeleteStatusMatrixSuccess = { type: 'delete status matrix success', id: string }
export type DeleteStatusMatrixFailure = { type: 'delete status matrix failure', error: string }

export type EditStatusMatrix = { type: 'edit', selected: StatusMatrixModel }

export type LoadStatusMatricesFailure = { type: 'load status matrices failure', error: string }
export type LoadStatusMatricesSuccess = { type: 'load status matrices success', statusMatrices: StatusMatrixModel[] }

export type SavedStatusMatrixSuccess = { type: 'saved status matrix success', saved: StatusMatrixModel }
export type SavedStatusMatrixFailure = { type: 'saved status matrix failure', error: string }

export type ClearName = { type: 'clear name' }
export type ClearErrors = { type: 'clear errors' }
export type ShowErrors = { type: 'show errors', error: string }
