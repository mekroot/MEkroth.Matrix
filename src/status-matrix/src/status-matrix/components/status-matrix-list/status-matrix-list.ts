import { StatusMatrixModel } from './../../models/status-matrix';
import { DELETE_STATUS_MATRIX_FAILURE, DELETE_STATUS_MATRIX_SUCCESS, SAVED_STATUS_MATRIX_SUCCESS, SAVED_STATUS_MATRIX_FAILURE, CREATE_NEW_STATUS_MATRIX, CreateNewStatusMatrix, CLEAR_ERRORS, ClearName, ClearErrors, SHOW_ERRORS, ShowErrors, CLEAR_NAME } from './../../../state/actions/status-matrix-actions';
import { IStore, fromState } from "@aurelia/state";
import { HttpClient, IHttpClient } from "@aurelia/fetch-client";
import { State } from "../../../state/state";
import { DeleteStatusMatrixFailure, DeleteStatusMatrixSuccess, EDIT_STATUS_MATRIX, EditStatusMatrix, LOAD_STATUS_MATRICES_FAILURE, LOAD_STATUS_MATRICES_SUCCESS, LoadStatusMatricesFailure, LoadStatusMatricesSuccess, SavedStatusMatrixFailure, SavedStatusMatrixSuccess } from "../../../state/actions/status-matrix-actions";
import { ICustomElementViewModel } from 'aurelia';
export class StatusMatrixList implements ICustomElementViewModel {

    @fromState((state: State) => state.error)
    error: string = '';
    
    @fromState((state: State) => state.currentSelected.name)
    name: string = '';

    @fromState((state: State) => state.statusMatrices)
    statusMatrices: StatusMatrixModel[] = [];

    @fromState((state: State) => state.currentSelected)
    currentSelected: StatusMatrixModel = { id: '', name: '', statuses: [] };

    constructor(
        @IHttpClient private http: HttpClient,
        @IStore private store: IStore<{}, SavedStatusMatrixSuccess 
            | SavedStatusMatrixFailure 
            | DeleteStatusMatrixSuccess 
            | DeleteStatusMatrixFailure
            | EditStatusMatrix 
            | LoadStatusMatricesSuccess 
            | LoadStatusMatricesFailure 
            | CreateNewStatusMatrix 
            | ClearName | ClearErrors | ShowErrors>) {

        http.configure(config =>
            config.withInterceptor({
                response(response) {
                    if (!response.ok) {
                        handleError(response);
                    }
                    return response;
                },
                responseError(error) {
                    handleError(error);
                    throw error; // Rethrow error after handling
                }
            })
                .withDefaults({ credentials: 'same-origin', })
                .withBaseUrl('https://localhost:44396/api/'));
    }

    showWarning = this.statusMatrices.length === 0;

    showError = this.error !== '';
    
    /// Calls when the component are created
    created() {
        this.store.dispatch({ type: CLEAR_ERRORS });
        this.http.fetch('status-matrices')
            .then((response) => {
                return response.json();
            })
            .then((statusMatrices) => {
                this.showWarning = statusMatrices.length === 0;
                this.store.dispatch({ type: LOAD_STATUS_MATRICES_SUCCESS, statusMatrices: statusMatrices })
            })
            .catch((error) => { console.error(error); this.store.dispatch({ type: LOAD_STATUS_MATRICES_FAILURE, error: error }) });
    }

    delete(id: string) {
        if(this.currentSelected.id === id) {
            this.store.dispatch({type: CREATE_NEW_STATUS_MATRIX });
        }

        this.store.dispatch({ type: CLEAR_ERRORS });
        this.http.delete('status-matrices/' + id)
            .then((response) => response.json())
            .then((statusMatrixId) => {
                this.store.dispatch({ type: DELETE_STATUS_MATRIX_SUCCESS, id: statusMatrixId })
            })
            .catch((error) => { console.error(error); this.store.dispatch({ type: DELETE_STATUS_MATRIX_FAILURE, error: error }) });
    }

    edit(id: string) {
        this.store.dispatch({ type: CLEAR_ERRORS });

        this.http.fetch('status-matrices/' + id)
            .then((response) => response.json())
            .then((statusMatrix) => {
                this.store.dispatch({ type: EDIT_STATUS_MATRIX, selected: statusMatrix })
            })
            .catch((error) => { console.error(error); this.store.dispatch({ type: LOAD_STATUS_MATRICES_FAILURE, error: error }) });
    }

    undo() {
        this.store.dispatch({ type: CLEAR_ERRORS });
        this.store.dispatch({ type: CLEAR_NAME });
    }

    save() {
        if(this.name === '') {
            this.error = 'Name can not be empty';
            return;
        }
        this.store.dispatch({ type: CLEAR_ERRORS });
        let saveStatusMatrix = { ...this.currentSelected };
        
        saveStatusMatrix.id = this.currentSelected.id;
        saveStatusMatrix.name = this.name;
        saveStatusMatrix.statuses = this.currentSelected.statuses;

        console.log(saveStatusMatrix);

        this.http.fetch('status-matrices', { method: 'post', body: JSON.stringify(saveStatusMatrix) })
            .then((response) => response.json())
            .then((statusMatrix) => {
                this.name = '';
                this.store.dispatch({ type: SAVED_STATUS_MATRIX_SUCCESS, saved: statusMatrix });
                this.store.dispatch({ type: CREATE_NEW_STATUS_MATRIX });
            })
            .catch((error) => { console.error(error); this.store.dispatch({ type: SAVED_STATUS_MATRIX_FAILURE, error: error }) });
    }
}

function handleError(response: any) {
    throw new Error(response);
}

