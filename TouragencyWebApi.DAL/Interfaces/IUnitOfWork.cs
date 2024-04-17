﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }
        IEmailRepository Emails { get; }
        IPhoneRepository Phones { get; }
        IPersonRepository Persons { get; }
        ICountriesRepository Countries { get; }
        ISettlementsRepository Settlements { get; }
        ITourStateRepository TourStates { get; }
        //IBookingRepository Bookings { get; }
        //IHotelRepository Hotels { get; }
        //IRoomRepository Rooms { get; }
        //IRoomTypeRepository RoomTypes { get; }
        //IBedConfigurationRepository BedConfigurations { get; }
        //IRoomConfigurationRepository RoomConfigurations { get; }
        //IHotelRoomRepository HotelRooms { get; }
        ITouragencyAccountRepository TouragencyAccounts { get; }
        ITouragencyAccountRoleRepository TouragencyAccountRoles { get; }
        ITouragencyEmployeeRepository TouragencyEmployees { get; }
        Task Save();
    }
}
