using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.DAL.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly TouragencyContext _context;

        public TourRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tour>> GetAll()
        {
            return await _context.Tours.ToListAsync();
        }
        public async Task<IEnumerable<Tour>> Get200Last()
        {
            return await _context.Tours.OrderByDescending(t => t.Id).Take(200).ToListAsync();
        }
        public async Task<Tour?> GetById(long id)
        {
            return await _context.Tours.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<IEnumerable<Tour>> GetByTourName(TourName tourName)
        {
            return await _context.Tours.Where(t => t.Name == tourName).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTourNameId(int tourNameId)
        {
            return await _context.Tours.Where(t => t.Name.Id == tourNameId).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTourNameStringName(string tourNameSubstring)
        {
            return await _context.Tours.Where(t => t.Name.Name.Contains(tourNameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByCountry(Country country)
        {
            //return await _context.Tours.Where(t => t.Settlements.Where(c => c.Country.Id == country.Id)).ToListAsync();
            return await _context.Tours.Where(t => t.Name.Countries.Any(c => c.Id == country.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByCountryId(int countryId)
        {
            return await _context.Tours.Where(t => t.Name.Countries.Any(c => c.Id == countryId)).ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetByContinentId(int continentId)
        {
            return await _context.Tours.Where(t => t.Name.Countries.Any(c => c.Continent.Id == continentId)).ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetByContinentName(string continentName)
        {
            return await _context.Tours.Where(t => t.Name.Countries.Any(c => c.Continent.Name.Contains(continentName))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByCountryName(string countryNameSubstring)
        {
            return await _context.Tours.Where(t => t.Name.Countries.Any(c => c.Name.Contains(countryNameSubstring))).ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetByStars(int[] stars)
        {
            return await _context.Tours.Where(t => t.Name.Hotels.Any(hotel => stars.Any(star => star == hotel.Stars))).ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetBySettlement(Settlement settlement)
        {
            return await _context.Tours.Where(t => t.Name.Settlements.Any(s => s.Id == settlement.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetBySettlementId(int settlementId)
        {
            return await _context.Tours.Where(t => t.Name.Settlements.Any(c => c.Id == settlementId)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetBySettlementName(string settlementNameSubstring){
            return await _context.Tours.Where(t => t.Name.Settlements.Any(c => c.Name.Contains(settlementNameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByHotel(Hotel hotel)
        {
            return await _context.Tours.Where(t => t.Name.Hotels.Any(h => h.Id == hotel.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByHotelName(string hotelNameSubstring){
            return await _context.Tours.Where(t => t.Name.Hotels.Any(c => c.Name.Contains(hotelNameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByHotelId(int hotelId)
        {
            return await _context.Tours.Where(t => t.Name.Hotels.Any(h => h.Id == hotelId)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Tours.Where(t => t.ArrivalDate >= startDate && t.DepartureDate <= endDate).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTourDuration(int[] durationDays)
        {
            return await _context.Tours.Where(t => durationDays.Contains((t.DepartureDate - t.ArrivalDate).Days)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByHotelServicesIds(int[] hotelServicesIds)
        {
            return await _context.Tours.Where(t => t.Name.Hotels.Any(h => h.HotelServices.Any(s => hotelServicesIds.Contains(s.Id)))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTransportType(TransportType transportType)
        {
            return await _context.Tours.Where(t => t.Name.TransportTypes.Any(tt => tt.Id == transportType.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTransportTypeId(int id){
            
            return await _context.Tours.Where(t => t.Name.TransportTypes.Any(tt => tt.Id == id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTransportTypeName(string transportTypeNameSubstring)
        {
            return await _context.Tours.Where(t => t.Name.TransportTypes.Any(tt => tt.Name.Contains(transportTypeNameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTourStateId(int tourStateId)
        {
            return await _context.Tours.Where(t => t.TourState.Id == tourStateId).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTouristNickname(string touristNickname)
        {
            return await _context.Tours.Where(t => t.Clients.Any(tourist => tourist.TouristNickname.Contains(touristNickname))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByClientFirstname(string clientFirstname)
        {
            return await _context.Tours.Where(t => t.Clients.Any(client => client.Person.Firstname.Contains(clientFirstname))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByClientLastname(string clientLastname)
        {
            return await _context.Tours.Where(t => t.Clients.Any(client => client.Person.Lastname.Contains(clientLastname))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByClientMiddlename(string clientMiddlename)
        {
            return await _context.Tours.Where(t => t.Clients.Any(client => client.Person.Middlename.Contains(clientMiddlename))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByClientId(int clientId)
        {
            return await _context.Tours.Where(t => t.Clients.Any(client => client.Id == clientId)).ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetByCompositeSearch(int? tourNameId, int? countryId, int? settlementId, int? hotelId,
            DateTime? startDate, DateTime? endDate, int[]? durationDays, int[]? hotelServicesIds, int? transportTypeId, int? tourStateId,
            string? touristNickname, string? clientFirstname, string? clientLastname, string? clientMiddlename, string? countryName, string? settlementName,
            string? hotelName, int? continentId, string? continentName, int[]? stars, int? clientId)
        {
            var tourCollections = new List<IEnumerable<Tour>>();

            if (tourNameId != null)
            {
                var hotelsByHotelServiceId = await GetByTourNameId(tourNameId.Value);
                tourCollections.Add(hotelsByHotelServiceId);
            }
            if (countryId != null)
            {
                var hotelsByCountryId = await GetByCountryId(countryId.Value);
                tourCollections.Add(hotelsByCountryId);
            }
            if (settlementId != null)
            {
                var hotelsBySettlementId = await GetBySettlementId(settlementId.Value);
                tourCollections.Add(hotelsBySettlementId);
            }
            if (hotelId != null)
            {
                var hotelsByHotelId = await GetByHotelId(hotelId.Value);
                tourCollections.Add(hotelsByHotelId);
            }
            if (startDate != null && endDate != null)
            {
                var hotelsByDateRange = await GetByDateRange(startDate.Value, endDate.Value);
                tourCollections.Add(hotelsByDateRange);
            }
            if (durationDays != null)
            {
                var hotelsByDuration = await GetByTourDuration(durationDays);
                tourCollections.Add(hotelsByDuration);
            }
            if (hotelServicesIds != null)
            {
                var hotelsByHotelServicesIds = await GetByHotelServicesIds(hotelServicesIds);
                tourCollections.Add(hotelsByHotelServicesIds);
            }
            if (transportTypeId != null)
            {
                var hotelsByTransportTypeId = await GetByTransportTypeId(transportTypeId.Value);
                tourCollections.Add(hotelsByTransportTypeId);
            }
            if (tourStateId != null)
            {
                var hotelsByTourStateId = await GetByTourStateId(tourStateId.Value);
                tourCollections.Add(hotelsByTourStateId);
            }
            if (touristNickname != null)
            {
                var hotelsByTouristNickname = await GetByTouristNickname(touristNickname);
                tourCollections.Add(hotelsByTouristNickname);
            }
            if (clientFirstname != null)
            {
                var hotelsByClientFirstname = await GetByClientFirstname(clientFirstname);
                tourCollections.Add(hotelsByClientFirstname);
            }
            if (clientLastname != null)
            {
                var hotelsByClientLastname = await GetByClientLastname(clientLastname);
                tourCollections.Add(hotelsByClientLastname);
            }
            if (clientMiddlename != null)
            {
                var hotelsByClientMiddlename = await GetByClientMiddlename(clientMiddlename);
                tourCollections.Add(hotelsByClientMiddlename);
            }
            if (countryName != null)
            {
                var hotelsByCountryName = await GetByCountryName(countryName);
                tourCollections.Add(hotelsByCountryName);
            }
            if (settlementName != null)
            {
                var hotelsBySettlementName = await GetBySettlementName(settlementName);
                tourCollections.Add(hotelsBySettlementName);
            }
            if (hotelName != null)
            {
                var hotelsByHotelName = await GetByHotelName(hotelName);
                tourCollections.Add(hotelsByHotelName);
            }
            if (continentId != null)
            {
                var hotelsByContinentId = await GetByContinentId(continentId.Value);
                tourCollections.Add(hotelsByContinentId);
            }
            if (continentName != null)
            {
                var hotelsByContinentName = await GetByContinentName(continentName);
                tourCollections.Add(hotelsByContinentName);
            }
            if (stars != null)
            {
                var hotelsByStars = await GetByStars(stars);
                tourCollections.Add(hotelsByStars);
            }
            if (clientId != null)
            {
                var hotelsByClientId = await GetByClientId(clientId.Value);
                tourCollections.Add(hotelsByClientId);
            }
            if (!tourCollections.Any())
            {
                return new List<Tour>();
            }

            return tourCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }
        public async Task Create(Tour tour)
        {
            await _context.Tours.AddAsync(tour);
        }
        public void Update(Tour tour)
        {
            _context.Entry(tour).State = EntityState.Modified;
        }
        public async Task Delete(long id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
            }
        }
    }
}
