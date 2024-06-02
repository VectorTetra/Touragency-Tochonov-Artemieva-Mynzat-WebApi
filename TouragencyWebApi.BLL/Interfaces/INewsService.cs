using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDTO>> GetAll();
        Task<IEnumerable<NewsDTO>> Get200Last();
        Task<NewsDTO?> GetById(long id);
        Task<IEnumerable<NewsDTO>> GetByCaptionSubstring(string caption);
        Task<IEnumerable<NewsDTO>> GetByTextSubstring(string text);
        Task<IEnumerable<NewsDTO>> GetByPhotoUrlSubstring(string url);
        Task<IEnumerable<NewsDTO>> GetByDateDiapazon(DateTime start, DateTime end);
        Task<IEnumerable<NewsDTO>> GetByVisibility(bool isVisible);
        Task<IEnumerable<NewsDTO>> GetByImportance(bool isImportant);
        Task<IEnumerable<NewsDTO>> GetByCompositeSearch(string? caption, string? text, DateTime? publishDateTimeDiapazonStart, DateTime? publishDateTimeDiapazonEnd, bool? isVisible, bool? isImportant);
        Task<IEnumerable<NewsDTO>> GetLastActiveToQuantinyPrioritizeIncludeImportant(int quantity);
        Task<NewsDTO> Create(NewsDTO news);
        Task<NewsDTO> Update(NewsDTO news);
        Task<NewsDTO> Delete(long id);
    }
}
