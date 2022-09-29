using DeviceManagement_WebApp.Models;
using System;
using System.Collections.Generic;

namespace DeviceManagement_WebApp.Repository
{
    public interface ICategorieRepository : IGenericRepository<Category>
    {

        IEnumerable<Category> GetCategories();
        Category GetCategoryByID(Guid categoryId);
        void InsertCategory(Category category);
        void DeleteCategory(Guid categoryID);
        void UpdateCategory(Category category);
        void Save();
    }
}
