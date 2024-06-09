namespace TouragencyWebApi.BLL.DTO
{
    public class TouragencyAccountRoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<int>? TouragencyEmployeeAccountsIds { get; set; }
        public ICollection<int>? ClientsIds { get; set; }
    }
}
