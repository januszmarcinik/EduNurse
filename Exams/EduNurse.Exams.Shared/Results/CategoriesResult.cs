using System.Collections.Generic;

namespace EduNurse.Exams.Shared.Results
{
    public class CategoriesResult : IResult
    {
        public IEnumerable<Category> Categories { get; }

        public CategoriesResult(IEnumerable<Category> categories)
        {
            Categories = categories;
        }

        public class Category
        {
            public Category(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }
    }
}
