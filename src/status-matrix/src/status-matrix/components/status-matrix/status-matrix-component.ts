import { ICustomElementViewModel, inject } from "aurelia";
import { IStore, fromState } from "@aurelia/state";
import { StatusMatrixModel } from "../../models/status-matrix";
import { ChangeStatus, CreateNewStatusMatrix, CHANGE_STATUS, CREATE_NEW_STATUS_MATRIX } from "../../../state/actions/status-matrix-actions";
import { State } from "../../../state/state";

@inject(IStore)
export class StatusMatrixComponent implements ICustomElementViewModel {

    @fromState((state:State) => state.currentSelected)
    current: StatusMatrixModel;

    store : IStore<{}, ChangeStatus | CreateNewStatusMatrix >;

    constructor(store: IStore<{}, ChangeStatus | CreateNewStatusMatrix >) {
        this.store = store;
    }

    changeStatus(index) 
    {
        this.store.dispatch({ type: CHANGE_STATUS, position: index })
    }

    createNew() 
    {
        this.store.dispatch({ type: CREATE_NEW_STATUS_MATRIX })
    }
}