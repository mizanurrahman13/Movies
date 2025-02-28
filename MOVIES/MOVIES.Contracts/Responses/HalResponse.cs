using System.Text.Json.Serialization;

namespace MOVIES.Contracts.Responses;

public abstract class HalResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Link> Links { get; set; } = new();
}

public class Link
{
    public required string Href { get; set; }
    public required string Rel { get; set; }
    public required string Type { get; set; }
}
