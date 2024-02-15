using static MEkroth.Matrix.Api.StatusMatrices.Exceptions.StatusMatrixExceptions;

namespace MEkroth.Matrix.Api.StatusMatrices.Exceptions
{
    public static class StatusMatrixErrors
    {
        public static StatusMatrixNotFoundException NotFound => new StatusMatrixNotFoundException("Status matrix doesn't exists.");
        public static StatusMatrixCouldNotDeleteException CouldNotDelete(Exception innerException) => new StatusMatrixCouldNotDeleteException("Something went wrong when trying delete status matrix.", innerException);
        public static StatusMatrixCouldNotSaveException CouldNotSave(Exception innerException) => new StatusMatrixCouldNotSaveException("Something went wrong when trying save status matrix.", innerException);
    }
}
