﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.Entities;
namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IPositionRepository
    {
        Task Create(Position position);
        void Update(Position position);
        Task Delete(int id);
        Task<IEnumerable<Position>> GetAll();
        Task<IEnumerable<Position>> Get200Last();
        Task<IEnumerable<Position>> GetByDescriptionSubstring(string positionDescriptionSubstring);
        Task<IEnumerable<Position>> GetByNameSubstring(string positionNameSubstring);
        Task<IEnumerable<Position>> GetByPersonFirstnameSubstring(string personFirstnameSubstring);
        Task<IEnumerable<Position>> GetByPersonLastnameSubstring(string personLastnameSubstring);
        Task<IEnumerable<Position>> GetByPersonMiddlenameSubstring(string personMiddlenameSubstring);
        Task<IEnumerable<Position>> GetByCompositeSearch(string? positionNameSubstring, string? positionDescriptionSubstring,
            string? personFirstnameSubstring, string? personLastnameSubstring, string? personMiddlenameSubstring);
        Task<Position?> GetByPersonId(int id);
        Task<Position?> GetByTouragencyEmployeeId(int id);
        Task<Position?> GetById(int id);

    }
}
