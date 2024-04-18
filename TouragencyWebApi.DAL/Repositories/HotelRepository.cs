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
    public class HotelRepository: IHotelRepository
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

        public async Task<IEnumerable<Hotel>> GetByTourId(long tourId)
        {
            return await _context.Hotels.Where(h => h.Tours.Any(t => t.Id == tourId)).ToListAsync();
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
        public async Task<IEnumerable<Hotel>> GetByCompositeSearch(string? nameSubstring, string? descriptionSubstring,
            int[]? stars, int? hotelConfigurationId, int? bedConfigurationId, int? settlementId, long? tourId,
            long? bookingId, int? hotelServiceId, long? hotelImageId)
        {
            var hotelCollections = new List<IEnumerable<Hotel>>();

            if (nameSubstring != null)
            {
                var hotelsByName = await GetByNameSubstring(nameSubstring);
                hotelCollections.Add(hotelsByName);
            }
            if(descriptionSubstring != null)
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
            if (tourId != null)
            {
                var hotelsByTourId = await GetByTourId(tourId.Value);
                hotelCollections.Add(hotelsByTourId);
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
