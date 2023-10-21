# Declaro.Net.Benchmarks
A comparison benchmark between DeclaroDotNet and competitors.
Contestants:
- DeclaroDotNet
- Refit
- Flurl.Http
- Direct (the built-in inflexible IHttpClientFactory)

## TL;DR - The Results

From the libraries, **DeclaroDotNet is the winner when it comes to speed and most importantly, _memory usage_**!
Since **Direct tests** use a plain HttpClientFactory, they are obvously faster and require less memory.
I wanted to show you that it's not a big lose compared to all the convinience that DeclaroDotNet offers.
*Declaro_Cached is obviously the fastest (see Time.cs for reference) because it stores the retrieved value in your local memory cache.
Of course you can implement caching with HttpClientFactory but it's code you have to write and maintain.*

**Download DeclaroDotNet NuGet Package: https://www.nuget.org/packages/Declaro.Net**
**Browse DeclaroDotNet source code: https://github.com/devopsdani/Declaro.Net**

| Method                          | Mean           | Error        | StdDev       | Allocated |
|-------------------------------- |----------------|--------------|--------------|-----------|
| ApiSpeedTest_GET_Declaro_Cached |       350.5 ns |      3.04 ns |      2.85 ns |     256 B |
| ApiSpeedTest_GET_Direct         | 1,650,240.4 ns | 14,692.13 ns | 13,024.19 ns |    6427 B |
| ApiSpeedTest_GET_Declaro        | 1,662,699.6 ns |  9,589.05 ns |  8,500.44 ns |    6686 B |
| ApiSpeedTest_GET_Refit          | 1,680,565.6 ns | 13,346.22 ns | 11,831.08 ns |    7955 B |
| ApiSpeedTest_GET_FlurlHttp      | 1,656,473.9 ns | 14,869.04 ns | 13,181.02 ns |    9612 B |
|                                 |                |              |              |           |
| ApiSpeedTest_DELETE_Direct      | 1,632,624.4 ns | 20,919.72 ns | 19,568.32 ns |    5101 B |
| ApiSpeedTest_DELETE_Declaro     | 1,634,819.3 ns | 13,829.41 ns | 12,936.04 ns |    5896 B |
| ApiSpeedTest_DELETE_Refit       | 1,686,838.4 ns | 13,751.62 ns | 12,863.27 ns |    7214 B |
| ApiSpeedTest_DELETE_FlurlHttp   | 1,677,055.1 ns | 14,876.82 ns | 13,187.91 ns |    9292 B |
|                                 |                |              |              |           |
| ApiSpeedTest_PATCH_Direct       | 1,679,008.0 ns | 27,095.99 ns | 25,345.61 ns |    6187 B |
| ApiSpeedTest_PATCH_Declaro      | 1,688,688.1 ns | 14,334.65 ns | 12,707.29 ns |    7799 B |
| ApiSpeedTest_PATCH_Refit        | 1,699,637.3 ns | 16,704.16 ns | 14,807.80 ns |    8718 B |
| ApiSpeedTest_PATCH_FlurlHttp    | 1,718,620.5 ns |  9,113.46 ns |  8,524.74 ns |   19600 B |
|                                 |                |              |              |           |
| ApiSpeedTest_POST_Direct        | 1,676,075.8 ns | 15,063.43 ns | 13,353.34 ns |    6182 B |
| ApiSpeedTest_POST_Declaro       | 1,698,341.2 ns | 10,313.13 ns |  9,646.91 ns |    7783 B |
| ApiSpeedTest_POST_Refit         | 1,690,671.7 ns |  8,211.27 ns |  7,279.08 ns |    8716 B |
| ApiSpeedTest_POST_FlurlHttp     | 1,708,607.5 ns | 16,570.27 ns | 13,836.92 ns |   19544 B |
|                                 |                |              |              |           |
| ApiSpeedTest_PUT_Direct         | 1,675,232.0 ns | 13,682.14 ns | 12,798.28 ns |    6174 B |
| ApiSpeedTest_PUT_Declaro        | 1,692,014.3 ns | 10,710.44 ns | 10,018.55 ns |    7783 B |
| ApiSpeedTest_PUT_Refit          | 1,697,727.6 ns | 19,946.74 ns | 17,682.27 ns |    8711 B |
| ApiSpeedTest_PUT_FlurlHttp      | 1,712,461.2 ns | 19,098.52 ns | 16,930.34 ns |   19544 B |

**Direct tests use** the built-in, best-practice solution for API communication in .NET currently.
*This approach requires a lot of manual coding even here in our test, not to speak about in real scenarios.*

The libraries are able to handle errors, make type conversion, manipulate request and/or content headers, caching, and much more...
All in purpose to make it convinient for developers to interact with HTTP APIs, so you can deliver your features faster.
**All libraries** in this test are amazing tools with a different approach and developer experience.
**Declaro.Net** happens to be the fastest. Since it is also the latest, it has the most potential for new features and further optimization.

## Replicate test results
1. Set "Benchmarks" and "Functions" as startup projects.
2. Change to "Release" configuration.
3. Start.

## Tested environment
The tests were executed on Windows and Ubuntu, native operation system installments.

**Windows system specs:**
- Ryzen 7 3700x
- 16GB DDR4 RAM
- SSD

**Ubuntu system specs:**
HP ProDesk 600 G2 DM (N5F41AV)
- Intel i3-6100T
- 16GB DDR3 RAM
- SSD
