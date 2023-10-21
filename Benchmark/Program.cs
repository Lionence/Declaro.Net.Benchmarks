using BenchmarkDotNet.Running;

Thread.Sleep(5000);
BenchmarkRunner.Run<BenchmarkTests>();
