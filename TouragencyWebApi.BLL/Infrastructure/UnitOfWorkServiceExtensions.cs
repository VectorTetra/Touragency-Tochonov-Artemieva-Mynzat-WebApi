using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TouragencyWebApi.DAL.Interfaces;
using TouragencyWebApi.DAL.Repositories;
using TouragencyWebApi.DAL.UnitOfWork;

namespace TouragencyWebApi.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
