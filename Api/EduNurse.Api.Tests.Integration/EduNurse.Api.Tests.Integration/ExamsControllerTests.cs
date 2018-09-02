using System;
using System.Collections.Generic;
using System.Linq;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Commands;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Shared.Results;
using EduNurse.Exams.Tests.Unit;
using EduNurse.Exams.Tests.Unit.Extensions;
using FluentAssertions;
using Xunit;

namespace EduNurse.Api.Tests.Integration
{
    public class ExamsControllerTests
    {
        private const string Url = "api/v1/exams";

        [Fact]
        public async void GetCategoriesByType_WhenExists_ReturnsDistinctedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                await sut.CreateManyAsync(new List<Exam>()
                {
                    new ExamBuilder("First Exam", ExamType.GeneralKnowledge, "Kardiologia").Build(),
                    new ExamBuilder("Second Exam", ExamType.GeneralKnowledge, "Interna").Build(),
                    new ExamBuilder("Third Exam", ExamType.Specialization, "Kardiologia").Build(),
                    new ExamBuilder("Fourth Exam", ExamType.GeneralKnowledge, "Kardiologia").Build(),
                    new ExamBuilder("Fifth Exam", ExamType.GeneralKnowledge, "Interna").Build(),
                    new ExamBuilder("Sixth Exam", ExamType.Specialization, "Urologia").Build(),
                    new ExamBuilder("Seventh Exam", ExamType.GeneralKnowledge, "Nefrologia").Build()
                });

                var url = $"{Url}/{nameof(ExamType.GeneralKnowledge)}/categories";
                var result = sut.HttpGet<CategoriesResult>(url);

