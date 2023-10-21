using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Declaro.Net;
using Declaro.Net.Connection;
using Flurl;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Net.Http.Json;
using Flurl.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using System;
using System.Text.Json;

[MemoryDiagnoser(false)]
[Orderer(SummaryOrderPolicy.Method)]
[RankColumn]
public class BenchmarkTests
{
    private readonly HttpService _httpService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITimeApi _timeApi;
    private readonly Url _timeConnection;
    private Time _time = new();

    public BenchmarkTests()
    {
        var baseUriString = "http://localhost:7071/";
        var sp = new ServiceCollection()
            .AddHttpClient("", client => client.BaseAddress = new Uri(baseUriString)).Services
            .AddHttpService()
            .AddMemoryCache()
            .AddRefitClient<ITimeApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUriString)).Services
            .BuildServiceProvider();

        _httpService = sp.GetRequiredService<HttpService>();
        _httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
        _timeApi = sp.GetRequiredService<ITimeApi>();

        _timeConnection = baseUriString.AppendPathSegment(Constants.TimeApi);
    }

    /*GET*/
    [Benchmark]
    public async Task ApiSpeedTest_GET_Refit()
    {
        await _timeApi.GetTime();
    }
    [Benchmark]
    public async Task ApiSpeedTest_GET_Declaro()
    {
        await _httpService.GetAsync<Time>();
    }
    [Benchmark]
    public async Task ApiSpeedTest_GET_Declaro_Cached()
    {
        await _httpService.GetAsync<TimeCached>();
    }
    [Benchmark]
    public async Task ApiSpeedTest_GET_FlurlHttp()
    {
        await _timeConnection.GetAsync();
    }
    [Benchmark]
    public async Task ApiSpeedTest_GET_Direct()
    {
        await _httpClientFactory.CreateClient().GetFromJsonAsync<Time>(Constants.TimeApi);
    }

    /*POST*/
    [Benchmark]
    public async Task ApiSpeedTest_POST_Direct()
    {
        await _httpClientFactory.CreateClient()
            .PostAsync(
                content: new StringContent(JsonSerializer.Serialize(_time), Encoding.UTF8, "application/json"),
                requestUri: Constants.TimeApi);
    }
    [Benchmark]
    public async Task ApiSpeedTest_POST_Declaro()
    {
        await _httpService.PostAsync(_time);
    }
    [Benchmark]
    public async Task ApiSpeedTest_POST_FlurlHttp()
    {
        await _timeConnection.PostJsonAsync(_time).ReceiveJson<Time>();
    }
    [Benchmark]
    public async Task ApiSpeedTest_POST_Refit()
    {
        await _timeApi.PostTime(_time);
    }

    /*PUT*/
    [Benchmark]
    public async Task ApiSpeedTest_PUT_Direct()
    {
        await _httpClientFactory.CreateClient()
            .PutAsync(
                content: new StringContent(JsonSerializer.Serialize(_time), Encoding.UTF8, "application/json"),
                requestUri: Constants.TimeApi);
    }
    [Benchmark]
    public async Task ApiSpeedTest_PUT_Declaro()
    {
        await _httpService.PutAsync(_time);
    }
    [Benchmark]
    public async Task ApiSpeedTest_PUT_FlurlHttp()
    {
        await _timeConnection.PutJsonAsync(_time).ReceiveJson<Time>();
    }
    [Benchmark]
    public async Task ApiSpeedTest_PUT_Refit()
    {
        await _timeApi.PutTime(_time);
    }

    /*PATCH*/
    [Benchmark]
    public async Task ApiSpeedTest_PATCH_Direct()
    {
        await _httpClientFactory.CreateClient()
            .PatchAsync(
                content: new StringContent(JsonSerializer.Serialize(_time), Encoding.UTF8, "application/json"),
                requestUri: Constants.TimeApi);
    }
    [Benchmark]
    public async Task ApiSpeedTest_PATCH_Declaro()
    {
        await _httpService.PatchAsync(_time);
    }
    [Benchmark]
    public async Task ApiSpeedTest_PATCH_FlurlHttp()
    {
        await _timeConnection.PatchJsonAsync(_time).ReceiveJson<Time>();
    }
    [Benchmark]
    public async Task ApiSpeedTest_PATCH_Refit()
    {
        await _timeApi.PatchTime(_time);
    }

    /*DELETE*/
    [Benchmark]
    public async Task ApiSpeedTest_DELETE_Direct()
    {
        await _httpClientFactory.CreateClient().DeleteAsync(Constants.TimeApi);
    }
    [Benchmark]
    public async Task ApiSpeedTest_DELETE_Declaro()
    {
        await _httpService.DeleteAsync(_time);
    }
    [Benchmark]
    public async Task ApiSpeedTest_DELETE_FlurlHttp()
    {
        await _timeConnection.DeleteAsync();
    }
    [Benchmark]
    public async Task ApiSpeedTest_DELETE_Refit()
    {
        await _timeApi.DeleteTime();
    }
}
