using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace TouragencyWebApi.DAL.Repositories
{
    public class BookingChildrenRepository: IBookingChildrenRepository
    {
        private readonly TouragencyContext _context;
        public BookingChildrenRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BookingChildren>> GetAll()
        {
            return await _context.BookingChildrens.ToListAsync();
        }

        public async Task<BookingChildren?> GetById(long id)
        {
            return await _context.BookingChildrens.FirstOrDefaultAsync(bc => bc.Id == id);
        }

        public async Task<IEnumerable<BookingChildren>> GetByBookingDataId(long bookingId)
        {
            return await _context.BookingChildrens.Where(bc => bc.BookingDataId == bookingId).ToListAsync();
        }

        public async Task<IEnumerable<BookingChildren>> GetByChildrenCount(short childrenCount)
        {
            return await _context.BookingChildrens.Where(bc => bc.ChildrenCount == childrenCount).ToListAsync();
        }

        public async Task<IEnumerable<BookingChildren>> GetByChildrenAge(short childrenAge)
        {
            return await _context.BookingChildrens.Where(bc => bc.ChildrenAge == childrenAge).ToListAsync();
        }

        public async Task Create(BookingChildren bookingChildren)
        {
            await _context.BookingChildrens.AddAsync(bookingChildren);
        }

        public void Update(BookingChildren bookingChildren)
        {
            _context.BookingChildrens.Update(bookingChildren);
        }

        public async Task Delete(long id)
        {
            var bookingChildren = await GetById(id);
            if (bookingChildren != null)
            {
                _context.BookingChildrens.Remove(bookingChildren);
            }
        }
    }
}
