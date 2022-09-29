using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        private ConnectedOfficeContext _context;
        public DeviceRepository(ConnectedOfficeContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Device> GetDevices()
        {
            return _context.Device.ToList();
        }
        public Device GetDeviceByID(Guid id)
        {
            return _context.Device.Find(id);
        }
        public void InsertDevice(Device device)
        {
            _context.Device.Add(device);
        }
        public void DeleteDevice(Guid deviceID)
        {
            Device device = _context.Device.Find(deviceID);
            _context.Device.Remove(device);
        }
        public void UpdateDevice(Device device)
        {
            _context.Entry(device).State = EntityState.Modified;
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
