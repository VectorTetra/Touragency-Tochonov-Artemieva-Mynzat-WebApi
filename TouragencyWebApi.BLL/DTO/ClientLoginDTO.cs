using System.ComponentModel.DataAnnotations;

namespace TouragencyWebApi.DTO
{
    public class ClientLoginDTO
    {
        /*
         Є кілька варіантів входу в систему:
            - За номером телефону
            - За електронною поштою
            - За ніком туриста
        */
        public string? TouristNickname { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
