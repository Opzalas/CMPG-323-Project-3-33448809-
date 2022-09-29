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
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        public DevicesController(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        // GET: Devices
        public ActionResult Index()
        {
            var devices = from device in _deviceRepository.GetDevices()
                        select device;
            return View(devices);
        }

        // GET: Devices/Details/5
        public ViewResult Details(Guid id)
        {
            Device device = _deviceRepository.GetDeviceByID(id);
            return View(device);
        }

        // GET: /Device/Create
        public ActionResult Create()
        {
            return View(new Device());
        }

        [HttpPost]
        public ActionResult Create(Device device)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _deviceRepository.InsertDevice(device);
                    _deviceRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(device);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(Guid id)
        {
            Device device = _deviceRepository.GetDeviceByID(id);
            return View(device);
        }
        [HttpPost]
        public ActionResult Edit(Device device)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _deviceRepository.UpdateDevice(device);
                    _deviceRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(device);
        }

        // GET: /Device/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, Guid id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Device device = _deviceRepository.GetDeviceByID(id);
            return View(device);
        }

        //
        // POST: /Device/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            try
            {
                Device device = _deviceRepository.GetDeviceByID(id);
                _deviceRepository.DeleteDevice(id);
                _deviceRepository.Save();
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
