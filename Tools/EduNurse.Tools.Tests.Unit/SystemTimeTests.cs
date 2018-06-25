using System;
using FluentAssertions;
using Xunit;

namespace EduNurse.Tools.Tests.Unit
{
    public class SystemTimeTests
    {
        [Fact]
        public void WhenSystemTimeWasNotSetExplicitly_SystemTimeNow_ReturnsRealActualTime()
        {
            var nowBefore = SystemTime.Now;

            var nowAfter = SystemTime.Now;

            nowAfter.Should().NotBe(nowBefore);
        }

        [Fact]
        public void WhenSystemTimeWasSetExplicitly_SystemTimeNow_ReturnsSetTime()
        {
            var setDate = new DateTime(2018, 6, 25, 16, 33, 0, 0);
            SystemTime.SetDate(setDate);

            var resultDate = SystemTime.Now;

            resultDate.Should().Be(setDate);
        }

        [Fact]
        public void WhenSystemTimeWasSetExplicitlyAndBeforeItWasSavedAnotherDate_SystemTimeNow_ReturnsDifferentTime()
        {
            var nowBefore = SystemTime.Now;
            SystemTime.SetDate(new DateTime(2018, 6, 25, 16, 33, 0, 0));

            var nowAfter = SystemTime.Now;

            nowAfter.Should().NotBe(nowBefore);
        }
    }
}
