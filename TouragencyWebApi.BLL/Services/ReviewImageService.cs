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
    public class ReviewImageService : IReviewImageService
    {
        IUnitOfWork Database;

        MapperConfiguration ReviewImage_ReviewImageDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<ReviewImage, ReviewImageDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("ImagePath", opt => opt.MapFrom(c => c.ImagePath))
        .ForMember("ReviewId", opt => opt.MapFrom(c => c.ReviewId))
        );
        public ReviewImageService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<IEnumerable<ReviewImageDTO>> GetAll()
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetAll());
        }

        public async Task<IEnumerable<ReviewImageDTO>> Get200Last()
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.Get200Last());
        }
        public async Task<ReviewImageDTO?> GetById(long id)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<ReviewImage, ReviewImageDTO>(await Database.ReviewImages.GetById(id));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByReviewId(long reviewId)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByReviewId(reviewId));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByReviewRatingDiapazone(short minRating, short maxRating)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByReviewRatingDiapazone(minRating, maxRating));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByImagePathSubstring(string imagePathSubstring)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByImagePathSubstring(imagePathSubstring));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByTourId(long tourId)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByTourId(tourId));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByTourNameId(int tourNameId)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByTourNameId(tourNameId));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByTourName(string tourName)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByTourName(tourName));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByClientFirstname(string clientFirstname)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByClientFirstname(clientFirstname));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByClientLastname(string clientLastname)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByClientLastname(clientLastname));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByClientMiddlename(string clientMiddlename)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByClientMiddlename(clientMiddlename));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByTouristNickname(string touristNickname)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByTouristNickname(touristNickname));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByCountryName(string countryName)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByCountryName(countryName));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetBySettlementName(string settlementName)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetBySettlementName(settlementName));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByHotelName(string hotelName)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByHotelName(hotelName));
        }

        public async Task<IEnumerable<ReviewImageDTO>> GetByCompositeSearch(long? reviewId, short? minRating, short? maxRating, string? imagePathSubstring, long? tourId, int? tourNameId, string? tourName, string? clientFirstname, string? clientLastname, string? clientMiddlename, string? touristNickname, string? countryName, string? settlementName, string? hotelName)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByCompositeSearch(reviewId, minRating, maxRating, imagePathSubstring, tourId, tourNameId, tourName, clientFirstname, clientLastname, clientMiddlename, touristNickname, countryName, settlementName, hotelName));
        }
        public async Task<ReviewImageDTO> Create(ReviewImageDTO reviewImageDTO)
        {
            var PreExistedReviewImage = await Database.ReviewImages.GetByImagePathSubstring(reviewImageDTO.ImagePath);
            if (PreExistedReviewImage.Any(em => em.ImagePath == reviewImageDTO.ImagePath))
            {
                throw new ValidationException("Таке зображення із вказаним ImagePath шляхом вже існує", "");
            }
            var PreExistedReview = await Database.Reviews.GetById(reviewImageDTO.ReviewId);
            if (PreExistedReview == null)
            {
                throw new ValidationException("Такий відгук із вказаним ReviewId не існує", "");
            }
            var newReviewImage = new ReviewImage{
                ReviewId = reviewImageDTO.ReviewId,
                ImagePath = reviewImageDTO.ImagePath
            };
            await Database.ReviewImages.Create(newReviewImage);
            await Database.Save();
            reviewImageDTO.Id = newReviewImage.Id;
            return reviewImageDTO;
        }

        public async Task<ReviewImageDTO> Update(ReviewImageDTO reviewImageDTO)
        {
            var PreExistedReviewImage = await Database.ReviewImages.GetById(reviewImageDTO.Id);
            if (PreExistedReviewImage == null)
            {
                throw new ValidationException("Таке зображення із вказаним Id не існує", "");
            }
            var PreExistedReview = await Database.Reviews.GetById(reviewImageDTO.ReviewId);
            if (PreExistedReview == null)
            {
                throw new ValidationException("Такий відгук із вказаним ReviewId не існує", "");
            }
            var newReviewImage = new ReviewImage
            {
                ReviewId = reviewImageDTO.ReviewId,
                ImagePath = reviewImageDTO.ImagePath
            };
            Database.ReviewImages.Update(newReviewImage);
            await Database.Save();
            return reviewImageDTO;
        }

        public async Task<ReviewImageDTO> Delete(long id)
        {
            var PreExistedReviewImage = await Database.ReviewImages.GetById(id);
            if (PreExistedReviewImage == null)
            {
                throw new ValidationException("Таке зображення із вказаним Id не існує", "");
            }
            var dto = await GetById(id);
            await Database.ReviewImages.Delete(id);
            await Database.Save();
            return dto;
        }
    }
}
