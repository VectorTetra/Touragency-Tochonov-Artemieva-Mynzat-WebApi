using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Repositories
{
    public class BookingDataRepository: IBookingDataRepository
    {
        private readonly TouragencyContext _context;

        public BookingDataRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BookingData>> GetAll()
        {
            return await _context.BookingDatas.ToListAsync();
        }

        public async Task<BookingData?> GetById(long id)
        {
            return await _context.BookingDatas.FindAsync(id);
        }

        public async Task<IEnumerable<BookingData>> GetByBookingId(long bookingId)
        {
            return await _context.BookingDatas.Where(bd => bd.BookingId == bookingId).ToListAsync();
        }

        public async Task<IEnumerable<BookingData>> GetByRoomNumber(int roomNumber)
        {
            return await _context.BookingDatas.Where(bd => bd.RoomNumber == roomNumber).ToListAsync();
        }

        public async Task<IEnumerable<BookingData>> GetByDateDiapazon(DateTime dateBeginPeriod, DateTime dateEndPeriod)
        {
            return await _context.BookingDatas.Where(bd => bd.DateBeginPeriod >= dateBeginPeriod && bd.DateEndPeriod <= dateEndPeriod).ToListAsync();
        }

        public async Task<IEnumerable<BookingData>> GetByTotalPriceDiapazon(int priceMinValue, int priceMaxValue)
        {
            return await _context.BookingDatas.Where(bd => bd.TotalPrice >= priceMinValue && bd.TotalPrice <= priceMaxValue).ToListAsync();
        }

        public async Task<IEnumerable<BookingData>> GetByAdultsCount(short adultsCount)
        {
            return await _context.BookingDatas.Where(bd => bd.AdultsCount == adultsCount).ToListAsync();
        }

        public async Task<IEnumerable<BookingData>> GetByBookingIdRoomNumber(long bookingId, int roomNumber)
        {
            return await _context.BookingDatas.Where(bd => bd.BookingId == bookingId && bd.RoomNumber == roomNumber).ToListAsync();
        }

        public async Task<IEnumerable<BookingData>> GetByBookingChildrenId(long bookingChildrenId)
        {
            return await _context.BookingDatas.Where(bd => bd.BookingChildren.Any(bch=>bch.Id == bookingChildrenId)).ToListAsync();
        }

        public async Task<IEnumerable<BookingData>> GetByBedConfigurationId(int bedConfigurationId)
        {
            return await _context.BookingDatas.Where(bd => bd.BedConfiguration.Id == bedConfigurationId).ToListAsync();
        }

        public async Task Create(BookingData bookingData)
        {
            await _context.BookingDatas.AddAsync(bookingData);
        }

        public void Update(BookingData bookingData)
        {
            _context.Entry(bookingData).State = EntityState.Modified;
        }

        public async Task Delete(long id)
        {
            var bookingData = await _context.BookingDatas.FindAsync(id);
            if (bookingData != null)
            {
                _context.BookingDatas.Remove(bookingData);
            }
        }
    }
}
