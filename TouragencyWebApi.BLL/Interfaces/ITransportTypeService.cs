﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DAL.Entities;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface ITransportTypeService
    {
        Task<IEnumerable<TransportTypeDTO>> GetAll();
        Task<TransportTypeDTO?> GetById(int id);
        Task<IEnumerable<TransportTypeDTO>> GetByNameSubstring(string nameSubstring);
        Task<IEnumerable<TransportTypeDTO>> GetByDescriptionSubstring(string descriptionSubstring);
        Task<IEnumerable<TransportTypeDTO>> GetByTourId(long tourId);
        Task<IEnumerable<TransportTypeDTO>> GetByTourName(string tourname);
        Task<IEnumerable<TransportTypeDTO>> GetByCompositeSearch(string? nameSubstring, string? descriptionSubstring,
            long? tourId, string? tourname);
        Task<TransportTypeDTO> Create(TransportTypeDTO transportType);
        Task<TransportTypeDTO> Update(TransportTypeDTO transportType);
        Task<TransportTypeDTO> Delete(int id);
    }
}
