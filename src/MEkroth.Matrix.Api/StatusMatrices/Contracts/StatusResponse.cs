using System.Text.Json.Serialization;

namespace MEkroth.Matrix.Api.StatusMatrices.Contracts
{
    public class StatusResponse
    {
        public StatusResponse(Status status)
        {
            Status = status;
            DisplayName = status.ToString();
            Name = status.ToString().ToLower();
        }

        public Status Status { get; set; }
        public string Name { get; init; }
        public string DisplayName { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Count { get; set; }
    }
}
