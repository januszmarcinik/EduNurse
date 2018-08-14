using System;
using System.Collections.Generic;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Enums;
using Microsoft.WindowsAzure.Storage.Table;

namespace EduNurse.Exams.AzureTableStorage
{
    internal class AtsExam : TableEntity
    {
        public AtsExam() { }

        public string Name { get; set; }
        public int Type { get; set; }
        public string Category { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsConfirmed { get; set; }

        public Exam ToDomainModel(List<AtsQuestion> questions)
        {
            var exam = ToDomainModel();
            questions.ForEach(q => exam.AddQuestion(q.Order, q.Text, q.A, q.B, q.C, q.D, (CorrectAnswer)q.CorrectAnswer, q.Explanation));
            return exam;
        }

        public Exam ToDomainModel()
        {
            return new Exam(Guid.Parse(RowKey), Name, (ExamType)Type, Category, CreatedBy, CreatedDate.ToLocalTime(), IsConfirmed);
        }
    }
}
