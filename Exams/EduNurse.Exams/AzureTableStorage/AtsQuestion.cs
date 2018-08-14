using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace EduNurse.Exams.AzureTableStorage
{
    internal class AtsQuestion : TableEntity
    {
        public AtsQuestion() { }
        public AtsQuestion(Guid id) : base(AtsExamsContext.QuestionsPartitionKey, id.ToString()) { }

        public Guid Id { get; set; }
        public Guid ExamId { get; set; }

        public int Order { get; set; }
        public string Text { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public int CorrectAnswer { get; set; }
        public string Explanation { get; set; }
    }
}
