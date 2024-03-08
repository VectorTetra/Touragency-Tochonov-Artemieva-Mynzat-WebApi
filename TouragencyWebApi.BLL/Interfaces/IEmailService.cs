using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouragencyWebApi.BLL.DTO;
using TouragencyWebApi.DTO;

namespace TouragencyWebApi.BLL.Interfaces
{
    public interface IEmailService
    {
        Task TryToAddNewEmail(EmailDTO emailDTO);
        void Update(EmailDTO emailDTO);
        Task Delete(int id);
    }
}

