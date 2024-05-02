using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.DAL.Repositories
{
    public class TourImageRepository: ITourImageRepository
    {
        private readonly TouragencyContext _context;
        public TourImageRepository(TouragencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourImage>> GetAll()
        {
            return await _context.TourImages.ToListAsync();
        }

        public async Task<IEnumerable<TourImage>> Get200Last()
        {
            return await _context.TourImages.OrderByDescending(ti => ti.Id).Take(200).ToListAsync();
        }

        public async Task<TourImage?> GetById(long id)
        {
            return await _context.TourImages.FindAsync(id);
        }

        public async Task<IEnumerable<TourImage>> GetByTourId(long tourId)
        {
            return await _context.TourImages.Where(ti => ti.TourName.Tours.Any(tt => tt.Id == tourId)).ToListAsync();
        }

        public async Task<IEnumerable<TourImage>> GetByTourNameId(int tourNameId)
        {
            return await _context.TourImages.Where(ti => ti.TourName.Id == tourNameId).ToListAsync();
        }

        public async Task<IEnumerable<TourImage>> GetByTourName(string tourName)
        {
            return await _context.TourImages.Where(ti => ti.TourName.Name.Contains(tourName)).ToListAsync();
        }
        public async Task<IEnumerable<TourImage>> GetByImageUrlSubstring(string imageUrlSubstring)
        {
            return await _context.TourImages.Where(ti => ti.ImageUrl.Contains(imageUrlSubstring)).ToListAsync();
        }   

        public async Task<IEnumerable<TourImage>> GetByCountryName(string countryNameSubstring)
        {
            return await _context.TourImages.Where(ti => ti.TourName.Settlements.Any(sss => sss.Country.Name.Contains(countryNameSubstring))).ToListAsync();
        }

        public async Task<IEnumerable<TourImage>> GetBySettlementName(string settlementNameSubstring)
        {
            return await _context.TourImages.Where(ti => ti.TourName.Settlements.Any(sss => sss.Name.Contains(settlementNameSubstring))).ToListAsync();
        }

        public async Task<IEnumerable<TourImage>> GetByHotelName(string hotelNameSubstring)
        {
            return await _context.TourImages.Where(ti => ti.TourName.Hotels.Any(h => h.Name.Contains(hotelNameSubstring))).ToListAsync();
        }

        public async Task<IEnumerable<TourImage>> GetByCompositeSearch(string? tourName, string? imageUrlSubstring, string? countryNameSubstring,
                       string? settlementNameSubstring, string? hotelNameSubstring, long? tourId, int? tourNameId)
        {
            var collections = new List<IEnumerable<TourImage>>();
            if (tourName != null)
            {
                collections.Add(await GetByTourName(tourName));
            }
            if (imageUrlSubstring != null)
            {
                collections.Add(await GetByImageUrlSubstring(imageUrlSubstring));
            }
            if (countryNameSubstring != null)
            {
                collections.Add(await GetByCountryName(countryNameSubstring));
            }
            if (settlementNameSubstring != null)
            {
                collections.Add(await GetBySettlementName(settlementNameSubstring));
            }
            if (hotelNameSubstring != null)
            {
                collections.Add(await GetByHotelName(hotelNameSubstring));
            }
            if (tourId != null)
            {
                collections.Add(await GetByTourId(tourId.Value));
            }
            if (tourNameId != null)
            {
                collections.Add(await GetByTourNameId(tourNameId.Value));
            }
            if (!collections.Any())
            {
                return new List<TourImage>();
            }
            return collections.Aggregate((a, b) => a.Intersect(b));
        }
        public async Task Create(TourImage tourImage)
        {
            await _context.TourImages.AddAsync(tourImage);
        }

        public void Update(TourImage tourImage)
        {
            _context.Entry(tourImage).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            TourImage tourImage = await GetById(id);
            if (tourImage != null)
            {
                _context.TourImages.Remove(tourImage);
            }
        }
    }
}