                result.IsSuccess.Should().BeTrue();
                result.Content.Should().BeEquivalentTo(new CategoriesResult(
                    new []
                    {
                        new CategoriesResult.Category("Kardiologia"), 
                        new CategoriesResult.Category("Interna"), 
                        new CategoriesResult.Category("Nefrologia"), 
                    })
                );
            }
        }

        [Fact]
        public async void GetExamsByTypeAndCategory_WhenExists_ReturnsResultWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var exams = await sut.CreateManyAsync(new List<Exam>()
                {
                    new ExamBuilder("First Exam", ExamType.GeneralKnowledge, "Kardiologia").Build(),
                    new ExamBuilder("Second Exam", ExamType.GeneralKnowledge, "Interna").Build(),
                    new ExamBuilder("Third Exam", ExamType.Specialization, "Kardiologia").Build()
                });

                var expected = new ExamsResult(new [] { sut.Mapper.Map<ExamsResult.Exam>(exams[1]) });

                var url = $"{Url}/{nameof(ExamType.GeneralKnowledge)}/Interna";
                var result = sut.HttpGet<ExamsResult>(url);

                result.IsSuccess.Should().BeTrue();
                result.Content.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public void GetExamById_WhenExamNotExists_EmptyValueIsReturnedWithStatus204()
        {
            using (var sut = new SystemUnderTest())
            {
                var result = sut.HttpGet<ExamWithQuestionsResult>(Url, Guid.NewGuid());

                result.Content.Should().BeNull();
                result.IsSuccess.Should().BeFalse();
                result.Message.Should().BeEquivalentTo("Exam with given Id does not exists.");
            }
        }

        [Fact]
        public async void GetExamById_WhenExamsExists_ExamIsReturnedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var exams = await sut.CreateManyAsync(new List<Exam>()
                { 
                    new ExamBuilder("First Exam", ExamType.GeneralKnowledge, "Kardiologia")
                        .WithQuestion("e1q1", CorrectAnswer.A)
                        .WithQuestion("e1q2", CorrectAnswer.B)
                        .Build(),
                    new ExamBuilder("Second Exam", ExamType.GeneralKnowledge, "Interna")
                        .WithQuestion("e2q1", CorrectAnswer.C)
                        .WithQuestion("e2q2", CorrectAnswer.D)
                        .Build(),
                    new ExamBuilder("Third Exam", ExamType.Specialization, "Kardiologia")
                        .WithQuestion("e3q1", CorrectAnswer.B)
                        .WithQuestion("e3q2", CorrectAnswer.D)
                        .Build()
                });

                var expected = sut.Mapper.Map<ExamWithQuestionsResult>(exams[1]);

                var result = sut.HttpGet<ExamWithQuestionsResult>($"{Url}/{exams[1].Id}");

                result.IsSuccess.Should().BeTrue();
                result.Content.Should().BeEquivalentTo(expected, o => o.Excluding(x => x.Questions));
                result.Content.Questions.Should().BeEquivalentTo(expected.Questions, o => o.Excluding(x => x.Id));
            }
        }

        [Fact]
        public async void AddExamWithQuestions_WhenCalled_CorrectlyCreatesObjectsWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                var exam = new ExamBuilder("Some-exam", ExamType.Specialization, "Some-category")
                    .WithQuestion("Q1", CorrectAnswer.A)
                    .WithQuestion("Q2", CorrectAnswer.B)
                    .WithQuestion("Q3", CorrectAnswer.C)
                    .Build();
                var command = sut.Mapper.Map<AddExamCommand>(exam);

                var commandResult = sut.HttpPost(Url, command);
                var allExams = await sut.GetAllExamsAsync();
                var result = await sut.GetExamByIdAsync(allExams.First().Id);

                commandResult.IsSuccess.Should().BeTrue();
                allExams.Count.Should().Be(1);
                result.CreatedDate.Should().BeAfter(exam.CreatedDate);
                result.Should().BeEquivalentTo(
                    exam,
                    options => options
                        .Excluding(p => p.Id)
                        .Excluding(p => p.CreatedDate)
                        .Excluding(p => p.CreatedBy)
                        .Excluding(p => p.Questions)
                );
                result.Questions.Should().BeEquivalentTo(
                    exam.Questions,
                    options => options
                        .Excluding(p => p.Id)
                        .Excluding(p => p.Exam)
                );
            }
        }

        [Fact]
        public async void EditExamWithQuestions_WhenExamExists_CorrectlyModifyExamAndQuestionsWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                await sut.CreateAsync(new ExamBuilder("Some-exam", ExamType.Specialization, "Some-category")
                    .WithQuestion("Q1", CorrectAnswer.A)
                    .WithQuestion("Q2", CorrectAnswer.B)
                    .WithQuestion("Q3", CorrectAnswer.C)
                    .Build()
                );

                var allExams = await sut.GetAllExamsAsync();
                var original = await sut.GetExamByIdAsync(allExams.First().Id);
                var modified = original.DeepClone();

                modified.SetName("Different name");
                modified.SetCategory("Interna");
                modified.ChangeType(ExamType.GeneralKnowledge);

                modified.RemoveQuestion(modified.Questions.Last().Id);
                modified.AddQuestion(3, "text", "a", "b", "c", "d", CorrectAnswer.D, "expl");
                modified.AddQuestion(4, "text-second", "e", "f", "g", "h", CorrectAnswer.A, "testing");
                modified.Questions.First().SetText("Changed text");

                var command = sut.Mapper.Map<EditExamCommand>(modified);

                var commandResult = sut.HttpPut(Url, modified.Id, command);
                var result = await sut.GetExamByIdAsync(modified.Id);

                commandResult.IsSuccess.Should().BeTrue();
                result.Should().NotBe(original);
                result.Questions.Count.Should().Be(4);
                result.Questions.Should().NotBeEquivalentTo(original.Questions);
            }
        }

        [Fact]
        public async void RemoveExamWithQuestions_WhenExamExists_DeleteExamsAndQuestionsWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                var exam = await sut.CreateAsync(new ExamBuilder("Some-exam", ExamType.Specialization, "Some-category")
                    .WithQuestion("Q1", CorrectAnswer.B)
                    .WithQuestion("Q2", CorrectAnswer.D)
                    .Build()
                );

                var addedExam = await sut.GetExamByIdAsync(exam.Id);

                var commandResult = sut.HttpDelete(Url, addedExam.Id);
                var result = await sut.GetExamByIdAsync(addedExam.Id);

                commandResult.IsSuccess.Should().BeTrue();
                addedExam.Should().NotBeNull();
                result.Should().BeNull();
            }
        }
    }
}
