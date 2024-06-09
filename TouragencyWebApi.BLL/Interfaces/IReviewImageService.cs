using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;


namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IReviewImageService
    {
        Task<IEnumerable<ReviewImageDTO>> GetAll();
        Task<IEnumerable<ReviewImageDTO>> Get200Last();
        Task<ReviewImageDTO?> GetById(long id);
        Task<IEnumerable<ReviewImageDTO>> GetByReviewId(long reviewId);
        Task<IEnumerable<ReviewImageDTO>> GetByReviewRatingDiapazone(short minRating, short maxRating);
        Task<IEnumerable<ReviewImageDTO>> GetByImagePathSubstring(string imagePathSubstring);
        Task<IEnumerable<ReviewImageDTO>> GetByTourId(long tourId);
        Task<IEnumerable<ReviewImageDTO>> GetByTourNameId(int tourNameId);
        Task<IEnumerable<ReviewImageDTO>> GetByTourName(string tourName);
        Task<IEnumerable<ReviewImageDTO>> GetByClientFirstname(string clientFirstname);
        Task<IEnumerable<ReviewImageDTO>> GetByClientLastname(string clientLastname);
        Task<IEnumerable<ReviewImageDTO>> GetByClientMiddlename(string clientMiddlename);
        Task<IEnumerable<ReviewImageDTO>> GetByTouristNickname(string touristNickname);
        Task<IEnumerable<ReviewImageDTO>> GetByCountryName(string countryName);
        Task<IEnumerable<ReviewImageDTO>> GetBySettlementName(string settlementName);
        Task<IEnumerable<ReviewImageDTO>> GetByHotelName(string hotelName);
        Task<IEnumerable<ReviewImageDTO>> GetByCompositeSearch(long? reviewId, short? minRating, short? maxRating,
            string? imagePathSubstring, long? tourId, int? tourNameId, string? tourName, string? clientFirstname, string? clientLastname,
            string? clientMiddlename, string? touristNickname, string? countryName, string? settlementName, string? hotelName);
        Task<ReviewImageDTO> Create(ReviewImageDTO reviewImage);
        Task<ReviewImageDTO> Update(ReviewImageDTO reviewImage);
        Task<ReviewImageDTO> Delete(long id);
    }
}
