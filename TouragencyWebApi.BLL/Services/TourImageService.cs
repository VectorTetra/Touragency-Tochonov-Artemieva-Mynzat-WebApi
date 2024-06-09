using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class TourImageService : ITourImageService
    {
        IUnitOfWork Database;
        MapperConfiguration TourImage_TourImageDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TourImage, TourImageDTO>()
       .ForMember("Id", opt => opt.MapFrom(c => c.Id))
       .ForMember("ImageUrl", opt => opt.MapFrom(c => c.ImageUrl))
       .ForPath(d => d.TourNameId, opt => opt.MapFrom(c => c.TourName.Id))
        );
        public TourImageService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<IEnumerable<TourImageDTO>> GetAll()
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.GetAll());
        }

        public async Task<IEnumerable<TourImageDTO>> Get200Last()
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.Get200Last());
        }
        public async Task<TourImageDTO?> GetById(long id)
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<TourImage, TourImageDTO>(await Database.TourImages.GetById(id));
        }
        public async Task<IEnumerable<TourImageDTO>> GetByTourId(long tourId)
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.GetByTourId(tourId));
        }
        public async Task<IEnumerable<TourImageDTO>> GetByTourNameId(int tourNameId)
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.GetByTourNameId(tourNameId));
        }

        public async Task<IEnumerable<TourImageDTO>> GetByTourName(string tourName)
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.GetByTourName(tourName));
        }
        public async Task<IEnumerable<TourImageDTO>> GetByImageUrlSubstring(string imageUrlSubstring)
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.GetByImageUrlSubstring(imageUrlSubstring));
        }
        public async Task<IEnumerable<TourImageDTO>> GetByCountryName(string countryNameSubstring)
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.GetByCountryName(countryNameSubstring));
        }
        public async Task<IEnumerable<TourImageDTO>> GetBySettlementName(string settlementNameSubstring)
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.GetBySettlementName(settlementNameSubstring));
        }
        public async Task<IEnumerable<TourImageDTO>> GetByHotelName(string hotelNameSubstring)
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.GetByHotelName(hotelNameSubstring));
        }



        public async Task<IEnumerable<TourImageDTO>> GetByCompositeSearch(string? tourName, string? imageUrlSubstring, string? countryNameSubstring,
                       string? settlementNameSubstring, string? hotelNameSubstring, long? tourId, int? tourNameId)
        {
            var mapper = new Mapper(TourImage_TourImageDTOMapConfig);
            return mapper.Map<IEnumerable<TourImage>, List<TourImageDTO>>(await Database.TourImages.GetByCompositeSearch(tourName,imageUrlSubstring, countryNameSubstring, settlementNameSubstring, hotelNameSubstring, tourId, tourNameId));
        }
        public async Task<TourImageDTO> Create(TourImageDTO tourImage)
        {
            var Existed = await Database.TourImages.GetByImageUrlSubstring(tourImage.ImageUrl);
            if (Existed.Any(em => em.ImageUrl == tourImage.ImageUrl))
            {
                throw new ValidationException("Таке зображення туру вже існує", "");
            }
            
            TourName? tourName = null;
            if (tourImage.TourNameId != null)
            {
                tourName = await Database.TourNames.GetById((int)tourImage.TourNameId);
            }
            if (tourName == null)
            {
                throw new ValidationException("Такого TourName не існує", "");
            }
            var newTourImage = new TourImage
            {
                ImageUrl = tourImage.ImageUrl,
                TourName = tourName
            };
            await Database.TourImages.Create(newTourImage);
            await Database.Save();
            tourImage.Id = newTourImage.Id;
            return tourImage;
        }
        public async Task<TourImageDTO> Update(TourImageDTO tourImage) 
        {
            var Existed = await Database.TourImages.GetById(tourImage.Id);
            if (Existed == null)
            {
                throw new ValidationException("Зображення туру не знайдено", "");
            }
            TourName? tourName = null;
            if (tourImage.TourNameId != null)
            {
                tourName = await Database.TourNames.GetById((int)tourImage.TourNameId);
            }

            Existed.ImageUrl = tourImage.ImageUrl;
            Existed.TourName = tourName;
            Database.TourImages.Update(Existed);
            await Database.Save();
            return tourImage;
        }
        public async Task<TourImageDTO> Delete(long id)
        {
            var BusyTourImage = await Database.HotelImages.GetById(id);
            if (BusyTourImage == null)
            {
                throw new ValidationException($"Зображення туру з таким id {id} не знайдено", "");
            }
            var dto = await GetById(id);
            await Database.TourImages.Delete(id);
            await Database.Save();
            return dto;
        }
    }
}
