using Declaro.Net.Attributes;
using Refit;

[Http(ApiEndpoint = Constants.TimeApi)]
[HttpList(ApiEndpoint = Constants.TimesApi)]
public class Time
{
    public virtual DateTimeOffset Value { get; set; } = DateTimeOffset.UtcNow;
}

[Http(ApiEndpoint = Constants.TimeApi)]
[HttpGet(ApiEndpoint = Constants.TimeApi, CacheTime = "00:00:01.000")]
public class TimeCached : Time
{
    public override DateTimeOffset Value { get; set; } = DateTimeOffset.UtcNow;
}

public interface ITimeApi
{
    [Get($"/{Constants.TimeApi}")]
    Task<Time> GetTime();

    [Post($"/{Constants.TimeApi}")]
    Task PostTime([Body(true)] Time time);

    [Put($"/{Constants.TimeApi}")]
    Task PutTime([Body(true)] Time time);

    [Patch($"/{Constants.TimeApi}")]
    Task PatchTime([Body(true)] Time time);

    [Delete($"/{Constants.TimeApi}")]
    Task DeleteTime();

    [Post($"/{Constants.TimesApi}")]
    Task<List<Time>> ListTime();
}