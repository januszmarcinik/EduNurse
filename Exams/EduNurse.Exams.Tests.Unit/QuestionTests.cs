using System;
using EduNurse.Exams.Api.Questions;
using EduNurse.Exams.Shared.Questions;
using FluentAssertions;
using Xunit;

namespace EduNurse.Exams.Tests.Unit
{
    public class QuestionTests
    {
        [Fact]
        public void WhenFullContructorExist_CreateByNewConstructor_ReturnsAppropriateObject()
        {
            var expected = new
            {
                Id = Guid.NewGuid(),
                Text = "sample-text",
                A = "answer-a",
                B = "answer-b",
                C = "answer-c",
                D = "answer-d",
                CorrectAnswer = CorrectAnswer.A
            };

            var actual = new Question(
                id: expected.Id,
                text: expected.Text,
                a: expected.A,
                b: expected.B,
                c: expected.C,
                d: expected.D,
                correctAnswer: expected.CorrectAnswer
            );

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
