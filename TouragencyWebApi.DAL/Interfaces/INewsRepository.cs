using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAll();
        Task<IEnumerable<News>> Get200Last();
        Task<News?> GetById(long id);
        Task<IEnumerable<News>> GetByCaptionSubstring(string caption);
        Task<IEnumerable<News>> GetByTextSubstring(string text);
        Task<IEnumerable<News>> GetByPhotoUrlSubstring(string url);
        Task<IEnumerable<News>> GetByDateDiapazon(DateTime start, DateTime end);
        Task<IEnumerable<News>> GetByVisibility(bool isVisible);
        Task<IEnumerable<News>> GetByImportance(bool isImportant);
        Task<IEnumerable<News>> GetByCompositeSearch(string? caption, string? text, DateTime? publishDateTimeDiapazonStart, DateTime? publishDateTimeDiapazonEnd, bool? isVisible, bool? isImportant);
        Task<IEnumerable<News>> GetLastActiveToQuantinyPrioritizeIncludeImportant(int quantity);
        Task Create(News news);
        void Update(News news);
        Task Delete(long id);
    }
}
