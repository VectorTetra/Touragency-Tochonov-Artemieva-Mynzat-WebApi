﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAll();
        Task<Hotel?> GetById(int id);
        Task<IEnumerable<Hotel>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<Hotel>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<Hotel>> GetByStars(int[] selectedStarsRatings);
        Task<IEnumerable<Hotel>> GetByHotelConfigurationId(int hotelConfigurationId);
        Task<IEnumerable<Hotel>> GetByBedConfigurationId(int bedConfigurationId);
        Task<IEnumerable<Hotel>> GetBySettlementId(int settlementId);
        Task<IEnumerable<Hotel>> GetByTourId(long tourId);
        Task<IEnumerable<Hotel>> GetByBookingId(long bookingId);
        Task<IEnumerable<Hotel>> GetByHotelServiceId(int hotelServiceId);
        Task<IEnumerable<Hotel>> GetByHotelImageId(long hotelImageId);
        Task<IEnumerable<Hotel>> GetByCompositeSearch(string? nameSubstring, string? descriptionSubstring,
            int[]? stars, int? hotelConfigurationId, int? bedConfigurationId, int? settlementId, long? tourId,
            long? bookingId, int? hotelServiceId, long? hotelImageId);
        Task Create(Hotel hotel);
        void Update(Hotel hotel);
        Task Delete(int id);
    }
}
