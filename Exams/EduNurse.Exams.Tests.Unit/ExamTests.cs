using System;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Enums;
using FluentAssertions;
using Xunit;

namespace EduNurse.Exams.Tests.Unit
{
    public class ExamTests
    {
        [Fact]
        public void WhenFullContructorExist_CreateByNewConstructor_ReturnsAppropriateObject()
        {
            var expected = new
            {
                Id = Guid.NewGuid(),
                Type = ExamType.Specialized,
                Name = "sample-name",
                Category = "sample-category"
            };

            var actual = new Exam(
                id: expected.Id,
                name: expected.Name,
                type: expected.Type,
                category: expected.Category
            );

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
