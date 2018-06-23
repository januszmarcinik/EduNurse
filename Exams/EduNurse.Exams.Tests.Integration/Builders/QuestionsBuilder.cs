using System;
using System.Collections.Generic;
using EduNurse.Exams.Api.Questions;

namespace EduNurse.Exams.Tests.Integration.Builders
{
    internal class QuestionsBuilder
    {
        public Question BuildOne()
        {
            return GetRandomQuestion();
        }

        public IEnumerable<Question> BuildMany(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            for (var i = 0; i < count; i++)
            {
                yield return GetRandomQuestion();
            }
        }

        private static Question GetRandomQuestion()
        {
            return new Question(Guid.NewGuid(), Guid.NewGuid().ToString());
        }
    }
}
