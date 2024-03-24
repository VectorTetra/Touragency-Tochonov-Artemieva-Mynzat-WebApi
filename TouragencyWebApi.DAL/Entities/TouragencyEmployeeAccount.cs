namespace TouragencyWebApi.DAL.Entities
{
    public class TouragencyEmployeeAccount
    {
        // Creating a model TouragencyAccount with properties
        // Id, Login, Password, TouragencyAccountRole, TouragencyAccountRoleId, TouragencyEmployee, TouragencyEmployeeId
        
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual TouragencyAccountRole TouragencyAccountRole { get; set; }
        public int TouragencyAccountRoleId { get; set; }
        public virtual TouragencyEmployee TouragencyEmployee { get; set; }
        public int TouragencyEmployeeId { get; set; }
    }
}
