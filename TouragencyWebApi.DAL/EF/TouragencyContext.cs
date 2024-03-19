using Microsoft.EntityFrameworkCore;
using TouragencyWebApi.DAL.Entities;
// Creating a context class Touragency that inherits from DbContext, and a DbSet Tours property for the entity set.
namespace TouragencyWebApi.DAL.EF
{
    public class TouragencyContext : DbContext
    {
        public DbSet<BedConfiguration> BedConfigurations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingChildren> BookingChildrens { get; set; }
        public DbSet<BookingData> BookingDatas { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<HotelConfiguration> HotelConfigurations { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Resort> Resorts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewImage> ReviewImages { get; set; }
        public DbSet<Settlement> Settlements { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TouragencyAccount> TouragencyAccounts { get; set; }
        public DbSet<TouragencyAccountRole> TouragencyAccountRoles { get; set; }
        public DbSet<TouragencyEmployee> TouragencyEmployees { get; set; }
        public DbSet<TourName> TourNames { get; set; }
        public DbSet<TourState> TourStates { get; set; }
        
        

       
                
        // The overrided OnConfiguring method is used to configure UseLazyLoadingProxies and UseSqlServer.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TouragencyDb;Trusted_Connection=True;");
        }
        public TouragencyContext(DbContextOptions<TouragencyContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
               
                Countries.Add(new Country { Name = "Австралія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b9/Flag_of_Australia.svg/160px-Flag_of_Australia.svg.png" });
                Countries.Add(new Country { Name = "Австрія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/41/Flag_of_Austria.svg/120px-Flag_of_Austria.svg.png" });
                Countries.Add(new Country { Name = "Азербайджан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/Flag_of_Azerbaijan.svg/160px-Flag_of_Azerbaijan.svg.png" });
                Countries.Add(new Country { Name = "Албанія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/36/Flag_of_Albania.svg/112px-Flag_of_Albania.svg.png" });
                Countries.Add(new Country { Name = "Алжир", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/77/Flag_of_Algeria.svg/120px-Flag_of_Algeria.svg.png" });
                Countries.Add(new Country { Name = "Ангола", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9d/Flag_of_Angola.svg/120px-Flag_of_Angola.svg.png" });
                Countries.Add(new Country { Name = "Андорра", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/19/Flag_of_Andorra.svg/114px-Flag_of_Andorra.svg.png" });
                Countries.Add(new Country { Name = "Антигуа і Барбуда", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/Flag_of_Antigua_and_Barbuda.svg/120px-Flag_of_Antigua_and_Barbuda.svg.png" });
                Countries.Add(new Country { Name = "Аргентина", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/1a/Flag_of_Argentina.svg/128px-Flag_of_Argentina.svg.png" });
                Countries.Add(new Country { Name = "Афганістан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/Flag_of_the_Taliban.svg/160px-Flag_of_the_Taliban.svg.png" });
                Countries.Add(new Country { Name = "Багамські Острови", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/93/Flag_of_the_Bahamas.svg/160px-Flag_of_the_Bahamas.svg.png" });
                Countries.Add(new Country { Name = "Бангладеш", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/f9/Flag_of_Bangladesh.svg/134px-Flag_of_Bangladesh.svg.png" });
                Countries.Add(new Country { Name = "Барбадос", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/Flag_of_Barbados.svg/120px-Flag_of_Barbados.svg.png" });
                Countries.Add(new Country { Name = "Бахрейн", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Flag_of_Bahrain.svg/134px-Flag_of_Bahrain.svg.png" });
                Countries.Add(new Country { Name = "Білорусь", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/85/Flag_of_Belarus.svg/160px-Flag_of_Belarus.svg.png" });
                Countries.Add(new Country { Name = "Беліз", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/Flag_of_Belize.svg/134px-Flag_of_Belize.svg.png" });
                Countries.Add(new Country { Name = "Бельгія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/65/Flag_of_Belgium.svg/92px-Flag_of_Belgium.svg.png" });
                Countries.Add(new Country { Name = "Бенін", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/0a/Flag_of_Benin.svg/120px-Flag_of_Benin.svg.png" });
                Countries.Add(new Country { Name = "Болгарія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Flag_of_Bulgaria.svg/134px-Flag_of_Bulgaria.svg.png" });
                Countries.Add(new Country { Name = "Болівія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Flag_of_Bolivia.svg/118px-Flag_of_Bolivia.svg.png" });
                Countries.Add(new Country { Name = "Боснія і Герцеговина", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/bf/Flag_of_Bosnia_and_Herzegovina.svg/160px-Flag_of_Bosnia_and_Herzegovina.svg.png" });
                Countries.Add(new Country { Name = "Ботсвана", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Flag_of_Botswana.svg/120px-Flag_of_Botswana.svg.png" });
                Countries.Add(new Country { Name = "Бразилія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/05/Flag_of_Brazil.svg/114px-Flag_of_Brazil.svg.png" });
                Countries.Add(new Country { Name = "Бруней", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Flag_of_Brunei.svg/160px-Flag_of_Brunei.svg.png" });
                Countries.Add(new Country { Name = "Буркіна-Фасо", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Flag_of_Burkina_Faso.svg/120px-Flag_of_Burkina_Faso.svg.png" });
                Countries.Add(new Country { Name = "Бурунді", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/50/Flag_of_Burundi.svg/134px-Flag_of_Burundi.svg.png" });
                Countries.Add(new Country { Name = "Бутан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/91/Flag_of_Bhutan.svg/120px-Flag_of_Bhutan.svg.png" });
                Countries.Add(new Country { Name = "Вануату", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Flag_of_Vanuatu.svg/134px-Flag_of_Vanuatu.svg.png" });
                Countries.Add(new Country { Name = "Ватикан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Flag_of_the_Vatican_City_%282001%E2%80%932023%29.svg/80px-Flag_of_the_Vatican_City_%282001%E2%80%932023%29.svg.png" });
                Countries.Add(new Country { Name = "Велика Британія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/Flag_of_the_United_Kingdom_%283-5%29.svg/134px-Flag_of_the_United_Kingdom_%283-5%29.svg.png" });
                Countries.Add(new Country { Name = "В'єтнам", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/21/Flag_of_Vietnam.svg/120px-Flag_of_Vietnam.svg.png" });
                Countries.Add(new Country { Name = "Вірменія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Flag_of_Armenia.svg/160px-Flag_of_Armenia.svg.png" });
                Countries.Add(new Country { Name = "Венесуела", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Flag_of_Venezuela.svg/120px-Flag_of_Venezuela.svg.png" });
                Countries.Add(new Country { Name = "Габон", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/Flag_of_Gabon.svg/107px-Flag_of_Gabon.svg.png" });
                Countries.Add(new Country { Name = "Гаїті", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/56/Flag_of_Haiti.svg/134px-Flag_of_Haiti.svg.png" });
                Countries.Add(new Country { Name = "Гаяна", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Flag_of_Guyana.svg/134px-Flag_of_Guyana.svg.png" });
                Countries.Add(new Country { Name = "Гамбія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/77/Flag_of_The_Gambia.svg/120px-Flag_of_The_Gambia.svg.png" });
                Countries.Add(new Country { Name = "Гана", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/19/Flag_of_Ghana.svg/120px-Flag_of_Ghana.svg.png" });
                Countries.Add(new Country { Name = "Гватемала", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/ec/Flag_of_Guatemala.svg/128px-Flag_of_Guatemala.svg.png" });
                Countries.Add(new Country { Name = "Гвінея", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/ed/Flag_of_Guinea.svg/120px-Flag_of_Guinea.svg.png" });
                Countries.Add(new Country { Name = "Гвінея-Бісау", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Flag_of_Guinea-Bissau.svg/160px-Flag_of_Guinea-Bissau.svg.png" });
                Countries.Add(new Country { Name = "Гондурас", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/82/Flag_of_Honduras.svg/160px-Flag_of_Honduras.svg.png" });
                Countries.Add(new Country { Name = "Гренада", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Flag_of_Grenada.svg/134px-Flag_of_Grenada.svg.png" });
                Countries.Add(new Country { Name = "Греція", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/Flag_of_Greece.svg/120px-Flag_of_Greece.svg.png" });
                Countries.Add(new Country { Name = "Грузія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/0f/Flag_of_Georgia.svg/120px-Flag_of_Georgia.svg.png" });
                Countries.Add(new Country { Name = "Данія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Flag_of_Denmark.svg/106px-Flag_of_Denmark.svg.png" });
                Countries.Add(new Country { Name = "Джибуті", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Flag_of_Djibouti.svg/120px-Flag_of_Djibouti.svg.png" });
                Countries.Add(new Country { Name = "Домініка", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c4/Flag_of_Dominica.svg/160px-Flag_of_Dominica.svg.png" });
                Countries.Add(new Country { Name = "Домініканська Республіка", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Flag_of_the_Dominican_Republic.svg/120px-Flag_of_the_Dominican_Republic.svg.png" });
                Countries.Add(new Country { Name = "Еквадор", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/e8/Flag_of_Ecuador.svg/120px-Flag_of_Ecuador.svg.png" });
                Countries.Add(new Country { Name = "Екваторіальна Гвінея", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Flag_of_Equatorial_Guinea.svg/120px-Flag_of_Equatorial_Guinea.svg.png" });
                Countries.Add(new Country { Name = "Еритрея", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Flag_of_Eritrea.svg/160px-Flag_of_Eritrea.svg.png" });
                Countries.Add(new Country { Name = "Естонія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/8f/Flag_of_Estonia.svg/126px-Flag_of_Estonia.svg.png" });
                Countries.Add(new Country { Name = "Ефіопія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/71/Flag_of_Ethiopia.svg/160px-Flag_of_Ethiopia.svg.png" });
                Countries.Add(new Country { Name = "Єгипет", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fe/Flag_of_Egypt.svg/120px-Flag_of_Egypt.svg.png" });
                Countries.Add(new Country { Name = "Ємен", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/Flag_of_Yemen.svg/120px-Flag_of_Yemen.svg.png" });
                Countries.Add(new Country { Name = "Замбія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Flag_of_Zambia.svg/120px-Flag_of_Zambia.svg.png" });
                Countries.Add(new Country { Name = "Зімбабве", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/6a/Flag_of_Zimbabwe.svg/160px-Flag_of_Zimbabwe.svg.png" });
                Countries.Add(new Country { Name = "Ізраїль", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/Flag_of_Israel.svg/110px-Flag_of_Israel.svg.png" });
                Countries.Add(new Country { Name = "Індія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/41/Flag_of_India.svg/120px-Flag_of_India.svg.png" });
                Countries.Add(new Country { Name = "Індонезія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Flag_of_Indonesia.svg/120px-Flag_of_Indonesia.svg.png" });
                Countries.Add(new Country { Name = "Ірак", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/Flag_of_Iraq.svg/120px-Flag_of_Iraq.svg.png" });
                //Countries.Add(new Country { Name = "Іран", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/ca/Flag_of_Iran.svg/140px-Flag_of_Iran.svg.png" });
                Countries.Add(new Country { Name = "Ірландія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/45/Flag_of_Ireland.svg/160px-Flag_of_Ireland.svg.png" });
                Countries.Add(new Country { Name = "Ісландія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/ce/Flag_of_Iceland.svg/111px-Flag_of_Iceland.svg.png" });
                Countries.Add(new Country { Name = "Іспанія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Flag_of_Spain.svg/120px-Flag_of_Spain.svg.png" });
                Countries.Add(new Country { Name = "Італія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Flag_of_Italy.svg/120px-Flag_of_Italy.svg.png" });
                Countries.Add(new Country { Name = "Йорданія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Flag_of_Jordan.svg/160px-Flag_of_Jordan.svg.png" });
                Countries.Add(new Country { Name = "Кабо-Верде", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/Flag_of_Cape_Verde.svg/136px-Flag_of_Cape_Verde.svg.png" });
                Countries.Add(new Country { Name = "Казахстан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Flag_of_Kazakhstan.svg/160px-Flag_of_Kazakhstan.svg.png" });
                Countries.Add(new Country { Name = "Камбоджа", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/Flag_of_Cambodia.svg/125px-Flag_of_Cambodia.svg.png" });
                Countries.Add(new Country { Name = "Камерун", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/Flag_of_Cameroon.svg/120px-Flag_of_Cameroon.svg.png" });
                Countries.Add(new Country { Name = "Канада", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Flag_of_Canada.svg/160px-Flag_of_Canada.svg.png" });
                Countries.Add(new Country { Name = "Катар", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/65/Flag_of_Qatar.svg/204px-Flag_of_Qatar.svg.png" });
                Countries.Add(new Country { Name = "Кенія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/49/Flag_of_Kenya.svg/120px-Flag_of_Kenya.svg.png" });
                Countries.Add(new Country { Name = "Кіпр", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/Flag_of_Cyprus.svg/120px-Flag_of_Cyprus.svg.png" });
                Countries.Add(new Country { Name = "Киргизстан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/Flag_of_Kyrgyzstan.svg/134px-Flag_of_Kyrgyzstan.svg.png" });
                Countries.Add(new Country { Name = "Кірибаті", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Flag_of_Kiribati.svg/160px-Flag_of_Kiribati.svg.png" });
                Countries.Add(new Country { Name = "Китайська Народна Республіка (КНР)", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/Flag_of_the_People%27s_Republic_of_China.svg/120px-Flag_of_the_People%27s_Republic_of_China.svg.png" });
                Countries.Add(new Country { Name = "Колумбія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/21/Flag_of_Colombia.svg/120px-Flag_of_Colombia.svg.png" });
                Countries.Add(new Country { Name = "Комори", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/94/Flag_of_the_Comoros.svg/134px-Flag_of_the_Comoros.svg.png" });
                Countries.Add(new Country { Name = "Демократична Республіка Конго", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/6f/Flag_of_the_Democratic_Republic_of_the_Congo.svg/107px-Flag_of_the_Democratic_Republic_of_the_Congo.svg.png" });
                Countries.Add(new Country { Name = "Республіка Конго", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/Flag_of_the_Republic_of_the_Congo.svg/120px-Flag_of_the_Republic_of_the_Congo.svg.png" });
                //Countries.Add(new Country { Name = "КНДР (Північна Корея)", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/51/Flag_of_North_Korea.svg/160px-Flag_of_North_Korea.svg.png" });
                Countries.Add(new Country { Name = "Республіка Корея (Південна Корея)", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Flag_of_South_Korea.svg/120px-Flag_of_South_Korea.svg.png" });
                Countries.Add(new Country { Name = "Коста-Рика", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/f2/Flag_of_Costa_Rica.svg/134px-Flag_of_Costa_Rica.svg.png" });
                Countries.Add(new Country { Name = "Кот-д'Івуар", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fe/Flag_of_C%C3%B4te_d%27Ivoire.svg/120px-Flag_of_C%C3%B4te_d%27Ivoire.svg.png" });
                Countries.Add(new Country { Name = "Куба", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Flag_of_Cuba.svg/160px-Flag_of_Cuba.svg.png" });
                Countries.Add(new Country { Name = "Кувейт", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/aa/Flag_of_Kuwait.svg/160px-Flag_of_Kuwait.svg.png" });
                Countries.Add(new Country { Name = "Лаос", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/56/Flag_of_Laos.svg/120px-Flag_of_Laos.svg.png" });
                Countries.Add(new Country { Name = "Латвія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Flag_of_Latvia.svg/160px-Flag_of_Latvia.svg.png" });
                Countries.Add(new Country { Name = "Лесото", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/4a/Flag_of_Lesotho.svg/120px-Flag_of_Lesotho.svg.png" });
                Countries.Add(new Country { Name = "Литва", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/11/Flag_of_Lithuania.svg/134px-Flag_of_Lithuania.svg.png" });
                Countries.Add(new Country { Name = "Ліберія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b8/Flag_of_Liberia.svg/152px-Flag_of_Liberia.svg.png" });
                Countries.Add(new Country { Name = "Ліван", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/Flag_of_Lebanon.svg/120px-Flag_of_Lebanon.svg.png" });
                Countries.Add(new Country { Name = "Лівія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/05/Flag_of_Libya.svg/160px-Flag_of_Libya.svg.png" });
                Countries.Add(new Country { Name = "Ліхтенштейн", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/Flag_of_Liechtenstein.svg/134px-Flag_of_Liechtenstein.svg.png" });
                Countries.Add(new Country { Name = "Люксембург", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/Flag_of_Luxembourg.svg/134px-Flag_of_Luxembourg.svg.png" });
                Countries.Add(new Country { Name = "Маврикій", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/77/Flag_of_Mauritius.svg/120px-Flag_of_Mauritius.svg.png" });
                Countries.Add(new Country { Name = "Мавританія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Flag_of_Mauritania.svg/120px-Flag_of_Mauritania.svg.png" });
                Countries.Add(new Country { Name = "Мадагаскар", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Flag_of_Madagascar.svg/120px-Flag_of_Madagascar.svg.png" });
                Countries.Add(new Country { Name = "Північна Македонія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/Flag_of_North_Macedonia.svg/160px-Flag_of_North_Macedonia.svg.png" });
                Countries.Add(new Country { Name = "Малаві", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d1/Flag_of_Malawi.svg/120px-Flag_of_Malawi.svg.png" });
                Countries.Add(new Country { Name = "Малайзія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/66/Flag_of_Malaysia.svg/160px-Flag_of_Malaysia.svg.png" });
                Countries.Add(new Country { Name = "Малі", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/Flag_of_Mali.svg/120px-Flag_of_Mali.svg.png" });
                Countries.Add(new Country { Name = "Мальдіви", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/0f/Flag_of_Maldives.svg/120px-Flag_of_Maldives.svg.png" });
                Countries.Add(new Country { Name = "Мальта", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/73/Flag_of_Malta.svg/120px-Flag_of_Malta.svg.png" });
                Countries.Add(new Country { Name = "Марокко", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Flag_of_Morocco.svg/120px-Flag_of_Morocco.svg.png" });
                Countries.Add(new Country { Name = "Маршаллові Острови", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/2e/Flag_of_the_Marshall_Islands.svg/152px-Flag_of_the_Marshall_Islands.svg.png" });
                Countries.Add(new Country { Name = "Мексика", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Flag_of_Mexico.svg/140px-Flag_of_Mexico.svg.png" });
                Countries.Add(new Country { Name = "Федеративні Штати Мікронезії (Мікронезія)", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/e4/Flag_of_the_Federated_States_of_Micronesia.svg/152px-Flag_of_the_Federated_States_of_Micronesia.svg.png" });
                Countries.Add(new Country { Name = "Мозамбік", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Flag_of_Mozambique.svg/120px-Flag_of_Mozambique.svg.png" });
                Countries.Add(new Country { Name = "Молдова", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/27/Flag_of_Moldova.svg/160px-Flag_of_Moldova.svg.png" });
                Countries.Add(new Country { Name = "Монако", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/ea/Flag_of_Monaco.svg/100px-Flag_of_Monaco.svg.png" });
                Countries.Add(new Country { Name = "Монголія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/4c/Flag_of_Mongolia.svg/160px-Flag_of_Mongolia.svg.png" });
                Countries.Add(new Country { Name = "М'янма", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/8c/Flag_of_Myanmar.svg/120px-Flag_of_Myanmar.svg.png" });
                Countries.Add(new Country { Name = "Намібія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/00/Flag_of_Namibia.svg/120px-Flag_of_Namibia.svg.png" });
                Countries.Add(new Country { Name = "Науру", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/Flag_of_Nauru.svg/160px-Flag_of_Nauru.svg.png" });
                Countries.Add(new Country { Name = "Непал", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9b/Flag_of_Nepal.svg/66px-Flag_of_Nepal.svg.png" });
                Countries.Add(new Country { Name = "Нігер", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Flag_of_Niger.svg/93px-Flag_of_Niger.svg.png" });
                Countries.Add(new Country { Name = "Нігерія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/Flag_of_Nigeria.svg/160px-Flag_of_Nigeria.svg.png" });
                Countries.Add(new Country { Name = "Нідерланди", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Flag_of_the_Netherlands.svg/120px-Flag_of_the_Netherlands.svg.png" });
                Countries.Add(new Country { Name = "Нікарагуа", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/19/Flag_of_Nicaragua.svg/134px-Flag_of_Nicaragua.svg.png" });
                Countries.Add(new Country { Name = "Німеччина", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Flag_of_Germany.svg/134px-Flag_of_Germany.svg.png" });
                Countries.Add(new Country { Name = "Нова Зеландія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Flag_of_New_Zealand.svg/160px-Flag_of_New_Zealand.svg.png" });
                Countries.Add(new Country { Name = "Норвегія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d9/Flag_of_Norway.svg/110px-Flag_of_Norway.svg.png" });
                Countries.Add(new Country { Name = "Об'єднані Арабські Емірати (OAE)", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/cb/Flag_of_the_United_Arab_Emirates.svg/160px-Flag_of_the_United_Arab_Emirates.svg.png" });
                Countries.Add(new Country { Name = "Оман", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/Flag_of_Oman.svg/160px-Flag_of_Oman.svg.png" });
                Countries.Add(new Country { Name = "Пакистан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Flag_of_Pakistan.svg/120px-Flag_of_Pakistan.svg.png" });
                Countries.Add(new Country { Name = "Палау", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Flag_of_Palau.svg/128px-Flag_of_Palau.svg.png" });
                Countries.Add(new Country { Name = "Панама", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/ab/Flag_of_Panama.svg/120px-Flag_of_Panama.svg.png" });
                Countries.Add(new Country { Name = "Папуа Нова Гвінея", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/e3/Flag_of_Papua_New_Guinea.svg/107px-Flag_of_Papua_New_Guinea.svg.png" });
                Countries.Add(new Country { Name = "Парагвай", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/27/Flag_of_Paraguay.svg/146px-Flag_of_Paraguay.svg.png" });
                Countries.Add(new Country { Name = "Перу", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Flag_of_Peru.svg/120px-Flag_of_Peru.svg.png" });
                Countries.Add(new Country { Name = "Південно-Африканська Республіка (ПАР)", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/af/Flag_of_South_Africa.svg/120px-Flag_of_South_Africa.svg.png" });
                Countries.Add(new Country { Name = "Південний Судан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/Flag_of_South_Sudan.svg/160px-Flag_of_South_Sudan.svg.png" });
                Countries.Add(new Country { Name = "Польща", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/Flag_of_Poland.svg/128px-Flag_of_Poland.svg.png" });
                Countries.Add(new Country { Name = "Португалія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/Flag_of_Portugal.svg/120px-Flag_of_Portugal.svg.png" });
                //Countries.Add(new Country { Name = "Росія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/f3/Flag_of_Russia.svg/120px-Flag_of_Russia.svg.png" });
                Countries.Add(new Country { Name = "Руанда", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/17/Flag_of_Rwanda.svg/120px-Flag_of_Rwanda.svg.png" });
                Countries.Add(new Country { Name = "Румунія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/73/Flag_of_Romania.svg/120px-Flag_of_Romania.svg.png" });
                Countries.Add(new Country { Name = "Сальвадор", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Flag_of_El_Salvador.svg/142px-Flag_of_El_Salvador.svg.png" });
                Countries.Add(new Country { Name = "Самоа", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Flag_of_Samoa.svg/160px-Flag_of_Samoa.svg.png" });
                Countries.Add(new Country { Name = "Сан-Марино", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b1/Flag_of_San_Marino.svg/107px-Flag_of_San_Marino.svg.png" });
                Countries.Add(new Country { Name = "Сан-Томе і Принсіпі", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/0a/Flag_of_S%C3%A3o_Tom%C3%A9_and_Pr%C3%ADncipe.svg/160px-Flag_of_S%C3%A3o_Tom%C3%A9_and_Pr%C3%ADncipe.svg.png" });
                Countries.Add(new Country { Name = "Саудівська Аравія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Flag_of_Saudi_Arabia.svg/120px-Flag_of_Saudi_Arabia.svg.png" });
                Countries.Add(new Country { Name = "Есватіні", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Flag_of_Eswatini.svg/120px-Flag_of_Eswatini.svg.png" });
                Countries.Add(new Country { Name = "Сейшельські Острови", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Flag_of_Seychelles.svg/160px-Flag_of_Seychelles.svg.png" });
                Countries.Add(new Country { Name = "Сенегал", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fd/Flag_of_Senegal.svg/120px-Flag_of_Senegal.svg.png" });
                Countries.Add(new Country { Name = "Сент-Вінсент і Гренадини", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Flag_of_Saint_Vincent_and_the_Grenadines.svg/120px-Flag_of_Saint_Vincent_and_the_Grenadines.svg.png" });
                Countries.Add(new Country { Name = "Сент-Кіттс і Невіс", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fe/Flag_of_Saint_Kitts_and_Nevis.svg/120px-Flag_of_Saint_Kitts_and_Nevis.svg.png" });
                Countries.Add(new Country { Name = "Сент-Люсія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Flag_of_Saint_Lucia.svg/160px-Flag_of_Saint_Lucia.svg.png" });
                Countries.Add(new Country { Name = "Сербія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/ff/Flag_of_Serbia.svg/120px-Flag_of_Serbia.svg.png" });
                Countries.Add(new Country { Name = "Сінгапур", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Flag_of_Singapore.svg/120px-Flag_of_Singapore.svg.png" });
                Countries.Add(new Country { Name = "Сирія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Flag_of_Syria.svg/120px-Flag_of_Syria.svg.png" });
                Countries.Add(new Country { Name = "Словаччина", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/e6/Flag_of_Slovakia.svg/120px-Flag_of_Slovakia.svg.png" });
                Countries.Add(new Country { Name = "Словенія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/f0/Flag_of_Slovenia.svg/160px-Flag_of_Slovenia.svg.png" });
                Countries.Add(new Country { Name = "Соломонові Острови", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/74/Flag_of_the_Solomon_Islands.svg/160px-Flag_of_the_Solomon_Islands.svg.png" });
                Countries.Add(new Country { Name = "Сомалі", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/a0/Flag_of_Somalia.svg/120px-Flag_of_Somalia.svg.png" });
                Countries.Add(new Country { Name = "Сполучені Штати Америки (США)", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Flag_of_the_United_States.svg/152px-Flag_of_the_United_States.svg.png" });
                Countries.Add(new Country { Name = "Судан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Flag_of_Sudan.svg/160px-Flag_of_Sudan.svg.png" });
                Countries.Add(new Country { Name = "Суринам", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/60/Flag_of_Suriname.svg/120px-Flag_of_Suriname.svg.png" });
                Countries.Add(new Country { Name = "Східний Тимор", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/2/26/Flag_of_East_Timor.svg/160px-Flag_of_East_Timor.svg.png" });
                Countries.Add(new Country { Name = "Сьєрра-Леоне", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/17/Flag_of_Sierra_Leone.svg/120px-Flag_of_Sierra_Leone.svg.png" });
                Countries.Add(new Country { Name = "Таджикистан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Flag_of_Tajikistan.svg/160px-Flag_of_Tajikistan.svg.png" });
                Countries.Add(new Country { Name = "Таїланд", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Flag_of_Thailand.svg/120px-Flag_of_Thailand.svg.png" });
                Countries.Add(new Country { Name = "Танзанія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/Flag_of_Tanzania.svg/120px-Flag_of_Tanzania.svg.png" });
                Countries.Add(new Country { Name = "Тонга", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Flag_of_Tonga.svg/160px-Flag_of_Tonga.svg.png" });
                Countries.Add(new Country { Name = "Того", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/68/Flag_of_Togo.svg/130px-Flag_of_Togo.svg.png" });
                Countries.Add(new Country { Name = "Тринідад і Тобаго", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Flag_of_Trinidad_and_Tobago.svg/134px-Flag_of_Trinidad_and_Tobago.svg.png" });
                Countries.Add(new Country { Name = "Тувалу", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/Flag_of_Tuvalu.svg/160px-Flag_of_Tuvalu.svg.png" });
                Countries.Add(new Country { Name = "Туніс", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/ce/Flag_of_Tunisia.svg/120px-Flag_of_Tunisia.svg.png" });
                Countries.Add(new Country { Name = "Туреччина", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b4/Flag_of_Turkey.svg/120px-Flag_of_Turkey.svg.png" });
                Countries.Add(new Country { Name = "Туркменістан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/1b/Flag_of_Turkmenistan.svg/120px-Flag_of_Turkmenistan.svg.png" });
                Countries.Add(new Country { Name = "Уганда", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/Flag_of_Uganda.svg/120px-Flag_of_Uganda.svg.png" });
                Countries.Add(new Country { Name = "Угорщина", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Flag_of_Hungary.svg/160px-Flag_of_Hungary.svg.png" });
                Countries.Add(new Country { Name = "Узбекистан", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/84/Flag_of_Uzbekistan.svg/160px-Flag_of_Uzbekistan.svg.png" });
                Countries.Add(new Country { Name = "Україна", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/49/Flag_of_Ukraine.svg/120px-Flag_of_Ukraine.svg.png" });
                Countries.Add(new Country { Name = "Уругвай", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fe/Flag_of_Uruguay.svg/120px-Flag_of_Uruguay.svg.png" });
                Countries.Add(new Country { Name = "Фіджі", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Flag_of_Fiji.svg/160px-Flag_of_Fiji.svg.png" });
                Countries.Add(new Country { Name = "Філіппіни", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Flag_of_the_Philippines.svg/160px-Flag_of_the_Philippines.svg.png" });
                Countries.Add(new Country { Name = "Фінляндія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Flag_of_Finland.svg/131px-Flag_of_Finland.svg.png" });
                Countries.Add(new Country { Name = "Франція", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/Flag_of_France.svg/120px-Flag_of_France.svg.png" });
                Countries.Add(new Country { Name = "Хорватія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/1b/Flag_of_Croatia.svg/160px-Flag_of_Croatia.svg.png" });
                Countries.Add(new Country { Name = "Центральноафриканська Республіка", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/6f/Flag_of_the_Central_African_Republic.svg/120px-Flag_of_the_Central_African_Republic.svg.png" });
                Countries.Add(new Country { Name = "Чад", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/4b/Flag_of_Chad.svg/120px-Flag_of_Chad.svg.png" });
                Countries.Add(new Country { Name = "Чехія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/cb/Flag_of_the_Czech_Republic.svg/120px-Flag_of_the_Czech_Republic.svg.png" });
                Countries.Add(new Country { Name = "Чилі", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/7/78/Flag_of_Chile.svg/120px-Flag_of_Chile.svg.png" });
                Countries.Add(new Country { Name = "Чорногорія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Flag_of_Montenegro.svg/160px-Flag_of_Montenegro.svg.png" });
                Countries.Add(new Country { Name = "Швеція", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/4/4c/Flag_of_Sweden.svg/128px-Flag_of_Sweden.svg.png" });
                Countries.Add(new Country { Name = "Швейцарія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/f3/Flag_of_Switzerland.svg/80px-Flag_of_Switzerland.svg.png" });
                Countries.Add(new Country { Name = "Шрі-Ланка", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/1/11/Flag_of_Sri_Lanka.svg/160px-Flag_of_Sri_Lanka.svg.png" });
                Countries.Add(new Country { Name = "Ямайка", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/0/0a/Flag_of_Jamaica.svg/160px-Flag_of_Jamaica.svg.png" });
                Countries.Add(new Country { Name = "Японія", FlagUrl = @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/Flag_of_Japan.svg/120px-Flag_of_Japan.svg.png" });

                TouragencyAccountRoles.Add(new TouragencyAccountRole { Name="СУПЕРАДМІНІСТРАТОР", Description="Має повний доступ до всіх операцій" });

                ContactTypes.Add(new ContactType { Description = "Телефон клієнта" });
                ContactTypes.Add(new ContactType { Description = "Телефон робітника турагенства" });
                ContactTypes.Add(new ContactType { Description = "Email клієнта" });
                ContactTypes.Add(new ContactType { Description = "Email робітника турагенства" });
                SaveChanges();
                
            }
        }
    }
}
