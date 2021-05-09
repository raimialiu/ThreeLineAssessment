using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ThreeLineAssessmentQuestionOne;
using Xunit;

namespace ThreeLineAssessment.Test
{
    public class SmallestPositiveIntegerTest
    {
        [Fact]
        public async Task ShouldReturnMinimumValueNotZero()
        {
            int result = Assessment.SmallestPositiveInteger(new[] { 2, 3, 7, 6, 8, 1, -10, 15 });

            result.Should().Be(4);
        }

        [Theory]
        [MemberData(nameof(SampleTestData))]
        public async Task ShouldReturnValueGreaterThanZero(int expected, int[] input)
        {
            int result = Assessment.SmallestPositiveInteger(input);

            result.Should().Be(expected);
        }

        public static IEnumerable<object[]> SampleTestData()
        {
            yield return new object[] { 3, new[] { 1, 5, 7, 4, 1, 2 } };
            yield return new object[] { 1, new[] { 2, 3, 7, 6, 8, -1, -10, 15 } };
            yield return new object[] { 2, new[] { 1, 1, 0, -1, -2 } };
            // {1, 1, 0, -1, -2}
            yield return new object[] { 4, new[] { 1, 2, 3 } };
            yield return new object[] { 1, new[] { -1,-3} };
        }
    }
}
