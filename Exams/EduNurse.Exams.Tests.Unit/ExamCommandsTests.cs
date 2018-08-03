using System.Linq;
using EduNurse.Exams.CommandHandlers;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Commands;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Tests.Shared;
using EduNurse.Exams.Tests.Shared.Extensions;
using FluentAssertions;
using Xunit;

namespace EduNurse.Exams.Tests.Unit
{
    public class ExamCommandsTests
    {
        [Fact]
        public async void AddExamWithQuestions_WhenCalled_CorrectlyCreatesObjects()
        {
            using (var fixture = new ExamsFixture())
            {
                var exam = new ExamBuilder("Some-exam", ExamType.Specialization, "Some-category")
                    .WithQuestion("Q1", CorrectAnswer.A)
                    .WithQuestion("Q2", CorrectAnswer.B)
                    .WithQuestion("Q3", CorrectAnswer.C)
                    .Build();

                var command = exam.ToAddExamCommand();
                var handler = new AddExamCommandHandler(fixture.Repository, fixture.User);
                await handler.HandleAsync(command);

                var allExams = fixture.GetAllExams();

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
        public async void EditExamWithQuestions_WhenExamExists_CorrectlyModifyExamAndQuestions()
        {
            using (var fixture = new ExamsFixture())
            {
                await fixture.Repository.AddAsync(new ExamBuilder("Some-exam", ExamType.Specialization, "Some-category")
                    .WithQuestion("Q1", CorrectAnswer.A)
                    .WithQuestion("Q2", CorrectAnswer.B)
                    .WithQuestion("Q3", CorrectAnswer.C)
                    .Build()
                );
                var original = fixture.GetAllExams().First();
                var modified = original.DeepClone();
                   
                modified.SetName("Different name");
                modified.SetCategory("Interna");
                modified.ChangeType(ExamType.GeneralKnowledge);

                modified.RemoveQuestion(modified.Questions.Last().Id);
                modified.AddQuestion(3, "text", "a", "b", "c", "d", CorrectAnswer.D, "expl");
                modified.AddQuestion(4, "text-second", "e", "f", "g", "h", CorrectAnswer.A, "testing");
                modified.Questions.First().SetText("Changed text");

                var command = modified.ToEditExamCommand();
                var handler = new EditExamCommandHandler(fixture.Repository, modified.Id);
                await handler.HandleAsync(command);

                var result = await fixture.Repository.GetByIdAsync(modified.Id);

                result.Should().NotBe(original);
                result.Questions.Count.Should().Be(4);
                result.Questions.Should().NotBeEquivalentTo(original.Questions);
            }
        }

        [Fact]
        public async void RemoveExamWithQuestions_WhenExamExists_DeleteExamsAndQuestions()
        {
            using (var fixture = new ExamsFixture())
            {
                await fixture.Repository.AddAsync(new ExamBuilder("Some-exam", ExamType.Specialization, "Some-category")
                    .WithQuestion("Q1", CorrectAnswer.B)
                    .WithQuestion("Q2", CorrectAnswer.D)
                    .Build()
                );

                var allExams = fixture.GetAllExams();
                var examsCountBeforeDelete = allExams.Count;

                var command = new DeleteExamCommand();
                var handler = new DeleteExamCommandHandler(fixture.Repository, allExams.First().Id);
                await handler.HandleAsync(command);

                var result = await fixture.Repository.GetByIdAsync(allExams.First().Id);

                examsCountBeforeDelete.Should().Be(1);
                result.Should().BeNull();
                fixture.GetAllExams().Count.Should().Be(0);
            }
        }
    }
}
