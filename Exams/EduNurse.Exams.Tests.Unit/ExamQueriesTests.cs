using System;
using System.Collections.Generic;
using EduNurse.Exams.Entities;
using EduNurse.Exams.QueryHandlers;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Shared.Queries;
using EduNurse.Exams.Shared.Results;
using EduNurse.Exams.Tests.Shared;
using EduNurse.Exams.Tests.Shared.Extensions;
using FluentAssertions;
using Xunit;

namespace EduNurse.Exams.Tests.Unit
{
    public class ExamQueriesTests
    {
        [Fact]
        public async void GetCategoriesByType_WhenExists_ReturnsDistincted()
        {
            using (var fixture = new ExamsFixture())
            {
                var exams = new List<Exam>()
                {
                    new ExamBuilder("First Exam", ExamType.GeneralKnowledge, "Kardiologia").Build(),
                    new ExamBuilder("Second Exam", ExamType.GeneralKnowledge, "Interna").Build(),
                    new ExamBuilder("Third Exam", ExamType.Specialization, "Kardiologia").Build(),
                    new ExamBuilder("Fourth Exam", ExamType.GeneralKnowledge, "Kardiologia").Build(),
                    new ExamBuilder("Fifth Exam", ExamType.GeneralKnowledge, "Interna").Build(),
                    new ExamBuilder("Sixth Exam", ExamType.Specialization, "Urologia").Build(),
                    new ExamBuilder("Seventh Exam", ExamType.GeneralKnowledge, "Nefrologia").Build()
                };
                await fixture.AddMany(exams);

                var query = new GetCategoriesByTypeQuery {Type = ExamType.GeneralKnowledge};
                var handler = new GetCategoriesByTypeQueryHandler(fixture.Repository);
                var result = await handler.HandleAsync(query);

                result.Should().BeEquivalentTo(new CategoriesResult(
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
        public async void GetExamsByTypeAndCategory_WhenExists_ReturnsResult()
        {
            using (var fixture = new ExamsFixture())
            {
                var exams = new List<Exam>
                {
                    new ExamBuilder("First Exam", ExamType.GeneralKnowledge, "Kardiologia").Build(),
                    new ExamBuilder("Second Exam", ExamType.GeneralKnowledge, "Interna").Build(),
                    new ExamBuilder("Third Exam", ExamType.Specialization, "Kardiologia").Build()
                };
                await fixture.AddMany(exams);
                var expected = new ExamsResult(new[] { exams[1].ToExamResult() });

                var query = new GetExamsByTypeAndCategoryQuery()
                {
                    Category = "Interna",
                    Type = ExamType.GeneralKnowledge
                };
                var handler = new GetExamsByTypeAndCategoryQueryHandler(fixture.Repository, fixture.Mapper);
                var result = await handler.HandleAsync(query);

                result.Should().BeEquivalentTo(expected);
            }
        }

        [Fact]
        public async void GetExamById_WhenExamNotExists_EmptyValueIsReturned()
        {
            using (var fixture = new ExamsFixture())
            {
                var query = new GetExamByIdQuery { Id = Guid.NewGuid() };
                var handler = new GetExamByIdQueryHandler(fixture.Repository, fixture.Mapper);
                var result = await handler.HandleAsync(query);

                result.Should().BeNull();
            }
        }

        [Fact]
        public async void GetExamById_WhenExamsExists_ExamIsReturned()
        {
            using (var fixture = new ExamsFixture())
            {
                var exams = new List<Exam>
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
                };
                await fixture.AddMany(exams);
                var expected = exams[1].ToExamWithQuestionsResult();

                var query = new GetExamByIdQuery { Id = exams[1].Id };
                var handler = new GetExamByIdQueryHandler(fixture.Repository, fixture.Mapper);
                var result = await handler.HandleAsync(query);

                result.Should().BeEquivalentTo(expected);
            }
        }
    }
}
