namespace TouragencyWebApi.DAL.Entities
{
    public class TouragencyEmployeeAccount
    {
        // Creating a model TouragencyAccount with properties
        // Id, Login, Password, TouragencyAccountRole, TouragencyAccountRoleId, TouragencyEmployee, TouragencyEmployeeId
        
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public virtual TouragencyAccountRole TouragencyAccountRole { get; set; }
        public int TouragencyAccountRoleId { get; set; }
        public virtual TouragencyEmployee TouragencyEmployee { get; set; }
        public int TouragencyEmployeeId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (TouragencyEmployeeAccount)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
