using FluentAssertions;
using System;
using System.Linq;
using System.Net;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Tests.Integration.Builders;
using Xunit;

namespace EduNurse.Exams.Tests.Integration
{
    public class ExamsControllerTests
    {
        private const string Url = "https://localhost:44311/api/v1/exams";

        [Fact]
        public void WhenExamsNotExists_Get_EmptyListAreReturnedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var apiResponse = sut.HttpGet(Url);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(new object[]{}.ToJson());
            }
        }

        [Fact]
        public void WhenExamNotExists_GetWithId_EmptyValueIsReturnedWithStatus204()
        {
            using (var sut = new SystemUnderTest())
            {
                var apiResponse = sut.HttpGet(Url, Guid.NewGuid());

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NoContent);
                apiResponse.Body.Should().BeEmpty();
            }
        }

        [Fact]
        public void WhenExamsExists_Get_ExamListAreReturnedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = sut.CreateMany(
                    new ExamsBuilder().BuildMany(2).ToList()
                );

                var apiResponse = sut.HttpGet(Url);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(new object[]
                {
                    new { insert[0].Id, insert[0].Name, insert[0].Type, insert[0].Category },
                    new { insert[1].Id, insert[1].Name, insert[1].Type, insert[1].Category }
                }.ToJson());
            }
        }

        [Fact]
        public void WhenExamsExists_GetWithId_OneAppropriateExamIsReturnedWithStatus200()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = sut.CreateMany(
                    new ExamsBuilder().BuildMany(3).ToList()
                );
                
                var apiResponse = sut.HttpGet(Url, insert[1].Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
                apiResponse.Body.Should().BeEquivalentTo(
                    new { insert[1].Id, insert[1].Name, insert[1].Type, insert[1].Category }.ToJson()
                );
            }
        }

        [Fact]
        public void WhenCalled_Post_ExamIsCreatedWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = new ExamsBuilder().BuildOne();

                var apiResponse = sut.HttpPost(Url, insert);
                var result = sut.GetById<Exam>(insert.Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
                result.Should().BeEquivalentTo(insert);
            }
        }

        [Fact]
        public void WhenCalled_Put_ExamIsModifiedWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = sut.Create(new ExamsBuilder().BuildOne());
                insert = new Exam(
                    id: insert.Id, 
                    name: Guid.NewGuid().ToString(),
                    type: ExamType.GeneralKnowledge,
                    category: Guid.NewGuid().ToString()
                );

                var apiResponse = sut.HttpPut(Url, insert.Id, insert);
                var result = sut.GetById<Exam>(insert.Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
                result.Should().NotBe(insert);
            }
        }

        [Fact]
        public void WhenCalled_Delete_ExamIsDeletedWithStatus202()
        {
            using (var sut = new SystemUnderTest())
            {
                var insert = sut.Create(new ExamsBuilder().BuildOne());

                var apiResponse = sut.HttpDelete(Url, insert.Id);
                var result = sut.GetById<Exam>(insert.Id);

                apiResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Accepted);
                result.Should().BeNull();
            }
        }
    }
}
