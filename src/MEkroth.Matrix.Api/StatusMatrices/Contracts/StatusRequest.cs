namespace MEkroth.Matrix.Api.StatusMatrices.Contracts
{
    public class StatusRequest
    {
        public int Status { get; set; }

        public Status Cast()
        {
            return (Status)Status;
        }
    }
}
