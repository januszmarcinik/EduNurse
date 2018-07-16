using System;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Tools;
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
                Type = ExamType.Specialization,
                Name = "sample-name",
                Category = "sample-category",
                CreatedBy = "sample-author",
                CreatedDate = DateTime.Now,
                IsConfirmed = true
            };

            var actual = new Exam(
                id: expected.Id,
                name: expected.Name,
                type: expected.Type,
                category: expected.Category,
                createdBy: expected.CreatedBy,
                createdDate: expected.CreatedDate,
                isConfirmed: expected.IsConfirmed
            );

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
