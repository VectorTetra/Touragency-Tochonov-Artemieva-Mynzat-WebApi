using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly TouragencyContext _context;
        public HotelRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Hotel>> GetAll()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> Get200Last()
        {
            return await _context.Hotels.OrderByDescending(h => h.Id).Take(200).ToListAsync();
        }

        public async Task<Hotel?> GetById(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task<IEnumerable<Hotel>> GetByNameSubstring(string nameSubstring)
        {
            return await _context.Hotels.Where(h => h.Name.Contains(nameSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByDescriptionSubstring(string descriptionSubstring)
        {
            return await _context.Hotels.Where(h => h.Description.Contains(descriptionSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByStars(int[] selectedStarsRatings)
        {
            return await _context.Hotels.Where(h => selectedStarsRatings.Any(sta => sta == h.Stars)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByHotelConfigurationId(int hotelConfigurationId)
        {
            return await _context.Hotels.Where(h => h.HotelConfigurations.Any(bc => bc.Id == hotelConfigurationId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByBedConfigurationId(int bedConfigurationId)
        {
            return await _context.Hotels.Where(h => h.BedConfigurations.Any(bc => bc.Id == bedConfigurationId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetBySettlementId(int settlementId)
        {
            return await _context.Hotels.Where(h => h.Settlement.Id == settlementId).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetBySettlementIds(int[] settlementIds)
        {
            //return await _context.Hotels
            //    .Where(h => settlementIds.Any(id => id == h.Settlement.Id))
            //    .OrderBy(h => h.Settlement.Country.Name)
            //    .ThenBy(h => h.Settlement.Name)
            //    .ThenBy(h => h.Name).ToListAsync();
            return await _context.Hotels
                .Where(s => settlementIds.Contains(s.Settlement.Id))
                .OrderBy(s => s.Settlement.Country.Name)
                .ThenBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByTourNameId(int tourNameId)
        {
            return await _context.Hotels.Where(h => h.TourNames.Any(tn => tn.Id == tourNameId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByTourName(string tourName)
        {
            return await _context.Hotels.Where(h => h.TourNames.Any(tn => tn.Name.Contains(tourName))).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByBookingId(long bookingId)
        {
            return await _context.Hotels.Where(h => h.Bookings.Any(b => b.Id == bookingId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByHotelServiceId(int hotelServiceId)
        {
            return await _context.Hotels.Where(h => h.HotelServices.Any(hs => hs.Id == hotelServiceId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByHotelImageId(long hotelImageId)
        {
            return await _context.Hotels.Where(h => h.HotelImages.Any(hi => hi.Id == hotelImageId)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByCountryNameSubstring(string countryNameSubstring)
        {
            return await _context.Hotels.Where(h => h.Settlement.Country.Name.Contains(countryNameSubstring)).ToListAsync();
        }
        public async Task<IEnumerable<Hotel>> GetBySettlementNameSubstring(string settlementNameSubstring)
        {
            return await _context.Hotels.Where(h => h.Settlement.Name.Contains(settlementNameSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<Hotel>> GetByCompositeSearch(string? nameSubstring, string? countryNameSubstring, string? settlementNameSubstring, string? descriptionSubstring,
            int[]? stars, int? hotelConfigurationId, int? bedConfigurationId, int? settlementId, int? tourNameId, string? tourName,
            long? bookingId, int? hotelServiceId, long? hotelImageId, int[]? settlementIds)
        {
            var hotelCollections = new List<IEnumerable<Hotel>>();

            if (nameSubstring != null)
            {
                var hotelsByName = await GetByNameSubstring(nameSubstring);
                hotelCollections.Add(hotelsByName);
            }

            if (countryNameSubstring != null)
            {
                var hotelsByCountryName = await GetByCountryNameSubstring(countryNameSubstring);
                hotelCollections.Add(hotelsByCountryName);
            }

            if (settlementNameSubstring != null)
            {
                var hotelsBySettlementName = await GetBySettlementNameSubstring(settlementNameSubstring);
                hotelCollections.Add(hotelsBySettlementName);
            }
            if (descriptionSubstring != null)
            {
                var hotelsByDescription = await GetByDescriptionSubstring(descriptionSubstring);
                hotelCollections.Add(hotelsByDescription);
            }
            if (stars != null)
            {
                var hotelsByStars = await GetByStars(stars);
                hotelCollections.Add(hotelsByStars);
            }
            if (hotelConfigurationId != null)
            {
                var hotelsByHotelConfigurationId = await GetByHotelConfigurationId(hotelConfigurationId.Value);
                hotelCollections.Add(hotelsByHotelConfigurationId);
            }
            if (bedConfigurationId != null)
            {
                var hotelsByBedConfigurationId = await GetByBedConfigurationId(bedConfigurationId.Value);
                hotelCollections.Add(hotelsByBedConfigurationId);
            }
            if (settlementId != null)
            {
                var hotelsBySettlementId = await GetBySettlementId(settlementId.Value);
                hotelCollections.Add(hotelsBySettlementId);
            }
            if (tourNameId != null)
            {
                var hotelsByTourNameId = await GetByTourNameId(tourNameId.Value);
                hotelCollections.Add(hotelsByTourNameId);
            }
            if (tourName != null)
            {
                var hotelsByTourName = await GetByTourName(tourName);
                hotelCollections.Add(hotelsByTourName);
            }
            if (bookingId != null)
            {
                var hotelsByBookingId = await GetByBookingId(bookingId.Value);
                hotelCollections.Add(hotelsByBookingId);
            }
            if (hotelServiceId != null)
            {
                var hotelsByHotelServiceId = await GetByHotelServiceId(hotelServiceId.Value);
                hotelCollections.Add(hotelsByHotelServiceId);
            }
            if (hotelImageId != null)
            {
                var hotelsByHotelImageId = await GetByHotelImageId(hotelImageId.Value);
                hotelCollections.Add(hotelsByHotelImageId);
            }
            if (settlementIds != null)
            {
                var hotelsBySettlementIds = await GetBySettlementIds(settlementIds);
                hotelCollections.Add(hotelsBySettlementIds);
            }
            if (!hotelCollections.Any())
            {
                return new List<Hotel>();
            }

            return hotelCollections.Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
        }
        public async Task Create(Hotel hotel)
        {
            await _context.Hotels.AddAsync(hotel);
        }

        public void Update(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
                _context.Hotels.Remove(hotel);
        }
    }
}
