using AutoMapper;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class TourNameService : ITourNameService
    {
        IUnitOfWork Database;
        MapperConfiguration TourName_TourNameDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<TourName, TourNameDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Name", opt => opt.MapFrom(c => c.Name))
        .ForMember("PageJSONStructureUrl", opt => opt.MapFrom(c => c.PageJSONStructureUrl))
        .ForMember("IsHaveNightRides", opt => opt.MapFrom(c => c.IsHaveNightRides))
        .ForMember("NightRidesCount", opt => opt.MapFrom(c => c.NightRidesCount))
        .ForMember("Route", opt => opt.MapFrom(c => c.Route))
        .ForMember("Duration", opt => opt.MapFrom(c => c.Duration))
        .ForPath(d => d.TourIds, opt => opt.MapFrom(c => c.Tours.Select(b => b.Id)))
        .ForPath(d => d.TourImageIds, opt => opt.MapFrom(c => c.TourImages.Select(b => b.Id)))
        .ForPath(d => d.CountryIds, opt => opt.MapFrom(c => c.Countries.Select(b => b.Id)))
        .ForPath(d => d.SettlementIds, opt => opt.MapFrom(c => c.Settlements.Select(b => b.Id)))
        .ForPath(d => d.HotelIds, opt => opt.MapFrom(c => c.Hotels.Select(b => b.Id)))
        .ForPath(d => d.TransportTypeIds, opt => opt.MapFrom(c => c.TransportTypes.Select(b => b.Id)))
        );
        public TourNameService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<IEnumerable<TourNameDTO>> GetAll()
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetAll());
        }

        public async Task<IEnumerable<TourNameDTO>> Get200Last()
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.Get200Last());
        }
        public async Task<TourNameDTO?> GetById(int id)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<TourName, TourNameDTO>(await Database.TourNames.GetById(id));
        }
        public async Task<IEnumerable<TourNameDTO>> GetByName(string tourNameSubstring)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetByName(tourNameSubstring));
        }

        public async Task<IEnumerable<TourNameDTO>> GetByContinentName(string continentNameSubstring)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetByContinentName(continentNameSubstring));
        }
        public async Task<IEnumerable<TourNameDTO>> GetByCountryName(string countryNameSubstring)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetByCountryName(countryNameSubstring));
        }
        public async Task<IEnumerable<TourNameDTO>> GetBySettlementName(string settlementNameSubstring)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetBySettlementName(settlementNameSubstring));
        }
        public async Task<IEnumerable<TourNameDTO>> GetByHotelName(string hotelNameSubstring)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetByHotelName(hotelNameSubstring));
        }
        public async Task<IEnumerable<TourNameDTO>> GetByPageJSONStructureUrlSubstring(string pageJSONStructureUrlSubstring)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetByPageJSONStructureUrlSubstring(pageJSONStructureUrlSubstring));
        }
        public async Task<IEnumerable<TourNameDTO>> GetByTourId(long tourId)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetByTourId(tourId));
        }
        public async Task<IEnumerable<TourNameDTO>> GetByTourImageId(long tourImageId)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetByTourImageId(tourImageId));
        }
        public async Task<IEnumerable<TourNameDTO>> GetByCompositeSearch(string? tourNameSubstring, string continentNameSubstring, string? countryNameSubstring, string? settlementNameSubstring, string? hotelNameSubstring, string? pageJSONStructureUrlSubstring, long? tourId, long? tourImageId)
        {
            var mapper = new Mapper(TourName_TourNameDTOMapConfig);
            return mapper.Map<IEnumerable<TourName>, List<TourNameDTO>>(await Database.TourNames.GetByCompositeSearch(tourNameSubstring, continentNameSubstring, countryNameSubstring, settlementNameSubstring, hotelNameSubstring, pageJSONStructureUrlSubstring, tourId, tourImageId));
        }
        public async Task<TourNameDTO> Create(TourNameDTO tourNameDTO)
        {
            var Existed = await Database.TourNames.GetByName(tourNameDTO.Name);
            if (Existed.Any(em => em.Name == tourNameDTO.Name))
            {
                throw new ValidationException("Така назва туру вже існує", "");
            }
            var ExistedPageJSON = await Database.TourNames.GetByPageJSONStructureUrlSubstring(tourNameDTO.PageJSONStructureUrl);
            if (ExistedPageJSON.Any(em => em.PageJSONStructureUrl == tourNameDTO.PageJSONStructureUrl))
            {
                throw new ValidationException("Така структура JSON вже існує", "");
            }
            if (tourNameDTO.IsHaveNightRides == false)
            {
                tourNameDTO.NightRidesCount = 0;
            }
            var newTourName = new TourName
            {
                Name = tourNameDTO.Name,
                IsHaveNightRides = tourNameDTO.IsHaveNightRides,
                NightRidesCount = tourNameDTO.NightRidesCount,
                PageJSONStructureUrl = tourNameDTO.PageJSONStructureUrl,
                Route = tourNameDTO.Route,
                Duration = tourNameDTO.Duration,
                Tours = new List<Tour>(),
                TourImages = new List<TourImage>(),
                Countries = new List<Country>(),
                Settlements = new List<Settlement>(),
                Hotels = new List<Hotel>(),
                TransportTypes = new List<TransportType>()
            };
            foreach (var id in tourNameDTO.TourIds)
            {
                var tour = await Database.Tours.GetById(id);
                if (tour == null)
                {
                    throw new ValidationException($"Тур з id {id} не знайдено", "");
                }
                newTourName.Tours.Add(tour);
            }
            foreach (var id in tourNameDTO.TourImageIds)
            {
                var tourImage = await Database.TourImages.GetById(id);
                if (tourImage == null)
                {
                    throw new ValidationException($"Зображення туру з id {id} не знайдено", "");
                }
                newTourName.TourImages.Add(tourImage);
            }
            foreach (var id in tourNameDTO.CountryIds)
            {
                var country = await Database.Countries.GetById(id);
                if (country == null)
                {
                    throw new ValidationException($"Країну з id {id} не знайдено", "");
                }
                newTourName.Countries.Add(country);
            }
            foreach (var id in tourNameDTO.SettlementIds)
            {
                var settlement = await Database.Settlements.GetById(id);
                if (settlement == null)
                {
                    throw new ValidationException($"Поселення з id {id} не знайдено", "");
                }
                newTourName.Settlements.Add(settlement);
            }
            foreach (var id in tourNameDTO.HotelIds)
            {
                var hotel = await Database.Hotels.GetById(id);
                if (hotel == null)
                {
                    throw new ValidationException($"Готель з id {id} не знайдено", "");
                }
                newTourName.Hotels.Add(hotel);
            }
            foreach (var id in tourNameDTO.TransportTypeIds)
            {
                var transportType = await Database.TransportTypes.GetById(id);
                if (transportType == null)
                {
                    throw new ValidationException($"Тип транспорту з id {id} не знайдено", "");
                }
                newTourName.TransportTypes.Add(transportType);
            }


            await Database.TourNames.Create(newTourName);
            await Database.Save();
            tourNameDTO.Id = newTourName.Id;
            return tourNameDTO;
        }
        public async Task<TourNameDTO> Update(TourNameDTO tourNameDTO)
        {
            TourName tourName = await Database.TourNames.GetById(tourNameDTO.Id);
            if (tourName == null)
            {
                throw new ValidationException("Така назва туру не знайдена", "");
            }
            var ExistedPageJSON = await Database.TourNames.GetByPageJSONStructureUrlSubstring(tourNameDTO.PageJSONStructureUrl);
            if (ExistedPageJSON.Any(em => em.PageJSONStructureUrl == tourNameDTO.PageJSONStructureUrl && em.Id != tourNameDTO.Id))
            {
                throw new ValidationException("Така структура JSON вже зайнята", "");
            }
            tourName.Name = tourNameDTO.Name;
            tourName.PageJSONStructureUrl = tourNameDTO.PageJSONStructureUrl;
            tourName.IsHaveNightRides = tourNameDTO.IsHaveNightRides;
            tourName.NightRidesCount = tourNameDTO.NightRidesCount;
            tourName.Route = tourNameDTO.Route;
            tourName.Duration = tourNameDTO.Duration;
            tourName.Tours.Clear();
            tourName.TourImages.Clear();
            tourName.Countries.Clear();
            tourName.Settlements.Clear();
            tourName.Hotels.Clear();
            tourName.TransportTypes.Clear();
            foreach (var id in tourNameDTO.TourIds)
            {
                var tour = await Database.Tours.GetById(id);
                if (tour == null)
                {
                    throw new ValidationException($"Тур з id {id} не знайдено", "");
                }
                tourName.Tours.Add(tour);
            }
            foreach (var id in tourNameDTO.TourImageIds)
            {
                var tourImage = await Database.TourImages.GetById(id);
                if (tourImage == null)
                {
                    throw new ValidationException($"Зображення туру з id {id} не знайдено", "");
                }
                tourName.TourImages.Add(tourImage);
            }
            foreach (var id in tourNameDTO.CountryIds)
            {
                var country = await Database.Countries.GetById(id);
                if (country == null)
                {
                    throw new ValidationException($"Країну з id {id} не знайдено", "");
                }
                tourName.Countries.Add(country);
            }
            foreach (var id in tourNameDTO.SettlementIds)
            {
                var settlement = await Database.Settlements.GetById(id);
                if (settlement == null)
                {
                    throw new ValidationException($"Поселення з id {id} не знайдено", "");
                }
                tourName.Settlements.Add(settlement);
            }
            foreach (var id in tourNameDTO.HotelIds)
            {
                var hotel = await Database.Hotels.GetById(id);
                if (hotel == null)
                {
                    throw new ValidationException($"Готель з id {id} не знайдено", "");
                }
                tourName.Hotels.Add(hotel);
            }
            foreach (var id in tourNameDTO.TransportTypeIds)
            {
                var transportType = await Database.TransportTypes.GetById(id);
                if (transportType == null)
                {
                    throw new ValidationException($"Тип транспорту з id {id} не знайдено", "");
                }
                tourName.TransportTypes.Add(transportType);
            }
            Database.TourNames.Update(tourName);
            await Database.Save();
            return tourNameDTO;
        }
        public async Task<TourNameDTO> Delete(int id)
        {
            var tourName = await Database.TourNames.GetById(id);
            if (tourName == null)
            {
                throw new ValidationException("Така назва туру не знайдена", "");
            }
            var dto = await GetById(id);
            await Database.TourNames.Delete(id);
            await Database.Save();
            return dto;
        }

    }
}
