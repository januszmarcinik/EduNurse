using System;
using System.Collections.Generic;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Tests.Integration.Builders
{
    internal class ExamsBuilder
    {
        public Exam BuildOne()
        {
            return GetRandomExam();
        }

        public IEnumerable<Exam> BuildMany(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            for (var i = 0; i < count; i++)
            {
                yield return GetRandomExam();
            }
        }

        private static Exam GetRandomExam()
        {
            return new Exam(
                id: Guid.NewGuid(), 
                name: Guid.NewGuid().ToString(),
                type: ExamType.GeneralKnowledge,
                category: Guid.NewGuid().ToString()
            );
        }
    }
}
