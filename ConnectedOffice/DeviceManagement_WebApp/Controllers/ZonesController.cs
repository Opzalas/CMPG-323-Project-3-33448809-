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

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]
    public class ZonesController : Controller
    {
        private readonly IZoneRepository _zoneRepository;

        public ZonesController(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            return View(_zoneRepository.GetAll());
        }

        // GET: Zone/Details/5
        public ViewResult Details(Guid id)
        {
            Zone zone = _zoneRepository.GetZoneByID(id);
            return View(zone);
        }

        // GET: /Zone/Create
        public ActionResult Create()
        {
            return View(new Zone());
        }
        [HttpPost]
        public ActionResult Create(Zone zone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _zoneRepository.InsertZone(zone);
                    _zoneRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(zone);
        }

        // GET: Zone/Edit/5
        public ActionResult Edit(Guid id)
        {
            Zone zone = _zoneRepository.GetZoneByID(id);
            return View(zone);
        }

        [HttpPost]
        public ActionResult Edit(Zone zone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _zoneRepository.UpdateZone(zone);
                    _zoneRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(zone);
        }


        // GET: /Zone/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, Guid id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Zone zone = _zoneRepository.GetZoneByID(id);
            return View(zone);
        }

        //
        // POST: /Zone/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            try
            {
                Zone zone = _zoneRepository.GetZoneByID(id);
                _zoneRepository.DeleteZone(id);
                _zoneRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}
