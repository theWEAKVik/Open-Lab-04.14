using System;
using System.Collections;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Open_Lab_04._14
{
    [TestFixture]
    class Tests
    {

        private bool shouldStop;
        private FramePrinter printer;
        private StringWriter writer;
        private TextWriter consoleWriter;

        private const int MaxWordSize = 100;
        private const int MaxWordCount = 100;
        private const int RandSeed = 34242252;
        private const int RandTestCasesCount = 98;

        [OneTimeSetUp]
        public void Init()
        {
            shouldStop = false;
            printer = new FramePrinter();
            writer = new StringWriter();
            consoleWriter = Console.Out;
        }

        [OneTimeTearDown]
        public void Cleanup() => writer.Close();

        [SetUp]
        public void Setup() => Console.SetOut(writer);

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(consoleWriter);
            writer.GetStringBuilder().Clear();

            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure ||
                TestContext.CurrentContext.Result.Outcome == ResultState.Error)
                shouldStop = true;
        }

        [TestCaseSource(nameof(GetPredefined))]
        public void TestWithPredefined(string[] input, string expectedOutput) => Test(input, expectedOutput);

        [TestCaseSource(nameof(GetRandom))]
        public void TestWithRandom(string[] input, string expectedOutput) => Test(input, expectedOutput);

        public void Test(string[] input, string expectedOutput)
        {
            if (shouldStop)
                Assert.Ignore("Previous test failed!");

            printer.Print(input);
            writer.Flush();

            var output = writer.ToString();

            //TODO: better explanation, when assertion fails
            var splitOutLineBreak = output.Split('\n');
            Assert.That(splitOutLineBreak[^1].Length == 0, "Please make sure, that last line has been written with Console.WriteLine(); on the end!");

            foreach (var entry in splitOutLineBreak)
                if (entry.Length != 0 && entry[^1] != '\r')
                    Assert.Fail("Please use Console.WriteLine(); to move into next line!");

            var splitOut = output.Split("\r\n");
            var splitExp = expectedOutput.Split("\r\n");

            Assert.That(splitExp.Length == splitOut.Length,
                $"Expected {splitExp.Length} lines in console, received {splitOut.Length}!");

            for (var i = 0; i < splitExp.Length; i++)
            {
                var exp = splitExp[i];
                var real = splitOut[i];

                Assert.That(exp.Length == real.Length, 
                    $"Expected {exp.Length} characters at line {i + 1}, received {real.Length}!");

                for (var j = 0; j < exp.Length; j++)
                    Assert.That(exp[j] == real[j],
                        $"Expected '{exp[j]}' at pos {j + 1}, line {i + 1}, received {real[j]}!");
            }
        }

        private static IEnumerable GetPredefined()
        {
            var input = new[] { "Hello", "World", "in", "a", "frame" };
            yield return new TestCaseData(input, CreateSolution(input));

            input = new[] { "Hello", "Xamarin", "Lab!" };
            yield return new TestCaseData(input, CreateSolution(input));
        }

        private static IEnumerable GetRandom()
        {
            var random = new Random(RandSeed);
            for (var z = 0; z < RandTestCasesCount; z++)
            {
                var input = new string[random.Next(MaxWordCount) + 1];
                for (var i = 0; i < input.Length; i++)
                {
                    var chars = new char[random.Next(MaxWordSize) + 1];

                    for (var j = 0; j < chars.Length; j++)
                        chars[j] = (char)random.Next(65, 91); //A - Z

                    input[i] = new string(chars);
                }
                yield return new TestCaseData(input, CreateSolution(input));
            }
        }

        private static string CreateSolution(string[] input)
        {
            var writer = new StringWriter();
            var biggestSize = input.OrderByDescending(str => str.Length).First().Length;

            for (var i = 0; i < biggestSize + 4; i++)
                writer.Write('*');

            writer.WriteLine();

            foreach (var str in input)
            {
                writer.Write($"* {str}");

                for (var i = 0; i < biggestSize - str.Length; i++)
                    writer.Write(' ');

                writer.WriteLine(" *");
            }

            for (var i = 0; i < biggestSize + 4; i++)
                writer.Write('*');

            writer.WriteLine();
            writer.Flush();

            var solution = writer.ToString();
            writer.Close();
            return solution;
        }
    }
}
