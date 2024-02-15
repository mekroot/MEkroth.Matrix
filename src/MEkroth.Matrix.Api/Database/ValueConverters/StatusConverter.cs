using MEkroth.Matrix.Api.StatusMatrices;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace MEkroth.Matrix.Api.Database.ValueConverters
{
    public sealed class StatusArrayConverter : ValueConverter<Status[], string>
    {
        public StatusArrayConverter() : base(statuses => ToJsonArray(statuses), value => ToStatusArray(value))
        { }

        private static string ToJsonArray(Status[] statuses)
        {
            return JsonSerializer.Serialize(statuses);
        }

        private static Status[] ToStatusArray(string storedValue)
        {
            return JsonSerializer.Deserialize<Status[]>(storedValue) ?? [];
        }
    }
}
