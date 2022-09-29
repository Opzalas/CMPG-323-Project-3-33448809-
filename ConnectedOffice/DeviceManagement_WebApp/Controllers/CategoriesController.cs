using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using DeviceManagement_WebApp.Repository;
using System.Data;
using System.Security.Cryptography;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategorieRepository _categorieRepository;
        public CategoriesController(ICategorieRepository categorieRepository)
        {
            _categorieRepository = categorieRepository;
        }


        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(_categorieRepository.GetAll());
        }

        // GET: Categorie/Details/5
        public ViewResult Details(Guid id)
        {
            Category category = _categorieRepository.GetCategoryByID(id);
            return View(category);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categorieRepository.InsertCategory(category);
                    _categorieRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(Guid id)
        {
            Category category = _categorieRepository.GetCategoryByID(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categorieRepository.UpdateCategory(category);
                    _categorieRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(category);
        }

        
        // GET: /Category/Delete/5

        public ActionResult DeleteCategory(Guid id )
        {
            Category category = _categorieRepository.GetCategoryByID(id);
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            try
            {
                Category category = _categorieRepository.GetCategoryByID(id);
                _categorieRepository.DeleteCategory(id);
                _categorieRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //_deviceRepository.DeleteDevice();
            base.Dispose(disposing);
        }

    }
}
