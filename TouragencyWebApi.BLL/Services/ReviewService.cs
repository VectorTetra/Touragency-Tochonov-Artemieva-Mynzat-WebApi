﻿using AutoMapper;
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
        .ForMember("ClientTouristNickname", opt => opt.MapFrom(c => c.Client.TouristNickname))
        .ForPath(d => d.ReviewImageIds, opt => opt.MapFrom(c => c.ReviewImages.Select(ri => ri.Id)))
        .ForPath(d => d.ReviewImageUrls, opt => opt.MapFrom(c => c.ReviewImages.Select(ri => ri.ImagePath)))
        .ForPath(d => d.ArrivalDate, opt => opt.MapFrom(c => c.Tour.ArrivalDate))
        .ForPath(d => d.DepartureDate, opt => opt.MapFrom(c => c.Tour.DepartureDate))
        .ForPath(d => d.TourName, opt => opt.MapFrom(c => c.Tour.Name.Name))
        .ForPath(d => d.TourNameId, opt => opt.MapFrom(c => c.Tour.Name.Id))
        .ForMember(d => d.ReviewImages, opt => opt.MapFrom(c => c.ReviewImages.Select(ri => new ReviewImageDTO { Id = ri.Id, ReviewId = ri.ReviewId, ImagePath = ri.ImagePath })))
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
        public async Task<ReviewDTO> Create(ReviewDTO reviewDTO)
        {
            var IsBusyReviewId = await Database.Reviews.GetById(reviewDTO.Id);
            if (IsBusyReviewId != null)
            {
                throw new ValidationException("Такий reviewId вже зайнято!", "");
            }
            //-----------------------------------------------------------------------------------------------------
            var reviewImages = new List<ReviewImage>();
            //-----------------------------------------------------------------------------------------------------
            if (reviewDTO.ReviewImageIds != null)
            {
                foreach (var reviewImageId in reviewDTO.ReviewImageIds)
                {
                    var reviewImage = await Database.ReviewImages.GetById(reviewImageId);
                    if (reviewImage == null)
                    {
                        throw new ValidationException("ReviewImageId не знайдено!", nameof(reviewImageId));
                    }
                    reviewImages.Add(reviewImage);
                }
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
            reviewDTO.Id = review.Id;
            return reviewDTO;
        }
        public async Task<ReviewDTO> Update(ReviewDTO reviewDTO)
        {
            var review = await Database.Reviews.GetById(reviewDTO.Id);
            if (review == null)
            {
                throw new ValidationException("Такого відгуку з вказаним reviewId не знайдено!", "");
            }
            //-----------------------------------------------------------------------------------------------------
            review.ReviewImages.Clear();
            //-----------------------------------------------------------------------------------------------------
            if (reviewDTO.ReviewImageIds != null)
            {
                foreach (var reviewImageId in reviewDTO.ReviewImageIds)
                {
                    var reviewImage = await Database.ReviewImages.GetById(reviewImageId);
                    if (reviewImage == null)
                    {
                        throw new ValidationException("ReviewImageId не знайдено!", nameof(reviewImageId));
                    }
                    review.ReviewImages.Add(reviewImage);
                }
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
            return reviewDTO;
        }
        public async Task<ReviewDTO> Delete(long id)
        {
            var review = await Database.Reviews.GetById(id);
            if (review == null)
            {
                throw new ValidationException("Такого відгуку з вказаним reviewId не знайдено!", "");
            }
            var dto = await GetById(id);
            await Database.Reviews.Delete(review.Id);
            await Database.Save();
            return dto;
        }

        public async Task<IEnumerable<ReviewDTO>> Get200Last()
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.Get200Last());
        }

        public async Task<IEnumerable<ReviewDTO>> GetByTourNameSubstring(string tourNameSubstring)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByTourNameSubstring(tourNameSubstring));
        }

        public async Task<IEnumerable<ReviewDTO>> GetByTouristNicknameSubstring(string touristNicknameSubstring)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByTouristNicknameSubstring(touristNicknameSubstring));
        }

        public async Task<IEnumerable<ReviewDTO>> GetByClientFirstnameSubstring(string clientFirstnameSubstring)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByClientFirstnameSubstring(clientFirstnameSubstring));
        }

        public async Task<IEnumerable<ReviewDTO>> GetByClientLastnameSubstring(string clientLastnameSubstring)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByClientLastnameSubstring(clientLastnameSubstring));
        }

        public async Task<IEnumerable<ReviewDTO>> GetByClientMiddlenameSubstring(string clientMiddlenameSubstring)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByClientMiddlenameSubstring(clientMiddlenameSubstring));
        }

        public async Task<IEnumerable<ReviewDTO>> GetByCountryNameSubstring(string countryNameSubstring)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByCountryNameSubstring(countryNameSubstring));
        }

        public async Task<IEnumerable<ReviewDTO>> GetByCompositeSearch(long? tourId, int? clientId, int? countryId, long? reviewImageId, string? reviewCaptionSubstring, string? reviewTextSubstring, short? startRating, short? endRating, DateTime? startDate, DateTime? endDate, string? tourNameSubstring, string? touristNicknameSubstring, string? clientFirstnameSubstring, string? clientLastnameSubstring, string? clientMiddlenameSubstring, string? countryNameSubstring, int? TourNameId)
        {
            var mapper = new Mapper(Review_ReviewDTOMapConfig);
            return mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDTO>>(await Database.Reviews.GetByCompositeSearch(tourId, clientId, countryId, reviewImageId, reviewCaptionSubstring, reviewTextSubstring, startRating, endRating, startDate, endDate, tourNameSubstring, touristNicknameSubstring, clientFirstnameSubstring, clientLastnameSubstring, clientMiddlenameSubstring, countryNameSubstring, TourNameId));
        }
    }
}
