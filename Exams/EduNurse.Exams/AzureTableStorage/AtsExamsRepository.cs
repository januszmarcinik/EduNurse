using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Enums;
using Microsoft.WindowsAzure.Storage.Table;

namespace EduNurse.Exams.AzureTableStorage
{
    internal class AtsExamsRepository : IExamsRepository
    {
        private readonly AtsExamsContext _context;
        private readonly IMapper _mapper;

        public AtsExamsRepository(AtsExamsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Exam> GetByIdAsync(Guid id)
        {
            var questions = await _context.GetListAsync(new TableQuery<AtsQuestion>()
                .Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition(nameof(AtsQuestion.PartitionKey), QueryComparisons.Equal, AtsExamsContext.QuestionsPartitionKey),
                        TableOperators.And,
                        TableQuery.GenerateFilterConditionForGuid(nameof(AtsQuestion.ExamId), QueryComparisons.Equal, id)
                    )
                )
            );

            var exam = await _context.GetSingleAsync(new TableQuery<AtsExam>()
                .Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition(nameof(AtsExam.PartitionKey), QueryComparisons.Equal, AtsExamsContext.ExamsPartitionKey),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition(nameof(AtsExam.RowKey), QueryComparisons.Equal, id.ToString())
                    )
                )
            );

            return exam?.ToDomainModel(questions);
        }

        public async Task<IEnumerable<Exam>> GetAllExamsAsync()
        {
            var exams = await _context.GetListAsync(new TableQuery<AtsExam>()
                .Where(TableQuery.GenerateFilterCondition(nameof(AtsExam.PartitionKey), QueryComparisons.Equal, AtsExamsContext.ExamsPartitionKey))
            );

            return exams.Select(x => x.ToDomainModel());
        }

        public async Task<IEnumerable<string>> GetCategoriesByTypeAsync(ExamType type)
        {
            var exams = await _context.GetListAsync(new TableQuery<AtsExam>()
                .Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition(nameof(AtsExam.PartitionKey), QueryComparisons.Equal, AtsExamsContext.ExamsPartitionKey),
                        TableOperators.And,
                        TableQuery.GenerateFilterConditionForInt(nameof(AtsExam.Type), QueryComparisons.Equal, (int)type)
                    )
                )
            );

            return exams
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public async Task<IEnumerable<Exam>> GetExamsByTypeAndCategoryAsync(ExamType type, string category)
        {
            var query = string.Format("{0} and {1} and {2}",
                TableQuery.GenerateFilterCondition(nameof(AtsExam.PartitionKey), QueryComparisons.Equal, AtsExamsContext.ExamsPartitionKey),
                TableQuery.GenerateFilterConditionForInt(nameof(AtsExam.Type), QueryComparisons.Equal, (int)type),
                TableQuery.GenerateFilterCondition(nameof(AtsExam.Category), QueryComparisons.Equal, category)
            );

            var exams = await _context.GetListAsync(new TableQuery<AtsExam>().Where(query));

            return exams.Select(x => x.ToDomainModel());
        }

        public async Task AddAsync(Exam exam)
        {
            var operation = TableOperation.Insert(_mapper.Map<AtsExam>(exam));
            await _context.Table.ExecuteAsync(operation);

            if (exam.Questions.Count > 0)
            {
                var batch = new TableBatchOperation();
                foreach (var q in exam.Questions)
                {
                    batch.Insert(_mapper.Map<AtsQuestion>(q));
                }

                await _context.Table.ExecuteBatchAsync(batch);
            }
        }

        public async Task UpdateAsync(Exam exam)
        {
            var operation = TableOperation.Replace(_mapper.Map<AtsExam>(exam));
            await _context.Table.ExecuteAsync(operation);

            await DeleteQuestionsByExamId(exam.Id);

            if (exam.Questions.Count > 0)
            {
                var batch = new TableBatchOperation();
                foreach (var q in exam.Questions)
                {
                    batch.Insert(_mapper.Map<AtsQuestion>(q));
                }

                await _context.Table.ExecuteBatchAsync(batch);
            }
        }

        public async Task RemoveAsync(Exam exam)
        {
            var operation = TableOperation.Delete(_mapper.Map<AtsExam>(exam));
            await _context.Table.ExecuteAsync(operation);
            await DeleteQuestionsByExamId(exam.Id);
        }

        private async Task DeleteQuestionsByExamId(Guid id)
        {
            var questions = await _context.GetListAsync(new TableQuery<AtsQuestion>()
                .Where(TableQuery.GenerateFilterCondition(nameof(AtsQuestion.PartitionKey), QueryComparisons.Equal, AtsExamsContext.QuestionsPartitionKey))
                .Where(TableQuery.GenerateFilterConditionForGuid(nameof(AtsQuestion.ExamId), QueryComparisons.Equal, id))
            );

            if (questions.Count > 0)
            {
                var batch = new TableBatchOperation();
                questions.ForEach(x => batch.Delete(x));
                await _context.Table.ExecuteBatchAsync(batch);
            }
        }
    }
}
