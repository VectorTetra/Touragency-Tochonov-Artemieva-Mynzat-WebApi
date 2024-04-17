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

        public async Task<IEnumerable<ReviewImageDTO>> GetByImagePathSubstring(string imagePathSubstring)
        {
            var mapper = new Mapper(ReviewImage_ReviewImageDTOMapConfig);
            return mapper.Map<IEnumerable<ReviewImage>, IEnumerable<ReviewImageDTO>>(await Database.ReviewImages.GetByImagePathSubstring(imagePathSubstring));
        }

        public async Task Create(ReviewImageDTO reviewImageDTO)
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
        }

        public async Task Update(ReviewImageDTO reviewImageDTO)
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
        }

        public async Task Delete(long id)
        {
            var PreExistedReviewImage = await Database.ReviewImages.GetById(id);
            if (PreExistedReviewImage == null)
            {
                throw new ValidationException("Таке зображення із вказаним Id не існує", "");
            }
            await Database.ReviewImages.Delete(id);
            await Database.Save();
        }
    }
}
