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
    public class BookingChildrenService: IBookingChildrenService
    {
        IUnitOfWork Database;
        public BookingChildrenService(IUnitOfWork uow)
        {
            Database = uow;
        }
        MapperConfiguration BookingChildren_BookingChildrenDTOMapConfig = new MapperConfiguration(cfg => cfg.CreateMap<BookingChildren, BookingChildrenDTO>()
        .ForMember("Id", opt => opt.MapFrom(c => c.Id))
        .ForMember("BookingDataId", opt => opt.MapFrom(c => c.BookingDataId))
        .ForMember("ChildrenCount", opt => opt.MapFrom(c => c.ChildrenCount))
        .ForMember("ChildrenAge", opt => opt.MapFrom(c => c.ChildrenAge))
        );
        public async Task<IEnumerable<BookingChildrenDTO>> GetAll()
        {
            IMapper mapper = new Mapper(BookingChildren_BookingChildrenDTOMapConfig);
            return mapper.Map<IEnumerable<BookingChildren>, IEnumerable<BookingChildrenDTO>>(await Database.BookingChildrens.GetAll());
        }

        public async Task<BookingChildrenDTO> GetById(long id)
        {
            IMapper mapper = new Mapper(BookingChildren_BookingChildrenDTOMapConfig);
            return mapper.Map<BookingChildren, BookingChildrenDTO>(await Database.BookingChildrens.GetById(id));
        }

        public async Task<IEnumerable<BookingChildrenDTO>> GetByBookingDataId(long bookingId)
        {
            IMapper mapper = new Mapper(BookingChildren_BookingChildrenDTOMapConfig);
            return mapper.Map<IEnumerable<BookingChildren>, IEnumerable<BookingChildrenDTO>>(await Database.BookingChildrens.GetByBookingDataId(bookingId));
        }

        public async Task<IEnumerable<BookingChildrenDTO>> GetByChildrenCount(short childrenCount)
        {
            IMapper mapper = new Mapper(BookingChildren_BookingChildrenDTOMapConfig);
            return mapper.Map<IEnumerable<BookingChildren>, IEnumerable<BookingChildrenDTO>>(await Database.BookingChildrens.GetByChildrenCount(childrenCount));    
        }

        public async Task<IEnumerable<BookingChildrenDTO>> GetByChildrenAge(short childrenAge)
        {
            IMapper mapper = new Mapper(BookingChildren_BookingChildrenDTOMapConfig);
            return mapper.Map<IEnumerable<BookingChildren>, IEnumerable<BookingChildrenDTO>>(await Database.BookingChildrens.GetByChildrenAge(childrenAge));
        }

        public async Task<BookingChildrenDTO> Create(BookingChildrenDTO bookingChildrenDTO)
        {
            var busyBookingChildrenId = await Database.BookingChildrens.GetById(bookingChildrenDTO.Id);
            if (busyBookingChildrenId != null)
            {
                throw new ValidationException($"Такий bookingChildrenId вже зайнято! (BookingChildrenId : {busyBookingChildrenId.Id})", "");
            }
            if (bookingChildrenDTO.BookingDataId != null)
            {
                var PreExistedBookingData = await Database.BookingDatas.GetById((long)bookingChildrenDTO.BookingDataId);
                if (PreExistedBookingData == null)
                {
                    throw new ValidationException($"Такий bookingChildrenDTO.BookingDataId не знайдено! (bookingChildrenDTO.BookingDataId : {bookingChildrenDTO.BookingDataId})", "");
                }
            }
            BookingChildren bookingChildren = new BookingChildren
            {
                BookingDataId = bookingChildrenDTO.BookingDataId,
                ChildrenCount = bookingChildrenDTO.ChildrenCount,
                ChildrenAge = bookingChildrenDTO.ChildrenAge
            };

            await Database.BookingChildrens.Create(bookingChildren);
            await Database.Save();
            bookingChildrenDTO.Id = bookingChildren.Id;
            return bookingChildrenDTO;
        }

        public async Task<BookingChildrenDTO> Update(BookingChildrenDTO bookingChildrenDTO)
        {
            var BookingChildren = await Database.BookingChildrens.GetById(bookingChildrenDTO.Id);
            if (BookingChildren == null)
            {
                throw new ValidationException($"Такий bookingChildrenId не знайдено! (bookingChildrenDTO.Id : {bookingChildrenDTO.Id})", "");
            }
            if (bookingChildrenDTO.BookingDataId != null)
            {
                var PreExistedBookingData = await Database.BookingDatas.GetById((long)bookingChildrenDTO.BookingDataId);
                if (PreExistedBookingData == null)
                {
                    throw new ValidationException($"Такий bookingChildrenDTO.BookingDataId не знайдено! (bookingChildrenDTO.BookingDataId : {bookingChildrenDTO.BookingDataId})", "");
                }
            }

            BookingChildren.BookingDataId = bookingChildrenDTO.BookingDataId;
            BookingChildren.ChildrenCount = bookingChildrenDTO.ChildrenCount;
            BookingChildren.ChildrenAge = bookingChildrenDTO.ChildrenAge;
            

            Database.BookingChildrens.Update(BookingChildren);
            await Database.Save();
            return bookingChildrenDTO;
        }

        public async Task<BookingChildrenDTO> Delete(long id)
        {
            var BookingChildren = await Database.BookingChildrens.GetById(id);
            if (BookingChildren == null)
            {
                throw new ValidationException("Такий bookingChildrenId не знайдено!", "");
            }
            var dto = await GetById(id);
            await Database.BookingChildrens.Delete(id);
            await Database.Save();
            return dto;
        }
    }
}
