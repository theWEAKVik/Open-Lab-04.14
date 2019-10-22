using System;
using System.Collections;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Open_Lab_04._14
{
    [TestFixture]
    class Tests
    {

        private Exercise exercise;
        private StringWriter writer;
        private TextWriter consoleWriter;

        private const int RandSeed = 34242252;
        private const int MaxWordSize = 100;
        private const int MaxWordCount = 100;

        [OneTimeSetUp]
        public void Init()
        {
            exercise = new Exercise();
            writer = new StringWriter();
            consoleWriter = Console.Out;
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            writer.Close();
        }

        [SetUp]
        public void Setup()
        {
            Console.SetOut(writer);
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(consoleWriter);
            writer.GetStringBuilder().Clear();
        }

        [TestCaseSource(nameof(GetPredefined))]
        public void TestWithPredefined(string[] input, string expectedOutput)
        {
            exercise.PrintInFrame(input);
            writer.Flush();

            Assert.That(expectedOutput.Equals(writer.ToString()));
        }

        [TestCaseSource(nameof(GetRandom))]
        public void TestWithRandom(string[] input, string expectedOutput)
        {
            exercise.PrintInFrame(input);
            writer.Flush();

            Assert.That(expectedOutput.Equals(writer.ToString()));
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
            for (var z = 0; z < 98; z++)
            {
                var input = new string[random.Next(MaxWordCount) + 1];
                for (var i = 0; i < input.Length; i++)
                {
                    var chars = new char[random.Next(MaxWordSize) + 1];

                    for (var j = 0; j < chars.Length; j++)
                        chars[j] = (char) random.Next(65, 91); //A - Z

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
