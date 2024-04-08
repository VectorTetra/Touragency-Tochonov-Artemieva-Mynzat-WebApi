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
            return await _context.Tours.Where(t => t.Settlements.Any(c => c.Country.Id == country.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByCountryId(int countryId)
        {
            return await _context.Tours.Where(t => t.Settlements.Any(c => c.Country.Id == countryId)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByCountryName(string countryNameSubstring)
        {
            return await _context.Tours.Where(t => t.Settlements.Any(c => c.Country.Name.Contains(countryNameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetBySettlement(Settlement settlement)
        {
            return await _context.Tours.Where(t => t.Settlements.Any(s => s.Id == settlement.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetBySettlementId(int settlementId)
        {
            return await _context.Tours.Where(t => t.Settlements.Any(c => c.Id == settlementId)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetBySettlementName(string settlementNameSubstring){
            return await _context.Tours.Where(t => t.Settlements.Any(c => c.Name.Contains(settlementNameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByHotel(Hotel hotel)
        {
            return await _context.Tours.Where(t => t.Hotels.Any(h => h.Id == hotel.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByHotelName(string hotelNameSubstring){
            return await _context.Tours.Where(t => t.Hotels.Any(c => c.Name.Contains(hotelNameSubstring))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByHotelId(int hotelId)
        {
            return await _context.Tours.Where(t => t.Hotels.Any(h => h.Id == hotelId)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Tours.Where(t => t.ArrivalDate >= startDate && t.DepartureDate <= endDate).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTourDuration(params int[] durationDays)
        {
            return await _context.Tours.Where(t => durationDays.Contains((t.DepartureDate - t.ArrivalDate).Days)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByHotelServicesIds(params int[] hotelServicesIds)
        {
            return await _context.Tours.Where(t => t.Hotels.Any(h => h.HotelServices.Any(s => hotelServicesIds.Contains(s.Id)))).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTransportType(TransportType transportType)
        {
            return await _context.Tours.Where(t => t.TransportTypes.Any(tt => tt.Id == transportType.Id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTransportTypeId(int id){
            
            return await _context.Tours.Where(t => t.TransportTypes.Any(tt => tt.Id == id)).ToListAsync();
        }
        public async Task<IEnumerable<Tour>> GetByTransportTypeName(string transportTypeNameSubstring)
        {
            return await _context.Tours.Where(t => t.TransportTypes.Any(tt => tt.Name.Contains(transportTypeNameSubstring))).ToListAsync();
        }
        public async Task Create(Tour tour)
        {
            await _context.Tours.AddAsync(tour);
        }
        public void Update(Tour tour)
        {
            _context.Entry(tour).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
            }
        }
    }
}
