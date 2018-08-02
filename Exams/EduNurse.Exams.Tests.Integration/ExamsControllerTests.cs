using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Shared.Results;
using EduNurse.Exams.Tests.Shared.Extensions;
using EduNurse.Exams.Tests.Shared;
using Xunit;

namespace EduNurse.Exams.Tests.Integration
{
    public class ExamsControllerTests
    {
        private const string Url = "api/v1/exams";

        [Fact]
        public void GetCategoriesByType_WhenExists_ReturnsDistinctedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                sut.CreateMany(new List<Exam>()
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
                var apiResponse = sut.HttpGet<CategoriesResult>(url);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(new CategoriesResult(
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
        public void GetExamsByTypeAndCategory_WhenExists_ReturnsResultWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var exams = sut.CreateMany(new List<Exam>()
                {
                    new ExamBuilder("First Exam", ExamType.GeneralKnowledge, "Kardiologia").Build(),
                    new ExamBuilder("Second Exam", ExamType.GeneralKnowledge, "Interna").Build(),
                    new ExamBuilder("Third Exam", ExamType.Specialization, "Kardiologia").Build()
                });

                var expected = new ExamsResult(new [] { exams[1].ToExamResult() });

                var url = $"{Url}/{nameof(ExamType.GeneralKnowledge)}/Interna";
                var apiResponse = sut.HttpGet<ExamsResult>(url);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public void GetExamById_WhenExamNotExists_EmptyValueIsReturnedWithStatus204()
        {
            using (var sut = new SystemUnderTest())
            {
                var apiResponse = sut.HttpGet<ExamWithQuestionsResult>(Url, Guid.NewGuid());

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NoContent);
                apiResponse.Body.Should().BeNull();
            }
        }

        [Fact]
        public void GetExamById_WhenExamsExists_ExamIsReturnedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var exams = sut.CreateMany(new List<Exam>()
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

                var expected = exams[1].ToExamWithQuestionsResult();

                var apiResponse = sut.HttpGet<ExamWithQuestionsResult>($"{Url}/{exams[1].Id}");

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public void AddExamWithQuestions_WhenCalled_CorrectlyCreatesObjectsWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                var exam = new ExamBuilder("Some-exam", ExamType.Specialization, "Some-category")
                    .WithQuestion("Q1", CorrectAnswer.A)
                    .WithQuestion("Q2", CorrectAnswer.B)
                    .WithQuestion("Q3", CorrectAnswer.C)
                    .Build();
                var command = exam.ToAddExamCommand();

                var apiResponse = sut.HttpPost(Url, command);
                var allExams = sut.GetAllExams().ToList();

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
                allExams.Count.Should().Be(1);
                allExams.First().Should().BeEquivalentTo(
                    exam,
                    options => options
                        .Excluding(p => p.Id)
                        .Excluding(p => p.CreatedDate)
                        .Excluding(p => p.CreatedBy)
                        .Excluding(p => p.Questions)
                );
                allExams.First().Questions.Should().BeEquivalentTo(
                    exam.Questions,
                    options => options
                        .Excluding(p => p.Id)
                        .Excluding(p => p.ExamId)
                        .Excluding(p => p.Exam)
                );
            }
        }

        [Fact]
        public void EditExamWithQuestions_WhenExamExists_CorrectlyModifyExamAndQuestionsWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                sut.Create(new ExamBuilder("Some-exam", ExamType.Specialization, "Some-category")
                    .WithQuestion("Q1", CorrectAnswer.A)
                    .WithQuestion("Q2", CorrectAnswer.B)
                    .WithQuestion("Q3", CorrectAnswer.C)
                    .Build()
                );

                var original = sut.GetAllExams().First();
                var modified = original.DeepClone();

                modified.SetName("Different name");
                modified.SetCategory("Interna");
                modified.ChangeType(ExamType.GeneralKnowledge);

                modified.RemoveQuestion(modified.Questions.Last().Id);
                modified.AddQuestion(3, "text", "a", "b", "c", "d", CorrectAnswer.D, "expl");
                modified.AddQuestion(4, "text-second", "e", "f", "g", "h", CorrectAnswer.A, "testing");
                modified.Questions.First().SetText("Changed text");

                var command = modified.ToEditExamCommand();

                var apiResponse = sut.HttpPut(Url, modified.Id, command);
                var result = sut.GetExamById(modified.Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
                result.Should().NotBe(original);
                result.Questions.Count.Should().Be(4);
                result.Questions.Should().NotBeEquivalentTo(original.Questions);
            }
        }

        [Fact]
        public void RemoveExamWithQuestions_WhenExamExists_DeleteExamsAndQuestionsWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                sut.Create(new ExamBuilder("Some-exam", ExamType.Specialization, "Some-category")
                    .WithQuestion("Q1", CorrectAnswer.B)
                    .WithQuestion("Q2", CorrectAnswer.D)
                    .Build()
                );

                var allExams = sut.GetAllExams().ToList();

                sut.GetAllExams().Count().Should().Be(1);

                var apiResponse = sut.HttpDelete(Url, allExams.First().Id);
                var result = sut.GetExamById(allExams.First().Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
                result.Should().BeNull();
                sut.GetAllExams().Count().Should().Be(0);
            }
        }
    }
}
