using NUnit.Framework;
using ReportGenerator.Services;

namespace ReportGenerator.Tests
{
    public class InputValidatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("1906121", true)]
        [TestCase("2609121", false)]
        [TestCase("notanumber", false)]
        public void check_test_number_validation(string testNumber, bool expected)
        {
            Assert.That(InputValidation.IsValidTestNum(testNumber), Is.EqualTo(expected));
        }
    }
}