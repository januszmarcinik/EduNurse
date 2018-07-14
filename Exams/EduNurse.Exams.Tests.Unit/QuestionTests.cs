using System;
using EduNurse.Exams.Api.Entities;
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
                ExamId = Guid.NewGuid(),
                Text = "sample-text",
                A = "answer-a",
                B = "answer-b",
                C = "answer-c",
                D = "answer-d",
                CorrectAnswer = Shared.Enums.CorrectAnswer.A,
                Explanation = "some-explanation"
            };

            var actual = new Question(
                id: expected.Id,
                examId: expected.ExamId,
                text: expected.Text,
                a: expected.A,
                b: expected.B,
                c: expected.C,
                d: expected.D,
                correctAnswer: expected.CorrectAnswer,
                explanation: expected.Explanation
            );

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
