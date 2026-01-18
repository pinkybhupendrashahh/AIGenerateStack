namespace AIGenerateStack.Server.Model
{
    public class ErrorResponse
    {
        public string TraceId { get; init; }
        public int StatusCode { get; init; }
        public string Message { get; init; }
        public string? Details { get; init; }
    }
}
