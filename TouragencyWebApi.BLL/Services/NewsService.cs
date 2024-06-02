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
    public class NewsService: INewsService
    {
        IUnitOfWork Database;

        MapperConfiguration News_NewsDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<News, NewsDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("Caption", opt => opt.MapFrom(c => c.Caption))
        .ForMember("Text", opt => opt.MapFrom(c => c.Text))
        .ForMember("PublishDateTime", opt => opt.MapFrom(c => c.PublishDateTime))
        .ForMember("PhotoUrl", opt => opt.MapFrom(c => c.PhotoUrl))
        .ForMember("IsVisible", opt => opt.MapFrom(c => c.IsVisible))
        .ForMember("IsImportant", opt => opt.MapFrom(c => c.IsImportant))
        );
        public NewsService(IUnitOfWork uow)
        {
            Database = uow;
        }
        
        public async Task<NewsDTO> Create(NewsDTO newsDTO)
        {
            var newNews = new News
            {
                Caption = newsDTO.Caption,
                Text = newsDTO.Text,
                PublishDateTime = newsDTO.PublishDateTime,
                PhotoUrl = newsDTO.PhotoUrl,
                IsVisible = newsDTO.IsVisible,
                IsImportant = newsDTO.IsImportant
            };
            await Database.News.Create(newNews);
            await Database.Save();
            newsDTO.Id = newNews.Id;
            return newsDTO;
        }

        public async Task<NewsDTO> Update(NewsDTO newsDTO)
        {
            News news = await Database.News.GetById(newsDTO.Id);
            if (news == null)
            {
                throw new ValidationException($"Новину з вказаним Id не знайдено (id : {newsDTO.Id})", "");
            }
            news.Caption = newsDTO.Caption;
            news.Text = newsDTO.Text;
            news.PublishDateTime = newsDTO.PublishDateTime;
            news.PhotoUrl = newsDTO.PhotoUrl;
            news.IsVisible = newsDTO.IsVisible;
            news.IsImportant = newsDTO.IsImportant;
            Database.News.Update(news);
            await Database.Save();
            return newsDTO;
        }

        public async Task<NewsDTO> Delete(long id)
        {
            News news = await Database.News.GetById(id);
            if (news == null)
            {
                throw new ValidationException($"Новину з вказаним Id не знайдено (id : {id})", "");
            }
            var newsDTO = await GetById(id);
            await Database.News.Delete(id);
            await Database.Save();
            return newsDTO;
        }

        public async Task<IEnumerable<NewsDTO>> GetAll()
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.GetAll());
        }

        public async Task<IEnumerable<NewsDTO>> Get200Last()
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.Get200Last());
        }

        public async Task<NewsDTO?> GetById(long id)
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<News, NewsDTO>(await Database.News.GetById(id));
        }

        public async Task<IEnumerable<NewsDTO>> GetByCaptionSubstring(string caption)
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.GetByCaptionSubstring(caption));
        }

        public async Task<IEnumerable<NewsDTO>> GetByTextSubstring(string text)
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.GetByTextSubstring(text));
        }

        public async Task<IEnumerable<NewsDTO>> GetByPhotoUrlSubstring(string url)
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.GetByPhotoUrlSubstring(url));
        }

        public async Task<IEnumerable<NewsDTO>> GetByDateDiapazon(DateTime start, DateTime end)
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.GetByDateDiapazon(start, end));
        }

        public async Task<IEnumerable<NewsDTO>> GetByVisibility(bool isVisible)
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.GetByVisibility(isVisible));
        }

        public async Task<IEnumerable<NewsDTO>> GetByImportance(bool isImportant)
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.GetByImportance(isImportant));
        }

        public async Task<IEnumerable<NewsDTO>> GetByCompositeSearch(string? caption, string? text, DateTime? publishDateTimeDiapazonStart, DateTime? publishDateTimeDiapazonEnd, bool? isVisible, bool? isImportant)
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.GetByCompositeSearch(caption,text, publishDateTimeDiapazonStart, publishDateTimeDiapazonEnd, isVisible, isImportant));
        }

        public async Task<IEnumerable<NewsDTO>> GetLastActiveToQuantinyPrioritizeIncludeImportant(int quantity)
        {
            var mapper = News_NewsDTOMapConfig.CreateMapper();
            return mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(await Database.News.GetLastActiveToQuantinyPrioritizeIncludeImportant(quantity));
        }
    }
}
