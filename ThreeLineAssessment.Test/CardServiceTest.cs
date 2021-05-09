using FluentAssertions;
using QuestionTwo.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ThreeLineAssessment.Test
{
    public class CardServiceTest : BaseTestClass
    {
        [Fact]
        public async Task ShouldReturnInvalidWhenInvalidDataIsPassed()
        {
            //arrange
            var CardPan = "6745783784578wjraew";
            var crdTo = new CardDTO()
            {
                type = CardType.CREDIT,
                Scheme = Scheme.AMEX
            };
            // act
            var result = _sut.VerifyCard(CardPan, crdTo);
            // assert
            result.success.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnTrueWhenValidDataIsUsed()
        {
            //arrange
            var CardPan = "7578458754895489589";
            var crdTo = new CardDTO()
            {
                type = CardType.CREDIT,
                Scheme = Scheme.AMEX
            };
            // act
            var result = _sut.VerifyCard(CardPan, crdTo);
            // assert
            result.success.Should().BeTrue();
        }
    }
}
