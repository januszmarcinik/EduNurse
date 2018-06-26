using System;
using AutoMapper;
using EduNurse.Exams.Api;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Dto;
using EduNurse.Exams.Shared.Enums;
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
                text: "sample-text",
                a: "answer-a",
                b: "answer-b",
                c: "answer-c",
                d: "answer-d",
                correctAnswer: Shared.Enums.CorrectAnswer.C
            );
            var expected = new QuestionDto(
                id: uut.Id,
                text: uut.Text,
                a: uut.A,
                b: uut.B,
                c: uut.C,
                d: uut.D,
                correctAnswer: uut.CorrectAnswer
            );

            var result = _mapper.Map<QuestionDto>(uut);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void WhenMapIsCreated_MapExamToExamDto_ReturnMappedObject()
        {
            var uut = new Exam(
                id: Guid.NewGuid(),
                name: "sample-name",
                type: ExamType.Specialized,
                category: "sample-category"
            );
            var expected = new Exam(
                id: uut.Id,
                name: uut.Name,
                type: uut.Type,
                category: uut.Category
            );

            var result = _mapper.Map<ExamDto>(uut);

            result.Should().BeEquivalentTo(expected);
        }
    }
}
