using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using EduNurse.Exams.Api.Questions;
using EduNurse.Exams.Tests.Integration.Builders;
using Xunit;

namespace EduNurse.Exams.Tests.Integration
{
    public class QuestionsControllerTests
    {
        private const string Url = "https://localhost:44311/api/v1/questions";

        [Fact]
        public void WhenQuestionsNotExists_Get_EmptyListAreReturnedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var apiResponse = sut.HttpGet(Url);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(new object[]{}.ToJson());
            }
        }

        [Fact]
        public void WhenQuestionNotExists_GetWithId_EmptyValueIsReturnedWithStatus204()
        {
            using (var sut = new SystemUnderTest())
            {
                var apiResponse = sut.HttpGet(Url, Guid.NewGuid());

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NoContent);
                apiResponse.Body.Should().BeEmpty();
            }
        }

        [Fact]
        public void WhenQuestionsExists_Get_QuestionListAreReturnedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = sut.SetQuestions(
                    new QuestionsBuilder().BuildMany(2).ToList()
                );

                var apiResponse = sut.HttpGet(Url);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(new object[]
                {
                    new {insert[0].Id, insert[0].Text},
                    new {insert[1].Id, insert[1].Text},
                }.ToJson());
            }
        }

        [Fact]
        public void WhenQuestionsExists_GetWithId_OneAppropriateQuestionIsReturnedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = sut.SetQuestions(
                    new QuestionsBuilder().BuildMany(3).ToList()
                );
                
                var apiResponse = sut.HttpGet(Url, insert[1].Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(
                    new {insert[1].Id, insert[1].Text}.ToJson()
                );
            }
        }

        [Fact]
        public void WhenCalled_Post_QuestionIsCreatedWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = new QuestionsBuilder().BuildOne();

                var apiResponse = sut.HttpPost(Url, insert);
                var result = sut.GetById<Question>(insert.Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
                result.Should().BeEquivalentTo(insert);
            }
        }

        [Fact]
        public void WhenCalled_Put_QuestionIsModifiedWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = sut.Create(new QuestionsBuilder().BuildOne());
                insert = new Question(insert.Id, Guid.NewGuid().ToString());

                var apiResponse = sut.HttpPut(Url, insert.Id, insert);
                var result = sut.GetById<Question>(insert.Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
                result.Should().NotBe(insert);
            }
        }

        [Fact]
        public void WhenCalled_Delete_QuestionIsDeletedWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = sut.Create(new QuestionsBuilder().BuildOne());

                var apiResponse = sut.HttpDelete(Url, insert.Id);

                Thread.Sleep(1000);
                var result = sut.GetById<Question>(insert.Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
                result.Should().BeNull();
            }
        }
    }
}
