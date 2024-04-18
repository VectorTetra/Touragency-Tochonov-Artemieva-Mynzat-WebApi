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
    public class HotelImageService : IHotelImageService
    {
        IUnitOfWork Database;
        public HotelImageService(IUnitOfWork uow)
        {
            Database = uow;
        }

        MapperConfiguration HotelImage_HotelImageDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<HotelImage, HotelImageDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("ImageUrl", opt => opt.MapFrom(c => c.ImageUrl))
        .ForPath(d => d.HotelId, opt => opt.MapFrom(c => c.Hotel.Id))
        );
        public async Task Create(HotelImageDTO hotelImageDTO)
        {
            var BusyHotelImageUrl = await Database.HotelImages.GetByImageUrlSubstring(hotelImageDTO.ImageUrl);
            if (BusyHotelImageUrl.Any(hi => hi.ImageUrl == hotelImageDTO.ImageUrl))
            {
                throw new ValidationException("Таке зображення готелю вже існує", "");
            }
            var BusyHotelImageId = await Database.HotelImages.GetById(hotelImageDTO.Id);
            if (BusyHotelImageId != null)
            {
                throw new ValidationException("Зображення готелю з таким ID вже існує", "");
            }
            Hotel? hotel = null;
            if (hotelImageDTO.HotelId != null)
            {
                hotel = await Database.Hotels.GetById((int)hotelImageDTO.HotelId);
                if (hotel == null)
                {
                    throw new ValidationException("Готель не знайдено", "");
                }
            }
            var newHotelImage = new HotelImage
            {
                ImageUrl = hotelImageDTO.ImageUrl,
                Hotel = hotel
            };
            await Database.HotelImages.Create(newHotelImage);
            await Database.Save();
        }
        public async Task Update(HotelImageDTO hotelImageDTO)
        {
            var BusyHotelImage = await Database.HotelImages.GetById(hotelImageDTO.Id);
            if (BusyHotelImage == null)
            {
                throw new ValidationException("Зображення готелю не знайдено", "");
            }
            var BusyHotelImageUrl = await Database.HotelImages.GetByImageUrlSubstring(hotelImageDTO.ImageUrl);
            if (BusyHotelImageUrl.Any(hi => hi.ImageUrl == hotelImageDTO.ImageUrl && hi.Id != hotelImageDTO.Id))
            {
                throw new ValidationException("Таке зображення готелю вже існує", "");
            }
            Hotel? hotel = null;
            if (hotelImageDTO.HotelId != null)
            {
                hotel = await Database.Hotels.GetById((int)hotelImageDTO.HotelId);
                if (hotel == null)
                {
                    throw new ValidationException("Готель не знайдено", "");
                }
            }
            BusyHotelImage.ImageUrl = hotelImageDTO.ImageUrl;
            BusyHotelImage.Hotel = hotel;
            Database.HotelImages.Update(BusyHotelImage);
            await Database.Save();
        }

        public async Task Delete(long id)
        {
            var BusyHotelImageId = await Database.HotelImages.GetById(id);
            if (BusyHotelImageId == null)
            {
                throw new ValidationException("Зображення готелю не знайдено", "");
            }
            await Database.HotelImages.Delete(id);
            await Database.Save();
        }

        public async Task<IEnumerable<HotelImageDTO>> GetAll()
        {
            var mapper = new Mapper(HotelImage_HotelImageDTOMapConfig);
            return mapper.Map<IEnumerable<HotelImage>, IEnumerable<HotelImageDTO>>(await Database.HotelImages.GetAll());
        }

        public async Task<HotelImageDTO?> GetById(long id)
        {
            var mapper = new Mapper(HotelImage_HotelImageDTOMapConfig);
            return mapper.Map<HotelImage, HotelImageDTO>(await Database.HotelImages.GetById(id));
        }

        public async Task<IEnumerable<HotelImageDTO>> GetByHotelId(int hotelId)
        {
            var mapper = new Mapper(HotelImage_HotelImageDTOMapConfig);
            return mapper.Map<IEnumerable<HotelImage>, IEnumerable<HotelImageDTO>>(await Database.HotelImages.GetByHotelId(hotelId));
        }

        public async Task<IEnumerable<HotelImageDTO>> GetByImageUrlSubstring(string imageUrlSubstring)
        {
            var mapper = new Mapper(HotelImage_HotelImageDTOMapConfig);
            return mapper.Map<IEnumerable<HotelImage>, IEnumerable<HotelImageDTO>>(await Database.HotelImages.GetByImageUrlSubstring(imageUrlSubstring));
        }


    }
}
