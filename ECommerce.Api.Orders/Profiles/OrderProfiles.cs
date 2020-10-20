using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrderProfiles : AutoMapper.Profile
    {
        public OrderProfiles()
        {
            CreateMap<Db.Order, Models.Order>();
            CreateMap<Db.OrderItem, Models.OrderItem>();
            //CreateMap<Db.Item, Models.Item>();
        }
    }
}
