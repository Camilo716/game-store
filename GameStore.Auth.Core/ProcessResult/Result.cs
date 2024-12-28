namespace GameStore.Auth.Core.ProcessResult;

public class Result
{
    public bool Success { get; set; }

    public IEnumerable<string> Errors { get; set; } =
    [
    ];

    public static Result SuccessResult() => new() { Success = true };

    public static Result FailureResult(IEnumerable<string> errors) => new() { Success = false, Errors = errors };
}