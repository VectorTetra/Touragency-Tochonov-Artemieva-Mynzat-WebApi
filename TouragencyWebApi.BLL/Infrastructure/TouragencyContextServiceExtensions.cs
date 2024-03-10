using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.DAL.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace TouragencyWebApi.BLL.Infrastructure
{
    public static class TouragencyContextServiceExtensions
    {
        public static void AddTouragencyContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<TouragencyContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connection));
        }
    }
}
