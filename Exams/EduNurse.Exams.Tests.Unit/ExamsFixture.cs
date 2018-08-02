﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using AutoMapper;
using EduNurse.Exams.Entities;
using Moq;

namespace EduNurse.Exams.Tests.Unit
{
    internal class ExamsFixture : IDisposable
    {
        public IExamsRepository Repository { get; }
        public IPrincipal User { get; }
        public IMapper Mapper { get; }

        public ExamsFixture()
        {
            Repository = new FakeExamsRepository();

            var principalMock = new Mock<IPrincipal>();
            principalMock.SetupGet(x => x.Identity.Name).Returns(default(string));
            User = principalMock.Object;

            Mapper = new MapperConfiguration(x => x.AddProfile<AutoMapperProfile>()).CreateMapper();
        }

        public void AddMany(IEnumerable<Exam> exams)
        {
            foreach (var exam in exams)
            {
                Repository.Add(exam);
            }
        }

        public List<Exam> GetAllExams()
        {
            var repository = Repository as FakeExamsRepository;
            return repository.Exams.ToList();
        }

        public void Dispose()
        {
        }
    }
}
