namespace TouragencyWebApi.DTO
{
    public class ClientRegisterDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Middlename { get; set; }
        public string TouristNickname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
