using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.DAL.Repositories
{
    public class NewsRepository: INewsRepository
    {
        private readonly TouragencyContext _context;
        public NewsRepository(TouragencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<IEnumerable<News>> Get200Last()
        {
            return await _context.News.OrderByDescending(p => p.Id).Take(200).ToListAsync();
        }

        public async Task<News?> GetById(long id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task<IEnumerable<News>> GetByCaptionSubstring(string caption)
        {
            return await _context.News
                .Where(p => p.Caption.Contains(caption))
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetByTextSubstring(string text)
        {
            return await _context.News
                .Where(p => p.Text.Contains(text))
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetByPhotoUrlSubstring(string url)
        {
            return await _context.News
                .Where(p => p.PhotoUrl.Contains(url))
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetByDateDiapazon(DateTime start, DateTime end)
        {
            return await _context.News
                .Where(p => p.PublishDateTime >= start && p.PublishDateTime <= end)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetByVisibility(bool isVisible)
        {
            return await _context.News
                .Where(p => p.IsVisible == isVisible)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetByImportance(bool isImportant)
        {
            return await _context.News
                .Where(p => p.IsImportant == isImportant)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetByCompositeSearch(string? caption, string? text, DateTime? publishDateTimeDiapazonStart, DateTime? publishDateTimeDiapazonEnd, bool? isVisible, bool? isImportant)
        {
            var collections = new List<IEnumerable<News>>();
            if (caption != null)
            {
                collections.Add(await GetByCaptionSubstring(caption));
            }
            if (text != null)
            {
                collections.Add(await GetByTextSubstring(text));
            }
            if (publishDateTimeDiapazonStart != null && publishDateTimeDiapazonEnd!= null)
            {
                collections.Add(await GetByDateDiapazon(publishDateTimeDiapazonStart.Value, publishDateTimeDiapazonEnd.Value));
            }
            if (isVisible != null)
            {
                collections.Add(await GetByVisibility(isVisible.Value));
            }
            if (isImportant != null)
            {
                collections.Add(await GetByImportance(isImportant.Value));
            }
            if (!collections.Any())
            {
                return new List<News>();
            }
            return collections.Aggregate((a, b) => a.Intersect(b));
        }

        public async Task<IEnumerable<News>> GetLastActiveToQuantinyPrioritizeIncludeImportant(int quantity)
        {
            return await _context.News
                .Where(p => p.IsVisible)
                .OrderByDescending(p => p.IsImportant)
                .ThenByDescending(p => p.PublishDateTime)
                .Take(quantity)
                .ToListAsync();
        }

        public async Task Create(News news)
        {
            await _context.News.AddAsync(news);
        }

        public void Update(News news)
        {
            _context.Entry(news).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            var news = await GetById(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            
        }
    }
}
