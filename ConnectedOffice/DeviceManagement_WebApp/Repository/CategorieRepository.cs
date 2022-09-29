using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DeviceManagement_WebApp.Repository
{
    public class CategorieRepository : GenericRepository<Category>, ICategorieRepository
    {
        private ConnectedOfficeContext _context;
        public CategorieRepository(ConnectedOfficeContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Category.ToList();
        }
        public Category GetCategoryByID(Guid id)
        {
            return _context.Category.Find(id);
        }
        public void InsertCategory(Category category)
        {
            _context.Category.Add(category);
        }
        public void DeleteCategory(Guid categoryID)
        {
            Category category = _context.Category.Find(categoryID);
            _context.Category.Remove(category);
        }
        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
