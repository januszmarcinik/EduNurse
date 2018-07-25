using System;
using System.Linq;
using AutoMapper;
using EduNurse.Exams.Api;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Shared.Results;
using EduNurse.Tools;
using FluentAssertions;
using Xunit;

namespace EduNurse.Exams.Tests.Unit
{
    public class AutoMapperTests
    {
        private readonly IMapper _mapper;

        public AutoMapperTests()
        {
            _mapper = AutoMapperConfig.Initialize();
        }

        [Fact]
        public void WhenMapIsCreated_MapQuestionToQuestionDto_ReturnMappedObject()
        {
            var uut = new Question(
                id: Guid.NewGuid(),
                examId: Guid.NewGuid(),
                order: 1,
                text: "sample-text",
                a: "answer-a",
                b: "answer-b",
                c: "answer-c",
                d: "answer-d",
                correctAnswer: Shared.Enums.CorrectAnswer.C,
                explanation: "exmplantation-sample"
            );
            var expected = new QuestionResult()
            {
                Id = uut.Id,
                ExamId = uut.ExamId,
                Order = uut.Order,
                A = uut.A,
                B = uut.B,
                C = uut.C,
                D = uut.D,
                CorrectAnswer = uut.CorrectAnswer,
                Explanation = uut.Explanation,
                Text = uut.Text
            };

            var result = _mapper.Map<QuestionResult>(uut);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void WhenMapIsCreated_MapExamToExamDto_ReturnMappedObject()
        {
            var uut = new Exam(
                id: Guid.NewGuid(),
                name: "sample-name",
                type: ExamType.Specialization,
                category: "sample-category",
                createdBy: "created-by",
                createdDate: SystemTime.Now,
                isConfirmed: false
            );
            var expected = new ExamWithQuestionsResult()
            {
                Category = uut.Category,
                CreatedBy = uut.CreatedBy,
                CreatedDate = uut.CreatedDate,
                Id = uut.Id,
                IsConfirmed = uut.IsConfirmed,
                Name = uut.Name,
                Questions = Enumerable.Empty<QuestionResult>(),
                Type = uut.Type
            };

            var result = _mapper.Map<ExamWithQuestionsResult>(uut);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
