namespace MOVIES.Contracts.Responses;

public class ValidationFailureResponse
{
    public required IEnumerable<ValidationResponse> Errors { get; init; }
}

public class ValidationResponse
{
    public required string PropertyName { get; init; } = string.Empty;
    public required string Message { get; init; } = string.Empty;
}
