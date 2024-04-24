﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace TouragencyWebApi.DAL.Repositories
{
    public class TouragencyEmployeeRepository : ITouragencyEmployeeRepository
    {
        private readonly TouragencyContext _context;
        public TouragencyEmployeeRepository(TouragencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TouragencyEmployee>> GetAll()
        {
            return await _context.TouragencyEmployees.ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployee>> Get200Last()
        {
            return await _context.TouragencyEmployees
                .OrderByDescending(p => p.Id)
                .Take(200)
                .ToListAsync();
        }
        public async Task<TouragencyEmployee?> GetById(int id)
        {
            return await _context.TouragencyEmployees.FindAsync(id);
        }

        public async Task<IEnumerable<TouragencyEmployee>> GetByFirstname(string firstname)
        {
            return await _context.TouragencyEmployees
                .Where(p => p.Person.Firstname.Contains(firstname))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployee>> GetByLastname(string lastname)
        {
            return await _context.TouragencyEmployees
                .Where(p => p.Person.Lastname.Contains(lastname))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployee>> GetByMiddlename(string middlename)
        {
            return await _context.TouragencyEmployees
                .Where(p => p.Person.Middlename.Contains(middlename))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<TouragencyEmployee>> GetByPositionName(string positionName)
        {
            return await _context.TouragencyEmployees
                .Where(p => p.Position.Name.Contains(positionName))
                .ToListAsync();
        }
        public async Task<IEnumerable<TouragencyEmployee>> GetByPositionDescription(string positionDescription)
        {
            return await _context.TouragencyEmployees
                .Where(p => p.Position.Description.Contains(positionDescription))
                .ToListAsync();
        }

        public async Task<IEnumerable<TouragencyEmployee>> GetByCompositeSearch(string? firstname, string? lastname,
                       string? middlename, string? positionName, string? positionDescription)
        {
            var collec = new List<IEnumerable<TouragencyEmployee>>();

            if (firstname != null)
            { 
                collec.Add(await GetByFirstname(firstname));
            }
            if (lastname != null)
            {
                collec.Add(await GetByLastname(lastname));
            }
            if (middlename != null)
            {
                collec.Add(await GetByMiddlename(middlename));
            }
            if (positionName != null)
            {
                collec.Add(await GetByPositionName(positionName));
            }
            if (positionDescription != null)
            {
                collec.Add(await GetByPositionDescription(positionDescription));
            }
            if(!collec.Any())
            {
                return new List<TouragencyEmployee>();
            }
            return collec.Aggregate((a, b) => a.Intersect(b));
        }
        public async Task Create(TouragencyEmployee employee)
        {
            await _context.TouragencyEmployees.AddAsync(employee);
        }
        public void Update(TouragencyEmployee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            TouragencyEmployee? employee = await _context.TouragencyEmployees.FindAsync(id);
            if (employee != null)
                _context.TouragencyEmployees.Remove(employee);
        }
    }
}
