namespace MEkroth.Matrix.Api.StatusMatrices
{
    public enum Status
    {
        None = 0,
        Ok = 1,
        Warning = 2,
        Error = 3,
    }

    public sealed class StatusMatrix
    {
        private const int DefaultMatrixSize = 25;
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Status[] Statuses { get; set; } = new Status[DefaultMatrixSize];
    }
}
