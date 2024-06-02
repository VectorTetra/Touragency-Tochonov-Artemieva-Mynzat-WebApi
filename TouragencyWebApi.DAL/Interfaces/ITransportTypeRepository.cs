using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface ITransportTypeRepository
    {
        Task<IEnumerable<TransportType>> GetAll();
        Task<TransportType?> GetById(int id);
        Task<IEnumerable<TransportType>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<TransportType>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<TransportType>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<TransportType>> GetByTourName(string tourName);
        Task<IEnumerable<TransportType>> GetByCompositeSearch(string? nameSubstring, string? descriptionSubstring,
           int? tourNameId, string? tourname);
        Task Create(TransportType transportType);
        void Update(TransportType transportType);
        Task Delete(int id);
    }
}
