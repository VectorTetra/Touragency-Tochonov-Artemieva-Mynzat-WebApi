namespace TouragencyWebApi.DAL.Entities
{
    public class TouragencyAccountRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TouragencyEmployeeAccount>? TouragencyEmployeeAccounts { get; set; }
        public virtual ICollection<Client>? Clients { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otheBC = (TouragencyAccountRole)obj;
            return Id == otheBC.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
