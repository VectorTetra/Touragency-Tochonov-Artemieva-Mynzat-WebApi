using AutoMapper;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.BLL.Services
{
    public class ReviewService : IReviewService
    {
        IUnitOfWork Database;
        public ReviewService(IUnitOfWork uow)
        {
            Database = uow;
        }
        MapperConfiguration Review_ReviewDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<Review, ReviewDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("TourId", opt => opt.MapFrom(c => c.TourId))
        .ForMember("ClientId", opt => opt.MapFrom(c => c.ClientId))
        .ForMember("Rating", opt => opt.MapFrom(c => c.Rating))
        .ForMember("ReviewCaption", opt => opt.MapFrom(c => c.ReviewCaption))
        .ForMember("ReviewText", opt => opt.MapFrom(c => c.ReviewText))
        .ForMember("CreationDate", opt => opt.MapFrom(c => c.CreationDate))
        .ForMember("Likes", opt => opt.MapFrom(c => c.Likes))
        .ForPath(d => d.ReviewImageIds, opt => opt.MapFrom(c => c.ReviewImages.Select(ri => ri.Id)))
        );
        public async Task<IEnumerable<ReviewDTO>> GetAll()
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetAll());
        }
        public async Task<ReviewDTO?> GetById(long id)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<Review, ReviewDTO>(await Database.Reviews.GetById(id));
        }
        public async Task<IEnumerable<ReviewDTO>> GetByTourId(int tourId)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByTourId(tourId));
        }
        public async Task<IEnumerable<ReviewDTO>> GetByClientId(int clientId)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByClientId(clientId));
        }
        public async Task<IEnumerable<ReviewDTO>> GetByCountryId(int countryId)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByCountryId(countryId));
        }
        public async Task<IEnumerable<ReviewDTO>> GetByReviewImageId(long reviewImageId)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByReviewImageId(reviewImageId));
        }
        public async Task<IEnumerable<ReviewDTO>> GetByReviewCaptionSubstring(string reviewCaptionSubstring)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByReviewCaptionSubstring(reviewCaptionSubstring));
        }
        public async Task<IEnumerable<ReviewDTO>> GetByReviewTextSubstring(string reviewTextSubstring)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByReviewTextSubstring(reviewTextSubstring));
        }
        public async Task<IEnumerable<ReviewDTO>> GetByRatingDiapazone(short start, short end)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByRatingDiapazone(start, end));
        }
        public async Task<IEnumerable<ReviewDTO>> GetByCreationDateDiapazone(DateTime start, DateTime end)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByCreationDateDiapazone(start, end));
        }
        public async Task Create(ReviewDTO reviewDTO)
        {
            var IsBusyReviewId = await Database.Reviews.GetById(reviewDTO.Id);
            if (IsBusyReviewId != null)
            {
                throw new ValidationException("Такий reviewId вже зайнято!", "");
            }
            //-----------------------------------------------------------------------------------------------------
            var reviewImages = new List<ReviewImage>();
            //-----------------------------------------------------------------------------------------------------
            foreach (var reviewImageId in reviewDTO.ReviewImageIds)
            {
                var reviewImage = await Database.ReviewImages.GetById(reviewImageId);
                if (reviewImage == null)
                {
                    throw new ValidationException("ReviewImageId не знайдено!", nameof(reviewImageId));
                }
                reviewImages.Add(reviewImage);
            }
            //-----------------------------------------------------------------------------------------------------
            var review = new Review
            {
                Id = reviewDTO.Id,
                TourId = reviewDTO.TourId,
                ClientId = reviewDTO.ClientId,
                Rating = reviewDTO.Rating,
                ReviewCaption = reviewDTO.ReviewCaption,
                ReviewText = reviewDTO.ReviewText,
                CreationDate = reviewDTO.CreationDate,
                Likes = reviewDTO.Likes,
                ReviewImages = reviewImages
            };
            await Database.Reviews.Create(review);
            await Database.Save();
        }
        public async Task Update(ReviewDTO reviewDTO)
        {
            var review = await Database.Reviews.GetById(reviewDTO.Id);
            if (review == null)
            {
                throw new ValidationException("Такого відгуку з вказаним reviewId не знайдено!", "");
            }
            //-----------------------------------------------------------------------------------------------------
            review.ReviewImages.Clear();
            //-----------------------------------------------------------------------------------------------------
            foreach (var reviewImageId in reviewDTO.ReviewImageIds)
            {
                var reviewImage = await Database.ReviewImages.GetById(reviewImageId);
                if (reviewImage == null)
                {
                    throw new ValidationException("ReviewImageId не знайдено!", nameof(reviewImageId));
                }
                review.ReviewImages.Add(reviewImage);
            }
            //-----------------------------------------------------------------------------------------------------

            review.TourId = reviewDTO.TourId;
            review.ClientId = reviewDTO.ClientId;
            review.Rating = reviewDTO.Rating;
            review.ReviewCaption = reviewDTO.ReviewCaption;
            review.ReviewText = reviewDTO.ReviewText;
            review.CreationDate = reviewDTO.CreationDate;
            review.Likes = reviewDTO.Likes;
            Database.Reviews.Update(review);
            await Database.Save();
        }
        public async Task Delete(long id) 
        {
            var review = await Database.Reviews.GetById(id);
            if (review == null)
            {
                throw new ValidationException("Такого відгуку з вказаним reviewId не знайдено!", "");
            }
            await Database.Reviews.Delete(review.Id);
            await Database.Save();
        }
    }
}
