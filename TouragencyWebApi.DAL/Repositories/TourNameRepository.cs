﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Entities;
using TouragencyWebApi.DAL.Interfaces;

namespace TouragencyWebApi.DAL.Repositories
{
    public class TourNameRepository: ITourNameRepository 
    {
        private readonly TouragencyContext _context;
        public TourNameRepository(TouragencyContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TourName>> GetAll()
        {
            return await _context.TourNames.ToListAsync();
        }

        public async Task<TourName?> GetById(int id)
        {
            return await _context.TourNames.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TourName>> GetByName(string tourNameSubstring)
        {
            return await _context.TourNames.Where(t => t.Name.Contains(tourNameSubstring)).ToListAsync();
        }

        public async Task<IEnumerable<TourName>> GetByPageJSONStructureUrlSubstring(string pageJSONStructureUrlSubstring)
        {
            return await _context.TourNames.Where(t => t.PageJSONStructureUrl.Contains(pageJSONStructureUrlSubstring)).ToListAsync();
        }


        public async Task<IEnumerable<TourName>> GetByTourId(long tourId)
        {
            return await _context.TourNames.Where(t => t.Tours.Any(tt => tt.Id == tourId) ).ToListAsync();
        }

        public async Task<IEnumerable<TourName>> GetByTourImageId(long tourImageId)
        {
            return await _context.TourNames.Where(t => t.TourImages.Any(ti=> ti.Id == tourImageId)).ToListAsync();
        }

        public async Task Create(TourName tourName)
        {
            await _context.TourNames.AddAsync(tourName);
        }

        public void Update(TourName tourName)
        {
            _context.Entry(tourName).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var tourName = await _context.TourNames.FindAsync(id);
            if (tourName != null)
            {
                _context.TourNames.Remove(tourName);
            }
        }
    }
}
