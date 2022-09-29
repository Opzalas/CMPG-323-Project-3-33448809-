using DeviceManagement_WebApp.Models;
using System;
using System.Collections.Generic;

namespace DeviceManagement_WebApp.Repository
{
    public interface IZoneRepository : IGenericRepository <Zone>
    {
        IEnumerable<Zone> GetZones();
        Zone GetZoneByID(Guid zoneId);
        void InsertZone(Zone zone);
        void DeleteZone(Guid zoneID);
        void UpdateZone(Zone zone);
        void Save();
    }
}
