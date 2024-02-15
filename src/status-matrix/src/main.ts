import Aurelia from 'aurelia';
import { App } from './app';
import { changeStatusHandler, clearError, clearName, createdNewStatusMatrixHandler, deleteStatusMatrixFailureHandler, deleteStatusMatrixSuccessHandler, editStatusMatrixHandler, loadedStatusMatrixFailureHandler, loadedStatusMatrixSuccessHandler, savedStatusMatrixFailureHandler, savedStatusMatrixSuccessHandler, showError } from './state/actions/status-matrix-actions-handlers';
import { StateDefaultConfiguration } from '@aurelia/state';
import { initialState } from './initialState';

Aurelia
    .register(
        StateDefaultConfiguration.init(
            initialState,
            changeStatusHandler,
            clearError, clearName, showError,
            createdNewStatusMatrixHandler,
            editStatusMatrixHandler,
            deleteStatusMatrixSuccessHandler,
            deleteStatusMatrixFailureHandler,
            loadedStatusMatrixSuccessHandler,
            loadedStatusMatrixFailureHandler,
            savedStatusMatrixSuccessHandler,
            savedStatusMatrixFailureHandler,
        )
    )
  .app(App)
  .start();
