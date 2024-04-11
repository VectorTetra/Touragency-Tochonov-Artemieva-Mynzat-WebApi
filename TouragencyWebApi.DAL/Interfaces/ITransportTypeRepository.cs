﻿using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITransportTypeRepository
    {
        Task<IEnumerable<TransportType>> GetAll();
        Task<TransportType?> GetById(int id);
        Task<IEnumerable<TransportType>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<TransportType>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<TransportType>> GetByTourId(long tourId);
        Task Create(TransportType transportType);
        void Update(TransportType transportType);
        Task Delete(int id);
    }
}
