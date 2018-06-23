using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using EduNurse.Exams.Api.Questions;
using Xunit;

namespace EduNurse.Exams.Tests.Integration
{
    public class QuestionsControllerTests
    {
        private const string Url = "https://localhost:44311/api/v1/questions";

        [Fact]
        public void WhenCalled_QuestionsGet_QuestionListAreReturnedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = new List<Question>()
                {
                    new Question(Guid.NewGuid(), "test-1"),
                    new Question(Guid.NewGuid(), "test-2")
                };
                sut.SetQuestions(insert);

                var apiResponse = sut.HttpGet(Url);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(new object[]
                {
                    new {insert[0].Id, insert[0].Text},
                    new {insert[1].Id, insert[1].Text},
                }.ToJson());
            }
        }
    }
}
