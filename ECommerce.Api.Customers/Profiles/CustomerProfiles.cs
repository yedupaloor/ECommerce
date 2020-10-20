namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerProfiles : AutoMapper.Profile
    {
        public CustomerProfiles()
        {
            CreateMap<Db.Customer, Models.Customer>();
        }
    }
}
