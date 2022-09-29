using DeviceManagement_WebApp.Models;
using System;
using System.Collections.Generic;

namespace DeviceManagement_WebApp.Repository
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        IEnumerable<Device> GetDevices();
        Device GetDeviceByID(Guid deviceId);
        void InsertDevice(Device device);
        void DeleteDevice(Guid deviceID);
        void UpdateDevice(Device device);
        void Save();
    }
}
