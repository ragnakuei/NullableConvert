using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace NullableConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();
        }
    }

    [ClrJob, MonoJob, CoreJob] // 可以針對不同的 CLR 進行測試
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class TestRunner
    {
        private readonly TestClass _test = new TestClass();

        public TestRunner()
        {
        }

        [Benchmark]
        public void TestMethod1() => _test.TestMethod1();

        [Benchmark]
        public void TestMethod2() => _test.TestMethod2();
    }

    public class TestClass
    {
        private readonly Test _sampleData = new Test{ Id = 1};

        public string TestMethod1()
        {
            var result = _sampleData?.Id;
            return result?.ToString();
        }

        public string TestMethod2()
        {
            return Convert.ToString(_sampleData?.Id);
        }
    }

    public class Test
    {
        public int Id { get; set; }
    }

}
